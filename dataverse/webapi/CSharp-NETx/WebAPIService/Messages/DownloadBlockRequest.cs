using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to download a block of data.
        /// </summary>
    public sealed class DownloadBlockRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the DownloadBlockRequest
        /// </summary>
        /// <param name="offset">offset (in bytes) from the beginning of the block to the first byte of 数据 in the block.</param>
        /// <param name="blockLength">size of the block in bytes.</param>
        /// <param name="fileContinuationToken">一个token that uniquely identifies a sequence of related 数据 blocks.</param>
        public DownloadBlockRequest(long offset, long blockLength, string fileContinuationToken)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(
                uriString: "DownloadBlock",
                uriKind: UriKind.Relative);

            JObject content = new() {
                { "Offset", offset },
                { "BlockLength", blockLength },
                { "FileContinuationToken", fileContinuationToken }
            };

            Content = new StringContent(
                    content: content.ToString(),
                    encoding: System.Text.Encoding.UTF8,
                    mediaType: "application/json");
        }
    }
}
