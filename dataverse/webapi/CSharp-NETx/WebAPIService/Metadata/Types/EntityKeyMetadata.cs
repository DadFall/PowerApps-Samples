using Newtonsoft.Json;
using System;

namespace PowerApps.Samples.Metadata.Types
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class EntityKeyMetadata : MetadataBase
    {
        /// <summary>
        /// asynchronous job.
        /// </summary>
        public Guid? AsyncJob { get; set; }

        /// <summary>
        /// 一个label containing the display name for the key.
        /// </summary>
        public Label DisplayName { get; set; }

        /// <summary>
        /// entity key index status.
        /// </summary>
        public EntityKeyIndexStatus EntityKeyIndexStatus { get; set; }

        /// <summary>
        /// entity logical name.
        /// </summary>
        public string EntityLogicalName { get; set; }

        /// <summary>
        /// 一个string identifying the solution version that the solution component was added in.
        /// </summary>
        public string IntroducedVersion { get; set; }

        /// <summary>
        /// Whether the entity key metadata is customizable.
        /// </summary>
        public BooleanManagedProperty IsCustomizable { get; set; }

        /// <summary>
        /// Whether entity key metadata is managed or not.
        /// </summary>
        public bool IsManaged { get; set; }

        /// <summary>
        /// key attributes.
        /// </summary>
        public string[] KeyAttributes { get; set; }

        /// <summary>
        /// logical name.
        /// </summary>
        public string LogicalName { get; set; }

        /// <summary>
        /// schema name.
        /// </summary>
        public string SchemaName { get; set; }
    }
}