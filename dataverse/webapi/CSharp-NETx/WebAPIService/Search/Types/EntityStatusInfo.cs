using Newtonsoft.Json;

namespace PowerApps.Samples.Search.Types;

/// <summary>
        /// Data contract for response parameters for index status api.
        /// </summary>
public sealed class EntityStatusInfo
{
    /// <summary>
        /// 获取或设置 the entity logical name.
        /// </summary>
    [JsonProperty(PropertyName = "entitylogicalname")]
    public string EntityLogicalName { get; set; }

    /// <summary>
        /// 获取或设置 the object type code.
        /// </summary>
    [JsonProperty(PropertyName = "objecttypecode")]
    public int ObjectTypeCode { get; set; }

    /// <summary>
        /// 获取或设置 the primary field name.
        /// </summary>
    [JsonProperty(PropertyName = "primarynamefield")]
    public string PrimaryNameField { get; set; }

    /// <summary>
        /// 获取或设置 the last data sync time.
        /// </summary>
    [JsonProperty(PropertyName = "lastdatasynctimestamp")]
    public string LastDataSyncTimeStamp { get; set; }

    /// <summary>
        /// 获取或设置 the last principal object access sync time.
        /// </summary>
    [JsonProperty(PropertyName = "lastprincipalobjectaccesssynctimestamp")]
    public string LastPrincipalObjectAccessSyncTimeStamp { get; set; }

    /// <summary>
        /// 获取或设置 entity level status.
        /// </summary>
    [JsonProperty(PropertyName = "entitystatus")]
    public string EntityStatus { get; set; }

    /// <summary>
        /// 获取或设置 the dictionary of attribute name and details.
        /// </summary>
    [JsonProperty(PropertyName = "searchableindexedfieldinfomap")]
    public IDictionary<string, FieldStatusInfo> SearchableIndexedFieldInfoMap { get; set; }
}