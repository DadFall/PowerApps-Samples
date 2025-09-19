using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PowerApps.Samples
{
    public partial class SampleProgram
    {
        private static Guid? _importMapId;
        private static String _mappingXml;
        private static Guid? _newImportMapId;
        private static bool prompt = true;
        /// <summary>
        /// Function to set up the sample.
        /// </summary>
        /// <param name="service">Specifies the service to connect to.</param>
        /// 
        private static void SetUpSample(CrmServiceClient service)
        {
            // 检查that the current version is greater than the minimum version
            if (!SampleHelpers.CheckVersion(service, new Version("7.1.0.0")))
            {
                //environment version is lower than version 7.1.0.0
                return;
            }

            CreateImportMapping(service);
            RetrieveMappingXml(service);
            ChangeMappingName(service);
            ImportMappingsByXml(service);
        }

        private static void CleanUpSample(CrmServiceClient service)
        {
            DeleteRequiredRecords(service, prompt);
        }


        /// <summary>
        /// 创建 the import mapping record.
        /// </summary>
        private static void CreateImportMapping(CrmServiceClient service)
        {
            // 创建the import map and populate a column
            ImportMap importMap = new ImportMap()
            {
                Name = "Original Import Mapping" + DateTime.Now.Ticks.ToString(),
                Source = "Import Accounts.csv",
                Description = "Description of data being imported",
                EntitiesPerFile =
                    new OptionSetValue((int)ImportMapEntitiesPerFile.SingleEntityPerFile),
                EntityState = EntityState.Created
            };

            _importMapId = service.Create(importMap);

            Console.WriteLine(String.Concat("Import map created: ", _importMapId.Value));

            #region Column One Mappings
            // 创建a column mapping for a 'text' type field
            ColumnMapping colMapping1 = new ColumnMapping()
            {
                // 设置source properties
                SourceAttributeName = "name",
                SourceEntityName = "Account_1",

                // 设置target properties
                TargetAttributeName = "name",
                TargetEntityName = Account.EntityLogicalName,

                // Relate this column mapping with the data map
                ImportMapId =
                    new EntityReference(ImportMap.EntityLogicalName, _importMapId.Value),

                // Force this column to be processed
                ProcessCode =
                    new OptionSetValue((int)ColumnMappingProcessCode.Process)
            };

            // 创建the mapping
            Guid colMappingId1 = service.Create(colMapping1);

            Console.WriteLine(String.Concat("Column mapping added SourceAttributeName: name",
                ", TargetAttributeName: name, TargetEntityName: account"));
            #endregion
        }

        /// <summary>
        /// Export the mapping that we created
        /// </summary>
        private static void RetrieveMappingXml(CrmServiceClient service)
        {
            if (!_importMapId.HasValue)
                return;

            // 检索the xml for the mapping 
            var exportRequest = new ExportMappingsImportMapRequest
            {
                ImportMapId = _importMapId.Value,
                ExportIds = true,
            };

            var exportResponse = (ExportMappingsImportMapResponse)service
                .Execute(exportRequest);

            _mappingXml = exportResponse.MappingsXml;

            Console.WriteLine(String.Concat("Import mapping exported for ",
                _importMapId.Value, "\nMappingsXml:\n", _mappingXml));
        }

        /// <summary>
        /// 解析the XML to change the name attribute
        /// </summary>
        private static void ChangeMappingName(CrmServiceClient service)
        {
            if (string.IsNullOrWhiteSpace(_mappingXml))
                return;

            // 加载into XElement
            var xElement = XElement.Parse(_mappingXml);

            // 获取the Name attribute
            var nameAttribute = xElement.Attribute("Name");

            // Swap the name out
            if (nameAttribute != null)
            {
                string newName = "New Import Mapping" + DateTime.Now.Ticks.ToString();
                Console.WriteLine(String.Concat("\nChanging the import name\n\tfrom: ",
                    nameAttribute.Value, ",\n\tto: ", newName));

                nameAttribute.Value = newName;
                _mappingXml = xElement.ToString();
            }
        }

        /// <summary>
        /// 创建a mapping from Xml
        /// </summary>
        private static void ImportMappingsByXml(CrmServiceClient service)
        {
            if (string.IsNullOrWhiteSpace(_mappingXml))
                return;

            // 创建the import mapping from the XML
            var request = new ImportMappingsImportMapRequest
            {
                MappingsXml = _mappingXml,

                ReplaceIds = true,
            };

            Console.WriteLine(String.Concat("\nCreating mapping based on XML:\n",
                _mappingXml));

            var response = (ImportMappingsImportMapResponse)
                service.Execute(request);

            _newImportMapId = response.ImportMapId;

            Console.WriteLine(String.Concat("\nNew import mapping created: ",
                _newImportMapId.Value));

            // 检索the value for validation
            var exportRequest = new ExportMappingsImportMapRequest
            {
                ImportMapId = _newImportMapId.Value,
                ExportIds = true,
            };

            var exportResponse = (ExportMappingsImportMapResponse)service
                .Execute(exportRequest);

            var mappingXml = exportResponse.MappingsXml;

            Console.WriteLine(String.Concat("\nValidating mapping xml for : ",
                _newImportMapId.Value, ",\nMappingsXml:\n", mappingXml));
        }

        /// <summary>
        /// 删除 the records created by this sample
        /// </summary>
        /// <param name="prompt"></param>
        private static void DeleteRequiredRecords(CrmServiceClient service, bool prompt)
        {
            bool toBeDeleted = true;

            if (prompt)
            {
                // Ask the user if the created entities should be deleted.
                Console.Write("\nDo you want these entity records deleted? (y/n) [y]: ");
                String answer = Console.ReadLine();
                if (answer.StartsWith("y") ||
                    answer.StartsWith("Y") ||
                    answer == String.Empty)
                {
                    toBeDeleted = true;
                }
                else
                {
                    toBeDeleted = false;
                }
            }

            if (toBeDeleted)
            {
                if (_importMapId.HasValue)
                {
                    Console.WriteLine(String.Concat("Deleting import mapping: ", _importMapId.Value));
                    service.Delete(ImportMap.EntityLogicalName, _importMapId.Value);
                }

                if (_newImportMapId.HasValue)
                {
                    Console.WriteLine(String.Concat("Deleting import mapping: ", _newImportMapId.Value));
                    service.Delete(ImportMap.EntityLogicalName, _newImportMapId.Value);
                }

                Console.WriteLine("Entity records have been deleted.");
            }
        }
    }
}
