using System.Data;
using System.Text;

namespace Kaushal_Darpan.Api.HtmlTempleteFile
{
    public interface IPrintHtmlFile
    {
        StringBuilder Dummy_CreatePDF();
        StringBuilder GetHtmlOfHeadingAndTabularForTabulation(DataRow streams_dr, DataTable heading_dt, DataSet tabular_ds);
        StringBuilder GetHtmlOfConsolidateForTabulation(DataTable consolidate_dt);
        StringBuilder CounsellingAllotmentOrder_GetHtml(DataTable consolidate_dt);
        StringBuilder GetHtmlOfTimeTable(int loopIndex, DataTable dtHeader, DataTable dtDetails);
        StringBuilder InternalAssessmentStudent_GetHtml(DataSet dataSet, int TypeID);
        StringBuilder GetHtmlOfApplicationGenrateOrderDteTHTE(DataSet ds);
    }
}