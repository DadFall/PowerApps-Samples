using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Metadata.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the response from the GetValidManyToMany Function
        /// </summary>
    public sealed class GetValidManyToManyResponse : HttpResponseMessage
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
        /// array of valid entity names that can be the related entity in a many-to-many relationship.
        /// </summary>
        public string[] EntityNames => JsonConvert.DeserializeObject<string[]>(_jObject[nameof(EntityNames)].ToString());
    }
}
