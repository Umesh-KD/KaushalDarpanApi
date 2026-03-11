//using EEMS.Core.Helper;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Kaushal_Darpan.Core.Helper;
//using Kaushal_Darpan.Core;
//using Kaushal_Darpan.Core.NewJanAdharCrypto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Kaushal_Darpan.Infra
{
    //public class JanAadhaarGenericService
    //{
    //	private readonly HttpClient _httpClient;
    //	private readonly CryptoHelper _crypto;

    //	public JanAadhaarGenericService()
    //	{
    //		// Load certificates from config
    //		var privateCert = new X509Certificate2(JanAadhaarConfig.PrivateCertPath, JanAadhaarConfig.PrivateCertPassword);
    //		var publicCert = CryptoHelper.LoadCertificate(JanAadhaarConfig.PublicCertPath);

    //		_crypto = new CryptoHelper(publicCert.GetRSAPublicKey(), privateCert.GetRSAPrivateKey());
    //		_httpClient = new HttpClient();
    //	}

    //       //public async Task<string> CallApiAsync(string endpoint, object dataPayload)
    //       //{
    //       //	try
    //       //	{
    //       //		var payload = new { data = dataPayload };
    //       //		string json = JsonConvert.SerializeObject(payload);
    //       //		string signature = _crypto.SignData(json);

    //       //		var finalPayload = new
    //       //		{
    //       //			data = dataPayload,
    //       //			signature = signature
    //       //		};

    //       //		string encryptedData = _crypto.EncryptDataWithAES(JsonConvert.SerializeObject(finalPayload));

    //       //		var request = new { data = encryptedData };
    //       //		string requestBody = JsonConvert.SerializeObject(request);

    //       //		string fingerprint = CryptoHelper.GetSha256Fingerprint(JanAadhaarConfig.PublicCertPath);
    //       //		_httpClient.DefaultRequestHeaders.Clear();
    //       //		//_httpClient.DefaultRequestHeaders.Add("X-Cert-Fingerprint", fingerprint);

    //       //		string apiUrl = $"{JanAadhaarConfig.BaseUrl}{endpoint}?client_id={JanAadhaarConfig.ClientId}";

    //       //		var response = await _httpClient.PostAsync(apiUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
    //       //		string encryptedResponse = await response.Content.ReadAsStringAsync();

    //       //		string encryptedRespData = JObject.Parse(encryptedResponse)["data"].ToString();
    //       //		string decryptedResponse = CryptoHelper.DecryptDataWithAES(encryptedRespData);

    //       //		return decryptedResponse;
    //       //	}
    //       //	catch (Exception ex)
    //       //	{
    //       //		return JsonConvert.SerializeObject(new
    //       //		{
    //       //			status = false,
    //       //			error = ex.Message,
    //       //			stack = ex.StackTrace
    //       //		});
    //       //	}
    //       //}
    //       public async Task<string> CallApiAsync(string endpoint, object dataPayload)
    //       {
    //           if (dataPayload == null)
    //               throw new ArgumentNullException(nameof(dataPayload));

    //           try
    //           {
    //               var payload = new { data = dataPayload };
    //               string json = JsonConvert.SerializeObject(payload);

    //               string signature = _crypto.SignData(json); // Must not be null
    //               var finalPayload = new
    //               {
    //                   data = dataPayload,
    //                   signature = signature
    //               };

    //               string encryptedData = _crypto.EncryptDataWithAES(JsonConvert.SerializeObject(finalPayload));
    //               var request = new { data = encryptedData };
    //               string requestBody = JsonConvert.SerializeObject(request);

    //               string fingerprint = CryptoHelper.GetSha256Fingerprint(JanAadhaarConfig.PublicCertPath);

    //               _httpClient.DefaultRequestHeaders.Clear();
    //               _httpClient.DefaultRequestHeaders.Add("X-Cert-Fingerprint", fingerprint);

    //               string apiUrl = $"{JanAadhaarConfig.BaseUrl}{endpoint}?client_id={JanAadhaarConfig.ClientId}";

    //               var response = await _httpClient.PostAsync(apiUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
    //               string encryptedResponse = await response.Content.ReadAsStringAsync();

    //               string encryptedRespData = JObject.Parse(encryptedResponse)["data"]?.ToString();
    //               if (string.IsNullOrEmpty(encryptedRespData))
    //                   throw new Exception("API returned no 'data' field.");

    //               string decryptedResponse = CryptoHelper.DecryptDataWithAES(encryptedRespData);

    //               return decryptedResponse;
    //           }
    //           catch (Exception ex)
    //           {
    //               return JsonConvert.SerializeObject(new
    //               {
    //                   status = false,
    //                   error = ex.Message,
    //                   stack = ex.StackTrace
    //               });
    //           }
    //       }
    //   }
    public class JanAadhaarGenericService
    {
        private readonly HttpClient _httpClient;
        private readonly CryptoHelperNew _crypto;
        public JanAadhaarGenericService()
        {
            try
            {
                var privatePath = ConfigurationHelper.PrivateCertPath;
                var publicPath = ConfigurationHelper.PublicCertPath;

                if (!File.Exists(privatePath))
                    throw new FileNotFoundException($"Private certificate not found at {privatePath}");
                if (!File.Exists(publicPath))
                    throw new FileNotFoundException($"Public certificate not found at {publicPath}");

                //var privateCert = new X509Certificate2(privatePath, JanAadhaarConfig.PrivateCertPassword);
                //var privateCert = new X509Certificate2(privatePath, "EEMS@123");
                var privateCert = new X509Certificate2(
                        ConfigurationHelper.PrivateCertPath,
                        ConfigurationHelper.PrivateCertPassword,
                        X509KeyStorageFlags.MachineKeySet |
                        X509KeyStorageFlags.PersistKeySet |
                        X509KeyStorageFlags.Exportable
                    );

                var publicCert = CryptoHelperNew.LoadCertificate(publicPath);

                _crypto = new CryptoHelperNew(publicCert.GetRSAPublicKey(), privateCert.GetRSAPrivateKey());
                _httpClient = new HttpClient();

            }
            catch (Exception ex)
            {
                string logFile = Path.Combine(AppContext.BaseDirectory, "cert_load_error.log");
                File.AppendAllText(logFile, $"[{DateTime.Now}] Certificate load error: {ex}\n");
                throw;
            }
        }
        public async Task<string> CallApiAsync(string endpoint, object dataPayload)
        {
            if (dataPayload == null)
                throw new ArgumentNullException(nameof(dataPayload));
            try
            {
                var payload = new { data = dataPayload };
                string jsonPayload = JsonConvert.SerializeObject(payload);
                string signature = _crypto.SignData(jsonPayload);
                if (string.IsNullOrEmpty(signature))
                    throw new InvalidOperationException("Signature could not be generated.");
                var finalPayload = new
                {
                    data = dataPayload,
                    signature = signature
                };
                string encryptedData = _crypto.EncryptDataWithAES(JsonConvert.SerializeObject(finalPayload));

                var request = new { data = encryptedData };
                string requestBody = JsonConvert.SerializeObject(request);
                string fingerprint = CryptoHelperNew.GetSha256Fingerprint(ConfigurationHelper.PublicCertPath);
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("X-Cert-Fingerprint", fingerprint);

                string clientId = ConfigurationHelper.IsLocal ? ConfigurationHelper.ClientId : ConfigurationHelper.ClientId_live;

                string baseUrl = ConfigurationHelper.IsLocal? 
                    "https://apitest.sewadwaar.rajasthan.gov.in/app/live/apiservice/janAadhaar/v1/": 
                    "https://api.sewadwaar.rajasthan.gov.in/app/live/apiservice/janAadhaar/v1/";


                string apiUrl = string.Empty;
                if (endpoint == "member-list")
                {
                    apiUrl = $"{baseUrl}member-list?client_id={clientId}";

                }
                else if (endpoint == "generate-otp")
                {
                    apiUrl = $"{baseUrl}generate-otp?client_id={clientId}";
                }
                else if (endpoint == "validate-otp")
                {
                    if (ConfigurationHelper.IsLocal)
                    {
                        baseUrl = "https://apitest.sewadwaar.rajasthan.gov.in/app/live/apiservice/janAadhaar/v1/";
                    }
                    else
                    {
                        baseUrl = "https://api.sewadwaar.rajasthan.gov.in/app/live/janAadhaar/v1/";
                    }
                    apiUrl = $"{baseUrl}validate-otp?client_id={clientId}";
                }
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"API Error {(int)response.StatusCode}: {errorBody}");
                }
                string encryptedResponse = await response.Content.ReadAsStringAsync();
                string encryptedRespData = JObject.Parse(encryptedResponse)["data"]?.ToString();
                if (string.IsNullOrEmpty(encryptedRespData))
                    throw new Exception("API returned no 'data' field.");
                string decryptedResponse = CryptoHelperNew.DecryptDataWithAES(encryptedRespData);
                return decryptedResponse;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = false,
                    message = "Error occurred while calling API.",
                    error = ex.Message,
                    stack = ex.StackTrace
                });

            }
        }
    }
}

