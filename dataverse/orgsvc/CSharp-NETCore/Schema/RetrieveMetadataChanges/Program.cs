using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System.ServiceModel;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace PowerPlatform.Dataverse.CodeSamples
{
    /// <summary>
        /// Demonstrates use of the RetrieveMetadataChanges message to create and maintain a cache
    /// of Dataverse schema data.
        /// </summary>
    /// <remarks>Set the appropriate Url and Username values for your test
    /// environment in the appsettings.json file before running this program.
    /// You will be prompted in the default browser to enter a password.</remarks>
    /// <see cref="https://learn.microsoft.com/power-apps/developer/data-platform/xrm-tooling/use-connection-strings-xrm-tooling-connect#connection-string-parameters"/>
    /// <permission cref="https://github.com/microsoft/PowerApps-Samples/blob/master/LICENSE"
    /// <author>Jim Daly</author>
    class Program
    {

        /// <summary>
        /// Contains the application's configuration settings.
        /// </summary>
        IConfiguration Configuration { get; }


        /// <summary>
        /// Constructor. 加载 the application configuration settings from a JSON file.
        /// </summary>
        Program()
        {

            // 获取the path to the appsettings file. If the environment variable is set,
            // use that file path. Otherwise, use the runtime folder's settings file.
            string? path = Environment.GetEnvironmentVariable("DATAVERSE_APPSETTINGS");
            if (path == null) path = "appsettings.json";

            // 加载the app's configuration settings from the JSON file.
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(path, optional: false, reloadOnChange: true)
                .Build();
        }

        static void Main(string[] args)
        {
            Program app = new();

            // 创建a Dataverse service client using the default connection string.
            ServiceClient serviceClient =
                new(app.Configuration.GetConnectionString("default"));

            // 一个simple list of column definitions to represent the cache
            List<AttributeMetadata> cachedAttributes = new();
            string clientVersionStamp = string.Empty;
            // name of a column to create when demonstrating changes
            string choiceColumnSchemaName = "sample_ChoiceColumnForSample";
            // Language code value from usersettingscollection.
            int? userLanguagePreference = RetrieveUserUILanguageCode(serviceClient);

            #region Define query

            // Define query for all Picklist Choice columns from Contact table
            EntityQueryExpression query = new()
            {
                Properties = new MetadataPropertiesExpression("LogicalName", "Attributes"),
                Criteria = new MetadataFilterExpression(filterOperator: LogicalOperator.And)
                {
                    Conditions =
                    {
                        {
                            new MetadataConditionExpression(
                                propertyName:"LogicalName",
                                conditionOperator: MetadataConditionOperator.Equals,
                                value:"contact")
                        }
                    }
                },
                AttributeQuery = new AttributeQueryExpression
                {
                    Properties = new MetadataPropertiesExpression("LogicalName", "OptionSet", "AttributeTypeName"),
                    Criteria = new MetadataFilterExpression(filterOperator: LogicalOperator.And)
                    {
                        Conditions =
                        {
                           {    // Only Picklist Option type
                                new MetadataConditionExpression(
                                propertyName:"AttributeTypeName",
                                conditionOperator: MetadataConditionOperator.Equals,
                                value:AttributeTypeDisplayName.PicklistType)
                            }
                        }
                    }
                }
            };

            // 返回only user language if they have a preference
            if (userLanguagePreference.HasValue)
            {
                query.LabelQuery = new LabelQueryExpression
                {
                    FilterLanguages = {
                        { userLanguagePreference.Value }
                    }
                };
            }

            #endregion Define query

            #region Initialize cache

            RetrieveMetadataChangesRequest initialRequest = new() { Query = query };

            var initialResponse = (RetrieveMetadataChangesResponse)serviceClient.Execute(initialRequest);

            Console.WriteLine($"Columns in initial response:{initialResponse.EntityMetadata.FirstOrDefault().Attributes.Count()}");

            // 初始化the cache
            cachedAttributes = initialResponse.EntityMetadata.FirstOrDefault().Attributes.ToList();

            Console.WriteLine($"Columns added to cache.");

            // 设置the client version
            clientVersionStamp = initialResponse.ServerVersionStamp;

            #endregion Initialize cache

            #region Add Choice column
            Console.WriteLine($"\nAdding a new choice column named {choiceColumnSchemaName}...");
            // 添加a new Choice column
            PicklistAttributeMetadata choiceColumn = new(choiceColumnSchemaName)
            {
                DisplayName = new Label("Choice column for sample", 1033),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                Description = new Label("Description", 1033),
                OptionSet = new OptionSetMetadata
                {
                    IsGlobal = false,
                    OptionSetType = OptionSetType.Picklist,
                    Options =
                    {
                        new OptionMetadata(
                            new Label("Choice 1", 1033), null),
                        new OptionMetadata(
                            new Label("Choice 2", 1033), null),
                        new OptionMetadata(
                            new Label("Choice 3", 1033), null)
                    }
                }
            };

            CreateAttributeRequest createChoiceColumnRequest = new()
            {
                EntityName = "contact",
                Attribute = choiceColumn
            };

            var createAttributeResponse = (CreateAttributeResponse)serviceClient.Execute(createChoiceColumnRequest);

            Console.WriteLine($"\nCreated Choice column: {choiceColumnSchemaName}");

            #endregion Add Choice column

            #region Detect added column

            // Detect changes
            RetrieveMetadataChangesRequest secondRequest = new()
            {
                Query = query, //Same query as before
                // 此time passing client version stamp value from previous request
                ClientVersionStamp = clientVersionStamp,
                DeletedMetadataFilters = DeletedMetadataFilters.Attribute
            };


            RetrieveMetadataChangesResponse secondResponse;

            try
            {
                secondResponse = (RetrieveMetadataChangesResponse)serviceClient.Execute(secondRequest);
                // Re-set the client version stamp
                clientVersionStamp = secondResponse.ServerVersionStamp;
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                // 检查for ErrorCodes.ExpiredVersionStamp (0x80044352)
                // Message: Version stamp associated with the client has expired. Please perform a full sync.
                // Will occur when the timestamp exceeds the Organization.ExpireSubscriptionsInDays value, which is 90 by default.
                if (ex.Detail.ErrorCode == unchecked((int)0x80044352))
                {
                    // TODO
                    // 添加code to re-initialize cache
                    throw new NotImplementedException("TODO: Manage case where cache must be re-initialized.");

                }
                else
                {
                    throw ex;
                }
            }

            // There should be only one representing the choice column just added
            Console.WriteLine($"\nColumns in second response:{secondResponse.EntityMetadata.FirstOrDefault().Attributes.Length}");

            // 更新cache to add new items.
            secondResponse.EntityMetadata.FirstOrDefault().Attributes.ToList().ForEach(att =>
            {
                if (!cachedAttributes.Contains(att))
                {
                    cachedAttributes.Add(att);
                }
            });
            Console.WriteLine($"New column added to cache.");

            // List the current cached Choice columns
            Console.WriteLine($"\nThe current {cachedAttributes.Count} cached choice columns:");
            cachedAttributes
                .ForEach(att =>
                {
                    Console.WriteLine($"\t{att.LogicalName}");
                });
            #endregion Detect added column


            #region Delete Choice Column
            Console.WriteLine($"\nDeleting the choice column named {choiceColumnSchemaName}...");

            DeleteAttributeRequest deleteChoiceColumnRequest = new()
            {
                EntityLogicalName = "contact",
                LogicalName = choiceColumnSchemaName.ToLower()
            };

            serviceClient.Execute(deleteChoiceColumnRequest);

            Console.WriteLine($"\nDeleted choice column: {choiceColumnSchemaName}");

            #endregion Delete Choice Column

            #region Detect deleted column

            RetrieveMetadataChangesRequest thirdRequest = new()
            {
                Query = query,
                // 此time passing client version stamp value from previous request
                ClientVersionStamp = clientVersionStamp,
                DeletedMetadataFilters = DeletedMetadataFilters.Attribute
            };


            RetrieveMetadataChangesResponse thirdResponse;
            try
            {
                thirdResponse = (RetrieveMetadataChangesResponse)serviceClient.Execute(thirdRequest);
                // Re-set the client version stamp
                clientVersionStamp = thirdResponse.ServerVersionStamp;

            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                // 检查for ErrorCodes.ExpiredVersionStamp (0x80044352)
                // Message: Version stamp associated with the client has expired. Please perform a full sync.
                // Will occur when the timestamp exceeds the Organization.ExpireSubscriptionsInDays value, which is 90 by default.
                if (ex.Detail.ErrorCode == unchecked((int)0x80044352))
                {
                    // TODO
                    // 添加code to re-initialize cache
                    throw new NotImplementedException("TODO: Manage case where cache must be re-initialized.");

                }
                else
                {
                    throw ex;
                }
            }

            // 移除deleted choice column from the cache

            // Confirm that the id of the column created and deleted exists in the 
            // DeletedMetadata:
            bool existsInDeletedMetadata = thirdResponse
                .DeletedMetadata[DeletedMetadataFilters.Attribute]
                .Contains(createAttributeResponse.AttributeId);

            Console.WriteLine($"\nThe deleted column {(existsInDeletedMetadata ? "exists" : "does not exist")} in the DeletedMetadata.");

            // 移除it from the cache
            thirdResponse.DeletedMetadata[DeletedMetadataFilters.Attribute]
                .ToList()
                .ForEach(id =>
                {
                    cachedAttributes.RemoveAll(a => a.MetadataId == id);
                });
            Console.WriteLine($"Deleted column removed from cache.");

            // List the current cached options again.
            // deleted choice column is no longer cached.
            Console.WriteLine($"\nThe current {cachedAttributes.Count} cached choice columns:");
            cachedAttributes
                .ForEach(att =>
                {
                    Console.WriteLine($"\t{att.LogicalName}");
                });

            Console.WriteLine($"\nNotice that '{choiceColumnSchemaName.ToLower()}' is no longer included.");

            #endregion Detect deleted column

            Console.WriteLine("\nSample complete.");
        }

        /// <summary>
        /// 检索 user's UI language preference
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        protected static int? RetrieveUserUILanguageCode(IOrganizationService service)
        {
            // To get the current user's systemuserid
            var whoIAm = (WhoAmIResponse)service.Execute(new WhoAmIRequest());

            var query = new QueryExpression("usersettings")
            {
                ColumnSet = new ColumnSet("uilanguageid", "systemuserid"),
                Criteria = new FilterExpression
                {
                    Conditions = {
                         {
                             new ConditionExpression(
                                 attributeName:"systemuserid",
                                 conditionOperator:ConditionOperator.Equal,
                                 value: whoIAm.UserId)
                         }
                     }
                },
                TopCount = 1
            };

            EntityCollection userSettings = service.RetrieveMultiple(query: query);
            if (userSettings.Entities.Count > 0)
            {
                return (int)userSettings.Entities[0]["uilanguageid"];
            }
            return null;
        }
    }
}