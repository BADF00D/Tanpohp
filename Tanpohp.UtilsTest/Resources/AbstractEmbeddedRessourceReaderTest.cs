#region usings

using System.IO;
using NUnit.Framework;
using Tanpohp.Utils.Resources;

#endregion

namespace Tanpohp.UtilsTest.Resources
{
    [TestFixture]
    public class AbstractEmbeddedRessourceReaderTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _reader = new Reader();
        }

        #endregion

        private IEmbeddedRessourceReader<string> _reader;

        public class Reader : AbstractEmbeddedRessourceReader<string>
        {
            protected override string ReadResourceFromStream(Stream stream)
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        [Test]
        public void TestLoad1()
        {
            var text = _reader.LoadResource("Resources.EmbeddedRessource.txt",
                                            typeof (AbstractEmbeddedRessourceReaderTest).Assembly);
            Assert.AreEqual("test", text);
        }

        [Test]
        [ExpectedException]
        public void TestLoad2()
        {
            _reader.LoadResource("Resources.Resource.txt", typeof (AbstractEmbeddedRessourceReaderTest).Assembly);
        }
    }
}