using PowerApps.Samples.Types;

namespace PowerApps.Samples.Messages
{
    /// <summary>
        /// Contains the data to perform the FormatAddress function
        /// </summary>
    public sealed class InitializeFromRequest : HttpRequestMessage
    {
        /// <summary>
        /// 初始化 the InitializeFromRequest
        /// </summary>
        /// <param name="entityMoniker">record that is the source for initializing.</param>
        /// <param name="targetEntityName">	logical 名称 of the target entity.</param>
        /// <param name="targetFieldType">attributes are to be initialized in the initialized 实例.</param>
        /// <param name="skipParentalRelationshipMapping">Does not include 值 for parental lookup.</param>
        public InitializeFromRequest(
            EntityReference entityMoniker,
            string targetEntityName,
            TargetFieldType targetFieldType,
            bool? skipParentalRelationshipMapping = null)
        {
            Dictionary<string, string> _queryParameters = new()
            {
                //Required parameters
                { "EntityMoniker", entityMoniker.AsODataId()},
                { "TargetEntityName", $"'{targetEntityName}'" },
                { "TargetFieldType", $"Microsoft.Dynamics.CRM.TargetFieldType'{targetFieldType}'" }
            };

            //Optional parameters
            if (skipParentalRelationshipMapping != null)
            {
                _queryParameters.Add("SkipParentalRelationshipMapping", $"{skipParentalRelationshipMapping.ToString().ToLowerInvariant()}");
            }

            List<string> _parameterNames = new();
            List<string> _parameterValues = new();
            int _count = 1;

            foreach (var parameter in _queryParameters) { 
            
                _parameterNames.Add($"{parameter.Key}=@p{_count}");
                _parameterValues.Add($"@p{_count}={parameter.Value}");
                _count++;
            }

            Method = HttpMethod.Get;
            RequestUri = new Uri(
                uriString: $"InitializeFrom({string.Join(",",_parameterNames)})?{string.Join("&",_parameterValues)}",
                uriKind: UriKind.Relative);
        }
    }
}
