using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Integration_Tests_Demo.Tests
{
    [TestFixture]
    public class MyTestFixture
    {
        [Test]
        public void MyTestMethod()
        {
            Assert.That(1, Is.EqualTo(1));
        }
    }
}
