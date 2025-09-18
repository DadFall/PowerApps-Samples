using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to create and retrieve a record.
        /// </summary>
    public sealed class CreateRetrieveRequest : HttpRequestMessage
    {

        /// <summary>
        /// 初始化 a CreateRetrieveRequest
        /// </summary>
        /// <param name="entitySetName">名称 of the entity set</param>
        /// <param name="record">record to create.</param>
        /// <param name="query">query for 数据 to return.</param>
        /// <param name="includeAnnotations">Whether the 结果s should include annotations</param>
        public CreateRetrieveRequest(string entitySetName, JObject record, string? query, bool includeAnnotations = false)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(uriString: $"{entitySetName}{query}", uriKind: UriKind.Relative);
            Content = new StringContent(
                content: record.ToString(),
                encoding: System.Text.Encoding.UTF8,
                mediaType: "application/json");
            if (includeAnnotations)
            {
                Headers.Add("Prefer", "odata.include-annotations=\"*\"");
            }
            Headers.Add("Prefer", "return=representation");
        }
    }
}
