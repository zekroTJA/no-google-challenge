using System.Security.Cryptography;

namespace ToDoList.Util
{
    public class CryptoRandom
    {
        public static byte[] GetBytes(uint size)
        {
            var bytes = new byte[size];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            return bytes;
        }
    }
}
