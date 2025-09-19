using Newtonsoft.Json;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class StatusOptionMetadata : OptionMetadata
    {
        [JsonProperty("@odata.type")]
        public string ODataType { get; } = "Microsoft.Dynamics.CRM.StatusOptionMetadata";

        /// <summary>
        /// state that the status is associated with.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// status transitions allowed for this status
        /// </summary>
        public string TransitionData { get; set; }
    }
}