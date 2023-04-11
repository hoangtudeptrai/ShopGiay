using System.Security.Cryptography;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ShopGiay.Helps
{
    public class PassWorkHasher
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Interations = 10000;

        public static string MD5Pass(string password)
        {
            //Tạo MD5 
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes("Chuỗi cần mã hóa");
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


        public static string HashPassWork(string password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(password, salt, Interations);
            var hash = key.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);
            return base64Hash;
        }
        public static bool VerigyPassWork(string password, string base64Hash)
        {
            var hashByte = Convert.FromBase64String(base64Hash);
            var salt = new byte[SaltSize];
            Array.Copy(hashByte, 0, salt, 0, SaltSize);

            var key = new Rfc2898DeriveBytes(password, salt, Interations);
            byte[] hash = key.GetBytes(HashSize);

            for (var i = 0; i < HashSize; i++)
            {
                if (hashByte[i + SaltSize] != hash[i])
                    return false;
            }
            return true;
        }
    }
}
