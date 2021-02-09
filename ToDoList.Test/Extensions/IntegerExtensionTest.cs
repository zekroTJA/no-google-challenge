using NUnit.Framework;
using ToDoList.Extensions;

namespace ToDoList.Test.Extensions
{
    [TestFixture]
    class IntegerExtensionTest
    {
        [Test]
        public void CapTest() 
        {
            Assert.AreEqual(-1, -1.Cap(2));
            Assert.AreEqual( 1,  1.Cap(2));
            Assert.AreEqual( 2,  2.Cap(2));
            Assert.AreEqual( 2,  3.Cap(2));
        }
    }
}
