using RecebaFacil.Domain.Services;
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RecebaFacil.Service
{
    public class SecurityService : ISecurityService
    {
        private readonly byte[] _AESKey = Encoding.UTF8.GetBytes("Rec3b@Facl1@2K20");

        public string HashValue(string valor)
        {
            byte[] _sha256Result;
            using (SHA256 sha = SHA256.Create())
            {
                _sha256Result = sha.ComputeHash(Encoding.UTF8.GetBytes(valor));
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in _sha256Result)
                stringBuilder.AppendFormat("{0:x2}", b);

            return stringBuilder.ToString();
        }
        public bool Match(string hashText, string valor)
        {
            if (string.IsNullOrWhiteSpace(hashText) || string.IsNullOrWhiteSpace(valor))
                return false;

            return HashValue(valor).Equals(hashText);
        }

        public string EncryptValue(Guid valor) => EncryptValue(valor.ToString());
        public string EncryptValue(int valor) => EncryptValue(valor.ToString());
        public string EncryptValue(string valor)
        {
            byte[] _result;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _AESKey;
                aesAlg.IV = _AESKey;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(valor);
                        }
                        _result = msEncrypt.ToArray();
                    }
                }

            }
            return Convert.ToBase64String(_result);
        }

        public string DecryptValue(string valor)
        {
            byte[] cryptedValue = Convert.FromBase64String(valor);
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = _AESKey;
            aesAlg.IV = _AESKey;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(cryptedValue);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
