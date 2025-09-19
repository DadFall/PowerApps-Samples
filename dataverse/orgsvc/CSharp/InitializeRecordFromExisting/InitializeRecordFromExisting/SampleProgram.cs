using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace PowerApps.Samples
{
    public partial class SampleProgram
    {
        [STAThread] // Required to support the interactive login experience
        static void Main(string[] args)
        {
            CrmServiceClient service = null;
            try
            {
                service = SampleHelpers.Connect("Connect");
                if (service.IsReady)
                {
                    // 创建any entity records that the demonstration code requires
                    SetUpSample(service);
                    #region Demonstrate
                    
                    Console.WriteLine();
                    Console.WriteLine(" Initializing new Account from the initial Account");

                    // 创建the request object
                    var initialize = new InitializeFromRequest
                    {

                        // 设置the properties of the request object
                        TargetEntityName = Account.EntityLogicalName.ToString(),

                        // 创建the EntityMoniker
                        EntityMoniker = _initialAccount.ToEntityReference()
                    };

                    // 执行the request
                    InitializeFromResponse initialized =
                        (InitializeFromResponse)service.Execute(initialize);

                    if (initialized.Entity != null)
                        Console.WriteLine("  New Account initialized successfully");

                    Console.WriteLine();
                
                    Console.WriteLine("  Initializing an Opportunity from the initial Lead");

                    // 创建the request object
                    initialize.TargetEntityName = Opportunity.EntityLogicalName.ToString();

                    // 创建the EntityMoniker
                    initialize.EntityMoniker = _initialLead.ToEntityReference();

                    // 执行the request
                    initialized =
                        (InitializeFromResponse)service.Execute(initialize);

                    if (initialized.Entity != null &&
                        initialized.Entity.LogicalName == Opportunity.EntityLogicalName)
                    {
                        var opportunity = (Opportunity)initialized.Entity;
                        Console.WriteLine("  New Opportunity initialized successfully (Name={0})",
                            opportunity.Name);
                    }
                    
                    #endregion Demonstrate

                    #region Clean up
                    CleanUpSample(service);
                    #endregion Clean up
                }
                else
                {
                    const string UNABLE_TO_LOGIN_ERROR = "Unable to Login to Microsoft Dataverse";
                    if (service.LastCrmError.Equals(UNABLE_TO_LOGIN_ERROR))
                    {
                        Console.WriteLine("Check the connection string values in cds/App.config.");
                        throw new Exception(service.LastCrmError);
                    }
                    else
                    {
                        throw service.LastCrmException;
                    }
                }
            }
            catch (Exception ex)
            {
                SampleHelpers.HandleException(ex);
            }

            finally
            {
                if (service != null)
                    service.Dispose();

                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
            }

        }
    }
}
