#region usings

using NUnit.Framework;
using Tanpohp.Data;

#endregion

namespace Tanpohp.DataTest
{
    [TestFixture]
    public class SealableObjectTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _sealable = new SealableDummy();
        }

        #endregion

        private SealableDummy _sealable;

        private class SealableDummy : SealableObject
        {
            private string _property;

            public string Property
            {
                get { return _property; }
                set { Assign(ref _property, value); }
            }
        }

        [Test]
        public void TestNormal()
        {
            _sealable.Property = "bla";

            Assert.AreEqual("bla", _sealable.Property);
        }

        [Test]
        [ExpectedException]
        public void TestSeal()
        {
            _sealable.Seal();
            _sealable.Property = "bla";
        }
    }
}