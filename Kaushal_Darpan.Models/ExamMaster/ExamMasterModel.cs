using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Examiners
{
    public class ExamMasterDataModel
    {
        public int? ExamMasterID { get; set; }   
        public int SessionYearID { get; set; }    
        public int SessionMonthID { get; set; }   
        public int ProgramTypeID { get; set; }  
        public int ExamTypeID { get; set; }       
        public int SemesterID { get; set; }      
        public int AdmissionCategoryID { get; set; } 
        public DateTime RTS { get; set; } 
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
    }

}
