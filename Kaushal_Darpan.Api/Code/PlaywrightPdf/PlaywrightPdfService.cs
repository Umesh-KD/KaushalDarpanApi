using Microsoft.Playwright;

namespace Kaushal_Darpan.Api.Code.PlaywrightPdf
{
    #region interface
    public interface IPlaywrightPdfService
    {
        Task<byte[]> GenerateAsync(string html, PdfOptions? options = null);
    }
    #endregion

    #region class PlaywrightBrowserManager
    public sealed class PlaywrightBrowserManager
    {
        public IBrowser Browser { get; }

        public PlaywrightBrowserManager(IBrowser browser)
        {
            Browser = browser;
        }
    }
    #endregion

    #region model
    public class PdfOptions
    {
        private string? _footerTemplate;

        public string Format { get; set; } //= "A0";
        public string Width { get; set; } //= "0in";
        public string Height { get; set; } //= "0in";
        public bool Landscape { get; set; } = false;
        public bool PrintBackground { get; set; } = true;
        public string MarginTop { get; set; } //= "0mm";
        public string MarginBottom { get; set; } //= "0mm";
        public string MarginLeft { get; set; } //= "0mm";
        public string MarginRight { get; set; } //= "0mm";
        public bool DisplayHeaderFooter { get; set; } = false;
        public string? HeaderTemplate { get; set; }
        public string? FooterTemplate
        {
            get
            {
                if (PrintFooterPageNo)
                {
                    return """
                    <div style="font-size: 10px; width: 100%; text-align: center;">
                        Page <span class="pageNumber"></span> of <span class="totalPages"></span>
                    </div>
                    """;
                }

                return _footerTemplate;
            }
            set
            {
                _footerTemplate = value;
            }
        }
        public bool PrintFooterPageNo { get; set; } = false;
        public float Scale { get; set; } = 1;
    }
    #endregion




    public class PlaywrightPdfService : IPlaywrightPdfService
    {
        private readonly PlaywrightBrowserManager _browserManager;

        public PlaywrightPdfService(PlaywrightBrowserManager browserManager)
        {
            _browserManager = browserManager;
        }

        public async Task<byte[]> GenerateAsync(string html, PdfOptions? options = null)
        {
            options ??= new PdfOptions();
            var page = await _browserManager.Browser.NewPageAsync();

            try
            {
                await page.SetContentAsync(html);

                return await page.PdfAsync(
                    new PagePdfOptions
                    {
                        Format = options.Format,
                        Width = options.Width,
                        Height = options.Height,
                        Landscape = options.Landscape,
                        PrintBackground = options.PrintBackground,
                        Scale = options.Scale,
                        DisplayHeaderFooter = options.DisplayHeaderFooter,
                        HeaderTemplate = options.HeaderTemplate,
                        FooterTemplate = options.FooterTemplate,
                        Margin = new Margin
                        {
                            Top = options.MarginTop,
                            Bottom = options.MarginBottom,
                            Left = options.MarginLeft,
                            Right = options.MarginRight
                        }
                    });
            }
            finally
            {
                await page.CloseAsync();
            }
        }
    }
}
