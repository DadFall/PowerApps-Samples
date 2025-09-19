namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to retrieve a column value
        /// </summary>
    public sealed class GetColumnValueRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the GetColumnValueRequest
        /// </summary>
        /// <param name="entityReference">一个reference to the record to get the column 数据 from.</param>
        /// <param name="property">名称 of the column.</param>
        public GetColumnValueRequest(EntityReference entityReference, string property)
        {
            Method = HttpMethod.Get;
            RequestUri = new Uri(
                uriString: $"{entityReference.Path}/{property}", 
                uriKind: UriKind.Relative);
        }
    }
}
