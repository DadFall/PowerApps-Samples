using Newtonsoft.Json;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BooleanAttributeMetadata : AttributeMetadata
    {


        [JsonProperty("@odata.type")]
        public string ODataType { get; } = "Microsoft.Dynamics.CRM.BooleanAttributeMetadata";

        public AttributeTypeCode AttributeType { get; } = AttributeTypeCode.Boolean;
        public AttributeTypeDisplayName AttributeTypeName { get; } = new AttributeTypeDisplayName(AttributeTypeDisplayNameValues.BooleanType);

        /// <summary>
        /// default value for a Boolean option set.
        /// </summary>
        public bool DefaultValue { get; set; }

        /// <summary>
        /// 获取或设置 the formula definition for calculated and rollup attributes.
        /// </summary>
        public string FormulaDefinition { get; set; }

        /// <summary>
        /// 获取 the bitmask value that describes the sources of data used in a calculated attribute or whether the data sources are invalid.
        /// </summary>
        public int? SourceTypeMask { get; set; }

        public BooleanOptionSetMetadata OptionSet { get; set; }
        public BooleanOptionSetMetadata GlobalOptionSet { get; set; }

    }
}