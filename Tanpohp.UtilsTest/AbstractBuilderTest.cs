#region usings

using System;
using System.Diagnostics;
using NUnit.Framework;
using Tanpohp.Utils;

#endregion

namespace Tanpohp.UtilsTest
{
    [TestFixture]
    public class AbstractBuilderTest
    {
        [Test]
        public void TestBuild1()
        {
            Dummy.Builder.BuildWithInt(3).BuildWithObject(new object()).Build();
        }

        [Test]
        public void TestBuild2()
        {
            //is ok because Value types are not checked.
            Dummy.Builder.BuildWithObject(new object()).Build();
        }
        [Test]
        [ExpectedException(typeof(Exception))]
        public void FailTestBuild1()
        {
            //should fail because one reference type is not set.
            Dummy.Builder.BuildWithInt(3).Build();
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void FailTestClear()
        {
            //is ok because Value types are not checked.
            Dummy.Builder.BuildWithObject(new object()).BuildWithInt(3);
            Dummy.Builder.Reset();
            //this should fail because Reset should set object to null.
            Dummy.Builder.Build();
        }

        private class Dummy
        {
            private readonly int _intValue;
            private readonly object _objectValue;

            public Dummy(int intValue, object objectValue)
            {
                _intValue = intValue;
                _objectValue = objectValue;
            }

            public static readonly DummyBuilder Builder = new DummyBuilder();

            internal class DummyBuilder : AbstractBuilder<Dummy>
            {
                private int _intValue;
                private object _objectValue;


                [DebuggerStepThrough]
                public DummyBuilder BuildWithInt(int intValue)
                {
                    _intValue = intValue;

                    return this;
                }

                [DebuggerStepThrough]
                public DummyBuilder BuildWithObject(object objectValue)
                {
                    _objectValue = objectValue;

                    return this;
                }

                public override Dummy Build()
                {
                    Validate(null);

                    return new Dummy(_intValue, _objectValue);
                }
            }
        }

        
    }
}
