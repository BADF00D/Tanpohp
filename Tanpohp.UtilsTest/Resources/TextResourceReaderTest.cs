#region usings

using System.Text;
using NUnit.Framework;
using Tanpohp.Utils.Resources;

#endregion

namespace Tanpohp.UtilsTest.Resources
{
    [TestFixture]
    public class TextResourceReaderTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _reader = new TextResourceReader(Encoding.UTF8);
        }

        #endregion

        private IEmbeddedRessourceReader<string> _reader;

        [Test]
        public void TestLoadTxt()
        {
            _reader.LoadResource("Resources.EmbeddedResource.txt",
                                 typeof (XDocumentResourceReaderTest).Assembly);
        }
    }
}