using System.Net.Http.Headers;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to retrieve the count of a collection up to 5000.
        /// </summary>
    public sealed class GetCollectionCountRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the GetCollectionCountRequest
        /// </summary>
        /// <param name="collectionPath">path to the 集合 to count</param>
        public GetCollectionCountRequest(string collectionPath)
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri(
                uriString: $"{collectionPath}/$count",
                uriKind: UriKind.Relative);
        }
    }
}
