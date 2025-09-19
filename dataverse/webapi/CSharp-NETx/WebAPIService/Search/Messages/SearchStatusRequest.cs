namespace PowerApps.Samples.Search.Messages
{
    /// <summary>
        /// Contains the data to perform the searchstatus function
        /// </summary>
    public sealed class SearchStatusRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the searchstatisticsRequest
        /// </summary>
        public SearchStatusRequest()
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri(
                uriString: "searchstatus",
                uriKind: UriKind.Relative);
        }
    }
}
