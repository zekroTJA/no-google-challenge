﻿using Konscious.Security.Cryptography;
using System;
using System.Buffers.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ToDoList.Util;

namespace ToDoList.Modules
{
    public class Argon2idHasher : IPasswordHasher
    {
        public Argon2idHasher()
        { }

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
            hasher.Salt = CryptoRandom.GetBytes(32);
        }

        private Argon2id GetHasher(string password) =>
            new Argon2id(Encoding.UTF8.GetBytes(password));

        // This is not the standardized way to store hashes!
        // I just dont remember how this string has to be assembled, so
        // because I can not google for it, I created my own format. :D
        private string EncodeHash(
            int degreeOfParallelism, int iterations,
            int memorySize, byte[] salt, byte[] hash) =>
            $"$argon2id$dop={degreeOfParallelism}$itr={iterations}$mem={memorySize}$slt={Convert.ToBase64String(salt)}$hsh={Convert.ToBase64String(hash)}";

        private (int DegreeOfParallelism, int Iterations, int MemorySize, byte[] salt, byte[] hash) DecodeHash(string hash)
        {
            if (!hash.StartsWith("$argon2id"))
                throw new ArgumentException("not an argon2id hash");

            var dop = int.Parse(GetKVValue(hash, "dop"));
            var itr = int.Parse(GetKVValue(hash, "itr"));
            var mem = int.Parse(GetKVValue(hash, "mem"));
            var slt = Convert.FromBase64String(GetKVValue(hash, "slt"));
            var hsh = Convert.FromBase64String(GetKVValue(hash, "hsh"));

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
