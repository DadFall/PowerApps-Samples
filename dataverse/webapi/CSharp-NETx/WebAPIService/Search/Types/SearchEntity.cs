using System.Runtime.Serialization;

namespace PowerApps.Samples.Search.Types;

/// <summary>
        /// entity schema to scope the search request.
        /// </summary>
public sealed class SearchEntity
{
    /// <summary>
        /// 获取或设置 the logical name of the table. Specifies scope of the query.
        /// </summary>
    [DataMember(Name = "name", IsRequired = true)]
    public string Name { get; set; }

    /// <summary>
        /// 获取或设置 the list of columns that needs to be projected when table documents are returned in response. 
    /// 如果empty, only PrimaryName will be returned.
        /// </summary>
    [DataMember(Name = "selectcolumns")]
    public List<string> SelectColumns { get; set; }

    /// <summary>
        /// 获取或设置 the list of columns to scope the query on.
    /// 如果empty, only PrimaryName will be searched on.
        /// </summary>
    [DataMember(Name = "searchcolumns")]
    public List<string> SearchColumns { get; set; }

    /// <summary>
        /// 获取或设置 the filters applied on the entity.
        /// </summary>
    [DataMember(Name = "filter")]
    public string Filter { get; set; }
}