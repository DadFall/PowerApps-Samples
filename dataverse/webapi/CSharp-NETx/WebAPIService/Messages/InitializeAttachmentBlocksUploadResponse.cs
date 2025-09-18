using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the data from the InitializeAttachmentBlocksUploadRequest
        /// </summary>
    public sealed class InitializeAttachmentBlocksUploadResponse : HttpResponseMessage
    {

        // 缓存the async content
        private string? _content;

        // Provides JObject for property getters
        private JObject JObject
        {
            get
            {
                _content ??= Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return JObject.Parse(_content);
            }
        }

        /// <summary>
        /// 一个token that uniquely identifies a sequence of related data uploads.
        /// </summary>
        public string FileContinuationToken => (string)JObject.GetValue(nameof(FileContinuationToken));

    }
}
