using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Kaushal_Darpan.Core.Helper
{
    public class EmitraHelper
    {

        private static readonly Encoding encoding = Encoding.UTF8;
        public static string AESEncrypt(string textToEncrypt, string encryptionKey)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.Key = SHA256.Create().ComputeHash(encoding.GetBytes(encryptionKey));
                aes.IV = MD5.Create().ComputeHash(encoding.GetBytes(encryptionKey));
                ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] buffer = encoding.GetBytes(textToEncrypt);
                return Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception e)
            {
                throw new Exception("Error encrypting: " + e.Message);
            }
        }
        public static string AESDecrypt(string textToDecrypt, string encryptionKey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    aes.KeySize = 256;
                    aes.BlockSize = 128;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Mode = CipherMode.CBC;
                    aes.Key = SHA256.Create().ComputeHash(encoding.GetBytes(encryptionKey));
                    aes.IV = MD5.Create().ComputeHash(encoding.GetBytes(encryptionKey));

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    byte[] buffer = Convert.FromBase64String(textToDecrypt);
                    return encoding.GetString(decryptor.TransformFinalBlock(buffer, 0, buffer.Length));
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error decrypting: " + e.Message);
            }
        }
        public string CreateSHA256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateSha256Hash(byte[] message)
        {
            try
            {
                IDigest digest = new Sha256Digest();
                digest.Reset();
                byte[] buffer = new byte[digest.GetDigestSize()];
                digest.BlockUpdate(message, 0, message.Length);
                digest.DoFinal(buffer, 0);

                return Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static string GenerateSha256HashNew(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public static string Decrypt(string textToDecrypt, string EncryptionPassword)
        {
            //textToDecrypt = textToDecrypt.te;//.Replace(" ", "+");
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 256;
            rijndaelCipher.BlockSize = 128;
            int mod4 = textToDecrypt.Length % 4;
            if (mod4 > 0)
            {
                textToDecrypt += new string('=', 4 - mod4);
            }

            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(EncryptionPassword);
            pwdBytes = SHA256.Create().ComputeHash(pwdBytes);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }
        public static string Encrypt(string textToEncrypt, string EncryptionPassword)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 256;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(EncryptionPassword);
            pwdBytes = SHA256.Create().ComputeHash(pwdBytes);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        public static string SSOAuthentication(string SSOToken)
        {

            try
            {

                //Base String

                string baseAddress = "https://sso.rajasthan.gov.in:4443/SSOREST/GetTokenDetailJSON/" + SSOToken;//production

                //string baseAddress = "http://ssotest.rajasthan.gov.in:8888/SSOREST/GetTokenDetailJSON/" + SSOToken;

                //Create Web Request

                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));

                http.Method = "GET";

                http.Accept = "application/json";

                http.ContentType = "application/x-www-form-urlencoded";

                //Read Response for posting done

                var response = http.GetResponse();

                var stream = response.GetResponseStream();

                var sr = new StreamReader(stream);

                var content = sr.ReadToEnd();

                //return contents

                return content;

            }

            catch (Exception ex)

            {

                throw ex;

            }

        }




    }
}
