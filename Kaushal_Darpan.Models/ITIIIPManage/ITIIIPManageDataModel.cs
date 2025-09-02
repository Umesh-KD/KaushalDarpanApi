
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIIIPManageDataModel
{
    public class ITIIIPManageDataModel : RequestBaseModel
    {
        public int RegOfficeID { get; set; } = 0;
        public string RegOfficeName { get; set; } = string.Empty;
        public string RegNumber { get; set; } = string.Empty;
        public string RegDate { get; set; } = string.Empty;
        public string RegLink { get; set; } = string.Empty;
        public List<IIPManageMemberDetailsDataModel>? IMCMemberDetails { get; set; }


    }


    public class IIPManageMemberDetailsDataModel : RequestBaseModel
    {
        public int? MemberTypeID { get; set; } = 0;
        public string MemberTypeName { get; set; } = string.Empty;
        public string MemberName { get; set; } = string.Empty;
        public string MemberAddress { get; set; } = string.Empty;
        public string MemberEmail { get; set; } = string.Empty;
        public string MemberContact { get; set; } = string.Empty;
    }

    public class IIPManageFundSearchModel : RequestBaseModel
    {
        public int FundID { get; set; } = 0;
        public int IMCRegID { get; set; } = 0;
        public int QuarterID { get; set; } = 0;
        public int InstalmentPaid { get; set; } = 0;
        public int InstalmentPending { get; set; } = 0;
        public int SchemeSanctioned { get; set; } = 0;
        public int TradeAffiliated { get; set; } = 0;
        public int TradeNotAffiliated { get; set; } = 0;

        public string InstalmentPaidAmt { get; set; } = string.Empty;
        public string InstalmentPendingAmt { get; set; } = string.Empty;
        public string SchemeSanctionedTrade { get; set; } = string.Empty;
        public string TradeAffiliatedName { get; set; } = string.Empty;
        public string TradeNotAffiliatedName { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string OrderDate { get; set; } = string.Empty;

        public List<TradeList> SanctionedTradeList { get; set; } = new List<TradeList>();
        public List<TradeList> AffilateTradeList { get; set; } = new List<TradeList>();
        public List<TradeList> NotAffilateTradeList { get; set; } = new List<TradeList>();
    }

    public class TradeList : RequestBaseModel
    {
        public int ID { get; set; } = 0;
        public int TypeId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }

    public class IMCFundRevenue : RequestBaseModel
    {
        public int? ID { get; set; }
        public int? IMCFundID { get; set; }
        public int? IMCRegID { get; set; }

        public int InterestReceivedAmt { get; set; }
        public int AdmissionFeesAmt { get; set; }
        public int OtherRevenueAmt { get; set; }
        public int TotalRevenueAmt { get; set; }
               
        public int CivilAmt { get; set; }
        public int ToolsAmt { get; set; }
        public int FurnitureAmt { get; set; }
        public int BooksAmt { get; set; }
        public int AdditionalAmt { get; set; }
        public int MaintenanceAmt { get; set; }
        public int MiscellaneousAmt { get; set; }
        public int TotalExpenditureAmt { get; set; }

        public int CivilSanctionedAmt { get; set; }
        public int ToolsSanctionedAmt { get; set; }
        public int FurnitureSanctionedAmt { get; set; }
        public int BooksSanctionedAmt { get; set; }
        public int AdditionalSanctionedAmt { get; set; }
        public int MaintenanceSanctionedAmt { get; set; }
        public int MiscellaneousSanctionedAmt { get; set; }
        public int TotalSanctionedAmt { get; set; }

        public int FundAvailableAmt { get; set; }

        public int? QuarterID { get; set; }

        public string? RTS { get; set; }
        public string? ModifyDate { get; set; }   // nullable (Checked)


    }
}
