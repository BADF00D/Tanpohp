#region usings

using System.Xml;
using System.Xml.Schema;
using NUnit.Framework;
using Tanpohp.Utils.Resources;

#endregion

namespace Tanpohp.UtilsTest.Resources
{
    [TestFixture]
    public class XmlSchemaRessourceReaderTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _reader = new XmlSchemaRessourceReader();
        }

        #endregion

        private IEmbeddedRessourceReader<XmlSchema> _reader;

        [Test]
        [ExpectedException(typeof (XmlException))]
        public void FailTestLoadXml()
        {
            _reader.LoadResource("Resources.EmbeddedResource.txt",
                                 typeof (XDocumentResourceReaderTest).Assembly);
        }

        [Test]
        public void TestLoadXml()
        {
            _reader.LoadResource("Resources.EmbeddedResource.xsd",
                                 typeof (XDocumentResourceReaderTest).Assembly);
        }
    }
}