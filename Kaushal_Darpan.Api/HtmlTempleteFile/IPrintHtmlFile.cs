using System.Data;

namespace Kaushal_Darpan.Api.HtmlTempleteFile
{
    public interface IPrintHtmlFile
    {
        string Dummy_CreatePDF();
        string GetHtmlOfResultTabulation(DataTable dataTable);
    }
}