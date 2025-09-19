using Newtonsoft.Json;

namespace PowerApps.Samples.Search.Types;

/// <summary>
        /// Data contract for response parameters for many to many relationship sync job status.
        /// </summary>
public sealed class ManyToManyRelationshipSyncStatus
{

    /// <summary>
        /// 获取 the relationship name.
        /// </summary>
    [JsonProperty("relationshipName")]
    public string RelationshipName { get; }

    /// <summary>
        /// 获取 the relationship metadata id.
        /// </summary>
    [JsonProperty("relationshipMetadataId")]
    public Guid RelationshipMetadataId { get; }

    /// <summary>
        /// 获取 the search entity name.
        /// </summary>
    [JsonProperty("searchEntity")]
    public string SearchEntity { get; }

    /// <summary>
        /// 获取 the second entity name.
        /// </summary>
    [JsonProperty("relatedEntity")]
    public string RelatedEntity { get; }

    /// <summary>
        /// 获取 the search entity Id attribute.
        /// </summary>
    [JsonProperty("searchEntityIdAttribute")]
    public string SearchEntityIdAttribute { get; }

    /// <summary>
        /// 获取 the related entity Id attribute.
        /// </summary>
    [JsonProperty("relatedEntityIdAttribute")]
    public string RelatedEntityIdAttribute { get; }

    /// <summary>
        /// 获取 the intersect entity name.
        /// </summary>
    [JsonProperty("intersectEntity")]
    public string IntersectEntity { get; }

    /// <summary>
        /// 获取 the search entity object type code.
        /// </summary>
    [JsonProperty("searchEntityObjectTypeCode")]
    public int SearchEntityObjectTypeCode { get; }

    /// <summary>
        /// 获取 the synced version.
        /// </summary>
    [JsonProperty("lastSyncedVersion")]
    public string LastSyncedDataVersion { get; }
}