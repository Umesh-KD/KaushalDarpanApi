namespace Kaushal_Darpan.Models.RPPPayment
{
    public class RPPRefundTransactionDataModel
    {
        public string RPPTXNID { get; set; }
        public string PRN { get; set; }
        public string REFUNDEDAMOUNT { get; set; }
        public string REMAININGAMOUNT { get; set; }
        public string STATUS { get; set; }

        public string RESPONSEMESSAGE { get; set; }


        public string RESPONSEJSON { get; set; }
        public string RESPONSECODE { get; set; }
        public int ApplyNocApplicationID { get; set; }
        public List<RPPTRANSACTIONDetailsModel> TRANSACTIONS { get; set; }

    }
}
