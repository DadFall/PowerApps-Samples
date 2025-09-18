namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to retrieve a file column value
    /// 
    public sealed class DownloadFileRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the DownloadFileRequest
        /// </summary>
        /// <param name="entityReference">一个reference to the record to get the column 数据 from.</param>
        /// <param name="property">名称 of the column.</param>
        /// <param name="returnFullSizedImage">When downloading image file, whether to return the full-sized image. Otherwise the thumbnail-sized image will be returned.</param>
        public DownloadFileRequest(EntityReference entityReference, string property, bool returnFullSizedImage = false)
        {
            string uriString = $"{entityReference.Path}/{property}/$value";

            if (returnFullSizedImage)
                uriString += "?size=full";

            Method = HttpMethod.Get;
            RequestUri = new Uri(
                uriString: uriString,
                uriKind: UriKind.Relative);
        }
    }
}
