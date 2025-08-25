using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Kaushal_Darpan.Api.Controllers;
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
    }
}
