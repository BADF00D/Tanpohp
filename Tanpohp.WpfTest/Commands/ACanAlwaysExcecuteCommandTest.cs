#region usings

using System;
using NUnit.Framework;
using Tanpohp.Wpf.Commands;

#endregion

namespace Tanpohp.WpfTest.Commands
{
    [TestFixture]
    public class ACanAlwaysExcecuteCommandTest
    {
        private class DummyACanAlwaysExcecuteCommand : ACanAlwaysExcecuteCommand
        {
            public override void Execute(object parameter)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void CanExcecuteWithInteger()
        {
            var dummy = new DummyACanAlwaysExcecuteCommand();

            Assert.IsTrue(dummy.CanExecute(5));
        }

        [Test]
        public void CanExcecuteWithNull()
        {
            var dummy = new DummyACanAlwaysExcecuteCommand();

            Assert.IsTrue(dummy.CanExecute(null));
        }

        [Test]
        public void CanExcecuteWithObject()
        {
            var dummy = new DummyACanAlwaysExcecuteCommand();

            Assert.IsTrue(dummy.CanExecute(new object()));
        }
    }
}