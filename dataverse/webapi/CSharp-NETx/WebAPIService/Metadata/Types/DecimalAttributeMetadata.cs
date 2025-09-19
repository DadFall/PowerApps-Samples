using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DecimalAttributeMetadata : AttributeMetadata
    {


        [JsonProperty("@odata.type")]
        public string ODataType { get; } = "Microsoft.Dynamics.CRM.DecimalAttributeMetadata";

        public AttributeTypeCode AttributeType { get; } = AttributeTypeCode.Decimal;

        public AttributeTypeDisplayName AttributeTypeName { get; } = new AttributeTypeDisplayName(AttributeTypeDisplayNameValues.DecimalType);

        /// <summary>
        /// minimum supported value for this attribute.
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// maximum supported value for this attribute.
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// precision for the attribute.
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// input method editor (IME) mode for the attribute.
        /// </summary>
        public ImeMode? ImeMode { get; set; }

        /// <summary>
        /// 一个bitmask value that describes the sources of data used in a calculated attribute or whether the data sources are invalid.
        /// </summary>
        public int? SourceTypeMask { get; set; }

        /// <summary>
        /// formula definition for calculated and rollup attributes.
        /// </summary>
        public string FormulaDefinition { get; set; }
    }
}