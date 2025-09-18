using Newtonsoft.Json.Linq;
using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// 创建 a record and retrieves it.
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="entitySetName">EntitySetName for the table</param>
        /// <param name="record">Contains the 数据 to create the record.</param>
        /// <param name="query">query 字符串 参数</param>
        /// <param name="includeAnnotations">Whether to include annotations with the 数据.</param>
        /// <returns>created record.</returns>
        public static async Task<JObject> CreateRetrieve(
            this Service service,
            string entitySetName,
            JObject record,
            string? query,
            bool includeAnnotations = false)
        {

            CreateRetrieveRequest request = new(
                entitySetName: entitySetName, 
                record: record,
                query: query,
                includeAnnotations: includeAnnotations);

            try
            {
                CreateRetrieveResponse response = await service.SendAsync<CreateRetrieveResponse>(request: request);

                return response.Record;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
