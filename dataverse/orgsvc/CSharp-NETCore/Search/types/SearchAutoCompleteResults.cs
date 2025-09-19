using PowerApps.Samples.Search.Types;

namespace PowerPlatform.Dataverse.CodeSamples.types
{
    /// <summary>
        /// Use to deserialize JSON data from the searchautocompleteResponse.response string property.
        /// </summary>
    class SearchAutoCompleteResults
    {
        /// <summary>
        /// Azure Cognitive error detail returned as part of response.
        /// </summary>
        public ErrorDetail? Error {  get; set; }

        /// <summary>
        /// text
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 此request is used for backend search, this is included for future feature releases, it is not currently used.
        /// </summary>
        public QueryContext? QueryContext { get; set; }
    }
}
