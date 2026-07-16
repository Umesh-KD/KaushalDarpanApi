using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.TheoryMarks;
using System.Data;
using System.Text;

namespace Kaushal_Darpan.Api.HtmlTempleteFile
{
    public interface IPrintHtmlFile
    {
        StringBuilder Dummy_CreatePDF();
        StringBuilder GetHtmlOfHeadingAndTabularForTabulation(DataRow streams_dr, DataTable heading_dt, DataSet tabular_ds, ResultPublishModel resultPublishModel, TabluationDataModel body);
        StringBuilder GetHtmlOfConsolidateForTabulation(DataTable consolidate_dt, DataTable heading_dt, ResultPublishModel resultPublishModel, TabluationDataModel body);
        StringBuilder CounsellingAllotmentOrder_GetHtml(DataTable consolidate_dt);
        StringBuilder GetHtmlOfTimeTable(int loopIndex, DataTable dtHeader, DataTable dtDetails);
        StringBuilder InternalAssessmentStudent_GetHtml(DataSet dataSet, int TypeID);
        StringBuilder GetHtmlOfApplicationGenrateOrderDteTHTE(DataSet ds);
        Task<StringBuilder> TheoryMarksReports_GetHtml(DataSet ds, int? IsReval);
        Task<StringBuilder> UFMCategoryReportPdf_BTER_GetHtml(DataSet ds);
        Task<StringBuilder> Collegwise_UFMCategoryReportPdf_BTER_GetHtml(DataSet ds);
        Task<StringBuilder> StudentResult_Public_GetHtml(DataSet dataSet, int ResultType);
        Task<StringBuilder> GetMarksStatisticsReport_GetHtml(DataSet dataSet, int ResultType,string ActionType );
        Task<StringBuilder> GetHtmlOfMarkSheet(DataSet ds);
        
        Task<StringBuilder> GetToppersReport_Html(DataSet dataSet, int ResultType, string ActionType);
        Task<StringBuilder> GetProvesionalMeritList_Html(DataSet dataSet, int ResultType, string ActionType);
        Task<StringBuilder> GetApprenticeshipFresherReports_Html(DataSet dataSet, int ResultType);


        Task<StringBuilder> GetHtmlOfDiplomaCertificate(DataSet ds);

        Task<StringBuilder> GetGuestHouseSlip_Html(DataSet dataSet, int ResultType);
    }
}