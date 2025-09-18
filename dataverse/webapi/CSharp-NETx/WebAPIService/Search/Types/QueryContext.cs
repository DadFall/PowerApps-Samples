using System.Runtime.Serialization;

namespace PowerApps.Samples.Search.Types;

/// <summary>
        /// query context returned as part of response.
        /// </summary>
public sealed class QueryContext
{
    /// <summary>
        /// 获取或设置 the query string as specified in the request.
        /// </summary>
    [DataMember(Name = "originalquery")]
    public string OriginalQuery { get; set; }

    /// <summary>
        /// 获取或设置 the query string that Dataverse search used to perform the query. 
    /// Dataverse search uses the altered query string if the original query string contained spelling mistakes or did not yield optimal results.
        /// </summary>
    [DataMember(Name = "alteredquery")]
    public string AlteredQuery { get; set; }

    /// <summary>
        /// 获取或设置 the reason behind query alter decision by Dataverse search.
        /// </summary>
    [DataMember(Name = "reason")]
    public List<string> Reason { get; set; }

    /// <summary>
        /// 获取或设置 the spell suggestion that are the likely words that represent user’s intent. 
    /// 此will be populated only when the query was altered by Dataverse search due to spell check.
        /// </summary>
    [DataMember(Name = "spellsuggestions")]
    public List<string> SpellSuggestions { get; set; }
}