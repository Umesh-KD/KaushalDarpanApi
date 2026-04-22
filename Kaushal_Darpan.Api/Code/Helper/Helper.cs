using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text.pdf;
using Kaushal_Darpan.Api.Controllers;
using Kaushal_Darpan.Core.Helper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;


namespace Kaushal_Darpan.Api.Code.Helper
{
    public class Helper
    {
    }

    public static class ExtentionHelper
    {
        /// <summary>
        /// Returns a Key/Value pair with all the errors in the model
        /// according to the data annotation properties.
        /// </summary>
        /// <param name="errDictionary"></param>
        /// <returns>
        /// Key: Name of the property
        /// Value: The error message returned from data annotation
        /// </returns>
        public static List<object> GetModelErrors(this ModelStateDictionary errDictionary)
        {
            List<object> lst = new List<object>();
            errDictionary.Where(k => k.Value.Errors.Count > 0).ToList().ForEach(i =>
            {
                foreach (var item in i.Value.Errors.Select(e => e.ErrorMessage))
                {
                    lst.Add(new
                    {
                        ProptyName = i.Key,
                        ErrorMessage = item
                    });
                }

            });
            return lst;
        }

        public static void ForceHindiFont(this DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordDoc)
        {
            var body = wordDoc.MainDocumentPart.Document.Body;

            foreach (var run in body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Run>())
            {
                DocumentFormat.OpenXml.Wordprocessing.RunProperties rPr = run.RunProperties ??= new DocumentFormat.OpenXml.Wordprocessing.RunProperties();

                rPr.RunFonts = new DocumentFormat.OpenXml.Wordprocessing.RunFonts
                {
                    Ascii = "Times New Roman",
                    HighAnsi = "Times New Roman",
                    EastAsia = "Mangal",
                    ComplexScript = "Mangal"
                };

                // Required for Hindi (RTL / Complex Script)
                rPr.Languages = new DocumentFormat.OpenXml.Wordprocessing.Languages
                {
                    Bidi = "hi-IN"
                };
            }
        }

        public static decimal SafeToDecimal(this object value)
        {
            try
            {
                if (value == null)
                    return 0;

                string str = value.ToString().Trim();

                if (string.IsNullOrEmpty(str) || str == "--" || str == "N/A")
                    return 0;

                str = str.Replace("%", "");

                if (decimal.TryParse(str, out decimal result))
                    return result;

                return 0;
            }
            catch
            {
                return 0;
            }
        }

    }

