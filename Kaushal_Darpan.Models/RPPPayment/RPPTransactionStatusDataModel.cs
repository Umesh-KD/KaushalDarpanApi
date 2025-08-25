using System.Security.Cryptography.X509Certificates;

namespace Kaushal_Darpan.Api.Models
{
    public class RPPTransactionStatusDataModel
    {
        public string ApplyNocApplicationID { get; set; } = string.Empty;
        public string ApplicationID { get; set; } = string.Empty;
        public string TransactionID { get; set; } = string.Empty;

        public string AMOUNT { get; set; } = string.Empty;
        public string PRN { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
        public string? RPPTXNID { get; set; }
        public string? SubOrderID { get; set; }
        public string? REFUNDID { get; set; }
        public string SSOID { get; set; } = string.Empty;
        public string ServiceID { get; set; }= string.Empty;
        public int ID { get; set; }
        public int ExamStudentStatus { get; set; }




    }




}
