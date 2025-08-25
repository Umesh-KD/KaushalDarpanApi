using Kaushal_Darpan.Models.AadhaarEsignAuth;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Kaushal_Darpan.Core.Helper
{
    public class AadhaarEsignAesEncryption
    {
        public static EncryptionResultModel Encrypt(string plainText, string keyString)
        {
            EncryptionResultModel model = new EncryptionResultModel();
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);
                if (keyBytes.Length != 32)
                {
                    Array.Resize(ref keyBytes, 32);
                }

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = keyBytes;
                    aesAlg.GenerateIV();
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        model.IsSuccess = true;
                        model.EncriptedData = msEncrypt.ToArray();
                        return model;
                    }
                }
            }
            catch (System.Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                return model;
            }
        }

        public static EncryptionResultModel Decrypt(string encryptedText, string keyString)
        {
            EncryptionResultModel model = new EncryptionResultModel();
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);
            byte[] encData = Convert.FromBase64String(encryptedText);
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = keyBytes;
                    byte[] iv = new byte[aesAlg.IV.Length];
                    Array.Copy(encData, 0, iv, 0, iv.Length);
                    aesAlg.IV = iv;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (MemoryStream msDecrypt = new MemoryStream(encData, iv.Length, encData.Length - iv.Length))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                model.IsSuccess = true;
                                model.DecriptedData = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
            }
            return model;
        }
        public static string ComputeSHA256Hash(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hashBytes = sha256Hash.ComputeHash(inputBytes); 
                StringBuilder hex = new StringBuilder(hashBytes.Length * 2);
                foreach (byte b in hashBytes)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
        }
        public static string DecryptNew(string base64EncryptedData, string base64Key)
        {
            byte[] key = Convert.FromBase64String(base64Key);
            byte[] iv = new byte[16]; // must match the IV used in encryption
            byte[] cipherText = Convert.FromBase64String(base64EncryptedData);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }
        public static EncryptionResultModel EncryptNEw(string plainText, string base64Key)
        {
            var result = new EncryptionResultModel();
            try
            {
                byte[] key = Convert.FromBase64String(base64Key);
                byte[] iv = new byte[16]; // or use a random 16-byte IV for better security

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor();
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    result.EncriptedData = encrypted;
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static EncryptionResultModel Encrypt_New(string plainText, string key)
        {
            EncryptionResultModel model = new EncryptionResultModel();
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                // Ensure the key is exactly 32 bytes (AES-256)
                if (keyBytes.Length != 32)
                {
                    Array.Resize(ref keyBytes, 32);
                }

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = keyBytes;
                    aesAlg.GenerateIV();
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        model.IsSuccess = true;
                        model.EncriptedData = msEncrypt.ToArray();
                    }
                }
                return model;
            }
            catch (System.Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
                return model;
            }
        }

    }
}
