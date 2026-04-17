using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace QuantityMeasurementAppBusinessLayer.Helpers
{
    public class EncryptionHelper
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptionHelper(IConfiguration configuration)
        {
            _key = Encoding.UTF8.GetBytes(configuration["EncryptionSettings:Key"]!);
            _iv = Encoding.UTF8.GetBytes(configuration["EncryptionSettings:IV"]!);
        }

        public string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);

            sw.Write(plainText);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            using Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            byte[] buffer = Convert.FromBase64String(cipherText);

            using var ms = new MemoryStream(buffer);
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}