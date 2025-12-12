using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Kaushal_Darpan.Core.Helper
{
    public class JanAadharHelper
    {
        private const string AES_ALGORITHM = "AES";
        private const string RSA_ALGORITHM = "RSA";
        private RSA publicKey; // Initialize with your public key  
        private RSA privateKey; // Initialize with your private key  


        public string EncryptAESKeyWithRSA(Aes aesKey)
        {
            using (var cipher = RSA.Create())
            {
                cipher.ImportParameters(publicKey.ExportParameters(false));
                byte[] encryptedAESKey = cipher.Encrypt(aesKey.Key,
       RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(encryptedAESKey);
            }
        }


        public Aes DecryptAESKeyWithRSA(string encryptedAESKey)
        {
            byte[] encryptedAESKeyBytes =
       Convert.FromBase64String(encryptedAESKey);
            using (var cipher = RSA.Create())
            {
                cipher.ImportParameters(privateKey.ExportParameters(true));
                byte[] decryptedAESKeyBytes =
       cipher.Decrypt(encryptedAESKeyBytes, RSAEncryptionPadding.Pkcs1);
                var aes = Aes.Create();
                aes.Key = decryptedAESKeyBytes;
                return aes;
            }
        }

        private Aes GenerateAESKey()
        {
            var aes = Aes.Create();
            aes.KeySize = 256; // 256-bit AES key  
            aes.GenerateKey();
            return aes;
        }

        public string SignData(string data)
        {
            using (var signature = RSA.Create())
            {
                signature.ImportParameters(privateKey.ExportParameters(true));
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] digitalSignature = signature.SignData(dataBytes,
       HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                return Convert.ToBase64String(digitalSignature);
            }
        }


        public string EncryptDataWithAES(string data)
        {
            // Generate a new AES key  
            using (var aesKey = GenerateAESKey())
            {

                // Encrypt the data using AES  
                using (var cipher = Aes.Create())
                {
                    cipher.Mode = CipherMode.CBC;
                    cipher.Padding = PaddingMode.PKCS7;

                    byte[] iv = new byte[16]; // AES block size is 16 bytes  
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(iv);
                    }
                    cipher.IV = iv;
                    cipher.Key = aesKey.Key;

                    using (var encryptor = cipher.CreateEncryptor())
                    {
                        byte[] encryptedData =
       encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(data), 0,
       data.Length);

                        // Base64 encode the encrypted data and IV  
                        string encryptedDataBase64 =
       Convert.ToBase64String(encryptedData);
                        string ivBase64 = Convert.ToBase64String(iv);

                        // Encrypt the AES key with RSA  
                        string encryptedAESKey = EncryptAESKeyWithRSA(aesKey);

                        // Combine the AES-encrypted data and IV for transmission  
                        return
       $"{ivBase64}:{encryptedDataBase64}:{encryptedAESKey}";
                    }
                }
            }
        }

        public string DecryptDataWithAES(string encryptedData)
        {
            // Split the encrypted string into IV, encrypted data, and encrypted 
            //AES key
             string[] parts = encryptedData.Split(':');

            string ivBase64 = parts[0];
            string encryptedDataBase64 = parts[1];
            string encryptedAESKey = parts[2];

            // Decode the base64-encoded IV, encrypted data, and AES key  
            byte[] iv = Convert.FromBase64String(ivBase64);
            byte[] encryptedDataBytes =
       Convert.FromBase64String(encryptedDataBase64);

            // Decrypt the AES key using RSA  
            using (var aesKey = DecryptAESKeyWithRSA(encryptedAESKey))
            {
                // Decrypt the data with AES  
                using (var cipher = Aes.Create())
                {
                    cipher.Mode = CipherMode.CBC;
                    cipher.Padding = PaddingMode.PKCS7;
                    cipher.IV = iv;
                    cipher.Key = aesKey.Key;

                    using (var decryptor = cipher.CreateDecryptor())
                    {
                        byte[] decryptedData =
       decryptor.TransformFinalBlock(encryptedDataBytes, 0,
       encryptedDataBytes.Length);
                        return Encoding.UTF8.GetString(decryptedData);
                    }
                }
            }
        }

    }
}
