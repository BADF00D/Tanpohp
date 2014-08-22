#region usings

using System;
using System.Threading;
using NUnit.Framework;
using Tanpohp.Utils.Environment;

#endregion

namespace Tanpohp.UtilsTest.Environment
{
    [TestFixture]
    public class EnvironmentVariableProviderTest
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _provider = new EnvironmentVariableProvider(100, true);
        }

        [TearDown]
        public void TearDown()
        {
            System.Environment.SetEnvironmentVariable(TmpVariableName, null, EnvironmentVariableTarget.User);
        }

        #endregion

        private IEnvironmentVariableProvider _provider;

        private const string TmpVariableName = "asdfgDeleteMehjkl";

        [Test]
        public void TestExists()
        {
            Assert.IsFalse(_provider.Exists(""));
            Assert.IsTrue(_provider.Exists("TMP", EnvironmentVariableTarget.Machine));
            Assert.IsTrue(_provider.Exists("TMP"));
            Assert.IsTrue(_provider.Exists("windir", EnvironmentVariableTarget.Machine));
            Assert.IsFalse(_provider.Exists("windir"));
        }

        [Test]
        public void TestSelfRefreshViaGet()
        {
            System.Environment.SetEnvironmentVariable(TmpVariableName, "HalloWorld", EnvironmentVariableTarget.User);
            var beforeChangeValue = _provider.Get(TmpVariableName);
            Thread.Sleep(200);
            System.Environment.SetEnvironmentVariable(TmpVariableName, "HalloWorld2", EnvironmentVariableTarget.User);
            var afterChangeValue = _provider.Get(TmpVariableName);

            Assert.AreNotEqual(beforeChangeValue, afterChangeValue);
        }

        [Test]
        public void TestRefresh()
        {
            System.Environment.SetEnvironmentVariable(TmpVariableName, "HalloWorld", EnvironmentVariableTarget.User);
            var beforeChangeValue = _provider.Get(TmpVariableName);
            System.Environment.SetEnvironmentVariable(TmpVariableName, "HalloWorld2", EnvironmentVariableTarget.User);
            _provider.Refresh();
            var afterChangeValue = _provider.Get(TmpVariableName);

            Assert.AreNotEqual(beforeChangeValue, afterChangeValue);
            Assert.AreEqual("HalloWorld2", afterChangeValue);
        }
    }
}