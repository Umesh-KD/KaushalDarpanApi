using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CounsellingMaster
{
    public class CounsellingMasterDataModel
    {
    }

    public class CounsellingMasterSearchModel
    {
    }


    public class CounsellingAllotmentListModel
    {
        public int TradeID { get; set; }
        public int? CandidateID { get; set; }
        //public string TradeName { get; set; }

        //public int CandidateCount { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }
        public string? action { get; set; }
    }

    public class Counselling_AllotmentDataModel
    {
        public int? TradeID { get; set; }
        public int? CandidateID { get; set; }
        public int? OptionID { get; set; }
        public int? ModifyBy { get; set; }
    }

    public class CounsellingAllottedListSearchModel
    {
        public int? TradeID { get; set; }
        public int? CandidateID { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? TradeName { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }
        public string? action { get; set; }
        public string? ApplicationNo { get; set; }
        public string? CandidateName { get; set; }
        public string? MobileNo { get; set; }
    }

    public class EditInstituteDataModel_Counselling
    {
        public int? TradeID { get; set; }
        public int? InstituteID { get; set; }
        public int? CandidateID { get; set; }
        public int? ModifyBy { get; set; }
        public int? OptionID { get; set; }
        public int? AllotmentID { get; set; }
        public string? AllotmentOrderPath { get; set; }
        public string? AllotmentOrder { get; set; }
    }

}
