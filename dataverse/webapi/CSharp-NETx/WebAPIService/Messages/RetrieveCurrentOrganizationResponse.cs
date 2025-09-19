using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PowerApps.Samples.Types;

namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the response from the RetrieveCurrentOrganizationRequest
        /// </summary>
    public sealed class RetrieveCurrentOrganizationResponse : HttpResponseMessage
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
        /// 获取 detailed information about the current organization.
        /// </summary>
        public OrganizationDetail Detail
        {
            get
            {
                return JsonConvert.DeserializeObject<OrganizationDetail>(_jObject["Detail"].ToString());
            }
        }
    }
}
