using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the data from the RetrieveRequest
        /// </summary>
    public sealed class RetrieveResponse : HttpResponseMessage
    {
        /// <summary>
        /// record returned.
        /// </summary>
        public JObject Record
        {
            get
            {
                return JObject.Parse(Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }
    }
}
