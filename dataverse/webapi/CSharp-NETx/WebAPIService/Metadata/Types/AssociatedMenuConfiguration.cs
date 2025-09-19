using Newtonsoft.Json;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class AssociatedMenuConfiguration
    {
        /// <summary>
        /// behavior of the associated menu for an entity relationship.
        /// </summary>
        public AssociatedMenuBehavior Behavior { get; set; }

        /// <summary>
        /// structure that contains extra data.
        /// </summary>
        public AssociatedMenuGroup Group { get; set; }

        /// <summary>
        /// label for the associated menu.
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// order for the associated menu.
        /// </summary>
        public int? Order { get; set; }

        public bool? IsCustomizable { get; set; }
        public string Icon { get; set; }
        public Guid ViewId { get; set; }
        public bool? AvailableOffline { get; set; }
        public string MenuId { get; set; }
        public string QueryApi { get; set; }
    }
}