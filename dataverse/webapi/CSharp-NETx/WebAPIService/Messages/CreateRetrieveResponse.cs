using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the data from the a CreateRetrieveRequest
        /// </summary>
    public sealed class CreateRetrieveResponse : HttpResponseMessage
    {
        /// <summary>
        /// record created.
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
