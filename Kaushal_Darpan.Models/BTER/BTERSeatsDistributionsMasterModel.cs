namespace Kaushal_Darpan.Models.BTEReatsDistributionsMaster
{
    public class BTERSeatsDistributionsDataModel
    {
        public int id { get; set; }
        public int max_strength { get; set; }
        public int total_seats { get; set; }
        public int remark { get; set; }
        public int sc { get; set; }
        public int sc_f { get; set; }
        public int st { get; set; }
        public int st_f { get; set; }
        public int obc { get; set; }
        public int obc_f { get; set; }
        public int mbc { get; set; }
        public int mbc_f { get; set; }
        public int ews { get; set; }
        public int ews_f { get; set; }
        public int gen { get; set; }
        public int gen_f { get; set; }

        public int tsp { get; set; }
        public int tsp_f { get; set; }

        public int km { get; set; }
        public int ph { get; set; }
        public int ex_m { get; set; }
        public int w_d { get; set; }
        public int mgm { get; set; }
        public int tfws { get; set; }
        public int mgmtotal { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int UserID { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
        public int AllotmentMasterId { get; set; }
        public string CollegeTradeId { get; set; }
        public string CollegeId { get; set; }
        public string StreamId { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public string StreamTypeId { get; set; }
        public string? IPAddress { get; set; }

    }
    public class BTERSeatsDistributionsSearchModel
    {
        public int id { get; set; }
        public int max_strength { get; set; }
        public int total_seats { get; set; }
        public int remark { get; set; }
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
  
    }

    public class BTERSeatMetrixModel
    {
        public string CollegeId { get; set; }
        public string CollegeStreamId { get; set; }
        public string StreamTypeId { get; set; }
        public string StreamId { get; set; }
        public string InstituteID { get; set; }
        public int UserId { get; set; }
        public string CollegeName { get; set; }
        public string StreamName { get; set; }
        public string StreamTypeName { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermId { get; set; }
        public string TotalSeats { get; set; }
        public string TotalM { get; set; }
        public string TotalF { get; set; }
        public string TotalSeatCumulative { get; set; }
        public string TotalSeatMCumulative { get; set; }
        public string TotalSeatFCumulative { get; set; }
        public string OBC_M { get; set; }
        public string OBC_MCumulative { get; set; }
        public string OBC_F { get; set; }
        public string OBC_FCumulative { get; set; }
        public string MBC_M { get; set; }
        public string MBC_MCumulative { get; set; }
        public string MBC_F { get; set; }
        public string MBC_FCumulative { get; set; }
        public string EWS_M { get; set; }
        public string EWS_MCumulative { get; set; }
        public string EWS_F { get; set; }
        public string EWS_FCumulative { get; set; }
        public string SC_M { get; set; }
        public string SC_MCumulative { get; set; }
        public string SC_F { get; set; }
        public string SC_FCumulative { get; set; }
        public string ST_M { get; set; }
        public string ST_MCumulative { get; set; }
        public string ST_F { get; set; }
        public string ST_FCumulative { get; set; }
        public string TSP_M { get; set; }
        public string TSP_MCumulative { get; set; }
        public string TSP_F { get; set; }
        public string TSP_FCumulative { get; set; }


        public string KM { get; set; }
        public string KM_Cumulative { get; set; }


        public string PH { get; set; }
        public string PH_Cumulative { get; set; }
        public string EX { get; set; }
        public string EX_Cumulative { get; set; }

        public string WID { get; set; }
        public string WID_Cumulative { get; set; }

        public string SMD { get; set; }
        public string SMD_Cumulative { get; set; }

        public string GEN_M { get; set; }
        public string GEN_F { get; set; }
        public string Total_M_H { get; set; }
        public string Total_F_H { get; set; }
        public string OBC_H { get; set; }
        public string MBC_H { get; set; }
        public string EWS_H { get; set; }
        public string SC_H { get; set; }
        public string ST_H { get; set; }
        public string TSP_H { get; set; }

        public string KM_H { get; set; }

        public string PH_H { get; set; }
        public string EX_H { get; set; }
        public string WID_H { get; set; }

        public string SMD_H { get; set; }


        public string Remark { get; set; }
        public string MGM { get; set; }

        public string TFWS { get; set; }


        public string? IPAddress { get; set; }
    }

    public class BTERSeatMetrixSaveModel
    {
        public string BranchCode { get; set; }
        public string CollegeId { get; set; }
        public string CollegeStreamId { get; set; }
        public string Collegename { get; set; }
        public string EWS { get; set; }
        public string EWS_F { get; set; }
        public string EX { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermId { get; set; }
        public string GEN { get; set; }
        public string GEN_F { get; set; }
        public string InstituteStreamName { get; set; }
        public string KM { get; set; }
        public string MBC { get; set; }
        public string MBC_F { get; set; }
        public string MGM { get; set; }
        public string OBC { get; set; }
        public string OBC_F { get; set; }
        public string PH { get; set; }
        public string SC { get; set; }
        public string SC_F { get; set; }
        public string SMD { get; set; }
        public string ST { get; set; }
        public string ST_F { get; set; }
        public string Shift { get; set; }
        public string StreamID { get; set; }
        public string StreamTypeId { get; set; }
        public string TFWS { get; set; }
        public string TSP { get; set; }
        public string TSP_F { get; set; }
        public string TotalF { get; set; }
        public string TotalM { get; set; }
        public string TotalSeats { get; set; }
        public int UserId { get; set; }
        public string WID { get; set; }
        public string IPAddress { get; set; }

    }



}
