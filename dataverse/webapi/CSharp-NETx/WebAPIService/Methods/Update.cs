using Newtonsoft.Json.Linq;
using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// 更新 a record.
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="entityReference">一个reference to the record to update.</param>
        /// <param name="record">数据 to update.</param>
        /// <param name="preventDuplicateRecord">Whether to throw an error when a duplicate record is detected.</param>
        /// <param name="partitionId">partition key to use.</param>
        /// <param name="eTag">current ETag 值 to compare.</param>
        /// <returns></returns>
        public static async Task Update(
            this Service service, 
            EntityReference entityReference, 
            JObject record, 
            bool preventDuplicateRecord = false,
            string? partitionId = null,
            string? eTag = null)
        {

            UpdateRequest request = new(
                entityReference: entityReference, 
                record: record,
                preventDuplicateRecord: preventDuplicateRecord,
                partitionId: partitionId,
                eTag: eTag);

            try
            {
                await service.SendAsync(request: request);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
