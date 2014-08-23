#region usings

using System.Collections.Generic;
using NUnit.Framework;
using Tanpohp.Data;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.DataTest
{
    [TestFixture]
    public class ChangeTrackingNotificationObjectTest
    {
        private Dummy _dummy;

        private List<string> _changingProperties, _changedProperties;

        [SetUp]
        public void Setup()
        {
            _dummy = new Dummy();
            _changingProperties = new List<string>();
            _changedProperties = new List<string>();
            _dummy.PropertyChanged += (sender, args) => _changedProperties.Add(args.PropertyName);
            _dummy.PropertyChanging += (sender, args) => _changingProperties.Add(args.PropertyName);
        }

        [Test]
        public void TestAssignTrackedWithAssignWithSelectorExpression()
        {
            _dummy.PropertyWithAssignWithSelectorExpression = 1;

            Assert.IsTrue(_changedProperties.Contains("PropertyWithAssignWithSelectorExpression"), "Changed not invoked correktly");
            Assert.IsTrue(_changingProperties.Contains("PropertyWithAssignWithSelectorExpression"), "Changing not invoked correktly");
        }

        [Test]
        public void TestAssignTrackedWithAssignWithString()
        {
            _dummy.PropertyWithAssignWithString = 1;

            Assert.IsTrue(_changedProperties.Contains("PropertyWithAssignWithString"), "Changed not invoked correktly");
            Assert.IsTrue(_changingProperties.Contains("PropertyWithAssignWithString"), "Changing not invoked correktly");
        }

        [Test]
        public void TestContainsChanges()
        {
            _dummy.PropertyWithAssignWithString = 1;

            Assert.IsTrue(_dummy.ContainsChanges);
        }

        [Test]
        public void TestGetChangedProperties()
        {
            _dummy.PropertyWithAssignWithSelectorExpression = 1;
            _dummy.PropertyWithAssignWithString = 2;

            var changedProperties = _dummy.GetChangedProperties();
            Assert.IsTrue(changedProperties.Contains("PropertyWithAssignWithSelectorExpression"));
            Assert.IsTrue(changedProperties.Contains("PropertyWithAssignWithString"));
            Assert.IsFalse(changedProperties.Contains(this.GetPropertyName(()=>_dummy.ContainsChanges)));
        }

        [Test]
        public void TestClear()
        {
            _dummy.PropertyWithAssignWithSelectorExpression = 1;
            _dummy.PropertyWithAssignWithString = 2;
            _dummy.ClearChanges();

            var changedProperties = _dummy.GetChangedProperties();
            Assert.AreEqual(0,changedProperties.Count);
            Assert.IsFalse(_dummy.ContainsChanges);
            Assert.AreEqual(0, _dummy.GetAmountOfChangedProperties());
        }

        [Test]
        public void TestBuildNestedName()
        {
            Assert.AreEqual("one.two", ChangeTrackingNotificationObject.BuildNestedName("one", "two"));
        }

        private class Dummy : ChangeTrackingNotificationObject
        {
            private int _property;
            public int PropertyWithAssignWithSelectorExpression
            {
                private get { return _property; }
                set { AssignTracked(ref _property, value, () => PropertyWithAssignWithSelectorExpression); }
            }

            public int PropertyWithAssignWithString
            {
                private get { return _property; }
                set { AssignTracked(ref _property, value, "PropertyWithAssignWithString"); }
            }
        }
    }
}
