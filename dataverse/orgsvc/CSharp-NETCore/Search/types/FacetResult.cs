using System.Runtime.Serialization;

namespace PowerApps.Samples.Search.Types;

/// <summary>
        /// 一个facet query result that reports the number of documents with a field
/// falling within a particular range or having a particular value or interval.
        /// </summary>
public sealed class FacetResult
{
    /// <summary>
        /// 获取或设置 the count of documents falling within the bucket described by this facet.
        /// </summary>
    [DataMember(Name = "count")]
    public long? Count { get; set; }

    /// <summary>
        /// 获取或设置 value indicating the inclusive lower bound of the facet's range, or null to indicate that there is no lower bound.
        /// </summary>
    [DataMember(Name = "from")]
    public object From { get; set; }

    /// <summary>
        /// 获取或设置 value indicating the exclusive upper bound of the facet's range, or null to indicate that there is no upper bound.
        /// </summary>
    [DataMember(Name = "to")]
    public object To { get; set; }

    /// <summary>
        /// 获取或设置 type of the facet - Value or Range.
        /// </summary>
    [DataMember(Name = "type")]
    public FacetType Type { get; set; }

    /// <summary>
        /// 获取或设置 value of the facet, or the inclusive lower bound if it's an interval facet.
        /// </summary>
    [DataMember(Name = "value")]
    public object Value { get; set; }

    /// <summary>
        /// 获取或设置 additional/ Optional value of the facet, will be populated while faceting on lookups.
        /// </summary>
    [DataMember(Name = "optionalvalue")]
    public object OptionalValue { get; set; }
}