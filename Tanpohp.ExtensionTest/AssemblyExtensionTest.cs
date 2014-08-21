#region usings

using System;
using NUnit.Framework;
using Simple.Mocking;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.ExtensionTest
{
    [TestFixture]
    public class AssemblyExtensionTest
    {
        [Test]
        public static void TestGetReferencedAssembliesRecursive1()
        {
            var assemblies = typeof (Mock).Assembly.GetReferencedAssembliesRecursive();

            Assert.AreEqual(8, assemblies.Count);
        }

        [Test]
        public static void TestTryGetAssembly1()
        {
            var assembly = typeof(Mock).Assembly.GetName().TryGetAssembly();

            Assert.IsNotNull(assembly);
        }
    }
}