using Newtonsoft.Json;

namespace PowerApps.Samples.Search.Types
{
    /// <summary>
        /// Result object for suggest results.
        /// </summary>
    public sealed class SuggestResult
    {
        /// <summary>
        /// 获取或设置 the text.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// 获取或设置 document.
        /// </summary>
        [JsonProperty(PropertyName = "document")]
        public Dictionary<string, object> Document { get; set; }
    }
}
