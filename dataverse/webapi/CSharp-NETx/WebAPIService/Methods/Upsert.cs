using Newtonsoft.Json.Linq;
using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// Performs an Upsert operation on a record
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="entityReference">一个reference to the record to upsert.</param>
        /// <param name="record">数据 for the record.</param>
        /// <param name="upsertBehavior">Controls whether to block Create or Update operations.</param>
        /// <returns></returns>
        public static async Task<EntityReference> Upsert(
            this Service service, 
            EntityReference entityReference, 
            JObject record, 
            UpsertBehavior upsertBehavior)
        {

            UpsertRequest request = new(
                entityReference: entityReference, 
                record: record, 
                upsertBehavior:upsertBehavior);

            try
            {
                UpsertResponse response = await service.SendAsync<UpsertResponse>(request: request);
                return response.EntityReference;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
