namespace Kaushal_Darpan.Models.Allotment
{


    public class BTERAllotmentdataModel
    {
        public int AllotmentId { get; set; }
        public int TradeLevel { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int DepartmentID { get; set; }
        public int UserID { get; set; }
        public int AcademicYearID { get; set; }
        public int EndTermId { get; set; }
        public int StreamTypeID { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public bool IsGenerateSecondAllotment { get; set; }

    }

    public class BTERSearchModel
    {
        public int AllotmentId { get; set; }
        public int CollegeID { get; set; }
        public int TradeID { get; set; }
        public int InstituteID { get; set; }
        public int StInstituteID { get; set; }
        public int TradeLevel { get; set; }
        public int DepartmentID { get; set; }
        public int ApplicationID { get; set; }
        public int EndTermId { get; set; }

    }



    public class BTERSearchModelCounter
    {
        public string AllotmentId { get; set; }
        public int StreamTypeID { get; set; }
        public int AcademicYearID { get; set; }
        public int EndTermId { get; set; }
        public int DepartmentID { get; set; }
        public int AllotmentMasterId { get; set; }
        public int UserID { get; set; }
        public int CollegeID { get; set; }
        public int TradeID { get; set; }
        public int InstituteID { get; set; }
        public int StInstituteID { get; set; }
        public int ApplicationID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int IsPH { get; set; }
        public string action { get; set; }
        public string FilterType { get; set; }
        public string SearchText { get; set; }
        public int TradeLevel { get; set; }
    }


    public class BTERAllotmentModel
    {
        public int AllotmentId { get; set; }

        public int AcademicYearID { get; set; }
        public int EndTermId { get; set; }

        public int DepartmentID { get; set; }
        public int UserID { get; set; }


        public int StreamTypeID { get; set; }
        public int AllotmentMasterId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchText { get; set; }
        public string? FilterType { get; set; }
        public string Action { get; set; }



        public int CollegeId { get; set; }
        public int StreamID { get; set; }
        public int ShiftId { get; set; }
        public string? CollegeCode { get; set; }
        public string? StreamCode { get; set; }
        public int FeePaid { get; set; }
        public int AllotmentStatus { get; set; }
        public int RoleID { get; set; }

    }

    public class BterUploadAllotmentDataModel
    {
        public int CourseType { get; set; }
        public int AcademicYearID { get; set; }
        public int AllotmentMasterId { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public string Action { get; set; }
        public List<BterAlotmentDataModel> AllotmentData { get; set; }


    }
    public class BterAlotmentDataModel
    {

        public int CollegeStreamId { get; set; }
        public int ApplicationNo { get; set; }
        public string AllotedCategory { get; set; }
        public string AllotedGender { get; set; }
        public int AllotedPriority { get; set; }


    }

}
