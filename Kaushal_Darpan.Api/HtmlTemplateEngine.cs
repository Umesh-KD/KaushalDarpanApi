
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public static class HtmlTemplateEngine
{
    public static string Render(string filePath, DataSet dataSet)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Template not found.", filePath);

        string html = File.ReadAllText(filePath);
        return RenderFromHtml(html, dataSet);
    }

    public static string RenderFromHtml(string htmlTemplate, DataSet dataSet)
    {
        if (dataSet == null || dataSet.Tables.Count == 0)
            return htmlTemplate;

        htmlTemplate = ProcessLoops(htmlTemplate, dataSet);
        htmlTemplate = ProcessConditions(htmlTemplate, dataSet);
        htmlTemplate = ReplaceSimpleFields(htmlTemplate, dataSet);

        return htmlTemplate;
    }

    private static string ReplaceSimpleFields(string html, DataSet dataSet)
    {
        foreach (DataTable table in dataSet.Tables)
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

    private static string ProcessLoops(string html, DataSet dataSet)
    {
        foreach (DataTable table in dataSet.Tables)
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

                    // Handle nested loops/conditions inside this row block
                    string nested = ProcessLoops(rowContent, dataSet);
                    nested = ProcessConditions(nested, dataSet);

                    repeatedContent.Append(nested);
                }

                html = html.Replace(match.Value, repeatedContent.ToString());
            }
        }

        return html;
    }

    private static string ProcessConditions(string html, DataSet dataSet)
    {
        // Handle IF/ELSE conditions like: {#IF.Table.Column=Value#}...{#ELSE.Table.Column=Value#}...{#ENDIF.Table.Column=Value#}
        string pattern = @"{#IF\.(\w+)\.(\w+)=([^#]+)#}(.*?){#ELSE\.\1\.\2=\3#}(.*?){#ENDIF\.\1\.\2=\3#}";

        html = Regex.Replace(html, pattern, match =>
        {
            string table = match.Groups[1].Value;
            string column = match.Groups[2].Value;
            string expected = match.Groups[3].Value;
            string trueContent = match.Groups[4].Value;
            string falseContent = match.Groups[5].Value;

            if (dataSet.Tables.Contains(table) && dataSet.Tables[table].Rows.Count > 0)
            {
                string actual = dataSet.Tables[table].Rows[0][column]?.ToString();
                return actual == expected ? trueContent : falseContent;
            }

            return falseContent;
        }, RegexOptions.Singleline);

        return html;
    }
}