    public static class WordHelper
    {
        public static void AddTable<T>(Body body, string[] headers, List<T> data)
        {
            Table table = new Table();

            // Create table properties
            TableProperties tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 }
                )
            );
            table.AppendChild(tblProp);

            // Create header row
            TableRow headerRow = new TableRow();
            foreach (var item in headers)
            {
                headerRow.AppendChild(CreateTableHeader(item));
            }
            table.AppendChild(headerRow);

            // Get properties 
            var properties = typeof(T).GetProperties();

            // Create data rows
            foreach (var item in data)
            {
                TableRow dataRow = new TableRow();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item)?.ToString() ?? string.Empty;
                    dataRow.AppendChild(CreateTableCell(value));
                }
                table.AppendChild(dataRow);
            }

            body.AppendChild(table);
        }
        public static void AddTable<T>(Body body, string[] headers, DataTable dataTable)
        {
            Table table = new Table();

            // Create table properties
            TableProperties tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 16 }
                )
            );
            table.AppendChild(tblProp);

            // Create header row
            TableRow headerRow = new TableRow();
            foreach (var item in headers)
            {
                headerRow.AppendChild(CreateTableHeader(item));
            }
            table.AppendChild(headerRow);

            /// Create data rows
            foreach (DataRow row in dataTable.Rows)
            {
                TableRow dataRow = new TableRow();
                foreach (var cell in row.ItemArray)
                {
                    dataRow.AppendChild(CreateTableCell(cell?.ToString() ?? string.Empty));
                }
                table.AppendChild(dataRow);
            }

            body.AppendChild(table);
        }
        public static void AddTitle(Body body, string text)
        {
            // Add a title
            Paragraph titlePara = body.AppendChild(new Paragraph());
            Run titleRun = titlePara.AppendChild(new Run());
            titleRun.AppendChild(new Text(text));

            // Make title bold and larger
            RunProperties titleRunProps = titleRun.PrependChild(new RunProperties());
            titleRunProps.AppendChild(new Bold());
            titleRunProps.AppendChild(new FontSize() { Val = "28" });
        }
        public static void AddParagraph(Body body, string text)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(text));
        }
        private static TableCell CreateTableHeader(string text)
        {
            TableCell cell = new TableCell();
            Paragraph para = new Paragraph();
            Run run = new Run();
            run.AppendChild(new Text(text));

            RunProperties runProps = new RunProperties();
            runProps.AppendChild(new Bold());
            run.PrependChild(runProps);

            para.AppendChild(run);
            cell.AppendChild(para);
            return cell;
        }
        private static TableCell CreateTableCell(string text)
        {
            TableCell cell = new TableCell();
            Paragraph para = new Paragraph();
            Run run = new Run();
            run.AppendChild(new Text(text));

            para.AppendChild(run);
            cell.AppendChild(para);
            return cell;
        }
        public static void MergeDocuments(string outputFilePath, List<string> inputFiles)
        {
            // Copy the first document as the base
            System.IO.File.Copy(inputFiles[0], outputFilePath, true);

            using (WordprocessingDocument mainDoc = WordprocessingDocument.Open(outputFilePath, true))
            {
                MainDocumentPart mainPart = mainDoc.MainDocumentPart;
                var mainBody = mainPart.Document.Body;

                for (int i = 1; i < inputFiles.Count; i++)
                {
                    // Add a page break before appending new content
                    mainBody.Append(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));

                    using (WordprocessingDocument tempDoc = WordprocessingDocument.Open(inputFiles[i], false))
                    {
                        Body tempBody = tempDoc.MainDocumentPart.Document.Body;
                        foreach (var element in tempBody.Elements())
                        {
                            mainBody.Append(element.CloneNode(true));
                        }
                    }
                }

                mainPart.Document.Save();
            }
        }

        public static byte[] MergePdfFiles(List<string> filePaths)
        {
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    iTextSharp.text.Document document = new iTextSharp.text.Document();
                    PdfCopy copy = new PdfCopy(document, memoryStream);
                    document.Open();

                    foreach (var file in filePaths)
                    {
                        string fileToUse = file;
                        if (!File.Exists(file))
                        {
                            fileToUse = $"{ConfigurationHelper.StaticFileRootPath}/default.pdf";
                        }

                        using (PdfReader reader = new PdfReader(fileToUse))
                        {

                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                copy.AddPage(copy.GetImportedPage(reader, i));
                            }
                        }
                    }
                    document.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return memoryStream.ToArray();
            }
        }



        #region pdf and images both get merge in a single file

        public static byte[] MergePdfAndImgFiles(List<string> filePaths)
        {
            using (var memoryStream = new MemoryStream())
            {
                var document = new iTextSharp.text.Document();
                //PdfSmartCopy
                var copy = new PdfCopy(document, memoryStream);
                document.Open();

                foreach (var file in filePaths)
                {
                    if (string.IsNullOrWhiteSpace(file)) continue;

                    string fileToUse = file;

                    if (!File.Exists(fileToUse))
                    {
                        fileToUse = Path.Combine(ConfigurationHelper.StaticFileRootPath, "default.pdf");
                    }

                    if (!File.Exists(fileToUse)) continue;

                    string ext = Path.GetExtension(fileToUse).ToLower();

                    try
                    {
                        // ✅ HANDLE PDF
                        if (ext == ".pdf")
                        {
                            using (PdfReader reader = new PdfReader(fileToUse))
                            {
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    copy.AddPage(copy.GetImportedPage(reader, i));
                                }
                            }
                        }

                        // ✅ HANDLE IMAGE (PNG/JPG)
                        else if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                        {
                            using (var imgStream = new MemoryStream())
                            {
                                var imgDoc = new iTextSharp.text.Document();
                                var writer = PdfWriter.GetInstance(imgDoc, imgStream);

                                imgDoc.Open();

                                var image = iTextSharp.text.Image.GetInstance(fileToUse);
                                image.ScaleToFit(imgDoc.PageSize.Width, imgDoc.PageSize.Height);
                                image.SetAbsolutePosition(0, 0);

                                imgDoc.Add(image);
                                imgDoc.Close();

                                using (PdfReader imgReader = new PdfReader(imgStream.ToArray()))
                                {
                                    for (int i = 1; i <= imgReader.NumberOfPages; i++)
                                    {
                                        copy.AddPage(copy.GetImportedPage(imgReader, i));
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file: {fileToUse} - {ex.Message}");
                    }
                }

                document.Close();
                return memoryStream.ToArray();
            }
        }

        #endregion
    }

}
