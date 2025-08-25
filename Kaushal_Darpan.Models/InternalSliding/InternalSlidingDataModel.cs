namespace Kaushal_Darpan.Models.ITIAllotment
{
    public class SearchSlidingModel
    {
        public int AllotmentId { get; set; }
        public int CollegeID { get; set; }
        public int TradeID { get; set; }
        public int InstituteID { get; set; }
        public int InsID { get; set; }
        public int UnitID { get; set; }
        public int CreatedBy { get; set; }
        public int StInstituteID { get; set; }
        public int UserId { get; set; }
        public int SeatMetrixId { get; set; }
        public string? action { get; set; }
        public string? IPAddress { get; set; }
        public int TradeLevel { get; set; }
        public int DepartmentID { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationNo { get; set; } = "";
        public int SwapApplicationID { get; set; }
        public int SeatIntakeId { get; set; }
        public string SwapApplicationNo { get; set; } = "";
        public string AllotedCategory { get; set; } = "";
    }
}
