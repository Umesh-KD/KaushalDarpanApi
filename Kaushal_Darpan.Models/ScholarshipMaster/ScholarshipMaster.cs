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

}
