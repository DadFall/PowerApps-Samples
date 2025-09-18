using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using PowerApps.Samples.Messages;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;


namespace PowerApps.Samples
{
    public class Service : IDisposable
    {
        // 传递给构造函数的服务配置数据
        private readonly Config config;

        private static IServiceProvider _serviceProvider { get; set; }
        private readonly string WebAPIClientName = "WebAPI";
        private bool _disposedValue;
        private string? _sessionToken = null;
        private int _recommendedDegreeOfParallelism;
        private Guid _userId;
        private Guid _businessUnitId;
        private Guid _organizationId;


        /// <summary>
        /// 服务的构造函数
        /// </summary>
        /// <param name="configParam"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public Service(Config configParam)
        {
            config = configParam;
            BaseAddress = new Uri($"{config.Url}/api/data/v{config.Version}/");

            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices(services =>
            {
                if (config.DisableCookies)
                {
                    //不使用 cookie
                    services.AddHttpClient(
                        name: WebAPIClientName,
                        configureClient: ConfigureHttpClient
                        )
                    .ConfigurePrimaryHttpMessageHandler(() =>
                        new HttpClientHandler
                        {
                            UseCookies = false
                        }
                    ).AddPolicyHandler(GetRetryPolicy(config));

                }
                else //使用 cookie
                {
                    services.AddHttpClient(
                        name: WebAPIClientName,
                        configureClient: ConfigureHttpClient)
                    .AddPolicyHandler(GetRetryPolicy(config));
                }
            });


            builder.ConfigureLogging(logging => {
                // 删除默认日志记录提供程序
                // 以便输出不会发送到控制台。
                // 您可能希望启用日志记录。
                // 更多信息：
                // https://learn.microsoft.com/dotnet/core/extensions/logging-providers
                logging.ClearProviders();
            });

            // 将命名的 HttpClient 配置添加到服务提供程序。
            _serviceProvider = builder.Build().Services;

            // 发送简单请求以访问推荐的并行度 (DOP)。
            var whoAmIResponse =  SendAsync<WhoAmIResponse>(new WhoAmIRequest()).GetAwaiter().GetResult();
            _recommendedDegreeOfParallelism = int.Parse(whoAmIResponse.Headers.GetValues("x-ms-dop-hint").FirstOrDefault());
            // 设置用户详细信息
            _userId = whoAmIResponse.UserId;
            _businessUnitId = whoAmIResponse.BusinessUnitId;
            _organizationId = whoAmIResponse.OrganizationId;

        }

        /// <summary>
        /// 在服务构造函数中设置的 HttpClient 配置
        /// </summary>
        /// <param name="httpClient"></param>
        void ConfigureHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = BaseAddress;
            httpClient.Timeout = TimeSpan.FromSeconds(config.TimeoutInSeconds);
            httpClient.DefaultRequestHeaders.Add("User-Agent", $"WebAPIService/{Assembly.GetExecutingAssembly().GetName().Version}");
            // 为所有请求设置默认标头
            // 参见 https://learn.microsoft.com/power-apps/developer/data-platform/webapi/compose-http-requests-handle-errors#http-headers
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("If-None-Match", "null");           
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// 指定重试策略
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(Config config)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(httpResponseMessage => httpResponseMessage.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                    retryCount: config.MaxRetries,
                    sleepDurationProvider: (count, response, context) =>
                    {
                        int seconds;
                        HttpResponseHeaders headers = response.Result.Headers;

                        // 如果存在，则使用 Retry-After 标头的值
                        // 参见 https://learn.microsoft.com/power-apps/developer/data-platform/api-limits#retry-operations

                        if (headers.Contains("Retry-After"))
                        {

                            seconds = int.Parse(headers.GetValues("Retry-After").FirstOrDefault());
                        }
                        else
                        {
                            seconds = (int)Math.Pow(2, count);
                        }
                        return TimeSpan.FromSeconds(seconds);
                    },
                    onRetryAsync: (_, _, _, _) => { return Task.CompletedTask; }
                );
        }

        /// <summary>
        /// 提供从服务提供程序访问 IHttpClientFactory 的方法
        /// </summary>
        /// <returns></returns>
        private static IHttpClientFactory GetHttpClientFactory()
        {
            return (IHttpClientFactory)_serviceProvider.GetService(typeof(IHttpClientFactory));

        }



