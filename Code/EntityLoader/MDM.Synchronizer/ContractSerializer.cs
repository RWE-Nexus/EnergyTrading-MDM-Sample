namespace MDM.Sync
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    public class ContractSerializer
    {
        public static void SerializeToXml<T>(T entity, string outputPath) where T : class
        {
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                var settings = new XmlWriterSettings { Indent = true };
                using (var xmlWriter = XmlWriter.Create(fileStream, settings))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(xmlWriter, entity);
                }
            }
        }
    }
}