
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
        public int? status { get; set; }
        
    }

    public class THTE_DropdownDataModel
    {
        public string? action { get; set; }
        public int? RoleID { get; set; }
    }

    public class UpdateApplicationStatusDataModel_Principle
    {
        public int? THTEAppID { get; set; }
        public int? ModifyBy { get; set; }
        public int? status { get; set; }
        public int? RoleID { get; set; }
        public string? Remark { get; set; }
    }

    public class UpdateApplicationStatusDataModel_Committee
    {
        public int? status { get; set; }
        public string? Remark { get; set; }
        public int? RoleID { get; set; }
        public int? ModifyBy { get; set; }
        public string? CommitteeDocs { get; set; }
        public string? Dis_CommitteeDocs { get; set; }
        public List<ApplicationListDataModel_THTE>? ApplicationListData {  get; set; }
    }

    public class ApplicationListDataModel_THTE
    {
        public int? THTEAppID { get; set; }
    }

    public class ApplicationGenrateOrderByDteListSearchModel : RequestBaseModel
    {
        public int Id { get; set; }
        public int THTEAppID { get; set; }

        public string Name { get; set; }
        public int StaffID { get; set; }
        public int? status { get; set; }
        public string THTEAppIDs { get; set; }
    }
}
