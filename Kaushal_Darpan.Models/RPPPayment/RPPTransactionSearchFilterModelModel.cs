namespace Kaushal_Darpan.Models.RPPPayment
{
    public class RPPTransactionSearchFilterModelModel
    {
        public int DepartmentID { get; set; }
        public int? CollegeID { get; set; }
        public int? TransactionID { get; set; }
        public string? PRN { get; set; }
        public string? RPPTranID { get; set; }
        public string Key { get; set; }
        public string? RefundID { get; set; }
        public int? ApplyNocApplicationID { get; set; }

    }
}
