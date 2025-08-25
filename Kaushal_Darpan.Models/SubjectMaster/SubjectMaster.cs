using Kaushal_Darpan.Models.TimeTable;

namespace Kaushal_Darpan.Models.SubjectMaster
{
    public class SubjectMaster
    {
        public int SubjectID { get; set; }
        public int CourseType { get; set; }

        public string? SubjectName { get; set; }

        public string? SubjectCode { get; set; }
        public int max_th { get; set; }
        public int max_pr { get; set; }
        public int max_ia { get; set; }
        public int sca_grade { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int ParentID { get; set; }
        public int EndTermID { get; set; }
        public string? SubjectType { get; set; }
        public bool is_th { get; set; }
        public bool is_pr { get; set; }
        public bool is_ia { get; set; }
        public bool is_sca { get; set; }
        public bool isParent { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public bool Selected { get; set; } = false;
        public bool IsTheorySubject { get; set; } = false;
        public bool IsPracticalSubject { get; set; } = false;
        public bool IsSCASubject { get; set; } = false;
        public bool IsInternalAssisment { get; set; } = false;
        
        public decimal SubjectCredits { get; set; }


    }

    public class SubjectSearchModel
    {
        public int BranchID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int SubjectID { get; set; }
        public int  CourseType{get; set;}
    }
    public class ParentSubjectMap
    {
        public int SubjectID { get; set; }
        public List<SubjectList> SubjectList { get; set ; }
    }




    public class BackSubjectModel
    {

        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }

        public int ScaGrade { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int ParentID { get; set; }

        public bool IsTh { get; set; }
        public bool IsPr { get; set; }
        public bool IsIa { get; set; }
        public bool IsSca { get; set; }
        public bool IsParent { get; set; }
        public int DepartmentID { get; set; }
        public bool Selected { get; set; }
        public bool IsTheorySubject { get; set; }
        public bool IsPracticalSubject { get; set; }
    }
    }
