using NUnit.Framework;
using System;
using System.Threading;
using ToDoList.Modules;

namespace ToDoList.Test.Modules
{
    [TestFixture]
    class RateLimiterTest
    {
        [Test]
        public void TryReserveTest()
        {
            var recover = TimeSpan.FromMilliseconds(200);
            const int burst = 5;

            var rl = new RateLimiter(burst, recover);
            Assert.AreEqual(burst, rl.Tokens);

            for (var i = 0; i < burst; i++)
            {
                Assert.AreEqual(burst - i, rl.Tokens);
                Assert.IsTrue(rl.TryReserve());
            }

            Assert.IsFalse(rl.TryReserve());

            Thread.Sleep(2 * recover);

            for (var i = 0; i < 2; i++)
                Assert.IsTrue(rl.TryReserve());

            Assert.IsFalse(rl.TryReserve());
        }

        [Test]
        public void TryReserve_OverflowTest()
        {
            var recover = TimeSpan.FromMilliseconds(200);
            const int burst = 2;

            var rl = new RateLimiter(burst, recover);

            Assert.IsTrue(rl.TryReserve());

            Thread.Sleep(2 * burst * recover);

            Assert.AreEqual(burst, rl.Tokens);

            for (var i = 0; i < burst; i++)
                Assert.IsTrue(rl.TryReserve());

            Assert.IsFalse(rl.TryReserve());
        }
    }
}
