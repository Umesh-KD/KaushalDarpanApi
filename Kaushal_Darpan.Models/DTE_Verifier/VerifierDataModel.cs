using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTE_Verifier
{
    public class VerifierDataModel
    {
        public int VerifierID { get; set; }
        public string Name { get; set; }
        public string SSOID { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string MobileNumber { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy {  get; set; }
        public int CreatedBy { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int CourseType { get; set; }
        public bool ShowAllApplication { get; set; }
    }
    
    public class NodalVerifierDataModel
    {
        public int NodalUserId { get; set; }
        public int CenterID { get; set; }
        public string? Name { get; set; }
        public string? SSOID { get; set; }
        public string? Email { get; set; }
        public string? Remark { get; set; }
        public string? MobileNumber { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public int ModifyBy {  get; set; }
        public int CreatedBy { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int CourseType { get; set; }
        public bool? ShowAllApplication { get; set; }
    }

    public class VerifierApiDataModel
    {
        public int VerifierID { get; set; }
        public string Name { get; set; }
        public string SSOID { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string MobileNumber { get; set; }
        public string appName { get; set; }
        public string password { get; set; }

       

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int CourseType { get; set; }
        public bool ShowAllApplication { get; set; }
    }


    public class ResVerifierApiDataModel
    {
        //public int VerifierID { get; set; }
        public string displayName { get; set; }
        public string SSOID { get; set; }
        //public string Email { get; set; }
        //public string Remark { get; set; }
        //public string MobileNumber { get; set; }
        //public string appName { get; set; }
        //public string password { get; set; }



        //public bool ActiveStatus { get; set; }
        //public bool DeleteStatus { get; set; }
        //public int ModifyBy { get; set; }
        //public int CreatedBy { get; set; }
        public int departmentId  { get; set; }
        //public int RoleID { get; set; }
        //public int CourseType { get; set; }
        //public bool ShowAllApplication { get; set; }
    }
}
