using Newtonsoft.Json;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class StringAttributeMetadata : AttributeMetadata
    {

        [JsonProperty("@odata.type")]
        public string ODataType { get; } = "Microsoft.Dynamics.CRM.StringAttributeMetadata";



        public  AttributeTypeCode AttributeType { get; } = AttributeTypeCode.String;

        public  AttributeTypeDisplayName AttributeTypeName { get; } = new AttributeTypeDisplayName(AttributeTypeDisplayNameValues.StringType);

        /// <summary>
        /// format options for the memo attribute.
        /// </summary>
        public StringFormat? Format { get; set; }

        /// <summary>
        /// input method editor (IME) mode for the attribute.
        /// </summary>
        public ImeMode? ImeMode { get; set; }

        /// <summary>
        /// maximum length for the attribute.
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Whether the attribute supports localizable values.
        /// </summary>
        public bool? IsLocalizable { get; set; }

        /// <summary>
        /// format for the string.
        /// </summary>

        public StringFormatName FormatName { get; set; }

        /// <summary>
        /// 对于internal use only.
        /// </summary>
        public string YomiOf { get; set; }

        public int? DatabaseLength { get; set; }

        /// <summary>
        /// formula definition for calculated and rollup attributes.
        /// </summary>
        public string FormulaDefinition { get; set; }

        /// <summary>
        /// 一个bitmask value that describes the sources of data used in a calculated attribute or whether the data sources are invalid.
        /// </summary>
        public int? SourceTypeMask { get; set; }
    }
}