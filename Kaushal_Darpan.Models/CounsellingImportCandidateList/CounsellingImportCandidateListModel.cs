namespace Kaushal_Darpan.Models.CounsellingImportCandidateListModel
{
    public class CounsellingImportExcelModel
    {
        public string? CandidateName { get; set; }
        public string? CandidateFatherName { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? SSOID { get; set; }
        public string? Trade { get; set; }
        public string? Designation { get; set; }
        public int? DepartmentID { get; set; }
        public int? ModifyBy { get; set; }
        public int? CandidateID { get; set; }
        //public string IPAddress { get; set; }
    }

    public class ImportCounsellingVacancyDataModel
    {
        public string? TradeCode { get; set; }
        public string? Designation { get; set; }
        public string? InstituteCode { get; set; }
        public string? VacantSeats { get; set; }
        public string? InstituteName { get; set; }
        public string? TradeName { get; set; }
        public int? UserID { get; set; }
        public int? RoleID { get; set; }
    }

    public class CounsellingVacancySearchModel
    {
        public int? TradeID { get; set; }
        public int? InstituteID { get; set; }
    }

    public class EditVacancyDataModel
    {
        public int? InstituteID { get; set; }
        public int? TradeInstituteID { get; set; }
        public int? TradeID { get; set; }
        public int? VacantSeats { get; set; }
        public string? Designation { get; set; }
        public int? UserID { get; set; }
    }
}
