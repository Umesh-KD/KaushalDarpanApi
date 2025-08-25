public class ITICollegeMasterModel
{

    public int Id { get; set; }
    public int InstituteTypeID { get; set; }
    public string SSOID { get; set; }
    public string DGETCode { get; set; }
    public string Name { get; set; }
    public string CollegeCode { get; set; }
    public string EmailAddress { get; set; }
    public string FaxNumber { get; set; }
    public string MobileNumber { get; set; }
    public string Pincode { get; set; }
    public int Has8th { get; set; }
    public int Has10th { get; set; }
    public int Has12th { get; set; }
    public int ManagementTypeID { get; set; }
    public bool ActiveStatus { get; set; }
    public bool DeleteStatus { get; set; }
    public int CreatedBy { get; set; }
    public int ModifyBy { get; set; }
    public string IPAddress { get; set; }
    public int DepartmentID { get; set; }
    public int CourseTypeID { get; set; }
    public List<SeatIntakesModel> SeatIntakes { get; set; }
    public string? Remark { get; set; }
}

public class SeatIntakesModel
{
    public int Id { get; set; }
    public string TradeName { get; set; }
    public string TradeScheme { get; set; }
    public int RemarkID { get; set; }
    public string Remark { get; set; }
    public int TradeID { get; set; }
    public int TradeSchemeID { get; set; }
    public string Shift { get; set; }
    public string Unit { get; set; }
    public string LastSession { get; set; }
    public int ModifyBy { get; set; }
 
}

public class ITIsSearchModel
{
    public int ZoneID { get; set; } = 0;
    public int DistrictID { get; set; } = 0;
    public int TradeID { get; set; } = 0;
    public int TehsilID { get; set; } = 0;
    public int FeeStatus { get; set; } = 0;
    public int ITItypeID { get; set; } = 0;
    public int ExamTypeId { get; set; } = 0;
    public int ExamSystemId { get; set; } = 0;
    public int CourseID { get; set; } = 0;
    public int Status { get; set; } = 0;
    public int DepartmentID { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string ItiCode { get; set; } = string.Empty;
    public int CourseTypeID { get; set; } = 0;
    public int InstituteID { get; set; } = 0;
}

public class ItiSearchCollegeModel
{
    public int DistrictID { get; set; } = 0;
    public int DivisionID { get; set; } = 0;
    public string SearchText { get; set; } = string.Empty;
}

