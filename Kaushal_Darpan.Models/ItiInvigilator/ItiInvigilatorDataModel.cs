using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ItiInvigilator
{
    public  class ItiInvigilatorDataModel:RequestBaseModel
    {
        public int InvigilatorID { get; set; }
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int StaffID { get; set; }
        public int TimeTableID { get; set; }
        public string? RollNoFrom { get; set; }
        public string? RollNoTo { get; set; }
        public int SemesterID { get; set; }
        public int ShiftID { get; set; }
        public string SubjectName { get; set; }

    }


    public class ItiInvigilatorSearchModel:RequestBaseModel
    {
        public int TimeTableID { get; set; }
        public int InstituteID { get; set; }
        public int InvigilatorID { get; set; }
        public int UserID { get; set; }
      
        public string? Action { get; set; }
        public string? SSOID { get; set; }
    }


    public class ItiTheoryStudentMaster
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int MarkEnter { get; set; }
        public int InternalPracticalID { get; set; }
        public string? RollNo { get; set; }
        public string? SubjectName { get; set; }
        public int GroupCodeID { get; set; }
        public int SSOID { get; set; }
        public int InstituteID { get; set; }
        public int? EndtermID { get; set; }
        public int? EngNong { get; set; }
    }

    public class ITI_InvigilatorPDFViewModal
    {
        public int InstituteID { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int InvigilatorID { get; set; }
        public string SSOID { get; set; }
        public int RoleID { get; set; }
        public int Status { get; set; }
        public int Userid { get; set; }
        public string ITIInvigilatorIDs { get; set; }
    }


    public class ITI_InvigilatorPDFForwardModal
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }        
        public string? FileName { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public string  ITIInvigilatorID { get; set; }

       
    }

    public class ITI_AdminInvigilatorRemunerationDetailModal
    {
         public string SSOID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Userid { get; set; }
        public int RemunerationPKID { get; set; } = 0;
        public string Remarks { get; set; } = "";

    }


}
