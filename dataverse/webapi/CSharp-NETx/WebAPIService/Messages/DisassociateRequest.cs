namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to disassociate a record
        /// </summary>
    public sealed class DisassociateRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the DisassociateRequest
        /// </summary>
        /// <param name="entityWithCollection">一个record with a 集合.</param>
        /// <param name="collectionName">名称 of the 集合.</param>
        /// <param name="entityToRemove">record to remove.</param>
        public DisassociateRequest(
            EntityReference entityWithCollection,
            string collectionName,
            EntityReference entityToRemove)
        {
            Method = HttpMethod.Delete;
            RequestUri = new Uri(
                uriString: $"{entityWithCollection.Path}/{collectionName}({entityToRemove.Id})/$ref", 
                uriKind: UriKind.Relative);
        }
    }
}
