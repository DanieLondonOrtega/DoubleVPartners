
using System.Security.Cryptography;
using System.Text;
using DoubleVPartners.Common.Dto;
using Microsoft.Extensions.Configuration;

namespace DoubleVPartners.Common.Crypto
{
    public class Crypto : ICrypto
    {
        private readonly IConfiguration _configuration;
        private const string CryptoSection = "Crypto";

        public Crypto(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Decrypt(string cipherText, CryptoDto options)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(cipherText);
            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(options.EncryptSecret));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public string Encrypt(string cipherText, CryptoDto options)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(cipherText);
            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(options.EncryptSecret));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }
    }
}
