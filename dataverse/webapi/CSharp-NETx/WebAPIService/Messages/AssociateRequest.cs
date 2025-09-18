using Newtonsoft.Json.Linq;
using System.Text;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to associate a record to a collection.
        /// </summary>
    public sealed class AssociateRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the AssociateRequest.
        /// </summary>
        /// <param name="baseAddress">Service.BaseAddress</param>
        /// <param name="entityWithCollection">entity with the 集合.</param>
        /// <param name="collectionName">名称 of the 集合</param>
        /// <param name="entityToAdd">record to add</param>
        public AssociateRequest(Uri baseAddress,
            EntityReference entityWithCollection,
            string collectionName,
            EntityReference entityToAdd)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(
                uriString: $"{entityWithCollection.Path}/{collectionName}/$ref", 
                uriKind: UriKind.Relative);
            Content = new StringContent(
                        content: new JObject() 
                        {
                           { "@odata.id", $"{baseAddress}{entityToAdd.Path}"}
                        }.ToString(),
                        encoding: Encoding.UTF8,
                        mediaType: "application/json");
        }
    }
}
