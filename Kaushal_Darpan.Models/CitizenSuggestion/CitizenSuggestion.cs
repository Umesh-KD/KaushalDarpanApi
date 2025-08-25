
namespace Kaushal_Darpan.Models.CitizenSuggestion
{
    public class CitizenSuggestion
    {
        public int Pk_ID { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? MobileNo { get; set; }

        public string? SubjectId { get; set; }

        public int InstituteID { get; set; }
        public int DepartmentID { get; set; }

        public int CommnID { get; set; }

        public string? Comment { get; set; }

        public string? SRNo { get; set; }

        public string? AttchFilePath { get; set; }
        

        public int CreatedBy { get; set; }

        public int ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? RTS { get; set; }

        public bool ActiveStatus { get; set; }

        public bool DeleteStatus { get; set; }
        public string? Replay { get; set; }
    }


    public class GetfilterDataQueryCitizen
    {
        public int DivisionID { get; set; }
        public int? DistrictID { get; set; }
        public int? TehsilID { get; set; }
    }

    public class ReplayQuery
    {
        public int Pk_ID { get; set; }
        public string? Replay { get; set; }
        public string? ReplayAttachment { get; set; }
        public int? ModifyBy { get; set; }
        public bool IsResolved { get; set; }
    }

    public class CitizenSuggestionSearchModel
    {
        public int SubjectId { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int CommnID { get; set; }

        public string? SRNumber { get; set; }
        public string? MobileNo { get; set; }
        public int QueryAging { get; set; }
        public int CitizenQueryStatus { get; set; }
    }

    public class UserRatingDataModel
    {
        public int Pk_ID { get; set; }
        public string? RatingRemarks { get; set; }
        public int ModifyBy { get; set; }
        public string? SRNo { get; set; }
        public int UserRating { get; set; }
    }

    public class CitizenSuggestionSearchSRModel
    {
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public int SubjectId { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int CommnID { get; set; }

        public string? SRNumber { get; set; }
        public string? MobileNo { get; set; }
        public int QueryAging { get; set; }
        public int RoleID { get; set; }
        public int CitizenQueryStatus { get; set; }
    }


}
