namespace Kaushal_Darpan.Models.CommonFunction
{
    public class CommonDDLCommonSubjectModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int? CommonSubjectID { get; set; }
        public bool? IsReval { get; set; }
    }
}
