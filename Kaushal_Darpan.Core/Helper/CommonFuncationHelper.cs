using Kaushal_Darpan.Models.SMSConfigurationSetting;
using Kaushal_Darpan.Models.StudentJanAadharDetail;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QRCoder;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Kaushal_Darpan.Core.Helper
{
    public static class CommonFuncationHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public static T ConvertDataTable<T>(DataTable dt)
        {
            var json = JsonConvert.SerializeObject(dt);
            if (typeof(T).Name != "List`1")
            {
                json = json.TrimStart('[').TrimEnd(']');

            }
            var obj = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Ignore,
            });
            return obj;
        }

        // for Get data from Excel Convert in int 
        public static T ConvertExcelData<T>(DataTable dt)
        {

            var json = JsonConvert.SerializeObject(dt);

            // Check if T is a List and remove the array brackets if not
            if (typeof(T).Name != "List`1")
            {
                json = json.TrimStart('[').TrimEnd(']');
            }

            // Deserialize with custom settings
            var obj = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> { new IntegerJsonConverter() } // Use custom converter
            });

            return obj;
        }


        public class IntegerJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(int) || objectType == typeof(int?);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                // If the value is a float or double, convert it to an integer
                if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
                {
                    return Convert.ToInt32(reader.Value);
                }

                return null;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value);
            }
        }

        public static PagigantionList GetPaginationData<T>(List<T> items, int? totalRecord, int pageNumber, int pageSize)
        {
            var _totalRecord = totalRecord ?? 0;
            var totalPagination = (int)Math.Ceiling(_totalRecord / (double)pageSize);
            var paginationData = new PagigantionList
            {
                PageRecord = items.Count,// per page result records count
                TotalRecord = _totalRecord,// total records count
                PageNumber = pageNumber,// page index
                PageSize = pageSize,// record to display
                TotalPagination = totalPagination,// total pages to display
                HasNext = pageNumber < totalPagination,// next option
                HasPrevious = pageNumber > 1,// prev. option
            };
            return paginationData;
        }
        public static string AvoidSQLInjection_Char(string str)
        {
            str = str?.Trim();
            str = str?.Replace("'", "");
            str = str?.Replace("==", "=");
            return str;
        }
        public static string MakeError(ErrorDescription errorDesc)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Error = {errorDesc.Message}");
            sb.AppendLine($"Page = {errorDesc.PageName}.{errorDesc.ActionName}");
            sb.AppendLine($"@SqlExecutableQuery = {errorDesc.SqlExecutableQuery}");
            return sb.ToString();
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    //sb.Append(hashBytes[i].ToString("X2"));
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string EmitraEncrypt(string Text)
        {
            string Key = "681D392DB4FD7B8C6562712DFEEF1";
            string initVector = "tu89geji340t89u2";
            int keysize = 256;

            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Encrypted = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(Encrypted);
        }
        public static string EmitraDecrypt(string EncryptedText)
        {
            string Key = "681D392DB4FD7B8C6562712DFEEF1";
            string initVector = "tu89geji340t89u2";
            int keysize = 256;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[DeEncryptedText.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        public static string WebRequestinJson(string url, string postData, string contenttype)
        {
            string ret = string.Empty;

            StreamWriter requestWriter;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "POST";
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.Timeout = 40000;

                webRequest.ContentType = contenttype;
                //POST the data.
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(postData);
                }
            }

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            ret = reader.ReadToEnd();


            return ret;
        }
        public static async Task<string> SendSMS(SMSConfigurationSettingModel SMSConfigurationSettingModel, string MobileNo, string Message, string TemplateID, string language = "ENG")
        {
            CommonFuncationHelper.WriteTextLog("SMS_IN");
            string RetrunValue = "";
            try
            {
                var Model = new UNOCSmsModel
                {
                    Language = language,
                    Message = Message,
                    MobileNo = new List<string> { MobileNo },
                    ServiceName = SMSConfigurationSettingModel.ServiceName,
                    UniqueID = SMSConfigurationSettingModel.UniqueID,
                    templateID = TemplateID
                };

                var response = string.Empty;
                WebRequest request = (HttpWebRequest)WebRequest.Create(SMSConfigurationSettingModel.SMSUrl + "?client_id=" + SMSConfigurationSettingModel.SmsClientID);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers.Add("username", SMSConfigurationSettingModel.SMSUserName);
                request.Headers.Add("password", SMSConfigurationSettingModel.SMSPassWord);
                var inputJsonSer = System.Text.Json.JsonSerializer.Serialize(Model);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(inputJsonSer);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                CommonFuncationHelper.WriteTextLog(inputJsonSer);
                CommonFuncationHelper.WriteTextLog(response);


                var outputJsonDser = System.Text.Json.JsonSerializer.Deserialize<SmsResponseModel>(response);
                outputJsonDser.TrustType = "Final Save";
                outputJsonDser.responseID = MobileNo;
                RetrunValue = outputJsonDser.responseMessage.ToString();
            }
            catch (WebException ex)
            {
                //SmsResponseModel res = new SmsResponseModel();
                //res.responseCode = 200;
                //res.responseID = "1234556789";
                RetrunValue = "Request send Successfully";
                //var outputJsonDser = res;
                //return outputJsonDser;
                throw ex;
            }

            return RetrunValue;

        }
        public static string MD5HASHING(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string AESEncrypt(string textToEncrypt, string encryptionKey)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            aes.IV = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] buffer = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }
        public static string AESDecrypt(string textToDecrypt, string encryptionKey)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            aes.IV = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
            ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] buffer = Convert.FromBase64String(textToDecrypt);
            return Encoding.UTF8.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));

        }
        public static string GenerateTransactionNumber()
        {
            // Generate a new GUID.
            Guid guid = Guid.NewGuid();

            // Convert the first 6 bytes of the GUID to a byte array.
            byte[] bytes = guid.ToByteArray();

            // Take the first 6 bytes and convert them to a hexadecimal string.
            string hexString = BitConverter.ToString(bytes, 0, 6).Replace("-", "");

            Random rnd = new Random();
            //Create a 12-character transaction number from the hexadecimal string.
            string transactionNumber = rnd.Next(10000, 99999) + hexString;
            return transactionNumber;
        }
        public static string EgrassEncrypt(string textToEncrypt, string FilePath)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = GetFileBytes(FilePath);
            byte[] keyBytes = new byte[0x10];
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
        public static string EgrassDecrypt(string textToDecrypt, string FilePath)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            textToDecrypt = textToDecrypt.Replace(" ", "+");
            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = GetFileBytes(FilePath);
            byte[] keyBytes = new byte[0x10];
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
        public static byte[] GetFileBytes(String filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;
                buffer = new byte[length];
                int count;
                int sum = 0;
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
        public static string SMS_GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = SMS_GenerateNewRandom();
            }
            return r;
        }
        public static void WriteTextLog(string log)
        {
            string fileName = $"{ConfigurationHelper.StaticFileRootPath}Log/log_{DateTime.Now.ToString("yyyyMMdd")}.txt";
            System.IO.File.AppendAllTextAsync(fileName, $"{Environment.NewLine}------{DateTime.Now}------ : - {Environment.NewLine}{log}{Environment.NewLine}");
        }
        public static void ResizeImage(Stream sourcePath, string targetPath, int width, int height)
        {
            try
            {
                var image = System.Drawing.Image.FromStream(sourcePath);
                // Dim width = CInt((image.Width))
                // Dim height = CInt((image.Height))
                var destRect = new Rectangle(0, 0, width, height);
                var destImage = new Bitmap(width, height);
                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                var thumbGraph = Graphics.FromImage(destImage);
                thumbGraph.CompositingMode = CompositingMode.SourceCopy;
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.PixelOffsetMode = PixelOffsetMode.HighQuality;
                var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                thumbGraph.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                // Dim ms As MemoryStream = New MemoryStream()
                destImage.Save(targetPath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static string Encrypt(string textToEncrypt, string EncryptionPassword = "DevITNOT")
        {
            var sb = new StringBuilder();
            var bytes = Encoding.Unicode.GetBytes(textToEncrypt);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"           
        }
        public static string Decrypt(string textToDecrypt, string EncryptionPassword = "DevITNOT")
        {
            var bytes = new byte[textToDecrypt.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(textToDecrypt.Substring(i * 2, 2), 16);
            }
            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"            
        }
        public static byte[] GenerateQrCode(string val)
        {
            string code = val;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCoder.BitmapByteQRCode qrCode = new QRCoder.BitmapByteQRCode(qrCodeData);
            byte[] bytes = qrCode.GetGraphic(20);
            return bytes;
        }


        #region Configure function
        public static string GetIpAddress()
        {
            try
            {
                var context = _httpContextAccessor?.HttpContext;
                if (context == null)
                    return string.Empty;

                // Your existing IP extraction logic
                var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (!string.IsNullOrEmpty(forwardedFor))
                {
                    var ip = forwardedFor.Split(',')[0].Trim();
                    if (IsValidIpAddress(ip))
                        return ip;
                }

                var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
                if (!string.IsNullOrEmpty(realIp) && IsValidIpAddress(realIp))
                    return realIp;

                var cfIp = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault();
                if (!string.IsNullOrEmpty(cfIp) && IsValidIpAddress(cfIp))
                    return cfIp;

                var remoteIp = context.Connection?.RemoteIpAddress?.ToString();
                if (!string.IsNullOrEmpty(remoteIp))
                {
                    if (remoteIp.StartsWith("::ffff:"))
                        remoteIp = remoteIp.Substring(7);

                    return remoteIp;
                }
            }
            catch { }

            return string.Empty;
        }

        private static bool IsValidIpAddress(string ip)
        {
            return System.Net.IPAddress.TryParse(ip, out _);
        }
        #endregion

        #region nested class
        public class FinancialYear
        {
            private int yearNumber;
            private int firstMonthInYear = 4;

            public static FinancialYear Current
            {
                get { return new FinancialYear(DateTime.Today); }
            }

            private FinancialYear(DateTime forDate)
            {
                if (forDate.Month < firstMonthInYear)
                {
                    yearNumber = forDate.Year + 1;
                }
                else
                {
                    yearNumber = forDate.Year;
                }
            }

            public override string ToString()
            {
                return yearNumber.ToString();
            }
        }
        #endregion



        #region "Janadhar Services"


        public static DataSet JanAdhSendOTP(string janId, string MemberID)
        {
            string JanAdhaarClientID = "b10d20ae-367c-4cab-8b14-49330d4a392d";
            DataSet objdsotp = new DataSet();
            try
            {
                string Scheme = "CEA";
                var request = (HttpWebRequest)WebRequest.Create("https://api.sewadwaar.rajasthan.gov.in/app/live/Janaadhaar/Prod/Service/Gen/Otp/For?client_id=" + JanAdhaarClientID);
                request.Method = "POST";
                string postData = "<root><genOtpFor><janaadharId>" + janId + "</janaadharId><mid>" + MemberID + "</mid><schemeId>" + Scheme + "</schemeId></genOtpFor></root>";
                byte[] byteArray = Encoding.ASCII.GetBytes(postData);
                request.ContentType = "text/xml;charset=utf-8";
                request.ContentLength = byteArray.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                int foundS1 = responseString.IndexOf("<root>");
                responseString = responseString.Substring(foundS1);

                objdsotp = ConvertXMLResponceToDataSet(responseString);

                StreamReader respStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                string receivedResponse = respStream.ReadToEnd();
                respStream.Close();
                response.Close();

            }
            catch (Exception ex) { }

            return objdsotp;
        }

        public static DataSet ConvertXMLResponceToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public static string GetAadharByVID(string _VID)
        {
            string _AadhaarNo = string.Empty;
            string subaua = "PDA8046000";//System.Configuration.ConfigurationManager.AppSettings["subauaRajkisan"].ToString();
            // string subaua = "PNSCL22866";
            try
            {
                if (!string.IsNullOrEmpty(_VID) && _VID.Length == 15)
                {
                    string ip = "";
                    string url = "https://aadhaarauthapi.rajasthan.gov.in/doit-aadhaar-enc-dec/demo/hsm/auth/detokenizeV2";
                    string ModifiedData = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><AuthRequest UUID=\"" + _VID + "\" subaua=\"" + subaua + "\" flagType=\"" + "A" + "\" ver=\"2.5\"></AuthRequest>";
                    System.Net.WebRequest req = null;
                    WebResponse rsp = null;
                    try
                    {
                        req = WebRequest.Create(url);
                        req.Method = "POST";
                        req.ContentType = "application/xml";
                        req.Headers["appname"] = "Agriculture";
                        StreamWriter writer = new StreamWriter(req.GetRequestStream());
                        writer.Write(ModifiedData);
                        writer.Close();
                        rsp = req.GetResponse();
                        StreamReader sr = new StreamReader(rsp.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(results);
                        XmlNodeList xnList = xml.SelectNodes("/DSMTokanize");
                        string sa = "";
                        foreach (XmlNode xn in xnList)
                        {
                            sa = xn["status"].InnerText;
                            if (sa.ToUpper() == "Y")
                                _AadhaarNo = xn["AadhaarNo"].InnerText;
                            else
                                _AadhaarNo = "NO";
                        }
                        if (sa.ToUpper() == "N")
                        {
                            _AadhaarNo = "NO";
                        }
                    }
                    catch (Exception ex)
                    {
                        _AadhaarNo = "NO#" + ex.Message;
                    }
                }
                else
                {
                    _AadhaarNo = "NO" + "#" + "ReferenceId Not Found.";
                }
            }
            catch (Exception ex)
            {
                _AadhaarNo = "NO" + "#" + "ReferenceId Not Found##" + ex.Message;
            }
            return _AadhaarNo;
        }





        public static APIResponce ValidateAadharOTP(string txn, string adhar, string otp)
        {
            APIResponce apiResponce = new APIResponce();
            AadharResponce Responce = new AadharResponce();
            Responce.UserAddress = new AadharAddressResponce();
            Responce.UserInfo = new AadharUserInfoResponce();
            Responce.UserLocation = new AadharLocationResponce();
            try
            {
                string auacode = ConfigurationHelper.AadharAuthSUBAUS;
                string lickey = ConfigurationHelper.AadharAuthLicenseKey;
                string client_id = ConfigurationHelper.AadharAuthClientId;

                string Certificatename = ConfigurationHelper.Certificatename;

                string url = ConfigurationHelper.VerifyOTPForAadharAuthURL + "?client_id=" + client_id;//new


                //string CertificatePath = context.Server.MapPath(Certificatename);



                string CertificatePath = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.AadharCertificate, ConfigurationHelper.Certificatename);

                //Creating PID XML and Generate in UTF-8 Byte of that XML.
                var tts = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss");
                string msg = "<Pid ts=\"" + tts + "\" ver=\"2.0\" wadh=\"DEL9Vn2tNcxY/T3g9Jf6F/0Ne43ufdO6AaBClCWkCiQ=\"><Pv otp=\"" + otp + "\" /></Pid>";

                string expiryDate = string.Empty;
                string encSessionKey = string.Empty;
                string encryptedHmacBytes = string.Empty;
                string encXMLPIDDataNew = GetEncryptedPIDXmlNew(msg, CertificatePath, tts, ref expiryDate, ref encSessionKey, ref encryptedHmacBytes);

                //V.2.5
                string authXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><authrequest uid=\"" + adhar + "\" tid=\"\" subaua=\"" + auacode + "\" bt=\"OTP\" lk=\"" + lickey + "\" rc=\"Y\" ra=\"O\" lr=\"Y\" de=\"N\" pfr=\"N\" mec=\"Y\" ver=\"2.5\" txn=\"" + txn + "\" dpID=\"\" rdsID=\"\" rdsVer=\"\" dc=\"\" mi=\"\" mc=\"\" deviceSrNO=\"NA\" deviceError=\"NA\" ip=\"NA\" fdc=\"NA\" idc=\"NC\" macadd=\"NA\" lot=\"P\" lov=\"302005\" ><deviceInfo fType=\"NA\" iCount=\"NA\" pCount=\"NA\" errCodeRDS=\"NA\" errInfoRDS=\"NA\" fCount=\"NA\" nmPoints=\"NA\" qScore=\"NA\" srno=\"NA\" deviceError=\"NA\" /><Skey ci=\"" + expiryDate + "\">" + encSessionKey + "</Skey><Hmac>" + encryptedHmacBytes + "</Hmac><Data type=\"X\">" + encXMLPIDDataNew + "</Data></authrequest>";


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(authXML);
                request.ContentType = "Application/xml";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.Headers["appname"] = "CO-Operative";

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                HttpWebResponse response = null;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException webEx)
                {
                    // Handle the error response from the server
                    if (webEx.Response is HttpWebResponse errorResponse)
                    {
                        using (StreamReader reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string errorResponseText = reader.ReadToEnd();
                            // You can log the errorResponseText to investigate the issue further
                            apiResponce.Status = "Failed";
                            apiResponce.Message = "Error response from server: " + errorResponseText;
                        }
                    }
                    else
                    {
                        // If the exception is not related to a response, log the general exception
                        apiResponce.Status = "Failed";
                        apiResponce.Message = "WebRequest error: " + webEx.Message;
                    }
                    return apiResponce; // Return the response with the error message
                }

                // If we reach here, it means the response was successful (HTTP status 200)
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string results = new StreamReader(responseStream).ReadToEnd();

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(results);

                    XmlNodeList xnList = xml.SelectNodes("//authresponse/auth");
                    string sa = "";
                    foreach (XmlNode xn in xnList)
                    {
                        sa = xn.Attributes["status"].Value;
                    }

                    if (sa.ToUpper() == "Y")
                    {
                        apiResponce.Status = "Success";
                        // Parse and assign other data here
                        apiResponce.Data = Responce; // Assuming Responce is populated elsewhere
                    }
                    else
                    {
                        XmlNodeList xDataList = xml.SelectNodes("//authresponse/message");
                        apiResponce.Status = "Failed";
                        apiResponce.Message = xDataList[0].InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponce.Status = "Failed";
                apiResponce.Message = ex.ToString();
            }
            return apiResponce;
        }

        public static DataSet JanAdhGenerateOTPValidation(string tid, string otp)
        {
            string JanAdhaarClientID = "b10d20ae-367c-4cab-8b14-49330d4a392d";
            DataSet objdsotp = new DataSet();
            try
            {
                string Scheme = "CEA";
                var request = (HttpWebRequest)WebRequest.Create("https://api.sewadwaar.rajasthan.gov.in/app/live/Janaadhaar/Prod/Service/Validate/Otp?client_id=" + JanAdhaarClientID);
                request.Method = "POST";
                string postData = "<root><validateOtp><tid>" + tid + "</tid><otp>" + otp + "</otp><schemeId>" + Scheme + "</schemeId></validateOtp></root>";
                byte[] byteArray = Encoding.ASCII.GetBytes(postData);
                request.ContentType = "text/xml;charset=utf-8";
                request.ContentLength = byteArray.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                int foundS1 = responseString.IndexOf("<root>");
                responseString = responseString.Substring(foundS1);

                objdsotp = ConvertXMLResponceToDataSet(responseString);

                StreamReader respStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
                string receivedResponse = respStream.ReadToEnd();
                respStream.Close();
                response.Close();

            }
            catch (Exception ex) { }

            return objdsotp;
        }







        public static JanAadharDetailsEntity GetDetailFromJanAadhar(string JanAadharid, string enrId, string aadharid, string janmemidselected)
        {
            JanAadharDetailsEntity data = new JanAadharDetailsEntity();
            data.Status = "";
            JanAadharMemberDetails dt = new JanAadharMemberDetails();
            string scheme = string.Empty;
            string client_id = ConfigurationHelper.JanAadharAuthClientId;
            string Url = ConfigurationHelper.JaAdharMemberDetailsURL + "?client_id=" + client_id;

            scheme = "HEADM";

            string infoFlag = "PFE";
            string authoMode = "AOTP";
            string dateTime = string.Empty;
            try
            {
                dateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            }
            catch { dateTime = DateTime.Now.ToString(); }
            StringBuilder requestXml = new StringBuilder();
            requestXml.Append("<root>");
            if (JanAadharid.Length == 10)
            {
                requestXml.Append("<Info><janaadhaarId>" + JanAadharid + "</janaadhaarId>");
                requestXml.Append("<enrId></enrId>");
            }
            else
            {
                requestXml.Append("<Info><janaadhaarId></janaadhaarId>");
                requestXml.Append("<enrId>" + enrId + "</enrId>");

            }
            //requestXml.Append("<aadharId>" + aadharid + "</aadharId>");
            requestXml.Append("<aadharId></aadharId>");
            requestXml.Append("<scheme>" + scheme.TrimEnd() + "</scheme>");
            requestXml.Append("<infoFlg>" + infoFlag + "</infoFlg>");
            requestXml.Append("<authMode>" + authoMode + "</authMode>");
            requestXml.Append("<dateTime>" + dateTime + "</dateTime>");
            requestXml.Append("</Info>");
            requestXml.Append("</root>");



            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(requestXml.ToString());
            string pathStringRequest = "~/JanaadhaarnLog/Request";
            string pathStringResponse = "~/JanaadhaarnLog/Response";
            //new csCommon().CreateTextFile(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(pathStringRequest), aadharid) + ".txt", pathStringRequest, requestXml.ToString());
            var req = (HttpWebRequest)WebRequest.Create(Url);



            req.ContentType = "application/xml";
            req.Method = "POST";
            req.ContentLength = bytes.Length;



            using (Stream os = req.GetRequestStream())
            {
                os.Write(bytes, 0, bytes.Length);
            }



            string response = "";



            using (WebResponse resp = req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    response = sr.ReadToEnd().Trim();
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(response);
                    //new csCommon().CreateTextFile(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(pathStringResponse), aadharid) + ".txt", pathStringResponse, response);
                    XmlElement Dtlist1 = xml.DocumentElement;
                    XmlElement Dtlist2 = xml.DocumentElement;
                    List<string[]> listx = new List<string[]>();
                    //XmlNodeList xDataList = xml.SelectNodes("//personalInfo/member");



                    XmlNodeList nodeList = Dtlist1.SelectNodes("//personalInfo/member");
                    string enrid = "", janaadhaarId = "", nameEng = "", nameHnd = "", fnameEng = "", fnameHnd = "", dob = "", aadhar = "", mobile = "", email = "", acc = "", bankName = "", ifsc = "", gender = "", bankBranch = "";
                    string addressEng = "", addressHnd = "", pin = "", caste = "", category = "", districtName = "", block_city = "", gp = "", village = "", age = "", snameHnd = "", snameEng = "", maritalStatus = "", relationTyp = "";
                    string micr = "", voterId = "", mnameEng = "", mnameHnd = "", income = "", occupation = "", qualification = "", panNo = "", passport = "", dlNo = "", eid = "", janmemid = "";


                    if (nodeList.Count > 0)
                    {
                        foreach (XmlNode node in nodeList)
                        {
                            janmemid = node.SelectSingleNode("jan_mid").InnerXml;
                            if (janmemid == "0")
                            {
                                janmemid = node.SelectSingleNode("hof_jan_m_id").InnerXml;
                            }


                            if (janmemidselected == janmemid)
                            {
                                data.Status = "OK";
                                dt.janmemid = janmemid;
                                dt.enrid = node.SelectSingleNode("enrId").InnerXml;
                                dt.janaadhaarId = node.SelectSingleNode("janaadhaarId").InnerXml;
                                dt.aadhar = node.SelectSingleNode("aadhar").InnerXml;
                                dt.nameEng = node.SelectSingleNode("nameEng").InnerXml;
                                dt.nameHnd = node.SelectSingleNode("nameHnd").InnerXml;
                                dt.fnameEng = node.SelectSingleNode("fnameEng").InnerXml;
                                dt.fnameHnd = node.SelectSingleNode("fnameHnd").InnerXml;
                                dt.dob = node.SelectSingleNode("dob").InnerXml;
                                dt.gender = node.SelectSingleNode("gender").InnerXml;
                                dt.mobile = node.SelectSingleNode("mobile").InnerXml;
                                dt.email = node.SelectSingleNode("email").InnerXml;



                                dt.acc = node.SelectSingleNode("acc").InnerXml;
                                dt.bankName = node.SelectSingleNode("bankName").InnerXml;
                                dt.ifsc = node.SelectSingleNode("ifsc").InnerXml;
                                dt.age = node.SelectSingleNode("age").InnerXml;
                                dt.bankBranch = node.SelectSingleNode("bankBranch").InnerXml;
                                dt.snameEng = node.SelectSingleNode("snameEng").InnerXml;
                                dt.snameHnd = node.SelectSingleNode("snameHnd").InnerXml;
                                dt.maritalStatus = node.SelectSingleNode("maritalStatus").InnerXml;
                                dt.relationTyp = node.SelectSingleNode("relationTyp").InnerXml;



                                dt.mnameEng = node.SelectSingleNode("mnameEng").InnerXml;
                                dt.mnameHnd = node.SelectSingleNode("mnameHnd").InnerXml;
                                dt.voterId = node.SelectSingleNode("voterId").InnerXml;
                                dt.micr = node.SelectSingleNode("micr").InnerXml;
                                dt.income = node.SelectSingleNode("income").InnerXml;
                                dt.occupation = node.SelectSingleNode("occupation").InnerXml;
                                dt.qualification = node.SelectSingleNode("qualification").InnerXml;
                                dt.panNo = node.SelectSingleNode("panNo").InnerXml;
                                dt.passport = node.SelectSingleNode("passport").InnerXml;
                                dt.dlNo = node.SelectSingleNode("dlNo").InnerXml;
                                dt.eid = node.SelectSingleNode("eid").InnerXml;
                                //dt.passportphotoBase64 = JanAadhaarMemberPhoto(JanAadharid, janmemid);
                            }
                        }
                        XmlNodeList nodeList1 = Dtlist2.SelectNodes("//family/familydetail");
                        dt.Address = new JanAadharMemberAddressDetails();
                        foreach (XmlNode node1 in nodeList1)
                        {



                            dt.Address.addressEng = node1.SelectSingleNode("addressEng").InnerXml;
                            dt.Address.addressHnd = node1.SelectSingleNode("addressHnd").InnerXml;
                            dt.Address.pin = node1.SelectSingleNode("pin").InnerXml;
                            dt.caste = node1.SelectSingleNode("caste").InnerXml;
                            dt.category = node1.SelectSingleNode("category").InnerXml;
                            dt.Address.districtName = node1.SelectSingleNode("districtName").InnerXml;
                            dt.Address.block_city = node1.SelectSingleNode("block_city").InnerXml;
                            dt.Address.gp = node1.SelectSingleNode("gp").InnerXml;
                            dt.Address.village = node1.SelectSingleNode("village").InnerXml;
                        }
                    }
                    else
                    {
                        data.Status = "";
                        data.Message = "Member details not found";
                    }
                }

            }
            if (data.Status == "")
            {
                data.Message = "Member details not found";
            }
            data.UserDetails = new JanAadharMemberDetails();
            data.UserDetails = dt;
            return data;
        }



        //public static class GlobleKeys
        //{


        //}

        public static string GetEncryptedPIDXmlNew(string pidXml, string certPath, string ts, ref string certExpiryDate, ref string encSessionKey, ref string encHmac)
        {
            ENCRYPTER enc = new ENCRYPTER(certPath);
            byte[] pidXmlBytes = Encoding.UTF8.GetBytes(pidXml);
            byte[] session_key = enc.generateSessionKey();
            byte[] encrypted_skey = enc.encryptUsingPublicKey(session_key);
            byte[] encPidXmlBytes = enc.encryptUsingSessionKeyNew(session_key, pidXmlBytes, ts, false);
            byte[] hmac = enc.generateSha256Hash(pidXmlBytes);
            byte[] encHmacBytes = enc.encryptUsingSessionKeyNew(session_key, hmac, ts, true);

            encSessionKey = Convert.ToBase64String(encrypted_skey);
            encHmac = Convert.ToBase64String(encHmacBytes);
            certExpiryDate = enc.getCertificateIdentifier();
            return Convert.ToBase64String(encPidXmlBytes);
        }
        #endregion



        public static string EncodeUTF8ToBase64(string text)
        {
            // Convert the string to UTF-8 bytes
            byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(text);
            // Convert the bytes to a Base64 string
            return Convert.ToBase64String(encodedBytes);
        }

        // Decode Base64 string back to original string
        public static string DecodeBase64ToString(string base64EncodedText)
        {
            // Convert Base64 string back to byte array
            byte[] decodedBytes = Convert.FromBase64String(base64EncodedText);
            // Convert the byte array back to the original UTF-8 string
            return System.Text.Encoding.UTF8.GetString(decodedBytes);
        }


        // Class properties (you can adjust the access modifiers as needed)

        public static string yearlyEng = "https://dteapp.rajasthan.gov.in/bter_engexam/files/documents/";
        public static string yearlyNonEng = "https://dteapp.hte.rajasthan.gov.in/bter_exam/files/documents/";
        public static string semEng = "https://dteapp.hte.rajasthan.gov.in/bter_eng_sem_exam/files/documents/";
        public static string semNonEng = "https://dteapp.hte.rajasthan.gov.in/bter_sem_exam/files/documents/";



        public static string GetStudentFilesForOldBter(int Eng_NonEng, bool IsYearly, int StudentID)
        {
            string OldFilePath = string.Empty;
            if (IsYearly)
            {
                if (Eng_NonEng == 1)
                {
                    OldFilePath = $"{yearlyEng}{StudentID}";
                }
                else if (Eng_NonEng == 2)
                {
                    OldFilePath = $"{yearlyNonEng}{StudentID}";
                }
            }
            else // Semester
            {
                if (Eng_NonEng == 1)
                {
                    OldFilePath = $"{semEng}{StudentID}";
                }
                else if (Eng_NonEng == 2)
                {
                    OldFilePath = $"{semNonEng}{StudentID}";
                }
            }
            return OldFilePath;
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[]
                {
            "Zero", "One", "Two", "Three", "Four", "Five",
            "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
            "Seventeen", "Eighteen", "Nineteen"
        };

                var tensMap = new[]
                {
            "Zero", "Ten", "Twenty", "Thirty", "Forty",
            "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }

        public static async Task DownloadAndSaveFileInFolderAsync(string fileUrl, string savePath)
        {
            using (HttpClient client = new HttpClient())
            {
                // Get the file as a byte array
                byte[] fileBytes = await client.GetByteArrayAsync(fileUrl);

                // Write the byte array to the specified file path
                await File.WriteAllBytesAsync(savePath, fileBytes);
            }
        }

        public static async Task<string> SendSMS(SMSConfigurationSettingModel SMSConfigurationSettingModel, List<string> MobileNos, string Message, string TemplateID, string language = "ENG")
        {
            CommonFuncationHelper.WriteTextLog("SMS_IN");
            string RetrunValue = "";
            try
            {
                var Model = new UNOCSmsModel
                {
                    Language = language,
                    Message = Message,
                    MobileNo = MobileNos,
                    ServiceName = SMSConfigurationSettingModel.ServiceName,
                    UniqueID = SMSConfigurationSettingModel.UniqueID,
                    templateID = TemplateID
                };

                var response = string.Empty;
                WebRequest request = (HttpWebRequest)WebRequest.Create(SMSConfigurationSettingModel.SMSUrl + "?client_id=" + SMSConfigurationSettingModel.SmsClientID);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.Headers.Add("username", SMSConfigurationSettingModel.SMSUserName);
                request.Headers.Add("password", SMSConfigurationSettingModel.SMSPassWord);
                var inputJsonSer = System.Text.Json.JsonSerializer.Serialize(Model);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(inputJsonSer);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                CommonFuncationHelper.WriteTextLog(inputJsonSer);
                CommonFuncationHelper.WriteTextLog(response);

                var outputJsonDser = System.Text.Json.JsonSerializer.Deserialize<SmsResponseModel>(response);
                outputJsonDser.TrustType = "Final Save";
                RetrunValue = outputJsonDser.responseMessage.ToString();
            }
            catch (WebException ex)
            {
                throw ex;
            }

            return RetrunValue;

        }
    }
}
