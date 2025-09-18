using Newtonsoft.Json.Linq;
using PowerApps.Samples.Metadata.Types;
using System.Text;

namespace PowerApps.Samples.Metadata.Messages
{
    /// <summary>
        /// Contains the data to create a relationship between tables
        /// </summary>
    public sealed class CreateRelationshipRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the CreateRelationshipRequest
        /// </summary>
        /// <param name="relationship">数据 that defines the relationship</param>
        /// <param name="solutionUniqueName">名称 of the solution to add the table to.</param>
        public CreateRelationshipRequest(RelationshipMetadataBase relationship, string? solutionUniqueName)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(
                uriString: "RelationshipDefinitions", 
                uriKind: UriKind.Relative);

            if (!string.IsNullOrWhiteSpace(solutionUniqueName))
            {
                Headers.Add("MSCRM.SolutionUniqueName", solutionUniqueName);
            }

            Content = new StringContent(
                content: JObject.FromObject(relationship).ToString(),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
        }
    }
}
