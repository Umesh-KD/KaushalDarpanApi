using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
public class AesEncryptionHelperMobile
{

   public static string key = "8080808080808080";  // Must be 16, 24, or 32 chars (for AES-128/192/256)
    public static string iv = "8080808080808080";   // Must be 16 chars for AES CBC
    public static string DecryptData(string cipherTextBase64)
    {
        // Convert key, IV, and cipher text from strings to byte arrays
        byte[] cipherBytes = Convert.FromBase64String(cipherTextBase64);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (MemoryStream ms = new MemoryStream(cipherBytes))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader reader = new StreamReader(cs, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
