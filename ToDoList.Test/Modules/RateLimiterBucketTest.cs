using NUnit.Framework;
using System;
using ToDoList.Modules;

namespace ToDoList.Test.Modules
{
    [TestFixture]
    class RateLimiterBucketTest
    {
        [Test]
        public void GetTest()
        {
            var recover = TimeSpan.FromMilliseconds(200);
            const int burst = 5;

            var rlb = new RateLimiterBucket(burst, recover, (b, r) => new RateLimiter(b, r));

            var rl1 = rlb.Get("1");
            var rl2 = rlb.Get("2");

            Assert.NotNull(rl1);
            Assert.NotNull(rl2);

            Assert.AreSame(rl1, rlb.Get("1"));
            Assert.AreSame(rl2, rlb.Get("2"));
        }
    }
}
