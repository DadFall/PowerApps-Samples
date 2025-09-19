using System.Net.Http.Headers;
namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Uploads a block of data to storage.
        /// </summary>
    public sealed class UploadFileChunkRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the UploadFileChunkRequest
        /// </summary>
        /// <param name="url">InitializeChunkedFileUploadResponse.Url 值</param>
        /// <param name="uploadFileName">名称 of the file to upload</param>
        /// <param name="chunkSize">InitializeChunkedFileUploadResponse.ChunkSize 值</param>
        /// <param name="fileBytes">bytes for the chunk.</param>
        /// <param name="offSet">offset for the chunk.</param>
        public UploadFileChunkRequest(
            Uri url,
            string uploadFileName,
            int chunkSize, 
            byte[] fileBytes, 
            int offSet)
        {
            Method = HttpMethod.Patch;
            RequestUri = url;

            var count = (offSet + chunkSize) > fileBytes.Length ? fileBytes.Length % chunkSize : chunkSize;

            Content = new ByteArrayContent(fileBytes, offSet, count);

            Content.Headers.Add("Content-Type", "application/octet-stream");
            Content.Headers.ContentRange = new ContentRangeHeaderValue(offSet, offSet + (count - 1), fileBytes.Length);

            Headers.Add("x-ms-file-name", uploadFileName);

        }
    }
}
