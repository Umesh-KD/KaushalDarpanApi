using Kaushal_Darpan.Models;

public class CenterAllocationtDataModel : RequestBaseModel
{
    public int CenterID { get; set; }
    public int InstituteID { get; set; }
    public int ModifyBy { get; set; }
    public int CourseTypeID { get; set; }
    public string? IPAddress { get; set; }

}

public class InstituteList
{
    public int InstituteID { get; set; }
}

public class CenterAllocationSearchFilter : RequestBaseModel
{
    public int? DistrictID { get; set; }
    public int CenterID { get; set; }
    public string? CenterCode { get; set; }
    public string? CenterName { get; set; }

}