using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace BlockChainApp
{
    static class CHash
    {
        public static string DecryptDES(string key,string cipher)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipher);
            using(Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using(MemoryStream memoryStream=new MemoryStream(buffer))
                {
                    using(CryptoStream cryptoStream=new CryptoStream((Stream)memoryStream,decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader reader=new StreamReader((Stream)cryptoStream))
                        {
                            return reader.ReadToEnd();
                        }

                    }
                }
            }
        }
        public static string EncryptDES(string data,string key)
        {
            using(Aes aes = Aes.Create())
            {
                byte[] iv = new byte[16];
                byte[] array;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter((Stream)cryptoStream))
                            {
                                writer.Write(data);
                            }
                            array = memoryStream.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(array);
            }
        }
        public static string EncryptSHA256(string data,string key)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                bytes = hash.ComputeHash(bytes);
                using(HMACSHA1 keyhash=new HMACSHA1(Encoding.UTF8.GetBytes(key)))
                {
                    keyhash.ComputeHash(bytes);
                }
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
