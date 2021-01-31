using Konscious.Security.Cryptography;
using System;
using System.Buffers.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ToDoList.Modules
{
    public class Argon2idHasher : IPasswordHasher
    {
        private readonly RNGCryptoServiceProvider rng;

        public Argon2idHasher()
        {
            rng = new RNGCryptoServiceProvider();
        }

        public bool CompareHashAndPassword(string hashString, string password)
        {
            var (degreeOfParallelism, iterations, memorySize, salt, hash) = DecodeHash(hashString);
            var hasher = GetHasher(password);
            hasher.DegreeOfParallelism = degreeOfParallelism;
            hasher.Iterations = iterations;
            hasher.MemorySize = memorySize;
            hasher.Salt = salt;

            return hasher.GetBytes(hash.Length).SequenceEqual(hash);
        }

        public string HashFromPassword(string password) 
        {
            var hasher = GetHasher(password);
            HydrateHasher(hasher);
            return EncodeHash(
                hasher.DegreeOfParallelism, hasher.Iterations, 
                hasher.MemorySize, hasher.Salt, hasher.GetBytes(64));
        }

        private void HydrateHasher(Argon2id hasher)
        {
            hasher.DegreeOfParallelism = Environment.ProcessorCount;
            hasher.Iterations = 5;
            hasher.MemorySize = 131_072; // 128MiB
            hasher.Salt = GetRandomSalt(32);
        }

        private Argon2id GetHasher(string password) =>
            new Argon2id(StringToBytes(password));

        private byte[] GetRandomSalt(uint size)
        {
            var bytes = new byte[size];
            rng.GetBytes(bytes);
            return bytes;
        }

        private string BytesToString(byte[] bytes) =>
            Convert.ToBase64String(bytes);

        private byte[] StringToBytes(string str) =>
            Convert.FromBase64String(str);

        // This is not the standardized way to store hashes!
        // I just dont remember how this string has to be assembled, so
        // because I can not google for it, I created my own format. :D
        private string EncodeHash(
            int degreeOfParallelism, int iterations,
            int memorySize, byte[] salt, byte[] hash) =>
            $"$argon2id$dop={degreeOfParallelism}$itr={iterations}$mem={memorySize}$slt={BytesToString(salt)}$hsh={BytesToString(hash)}";

        private (int DegreeOfParallelism, int Iterations, int MemorySize, byte[] salt, byte[] hash) DecodeHash(string hash)
        {
            if (hash.StartsWith("$argon2id"))
                throw new ArgumentException("not an argon2id hash");

            var dop = int.Parse(GetKVValue(hash, "dop"));
            var itr = int.Parse(GetKVValue(hash, "itr"));
            var mem = int.Parse(GetKVValue(hash, "mem"));
            var slt = StringToBytes(GetKVValue(hash, "slt"));
            var hsh = StringToBytes(GetKVValue(hash, "hsh"));

            return (dop, itr, mem, slt, hsh);
        }

        private static string GetKVValue(string hash, string key)
        {
            key = $"${key}=";
            var i = hash.IndexOf(key);
            if (i < 0)
                throw new ArgumentException($"key '{key}' not found in hash");

            var val = hash[(i + key.Length)..];
            i = val.IndexOf('$');
            if (i > 0)
                val = val[0..i];

            return val;
        }
    }
}
