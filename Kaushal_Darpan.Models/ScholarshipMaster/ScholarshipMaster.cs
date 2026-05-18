using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ScholarshipMaster
{
    public class ScholarshipMaster:RequestBaseModel
    {
        public int ScholarshipID {  get; set; }
        public int StreamID {  get; set; }
        public int InstituteID   {  get; set; }
        public int SemesterID {  get; set; }
        public int Category {  get; set; }
        public int Amount {  get; set; }
        public int ModifyBy {  get; set; }
        public int TotalStudent {  get; set; }
        public string Document { get; set; }
        public string Dis_DocName { get; set; }
    }
    public class ScholarshipSearchModel:RequestBaseModel
    {
        public int StreamID { get; set; }
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
    }

    public class ScholarshipOnboardModel
    {
        public string NODALOFFICERNAME { get; set; }
        public string NODALOFFICEREMAIL { get; set; }
        public string NODALOFFICERMOBILE { get; set; }
        public string NODALOFFICERAADHAAR { get; set; }
        public string NODALOFFICERAADHAAR_REFNO { get; set; }

        public string DESIGNATION1 { get; set; }
        public string NAME1 { get; set; }
        public string EMAILADDRESS1 { get; set; }
        public string MOBILENUMBER1 { get; set; }

        public string DESIGNATION2 { get; set; }
        public string NAME2 { get; set; }
        public string EMAILADDRESS2 { get; set; }
        public string MOBILENUMBER2 { get; set; }
        public string SSOID { get; set; }
        public string? InstCode { get; set; }
    }
}
