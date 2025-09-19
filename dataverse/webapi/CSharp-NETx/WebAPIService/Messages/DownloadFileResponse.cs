namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the data from the GetColumnValueRequest.
        /// </summary>
    public sealed class DownloadFileResponse : HttpResponseMessage
    {
        /// <summary>
        /// requested file column  value.
        /// </summary>
        public byte[] File => Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
    }
}
