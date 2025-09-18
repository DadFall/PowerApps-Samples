namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to delete a column value
        /// </summary>
    public sealed class DeleteColumnValueRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 a DeleteColumnValueRequest
        /// </summary>
        /// <param name="entityReference">一个reference to a record that has the property</param>
        /// <param name="propertyName">名称 of the property with the 值 to delete.</param>
        public DeleteColumnValueRequest(EntityReference entityReference, string propertyName)
        {
            Method = HttpMethod.Delete;
            RequestUri = new Uri(
                uriString: $"{entityReference.Path}/{propertyName}",
                uriKind: UriKind.Relative);
        }
    }
}
