using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Kaushal_Darpan.Api.Code.Helper
{

    public class PageBorderHelper : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfContentByte cb = writer.DirectContent;

            // Draw rectangle for border
            Rectangle rect = new Rectangle(
                document.PageSize.Left + 15,    // left margin for border
                document.PageSize.Bottom + 15, // bottom margin for border
                document.PageSize.Right - 15,  // right margin
                document.PageSize.Top - 15     // top margin
            );

            rect.Border = Rectangle.BOX;         // Border on all sides
            rect.BorderWidth = 1f;               // Border thickness
            rect.BorderColor = BaseColor.BLACK;  // Border color

            cb.Rectangle(rect);
        }
    }


}
