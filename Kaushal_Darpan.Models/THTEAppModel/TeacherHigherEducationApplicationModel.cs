
namespace Kaushal_Darpan.Models.Test
{
    public class TeacherHigherEducationApplicationModel : RequestBaseModel

    {
        public int THTEAppID { get; set; }
        public int StaffID { get; set; }
        public string SSOID { get; set; }
        public string TeacherName { get; set; }
        public string DOB { get; set; }
        public string JoiningDate { get; set; }
        public int AppliedCourse { get; set; }
        public string AppliedInstitute { get; set; }
        public int PHDStatus { get; set; }
        public int AppliedInstituteDistance { get; set; }
        public int AppliedInstituteCourseCategory { get; set; }
        public int AppliedInstituteSubCategory { get; set; }
        public string Remark { get; set; }
        public int CreatedBy{ get; set; }

    }

    public class THTE_DDL
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class THTE_ApplicationSearchModel
    {
        public int Id { get; set; }
        public int THTEAppID { get; set; }
        public string Name { get; set; }
        public int StaffID { get; set; }
    }

    public class PrincipleApplicationListSearchModel: RequestBaseModel
    {
        public int Id { get; set; }
        public int THTEAppID { get; set; }
        public string Name { get; set; }
        public int StaffID { get; set; }
    }

    public class DropdownDataModel
    {
        public string? action { get; set; }
    }
}
