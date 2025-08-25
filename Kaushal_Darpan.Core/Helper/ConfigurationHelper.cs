using Microsoft.Extensions.Configuration;

namespace Kaushal_Darpan.Core.Helper
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;
        private static string _rootPath;
        public static void Configure(IConfiguration configuration, string rootPath)
        {
            _configuration = configuration;
            _rootPath = rootPath;
        }

        public static string ConnectionString => _configuration.GetConnectionString("DbConnection");
        public static string JwtIssuer => _configuration["Jwt:Issuer"];
        public static string JwtAudience => _configuration["Jwt:Audience"];
        public static string JwtSecret => _configuration["Jwt:Secret"];
        public static int SessionTime => int.Parse(_configuration["SessionTime"] ?? "0");
        public static string JwtAuthority => _configuration["Jwt:Authority"];
        public static string EgrassPaymentSuccessUrl => _configuration["Egrass-Payment-SuccessUrl"];
        public static string RootPath => $"{_rootPath}/";
        public static string StaticFileRootPath => $"{_rootPath}/StaticFiles/";


        public static string SSOBaseUrl => _configuration["SSOLanding:SSOBaseUrl"];
        public static string AppName => _configuration["AppName"];
        public static string WebServiceUser => _configuration["SSOLanding:WebServiceUser"];
        public static string WebServicePwd => _configuration["SSOLanding:WebServicePwd"];
        public static string ApplicationURL => _configuration["SSOLanding:ApplicationURL"];
        public static string SSOTokenDetailUrl => _configuration["SSOLanding:SSOTokenDetailUrl"];
        public static string SSOProfileDetailUrl => _configuration["SSOLanding:SSOProfileDetailUrl"];
        public static string SSOAutenticationUrl => _configuration["SSOLanding:SSOAutenticationUrl"];

        public static string SSOLogoutUrl => _configuration["SSOLanding:SSOLogoutUrl"];
        public static string SSOBackToUrl => _configuration["SSOLanding:SSOBackToUrl"];
        public static string SSOIncreaseSessionTimeUrl => _configuration["SSOLanding:SSOIncreaseSessionTimeUrl"];

        public static string BaseURL => _configuration["BaseURL"] ?? "";
        public static string SMTPHost => _configuration["SMTP:SMTPHost"] ?? "";
        public static int SMTPPort => Convert.ToInt32(_configuration["SMTP:SMTPPort"] ?? "0");
        public static string SMTPEmail => _configuration["SMTP:SMTPEmail"] ?? "";
        public static string SMTPUsername => _configuration["SMTP:SMTPUsername"] ?? "";
        public static string SMTPPassword => _configuration["SMTP:SMTPPassword"] ?? "";
        public static bool IsLiveServer => Convert.ToBoolean(_configuration["SMTP:IsLiveServer"] ?? "true");
        public static bool EnableSsl => Convert.ToBoolean(_configuration["SMTP:EnableSsl"] ?? "false");
        public static bool UseDefaultCredentials => Convert.ToBoolean(_configuration["SMTP:UseDefaultCredentials"] ?? "false");
        public static bool IsLocal => Convert.ToBoolean(_configuration["IsLocal"] ?? "false");


        public static readonly string AadharAuthLicenseKey = "MDAW2xSJCuC-htWkTHcw9LTYXs1rl8kyfygbLztYldZocGJR6DDtE5A";//"MKmyGwbThaYG35Ahinwx35nLtBYXrNMP4ejWD7A9-x6InP7y4xLROXU";

        #region Jan Aadhar Configuration
        public static readonly string AadharAuthClientId = "0c8fe990-3b61-406d-b185-0fafc80e73d7";
        public static readonly string AadharAuthSUBAUS = "PRCIS22869"; //"PNSCL22866";
        public static readonly string AadhaarDSMDETOKANIZEV2URL = "https://api.sewadwaar.rajasthan.gov.in/app/live/Aadhaar/Prod/detokenizeV2/doitAadhaar/encDec/demo/hsm/auth";
        public static readonly string SendOTPForAadharAuthURL = "https://api.sewadwaar.rajasthan.gov.in/app/live/was/otp/request/prod";
        public static readonly string VerifyOTPForAadharAuthURL = "https://api.sewadwaar.rajasthan.gov.in/app/live/was/kyc/otp/prod";
        public static readonly string AadharAuthEsignClientId = "02a79073-c2db-4c84-b810-885fdaa8c54a";
        public static readonly string SendOTPForAadharAuthEsignURL = "https://api.sewadwaar.rajasthan.gov.in/app/live/RajeSign/Prod/all/webresources/generic/sendOTP/Aadhar";
        public static readonly string VerifyOTPForAadharAuthEsignURL = "https://api.sewadwaar.rajasthan.gov.in/app/live/RajeSign/Prod/all/webresources/generic/authOTP/Aadhar";
        public static readonly string EsignPDFURL = "https://api.sewadwaar.rajasthan.gov.in/app/live/rajesign/Prod/Service/all/webresources/generic/esign/Doc";
        public static readonly string Certificatename = "uidai_auth_prod.cer";
        public static readonly string JaAdharMemberListURL = "https://api.sewadwaar.rajasthan.gov.in/app/live/Janaadhaar/Prod/Service/action/fetchJayFamily";
        public static readonly string JaAdharMemberDetailsURL = "https://api.sewadwaar.rajasthan.gov.in/app/live/Janaadhaar/Prod/Service/Info/Fetch";
        public static readonly string JanAadharAuthClientId = "f6de7747-60d3-4cf0-a0ae-71488abd6e95";
        public static readonly string JanAadhaarMemberPhotoURL = "f6de7747-60d3-4cf0-a0ae-71488abd6e95";
        #endregion                                                                                                             //public static readonly string AadharAuthLicenseKey = "MJSazxO49Eh5vQ2BlcbUG--uNQ4tCpqKPF-OoFa0BZo0CE4CDBBtXVA";//"MKmyGwbThaYG35Ahinwx35nLtBYXrNMP4ejWD7A9-x6InP7y4xLROXU";

    }
}
