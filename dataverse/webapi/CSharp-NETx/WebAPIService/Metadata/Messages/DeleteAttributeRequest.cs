namespace PowerApps.Samples.Metadata.Messages
{
    /// <summary>
        /// Contains the data to delete a column.
        /// </summary>
    public sealed class DeleteAttributeRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the DeleteAttributeRequest
        /// </summary>
        /// <param name="entityLogicalName">logical 名称 of the table.</param>
        /// <param name="logicalName">logical 名称 of the column.</param>
        /// <param name="strongConsistency">Whether to apply strong consistency header to the 请求.</param>
        public DeleteAttributeRequest(string entityLogicalName, string logicalName, bool strongConsistency = false)
        {
            Method = HttpMethod.Delete;
            RequestUri = new Uri(
                uriString: $"EntityDefinitions(LogicalName='{entityLogicalName}')/Attributes(LogicalName='{logicalName}')", 
                uriKind: UriKind.Relative);
            if (strongConsistency)
            {
                Headers.Add("Consistency", "Strong");
            }
        }
    }
}
