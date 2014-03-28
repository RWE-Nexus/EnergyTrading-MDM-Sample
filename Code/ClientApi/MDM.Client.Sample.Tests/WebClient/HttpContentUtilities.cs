namespace Mdm.Client.Sample.Tests.WebClient
{
    using System.IO;
    using System.Net.Http;
    using System.ServiceModel.Syndication;
    using System.Text;
    using System.Xml;

    public class HttpContentUtilities
    {
        public static StreamContent CreateContentFromAtom10SyndicationFeed(SyndicationFeed feed)
        {
            SyndicationFeedFormatter formatter = new Atom10FeedFormatter<SyndicationFeed>(feed);
            var memoryStream = new MemoryStream();
            var settings = new XmlWriterSettings { Encoding = Encoding.UTF8, ConformanceLevel = ConformanceLevel.Document, Indent = true };
            using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
            {
                formatter.WriteTo(xmlWriter);
                xmlWriter.Flush();
                xmlWriter.Close();
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new StreamContent(memoryStream);
        }
    }
}