        /// <summary>
        /// 处理请求并返回响应。管理服务保护限制错误。
        /// </summary>
        /// <param name="request">要发送的请求</param>
        /// <returns>来自 HttpClient 的响应</returns>
        /// <exception cref="Exception"></exception>
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            // 弹性表使用的会话令牌以启用强一致性
            // 参见 https://learn.microsoft.com/power-apps/developer/data-platform/use-elastic-tables?tabs=webapi#sending-the-session-token
            if (!string.IsNullOrWhiteSpace(_sessionToken) && request.Method == HttpMethod.Get) {
                request.Headers.Add("MSCRM.SessionToken", _sessionToken);
            }

            // 使用传递给构造函数的 Config 中的函数设置访问令牌
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await config.GetAccessToken());

            // 从 IHttpClientFactory 获取命名的 HttpClient
            var client = GetHttpClientFactory().CreateClient(WebAPIClientName);

            HttpResponseMessage response = await client.SendAsync(request);

            // 捕获当前会话令牌值
            // 参见 https://learn.microsoft.com/power-apps/developer/data-platform/use-elastic-tables?tabs=webapi#getting-the-session-token
            if (response.Headers.Contains("x-ms-session-token"))
            {
                _sessionToken = response.Headers.GetValues("x-ms-session-token")?.FirstOrDefault()?.ToString();
            }

             //SampleGenerator.WriteHttpSample(request, response, BaseAddress, "H:\\temp\\GeneratedSamples");

            // 如果请求不成功则抛出异常
            if (!response.IsSuccessStatusCode)
            {
                ServiceException exception = await ParseError(response);
                throw exception;
            }
            return response;
        }

        /// <summary>
        /// 处理具有类型化响应的请求
        /// </summary>
        /// <typeparam name="T">从 HttpResponseMessage 派生的类型</typeparam>
        /// <param name="request">请求</param>
        /// <returns></returns>
        public async Task<T> SendAsync<T>(HttpRequestMessage request) where T : HttpResponseMessage
        {
            HttpResponseMessage response = await SendAsync(request);

            // 'As' method is Extension of HttpResponseMessage see Extensions.cs
            return response.As<T>();
        }

        public static async Task<ServiceException> ParseError(HttpResponseMessage response)
        {
            string requestId = string.Empty;
            if (response.Headers.Contains("REQ_ID"))
            {
                requestId = response.Headers.GetValues("REQ_ID").FirstOrDefault();
            }

            var content = await response.Content.ReadAsStringAsync();
            ODataError? oDataError = null;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                oDataError = JsonSerializer.Deserialize<ODataError>(content, options);
            }
            catch (Exception)
            {
                // 错误可能不是正确的 OData 错误格式，所以继续尝试...
            }

            if (oDataError?.Error != null)
            {

                var exception = new ServiceException(oDataError.Error.Message)
                {
                    ODataError = oDataError,
                    Content = content,
                    ReasonPhrase = response.ReasonPhrase,
                    HttpStatusCode = response.StatusCode,
                    RequestId = requestId
                };
                return exception;
            }
            else
            {
                try
                {
                    ODataException oDataException = JsonSerializer.Deserialize<ODataException>(content);

                    ServiceException otherException = new(oDataException.Message)
                    {
                        Content = content,
                        ReasonPhrase = response.ReasonPhrase,
                        HttpStatusCode = response.StatusCode,
                        RequestId = requestId
                    };
                    return otherException;

                }
                catch (Exception)
                {

                }

                //当其他方法都不起作用时
                ServiceException exception = new(response.ReasonPhrase)
                {
                    Content = content,
                    ReasonPhrase = response.ReasonPhrase,
                    HttpStatusCode = response.StatusCode,
                    RequestId = requestId
                };
                return exception;
            }
        }

        /// <summary>
        /// WebAPI HttpClient 的 BaseAddress 属性
        /// </summary>
        public Uri BaseAddress { get; }

        /// <summary>
        /// 连接的推荐并行度
        /// </summary>
        public int RecommendedDegreeOfParallelism {
            get {
                return _recommendedDegreeOfParallelism;
            }
        }

        /// <summary>
        /// 已连接用户的 UserId
        /// </summary>
        public Guid UserId
        {
            get
            {
                return _userId;
            }
        }

        /// <summary>
        /// 已连接用户的 OrganizationId
        /// </summary>
        public Guid OrganizationId
        {
            get
            {
                return _organizationId;
            }
        }

        /// <summary>
        /// 已连接用户的 BusinessUnitId
        /// </summary>
        public Guid BusinessUnitId
        {
            get
            {
                return _businessUnitId;
            }
        }

        ~Service() => Dispose(false);

        // 供使用者调用的 Dispose 模式的公共实现。
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        // Dispose 模式的受保护实现。
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _disposedValue = true;
            }
        }
    }
}