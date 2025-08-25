namespace Kaushal_Darpan.Models.CreateTpoMaster
{
    public class CreateTpoAddEditModel
    {
        public int UserID { get; set; }//insert or update on behalf
        public int InstituteID { get; set; }
        public string DistrictNameEnglish { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string? InstituteNameHindi { get; set; }
        public string MobileNumber { get; set; }
        public string SSOID { get; set; }
        public string Email { get; set; }
        public string EmailOfficial { get; set; }
        public string Name { get; set; }
        public bool Marked { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
    }
    
    public class TpoWebModel
    {
        public string InstituteCode { get; set; }
        public int InstituteID { get; set; }
        public int DistrictID { get; set; }
        public string DistrictNameEnglish { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string? InstituteNameHindi { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string EmailOfficial { get; set; }
        public string TPOName { get; set; }
    }






}
