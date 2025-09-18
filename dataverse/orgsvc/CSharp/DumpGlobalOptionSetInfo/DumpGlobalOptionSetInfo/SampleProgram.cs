using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.IO;
using System.Linq;
using System.Xml;

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

                    var retrieveAllOptionSetsRequest = new RetrieveAllOptionSetsRequest();

                    // 执行the request
                    var retrieveAllOptionSetsResponse = (RetrieveAllOptionSetsResponse)service.Execute(
                        retrieveAllOptionSetsRequest);

                    // 创建an instance of StreamWriter to write text to a file.
                    // using statement also closes the StreamWriter.
                    // To view this file, right click the file and choose open with Excel. 
                    // Excel will figure out the schema and display the information in columns.

                    String filename = String.Concat("AllOptionSetValues.xml");
                    using (var sw = new StreamWriter(filename))
                    {
                        // 创建Xml Writer.
                        var metadataWriter = new XmlTextWriter(sw);

                        // 开始Xml File.
                        metadataWriter.WriteStartDocument();

                        // Metadata Xml Node.
                        metadataWriter.WriteStartElement("Metadata");

                        if (retrieveAllOptionSetsResponse.OptionSetMetadata.Count() > 0)
                        {

                            foreach (OptionSetMetadataBase optionSetMetadataBase in
                                retrieveAllOptionSetsResponse.OptionSetMetadata)
                            {
                                if (optionSetMetadataBase.OptionSetType != null)
                                {
                                    if ((OptionSetType)optionSetMetadataBase.OptionSetType == OptionSetType.Picklist)
                                    {
                                        OptionSetMetadata optionSetMetadata = (OptionSetMetadata)optionSetMetadataBase;
                                        // 开始OptionSet Node
                                        metadataWriter.WriteStartElement("OptionSet");
                                        metadataWriter.WriteAttributeString("OptionSetType", OptionSetType.Picklist.ToString());
                                        metadataWriter.WriteElementString("OptionSetDisplayName",
                                            (optionSetMetadata.DisplayName.LocalizedLabels.Count > 0) ? optionSetMetadata.DisplayName.LocalizedLabels[0].Label : String.Empty);

                                        // Writes the options
                                        metadataWriter.WriteStartElement("Options");

                                        foreach (OptionMetadata option in optionSetMetadata.Options)
                                        {
                                            metadataWriter.WriteStartElement("Option");
                                            metadataWriter.WriteElementString("OptionValue", option.Value.ToString());
                                            metadataWriter.WriteElementString("OptionDescription", option.Label.UserLocalizedLabel.Label.ToString());
                                            metadataWriter.WriteEndElement();
                                        }
                                        metadataWriter.WriteEndElement();

                                        // 结束OptionSet Node
                                        metadataWriter.WriteEndElement();
                                    }
                                    else if ((OptionSetType)optionSetMetadataBase.OptionSetType == OptionSetType.Boolean)
                                    {
                                        BooleanOptionSetMetadata optionSetMetadata = (BooleanOptionSetMetadata)optionSetMetadataBase;
                                        // 开始OptionSet Node
                                        metadataWriter.WriteStartElement("OptionSet");
                                        metadataWriter.WriteAttributeString("OptionSetType", OptionSetType.Boolean.ToString());
                                        if (optionSetMetadata.DisplayName.LocalizedLabels.Count != 0)
                                            metadataWriter.WriteElementString("OptionSetDisplayName", optionSetMetadata.DisplayName.LocalizedLabels[0].Label);
                                        else
                                            metadataWriter.WriteElementString("OptionSetDisplayName", "UNDEFINED");

                                        // Writes the TrueOption
                                        metadataWriter.WriteStartElement("TrueOption");
                                        metadataWriter.WriteElementString("OptionValue", optionSetMetadata.TrueOption.Value.ToString());
                                        metadataWriter.WriteElementString("OptionDescription", optionSetMetadata.TrueOption.Label.UserLocalizedLabel.Label.ToString());
                                        metadataWriter.WriteEndElement();
                                        // Writes the FalseOption
                                        metadataWriter.WriteStartElement("FalseOption");
                                        metadataWriter.WriteElementString("OptionValue", optionSetMetadata.FalseOption.Value.ToString());
                                        metadataWriter.WriteElementString("OptionDescription", optionSetMetadata.FalseOption.Label.UserLocalizedLabel.Label.ToString());
                                        metadataWriter.WriteEndElement();

                                        // 结束OptionSet Node
                                        metadataWriter.WriteEndElement();
                                    }
                                }
                            }
                        }

                        // 结束Metadata Xml Node
                        metadataWriter.WriteEndElement();
                        metadataWriter.WriteEndDocument();

                        // Close xml writer.
                        metadataWriter.Close();
                    }

                    Console.WriteLine("Done.");
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
