using System.Runtime.Serialization;

namespace PowerApps.Samples.Search.Types;

/// <summary>
        /// Data contract for autocomplete parameters.
        /// </summary>
public sealed class AutocompleteParameters
{
    /// <summary>
        /// 获取或设置 the search terms.
        /// </summary>
    [DataMember(Name = "search", IsRequired = true)]
    public string Search { get; set; }

    /// <summary>
        /// 获取或设置 list of entities.
        /// </summary>
    [DataMember(Name = "entities")]
    public List<SearchEntity> Entities { get; set; }

    /// <summary>
        /// 获取或设置 the common filters applied on the request.
        /// </summary>
    [DataMember(Name = "filter")]
    public string? Filter { get; set; }

    /// <summary>
        /// 获取或设置 a value indicating whether to use fuzzy matching for the autocomplete query.
    /// 当set to true, the query will find terms even if there's a substituted or missing character in the search text.
        /// </summary>
    [DataMember(Name = "fuzzy")]
    public bool Fuzzy { get; set; }
}
