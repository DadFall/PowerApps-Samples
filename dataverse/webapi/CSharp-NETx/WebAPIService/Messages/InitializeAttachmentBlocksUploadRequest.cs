using Newtonsoft.Json.Linq;
using System.Text;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to initialize upload of an attachment
        /// </summary>
    public sealed class InitializeAttachmentBlocksUploadRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the InitializeAttachmentBlocksUploadRequest
        /// </summary>
        /// <param name="target">activitymimeattachment record with an 'activitymimeattachmentid' 值.</param>
        public InitializeAttachmentBlocksUploadRequest(JObject target)
        {
            // @odata.type is required. Add it if it isn't there.
            if (!target.ContainsKey("@odata.type"))
            {
                target.Add("@odata.type", "Microsoft.Dynamics.CRM.activitymimeattachment");
            }

            Method = HttpMethod.Post;
            RequestUri = new Uri(
                uriString: "InitializeAttachmentBlocksUpload",
                uriKind: UriKind.Relative);

            JObject body = new()
            {
                {"Target", target}
            };

            Content = new StringContent(
                    content: body.ToString(),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json");
        }
    }
}
