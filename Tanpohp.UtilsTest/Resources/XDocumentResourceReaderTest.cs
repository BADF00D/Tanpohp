#region usings

using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
using Tanpohp.Utils.Resources;

#endregion

namespace Tanpohp.UtilsTest.Resources
{
    [TestFixture]
    public class XDocumentResourceReaderTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _reader = new XDocumentResourceReader();
        }

        #endregion

        private IEmbeddedRessourceReader<XDocument> _reader;

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
            _reader.LoadResource("Resources.EmbeddedResource.xml",
                                 typeof (XDocumentResourceReaderTest).Assembly);
        }
    }
}