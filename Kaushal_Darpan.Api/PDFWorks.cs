using Azure.Core.Pipeline;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.parser;
using Kaushal_Darpan.Core.Helper;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;

namespace Utility
{

    public static class PDFWorks
    {

     
        public static bool GeneratePDF(StringBuilder HtmlString, string filepath, string PageOriantation = "",string watermarkImagePath="")
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // Default margins
            float leftMargin = 18f;
            float rightMargin = 18f;
            float topMargin = 25f;
            float bottomMargin = 25f;

            string headerHtml = "";
            string footerHtml = "";

            var headerMatch = Regex.Match(HtmlString.ToString(), @"<table[^>]*id\s*=\s*[""']pdf-header[""'][^>]*>(.*?)</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var footerHtmlMatch = Regex.Match(HtmlString.ToString(), @"<table[^>]*id\s*=\s*[""']pdf-footer[""'][^>]*>(.*?)</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (headerMatch.Success)
            {
                headerHtml = headerMatch.ToString(); // Only inner HTML               
                topMargin = 95f;
               
            }

            if (footerHtmlMatch.Success)
            {
                footerHtml = footerHtmlMatch.ToString(); // Only inner HTML 
                bottomMargin = 95f;
            }

            string cleanedHtml = Regex.Replace(HtmlString.ToString(), @"<table[^>]*id\s*=\s*[""']pdf-header[""'][^>]*>.*?</table>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            cleanedHtml = Regex.Replace(cleanedHtml, @"<table[^>]*id\s*=\s*[""']pdf-footer[""'][^>]*>.*?</table>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Replace the HtmlString content
            HtmlString.Clear();
            HtmlString.Append(cleanedHtml);

            Document pdfDoc = PageOriantation == ""
               ? new Document(PageSize.A4, leftMargin, rightMargin, topMargin, bottomMargin)
               : new Document(PageSize.A4.Rotate(), leftMargin, rightMargin, topMargin, bottomMargin);

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(filepath, FileMode.Create));

            var fontPath1 = $"{ConfigurationHelper.RootPath}/fonts/K010_1.TTF";
            var fontPath2 = $"{ConfigurationHelper.RootPath}/fonts/krdv011.ttf";
            // 🔥 Attach header/footer event before opening the doc
            if (!string.IsNullOrWhiteSpace(headerHtml) || !string.IsNullOrWhiteSpace(footerHtml))
            {
                var headerFooter = new PdfHeaderFooter(headerHtml, footerHtml, fontPath1, fontPath2);
                writer.PageEvent = headerFooter;
            }

            if (!string.IsNullOrWhiteSpace(watermarkImagePath) || !string.IsNullOrWhiteSpace(watermarkImagePath))
            {
                var watermark = new PdfWatermark(watermarkImagePath);
                writer.PageEvent = watermark;
            }

            pdfDoc.Open();

            try
            {
                
                var fontProvider = new XMLWorkerFontProvider(XMLWorkerFontProvider.DONTLOOKFORFONTS);
                fontProvider.Register(fontPath1, "Kruti Dev 010");
                fontProvider.Register(fontPath2, "Kruti Dev 010");

                var cssFiles = new CssFilesImpl();
                cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());

                var cssResolver = new StyleAttrCSSResolver(cssFiles);
                var cssAppliers = new CssAppliersImpl(fontProvider);
                var context = new HtmlPipelineContext(cssAppliers);
                context.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                var htmlPipeline = new HtmlPipeline(context, new PdfWriterPipeline(pdfDoc, writer));
                var cssPipeline = new CssResolverPipeline(cssResolver, htmlPipeline);

                var worker = new XMLWorker(cssPipeline, true);
                var xmlParser = new XMLParser(true, worker, Encoding.UTF8);

                using (var sr = new StringReader(HtmlString.ToString()))
                {
                    xmlParser.Parse(sr);
                }

                pdfDoc.Close();
                writer.Close();
                return true;
            }
            catch (Exception ex)
            {
                pdfDoc.Close();
                writer.Close();
                throw ex;
            }
        }

        public static byte[] GeneratePDFGetByte(StringBuilder HtmlString, string PageOriantation = "" ,string watermarkImagePath="")
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            float leftMargin = 18f;
            float rightMargin = 18f;
            float topMargin = 25f;
            float bottomMargin = 25f;

