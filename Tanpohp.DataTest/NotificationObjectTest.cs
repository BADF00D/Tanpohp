#region usings

using NUnit.Framework;
using Tanpohp.Data;

#endregion

namespace Tanpohp.DataTest
{
    [TestFixture]
    public class NotificationObjectTest
    {
        private Dummy _dummy;

        [SetUp]
        public void Setup()
        {
            _dummy = new Dummy ();
        }

        [Test]
        public void TestSetPropertyWithAssignWithSelectorExpression()
        {
            string propertyNameOfChanging = string.Empty, propertyNameOfChanged = string.Empty;
            _dummy.PropertyChanged += (sender, args) => propertyNameOfChanged = args.PropertyName;
            _dummy.PropertyChanging += (sender, args) => propertyNameOfChanging = args.PropertyName;
            _dummy.PropertyWithAssignWithSelectorExpression = 1;

            Assert.AreEqual("PropertyWithAssignWithSelectorExpression", propertyNameOfChanged, "Changed not invoked correktly");
            Assert.AreEqual("PropertyWithAssignWithSelectorExpression", propertyNameOfChanging, "Changing not invoked correktly");
        }

        [Test]
        public void TestSetPropertyWithAssignWithString()
        {
            string propertyNameOfChanging = string.Empty, propertyNameOfChanged = string.Empty;
            _dummy.PropertyChanged += (sender, args) => propertyNameOfChanged = args.PropertyName;
            _dummy.PropertyChanging += (sender, args) => propertyNameOfChanging = args.PropertyName;
            _dummy.PropertyWithAssignWithString = 1;

            Assert.AreEqual("PropertyWithAssignWithString", propertyNameOfChanged, "Changed not invoked correktly");
            Assert.AreEqual("PropertyWithAssignWithString", propertyNameOfChanging, "Changing not invoked correktly");
        }


        private class Dummy : NotificationObject
        {
            private int _property;
            public int PropertyWithAssignWithSelectorExpression
            {
                private get { return _property; }
                set { Assign(ref _property, value, () => PropertyWithAssignWithSelectorExpression); }
            }

            public int PropertyWithAssignWithString
            {
                private get { return _property; }
                set { Assign(ref _property, value, "PropertyWithAssignWithString"); }
            }
        }
    }
}
