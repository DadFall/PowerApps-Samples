using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// 检索 a single column value from a table.
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="service">服务</param>
        /// <param name="entityReference">一个reference to the record.</param>
        /// <param name="property">名称 of the column.</param>
        /// <returns></returns>
        public static async Task<T> GetColumnValue<T>(
            this Service service,
            EntityReference entityReference, 
            string property)
        {
            GetColumnValueRequest request = new(
                entityReference: entityReference,
                property: property);

            try
            {
                GetColumnValueResponse<T> response = await service.SendAsync<GetColumnValueResponse<T>>(request: request);

                return response.Value;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
