namespace PowerApps.Samples.Metadata.Messages
{
    /// <summary>
        /// Contains the data to delete a table.
        /// </summary>
    public sealed class DeleteEntityRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the DeleteEntityRequest
        /// </summary>
        /// <param name="logicalName">logical 名称 of the table.</param>
        /// <param name="strongConsistency">Whether to apply strong consistency header to the 请求.</param>
        public DeleteEntityRequest(string logicalName, bool strongConsistency = false)
        {
            Method = HttpMethod.Delete;
            RequestUri = new Uri(uriString: $"EntityDefinitions(LogicalName='{logicalName}')", uriKind: UriKind.Relative);
            if (strongConsistency)
            {
                Headers.Add("Consistency", "Strong");
            }
        }
    }
}
