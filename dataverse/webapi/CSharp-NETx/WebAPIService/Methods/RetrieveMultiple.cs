using Newtonsoft.Json.Linq;
using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// 检索 the results of an OData query.
        /// </summary>
        /// <param name="service">Service.</param>
        /// <param name="queryUri">一个absolute or relative Uri.</param>
        /// <param name="maxPageSize">maximum number of records to return in a page.</param>
        /// <param name="includeAnnotations">Whether to include annotations with the 结果s.</param>
        /// <returns></returns>
        public static async Task<RetrieveMultipleResponse> RetrieveMultiple(
            this Service service,
            string queryUri,
            int? maxPageSize = null,
            bool includeAnnotations = false)
        {
            RetrieveMultipleRequest request = new(
                queryUri: queryUri,
                maxPageSize: maxPageSize,
                includeAnnotations: includeAnnotations);


            try
            {
                return await service.SendAsync<RetrieveMultipleResponse>(request: request);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
