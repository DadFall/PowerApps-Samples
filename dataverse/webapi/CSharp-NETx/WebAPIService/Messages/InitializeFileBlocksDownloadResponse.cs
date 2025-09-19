using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the data from the InitializeFileBlocksDownloadRequest
        /// </summary>
    public sealed class InitializeFileBlocksDownloadResponse : HttpResponseMessage
    {

        // 缓存the async content
        private string? _content;

        // Provides JObject for property getters
        private JObject jObject
        {
            get
            {
                _content ??= Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return JObject.Parse(_content);
            }
        }

        /// <summary>
        /// 一个token that uniquely identifies a sequence of related data blocks.
        /// </summary>
        public string FileContinuationToken => (string)jObject.GetValue(nameof(FileContinuationToken));

        /// <summary>
        /// size of the data file in bytes.
        /// </summary>
        public long FileSizeInBytes => (long)jObject.GetValue(nameof(FileSizeInBytes));


        /// <summary>
        /// name of the stored file.
        /// </summary>
        public string FileName => (string)jObject.GetValue(nameof(FileName));

    }
}
