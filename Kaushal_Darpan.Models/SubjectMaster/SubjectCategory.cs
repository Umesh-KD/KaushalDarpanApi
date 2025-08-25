namespace Kaushal_Darpan.Models.SubjectCategory
{
    public class SubjectCategory
    {
        public int SubjectCategoryID { get; set; }

        public string? SubjectCategoryName { get; set; }


        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }
}
