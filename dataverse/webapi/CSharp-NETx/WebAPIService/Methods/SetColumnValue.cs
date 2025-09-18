using PowerApps.Samples.Messages;

namespace PowerApps.Samples.Methods
{
    public static partial class Extensions
    {
        /// <summary>
        /// 设置 the value of a column for a table row
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="service">服务</param>
        /// <param name="entityReference">一个reference to the table row.</param>
        /// <param name="propertyName">名称 of the property.</param>
        /// <param name="value">值 to set</param>
        /// <returns></returns>
        public static async Task SetColumnValue<T>(this Service service, EntityReference entityReference, string propertyName, T value)
        {

            SetColumnValueRequest<T> request = new(entityReference, propertyName, value);

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
