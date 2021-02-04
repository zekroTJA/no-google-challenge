using NUnit.Framework;
using System;
using ToDoList.Modules;

namespace ToDoList.Test.Modules
{
    [TestFixture]
    class Argon2idHasherTest
    {
        [Test]
        public void CompareHashAndPasswordTest()
        {
            const string password = "my_test_password";
            const string wrongPassword = "my_wrong_password";

            var hasher = new Argon2idHasher();

            var hash = hasher.HashFromPassword(password);
            Assert.IsTrue(hasher.CompareHashAndPassword(hash, password));
            Assert.IsFalse(hasher.CompareHashAndPassword(hash, wrongPassword));

            Assert.Throws<ArgumentException>(() => 
                hasher.CompareHashAndPassword(hash, ""));
            Assert.Throws<ArgumentException>(() =>
                hasher.CompareHashAndPassword("", password));
            Assert.Throws<ArgumentNullException>(() => 
                hasher.CompareHashAndPassword(hash, null));
            Assert.Throws<ArgumentNullException>(() =>
                hasher.CompareHashAndPassword(null, hash));
        }

        [Test]
        public void HashPasswordTest()
        {
            const string password = "my_test_password";

            var hasher = new Argon2idHasher();

            var hash1 = hasher.HashFromPassword(password);
            var hash2 = hasher.HashFromPassword(password);

            Assert.IsNotEmpty(hash1);
            Assert.IsNotEmpty(hash2);
            Assert.AreNotEqual(hash1, hash2);

            Assert.Throws<ArgumentException>(() => hasher.HashFromPassword(""));
            Assert.Throws<ArgumentNullException>(() => hasher.HashFromPassword(null));
        }
    }
}
