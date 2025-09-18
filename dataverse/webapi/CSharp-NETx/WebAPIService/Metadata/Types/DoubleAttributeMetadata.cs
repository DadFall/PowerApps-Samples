using Newtonsoft.Json;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DoubleAttributeMetadata : AttributeMetadata
    {


        [JsonProperty("@odata.type")]
        public string ODataType { get; } = "Microsoft.Dynamics.CRM.DoubleAttributeMetadata";

        public AttributeTypeCode AttributeType { get; } = AttributeTypeCode.Double;

        public AttributeTypeDisplayName AttributeTypeName { get; } = new AttributeTypeDisplayName(AttributeTypeDisplayNameValues.DoubleType);

        /// <summary>
        /// minimum supported value for this attribute.
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        /// maximum supported value for this attribute.
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// precision for the attribute.
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// input method editor (IME) mode for the attribute.
        /// </summary>
        public ImeMode ImeMode { get; set; }
    }
}