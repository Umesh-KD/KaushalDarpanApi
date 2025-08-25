namespace Kaushal_Darpan.Models.BTERIMCAllocationModel
{
    public class BTERIMCAllocationDataModel
    {
        public int InstituteID { get; set; }
        public int TradeID { get; set; }
        public int ApplicationID { get; set; }
        public string? MobileNo { get; set; }
        public int DirectAdmissionType { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public int CollegeTradeID { get; set; }
        public int ShiftUnit { get; set; }
        public int SeatMetrixId { get; set; }
        public string AllotedCategory { get; set; }
        public string SeatMetrixColumn { get; set; }
    }
    public class BTERIMCAllocationSearchModel
    {
        public int InstituteID { get; set; }
        public int TradeID { get; set; }
        public int ApplicationID { get; set; }
        public int ApplicationNo { get; set; }
        public int CollegeTradeID { get; set; }
        public string AllotedCategory { get; set; }
        public string MobileNo { get; set; }
        public int TradeLevel { get; set; }

    }
}
