using Newtonsoft.Json.Linq;
using PowerApps.Samples.Types;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to grant a security principal (user or team) access to the specified record.
        /// </summary>
    public sealed class GrantAccessRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the GrantAccessRequest
        /// </summary>
        /// <param name="target">entity that is the target of the 请求 to grant access.</param>
        /// <param name="principalAccess">team or user that is granted access to the specified record.</param>
        public GrantAccessRequest(JObject target, PrincipalAccess principalAccess)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(
                uriString: "GrantAccess",
                uriKind: UriKind.Relative);

            JObject _content = new() {
                { "Target", target},
                { "PrincipalAccess", JObject.FromObject(principalAccess) }
            };

            Content = new StringContent(
                    content: _content.ToString(),
                    encoding: System.Text.Encoding.UTF8,
                    mediaType: "application/json");
        }
    }
}