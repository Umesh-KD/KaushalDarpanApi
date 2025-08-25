namespace Kaushal_Darpan.Models.ITISeatMatrix
{

    public class SeatSearchModel
    {
        public int AllotmentId { get; set; }
        public int FinancialYearID { get; set; }
        public int UserId    { get; set; }
        public string? IMC_SC { get; set; }
        public string? IMC_ST { get; set; }
        public string? IMC_OBC { get; set; }
        public string? IMC_GEN { get; set; }

    }
}
