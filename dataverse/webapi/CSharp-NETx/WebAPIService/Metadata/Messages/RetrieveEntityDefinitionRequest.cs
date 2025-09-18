using PowerApps.Samples.Metadata.Types;
using System.Text;

namespace PowerApps.Samples.Metadata.Messages
{
    /// <summary>
        /// Contains the data to retrieve the EntityMetadata for a table definition.
        /// </summary>
    public sealed class RetrieveEntityDefinitionRequest : HttpRequestMessage
    {

        private Guid? _metadataId;
        private string? _logicalName;
        private string? _query;
        private string _uri = string.Empty;

        /// <summary>
        /// 初始化 the RetrieveEntityDefinitionRequest
        /// </summary>
        /// <param name="metadataId">Id of the table</param>
        /// <param name="logicalName">logical 名称 of the table</param>
        /// <param name="query">OData system query 选项 to control what is returned.</param>
        /// <exception cref="Exception"></exception>
        public RetrieveEntityDefinitionRequest(
            Guid? metadataId = null,
            string? logicalName =null,
            string? query = null)
        {

            _metadataId = metadataId;
            _logicalName = logicalName;
            _query = query;

            if (_metadataId == null && string.IsNullOrWhiteSpace(_logicalName))
            {
                throw new Exception("RetrieveEntityDefinitionRequest requires either LogicalName or MetadataId parameters.");
            }
            //获取the key to use
            string key;
            if (metadataId != null)
            {
                key = metadataId.ToString();
            }
            else
            {
                key = $"LogicalName='{logicalName}'";
            }

            _uri = $"EntityDefinitions({key}){_query}";

            Method = HttpMethod.Get;
            RequestUri = new Uri(uriString: _uri, uriKind: UriKind.Relative);
            Headers.Add("Consistency", "Strong");
        }
    }
}

