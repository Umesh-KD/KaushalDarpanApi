public class ITICollegeMasterModel
{

    public int Id { get; set; }
    public int InstituteTypeID { get; set; }
    public string? SSOID { get; set; }
    public string? DGETCode { get; set; }
    public string? Name { get; set; }
    public string? CollegeCode { get; set; }
    public string? EmailAddress { get; set; }
    public string? FaxNumber { get; set; }
    public string? MobileNumber { get; set; }
    public string? Pincode { get; set; }
    public int Has8th { get; set; }
    public int Has10th { get; set; }
    public int Has12th { get; set; }
    public int ManagementTypeID { get; set; }
    public bool ActiveStatus { get; set; }
    public bool DeleteStatus { get; set; }
    public int CreatedBy { get; set; }
    public int ModifyBy { get; set; }
    public string? IPAddress { get; set; }
    public int DepartmentID { get; set; }
    public int CourseTypeID { get; set; }
    public int? CampusID { get; set; }
    public Boolean IsCampus { get; set; }
    public List<SeatIntakesModel>? SeatIntakes { get; set; }
    public string? Remark { get; set; }

    public string? OrderNo { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime EffectiveDate { get; set; }
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
    public string DgetCode { get; set; } = string.Empty;
    public int CourseTypeID { get; set; } = 0;
    public int CampusID { get; set; } = 0;
    public int InstituteID { get; set; } = 0;
    public string? IsCampus { get; set; }
}




public class ItiSearchCollegeModel
{
    public int DistrictID { get; set; } = 0;
    public int DivisionID { get; set; } = 0;
    public string SearchText { get; set; } = string.Empty;
    public int DepartmentID { get; set; } = 0;
}

public class PolotectnicSearchCollegeModel
{
    public int DistrictID { get; set; } = 0;
    public int DivisionID { get; set; } = 0;
    public string SearchText { get; set; } = string.Empty;
}

public class ItiCollegeModel
{
    public int CollegeID { get; set; } = 0;

}

public class ItiEstablishmentSearchModel
{
    public int InstituteID { get; set; } 
    public int IsNewCollege { get; set; }
    public int DistrictID { get; set; }
    public int UserID { get; set; }
    public int RoleID { get; set; }
    public string? CollegeName { get; set; }
}


public class ITIPlanningBankGuaranteeModel
{
    public int? BankGuaranteeID { get; set; }
    public int? CollageId { get; set; }
    public string? BankGuaranteeNumber { get; set; }
    public string? BankName { get; set; } 
    public string? DateOfIssue { get; set; }
    public string? Maturitydate { get; set; }
    public string? Duration { get; set; } 
    public decimal Amount { get; set; }
    public string? BankAgreementDocument { get; set; } 
    public int? Status { get; set; } 
    public string? Remarks { get; set; } 
    public int? FinYearId { get; set; } 
    public int? BankID { get; set; } 
    public string? ActionType { get; set; }
    public int? OrderNo { get; set; }
    public string? Orderdate { get; set; }

}



public class ITIPlanningBankGuarantee
{
    public int BankGuaranteeID { get; set; }
    public int ?CollageId { get; set; }
    public string? BankGuaranteeNumber { get; set; }
    public string? BankName { get; set; }
    public DateTime? DateOfIssue { get; set; }
    public DateTime? Maturitydate { get; set; }
    public string? Duration { get; set; }
    public decimal Amount { get; set; }
    public string? BankAgreementDocument { get; set; }
    public int? Status { get; set; }
    public string? Remarks { get; set; }
    public int? FinYearId { get; set; }
    public int? UserID { get; set; }

}
public class ITIPlanningBankGuaranteeSearch
{
    public int? Status { get; set; }
    public int CollageId { get; set; }
}

public class ITIPlanningBankGuaranteeReturn
{
    public int? BankGuaranteeID { get; set; }
    public int? Status { get; set; }
}

public class ITIPlanningStatusUpdateByIdModel
{
    public int? BankGuaranteeID { get; set; }
    public int? Status { get; set; }
    public string? Remarks { get; set; }
    public int OrderNo { get; set; }
    public string Orderdate { get; set; }
}

public class ITIPlanningBankGuaranteeSearchList
{
    public string? BankGuaranteeNumber { get; set; }
    public string? BankName { get; set; }
    public int status { get; set; }
    public int CollageId { get; set; }
    public int dayWise { get; set; }
    public string? GauranteeNo { get; set; }


}

public class DgtOrdersMasterModel
{
    public int Id { get; set; }

    public string OrderNo { get; set; }

    public DateTime? OrderDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool ActiveStatus { get; set; }
}
public class ITICampusStatusModel
{

    public int Id { get; set; }
    public int ModifyBy { get; set; }
    public string? CampusRemovedRemark { get; set; }

    public string? CampusRemovedOrderNo { get; set; }

    public string? CampusRemovedOrderDate { get; set; }

    public string? CampusRemovedFilePath { get; set; }
    public string? CampusRemovedDisFilePath { get; set; }



}

public class BankGuaranteeConsolidatedReportRequest
{
    public int Id { get; set; }
    public string Action { get; set; } = "_getAllData";
    public int FinancialYearID { get; set; }
}

public class BankGuaranteeConsolidatedReportModel
{
    public int Id { get; set; }
    public string CollegeName { get; set; }
    public string CollegeCode { get; set; }

    public decimal AmountAvailable { get; set; }
    public decimal AmountRequired { get; set; }
    public decimal AmountDifference { get; set; }

    public DateTime? CourtDate { get; set; }
    public string WritNo { get; set; }

    public bool? IsCourt { get; set; }
    public int? HighCourt { get; set; }
    public string HighCourtName { get; set; }

    public string CourtDocumernt { get; set; }
    public string BankStatus { get; set; }
    public string BankRemark { get; set; }
}