            string headerHtml = "";
            string footerHtml = "";

            var headerMatch = Regex.Match(HtmlString.ToString(), @"<table[^>]*id\s*=\s*[""']pdf-header[""'][^>]*>(.*?)</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var footerHtmlMatch = Regex.Match(HtmlString.ToString(), @"<table[^>]*id\s*=\s*[""']pdf-footer[""'][^>]*>(.*?)</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (headerMatch.Success)
            {
                headerHtml = headerMatch.ToString();
                topMargin = 95f;
            }

            if (footerHtmlMatch.Success)
            {
                footerHtml = footerHtmlMatch.ToString();
                bottomMargin = 95f;
            }

            string cleanedHtml = Regex.Replace(HtmlString.ToString(), @"<table[^>]*id\s*=\s*[""']pdf-header[""'][^>]*>.*?</table>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            cleanedHtml = Regex.Replace(cleanedHtml, @"<table[^>]*id\s*=\s*[""']pdf-footer[""'][^>]*>.*?</table>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            HtmlString.Clear();
            HtmlString.Append(cleanedHtml);

            using (var memoryStream = new MemoryStream())
            {

                Document pdfDoc = PageOriantation == ""
                    ? new Document(PageSize.A4, leftMargin, rightMargin, topMargin, bottomMargin)
                    : PageOriantation == "LANDSCAPE"? new Document(PageSize.A4.Rotate(), leftMargin, rightMargin, topMargin, bottomMargin)
                    : PageOriantation == "LANDSCAPE A4" ? new Document(PageSize.LEGAL.Rotate(), leftMargin, rightMargin, topMargin, bottomMargin):
                    new Document(PageSize.A4.Rotate(), leftMargin, rightMargin, topMargin, bottomMargin)
                    ;

                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                var fontPath1 = $"{ConfigurationHelper.RootPath}/fonts/K010_1.TTF";
                var fontPath2 = $"{ConfigurationHelper.RootPath}/fonts/krdv011.ttf";

                if (!string.IsNullOrWhiteSpace(headerHtml) || !string.IsNullOrWhiteSpace(footerHtml))
                {
                    var headerFooter = new PdfHeaderFooter(headerHtml, footerHtml, fontPath1, fontPath2);
                    writer.PageEvent = headerFooter;
                }

                if (!string.IsNullOrWhiteSpace(watermarkImagePath) || !string.IsNullOrWhiteSpace(watermarkImagePath))
                {
                    var watermark = new PdfWatermark(watermarkImagePath);
                    writer.PageEvent = watermark;
                }


                pdfDoc.Open();

                try
                {
                    var fontProvider = new XMLWorkerFontProvider(XMLWorkerFontProvider.DONTLOOKFORFONTS);
                    fontProvider.Register(fontPath1, "Kruti Dev 010");
                    fontProvider.Register(fontPath2, "Kruti Dev 010");

                    var cssFiles = new CssFilesImpl();
                    cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());

                    var cssResolver = new StyleAttrCSSResolver(cssFiles);
                    var cssAppliers = new CssAppliersImpl(fontProvider);
                    var context = new HtmlPipelineContext(cssAppliers);
                    context.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                    var htmlPipeline = new HtmlPipeline(context, new PdfWriterPipeline(pdfDoc, writer));
                    var cssPipeline = new CssResolverPipeline(cssResolver, htmlPipeline);

                    var worker = new XMLWorker(cssPipeline, true);
                    var xmlParser = new XMLParser(true, worker, Encoding.UTF8);

                    using (var sr = new StringReader(HtmlString.ToString()))
                    {
                        xmlParser.Parse(sr);
                    }

                    pdfDoc.Close();
                    writer.Close();

                    return memoryStream.ToArray(); // ✅ Return byte array
                }
                catch (Exception ex)
                {
                    pdfDoc.Close();
                    writer.Close();
                    throw ex;
                }
            }
        }

   
        public static void MergePDFs(string outPutFilePath, params string[] filesPath)
        {
            List<PdfReader> readerList = new List<PdfReader>();
            foreach (string filePath in filesPath)
            {
                PdfReader pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Create blank output pdf file and get the stream to write on it.
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.Create));
            document.Open();

            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();
        }

        public static string GetHtml1(string filePath, DataSet data)
        {
            if (!System.IO.File.Exists(filePath))
                throw new FileNotFoundException("HTML template not found.", filePath);

            string html = System.IO.File.ReadAllText(filePath);

            if (data == null || data.Tables.Count == 0)
                return html;

            foreach (DataTable table in data.Tables)
            {
                if (table.Rows.Count == 0)
                    continue;

                DataRow row = table.Rows[0]; // First row per table
                string tableName = table.TableName;

                foreach (DataColumn col in table.Columns)
                {
                    string placeholder = $"{{#={tableName}.{col.ColumnName}=#}}";
                    string value = row[col]?.ToString() ?? string.Empty;
                    html = html.Replace(placeholder, value);
                }
            }
            return html;
        }
        private static string GetHtml2(string filePath, DataSet data)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("HTML template not found.", filePath);

            string html = File.ReadAllText(filePath);

            if (data == null || data.Tables.Count == 0)
                return html;

            // Process all table loops first
            foreach (DataTable table in data.Tables)
            {
                string tableName = table.TableName;

                string pattern = $@"{{#LOOP\.{tableName}#}}(.*?){{#ENDLOOP\.{tableName}#}}";
                var matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

                foreach (Match match in matches)
                {
                    string loopContent = match.Groups[1].Value;
                    StringBuilder repeatedContent = new StringBuilder();

                    foreach (DataRow row in table.Rows)
                    {
                        string rowContent = loopContent;

                        foreach (DataColumn col in table.Columns)
                        {
                            string placeholder = $"{{#={tableName}.{col.ColumnName}=#}}";
                            string value = row[col]?.ToString() ?? string.Empty;
                            rowContent = rowContent.Replace(placeholder, value);
                        }

                        repeatedContent.Append(rowContent);
                    }

                    html = html.Replace(match.Value, repeatedContent.ToString());
                }
            }

            // Process conditions
            html = Regex.Replace(html, @"{#IF\.(\w+)\.(\w+)#}(.*?){#ENDIF\.\1\.\2#}", match =>
            {
                string tableName = match.Groups[1].Value;
                string columnName = match.Groups[2].Value;
                string content = match.Groups[3].Value;

                if (data.Tables.Contains(tableName) && data.Tables[tableName].Rows.Count > 0)
                {
                    var value = data.Tables[tableName].Rows[0][columnName]?.ToString();
                    return !string.IsNullOrEmpty(value) ? content : "";
                }

                return "";
            }, RegexOptions.Singleline);

            // Simple value replacement from all tables (first row only)
            foreach (DataTable table in data.Tables)
            {
                if (table.Rows.Count == 0)
                    continue;

                DataRow row = table.Rows[0];
                string tableName = table.TableName;

                foreach (DataColumn col in table.Columns)
                {
                    string placeholder = $"{{#={tableName}.{col.ColumnName}=#}}";
                    string value = row[col]?.ToString() ?? string.Empty;
                    html = html.Replace(placeholder, value);
                }
            }

            return html;
        }

        public static string GetHtml(string filePath, DataSet data)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Template not found.", filePath);

            string html = File.ReadAllText(filePath);

            // Process loops
            html = ProcessLoops(html, data);
            html = ProcessLoops(html, data);
            html = ProcessLoops(html, data);

            // Process IF–ELSE conditions
            html = TemplateProcessor.ProcessConditions(html, data);
            html = TemplateProcessor.ProcessConditions(html, data);
            html = TemplateProcessor.ProcessConditions(html, data);

            // Replace remaining {#=Table.Column=#}
            html = ReplaceSimpleFields(html, data);

            html = Regex.Replace(html, @"</tr>\s*<tr>", "</tr><tr>", RegexOptions.IgnoreCase);
            return html;

            /*
            <!--{#IF.Student.Gender=Female#}
            <p>Welcome, Ma'am</p>
            {#ELSE.Student.Gender=Female#}
            <p>Welcome, Sir</p>
            {#ENDIF.Student.Gender=Female#}-->


            <!--{#LOOP.Marks#}
            <tr>
                <td>{#=Marks.Subject=#}</td>
                <td>{#=Marks.Score=#}</td>
            </tr>
            {#ENDLOOP.Marks#}-->
            <!--{#IF.Student.Name#}
            <p>Student Name: {#=Student.Name=#}</p>
            {#ENDIF.Student.Name#}-->


            <!--{#LOOP.Class#}
            <h4>Class: {#=Class.Name=#}</h4>
            <table>
            {#LOOP.ClassStudents#}
            <tr><td>{#=ClassStudents.Name=#}</td></tr>
            {#ENDLOOP.ClassStudents#}
            </table>
            {#ENDLOOP.Class#}-->



            <!--{#IF.Student.Gender=Female#}
            <p>Welcome, Ma'am</p>
            {#ELSE.Student.Gender=Female#}
            <p>Welcome, Sir</p>
            {#ENDIF.Student.Gender=Female#}

            {#LOOP.Subjects#}
            <p>Subject: {#=Subjects.Name=#} - Marks: {#=Subjects.Marks=#}</p>
            {#ENDLOOP.Subjects#}-->



            <!--{#IF.Student.Gender=Female#}
            <p>Welcome, Ma'am</p>
            {#ELSE.Student.Gender=Female#}
            <p>Welcome, Sir</p>
            {#ENDIF.Student.Gender=Female#}

            <table>
            {#LOOP.Subjects#}
            <tr><td>{#=Subjects.Name=#}</td><td>{#=Subjects.Score=#}</td></tr>
            {#ENDLOOP.Subjects#}
            </table>-->
            */


        }

        private static string ReplaceSimpleFields(string html, DataSet data)
        {
            foreach (DataTable table in data.Tables)
            {
                if (table.Rows.Count == 0) continue;

                var row = table.Rows[0];
                foreach (DataColumn col in table.Columns)
                {
                    string placeholder = $"{{#={table.TableName}.{col.ColumnName}=#}}";
                    string value = row[col]?.ToString() ?? string.Empty;
                    html = html.Replace(placeholder, value);
                }
            }

            return html;
        }

        public static string ReplaceCustomTag(string html) {

            var pattern = @"<Custom>(.*?)</Custom>";
            var matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                string original = match.Value;           
                string innerText = match.Groups[1].Value.Replace("\r\n", ""); 
                // Apply your transformation here (example: uppercase)
                string converted = UnicodeToKrutidev.GetKrutidev(innerText);
                string replacement = $"<Custom>{converted}</Custom>";
                html = html.Replace(original, replacement);
            }

            return html;

        }

        private static string ProcessLoops(string html, DataSet data)
        {
            foreach (DataTable table in data.Tables)
            {
                string tableName = table.TableName;
                string pattern = $@"{{#LOOP\.{tableName}#}}(.*?){{#ENDLOOP\.{tableName}#}}";
                var matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

                foreach (Match match in matches)
                {
                    string loopContent = match.Groups[1].Value;
                    StringBuilder repeatedContent = new StringBuilder();

                    foreach (DataRow row in table.Rows)
                    {
                        string rowContent = loopContent;
                        foreach (DataColumn col in table.Columns)
                        {
                            string placeholder = $"{{#={tableName}.{col.ColumnName}=#}}";
                            string value = row[col]?.ToString() ?? string.Empty;
                            rowContent = rowContent.Replace(placeholder, value);
                        }

                        // Support nested loops recursively
                        string nested = ProcessLoops(rowContent, data);
                        nested = ProcessConditions(nested, data);
                        repeatedContent.Append(nested);
                    }

                    html = html.Replace(match.Value, repeatedContent.ToString());
                }
            }

            return html;
        }

        private static string ProcessConditions1(string html, DataSet data)
        {
            // IF–ELSE
            string pattern = @"{#IF\.(\w+)\.(\w+)=([^#]+)#}(.*?){#ELSE\.\1\.\2=\3#}(.*?){#ENDIF\.\1\.\2=\3#}";
            html = Regex.Replace(html, pattern, match =>
            {
                string table = match.Groups[1].Value;
                string column = match.Groups[2].Value;
                string expected = match.Groups[3].Value;
                string trueContent = match.Groups[4].Value;
                string falseContent = match.Groups[5].Value;

                if (data.Tables.Contains(table) && data.Tables[table].Rows.Count > 0)
                {
                    string actual = data.Tables[table].Rows[0][column]?.ToString();
                    return actual == expected ? trueContent : falseContent;
                }

                return falseContent;
            }, RegexOptions.Singleline);

            return html;
        }

        private static string ProcessConditions(string html, DataSet data)
        {
            // Handle IF–ELSE blocks
            string ifElsePattern = @"{#IF\.(\w+)\.(\w+)=([^#]+)#}(.*?){#ELSE\.\1\.\2=\3#}(.*?){#ENDIF\.\1\.\2=\3#}";
            html = Regex.Replace(html, ifElsePattern, match =>
            {
                string table = match.Groups[1].Value;
                string column = match.Groups[2].Value;
                string expected = match.Groups[3].Value;
                string trueContent = match.Groups[4].Value;
                string falseContent = match.Groups[5].Value;

                if (data.Tables.Contains(table) && data.Tables[table].Rows.Count > 0)
                {
                    string actual = data.Tables[table].Rows[0][column]?.ToString();
                    return actual == expected ? trueContent : falseContent;
                }

                return falseContent;
            }, RegexOptions.Singleline);

            // Handle IF–ENDIF blocks (no ELSE)
            string ifPattern = @"{#IF\.(\w+)\.(\w+)=([^#]+)#}(.*?){#ENDIF\.\1\.\2=\3#}";
            html = Regex.Replace(html, ifPattern, match =>
            {
                string table = match.Groups[1].Value;
                string column = match.Groups[2].Value;
                string expected = match.Groups[3].Value;
                string content = match.Groups[4].Value;

                if (data.Tables.Contains(table) && data.Tables[table].Rows.Count > 0)
                {
                    string actual = data.Tables[table].Rows[0][column]?.ToString();
                    return actual == expected ? content : "";
                }

                return "";
            }, RegexOptions.Singleline);

            return html;
        }
    }

 

    public class CustomImageTagProcessor : iTextSharp.tool.xml.html.Image
    {
        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
        {
            IDictionary<string, string> attributes = tag.Attributes;
            string src;
            if (!attributes.TryGetValue(HTML.Attribute.SRC, out src))
                return new List<IElement>(1);

            if (string.IsNullOrEmpty(src))
                return new List<IElement>(1);

            if (src.StartsWith("data:image/", StringComparison.InvariantCultureIgnoreCase))
            {
                var base64Data = src.Substring(src.IndexOf(",") + 1);
                var imagedata = Convert.FromBase64String(base64Data);
                var image = iTextSharp.text.Image.GetInstance(imagedata);

                var list = new List<IElement>();
                var htmlPipelineContext = GetHtmlPipelineContext(ctx);
                list.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
                return list;
            }
            else
            {
                return base.End(ctx, tag, currentContent);
            }
        }



    }



    public static class TemplateProcessor
    {
        public static string ProcessConditions(string html, DataSet data)
        {
            // IF–ELSE blocks
            string ifElsePattern = @"{#IF\.(.*?)#}(.*?){#ELSE\.\1#}(.*?){#ENDIF\.\1#}";
            html = Regex.Replace(html, ifElsePattern, match =>
            {
                string condition = match.Groups[1].Value.Trim();
                string trueContent = match.Groups[2].Value;
                string falseContent = match.Groups[3].Value;

                return Evaluate(condition, data) ? trueContent : falseContent;
            }, RegexOptions.Singleline);

            // IF only blocks
            string ifPattern = @"{#IF\.(.*?)#}(.*?){#ENDIF\.\1#}";
            html = Regex.Replace(html, ifPattern, match =>
            {
                string condition = match.Groups[1].Value.Trim();
                string content = match.Groups[2].Value;

                return Evaluate(condition, data) ? content : "";
            }, RegexOptions.Singleline);

            return html;
        }

        private static bool Evaluate(string condition, DataSet data)
        {
            try
            {
                var tokens = new ConditionTokenizer(condition).Tokenize();
                var parser = new ConditionParser(tokens, data);
                return parser.ParseExpression();
            }
            catch
            {
                return false;
            }
        }

        // ---------------------------
        //       TOKENIZER
        // ---------------------------
        private enum TokenType { And, Or, LParen, RParen, Operator, Identifier, Literal, End }

        private class Token
        {
            public TokenType Type;
            public string Value;
            public Token(TokenType type, string value) { Type = type; Value = value; }
        }

        private class ConditionTokenizer
        {
            private readonly string _input;
            private int _pos;

            public ConditionTokenizer(string input) => _input = input;

            public List<Token> Tokenize()
            {
                var tokens = new List<Token>();

                while (_pos < _input.Length)
                {
                    if (char.IsWhiteSpace(_input[_pos])) { _pos++; continue; }

                    if (_pos + 2 <= _input.Length && _input.Substring(_pos, 2) == "&&")
                    {
                        tokens.Add(new Token(TokenType.And, "&&")); _pos += 2; continue;
                    }
                    if (_pos + 2 <= _input.Length && _input.Substring(_pos, 2) == "||")
                    {
                        tokens.Add(new Token(TokenType.Or, "||")); _pos += 2; continue;
                    }
                    if (_input[_pos] == '(') { tokens.Add(new Token(TokenType.LParen, "(")); _pos++; continue; }
                    if (_input[_pos] == ')') { tokens.Add(new Token(TokenType.RParen, ")")); _pos++; continue; }

                    if (_pos + 2 <= _input.Length)
                    {
                        string op2 = _input.Substring(_pos, 2);
                        if (op2 == ">=" || op2 == "<=" || op2 == "!=")
                        {
                            tokens.Add(new Token(TokenType.Operator, op2));
                            _pos += 2;
                            continue;
                        }
                    }

                    if ("=><".Contains(_input[_pos]))
                    {
                        tokens.Add(new Token(TokenType.Operator, _input[_pos].ToString()));
                        _pos++;
                        continue;
                    }

                    // Identifier or Literal
                    var sb = new StringBuilder();
                    while (_pos < _input.Length && !char.IsWhiteSpace(_input[_pos]) &&
                           _input[_pos] != '&' && _input[_pos] != '|' && _input[_pos] != '(' && _input[_pos] != ')' &&
                           _input[_pos] != '=' && _input[_pos] != '!' && _input[_pos] != '<' && _input[_pos] != '>')
                    {
                        sb.Append(_input[_pos++]);
                    }

                    string tokenStr = sb.ToString();
                    if (tokenStr.Contains("."))
                        tokens.Add(new Token(TokenType.Identifier, tokenStr));
                    else
                        tokens.Add(new Token(TokenType.Literal, tokenStr));
                }

                tokens.Add(new Token(TokenType.End, ""));
                return tokens;
            }
        }

        // ---------------------------
        //     PARSER & EVALUATOR
        // ---------------------------
        private class ConditionParser
        {
            private readonly List<Token> _tokens;
            private int _index;
            private readonly DataSet _data;

            public ConditionParser(List<Token> tokens, DataSet data)
            {
                _tokens = tokens;
                _index = 0;
                _data = data;
            }

            private Token Peek() => _tokens[_index];
            private Token Next() => _tokens[_index++];

            public bool ParseExpression()
            {
                bool result = ParseTerm();
                while (Peek().Type == TokenType.Or)
                {
                    Next(); // skip ||
                    result = result || ParseTerm();
                }
                return result;
            }

            private bool ParseTerm()
            {
                bool result = ParseFactor();
                while (Peek().Type == TokenType.And)
                {
                    Next(); // skip &&
                    result = result && ParseFactor();
                }
                return result;
            }

            private bool ParseFactor()
            {
                if (Peek().Type == TokenType.LParen)
                {
                    Next(); // (
                    bool result = ParseExpression();
                    if (Peek().Type != TokenType.RParen) throw new Exception("Missing )");
                    Next(); // )
                    return result;
                }

                return ParseCondition();
            }

            private bool ParseCondition()
            {
                var idToken = Next();
                if (idToken.Type != TokenType.Identifier) throw new Exception("Expected identifier");

                var op = Next();
                if (op.Type != TokenType.Operator) throw new Exception("Expected operator");

                var valToken = Next();
                if (valToken.Type != TokenType.Literal) throw new Exception("Expected literal");

                var parts = idToken.Value.Split('.');
                if (parts.Length != 2) return false;

                string table = parts[0];
                string column = parts[1];

                if (!_data.Tables.Contains(table) || _data.Tables[table].Rows.Count == 0)
                    return false;

                var actualValue = _data.Tables[table].Rows[0][column]?.ToString() ?? "";
                var expectedValue = valToken.Value;

                int comparison = string.Compare(actualValue, expectedValue, StringComparison.OrdinalIgnoreCase);
                double actualNum = 0, expectedNum = 0;
                bool numeric = double.TryParse(actualValue, out actualNum) && double.TryParse(expectedValue, out expectedNum);

                return op.Value switch
                {
                    "=" => actualValue == expectedValue,
                    "!=" => actualValue != expectedValue,
                    ">" => numeric ? actualNum > expectedNum : comparison > 0,
                    "<" => numeric ? actualNum < expectedNum : comparison < 0,
                    ">=" => numeric ? actualNum >= expectedNum : comparison >= 0,
                    "<=" => numeric ? actualNum <= expectedNum : comparison <= 0,
                    _ => throw new Exception("Unknown operator")
                };
            }
        }
    }

    public class PdfHeaderFooter : PdfPageEventHelper
    {
        private readonly ElementList _headerElements;
        private readonly ElementList _footerElements;

        public PdfHeaderFooter(string headerHtml, string footerHtml, string fontPath1, string fontPath2)
        {
            _headerElements = ParseHtmlToElements(headerHtml, fontPath1, fontPath2);
            _footerElements = ParseHtmlToElements(footerHtml, fontPath1, fontPath2);
        }

        private ElementList ParseHtmlToElements(string html, string fontPath1, string fontPath2)
        {
            var elements = new ElementList();

            var cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(true);
            var fontProvider = new XMLWorkerFontProvider(XMLWorkerFontProvider.DONTLOOKFORFONTS);
            fontProvider.Register(fontPath1, "Kruti Dev 010");
            fontProvider.Register(fontPath2, "Kruti Dev 010");

            var cssAppliers = new CssAppliersImpl(fontProvider);
            var htmlContext = new HtmlPipelineContext(cssAppliers);
            htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

            var pipeline = new CssResolverPipeline(cssResolver,
                             new HtmlPipeline(htmlContext,
                             new ElementHandlerPipeline(elements, null)));

            var worker = new XMLWorker(pipeline, true);
            var parser = new XMLParser(true, worker);
            parser.Parse(new StringReader(html));
            return elements;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // Header
            PdfPTable header = new PdfPTable(1);
            header.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            PdfPCell headerCell = new PdfPCell { Border = Rectangle.NO_BORDER };
            foreach (IElement element in _headerElements)
            {
                headerCell.AddElement(element); // ✅ this works on cell, not on header directly
            }

            header.AddCell(headerCell);
            header.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 10, writer.DirectContent);

            // Footer
            PdfPTable footer = new PdfPTable(1);
            footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            PdfPCell footerCell = new PdfPCell { Border = Rectangle.NO_BORDER };
            foreach (IElement element in _footerElements)
            {
                footerCell.AddElement(element);
            }

            footer.AddCell(footerCell);
            footer.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin - 5, writer.DirectContent);
        }
    }
}


public class PdfWatermark : PdfPageEventHelper
{
    private string _watermarkImagePath;

    public PdfWatermark(string watermarkImagePath)
    {
        _watermarkImagePath = watermarkImagePath;
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        if (string.IsNullOrEmpty(_watermarkImagePath)) return;

        try
        {
            PdfContentByte cb = writer.DirectContentUnder;
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(_watermarkImagePath);

            float width = img.ScaledWidth;
            float height = img.ScaledHeight;

            // Set transparency
            PdfGState gState = new PdfGState();
            gState.FillOpacity = 0.1f; // 10% opacity
            cb.SetGState(gState);

            // Position watermark in the center of the page
            float x = (document.PageSize.Width - width) / 2;
            float y = (document.PageSize.Height - height) / 2;

            img.SetAbsolutePosition(x, y);
            cb.AddImage(img);
        }
        catch { /* ignore if image fails */ }
    }
}