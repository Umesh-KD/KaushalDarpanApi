using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTE_AssignApplication
{
    public class AssignApplicaitonDataModel
    {
        public int ID { get; set; }
        public string? FromApplicationNo { get; set; }
        public string? ToApplicationNo { get; set; }
        public int VerifierID { get; set; }
        public int Applied { get; set; }
        public int EndTermID { get; set; }

        public string Remark { get; set; }
        public string VerifierName { get; set; }
        public string ApprovedCount { get; set; }
        public string RejectedCount { get; set; }
        public string PendingCount { get; set; }

        public string NotifiedCount { get; set; }
        public string ChangedCount { get; set; }
        public string DeficiencyUploaded { get; set; }

        public string DeficiencyMarkedCount { get; set; }


        public string Unchecked { get; set; }
        public string Checked { get; set; }




        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy {  get; set; }
        public int CreatedBy { get; set; }
        public int DepartmentID { get; set; }
        public int CourseType { get; set; }
        public bool ShowAllApplication { get; set; }
    }

    public class AssignedApplicationStudentDataModel
    {
        public string Name { get; set; }
        public string ApplicationID { get; set; }
        public string Gender_Name { get; set; }
        public string CategoryA_Name {  get; set; }
        public string CategoryB_Name { get; set; }
        public string CategoryC_Name { get; set; } 
        public string CategoryD_Name { get; set; }

        public int Gender {  get; set; }
        public int CategoryA { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
        public int CategoryD { get; set; }
        public int Status { get; set; }
        public string? StatusName { get; set; }
        public string? FatherName { get; set; }
        public string? MobileNo { get; set; }
        public string? VerifierID { get; set; }
        public string? Verifier { get; set; }
        public string? ApplicationNo { get; set; }
    }

    public class AssignCheckerModel
    {
        public int ApplicationID { get; set; }
        public int VerifierID { get; set; } 
        public int CheckerID { get; set; } 
        public int  ModifyBy { get; set; }
    }

    public class RevertApplicationDataModel: RequestBaseModel
    {
      public int? ApplicationID { get; set; }
      public int? UserID { get; set; }
    }
}
