

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITINCVT
{
    public class ITINCVTDataModel
    {
        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; }
        public int CreatedBy { get; set; }
        public int IPAddress { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? sortOrder { get; set; }
        public string? PushData { get; set; }
        public string? sortColumn { get; set; }
        public string? SearchText { get; set; }
        public string Action { get; set; }

        public int CollegeType { get; set; }
        public int CollegeId { get; set; }
        public string CollegeCode { get; set; }
        public int TradeId { get; set; }
        public string TradeCode { get; set; }

    }

    public class ITINCVTImportDataModel
    {
        public string id { get; set; }
        public string? student_exam_id { get; set; }
        public string? RegNo { get; set; }
        public string? roll_no { get; set; }
        public string? Exam_Type { get; set; }
        public string? DGT_code { get; set; }
        public string? Inst_Code { get; set; }
        public string? shift { get; set; }
        public string? unit { get; set; }
        public string? Type { get; set; }
        public string? Trade_code { get; set; }
        public string? Course_duration { get; set; }
        public string? Practical { get; set; }
        public string? Paper1 { get; set; }
        public string? Paper2 { get; set; }
        public string? paper3 { get; set; }
        public string? paper4 { get; set; }
        public string? Name { get; set; }
        public string? father_name { get; set; }
        public string? institute_id { get; set; }
        public string? institute_name { get; set; }
        public string? stream_id { get; set; }
        public string? stream_name { get; set; }
        public string? trade_type { get; set; }
        public string? trade_scheme_type { get; set; }
        public string? Exam_Fees { get; set; }

        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }

    }

    public class NcvtBulkDataModel
    {

        public int sessionId { get; set; }
        public int chunkIndex { get; set; }
        public int totalChunks { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public string? ExcelImportID { get; set; }
        public List<ITINCVTImportDataModel> ListData { get; set; } = new List<ITINCVTImportDataModel>();

    }


}
