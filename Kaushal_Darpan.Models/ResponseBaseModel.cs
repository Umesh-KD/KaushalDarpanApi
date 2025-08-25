namespace Kaushal_Darpan.Models
{
    public class ResponseBaseModel
    {
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }// course type
        public int EndTermID { get; set; }
        public int TermPart { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
        public int? RoleID { get; set; } = 0;
    }
}
