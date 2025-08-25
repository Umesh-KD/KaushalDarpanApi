using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;

namespace Kaushal_Darpan.Core.Helper
{
    public class ENCRYPTER
    {
        private const string ASYMMETRIC_ALGO = "RSA/ECB/PKCS1Padding";
        private const int SYMMETRIC_KEY_SIZE = 256;
        private DateTime certExpiryDate;
        private RsaKeyParameters publicKey;
        public static int IV_SIZE_BITS = 96;
        // Additional authentication data - last 128 bits of ISO format timestamp
        public static int AAD_SIZE_BITS = 128;
        // Authentication tag length - in bits
        public static int AUTH_TAG_SIZE_BITS = 128;

        public ENCRYPTER(string publicKeyFilePath)
        {
            FileStream fileStream = null;
            try
            {
                using (fileStream = new FileStream(publicKeyFilePath, FileMode.Open, FileAccess.Read))
                {
                    Org.BouncyCastle.X509.X509Certificate x509Certificate = new Org.BouncyCastle.X509.X509CertificateParser().ReadCertificate((Stream)fileStream);
                    this.publicKey = (RsaKeyParameters)x509Certificate.GetPublicKey();
                    this.certExpiryDate = x509Certificate.NotAfter;
                    fileStream.Close();
                }
            }
            catch (Exception innerException)
            {
                throw new Exception("Could not intialize encryption module", innerException);
            }
            finally
            {
                if (fileStream != null) { fileStream.Close(); }
            }
        }

        public ENCRYPTER(byte[] publicKeyBytes)
        {
            try
            {
                Org.BouncyCastle.X509.X509Certificate x509Certificate = new Org.BouncyCastle.X509.X509CertificateParser().ReadCertificate(publicKeyBytes);
                this.publicKey = (RsaKeyParameters)x509Certificate.GetPublicKey();
                this.certExpiryDate = x509Certificate.NotAfter;
            }
            catch (Exception innerException)
            {
                throw new Exception("Could not intialize encryption module", innerException);
            }
        }

        public byte[] encryptUsingPublicKey(byte[] data)
        {
            IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
            cipher.Init(true, this.publicKey);
            return cipher.DoFinal(data);
        }

        public byte[] encryptUsingSessionKey(byte[] skey, byte[] data)
        {
            PaddedBufferedBlockCipher paddedBufferedBlockCipher = new PaddedBufferedBlockCipher(new AesEngine(), new Pkcs7Padding());
            paddedBufferedBlockCipher.Init(true, new KeyParameter(skey));
            byte[] array = new byte[paddedBufferedBlockCipher.GetOutputSize(data.Length)];
            int num = paddedBufferedBlockCipher.ProcessBytes(data, 0, data.Length, array, 0);
            int num2 = paddedBufferedBlockCipher.DoFinal(array, num);
            byte[] array2 = new byte[num + num2];
            Array.Copy(array, 0, array2, 0, array2.Length);
            return array2;
        }

        // AES/GCM ENCRYPTION
        public byte[] encryptUsingSessionKeyNew(byte[] skey, byte[] data, string ts, bool isHmac)
        {
            byte[] tsBytes = null;
            try
            {
                tsBytes = Encoding.UTF8.GetBytes(ts);
                byte[] ivBytes = new byte[IV_SIZE_BITS / 8];
                byte[] aadBytes = new byte[AAD_SIZE_BITS / 8];
                Array.Copy(tsBytes, tsBytes.Length - 12, ivBytes, 0, ivBytes.Length);
                Array.Copy(tsBytes, tsBytes.Length - 16, aadBytes, 0, aadBytes.Length);

                var cipher = new GcmBlockCipher(new AesFastEngine());
                var parameters = new AeadParameters(new KeyParameter(skey), AUTH_TAG_SIZE_BITS, ivBytes, aadBytes);
                cipher.Init(true, parameters);
                byte[] result = new byte[cipher.GetOutputSize(data.Length)];
                int processLen = cipher.ProcessBytes(data, 0, data.Length, result, 0);
                cipher.DoFinal(result, processLen);
                if (isHmac) return result;
                // With Add for Session Key Encryption
                byte[] packedCipherData = new byte[result.Length + tsBytes.Length];
                Array.Copy(tsBytes, 0, packedCipherData, 0, tsBytes.Length);
                Array.Copy(result, 0, packedCipherData, tsBytes.Length, result.Length);
                return packedCipherData;
            }
            catch { throw; }
        }

        public byte[] generateSessionKey()
        {
            SecureRandom random = new SecureRandom();
            KeyGenerationParameters parameters = new KeyGenerationParameters(random, 256);
            CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator("AES");
            keyGenerator.Init(parameters);
            return keyGenerator.GenerateKey();
        }

        public byte[] generateSha256Hash(byte[] message)
        {
            IDigest digest = new Sha256Digest();
            digest.Reset();
            byte[] array = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(message, 0, message.Length);
            digest.DoFinal(array, 0);
            return array;
        }

        public string getCertificateIdentifier()
        {
            return this.certExpiryDate.ToString("yyyyMMdd");
        }
    }
}
