using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;
using PowerApps.Samples.Messages;
using PowerApps.Samples.Methods;
using System;

namespace PowerApps.Samples
{
    public class Utility
    {
        /// <summary>
        /// 获取 the mimetype for a file.
        /// </summary>
        /// <param name="file">一个reference to the file.</param>
        /// <returns>mimetype</returns>
        public static string GetMimeType(FileInfo file)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (provider.TryGetContentType(file.Name, out string fileMimeType))
                return fileMimeType;
            else
                return "application/octet-stream";
        }

        /// <summary>
        /// 获取 the MaxUploadFileSize in bytes.
        /// </summary>
        /// <param name="service">WebAPIService to use.</param>
        /// <returns>current system configured MaxUploadFileSize.</returns>
        public static async Task<int> GetMaxUploadFileSize(Service service)
        {
            RetrieveMultipleResponse results =
                  await service.RetrieveMultiple(queryUri: "organizations?$select=maxuploadfilesize");
            // There is only one row in organization table.
            return (int)results.Records.FirstOrDefault()["maxuploadfilesize"];
        }

        /// <summary>
        /// 设置the current system configured MaxUploadFileSize
        /// </summary>
        /// <param name="service">WebAPIService to use.</param>
        /// <param name="maxUploadFileSizeInBytes">值 to set.</param>
        /// <returns>Task</returns>
        /// <exception cref="ArgumentOutOfRangeException">The maxUploadFileSizeInBytes 参数 must be less than 131072000 bytes and greater than 0 bytes.</exception>
        public static async Task SetMaxUploadFileSize(Service service, int maxUploadFileSizeInBytes)
        {
            if (maxUploadFileSizeInBytes > 131072000 || maxUploadFileSizeInBytes < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxUploadFileSizeInBytes), "The maxUploadFileSizeInBytes parameter must be less than 131072000 bytes and greater than 0 bytes.");
            }

            RetrieveMultipleResponse results =
                  await service.RetrieveMultiple(queryUri: "organizations?$select=organizationid");
            Guid organizationid = (Guid)results.Records.FirstOrDefault()["organizationid"];

            EntityReference organizationRef = new("organizations", organizationid);

            JObject organization = new() {
                { "maxuploadfilesize", maxUploadFileSizeInBytes }
            };

            await service.Update(organizationRef, organization);
        }

    }
}

