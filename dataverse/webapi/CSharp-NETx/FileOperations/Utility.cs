using PowerApps.Samples.Metadata.Messages;
using PowerApps.Samples.Metadata.Types;

namespace PowerApps.Samples
{
    /// <summary>
        /// Contains metadata operation functions for the FileOperations samples.
        /// </summary>
    public class Utility
    {
        /// <summary>
        /// 创建 a custom file column on the designated table with the specified schema name.
        /// </summary>
        /// <param name="service">服务 to use.</param>
        /// <param name="entityLogicalName">logical 名称 of the table to create the file column in.</param>
        /// <param name="fileColumnSchemaName">schema 名称 of the file column.</param>
        /// <returns></returns>
        public static async Task CreateFileColumn(Service service, string entityLogicalName, string fileColumnSchemaName)
        {
            Console.WriteLine($"Creating file column named '{fileColumnSchemaName}' on the {entityLogicalName} table ...");

            FileAttributeMetadata fileColumn = new()
            {
                SchemaName = fileColumnSchemaName,
                DisplayName = new Label("Sample File Column", 1033),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(
                      AttributeRequiredLevel.None),
                Description = new Label("Sample File Column for FileOperation samples", 1033),
                MaxSizeInKB = 10 * 1024 // 10 MB

            };

            CreateAttributeRequest createfileColumnRequest = new(
                entityLogicalName: entityLogicalName,
                attributeMetadata: fileColumn
                );

            await service.SendAsync<CreateAttributeResponse>(createfileColumnRequest);

            Console.WriteLine($"Created file column named '{fileColumnSchemaName}' in the {entityLogicalName} table.");
        }


        /// <summary>
        /// 更新the MaxSizeInKB for a file column
        /// </summary>
        /// <param name="entityLogicalName">logical 名称 of the table that has the column.</param>
        /// <param name="fileColumnLogicalName">logical 名称 of the file column.</param>
        /// <param name="maxSizeInKB">new 值 for MaxSizeInKB</param>
        /// <returns></returns>
        public static async Task UpdateFileColumnMaxSizeInKB(Service service, string entityLogicalName, string fileColumnLogicalName, int maxSizeInKB)
        {
            // 检索the full column definition
            RetrieveAttributeRequest retrieveAttributeRequest = new(
                entityLogicalName: entityLogicalName,
                logicalName: fileColumnLogicalName,
                type: AttributeType.FileAttributeMetadata);

            var retrieveAttributeResponse =
                await service.SendAsync<RetrieveAttributeResponse<FileAttributeMetadata>>(retrieveAttributeRequest);

            FileAttributeMetadata fileColumn = retrieveAttributeResponse.AttributeMetadata;

            // 更新the MaxSizeInKB value
            fileColumn.MaxSizeInKB = maxSizeInKB;

            // 创建request
            UpdateAttributeRequest updateAttributeRequest = new(
                entityLogicalName: entityLogicalName,
                attributeLogicalName: fileColumnLogicalName,
                attributeMetadata: fileColumn);

            // 发送the update request
            await service.SendAsync(updateAttributeRequest);

        }


        /// <summary>
        /// 检索 the MaxSizeInKb property of a file column.
        /// </summary>
        /// <param name="service">服务.</param>
        /// <param name="entityLogicalName">logical 名称 of the table that has the column</param>
        /// <param name="fileColumnLogicalName">logical 名称 of the file column.</param>
        /// <returns></returns>
        public static async Task<int> GetFileColumnMaxSizeInKb(Service service, string entityLogicalName, string fileColumnLogicalName)
        {
            RetrieveAttributeRequest retrieveAttributeRequest = new(
                entityLogicalName: entityLogicalName,
                logicalName: fileColumnLogicalName,
                type: AttributeType.FileAttributeMetadata, query: "?$select=MaxSizeInKB");

            try
            {
                var retrieveAttributeResponse =
                await service.SendAsync<RetrieveAttributeResponse<FileAttributeMetadata>>(retrieveAttributeRequest);

                return retrieveAttributeResponse.AttributeMetadata.MaxSizeInKB;
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// 删除 a custom file column on the table
        /// </summary>
        /// <param name="service">服务 to use</param>
        /// <param name="entityLogicalName">logical 名称 of the table with the file column.</param>
        /// <param name="fileColumnLogicalName">logical 名称 of the file column.</param>
        /// <returns></returns>
        public static async Task DeleteFileColumn(Service service, string entityLogicalName, string fileColumnLogicalName)
        {

            Console.WriteLine($"Deleting the file column named '{fileColumnLogicalName}' on the {entityLogicalName} table ...");

            DeleteAttributeRequest deletefileColumnRequest = new(
                entityLogicalName: entityLogicalName,
                logicalName: fileColumnLogicalName,
                strongConsistency: true);

            await service.SendAsync(deletefileColumnRequest);

            Console.WriteLine($"Deleted the file column named '{fileColumnLogicalName}' in the {entityLogicalName} table.");
        }
    }
}

