using Newtonsoft.Json;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DateTimeAttributeMetadata : AttributeMetadata
    {


        [JsonProperty("@odata.type")]
        public string ODataType { get; } = "Microsoft.Dynamics.CRM.DateTimeAttributeMetadata";

        public AttributeTypeCode AttributeType { get; } = AttributeTypeCode.DateTime;
        public AttributeTypeDisplayName AttributeTypeName { get; } = new AttributeTypeDisplayName(AttributeTypeDisplayNameValues.DateTimeType);
        /// <summary>
        /// minimum supported value for this attribute.
        /// </summary>
        public DateTime? MinSupportedValue { get; set; }

        /// <summary>
        /// maximum supported value for this attribute.
        /// </summary>
        public DateTime? MaxSupportedValue { get; set; }

        /// <summary>
        /// date/time display format.
        /// </summary>
        public DateTimeFormat Format { get; set; }

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
        public string? FormulaDefinition { get; set; }

        /// <summary>
        /// behavior of the DateTime attribute.
        /// </summary>       
        public DateTimeBehavior? DateTimeBehavior { get; set; }

        /// <summary>
        /// Whether the date and time behavior can be changed for the attribute.
        /// </summary>
        public BooleanManagedProperty CanChangeDateTimeBehavior { get; set; }
    }
}