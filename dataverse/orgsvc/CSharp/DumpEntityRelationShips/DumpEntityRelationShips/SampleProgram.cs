using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PowerApps.Samples
{
    public partial class SampleProgram
    {
        // 创建storage for new attributes being created
        public List<Microsoft.Xrm.Sdk.Metadata.AttributeMetadata> addedAttributes;

        // Specify which language code to use in the sample. If you are using a language
        // other than US English, you will need to modify this value accordingly.
        // 参见 https://learn.microsoft.com/previous-versions/windows/embedded/ms912047(v=winembedded.10)
        public const int _languageCode = 1033;

        // 定义IDs/variables needed for this sample.
        public int _insertedStatusValue;
        [STAThread] // Required to support the interactive login experience
        static void Main(string[] args)
        {
            CrmServiceClient service = null;
            try
            {
                service = SampleHelpers.Connect("Connect");
                if (service.IsReady)
                {
                    #region Demonstrate
                    // 创建any entity records that the demonstration code requires
                    SetUpSample(service);
                    var request = new RetrieveAllEntitiesRequest()
                    {
                        EntityFilters = EntityFilters.Relationships,
                        RetrieveAsIfPublished = true
                    };

                    // 检索the MetaData.
                    var response = (RetrieveAllEntitiesResponse)service.Execute(request);


                    // 创建an instance of StreamWriter to write text to a file.
                    // using statement also closes the StreamWriter.
                    // To view this file, right click the file and choose open with Excel. 
                    // Excel will figure out the schema and display the information in columns.

                    String filename = String.Concat("RelationshipInfo.xml");
                    using (var sw = new StreamWriter(filename))
                    {
                        // 创建Xml Writer.
                        var metadataWriter = new XmlTextWriter(sw);

                        // 开始Xml File.
                        metadataWriter.WriteStartDocument();

                        // Metadata Xml Node.
                        metadataWriter.WriteStartElement("Metadata");

                        foreach (EntityMetadata currentEntity in response.EntityMetadata)
                        {
                            //if (currentEntity.IsIntersect.Value == false)
                            if (true)
                            {
                                // 开始Entity Node
                                metadataWriter.WriteStartElement("Entity");

                                // Write the Entity's Information.
                                metadataWriter.WriteElementString("EntitySchemaName", currentEntity.SchemaName);

                                // 开始OneToManyRelationships Node
                                metadataWriter.WriteStartElement("OneToManyRelationships");

                                foreach (OneToManyRelationshipMetadata currentRelationship in currentEntity.OneToManyRelationships)
                                {
                                    // 开始 Node
                                    metadataWriter.WriteStartElement("Relationship");

                                    metadataWriter.WriteElementString("OtoM_SchemaName", currentRelationship.SchemaName);
                                    metadataWriter.WriteElementString("OtoM_ReferencingEntity", currentRelationship.ReferencingEntity);
                                    metadataWriter.WriteElementString("OtoM_ReferencedEntity", currentRelationship.ReferencedEntity);
                                    // 结束 Node
                                    metadataWriter.WriteEndElement();

                                }

                                // 结束OneToManyRelationships Node
                                metadataWriter.WriteEndElement();

                                // 开始ManyToManyRelationships Node
                                metadataWriter.WriteStartElement("ManyToManyRelationships");


                                foreach (ManyToManyRelationshipMetadata currentRelationship in currentEntity.ManyToManyRelationships)
                                {
                                    // 开始 Node
                                    metadataWriter.WriteStartElement("Relationship");

                                    metadataWriter.WriteElementString("MtoM_SchemaName", currentRelationship.SchemaName);
                                    metadataWriter.WriteElementString("MtoM_Entity1", currentRelationship.Entity1LogicalName);
                                    metadataWriter.WriteElementString("MtoM_Entity2", currentRelationship.Entity2LogicalName);
                                    metadataWriter.WriteElementString("IntersectEntity", currentRelationship.IntersectEntityName);
                                    // 结束 Node
                                    metadataWriter.WriteEndElement();

                                }

                                // 结束ManyToManyRelationships Node
                                metadataWriter.WriteEndElement();

                                // 开始ManyToOneRelationships Node
                                metadataWriter.WriteStartElement("ManyToOneRelationships");


                                foreach (OneToManyRelationshipMetadata currentRelationship in currentEntity.ManyToOneRelationships)
                                {
                                    // 开始 Node
                                    metadataWriter.WriteStartElement("Relationship");

                                    metadataWriter.WriteElementString("MtoO_SchemaName", currentRelationship.SchemaName);
                                    metadataWriter.WriteElementString("MtoO_ReferencingEntity", currentRelationship.ReferencingEntity);
                                    metadataWriter.WriteElementString("MtoO_ReferencedEntity", currentRelationship.ReferencedEntity);
                                    // 结束 Node
                                    metadataWriter.WriteEndElement();

                                }

                                // 结束ManyToOneRelationships Node
                                metadataWriter.WriteEndElement();

                                // 结束Relationships Node
                                // metadataWriter.WriteEndElement();



                                // 结束Entity Node
                                metadataWriter.WriteEndElement();
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
