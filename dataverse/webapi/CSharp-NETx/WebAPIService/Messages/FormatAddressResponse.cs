using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the response from the FormatAddressRequest
        /// </summary>
    public sealed class FormatAddressResponse : HttpResponseMessage
    {
        // 缓存the async content
        private string? _content;

        //Provides JObject for property getters
        private JObject _jObject
        {
            get
            {
                _content ??= Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return JObject.Parse(_content);
            }
        }

        /// <summary>
        /// 获取 the formatted address
        /// </summary>
        public string Address => (string)_jObject.GetValue(nameof(Address));
    }
}
