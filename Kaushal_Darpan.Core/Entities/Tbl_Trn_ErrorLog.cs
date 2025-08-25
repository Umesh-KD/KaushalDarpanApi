namespace Kaushal_Darpan.Core.Entities
{
    public class Tbl_Trn_ErrorLog
    {
        public int Id { get; set; }
        public string? ErrorDescription { get; set; }
        public DateTime CreatedDate { get; set; }

        //other use
        public int TotalRecord { get; set; }
    }
}
