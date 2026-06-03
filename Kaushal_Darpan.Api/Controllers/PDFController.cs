using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Kaushal_Darpan.Api.Controllers
{
  
   


    [ApiController]
    [Route("api/[controller]")]
    public class PDFController : Controller
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConverter _converter;

        public PDFController(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IConverter converter)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _converter = converter;
        }

        [HttpGet("downloadRavi")]
        public async Task<IActionResult> DownloadPdf()
        {
            // ✅ Dummy model (replace with your DB data)
            var model = new
            {
                Name = "Ravi",
                Date = DateTime.Now,
                FatherName="test"
            };

            // ✅ Step 1: Render View to HTML string
            var html = await RenderViewToString("PdfTemplate", model);

            // ✅ Step 2: Convert HTML to PDF
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            },
                Objects = {
                new ObjectSettings()
                {
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
            };

            var pdf = _converter.Convert(doc);

            // ✅ Return PDF
            return File(pdf, "application/pdf", "report.pdf");
        }

        // 🔥 Render Razor View as String (inside controller)
        private async Task<string> RenderViewToString(string viewName, object model)
        {
            var actionContext = new ActionContext(
                HttpContext,
                RouteData,
                ControllerContext.ActionDescriptor
            );

            using (var sw = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                    throw new Exception($"View '{viewName}' not found");

                var viewDictionary = new ViewDataDictionary(
                    new EmptyModelMetadataProvider(),
                    new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            }
        }
    }



 }