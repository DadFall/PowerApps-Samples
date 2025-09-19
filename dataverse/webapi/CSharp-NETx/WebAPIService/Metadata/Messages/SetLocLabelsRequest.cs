using Newtonsoft.Json.Linq;
using PowerApps.Samples.Metadata.Types;
using System.Text;

namespace PowerApps.Samples.Metadata.Messages
{
    /// <summary>
        /// Contains the data that is needed to set localized labels for a limited set of entity attributes.
        /// </summary>
    public sealed class SetLocLabelsRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the SetLocLabelsRequest
        /// </summary>
        /// <param name="entityMoniker">Reference to the item</param>
        /// <param name="attributeName">Name of the property</param>
        /// <param name="labels">labels to set.</param>
        /// <param name="solutionUniqueName">名称 of the solution.</param>
        public SetLocLabelsRequest(JObject entityMoniker, string attributeName, LocalizedLabel[] labels, string? solutionUniqueName = null)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(uriString: "SetLocLabels", uriKind: UriKind.Relative);
            if (!string.IsNullOrWhiteSpace(solutionUniqueName))
            {
                Headers.Add("MSCRM.SolutionUniqueName", solutionUniqueName);
            }

            JObject content = new()
            {
                { "EntityMoniker", entityMoniker},
                { "AttributeName", attributeName},
                { "Labels", JToken.FromObject(labels) }
            };

            Content = new StringContent(
                content: content.ToString(),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
        }
    }
}