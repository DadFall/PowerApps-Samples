namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to delete a record
        /// </summary>
    public sealed class DeleteRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the DeleteRequest
        /// </summary>
        /// <param name="entityReference">一个reference to the record to delete.</param>
        /// <param name="partitionId">partition key to use.</param>
        /// <param name="strongConsistency">Whether strong consistency should be applied.</param>
        /// <param name="eTag">current ETag 值 to compare.</param>
        public DeleteRequest(EntityReference entityReference, string? partitionId = null, bool strongConsistency = false, string? eTag = null)
        {
            string path;
            if (partitionId != null)
            {
                path = $"{entityReference.Path}?partitionId={partitionId}";
            }
            else
            {
                path = entityReference.Path;
            }

            Method = HttpMethod.Delete;
            RequestUri = new Uri(
                uriString: path, 
                uriKind: UriKind.Relative);
            if (strongConsistency)
            { 
                Headers.Add("Consistency", "Strong");
            }
            if (eTag != null)
            {
                //Prevent delete if record has changed on the server
                Headers.Add("If-Match", eTag);
            }
        }
    }
}
