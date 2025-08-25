using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ApplicationStatus
{
    public class EmitraApplicationstatusModel
    {
        public int ApplicationID { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? Email { get; set; }
        public int StatusID { get; set; }
        public string? Status { get; set; }
        public int DepartmentID {  get; set; }
        public string? Department {  get; set; }
        public string? CreateDate {  get; set; }
        public int IsFinalSubmit { get; set; }
        public bool IsPaymentSuccess { get; set; }
    }
}
