using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// 删除 the value of a column for a table row
        /// </summary>
        /// <param name="service">服务</param>
        /// <param name="entityReference">一个reference to the table row.</param>
        /// <param name="propertyName">名称 of the property.</param>
        /// <returns></returns>
        public static async Task DeleteColumnValue(this Service service, EntityReference entityReference, string propertyName)
        {

            DeleteColumnValueRequest request = new(entityReference: entityReference, propertyName: propertyName);

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