using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to commits the uploaded data blocks to the File store.
        /// </summary>
    public sealed class CommitFileBlocksUploadRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the CommitFileBlocksUploadRequest
        /// </summary>
        /// <param name="fileName">一个file名称 to associate with the binary 数据 file.</param>
        /// <param name="mimeType">MIME 类型 of the uploaded file.</param>
        /// <param name="blockList">IDs of the uploaded 数据 blocks, in the correct sequence, that will 结果 in the final File when the 数据 blocks are combined.</param>
        /// <param name="fileContinuationToken">一个token that uniquely identifies a sequence of related 数据 uploads.</param>
        public CommitFileBlocksUploadRequest(
            string fileName, 
            string mimeType, 
            List<string> blockList, 
            string fileContinuationToken)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(
                uriString: "CommitFileBlocksUpload",
                uriKind: UriKind.Relative);

            JObject body = new()
            {
                { "FileName", fileName },
                { "MimeType", mimeType },
                { "BlockList", JToken.FromObject(blockList.ToArray()) },
                { "FileContinuationToken", fileContinuationToken}
            };

            Content = new StringContent(
                    content: body.ToString(),
                    encoding: System.Text.Encoding.UTF8,
                    mediaType: "application/json");
        }
    }
}
