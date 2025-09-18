using PowerApps.Samples.Metadata.Types;

namespace PowerApps.Samples.Metadata.Messages
{
    /// <summary>
        /// Contains the data to retrieve a global optionset (choice)
        /// </summary>
    public sealed class RetrieveGlobalOptionSetRequest : HttpRequestMessage
    {
        /// <summary>
        /// Returns a RetrieveGlobalOptionSetRequest
        /// </summary>
        /// <param name="metadataid">Id of the global 选项et</param>
        /// <param name="name">名称 of the global 选项et</param>
        /// <param name="type">Specify the 类型 of option set: Pick列表 or Boolean. Defaults to Pick列表</param>
        /// <exception cref="Exception"></exception>
        public RetrieveGlobalOptionSetRequest(
            Guid? metadataid = null,
            string? name = null,
            OptionSetType type = OptionSetType.Picklist)
        {

            if (metadataid == null && string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("RetrieveGlobalOptionSetRequest constructor requires either name or metadataid parameters.");
            }
            if (type == OptionSetType.State || type == OptionSetType.Status) {
                throw new Exception("RetrieveGlobalOptionSetRequest type parameter must be OptionSetType.Picklist or OptionSetType.Boolean");               
            }


            //获取the key to use
            string key;
            if (metadataid != null)
            {
                key = metadataid.ToString();
            }
            else
            {
                key = $"Name='{name}'";
            }

            // Boolean global option sets exist, but cannot be used in custom Boolean columns.
            string optionSetType = (type == OptionSetType.Picklist) ? "OptionSetMetadata" : "BooleanOptionSetMetadata";

           string _uri = new($"GlobalOptionSetDefinitions({key})" +
                       $"/Microsoft.Dynamics.CRM.{optionSetType}");

            Method = HttpMethod.Get;
            RequestUri = new Uri(uriString: _uri, uriKind: UriKind.Relative);
            Headers.Add("Consistency", "Strong");
        }
    }
}

