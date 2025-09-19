using Newtonsoft.Json.Linq;
using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// 检索 a record.
        /// </summary>
        /// <param name="service">服务.</param>
        /// <param name="entityReference">一个reference to the record to retrieve</param>
        /// <param name="query">query 字符串 参数</param>
        /// <param name="includeAnnotations">Whether to include annotations with the 数据.</param>
        /// <param name="eTag">current ETag 值 to compare.</param>
        /// <returns></returns>
        public static async Task<JObject> Retrieve(
            this Service service, 
            EntityReference entityReference, 
            string? query, 
            bool includeAnnotations = false,
            string? eTag = null,
            string? partitionId = null)
        {
            RetrieveRequest request = new(
                entityReference: entityReference,
                query: query,
                includeAnnotations: includeAnnotations,
                eTag: eTag, 
                partitionid:partitionId);

            try
            {
                RetrieveResponse response = await service.SendAsync<RetrieveResponse>(request: request);

                return response.Record;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
