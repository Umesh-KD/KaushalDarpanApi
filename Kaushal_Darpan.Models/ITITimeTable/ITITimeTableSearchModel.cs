using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITITimeTable
{
    public class ITITimeTableSearchModel
    {
        public int DepartmentID { get; set; }
        public int SemesterID { get; set; }
        public int InstituteID { get; set; }
        public int EndTermID { get; set; }
        public int ShiftID { get; set; }
        public int? FinancialYearID { get; set; }
        public int? Eng_NonEng {  get; set; }
        public string? Action {  get; set; }
        public int? Status { get; set; }
    }


    
}
