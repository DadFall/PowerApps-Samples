using Newtonsoft.Json.Linq;

namespace PowerApps.Samples.Messages
{
    // 此class must be instantiated by either:
    // - The Service.SendAsync<T> method
    // - The HttpResponseMessage.As<T> extension in Extensions.cs

    /// <summary>
        /// Contains the data from the CommitAttachmentBlocksUploadRequest
        /// </summary>
    public sealed class CommitAttachmentBlocksUploadResponse : HttpResponseMessage
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
        /// unique identifier of the stored activitymimeattachment.
        /// </summary>
        public Guid ActivityMimeAttachmentId => (Guid)JObject.GetValue(nameof(ActivityMimeAttachmentId));

        /// <summary>
        /// size of the stored activitymimeattachment in bytes.
        /// </summary>
        public int FileSizeInBytes => (int)JObject.GetValue(nameof(FileSizeInBytes));

    }
}
