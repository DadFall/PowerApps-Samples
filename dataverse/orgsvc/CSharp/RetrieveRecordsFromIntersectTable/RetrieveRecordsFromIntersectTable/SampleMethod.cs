using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerApps.Samples
{
   public partial class SampleProgram
    {
        private static Guid userId;
        private static Guid roleId;
        private static bool prompt = true;
        /// <summary>
        /// Function to set up the sample.
        /// </summary>
        /// <param name="service">Specifies the service to connect to.</param>
        private static void SetUpSample(CrmServiceClient service)
        {
            // 检查that the current version is greater than the minimum version
            if (!SampleHelpers.CheckVersion(service, new Version("7.1.0.0")))
            {
                //environment version is lower than version 7.1.0.0
                return;
            }
            CreateRequiredRecords(service);

        }
        private static void CleanUpSample(CrmServiceClient service)
        {
            DeleteRequiredRecords(prompt, service);
        }

        /// <summary>
        /// 创建 any entity records that this sample requires.
        /// </summary>
        public static void CreateRequiredRecords(CrmServiceClient service)
        {
            // 检索the default business unit needed to create the team.
            var queryDefaultBusinessUnit = new QueryExpression
            {
                EntityName = BusinessUnit.EntityLogicalName,
                ColumnSet = new ColumnSet("businessunitid"),
                Criteria = new FilterExpression()
            };

            // 执行the request.
            queryDefaultBusinessUnit.Criteria.AddCondition("parentbusinessunitid",
                ConditionOperator.Null);

            var defaultBusinessUnit = (BusinessUnit)service.RetrieveMultiple(
                queryDefaultBusinessUnit).Entities[0];

            // 获取the GUID of the current user.
            var who = new WhoAmIRequest();
            var whoResp = (WhoAmIResponse)service.Execute(who);
            userId = whoResp.UserId;

            // Instantiate a role entity record and set its property values.
            // See the Entity Metadata topic in the SDK documentation to determine
            // which attributes must be set for each entity.
            var setupRole = new Role
            {
                Name = "ABC Management Role",
                BusinessUnitId = new EntityReference(BusinessUnit.EntityLogicalName,
                    defaultBusinessUnit.Id)
            };

            //Create a role record.
            roleId = service.Create(setupRole);
            Console.WriteLine("Created Role.");

            // Assign User to Managers role.
            var associate = new AssociateRequest()
            {
                Target = new EntityReference(SystemUser.EntityLogicalName, userId),
                RelatedEntities = new EntityReferenceCollection()
                {
                    new EntityReference(Role.EntityLogicalName, roleId),
                },
                Relationship = new Relationship("systemuserroles_association")
            };

            // 执行the request.
            service.Execute(associate);
        }

        /// <summary>
        /// 删除 any entity records and files that were created for this sample.
        /// <param name="prompt">Indicates whether to prompt the user 
        /// to delete the records created in this sample.</param>
        /// </summary>
        public static void DeleteRequiredRecords(bool prompt, CrmServiceClient service)
        {
            bool deleteRecords = true;

            if (prompt)
            {
                Console.WriteLine("\nDo you want these entity records deleted? (y/n) [y]: ");
                String answer = Console.ReadLine();

                deleteRecords = (answer.StartsWith("y") || answer.StartsWith("Y") || answer == String.Empty);
            }

            if (deleteRecords)
            {
                service.Delete(Role.EntityLogicalName, roleId);
                Console.WriteLine("Entity records have been deleted.");
            }
        }

    }
}
