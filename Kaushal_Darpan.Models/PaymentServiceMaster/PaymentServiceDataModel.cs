using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PaymentServiceMaster
{
    public class PaymentServiceDataModel
    {
        public int ID {  get; set; }
        public int SchemeId { get; set; }
        public string? ServiceName { get; set; }
        public int ServiceId { get; set; }
        public int SubServiceId { get; set; }
        public string? MerchantCode { get; set; }
        public string? RevenueHead { get; set; }
        public int CommType { get; set; }
        public string? OfficeCode { get; set; }
        public string? ServiceURL { get; set; }
        public string? EncryptionKey { get; set; }
        public string? VerifyURL { get; set; }
        public string? Flag { get; set; }
        public int IsActive { get; set; }
        public int UserID { get; set; }
        public string? ViewName { get; set; }
        public string? ControllerName { get; set; }
        public string? JanaadhaarSchemeCode { get; set; }
        public int IsLive { get; set; }
        public bool IsKiosk { get; set; }
        public string? CHECKSUMKEY { get; set; }
        public string? REDIRECTURL { get; set; }
        public string? WebServiceURL { get; set; }
        public string? SuccessFailedURL { get; set; }
        public string? SuccessURL { get; set; }
        public int ExamStudentStatus { get; set; }
        public int CourseType { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
    }
}
