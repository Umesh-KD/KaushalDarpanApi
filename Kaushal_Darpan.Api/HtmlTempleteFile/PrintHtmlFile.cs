
using AngleSharp.Html;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.TheoryMarks;
using System.Data;
using System.Text;


namespace Kaushal_Darpan.Api.HtmlTempleteFile
{
    public class PrintHtmlFile : IPrintHtmlFile
    {
        #region Test
        public StringBuilder Dummy_CreatePDF()
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"hi\">");
                sb.AppendLine("<head>");
                sb.AppendLine("    <meta charset=\"UTF-8\" />");
                sb.AppendLine("    <title>राष्ट्रीय विकास रिपोर्ट / National Development Report</title>");
                sb.AppendLine("    <style>");
                sb.AppendLine("        @font-face {");
                sb.AppendLine("            font-family: 'Noto Sans Devanagari';");
                sb.AppendLine($"            src: local('Noto Sans Devanagari'), url(\"{ConfigurationHelper.FontPath_Noto_Sans_Devanagari}\") format('truetype');");
                sb.AppendLine("        }");
                sb.AppendLine("        body {");
                sb.AppendLine("            font-family: Arial, sans-serif;");
                sb.AppendLine("            font-size: 14pt;");
                sb.AppendLine("            line-height: 1.6;");
                sb.AppendLine("            color: #222;");
                sb.AppendLine("            margin: 30px;");
                sb.AppendLine("        }");
                sb.AppendLine("        ol > li::before {");
                sb.AppendLine("            font-family: 'Noto Sans Devanagari', serif;");
                sb.AppendLine("            font-weight: bold;");
                sb.AppendLine("            margin-right: 8px;");
                sb.AppendLine("        }");
                sb.AppendLine("        .footer {");
                sb.AppendLine("            font-size: 10pt;");
                sb.AppendLine("            color: #888;");
                sb.AppendLine("            text-align: center;");
                sb.AppendLine("            margin-top: 50px;");
                sb.AppendLine("        }");
                sb.AppendLine("    </style>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                sb.AppendLine("    <h1 >राष्ट्रीय विकास रिपोर्ट 2025 / National Development Report 2025</h1>");
                sb.AppendLine("    <p>यह रिपोर्ट भारत सरकार द्वारा विभिन्न राज्यों के विकास सूचकों पर आधारित है, जिसमें शिक्षा, स्वास्थ्य, कृषि और तकनीकी प्रगति के आँकड़े सम्मिलित हैं। <br />");
                sb.AppendLine("    This report is based on the development indicators of various states by the Government of India, including education, health, agriculture, and technological progress.</p>");
                sb.AppendLine("    <ol>");
                sb.AppendLine("        <li class=\"section\">");
                sb.AppendLine("            <h2 >शिक्षा क्षेत्र में प्रगति / Progress in Education</h2>");
                sb.AppendLine("            <p >शिक्षा के क्षेत्र में पिछले पाँच वर्षों में उल्लेखनीय प्रगति हुई है। प्राथमिक शिक्षा में नामांकन की दर में 10% की वृद्धि हुई है।</p>");
                sb.AppendLine("            <p>Significant progress has been made in the education sector over the past five years. Enrollment rates in primary education have increased by 10%.</p>");
                sb.AppendLine("            <table>");
                sb.AppendLine("                <thead>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <th >राज्य / State</th>");
                sb.AppendLine("                        <th>Number of Schools</th>");
                sb.AppendLine("                        <th >छात्र संख्या / Students</th>");
                sb.AppendLine("                        <th>Literacy Rate (%)</th>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                </thead>");
                sb.AppendLine("                <tbody>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >उत्तर प्रदेश</td>");
                sb.AppendLine("                        <td>57,320</td>");
                sb.AppendLine("                        <td >18,40,000</td>");
                sb.AppendLine("                        <td>71.91%</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >महाराष्ट्र</td>");
                sb.AppendLine("                        <td>42,110</td>");
                sb.AppendLine("                        <td >12,75,000</td>");
                sb.AppendLine("                        <td>82.34%</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >बिहार</td>");
                sb.AppendLine("                        <td>39,850</td>");
                sb.AppendLine("                        <td >14,20,000</td>");
                sb.AppendLine("                        <td>61.80%</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                </tbody>");
                sb.AppendLine("            </table>");
                sb.AppendLine("        </li>");
                sb.AppendLine("        <li class=\"section\">");
                sb.AppendLine("            <h2 >स्वास्थ्य सेवाओं में सुधार / Improvement in Healthcare</h2>");
                sb.AppendLine("            <p >स्वास्थ्य सेवाओं की गुणवत्ता में सुधार के लिए 500 से अधिक प्राथमिक स्वास्थ्य केंद्रों का निर्माण किया गया है।</p>");
                sb.AppendLine("            <p>More than 500 primary health centers have been constructed to improve healthcare quality.</p>");
                sb.AppendLine("            <table>");
                sb.AppendLine("                <thead>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <th >राज्य / State</th>");
                sb.AppendLine("                        <th>Number of Hospitals</th>");
                sb.AppendLine("                        <th >डॉक्टर (सरकारी) / Doctors (Govt.)</th>");
                sb.AppendLine("                        <th>Vaccination (%)</th>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                </thead>");
                sb.AppendLine("                <tbody>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >राजस्थान</td>");
                sb.AppendLine("                        <td>3,200</td>");
                sb.AppendLine("                        <td >8,500</td>");
                sb.AppendLine("                        <td>89%</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >पश्चिम बंगाल</td>");
                sb.AppendLine("                        <td>2,900</td>");
                sb.AppendLine("                        <td >7,300</td>");
                sb.AppendLine("                        <td>91%</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >कर्नाटक</td>");
                sb.AppendLine("                        <td>2,750</td>");
                sb.AppendLine("                        <td >6,800</td>");
                sb.AppendLine("                        <td>94%</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                </tbody>");
                sb.AppendLine("            </table>");
                sb.AppendLine("        </li>");
                sb.AppendLine("        <li class=\"section\">");
                sb.AppendLine("            <h2 >कृषि उत्पादन एवं तकनीकी प्रगति / Agricultural Production & Technology</h2>");
                sb.AppendLine("            <p >कृषि में तकनीक के समावेश से पैदावार में वृद्धि हुई है। ड्रोन, सेंसर और मिट्टी परीक्षण जैसे उपकरणों का प्रयोग बढ़ा है।</p>");
                sb.AppendLine("            <p>The inclusion of technology in agriculture has increased yield. Usage of drones, sensors, and soil testing tools has risen.</p>");
                sb.AppendLine("            <table>");
                sb.AppendLine("                <thead>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <th >फसल / Crop</th>");
                sb.AppendLine("                        <th>Production (Lakh Tons)</th>");
                sb.AppendLine("                        <th >प्रमुख राज्य / Major States</th>");
                sb.AppendLine("                        <th>Technical Support</th>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                </thead>");
                sb.AppendLine("                <tbody>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >चावल</td>");
                sb.AppendLine("                        <td>115</td>");
                sb.AppendLine("                        <td >पंजाब, छत्तीसगढ़</td>");
                sb.AppendLine("                        <td>Smart Irrigation, Improved Seeds</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >गेहूं</td>");
                sb.AppendLine("                        <td>103</td>");
                sb.AppendLine("                        <td >उत्तर प्रदेश, हरियाणा</td>");
                sb.AppendLine("                        <td>Sensor-based Monitoring</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                    <tr>");
                sb.AppendLine("                        <td >गन्ना</td>");
                sb.AppendLine("                        <td>65</td>");
                sb.AppendLine("                        <td >महाराष्ट्र, बिहार</td>");
                sb.AppendLine("                        <td>Drone Spraying</td>");
                sb.AppendLine("                    </tr>");
                sb.AppendLine("                </tbody>");
                sb.AppendLine("            </table>");
                sb.AppendLine("        </li>");
                sb.AppendLine("        <li class=\"section\">");
                sb.AppendLine("            <h2 >निष्कर्ष / Conclusion</h2>");
                sb.AppendLine("            <p >भारत सरकार की योजनाओं और योजनाबद्ध प्रयासों से देश के समग्र विकास में सकारात्मक परिणाम देखने को मिले हैं। शिक्षा, स्वास्थ्य और कृषि के क्षेत्रों में सुधार स्पष्ट रूप से परिलक्षित होता है।</p>");
                sb.AppendLine("            <p>The Government of India’s schemes and planned efforts have led to positive outcomes in overall national development. Improvements in education, health, and agriculture are clearly evident.</p>");
                sb.AppendLine("        </li>");
                sb.AppendLine("    </ol>");
                sb.AppendLine("    <div class=\"footer\">");
                sb.AppendLine("        <p>© 2025 भारत सरकार | National Policy Commission</p>");
                sb.AppendLine("    </div>");
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                return sb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion



        #region Result Tabulation
        public StringBuilder GetHtmlOfHeadingAndTabularForTabulation(DataRow streams_dr, DataTable heading_dt, DataSet tabular_ds, ResultPublishModel resultPublishModel, TabluationDataModel body)
        {
            try
            {
                StringBuilder sb_hm = new StringBuilder();
                StringBuilder sb_h = new StringBuilder();

                // heading
                sb_hm.AppendLine("        <table cellspacing=\"0\" cellpadding=\"5\" style=\"width:100%; border-collapse:collapse; border: 1px solid #c3c3c3; font-family:Arial, sans-serif; font-size:14px;\">");
                sb_hm.AppendLine("            <tr>");
                sb_hm.AppendLine("                <td style=\"width:20%;\"></td>");
                sb_hm.AppendLine("                <td style=\"width:60%; text-align:center; line-height:1.5;\">");
                sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_1"]}</strong><br>");
                sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_2"]}</strong><br>");
                sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_3"]}</strong><br>");
                // for rwh
                if (body.ResultTypeId == (int)EnumResultType.RwhResult || body.ResultTypeId == (int)EnumResultType.RwhRevalEffected)
                {
                    sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_4"]}</strong>");
                }
                sb_hm.AppendLine("                </td>");
                sb_hm.AppendLine("                <td style=\"width:20%; text-align:right; vertical-align:bottom;\">");
                sb_hm.AppendLine("                    <strong>Date of Result Declaration</strong><br>");
                sb_hm.AppendLine($"                    <strong>{resultPublishModel?.ResultDeclarationDate}</strong>");
                sb_hm.AppendLine("                </td>");
                sb_hm.AppendLine("            </tr>");
                sb_hm.AppendLine("        </table>");

                // table -1
                sb_h.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\">");
                sb_h.AppendLine("            <tr style=\"border-bottom: 1px solid #000;\">");
                sb_h.AppendLine($"                <td colspan=\"14\" style=\"padding-left: 0;\"><strong>{heading_dt.Rows[0]["Institute"]}</strong></td>");
                sb_h.AppendLine($"                <td colspan=\"12\"><strong>PROGRAMME : ({streams_dr["Code"]}){streams_dr["Name"]}</strong></td>");
                sb_h.AppendLine("            </tr>");


                // get top records(header rows) of detail for header block and delete from main details tables
                int headerRowBlockCount = 5;// get only top header
                int dataRowBlockCount = 7;//data row block dotted separation line count 
                int k = 1;
                string borderSeperationStyle = "";

                bool IsAlreadySubjectTablePrinted = false;

                // Top rows
                DataTable dt_h = tabular_ds.Tables[0].AsEnumerable()
                                          .Take(headerRowBlockCount)
                                          .CopyToDataTable();

                //column
                // table -1 (heading) 
                sb_h.AppendLine("            <tr>");
                foreach (DataColumn dc in dt_h.Columns)
                {
                    sb_h.AppendLine($"                <th style=\"text-align:left;\"> {dc.ColumnName} </th>");
                }
                sb_h.AppendLine("            </tr>");

                // table -1 (heading data) 
                k = 1;
                borderSeperationStyle = "";
                foreach (DataRow dr in dt_h.Rows)
                {
                    if (k == headerRowBlockCount)
                    {
                        borderSeperationStyle = "style=\"border-bottom: 2px solid #000;\"";
                        k = 0;// reset
                    }
                    sb_h.AppendLine($"            <tr {borderSeperationStyle}>");
                    foreach (DataColumn dc in dt_h.Columns)
                    {
                        sb_h.AppendLine($"                <td> {dr[dc.ColumnName]} </td>");
                    }
                    sb_h.AppendLine("            </tr>");
                    k++;// increment
                }

                // (after) Remaining rows
                DataTable dt_tabluerdet = tabular_ds.Tables[0].AsEnumerable()
                                          .Skip(headerRowBlockCount)
                                          .CopyToDataTable();

                // main sb
                StringBuilder sb = new StringBuilder();

                // page break with pagging
                int pageSize = 49; // 7 students details

                int totalRows = dt_tabluerdet.Rows.Count;
                int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

                for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
                {
                    int startIndex = (pageNumber - 1) * pageSize; // from row
                    int endIndex = Math.Min(startIndex + pageSize, totalRows); // to row

                    DataTable dt_tabluerdet1 = dt_tabluerdet.Clone();

                    for (int j = startIndex; j < endIndex; j++)
                    {
                        dt_tabluerdet1.ImportRow(dt_tabluerdet.Rows[j]);
                    }

                    bool isLastPage = (pageNumber == totalPages);// last page

                    // main heading
                    sb.Append(sb_hm);

                    // heading
                    sb.Append(sb_h);

                    // data dynamic
                    k = 1;
                    borderSeperationStyle = "";
                    foreach (DataRow dr in dt_tabluerdet1.Rows)
                    {
                        // set seperation
                        if (k == dataRowBlockCount)
                        {
                            borderSeperationStyle = "style=\"border-bottom: 2px dotted #000;\"";
                            k = 0;// reset
                        }
                        sb.AppendLine($"            <tr {borderSeperationStyle}>");
                        for (int i = 0; i < dt_tabluerdet1.Columns.Count; i++)
                        {
                            DataColumn dc = dt_tabluerdet1.Columns[i];
                            var colval = dr[dc.ColumnName]?.ToString();
                            if (colval?.ToLower() == "detained" || colval?.ToLower() == "ufm")
                            {
                                if (k == 1)
                                {
                                    sb.AppendLine($"<td rowspan=\"{dataRowBlockCount}\" colspan=\"{dt_tabluerdet1.Columns.Count}\" style=\"text-align:center;font-weight:bolder;text-transform: uppercase;font-size:1.5em;letter-spacing:3px;\"> {dr[dc.ColumnName]} </td>");
                                }
                                break;// print one then rest exclude from creation
                            }
                            else if (colval?.StartsWith("Regul. Sub.", StringComparison.OrdinalIgnoreCase) == true ||
                                colval?.StartsWith("Fail. Sub.", StringComparison.OrdinalIgnoreCase) == true ||
                                colval?.StartsWith("RWH(Previous Semester Not Cleared)", StringComparison.OrdinalIgnoreCase) == true)
                            {
                                sb.AppendLine($"<td colspan=\"4\" style=\"text-align:left;\"> {dr[dc.ColumnName]} </td>");
                                // skip next 3 columns,
                                // print one then exclude for colspan=4, rest as it is from creation 
                                i += 3;
                                continue;
                            }
                            else
                            {
                                sb.AppendLine($"<td> {dr[dc.ColumnName]} </td>");
                            }
                        }
                        sb.AppendLine("            </tr>");

                        borderSeperationStyle = "";
                        // increament
                        k++;
                    }

                    // data close
                    sb.AppendLine("        </table>");
                    sb.AppendLine("</br>");

                    // check if possible to print subject table
                    int totalSubjectsInTable = tabular_ds.Tables[1].Rows.Count + 6; // 6 = extra row margin                    
                    if (isLastPage && Math.Abs(startIndex - endIndex) + totalSubjectsInTable < pageSize)
                    {
                        IsAlreadySubjectTablePrinted = true;
                        // table-2 to handle for new scheme
                        // data
                        if (tabular_ds.Tables[1].Rows.Count > 0)
                        {
                            sb.AppendLine("</br>");

                            // table -1
                            sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\" border=\"1\">");

                            //column table-3(actual-2)
                            // Main Header Row
                            sb.AppendLine("            <tr>");
                            foreach (DataColumn dc in tabular_ds.Tables[1].Columns)
                            {
                                sb.AppendLine($"                <th style=\"text-align:left;\"> {dc.ColumnName} </th>");
                            }
                            sb.AppendLine("            </tr>");

                            //row
                            //column data
                            foreach (DataRow dr in tabular_ds.Tables[1].Rows)
                            {
                                sb.AppendLine($"            <tr>");
                                foreach (DataColumn dc in tabular_ds.Tables[1].Columns)
                                {
                                    sb.AppendLine($"<td> {dr[dc.ColumnName]} </td>");
                                }
                                sb.AppendLine("            </tr>");
                            }
                            // table close
                            sb.AppendLine("        </table>");

                            // note
                            sb.Append("</br>");
                            sb.AppendLine("<div><b>Note : </b> (Student Centered Activity) Grading : A = Very Good, B = Good, C = Average, D = Satisfactory</div>");
                        }
                    }

                    // end pagging
                    // page break
                    sb.Append("<div class='page-break'></div>");
                }


                // table-2 to handle for new scheme
                // data
                if (IsAlreadySubjectTablePrinted == false && tabular_ds.Tables[1].Rows.Count > 0)
                {
                    // subjects
                    sb_h = new StringBuilder();
                    // table -1
                    sb_h.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\" border=\"1\">");
                    sb_h.AppendLine("            <tr>");
                    sb_h.AppendLine($"                <td colspan=\"5\" style=\"padding-left: 0;\"><strong>{heading_dt.Rows[0]["Institute"]}</strong></td>");
                    sb_h.AppendLine($"                <td colspan=\"4\"><strong>PROGRAMME : ({streams_dr["Code"]}){streams_dr["Name"]}</strong></td>");
                    sb_h.AppendLine("            </tr>");

                    // main heading
                    sb.Append(sb_hm);

                    // heading
                    sb.Append(sb_h);

                    //column table-3(actual-2)
                    // Main Header Row
                    sb.AppendLine("            <tr>");
                    foreach (DataColumn dc in tabular_ds.Tables[1].Columns)
                    {
                        sb.AppendLine($"                <th style=\"text-align:left;\"> {dc.ColumnName} </th>");
                    }
                    sb.AppendLine("            </tr>");

                    //row
                    //column data
                    foreach (DataRow dr in tabular_ds.Tables[1].Rows)
                    {
                        sb.AppendLine($"            <tr>");
                        foreach (DataColumn dc in tabular_ds.Tables[1].Columns)
                        {
                            sb.AppendLine($"<td> {dr[dc.ColumnName]} </td>");
                        }
                        sb.AppendLine("            </tr>");
                    }
                    // table close
                    sb.AppendLine("        </table>");

                    // note
                    sb.Append("</br>");
                    sb.AppendLine("<div><b>Note : </b> (Student Centered Activity) Grading : A = Very Good, B = Good, C = Average, D = Satisfactory</div>");
                }

                // page break                
                sb.Append("<div class='page-break'></div>");

                return sb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public StringBuilder GetHtmlOfConsolidateForTabulation(DataTable consolidate_dt, DataTable heading_dt, ResultPublishModel resultPublishModel, TabluationDataModel body)
        {
            try
            {
                StringBuilder sb_hm = new StringBuilder();

                // heading main
                sb_hm.AppendLine("        <table cellspacing=\"0\" cellpadding=\"5\" style=\"width:100%; border-collapse:collapse; border: 1px solid #c3c3c3; font-family:Arial, sans-serif; font-size:14px;\" >");
                sb_hm.AppendLine("            <tr>");
                sb_hm.AppendLine("                <td style=\"width:20%;\"></td>");
                sb_hm.AppendLine("                <td style=\"width:60%; text-align:center; line-height:1.5;\">");
                sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_1"]}</strong><br>");
                sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_2"]}</strong><br>");
                sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_3"]}</strong><br>");
                // for rwh
                if (body.ResultTypeId == (int)EnumResultType.RwhResult || body.ResultTypeId == (int)EnumResultType.RwhRevalEffected)
                {
                    sb_hm.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_4"]}</strong>");
                }
                sb_hm.AppendLine("                </td>");
                sb_hm.AppendLine("                <td style=\"width:20%; text-align:right; vertical-align:bottom;\">");
                sb_hm.AppendLine("                    <strong>Date of Result Declaration</strong><br>");
                sb_hm.AppendLine($"                    <strong>{resultPublishModel?.ResultDeclarationDate}</strong>");
                sb_hm.AppendLine("                </td>");
                sb_hm.AppendLine("            </tr>");
                sb_hm.AppendLine("        </table>");

                // heading
                sb_hm.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\">");
                sb_hm.AppendLine("            <tr style=\"border-bottom: 1px solid #000;\">");
                sb_hm.AppendLine($"                <td style=\"padding-left: 0;\"><strong>{heading_dt.Rows[0]["Institute"]}</strong></td>");
                sb_hm.AppendLine("            </tr>");
                sb_hm.AppendLine("        </table>");

                // 
                StringBuilder sb = new StringBuilder();

                // main heading
                sb.Append(sb_hm);

                // table -3
                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; font-family:Arial, sans-serif; font-size:14px; \" border=\"1\">");

                //column
                // Main Header Row
                sb.AppendLine("            <tr>");
                foreach (DataColumn dc in consolidate_dt.Columns)
                {
                    sb.AppendLine($"                <th style=\"text-align:left;\">{dc.ColumnName}</th>");
                }
                sb.AppendLine("            </tr>");

                //row
                //column data
                foreach (DataRow dr in consolidate_dt.Rows)
                {
                    sb.AppendLine($"            <tr>");
                    foreach (DataColumn dc in consolidate_dt.Columns)
                    {
                        sb.AppendLine($"                <td>{dr[dc.ColumnName]}</td>");
                    }
                    sb.AppendLine("            </tr>");
                }
                sb.AppendLine("        </table>");

                // note
                sb.Append("</br>");
                sb.AppendLine("<div><b>Note : </b> R -> FOR REGULATION, F ―> FAIL, P -> PASS, EC -> EARN CREDIT, GP -> GRADE POINT, PT ―> POINT SCORED, N ―>1% of Total Reg, NT -> Total Reg.ln Board Exam, NP -> NOT PROMOTED, BP --> Bridge Pass, BR --> Bridge Regulation, BU -> Bridge Unregistered</div>");

                return sb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Counselling Allotment Order
        public StringBuilder CounsellingAllotmentOrder_GetHtml(DataTable consolidate_dt)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("</br>");
                sb.AppendLine("</br>");

                // table -3
                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\">");

                //column
                // Main Header Row
                sb.AppendLine("            <tr>");
                foreach (DataColumn dc in consolidate_dt.Columns)
                {
                    sb.AppendLine($"                <th style=\"text-align:left;\">{dc.ColumnName}</th>");
                }
                sb.AppendLine("            </tr>");

                //row
                //column data
                foreach (DataRow dr in consolidate_dt.Rows)
                {
                    sb.AppendLine($"            <tr>");
                    foreach (DataColumn dc in consolidate_dt.Columns)
                    {
                        sb.AppendLine($"                <td>{dr[dc.ColumnName]}</td>");
                    }
                    sb.AppendLine("            </tr>");
                }
                sb.AppendLine("        </table>");

                return sb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Time Table
        public StringBuilder GetHtmlOfTimeTable(int loopIndex, DataTable dtHeader, DataTable dtDetails)
        {
            try
            {
                var sb = new StringBuilder();

                if (loopIndex == 1)
                {
                    // html tag
                    sb.AppendLine("<!DOCTYPE html>");
                    sb.AppendLine("<html lang=\"hi\">");
                    sb.AppendLine("<head>");
                    sb.AppendLine("    <meta charset=\"UTF-8\" />");
                    //sb.AppendLine("    <title>Examination Program - BTER Jodhpur</title>");

                    sb.AppendLine("    <style>");
                    //sb.AppendLine("        @font-face {");
                    //sb.AppendLine("            font-family: 'Noto Sans Devanagari';");
                    //sb.AppendLine($"            src: url(\"{ConfigurationHelper.FontPath_Noto_Sans_Devanagari}\") format('truetype');");
                    //sb.AppendLine("        }");

                    sb.AppendLine("        body {");
                    //sb.AppendLine("            font-family: 'Noto Sans Devanagari', 'Times New Roman', serif;");
                    sb.AppendLine("            font-size: 14px;");
                    sb.AppendLine("            line-height: 1.6;");
                    sb.AppendLine("            margin: 20px;");
                    sb.AppendLine("            color: #000;");
                    sb.AppendLine("        }");

                    sb.AppendLine("        .page {");
                    sb.AppendLine("            border: 1px solid #000;");
                    sb.AppendLine("            padding: 30px;");
                    sb.AppendLine("            margin-bottom: 40px;");
                    sb.AppendLine("        }");

                    sb.AppendLine("        h3 {");
                    sb.AppendLine("            color: #000;");
                    sb.AppendLine("            font-weight: bold;");
                    sb.AppendLine("            text-align: center;");
                    sb.AppendLine("        }");

                    sb.AppendLine("        .text-center { text-align: center; }");
                    sb.AppendLine("        .text-right { text-align: right; }");
                    sb.AppendLine("        .bold { font-weight: bold; }");
                    sb.AppendLine("        .underline { text-decoration: underline; }");

                    sb.AppendLine("        table {");
                    sb.AppendLine("            width: 100%;");
                    sb.AppendLine("            border-collapse: collapse;");
                    sb.AppendLine("            font-size: 13px;");
                    sb.AppendLine("        }");

                    sb.AppendLine("        th, td {");
                    sb.AppendLine("            border: 1px solid #000;");
                    sb.AppendLine("            padding: 6px;");
                    sb.AppendLine("            text-align: center;");
                    sb.AppendLine("        }");

                    sb.AppendLine("        .signature {");
                    sb.AppendLine("            margin-top: 40px;");
                    sb.AppendLine("            text-align: right;");
                    sb.AppendLine("            font-weight: bold;");
                    sb.AppendLine("        }");

                    sb.AppendLine("    </style>");
                    sb.AppendLine("</head>");
                    sb.AppendLine("<body>");

                }

                // ================= PAGE 1 =================
                sb.AppendLine("<div class=\"\">");

                sb.AppendLine("<div class=\"text-center\">");
                sb.AppendLine("<h3 style=\"text-align:center;color:#000;font-weight:bold;font-size:18px;margin:0;\">राजस्थान सरकार</h3>");
                sb.AppendLine("<h2 style=\"text-align:center;color:#000;font-weight:bold;font-size:16px;\">प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर</h2>");
                sb.AppendLine("<p style=\"text-align:center;color:#000;font-size:13px;\">डब्ल्यू-6 रेजीडेन्सी रोड जोधपुर<br/>ई-मेल : bter.jodhpur@rajasthan.gov.in</p>");
                sb.AppendLine("</div>");

                sb.AppendLine($"<p class=\"bold\">क्रमांक:- एफ-7 / प्राशिमं / परीक्षा / {dtHeader.Rows[0]["EndTermName"]} /</p>");

                sb.AppendLine("<p>");
                sb.AppendLine("प्रधानाचार्य,<br/>");
                sb.AppendLine($"समस्त राजकीय एवं निजी पॉलिटेक्निक कॉलेज ({dtHeader.Rows[0]["CourseTypeName"]})");
                sb.AppendLine("</p>");

                sb.AppendLine("<p class=\"bold underline\">");
                sb.AppendLine($"विषय :- परीक्षा {dtHeader.Rows[0]["CourseTypeName"]} सत्र {dtHeader.Rows[0]["FinancialYearName"]} ({dtHeader.Rows[0]["CourseTypeName"]}) सेमेस्टर पद्धति एवं सत्र {dtHeader.Rows[0]["FinancialYearName"]} ({dtHeader.Rows[0]["CourseTypeName"]}) स्पेशल परीक्षा कार्यक्रम।");
                sb.AppendLine("</p>");

                sb.AppendLine("<p>महोदय,</p>");

                sb.AppendLine("<p style=\"text-indent:40px;\">");
                sb.AppendLine($"उपरोक्त विषयानुसार लेख है कि परीक्षा <b>{dtHeader.Rows[0]["EndTermName"]} सत्र {dtHeader.Rows[0]["FinancialYearName"]} ({dtHeader.Rows[0]["CourseTypeName"]})</b> सेमेस्टर पद्धति एवं ");
                sb.AppendLine($"<b>{dtHeader.Rows[0]["FinancialYearName"]} ({dtHeader.Rows[0]["CourseTypeName"]})</b> स्पेशल परीक्षा का कार्यक्रम आपको प्रेषित किया जा रहा है। ");
                sb.AppendLine("यदि कोई त्रुटि पाई जाये तो अविलम्ब सूचित करें।");
                sb.AppendLine("</p>");

                sb.AppendLine("<p>संलग्न :- उपरोक्तानुसार</p>");

                sb.AppendLine("<div style=\"text-align:right;font-weight:bold;\">");
                sb.AppendLine("भवदीय<br/>");
                sb.AppendLine("संयुक्त निदेशक एवं रजिस्ट्रार");
                sb.AppendLine("</div>");

                sb.AppendLine("<p style=\"display:flex; align-items:center; justify-content:space-between;\">");
                sb.AppendLine($"<p style=\"font-size:13px;\">क्रमांक:- एफ-7 / प्राशिमं / परीक्षा / {dtHeader.Rows[0]["YearName"]}/</p>");
                sb.AppendLine($"<p style=\"text-align:right; font-size:13px;\">Date: {dtHeader.Rows[0]["CurrentDate"]}</p> ");
                sb.AppendLine("</p>");

                sb.AppendLine("<p style=\"text-indent:40px;\">");
                sb.AppendLine("प्रतिलिपि निम्नलिखित को सूचनार्थ एवं आवश्यक कार्यवाही हेतु प्रेषित है :-<br>");
                sb.AppendLine("1. निदेशक प्राविधिक शिक्षा निदेशालय जोधपुर।<br>");
                sb.AppendLine("2. संयुक्त निदेशक एवं सचिव, प्राविधिक शिक्षा मण्डल जोधपुर।<br>");
                sb.AppendLine("3. संयुक्त सचिव, तकनीकी शिक्षा, शासन सचिवालय, जयपुर।<br>");
                sb.AppendLine("4. संयुक्त निदेशक (ई-1/ई-2), प्राविधिक शिक्षा निदेशालय, जोधपुर।<br>");
                sb.AppendLine("5. संयुक्त निदेशक, गोपनीय प्रथम/द्वितीय प्राविधिक शिक्षा मंडल, जोधपुर।<br>");
                sb.AppendLine("6. एस.ए (संयुक्त निदेशक) प्राविधिक शिक्षा मंडल, जोधपुर को भेजकर लेख है कि परीक्षा कार्यक्रम, रोल लिस्ट एवं प्रवेश कर अपलोड करावें ।<br>");
                sb.AppendLine("7. कार्यालयाध्यक्ष, प्राविधिक शिक्षा मण्डल जोधपुर ।<br>");
                sb.AppendLine("8. प्रभारी, कम्प्यूटर / भण्डार / सीडीसी / लेखा शाखा, प्राशिसं, जोधपुर ।<br>");
                sb.AppendLine("9. नोडल अधिकारी प्राविधिक शिक्षा मण्डल, जोधपुर ।<br>");
                sb.AppendLine("</p>");

                sb.AppendLine("</div>");

                // ================= PAGE 2 =================
                sb.AppendLine("<div class=\"\">");

                sb.AppendLine("<div class=\"text-center\">");
                sb.AppendLine("<h3 style=\"text-align:center;color:#000;font-weight:bold;font-size:16px;margin:0;\">GOVERNMENT OF RAJASTHAN</h3>");
                sb.AppendLine("<h2 style=\"text-align:center;color:#000;font-weight:bold;font-size:16px;margin:0;\">BOARD OF TECHNICAL EDUCATION RAJASTHAN, JODHPUR</h2>");
                sb.AppendLine($"<h3 style=\"text-align:center;color:#000;font-weight:bold;font-size:16px;margin:0;\">EXAMINATION PROGRAMME OF DIPLOMA IN {dtHeader.Rows[0]["CourseTypeNameFull"]}</h3>");
                sb.AppendLine($"<h3 style=\"text-align:center;color:#000;font-weight:bold;font-size:16px;margin:0;\">{dtHeader.Rows[0]["ExamName"]}</h3>");
                sb.AppendLine($"<h3 style=\"text-align:center;color:#000;font-weight:bold;font-size:11px;margin:0;\">{dtHeader.Rows[0]["CommonSubjectText"]}</h3>");
                sb.AppendLine($"<h4 style=\"text-align:center;color:#000;font-weight:bold;font-size:16px; margin-bottom:10px;\">{dtHeader.Rows[0]["ExamScheme"]}</h4>");
                sb.AppendLine("<h3 style=\"text-align:center;color:#000;font-weight:bold;font-size:11px;margin:0;\">(Branches: AR, AC, CC, CE, CI, CH, CS, EF, EE, EL, ER, FD, HM, IE, IT, MA, ME, MP, MR, MT, PE, PL, PR, CV, RA, LS, RE)</h3>");
                sb.AppendLine("<h3 style=\"text-align:center;color:#000;font-weight:bold;font-size:11px;margin-bottom:10px;\">Practical exam will be conducted from ___________ to ___________ at institute level.</h3>");
                sb.AppendLine("</div>");

                sb.AppendLine("<table style=\"font-size:14px;\">");
                sb.AppendLine("<thead>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<th style=\"font-size:13px; font-weight:bold;padding:5px;text-align:center;\">Time</th>");
                sb.AppendLine("<th style=\"font-size:13px; font-weight:bold; padding:5px;text-align:center;\">Date</th>");
                sb.AppendLine("<th style=\"font-size:13px; font-weight:bold; padding:5px;text-align:center;\">Code</th>");
                sb.AppendLine("<th style=\"font-size:13px; font-weight:bold;padding:5px;text-align:center;\">Subject</th>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</thead>");

                sb.AppendLine("<tbody>");

                //  details
                foreach (DataRow dr in dtDetails.Rows)
                {
                    sb.AppendLine("<tr>");
                    //sb.AppendLine($"<td style=\"font-size:13px;padding:5px;text-align:center;\" rowspan=\"4\">{dr["StartTime"]} - {dr["EndTime"]}</td>");
                    sb.AppendLine($"<td style=\"font-size:13px;padding:5px;text-align:center;\">{dr["StartTime"]} - {dr["EndTime"]}</td>");
                    sb.AppendLine($"<td style=\"font-size:13px;padding:5px;text-align:center;\">{dr["ExamDate"]}</td>");
                    sb.AppendLine($"<td style=\"font-size:13px;padding:5px;text-align:center;\">{dr["PaperCode"]}</td>");
                    sb.AppendLine($"<td style=\"font-size:13px;padding:5px;text-align:center;\">{dr["SubjectName"]}</td>");
                    sb.AppendLine("</tr>");

                }

                sb.AppendLine("</tbody>");
                sb.AppendLine("</table>");

                sb.AppendLine("<p style=\"font-size:10px;\">");
                sb.AppendLine("<b>Note:-</b><br>");
                sb.AppendLine("Institution Level Practical Examination will be arranged by the concerned Principal of the Institution. Send a copy of Practical Examination program and practical papers to the Board.<br>");
                sb.AppendLine("</p>");

                sb.AppendLine("<p style=\"font-size:10px;\">");
                sb.AppendLine("<b>Instructions for students :-</b><br>");
                sb.AppendLine("1. Candidates must carefully note the date, day and time given in this program. Ignorance of correct time and date will not serve as an excuse for delay or absence.<br>");
                sb.AppendLine("2. Candidate should bring their instrument boxes and other materials such as pen, holders, pencils, setsquares, rubber etc. The programmable calculators are not permitted in the examination hall.<br>");
                sb.AppendLine("3. No borrowings or lending of any materials will be permitted in examination hall.<br>");
                sb.AppendLine("4. Candidate should not bring with them in the examination hall any unauthorized materials written or printed or scribbled by person or any loose papers or books, otherwise they shall be penalized for using unfair means under rules.<br>");
                sb.AppendLine("5. No guarantee is given to candidate regarding the order of the question papers.<br>");
                sb.AppendLine("6. All safety measures as prescribed by the Central Govt. & Rajasthan State Govt. in view of COVID-19 pandemic (eg. Thermal Scanning, wearing of mask, social distancing,sanitization etc.) to be strictly adhered by all students and institutions.\r\n<br>");
                sb.AppendLine("</p>");

                sb.AppendLine("<div style=\"text-align:right; font-weight:bold;margin-top:15px;\">REGISTRAR</div>");
                sb.AppendLine("</div>");

                return sb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Internal Assessment Student
        public StringBuilder InternalAssessmentStudent_GetHtml(DataSet dataSet, int TypeID)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable dt_heading = dataSet.Tables[0];
                DataTable dt_data = dataSet.Tables[1];

                // heading
                DataRow firstRow = dt_heading.Rows[0];

                string StreamName = firstRow["StreamName"].ToString();
                string InstituteName = firstRow["InstituteName"].ToString();
                string ReportType = firstRow["ReportType"].ToString();
                string ExamSession = firstRow["ExamSession"].ToString();

                // page break with pagging
                int pageSize = 23;

                for (int i = 0; i < dt_data.Rows.Count; i += pageSize)
                {
                    DataTable dt = dt_data.Clone();

                    // max marks row
                    if (i > 0)
                    {
                        dt.ImportRow(dt_data.Rows[0]);
                    }

                    for (int j = i; j < i + pageSize && j < dt_data.Rows.Count; j++)
                    {
                        dt.ImportRow(dt_data.Rows[j]);
                    }

                    sb.Append(@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    <meta charset='utf-8'>
                    <style>
                        body { font-family: Arial; font-size: 12px; }
                        table { width: 100%; border-collapse: collapse; }
                        th, td { border: 1px solid #000; padding: 4px; text-align: center; font-size:10px;}
                        th { background-color: #f2f2f2; }
                        .table-row {
                            font-size: 11px;
                        }
                        .left { text-align: left; }
                        .header { font-size:15px;text-align: center; font-weight: bold; margin-bottom: 10px; }
                        .page-break {page-break-after: always; }
                        
                    .line {
                          display: inline-block;
                          min-width: 150px;
                          border-bottom: 1px dashed #000;
                        }
                        .footer {
                          bottom: 0;
                          left: 0;
                          right: 0;
                          border-collapse: collapse;
                          padding: 8px 10px;
                          margin-top:10px;
                        }
                        .footer .note {
                          font-size: 11px;
                          margin-bottom: 8px;
                        }
                        .footer-table {
                          width: 100%;
                          border-collapse: collapse;
                        }
 
                        .footer-table td {
                          width: 33%;
                          vertical-align: bottom;
                          padding-top: 5px;
                        }
                        .footer-table tr,
                        .footer-table td,
                        .footer-table th {
                          border: none !important;
                          text-align:left;
                        }
                        .bold-text {
                            font-weight: bold;
                        }

                    </style>
                    </head>
                    <body>
                ");

                    sb.Append($"<div class='header'> {ExamSession} <span> {ReportType} </span></div>");
                    sb.Append($"<div style='font-weight:bold; margin-bottom:8px; font-size:12px' >  College Name: {InstituteName}  (Branch: {StreamName}) </div>");

                    // table
                    sb.Append("<table style='font-size:12px'>");

                    // th
                    sb.Append("<tr>");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sb.Append($"<th>{dc.ColumnName}</th>");
                    }
                    sb.Append("</tr>");

                    // td

                    foreach (DataRow dr in dt.Rows)
                    {


                        sb.Append("<tr class='table-row'>");

                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dr[dc.ColumnName].ToString() == "MAX MARKS")
                            {
                                sb.Append($"<td class='bold-text'>{dr[dc.ColumnName]}</td>");
                            }
                            else
                            {
                                sb.Append($"<td>{dr[dc.ColumnName]}</td>");
                            }
                        }

                        sb.Append("</tr>");
                    }

                    // teacher row
                    if (TypeID != 3)
                    {
                        int i1 = 0;
                        sb.Append("<tr>");
                        sb.Append($"<td colspan='5'>Teacher Signature :</td>");
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (i1 > 4)
                            {
                                sb.Append($"<td >&nbsp;</td>");
                            }
                            i1++;
                        }
                        sb.Append("</tr>");
                    }
                    // table
                    sb.Append("</table>");

                    // footer
                    sb.Append("<div class=\"footer\">\r\n  <div><b>CERTIFICATE :</b> Entered marks as per maintained records by institute.</div>\r\n\r\n  <div class=\"note\">\r\n    <b>NOTE:</b> The record of students securing &lt; 45% or &gt; 85% marks have been reviewed to my satisfaction.\r\n  </div>\r\n\r\n  <table class=\"footer-table\">\r\n  <tr>\r\n  <th>Feeded By</th>\r\n  <th>Checked By</th>\r\n  <th>Signature:</th>\r\n  </tr>\r\n    <tr>\r\n      <td>\r\n        Signature: <span class=\"line\"></span><br><br>\r\n        Name: <span class=\"line\"></span><br><br>\r\n        Date: <span class=\"line\"></span>\r\n      </td>\r\n\r\n      <td>\r\n        Signature: <span class=\"line\"></span><br><br>\r\n        Name: <span class=\"line\"></span>\r\n      </td>\r\n\r\n      <td>\r\n        Principal: <span class=\"line\"></span><br>\r\n      </td>\r\n    </tr>\r\n  </table>\r\n</div>");

                    sb.Append("<div class='page-break'></div>");
                    sb.Append("</body>");
                    sb.Append("</html>");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating HTML", ex);
            }

            return sb;
        }
        #endregion

        #region Application GenrateOrder Dte THTE
        public StringBuilder GetHtmlOfApplicationGenrateOrderDteTHTE(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                DataTable dt1 = ds.Tables[0];//session
                DataTable dt2 = ds.Tables[1];//data

                // heading
                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"hi\">");
                sb.AppendLine("<head>");
                sb.AppendLine("    <meta charset=\"UTF-8\" />");
                sb.AppendLine("    <title>Office Order</title>");
                sb.AppendLine("    <style>");
                sb.AppendLine("        @font-face {");
                sb.AppendLine("            font-family: 'Noto Sans Devanagari';");
                sb.AppendLine($"            src: local('Noto Sans Devanagari'), url(\"{ConfigurationHelper.FontPath_Noto_Sans_Devanagari}\") format('truetype');");
                sb.AppendLine("        }");
                sb.AppendLine("</style>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");

                //1. 
                sb.AppendLine($"<div style=\"max-width: 210mm; margin: 0 auto; padding: 20px;background-color: white;color: black;line-height: 1.5;\">          <div style=\"text-align: center;margin-bottom: 20px; position: relative;\">             <div style=\"text-align: right;text-decoration: underline;font-weight: bold;margin-bottom: 10px;\">ई मेल से                 प्रेषित (अति-आवश्यक)</div>             <div style=\" font-size: 1.2em; font-weight: bold;\">राजस्थान सरकार</div>             <div style=\"font-size: 1.1em;font-weight: bold;\">तकनीकी शिक्षा निदेशालय, राजस्थान, जोधपुर।</div>         </div>          <div style=\"display: flex;justify-content: space-between;margin-top: 20px;margin-bottom: 20px;\">             <div>क्रमांक :- एफ 10(17) प्राशिनि / ई-1 / सी-1 / {dt1.Rows[0]["YearName"]} /</div>             <div>दिनांक :- {dt1.Rows[0]["CurrentDate"]}</div>         </div>          <div             style=\"text-align: center;font-weight: bold;text-decoration: underline;font-size: 1.2em;margin-bottom: 20px;\">             कार्यालय आदेश</div>          <div style=\"text-align: justify;margin-bottom: 20px;\">             प्रशासनिक विभाग द्वारा जारी नई उच्च अध्ययन नीति दिनांक {dt1.Rows[0]["HTERulesDate"]} में प्रावधित नियमों के अनुसार गठित कमेटी की             अभिशंषा एवं प्रशासनिक विभाग के पत्रांक एफ 8 (17) त.शि./ {dt1.Rows[0]["HTERulesYear"]} पार्ट दिनांक {dt1.Rows[0]["CurrentDate"]} द्वारा अनुमोदित अन्तिम             सूची             अनुसार शैक्षणिक सत्र {dt1.Rows[0]["FinancialYearName"]} (प्रथम सत्र उच्च अध्ययन प्रक्रिया के अन्तर्गत) हेतु निम्न शिक्षकों को             शैक्षणिक             सत्र {dt1.Rows[0]["FinancialYearName"]} के लिए अंशकालीन / पार्टटाईम / मोड्यूलर आधार पर उच्च अध्ययन किये जाने की अनुमति निम्न शर्तों             के             अध्याधीन प्रदान की जाती है :-         </div>");

                //2.
                sb.Append($"<ol style=\" margin-left: 20px;\">             <li style=\"margin-bottom: 10px;\">उच्च अध्ययन के कारण संस्थान में शैक्षणिक / अध्ययन कार्य एवं राजकीय कार्य                 बाधित                 नहीं होंगे।</li>             <li style=\"margin-bottom: 10px;\">संस्थान के कार्यालय समय में उच्च अध्ययन कोर्स करने की अनुमति नहीं होगी।             </li>             <li style=\"margin-bottom: 10px;\">उच्च अध्ययन हेतु किसी संस्थान में चयन हो जाने मात्र से कार्मिक को उच्च                 अध्ययन                 हेतु अनुमति का अधिकार प्राप्त                 नहीं होगा।</li>             <li style=\"margin-bottom: 10px;\">राज्य सरकार प्रशासनिक कारणों से किसी भी समय उच्च अध्ययन की अनुमति समाप्त कर                 सकती है</li>             <li style=\"margin-bottom: 10px;\">राज्य सरकार प्रशासनिक कार्यों से, ऐसे कार्मिक जिन्हे इस उच्च अध्ययन नीति के                 तहत                 अनुमति प्रदान की गई है, का                 स्थानान्तरण कर सकती है।</li>             <li style=\"margin-bottom: 10px;\">पीएचडी (पार्ट टाईम) उच्च अध्ययन हेतु पदस्थापित पॉलिटेक्निक संस्थान से उच्च                 अध्ययन संस्थान के मध्य दूरी {dt1.Rows[0]["DistanceBetweenInstitute"]}                 कि.मी. से अधिक न हो।</li>         </ol>");

                //3.
                sb.Append("<table style=\"width: 100%;border-collapse: collapse;margin-top: 20px;margin-bottom: 20px;\">             <thead>                 <tr>                     <th                         style=\"width: 5%;text-align: center; font-weight: bold;border: 1px solid black;padding: 8px;vertical-align: top;\">                         क्र.सं.</th>                     <th                         style=\"width: 30%; text-align: center; font-weight: bold;border: 1px solid black;padding: 8px;vertical-align: top;\">                         कार्मिक का नाम,पद, एवं पदस्थापन स्थान                     </th>                     <th                         style=\"width: 30%;text-align: center; font-weight: bold;border: 1px solid black;padding: 8px;vertical-align: top;\">                         पाठ्यक्रम का नाम जिसके लिये आवेदन किया गया                         है</th>                     <th                         style=\"width: 35%;text-align: center; font-weight: bold;border: 1px solid black;padding: 8px;vertical-align: top;\">                         उच्च अध्ययन की जाने वाली संस्थान का नाम                     </th>                 </tr>             </thead>");

                //loop table data
                sb.Append("<tbody>");
                int i = 1;
                foreach (DataRow dr in dt2.Rows)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td style=\"text-align: center;border: 1px solid black;padding: vertical-align: top;\">{i}</td>");
                    sb.Append($"<td style=\"border: 1px solid black;padding: 8px;text-align: left;vertical-align: top;\">{dr["TeacherName"]}, {dr["DesignationNameEnglish"]}, {dr["DistrictNameEnglish"]}</td>");
                    sb.Append($"<td style=\"border: 1px solid black;padding: 8px;text-align: left;vertical-align: top;\">{dr["AppliedInstituteCategory"]}</td>");
                    sb.Append($"<td style=\"border: 1px solid black;padding: 8px;text-align: left;vertical-align: top;\">{dr["AppliededInstitute"]}</td>");
                    sb.Append("</tr>");
                    i++;
                }
                sb.Append("</tbody>");
                sb.Append("</table>");


                //4.
                //sb.Append($"<div style=\"display: flex;justify-content: flex-end; width: 100%;\">             <div style=\"text-align:center; margin-top: 40px; margin-bottom: 20px; width:250px; float:right;\">                 (आलोक बंसल)<br>                 निदेशक, तकनीकी शिक्षा             </div>         </div>          <div style=\"margin-bottom: 10px;display: flex;justify-content: space-between;\">             <div>क्रमांक :- एफ 10(17) प्राशिनि / ई-1 / सी-1 / {dt1.Rows[0]["YearName"]} / .... </div>             <div style=\"white-space: nowrap;\">दिनांक :- .....</div>         </div>          <div>प्रतिलिपि निम्नलिखित को सूचनार्थ एवं आवश्यक कार्यवाही हेतु प्रेषित है :-</div>");
                sb.Append($@"
<div style='display: flex; justify-content: flex-end; width: 100%;'>
    <div style='text-align: right; margin-top: 40px; margin-bottom: 20px; width: 250px;'>
        (आलोक बंसल)<br>
        निदेशक, तकनीकी शिक्षा
    </div>
</div>

<div style='margin-bottom: 10px; display: flex; justify-content: space-between;'>
    <div>क्रमांक :- एफ 10(17) प्राशिनि / ई-1 / सी-1 / {dt1.Rows[0]["YearName"]} / .... </div>
    <div style='white-space: nowrap;'>दिनांक :- .....</div>
</div>

<div>
    प्रतिलिपि निम्नलिखित को सूचनार्थ एवं आवश्यक कार्यवाही हेतु प्रेषित है :-
</div>");


                //5.
                sb.Append($"<ol style=\"list-style-type: decimal;margin-left: 20px; \">             <li style=\"margin-bottom: 10px;\">निजी सचिव, शासन सचिव, तकनीकी शिक्षा विभाग, शासन सचिवालय, जयपुर।</li>             <li style=\"margin-bottom: 10px;\">संयुक्त शासन सचिव, तकनीकी शिक्षा विभाग, शासन सचिवालय, जयपुर को उनके पत्रांक                 एफ 8 (17) त.शि./ {dt1.Rows[0]["HTERulesYear"]} पार्ट दिनांक {dt1.Rows[0]["CurrentDate"]} की अनुपालना में।</li>             <li style=\"margin-bottom: 10px;\">प्रधानाचार्य, राजकीय पॉलिटेक्निक महाविद्यालय - झालावाड़/ कोटा/                 भीलवाडा/डूंगरपुर/झुन्झुनू/श्रीगंगानगर/अलवर/हनुमानगढ़/नागौर/उदयपुर/ अजमेर/सवाईमाधोपुर/सिरोही/मण्डोर                 (कैम्प जोधपुर)/पाली/बून्दी/करौली (कैम्प अवलर)/ जोधपुर/ भरतपुर/ बांसवाड़ा/ दौसा/ बीकानेर/ धौलपुर/ खेतान                 जयपुर/ सीकर</li>             <li style=\"margin-bottom: 10px;\">प्रधानाचार्य, राजकीय महिला पॉलिटेक्निक महाविद्यालय, जोधपुर।</li>             <li style=\"margin-bottom: 10px;\">संबंधित उपरोक्त कार्मिक द्वारा प्रधानाचार्य।</li>             <li style=\"margin-bottom: 10px;\">निजी/ रक्षित पत्रावली -संस्था स्तर।</li>         </ol>");

                //6.
                sb.Append("<div style=\"display: flex;justify-content: flex-end; width: 100%;\">             <div style=\"text-align: right; margin-top: 40px; margin-bottom: 20px; width:250px; float:right;\">                 (आलोक बंसल)<br>                 निदेशक, तकनीकी शिक्षा             </div>         </div>");

                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                return sb;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating HTML", ex);
            }
        }
        #endregion

        #region Theory Marks Reports
        public async Task<StringBuilder> TheoryMarksReports_GetHtml(DataSet ds, int? IsReval)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable dt = ds.Tables[0];

                // GROUPING (Better than Distinct + Where)
                var groupedData = dt.AsEnumerable()
                                        .GroupBy(row => new
                                        {
                                            ExaminerCode = row["ExaminerCode"],
                                            GroupCode = row["GroupCode"],
                                            BranchName = row["BranchName"],
                                            SubjectName = row["SubjectName"],
                                            //   CenterCode = row["CenterCode"],
                                            SubjectCode = row["SubjectCode"],
                                            MaximumMarks = row["MAXIMUM_MARKS"], // FIXED
                                            ExaminerName = row["ExaminerName"],
                                            MobileNo = row["MobileNo"],
                                            Designation = row["Designation"],
                                            SessionName = row["SessionName"]
                                        })
                                        .OrderBy(g => g.Key.ExaminerCode)
                                        .ThenBy(g => g.Key.GroupCode)
                                        .ThenBy(g => g.Key.BranchName)
                                        .ThenBy(g => g.Min(r => r["CenterCode"].ToString()))
                                        .ThenBy(g => g.Key.SubjectName)
                                        .ThenBy(g => g.Key.SubjectCode)
                                        .ThenBy(g => g.Key.MaximumMarks)
                                        .ThenBy(g => g.Key.ExaminerName)
                                        .ThenBy(g => g.Key.MobileNo)
                                        .ThenBy(g => g.Key.Designation)
                                        .ThenBy(g => g.Key.SessionName)
                                        .ToList();

                int sno = 1; // by group code and cccode
                string _snoKeyCodeDiff = "";
                string _snoKeyCodeOrg = "";

                // grouped data loop
                foreach (var group in groupedData)
                {
                    var header = group.Key;

                    _snoKeyCodeOrg = $"{header.GroupCode}-{header.BranchName}";
                    // group code different then reset
                    if (_snoKeyCodeOrg != _snoKeyCodeDiff)
                    {
                        sno = 1;
                    }
                    _snoKeyCodeDiff = _snoKeyCodeOrg;

                    // pagging
                    int pageSize = 28;
                    int totalRecords = group.Count();
                    int pageCount = (int)Math.Ceiling((double)totalRecords / pageSize);

                    var orderedData = group
                        .OrderBy(x => x["CenterCode"])
                        //   .OrderBy(x => x["RollNo"])
                        .ToList();

                    var revaltext = IsReval == 1 ? "(Revaluation) " : "";

                    // pagged data loop 
                    for (int page = 0; page < pageCount; page++)
                    {
                        var pageData = orderedData
                            .Skip(page * pageSize)
                            .Take(pageSize);

                        sb.Append(@"<!DOCTYPE html>
                        <html lang='en'>
                        <head>
                        <meta charset='UTF-8'>");
                        sb.Append($"<title>Theory Marks Report {revaltext}</title>");
                        sb.Append(@"<style>
                        .page-break { page-break-after: always; }
                        </style>
                        </head>
                        <body style='font-family: Arial, sans-serif; margin: 20px; color: #000; line-height: 1.0;'>");

                        // repeat header every page
                        sb.Append($"<div style='width: 100%; max-width: 90%; margin: 0 auto; border: 1px solid #000; padding: 20px;'>");

                        // Header
                        sb.Append($"<div style='text-align:center; font-weight:bold; font-size:18px; margin-bottom:20px;'>THEORY MARKS {revaltext} {header.SessionName}</div>");

                        sb.Append($@"<table style='width: 100%; border-collapse: collapse; font-size: 15px;'>
                            <tr>
                            <td style='display: flex; justify-content: space-between; margin-bottom: 10px;>
                            <div style='width: 45%;'>                           
                            <div style='text-decoration: underline; font-weight: bold; font-size: 16px; margin-bottom: 5px;'>Theory Exam Reports</div>
                            <div>Branch : <b>{header.BranchName}</b></div>
                            <div>Examiner Code : <b>{header.ExaminerCode}</b></div>
                            <div>Group Code : <b>{header.GroupCode}</b></div>
                            </div>
                            </td>
                            <td style='display: flex; justify-content: space-between; margin-bottom: 10px;>
                            <div style='width: 45%; float: right;'>
                           
                            <div>Subject : <b>{header.SubjectName}</b></div>
                            <div>Subject Code : <b>{header.SubjectCode}</b></div>
                            <div>Maximum Marks : <b>{header.MaximumMarks}</b></div>
                            </div>
                            </td>
                            </tr>
                            </table>");

                        // TABLE START (Only once per group)
                        sb.Append(@"
                            <table style='width: 100%; border-collapse: collapse; margin-top: 15px; margin-bottom: 50px; text-align: center; font-size: 14px;'>
                            <thead>
                            <tr>
                            <th style='border: 1px solid #ccc; padding: 5px; width: 40px;'>S.No</th>
                            <th style='border: 1px solid #ccc; padding: 5px; width: 150px;'>Center Code</th>
                            <th style='border: 1px solid #ccc; padding: 5px; width: 150px;'>Roll No</th>
                            <th colspan='2' style='border: 1px solid #ccc; padding: 5px;'>MARKS OBTAINED</th>
                            </tr>
                            <tr>
                            <th style='border: 1px solid #ccc; padding: 5px;'></th>
                            <th style='border: 1px solid #ccc; padding: 5px;'></th>
                            <th style='border: 1px solid #ccc; padding: 5px; width: 50%;'></th>
                            <th style='border: 1px solid #ccc; padding: 5px;'>In Words</th>
                            <th style='border: 1px solid #ccc; padding: 5px;'>In Fig.</th>
                            </tr>
                            </thead>
                            <tbody>");

                        // table data loop
                        foreach (var row in pageData)
                        {
                            sb.Append($@"
                                    <tr>
                                        <td style='border:1px solid #ccc; padding:8px;'>{sno++}</td>
                                        <td style='border:1px solid #ccc; padding:8px;'>{row["CenterCode"]}</td>
                                        <td style='border:1px solid #ccc; padding:8px;'>{row["RollNo"]}</td>
                                        <td style='border:1px solid #ccc; padding:8px;'>{row["ObtainedTheoryInword"]}</td>
                                        <td style='border:1px solid #ccc; padding:8px;'>{row["ObtainedTheory"]}</td>
                                    </tr>");
                        }

                        sb.Append("</tbody></table>");

                        // Footer
                        sb.Append($@"
                            <div style='border: 1px solid #ccc; padding: 10px; font-size: 13px;'>
                             <p style='margin: 0 0 10px 0;'>
                                I have gone through all the examiner instructions & I certify that I have followed them. Also, the
                                answer books are accessed by me as per direction of BTER, Jodhpur.
                            </p>
                            <div style='display:flex; justify-content:space-between;'>
                            <div>
                            <div>Name: {header.ExaminerName}</div>
                            <div>Post: {header.Designation}</div>
                            <div>Mobile No: {header.MobileNo}</div>
                            </div>
                            <div style='width: 300px; padding-top: 20px;'>
                            <div style='margin-bottom: 15px;'>Date: _____________</div>
                            <div>Signature: _____________</div>
                            </div>
                            </div>
                            </div>");

                        sb.Append("<div class='page-break'></div>");

                        sb.Append("</div>");

                        sb.Append("</body></html>");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error generating HTML", ex);
            }

            return sb;
        }
        #endregion

        #region UFM category Reports
        //public async Task<StringBuilder> UFMCategoryReportPdf_BTER_GetHtml(DataSet ds)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        DataTable dt = ds.Tables[0];

        //        // GROUPING (Better than Distinct + Where)
        //        var groupedData = dt.AsEnumerable()
        //                                .GroupBy(row => new
        //                                {
        //                                    StudentID = row["StudentID"],
        //                                    UFMCategory = row["UFMCategory"],
        //                                    UFMCategoryName = row["UFMCategoryName"],
        //                                    RollNo = row["RollNo"]
        //                                    //CenterCode = row["CenterCode"],
        //                                    //BranchName = row["BranchName"],
        //                                    //SubjectName = row["SubjectName"],
        //                                    //SubjectCode = row["SubjectCode"],
        //                                    //MaximumMarks = row["MAXIMUM_MARKS"], // FIXED
        //                                    //ExaminerName = row["ExaminerName"],
        //                                    //MobileNo = row["MobileNo"],
        //                                    //Designation = row["Designation"],
        //                                    //SessionName = row["SessionName"]
        //                                })
        //                                .OrderBy(g => g.Key.UFMCategory)
        //                                .ThenBy(g => g.Key.RollNo)
        //                                //.ThenBy(g => g.Key.CenterCode)
        //                                //.ThenBy(g => g.Key.BranchName)
        //                                //.ThenBy(g => g.Key.SubjectName)
        //                                //.ThenBy(g => g.Key.SubjectCode)
        //                                //.ThenBy(g => g.Key.MaximumMarks)
        //                                //.ThenBy(g => g.Key.ExaminerName)
        //                                //.ThenBy(g => g.Key.MobileNo)
        //                                //.ThenBy(g => g.Key.Designation)
        //                                //.ThenBy(g => g.Key.SessionName)
        //                                .ToList();

        //        int sno = 1; // by group code and cccode
        //        string _snoKeyCodeDiff = "";
        //        string _snoKeyCodeOrg = "";

        //        // grouped data loop
        //        foreach (var group in groupedData)
        //        {
        //            var header = group.Key;

        //            //_snoKeyCodeOrg = $"{header.GroupCode}-{header.CenterCode}";
        //            // group code different then reset
        //            if (_snoKeyCodeOrg != _snoKeyCodeDiff)
        //            {
        //                sno = 1;
        //            }
        //            _snoKeyCodeDiff = _snoKeyCodeOrg;

        //            // pagging
        //            int pageSize = 20;
        //            int totalRecords = group.Count();
        //            int pageCount = (int)Math.Ceiling((double)totalRecords / pageSize);

        //            var orderedData = group
        //                .OrderBy(x => x["RollNo"])
        //                .ToList();

        //            //var revaltext = IsReval == 1 ? "(Revaluation) " : "";
        //            var revaltext ="(Revaluation) ";

        //            // pagged data loop 
        //            for (int page = 0; page < pageCount; page++)
        //            {
        //                var pageData = orderedData
        //                    .Skip(page * pageSize)
        //                    .Take(pageSize);

        //                sb.Append(@"<!DOCTYPE html>
        //                <html lang='en'>
        //                <head>
        //                <meta charset='UTF-8'>");
        //                sb.Append($"<title>Theory Marks Report {revaltext}</title>");
        //                sb.Append(@"<style>
        //                .page-break { page-break-after: always; }
        //                </style>
        //                </head>
        //                <body style='font-family: Arial, sans-serif; margin: 20px; color: #000; line-height: 1.0;'>");

        //                // repeat header every page
        //                sb.Append($"<div style='width: 100%; max-width: 90%; margin: 0 auto; border: 1px solid #000; padding: 20px;'>");

        //                // Header
        //                sb.Append($"<div style='text-align:center; font-weight:bold; font-size:18px; margin-bottom:20px;'>THEORY MARKS {revaltext} {header.StudentID}</div>");

        //                sb.Append($@"<table style='width: 100%; border-collapse: collapse; font-size: 15px;'>
        //                    <tr>
        //                    <td style='display: flex; justify-content: space-between; margin-bottom: 10px;>
        //                    <div style='width: 45%;'>                           
        //                    <div style='text-decoration: underline; font-weight: bold; font-size: 16px; margin-bottom: 5px;'>Theory Exam Reports</div>
        //                    <div>Branch : <b>{header.StudentID}</b></div>
        //                    <div>Examiner Code : <b>{header.StudentID}</b></div>
        //                    <div>Group Code : <b>{header.StudentID}</b></div>
        //                    </div>
        //                    </td>
        //                    <td style='display: flex; justify-content: space-between; margin-bottom: 10px;>
        //                    <div style='width: 45%; float: right;'>
        //                    <div>CC Code : <b>{header.StudentID}</b></div>
        //                    <div>Subject : <b>{header.StudentID}</b></div>
        //                    <div>Subject Code : <b>{header.StudentID}</b></div>
        //                    <div>Maximum Marks : <b>{header.StudentID}</b></div>
        //                    </div>
        //                    </td>
        //                    </tr>
        //                    </table>");

        //                // TABLE START (Only once per group)
        //                sb.Append(@"
        //                    <table style='width: 100%; border-collapse: collapse; margin-top: 15px; margin-bottom: 50px; text-align: center; font-size: 14px;'>
        //                    <thead>
        //                    <tr>
        //                    <th style='border: 1px solid #ccc; padding: 5px; width: 40px;'>S.No</th>
        //                    <th style='border: 1px solid #ccc; padding: 5px; width: 150px;'>Roll No</th>
        //                    <th colspan='2' style='border: 1px solid #ccc; padding: 5px;'>MARKS OBTAINED</th>
        //                    </tr>
        //                    <tr>
        //                    <th style='border: 1px solid #ccc; padding: 5px;'></th>
        //                    <th style='border: 1px solid #ccc; padding: 5px;'></th>
        //                    <th style='border: 1px solid #ccc; padding: 5px; width: 50%;'>In Words</th>
        //                    <th style='border: 1px solid #ccc; padding: 5px;'>In Fig.</th>
        //                    </tr>
        //                    </thead>
        //                    <tbody>");

        //                // table data loop
        //                foreach (var row in pageData)
        //                {
        //                    sb.Append($@"
        //                            <tr>
        //                                <td style='border:1px solid #ccc; padding:8px;'>{sno++}</td>
        //                                <td style='border:1px solid #ccc; padding:8px;'>{row["RollNo"]}</td>
        //                                <td style='border:1px solid #ccc; padding:8px;'>{row["ObtainedTheoryInword"]}</td>
        //                                <td style='border:1px solid #ccc; padding:8px;'>{row["ObtainedTheory"]}</td>
        //                            </tr>");
        //                }

        //                sb.Append("</tbody></table>");

        //                // Footer
        //                sb.Append($@"
        //                    <div style='border: 1px solid #ccc; padding: 10px; font-size: 13px;'>
        //                     <p style='margin: 0 0 10px 0;'>
        //                        I have gone through all the examiner instructions & I certify that I have followed them. Also, the
        //                        answer books are accessed by me as per direction of BTER, Jodhpur.
        //                    </p>
        //                    <div style='display:flex; justify-content:space-between;'>
        //                    <div>
        //                    <div>Name: {header.StudentID}</div>
        //                    <div>Post: {header.StudentID}</div>
        //                    <div>Mobile No: {header.StudentID}</div>
        //                    </div>
        //                    <div style='width: 300px; padding-top: 20px;'>
        //                    <div style='margin-bottom: 15px;'>Date: _____________</div>
        //                    <div>Signature: _____________</div>
        //                    </div>
        //                    </div>
        //                    </div>");

        //                sb.Append("<div class='page-break'></div>");

        //                sb.Append("</div>");

        //                sb.Append("</body></html>");
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error generating HTML", ex);
        //    }

        //    return sb;
        //}

        //public async Task<StringBuilder> UFMCategoryReportPdf_BTER_GetHtml(DataSet ds)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        DataTable dt = ds.Tables[0];

        //        //string financialyear = "";
        //        //if (dt.Rows.Count > 0)
        //        //{
        //        //    financialyear = Convert.ToString(dt.Rows[0]["FinancialYearName"]);
        //        //}

        //       // string financialYear = dt.AsEnumerable().Select(x => Convert.ToString(x["FinancialYearName"])).FirstOrDefault();
        //        string financialYear = dt.AsEnumerable()
        //        .Select(x => Convert.ToString(x["FinancialYearName"]))
        //        .FirstOrDefault() ?? "";

        //        var groupedData = dt.AsEnumerable()
        //            .GroupBy(x => new
        //            {
        //                UFMCategory = x["UFMCategory"].ToString(),
        //                UFMCategoryName = x["UFMCategoryName"].ToString()
        //            })
        //            .OrderBy(x => Convert.ToInt32(x.Key.UFMCategory))
        //            .ToList();

        //        sb.Append(@"
        //            <!DOCTYPE html>
        //            <html>
        //            <head>
        //                <meta charset='utf-8'>
        //                <style>
        //                    body{
        //                        font-family:'Nirmala UI','Mangal','Noto Sans Devanagari', 'Mangal', 'Arial Unicode MS', 'sans-serif','Arial';
        //                        font-size:14px;
        //                        margin:30px;
        //                        line-height:1.7;
        //                    }

        //                    .header{
        //                        text-align:center;
        //                        font-weight:bold;
        //                        font-size:18px;
        //                    }

        //                    .office-order{
        //                        text-align:center;
        //                        font-weight:bold;
        //                        font-size:20px;
        //                        margin-top:20px;
        //                        margin-bottom:20px;
        //                    }

        //                    .roll-table{
        //                        width:100%;
        //                        border-collapse:collapse;
        //                        margin-top:10px;
        //                        margin-bottom:20px;
        //                    }

        //                    .roll-table td{
        //                        padding:6px;
        //                        text-align:center;
        //                        font-weight:bold;
        //                        width:25%;
        //                    }

        //                    .signature{
        //                        text-align:right;
        //                        margin-top:40px;
        //                        font-weight:bold;
        //                    }
        //                </style>
        //            </head>
        //            <body>");

        //        // Header
        //        sb.Append(@"
        //                    <div class='header'>
        //                        राजस्थान सरकार<br/>
        //                        प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर
        //                    </div>

        //                    <div class='office-order'>
        //                        कार्यालय आदेश
        //                    </div>");

        //        // Category Wise Data
        //        foreach (var group in groupedData)
        //        {
        //            sb.Append($@"
        //                    <div style='margin-top:20px; text-align:justify;'>
        //                        सत्र {financialYear} के अनुचित साधन के मामलों की समिति द्वारा लिये गये
        //                        निर्णयानुसार निम्नांकित परीक्षार्थियों को दण्ड सारणी श्रेणी
        //                        <b>{group.Key.UFMCategoryName}</b>
        //                        के अन्तर्गत दण्डित किया जाता है :-
        //                    </div>");

        //            sb.Append("<table class='roll-table'>");

        //            int count = 0;

        //            foreach (var row in group)
        //            {
        //                if (count % 4 == 0)
        //                {
        //                    sb.Append("<tr>");
        //                }

        //                sb.Append($@"
        //                <td>
        //                    {row["RollNo"]}
        //                </td>");

        //                count++;

        //                if (count % 4 == 0)
        //                {
        //                    sb.Append("</tr>");
        //                }
        //            }

        //            if (count % 4 != 0)
        //            {
        //                sb.Append("</tr>");
        //            }

        //            sb.Append("</table>");
        //        }

        //        // Signature
        //        sb.Append(@"
        //            <div class='signature'>
        //                <br/><br/>
        //                (रघुनाथ सिंह)<br/>
        //                संयुक्त निदेशक (गोपनीय)
        //            </div>

        //            </body>
        //            </html>");

        //        return sb;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error generating UFM Officer Order HTML", ex);
        //    }
        //}


        //public async Task<StringBuilder> UFMCategoryReportPdf_BTER_GetHtml(DataSet ds)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        DataTable dt = ds.Tables[0];

        //        string financialYear = dt.AsEnumerable()
        //            .Select(x => Convert.ToString(x["FinancialYearName"]))
        //            .FirstOrDefault() ?? "";

        //        var groupedData = dt.AsEnumerable()
        //            .GroupBy(x => new
        //            {
        //                UFMCategory = x["UFMCategory"].ToString(),
        //                UFMCategoryName = x["UFMCategoryName"].ToString()
        //            })
        //            .OrderBy(x => Convert.ToInt32(x.Key.UFMCategory))
        //            .ToList();

        //        sb.Append(@"
        //        <!DOCTYPE html>
        //        <html>
        //        <head>
        //            <meta charset='utf-8'>
        //            <style>
        //                body{
        //                    font-family:'Nirmala UI','Mangal','Noto Sans Devanagari','Arial Unicode MS',sans-serif;
        //                    font-size:14px;
        //                    margin:20px;
        //                    line-height:1.6;
        //                    color:#000;
        //                }

        //                .header{
        //                    text-align:center;
        //                    font-weight:bold;
        //                    font-size:18px;
        //                }

        //                .office-order{
        //                    text-align:center;
        //                    font-weight:bold;
        //                    font-size:18px;
        //                    margin-top:20px;
        //                    margin-bottom:20px;
        //                }

        //                .roll-table{
        //                    width:100%;
        //                    border-collapse:collapse;
        //                    margin-top:10px;
        //                    margin-bottom:15px;
        //                }

        //                .roll-table td{
        //                    width:20%;
        //                    text-align:center;
        //                    padding:6px;
        //                    font-weight:bold;
        //                    font-size:15px;
        //                }

        //                .signature{
        //                    text-align:right;
        //                    margin-top:30px;
        //                    font-weight:bold;
        //                }

        //                .copy-section{
        //                    margin-top:30px;
        //                    line-height:2;
        //                }
        //            </style>
        //        </head>
        //        <body>");

        //        // Header
        //        sb.Append(@"
        //        <div class='header'>
        //            राजस्थान सरकार
        //        </div>

        //        <div class='header' style='margin-top:5px;'>
        //            प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर
        //        </div>

        //        <table style='width:100%;margin-top:15px;'>
        //            <tr>
        //                <td style='text-align:left;font-weight:bold;'>
        //                    क्रमांकः एफ(12) प्राशिम/गोप./2026/
        //                </td>

        //                <td style='text-align:right;font-weight:bold;'>
        //                    दिनांकः
        //                </td>
        //            </tr>
        //        </table>

        //        <div class='office-order'>
        //            कार्यालय आदेश
        //        </div>");

        //        // Category-wise content
        //        foreach (var group in groupedData)
        //        {
        //            sb.Append($@"
        //            <div style='text-align:justify; margin-top:15px;'>
        //                सत्र {financialYear} के अनुचित साधन के मामलों की समिति द्वारा लिये गये
        //                निर्णयानुसार निम्नांकित परीक्षार्थियों को मण्डल की दण्ड सारणी की
        //                श्रेणी <b>{group.Key.UFMCategoryName}</b> के अन्तर्गत दण्डित किया जाता है :-
        //            </div>");

        //            sb.Append("<table class='roll-table'>");

        //            int count = 0;

        //            foreach (var row in group)
        //            {
        //                if (count % 5 == 0)
        //                {
        //                    sb.Append("<tr>");
        //                }

        //                sb.Append($@"
        //                    <td>{row["RollNo"]}</td>");

        //                count++;

        //                if (count % 5 == 0)
        //                {
        //                    sb.Append("</tr>");
        //                }
        //            }

        //            if (count % 5 != 0)
        //            {
        //                while (count % 5 != 0)
        //                {
        //                    sb.Append("<td></td>");
        //                    count++;
        //                }

        //                sb.Append("</tr>");
        //            }

        //            sb.Append("</table>");

        //            sb.Append($@"
        //            <div style='margin-top:10px;margin-bottom:20px;text-align:justify;'>
        //                ""उपरोक्त रोल नम्बर के परीक्षार्थियों की सत्र {financialYear}
        //                में दी गई डिप्लोमा इंजीनियरिंग की समस्त सैद्धान्तिक एवं
        //                प्रायोगिक विषयों की परीक्षाएं (जिसमें छात्र बैठा)
        //                निरस्त की जाती हैं।""
        //            </div>");
        //                }

        //        // Signature
        //        sb.Append(@"
        //        <div class='signature'>
        //            (रघुनाथ सिंह)<br/>
        //            संयुक्त निदेशक (गोपनीय)
        //        </div>");

        //        // Copy section
        //        sb.Append(@"
        //        <div class='copy-section'>
        //            <div><b>प्रतिलिपिः</b></div>

        //            <div>01. निदेशक एवं अध्यक्ष, प्रा.शि.मं. जोधपुर</div>
        //            <div>02. संयुक्त निदेशक (रजिस्ट्रार), प्रा.शि.मं. जोधपुर</div>
        //            <div>03. प्रभारी, कम्प्यूटर, परीक्षा प्रा.शि.मं. जोधपुर</div>
        //            <div>04. सम्बन्धित संस्थान</div>
        //        </div>

        //        <div class='signature'>
        //            (रघुनाथ सिंह)<br/>
        //            संयुक्त निदेशक (गोपनीय)
        //        </div>

        //        </body>
        //        </html>");

        //        return sb;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error generating UFM Officer Order HTML", ex);
        //    }
        //}


        //----------------------------------------------------------------------------------------------------
        //public async Task<StringBuilder> UFMCategoryReportPdf_BTER_GetHtml(DataSet ds)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        DataTable dt = ds.Tables[0];

        //        string financialYear = dt.AsEnumerable()
        //            .Select(x => Convert.ToString(x["FinancialYearName"]))
        //            .FirstOrDefault() ?? "";


        //        string EndTermName = dt.AsEnumerable()
        //            .Select(x => Convert.ToString(x["EndTermName"]))
        //            .FirstOrDefault() ?? "";

        //        var groupedData = dt.AsEnumerable()
        //            .GroupBy(x => new
        //            {
        //                UFMCategory = x["UFMCategory"].ToString(),
        //                UFMCategoryName = x["UFMCategoryName"].ToString(),
        //                CodeID = x["CodeID"].ToString(),
        //                ShortCode = x["ShortCode"].ToString(),
        //                SemesterID = x["SemesterID"].ToString()

        //            })
        //            .OrderBy(x => Convert.ToInt32(x.Key.UFMCategory))
        //            .ThenBy(x=> Convert.ToInt32(x.Key.SemesterID))
        //            .ToList();

        //        sb.AppendLine("<!DOCTYPE html>");
        //        sb.AppendLine("<html lang='hi'>");
        //        sb.AppendLine("<head>");
        //        sb.AppendLine("<meta charset='UTF-8' />");
        //        sb.AppendLine("<title>UFM Office Order</title>");

        //        sb.AppendLine("<style>");
        //        //'Nirmala UI','Mangal','Noto Sans Devanagari';
        //        sb.AppendLine("@font-face {");
        //        sb.AppendLine("font-family: 'Mangal','Noto Sans Devanagari';");
        //        sb.AppendLine($"src: local('Noto Sans Devanagari'), url('{ConfigurationHelper.FontPath_Noto_Sans_Devanagari}') format('truetype');");
        //        sb.AppendLine("}");

        //        sb.AppendLine("body {");
        //        sb.AppendLine("font-family: Arial, sans-serif;");
        //        sb.AppendLine("font-size: 14pt;");
        //        sb.AppendLine("line-height: 1.6;");
        //        sb.AppendLine("color: #000;");
        //        sb.AppendLine("margin: 30px;");
        //        sb.AppendLine("}");

        //        sb.AppendLine(".hindi {");
        //        sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
        //        sb.AppendLine("}");

        //        sb.AppendLine(".header {");
        //        sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
        //        sb.AppendLine("text-align:center;");
        //        sb.AppendLine("font-weight:bold;");
        //        sb.AppendLine("font-size:18pt;");
        //        sb.AppendLine("}");

        //        sb.AppendLine(".office-order {");
        //        sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
        //        sb.AppendLine("text-align:center;");
        //        sb.AppendLine("font-weight:bold;");
        //        sb.AppendLine("font-size:18pt;");
        //        sb.AppendLine("margin-top:20px;");
        //        sb.AppendLine("margin-bottom:20px;");
        //        sb.AppendLine("}");

        //        sb.AppendLine(".roll-table {");
        //        sb.AppendLine("width:100%;");
        //        sb.AppendLine("border-collapse:collapse;");
        //        sb.AppendLine("margin-top:10px;");
        //        sb.AppendLine("margin-bottom:15px;");
        //        sb.AppendLine("}");

        //        sb.AppendLine(".roll-table td {");
        //        sb.AppendLine("width:20%;");
        //        sb.AppendLine("text-align:center;");
        //        sb.AppendLine("padding:5px;");
        //        sb.AppendLine("font-weight:bold;");
        //        sb.AppendLine("font-size:11pt;");
        //        sb.AppendLine("}");

        //        sb.AppendLine(".signature {");
        //        sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
        //        sb.AppendLine("text-align:right;");
        //        sb.AppendLine("margin-top:25px;");
        //        sb.AppendLine("font-weight:bold;");
        //        sb.AppendLine("}");

        //        sb.AppendLine(".copy-section {");
        //        sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
        //        sb.AppendLine("margin-top:25px;");
        //        sb.AppendLine("line-height:1.8;");
        //        sb.AppendLine("}");

        //        sb.AppendLine("</style>");
        //        sb.AppendLine("</head>");
        //        sb.AppendLine("<body>");

        //        // Header
        //        sb.AppendLine("<div class='header'>राजस्थान सरकार</div>");
        //        sb.AppendLine("<div class='header'>प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर</div>");

        //        sb.AppendLine("<table style='width:100%;margin-top:15px;'>");
        //        sb.AppendLine("<tr>");
        //        sb.AppendLine("<td class='hindi' style='text-align:left;font-weight:bold;'>");
        //        sb.AppendLine("क्रमांकः एफ(12) प्राशिम/गोप./2026/");
        //        sb.AppendLine("</td>");
        //        sb.AppendLine("<td class='hindi' style='text-align:right;font-weight:bold;'>");
        //        sb.AppendLine($"दिनांकः");
        //        sb.AppendLine("</td>");
        //        sb.AppendLine("</tr>");
        //        sb.AppendLine("</table>");

        //        //{ DateTime.Now:dd / MM / yyyy}

        //        sb.AppendLine("<div class='office-order'>कार्यालय आदेश</div>");

        //        // Category Wise Data
        //        foreach (var group in groupedData)
        //        {
        //            //सत्र 2024 - 2025(छठे सेमेस्टर, नवंबर 2024) के अनुचित साधनों के मामलों पर गठित समिति ने 09 / 10 / 2025 को 
        //            //    आयोजित अपनी बैठक में संपूर्ण रिकॉर्ड का अवलोकन, अध्ययन और विचार - विमर्श करने के बाद 
        //            //    निम्नलिखित रोल नंबर वाले छात्रों को बोर्ड की दंड अनुसूची की धारा 2(दो) के तहत दंडित करने का निर्णय लिया है

        //            sb.AppendLine($@"
        //    <div class='hindi' style='text-align:justify;margin-top:15px;'>
        //        सत्र {financialYear} ({group.Key.ShortCode} सेमेस्टर, {EndTermName}) के अनुचित साधन के मामलों पर गठित समिति ने {DateTime.Now:dd/MM/yyyy} को 
        //                आयोजित अपनी बैठक में संपूर्ण रिकॉर्ड का अवलोकन,अध्ययन और विचार - विमर्श करने के बाद 
        //                निम्नलिखित रोल नंबर वाले छात्रों को बोर्ड की दंड अनुसूची की धारा <b>{group.Key.CodeID}</b>  की समिति द्वारा लिये गये
        //        निर्णयानुसार निम्नांकित परीक्षार्थियों को दण्ड सारणी श्रेणी
        //        <b>{group.Key.UFMCategoryName}</b>
        //        के अन्तर्गत दण्डित किया जाता है :-
        //    </div>");

        //            sb.AppendLine("<table class='roll-table'>");

        //            int count = 0;

        //            foreach (var row in group)
        //            {
        //                if (count % 5 == 0)
        //                {
        //                    sb.AppendLine("<tr>");
        //                }

        //                sb.AppendLine($@"<td>{row["RollNo"]}</td>");

        //                count++;

        //                if (count % 5 == 0)
        //                {
        //                    sb.AppendLine("</tr>");
        //                }
        //            }

        //            if (count % 5 != 0)
        //            {
        //                while (count % 5 != 0)
        //                {
        //                    sb.AppendLine("<td></td>");
        //                    count++;
        //                }

        //                sb.AppendLine("</tr>");
        //            }

        //            sb.AppendLine("</table>");

        //        }

        //        sb.AppendLine($@"
        //    <div class='hindi' style='text-align:justify;margin-bottom:25px;'>
        //        उपरोक्त रोल नम्बर के परीक्षार्थियों की सत्र {EndTermName}
        //        में आयोजित परीक्षा की समस्त सैद्धान्तिक एवं प्रायोगिक विषयों की
        //        परीक्षाएं (जिसमें छात्र बैठा) निरस्त की जाती हैं।
        //    </div>");

        //        // Signature
        //        sb.AppendLine("<div class='signature'>");
        //        //sb.AppendLine("(रघुनाथ सिंह)<br/>");
        //        sb.AppendLine("संयुक्त निदेशक (गोपनीय)");
        //        sb.AppendLine("</div>");

        //        // Copy Section
        //        sb.AppendLine("<div class='copy-section'>");
        //        sb.AppendLine("<br/><br/>");
        //        sb.AppendLine("<b>प्रतिलिपिः</b><br/>");
        //        sb.AppendLine("01. निदेशक एवं अध्यक्ष, प्रा.शि.मं. जोधपुर<br/>");
        //        sb.AppendLine("02. संयुक्त निदेशक (रजिस्ट्रार), प्रा.शि.मं. जोधपुर<br/>");
        //        sb.AppendLine("03. प्रभारी, कम्प्यूटर, परीक्षा प्रा.शि.मं. जोधपुर<br/>");
        //        sb.AppendLine("04. सम्बन्धित संस्थान");
        //        sb.AppendLine("</div>");

        //        sb.AppendLine("<div class='signature'>");
        //        sb.AppendLine("(रघुनाथ सिंह)<br/>");
        //        sb.AppendLine("संयुक्त निदेशक (गोपनीय)");
        //        sb.AppendLine("</div>");

        //        sb.AppendLine("</body>");
        //        sb.AppendLine("</html>");

        //        return sb;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error generating UFM Category Report HTML", ex);
        //    }
        //}


        public async Task<StringBuilder> UFMCategoryReportPdf_BTER_GetHtml(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable dt = ds.Tables[0];

                string financialYear = dt.AsEnumerable()
                    .Select(x => Convert.ToString(x["FinancialYearName"]))
                    .FirstOrDefault() ?? "";

                string FinancialYearName = dt.AsEnumerable()
                    .Select(x => Convert.ToString(x["FinancialYearName"]))
                    .FirstOrDefault() ?? "";


                string EndTermName = dt.AsEnumerable()
                    .Select(x => Convert.ToString(x["EndTermName"]))
                    .FirstOrDefault() ?? "";

                string YearName = dt.AsEnumerable()
                    .Select(x => Convert.ToString(x["YearName"]))
                    .FirstOrDefault() ?? "";

                string TermNameHindi = dt.AsEnumerable()
                    .Select(x => Convert.ToString(x["TermNameHindi"]))
                    .FirstOrDefault() ?? "";

                string CourseTypeHindiName = dt.AsEnumerable()
                    .Select(x => Convert.ToString(x["CourseTypeHindiName"]))
                    .FirstOrDefault() ?? "";

                var groupedData = dt.AsEnumerable()
                    .GroupBy(x => new
                    {
                        UFMCategory = x["UFMCategory"].ToString(),
                        UFMCategoryName = x["UFMCategoryName"].ToString(),
                        CodeID = x["CodeID"].ToString(),
                        ShortCode = x["ShortCode"].ToString(),
                        SemesterID = x["SemesterID"].ToString(),
                        SemesterNameHindi = x["SemesterNameHindi"].ToString(),
                        TermNameHindi = x["TermNameHindi"].ToString(),
                        // RollNo = x["RollNo"].ToString()

                    })
                    .OrderBy(x => Convert.ToInt32(x.Key.UFMCategory))
                    //.ThenBy(x => Convert.ToInt32(x.Key.RollNo))
                    .ToList();

                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang='hi'>");
                sb.AppendLine("<head>");
                sb.AppendLine("<meta charset='UTF-8' />");
                sb.AppendLine("<title>UFM Office Order</title>");

                sb.AppendLine("<style>");
                //'Nirmala UI','Mangal','Noto Sans Devanagari';
                sb.AppendLine("@font-face {");
                sb.AppendLine("font-family: 'Mangal','Noto Sans Devanagari';");
                sb.AppendLine($"src: local('Noto Sans Devanagari'), url('{ConfigurationHelper.FontPath_Noto_Sans_Devanagari}') format('truetype');");
                sb.AppendLine("font-weight: normal;");
                sb.AppendLine("font-style: normal;");
                sb.AppendLine("}");

                sb.AppendLine("body {");
                sb.AppendLine("font-family: 'Noto Sans Devanagari';");
                sb.AppendLine("font-size: 11pt;");
                sb.AppendLine("font-weight: normal;");
                sb.AppendLine("line-height: 1.4;");
                sb.AppendLine("color: #000;");
                sb.AppendLine("margin: 20px;");
                sb.AppendLine("}");

                sb.AppendLine(".hindi {");
                sb.AppendLine("font-family: 'Noto Sans Devanagari';");
                sb.AppendLine("font-weight: normal;");
                sb.AppendLine("}");

                sb.AppendLine(".header {");
                sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
                sb.AppendLine("text-align:center;");
                sb.AppendLine("font-weight:bold;");
                sb.AppendLine("font-size:13pt;");
                sb.AppendLine("}");

                sb.AppendLine(".office-order {");
                sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
                sb.AppendLine("text-align:center;");
                sb.AppendLine("font-weight:bold;");
                sb.AppendLine("font-size:12pt;");
                sb.AppendLine("margin-top:20px;");
                sb.AppendLine("margin-bottom:20px;");
                sb.AppendLine("}");

                sb.AppendLine(".roll-table {");
                sb.AppendLine("width:100%;");
                sb.AppendLine("border-collapse:collapse;");
                sb.AppendLine("margin-top:10px;");
                sb.AppendLine("margin-bottom:15px;");
                sb.AppendLine("}");

                sb.AppendLine(".roll-table td {");
                sb.AppendLine("width:20%;");
                sb.AppendLine("text-align:center;");
                sb.AppendLine("padding:5px;");
                sb.AppendLine("font-weight:normal;");
                sb.AppendLine("font-size:10pt;");
                sb.AppendLine("}");

                sb.AppendLine(".signature {");
                sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
                sb.AppendLine("width:250px;");
                sb.AppendLine("text-align:center;");   // Center text within the block
                sb.AppendLine("margin-top:25px;");
                sb.AppendLine("font-weight:bold;");
                sb.AppendLine("margin-left:auto;");       // Keep block on left side
                sb.AppendLine("margin-right:0;");
                sb.AppendLine("line-height:1.7;");   // Reduce line spacing
                sb.AppendLine("}");

                sb.AppendLine(".copy-section {");
                sb.AppendLine("font-family: 'Noto Sans Devanagari', serif;");
                sb.AppendLine("margin-top:25px;");
                sb.AppendLine("line-height:1.8;");
                sb.AppendLine("}");

                sb.AppendLine("</style>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");

                // Header
                sb.AppendLine("<div class='header'>राजस्थान सरकार</div>");
                sb.AppendLine("<div class='header'>प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर</div>");

                sb.AppendLine("<table style='width:100%;margin-top:15px;'>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td class='hindi' style='text-align:left;font-weight:bold;'>");
                sb.AppendLine("क्रमांकः एफ(12) प्राशिम/गोप./2026/");
                sb.AppendLine("</td>");
                sb.AppendLine("<td class='hindi' style='text-align:right;padding-right:65px;font-weight:bold;'>");
                sb.AppendLine($"दिनांकः");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");

                //{ DateTime.Now:dd / MM / yyyy}

                sb.AppendLine("<div class='office-order'>कार्यालय आदेश</div>");

                // Category Wise Data
                foreach (var group in groupedData)
                {
                    //सत्र 2024 - 2025(छठे सेमेस्टर, नवंबर 2024) के अनुचित साधनों के मामलों पर गठित समिति ने 09 / 10 / 2025 को 
                    //    आयोजित अपनी बैठक में संपूर्ण रिकॉर्ड का अवलोकन, अध्ययन और विचार - विमर्श करने के बाद 
                    //    निम्नलिखित रोल नंबर वाले छात्रों को बोर्ड की दंड अनुसूची की धारा 2(दो) के तहत दंडित करने का निर्णय लिया है
                    //{ group.Key.CodeID}

                    sb.AppendLine($@"
            <div class='hindi' style='text-align:justify;margin-top:15px;'>
                सत्र {financialYear} (सेमेस्टर पद्धति {group.Key.SemesterNameHindi} {group.Key.TermNameHindi}, {YearName}) के अनुचित साधन के मामलों की समिति ने 
                    {DateTime.Now:dd/MM/yyyy} को हुई बैठक में संपूर्ण रिकॉर्ड के अवलोकन,अध्ययन एवं विचार विमर्श के पश्चात् निम्न रोल नंबर के विद्यार्थियों को मंडल की दण्ड सारणी की
                    श्रेणी {group.Key.UFMCategoryName} के अन्तर्गत दण्डित करने का निर्णय दिया गया है :-
            </div>");

                    sb.AppendLine("<table class='roll-table'>");

                    int count = 0;

                    foreach (var row in group)
                    {
                        if (count % 5 == 0)
                        {
                            sb.AppendLine("<tr>");
                        }

                        sb.AppendLine($@"<td style='font-size:18px;'>{row["RollNo"]}</td>");

                        count++;

                        if (count % 5 == 0)
                        {
                            sb.AppendLine("</tr>");
                        }
                    }

                    if (count % 5 != 0)
                    {
                        while (count % 5 != 0)
                        {
                            sb.AppendLine("<td></td>");
                            count++;
                        }

                        sb.AppendLine("</tr>");
                    }

                    sb.AppendLine("</table>");

                }

                sb.AppendLine($@"
            <div class='hindi' style='text-align:justify;margin-bottom:25px;'>
                उपरोक्त रोल नम्बर के परीक्षार्थियों की सत्र {FinancialYearName}
                में दी गयी डिप्लोमा {CourseTypeHindiName} {TermNameHindi}, {YearName} की समस्त सैद्धान्तिक एवं प्रायोगिक विषयों की
                परीक्षाएं (जिसमें छात्र बैठा) निरस्त की जाती हैं।
            </div>");

                // Signature
                //sb.AppendLine("<div class='signature'>");
                //sb.AppendLine("(रघुनाथ सिंह)<br/>");
                //sb.AppendLine("संयुक्त निदेशक (गोपनीय)<br/><br/>");
                //sb.AppendLine("दिनांक:");
                //sb.AppendLine("</div>");

                // Signature
                sb.AppendLine("<div class='signature'>");
                sb.AppendLine("(रघुनाथ सिंह)<br/>");
                sb.AppendLine("संयुक्त निदेशक (गोपनीय)");
                sb.AppendLine("</div>");

                // Bottom Kramank & Dinank
                sb.AppendLine("<table style='width:100%; margin-top:30px;'>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='text-align:left; font-weight:bold;'>");
                sb.AppendLine("क्रमांक : एफ6(12)प्राशिम/गोप./2026/");
                sb.AppendLine("</td>");
                sb.AppendLine("<td style='text-align:right;padding-right:65px;font-weight:bold;'>");
                sb.AppendLine($"दिनांक :");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");

                // Copy Section
                sb.AppendLine("<div class='copy-section'>");
                //sb.AppendLine("<br/><br/>");
                //sb.AppendLine("<b>क्रमांक : एफ6(12)प्रशिम/गोप./2026/ </b><br/>");
                sb.AppendLine("<b>प्रतिलिपिः</b><br/>");
                sb.AppendLine("01. निदेशक एवं अध्यक्ष, प्रा.शि.मं. जोधपुर<br/>");
                sb.AppendLine("02. संयुक्त निदेशक (रजिस्ट्रार), प्रा.शि.मं. जोधपुर<br/>");
                sb.AppendLine("03. प्रभारी, कम्प्यूटर, परीक्षा प्रा.शि.मं. जोधपुर<br/>");
                sb.AppendLine("04. सम्बन्धित संस्थान");
                sb.AppendLine("</div>");

                sb.AppendLine("<div class='signature'>");
                sb.AppendLine("(रघुनाथ सिंह)<br/>");
                sb.AppendLine("संयुक्त निदेशक (गोपनीय)");
                sb.AppendLine("</div>");

                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                return sb;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating UFM Category Report HTML", ex);
            }
        }


        public async Task<StringBuilder> Collegwise_UFMCategoryReportPdf_BTER_GetHtml(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            DataTable dt = ds.Tables[0];

            string financialYear = dt.Rows.Count > 0
                ? Convert.ToString(dt.Rows[0]["FinancialYearName"])
                : "";

            string SemesterName = dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0]["SemesterName"]) : "";
            string endTermName = dt.Rows.Count > 0
                ? Convert.ToString(dt.Rows[0]["EndTermName"])
                : "";

            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("body{font-family:Arial;font-size:12px;}");
            sb.AppendLine("table{width:100%;border-collapse:collapse;}");
            sb.AppendLine("th,td{border:1px solid #000;padding:5px;text-align:center;}");
            sb.AppendLine(".header{text-align:center;font-weight:bold;font-size:18px;}");
            sb.AppendLine(".subheader{text-align:center;font-weight:bold;font-size:14px;margin-bottom:10px;}");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='header'>राजस्थान सरकार</div>");
            sb.AppendLine("<div class='header'>प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर</div>");
            sb.AppendLine(
                $"<div class='subheader'>{SemesterName} UFM Student Exam. {endTermName} Session {financialYear}</div>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th rowspan='2'>S.No.</th>");
            sb.AppendLine("<th rowspan='2'>Instt. Code</th>");
            sb.AppendLine("<th rowspan='2'>Name</th>");
            sb.AppendLine("<th colspan='2'>Registered</th>");
            sb.AppendLine("<th colspan='2'>Result Declared Cat. 1</th>");
            sb.AppendLine("<th colspan='2'>Result Declared Cat. 2</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Regular</th>");
            sb.AppendLine("<th>Ex.</th>");
            sb.AppendLine("<th>Regular</th>");
            sb.AppendLine("<th>Ex.</th>");
            sb.AppendLine("<th>Regular</th>");
            sb.AppendLine("<th>Ex.</th>");
            sb.AppendLine("</tr>");
            int sno = 1;
            int totalRegRegular = 0;
            int totalRegEx = 0;
            int totalCat1Regular = 0;
            int totalCat1Ex = 0;
            int totalCat2Regular = 0;
            int totalCat2Ex = 0;
            //string totalCat5RollNos = "";

            List<string> totalCat5RollNos = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{sno++}</td>");
                sb.AppendLine($"<td>{row["InstituteCode"]}</td>");
                sb.AppendLine($"<td style='text-align:left'>{row["InstituteName"]}</td>");
                sb.AppendLine($"<td>{row["RegisteredRegular"]}</td>");
                sb.AppendLine($"<td>{row["RegisteredEx"]}</td>");
                sb.AppendLine($"<td>{row["Cat1Regular"]}</td>");
                sb.AppendLine($"<td>{row["Cat1Ex"]}</td>");
                sb.AppendLine($"<td>{row["Cat2Regular"]}</td>");
                sb.AppendLine($"<td>{row["Cat2Ex"]}</td>");
                sb.AppendLine("</tr>");
                totalRegRegular += Convert.ToInt32(row["RegisteredRegular"]);
                totalRegEx += Convert.ToInt32(row["RegisteredEx"]);
                totalCat1Regular += Convert.ToInt32(row["Cat1Regular"]);
                totalCat1Ex += Convert.ToInt32(row["Cat1Ex"]);
                totalCat2Regular += Convert.ToInt32(row["Cat2Regular"]);
                totalCat2Ex += Convert.ToInt32(row["Cat2Ex"]);

                //if (!string.IsNullOrWhiteSpace(row["Cat5RollNos"]?.ToString()))
                //{
                //    if (!string.IsNullOrEmpty(totalCat5RollNos))
                //        totalCat5RollNos += ",";

                //    totalCat5RollNos += row["Cat5RollNos"].ToString();
                //}
                var rollNos = row["Cat5RollNos"]?.ToString();

                if (!string.IsNullOrWhiteSpace(rollNos))
                {
                    totalCat5RollNos.Add(rollNos);
                }

            }
            sb.AppendLine("<tr style='font-weight:bold'>");
            sb.AppendLine("<td colspan='3'>Total</td>");
            sb.AppendLine($"<td>{totalRegRegular}</td>");
            sb.AppendLine($"<td>{totalRegEx}</td>");
            sb.AppendLine($"<td>{totalCat1Regular}</td>");
            sb.AppendLine($"<td>{totalCat1Ex}</td>");
            sb.AppendLine($"<td>{totalCat2Regular}</td>");
            sb.AppendLine($"<td>{totalCat2Ex}</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");

            // Show note only if Cat5RollNos contains data
            if (totalCat5RollNos.Any())
            {
                string rollNos = string.Join(",", totalCat5RollNos);

                sb.AppendLine("<br/>");
                sb.AppendLine("<p style='font-size:15px; margin:0;'>");
                sb.AppendLine("<strong>Note:</strong> Enrollment Cancel due to UFM.");
                sb.AppendLine("</p>");

                sb.AppendLine($@"
                    <div style='
                        font-size:14px;
                        margin-right:10px;
                        margin-top:10px;
                        line-height:1.5;
                        word-break:break-all;
                        overflow-wrap:anywhere;
                        white-space:normal;
                        width:100%;'>
                        Roll No(s) : {rollNos}
                    </div>");
            }


            //sb.AppendLine("<br/>");
            //sb.AppendLine("<p style='font-size:15px;'>");
            //sb.AppendLine("<strong>Note:</strong> Enrollment Cancel due to UFM.");
            //sb.AppendLine("</p>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb;
        }

        #endregion

        #region Student Marksheet public
        public async Task<StringBuilder> StudentResult_Public_GetHtml(DataSet dataSet, int ResultType)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable dtStudent = dataSet.Tables[0];
                DataTable dtSubjects = dataSet.Tables[1];
                DataTable dtSummary = dataSet.Tables[2];

                DataRow result = dtSummary.Rows[0];

                if (dtStudent.Rows.Count == 0)
                    return sb;

                int streamId = Convert.ToInt32(dtStudent.Rows[0]["StreamId"]);
                DataRow student = dtStudent.Rows[0];

                sb.Append(@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    <meta charset='utf-8'>                    
                    <style>                    
                    body{
                        font-family: ""Segoe UI"", Arial, Helvetica, sans-serif;
                        font-size:13px;
                        line-height:1.6;
                        color:#000;
                        letter-spacing:0.2px;
                    }
                    
                    .header{
                        text-align:center;
                        margin-bottom:15px;
                    }
                    
                    .header h2{
                        margin:0;
                        font-size:18px;
                    }
                    
                    .header h3{
                        margin:3px;
                        font-size:16px;
                    }
                    
                    .header p{
                        margin:3px;
                    }
                    
                    table{
                        width:100%;
                        border-collapse:collapse;
                        table-layout:fixed;
                        margin-top:12px;
                    }
                    
                    th{
                        border:1px solid #444;
                        padding:8px 10px;
                        font-size:13px;
                        line-height:1.5;
                        font-weight:bold;
                        vertical-align:middle;
                    }
                    
                    td{
                        border:1px solid #444;
                        padding:7px 8px;
                        font-size:12px;
                        line-height:1.5;
                        vertical-align:middle;
                        word-wrap:break-word;
                        overflow-wrap:break-word;
                    }
                    
                    .info td{
                        border:none;
                        padding:4px;
                    }
                    
                    .subjectTable th{
                        background:#efefef;
                    }
                    
                    .summaryTable th{
                        background:#efefef;
                    }
                    
                    .title{
                        font-weight:bold;
                        margin-top:15px;
                        margin-bottom:5px;
                        font-size:13px;
                    }
                    
                    .footer{
                        margin-top:20px;
                        font-size:11px;
                    }

                    .subjectTable{
                        table-layout:fixed;
                        width:100%;
                    }
                    
                    .subjectTable td,
                    .subjectTable th{
                        padding:8px;
                        line-height:1.5;
                    }
                    
                    .subjectTable td:nth-child(2){
                        white-space:normal;
                        word-break:break-word;
                    }
                    
                    </style>                    
                    </head>                    
                    <body>");

                //Header
                sb.Append($@"
                    <div class='header'>                    
                    <h2>{student["HeaderLine1"]}</h2>                    
                    <h3>{student["HeaderLine2"]}</h3>                    
                    <p>{student["HeaderLine3"]}</p>                    
                    </div>");

                //Student Details
                sb.Append("<table class='info'>");
                sb.Append($@"
                    <tr>
                    <td><b>Name</b></td>
                    <td>{student["StudentName"]}</td>

                    <td><b>Enrollment No.</b></td>
                    <td>{student["EnrollmentNo"]}</td>
                    </tr>
                    
                    <tr>
                    <td><b>Father's Name</b></td>
                    <td>{student["FatherName"]}</td>
                    
                    <td><b>Roll No.</b></td>
                    <td>{student["RollNo"]}</td>
                    </tr>
                    
                    <tr>
                    <td><b>Mother's Name</b></td>
                    <td>{student["MotherName"]}</td>
                    
                    <td><b>Course</b></td>
                    <td>{student["Branch"]}</td>
                    </tr>
                    
                    <tr>
                    <td><b>Branch</b></td>
                    <td>{student["StreamName"]}</td>
                    
                    <td><b>Class</b></td>
                    <td>{student["YearSemester"]}</td>
                    </tr>
                    
                    <tr>
                    <td><b>College</b></td>
                    <td colspan='3'>{student["InstituteName"]}</td>
                    </tr>");

                sb.Append("</table>");

                //Subject Table

                sb.Append(@"
                    <table class='subjectTable' style='width:100%; border-collapse:collapse; table-layout:fixed;'>
                    
                    <tr>
                        <th style='width:8%;'>Code</th>
                        <th style='width:52%;'>Subject</th>
                        <th style='width:12%;'>Registered Credits</th>
                        <th style='width:12%;'>Earned Credits</th>
                        <th style='width:8%;'>Grade</th>
                        <th style='width:8%;'>Remarks</th>
                    </tr>");
                foreach (DataRow dr in dtSubjects.Rows)
                {
                    if (Convert.ToInt32(dr["IsStudentCenteredActivity"]) == 1)
                    {
                        sb.Append($@"
                            <tr>                            
                                <td style='text-align:center;'>{dr["SubjectCode"]}</td>                            
                                <td style='text-align:left;padding-left:8px;word-wrap:break-word;'>{dr["SubjectName"]}</td>                            
                                <td colspan='4' style='text-align:center;'>{dr["EarnedCredits"]}</td>                        
                            </tr>");
                    }
                    else
                    {
                        var ExCurrentStatus = Convert.ToInt32(dr["IsExCurrent"]) == 1 ? "<span style='color: blue;'> *</span>" : "";
                        sb.Append($@"
                            <tr>                            
                                <td style='text-align:center;'>{dr["SubjectCode"]}</td>                            
                                <td style='text-align:left;padding-left:8px;word-wrap:break-word;'>{dr["SubjectName"]}</td>
                                <td style='text-align:center;'>{dr["SubjectCredits"]}</td>
                                <td style='text-align:center;'>{dr["EarnedCredits"]}{ExCurrentStatus}</td>
                                <td style='text-align:center;'>{dr["Grade"]}</td>
                                <td style='text-align:center;'>{dr["Remarks"]}</td>                           
                            </tr>");
                    }
                }
                sb.Append("</table>");

                //Result Summary
                sb.Append(@"
                    <div style='padding:10px;font-weight:bold;font-size:13px;'>
                        DETAILS UP TO THIS END TERM EXAMINATION RESULT
                    </div>
                    
                    <table style='width:100%;border-collapse:collapse;font-size:12px;text-align:center;'>");

                #region Header

                sb.Append("<tr>");

                sb.Append("<th style='padding:8px;border:1px solid #444;text-align:left;width:180px;'>Semester</th>");

                sb.Append("<th style='padding:8px;border:1px solid #444;'>1</th>");
                sb.Append("<th style='padding:8px;border:1px solid #444;'>2</th>");
                sb.Append("<th style='padding:8px;border:1px solid #444;'>3</th>");
                sb.Append("<th style='padding:8px;border:1px solid #444;'>4</th>");

                if (streamId != 43)
                {
                    sb.Append("<th style='padding:8px;border:1px solid #444;'>5</th>");
                    sb.Append("<th style='padding:8px;border:1px solid #444;'>6</th>");
                    sb.Append("<th style='padding:8px;border:1px solid #444;'>Semester Result</th>");
                }
                else
                {
                    sb.Append("<th colspan='3' style='padding:8px;border:1px solid #444;'>Semester Result</th>");
                }

                sb.Append("</tr>");

                #endregion


                #region Credit Registered

                sb.Append("<tr>");

                sb.Append("<td style='text-align:left;border:1px solid #444;'>Credit Registered</td>");

                sb.Append($"<td>{FormatNumber(result["SubjectCreditsSem1"])}</td>");
                sb.Append($"<td>{FormatNumber(result["SubjectCreditsSem2"])}</td>");
                sb.Append($"<td>{FormatNumber(result["SubjectCreditsSem3"])}</td>");
                sb.Append($"<td>{FormatNumber(result["SubjectCreditsSem4"])}</td>");

                if (streamId != 43)
                {
                    sb.Append($"<td>{FormatNumber(result["SubjectCreditsSem5"])}</td>");
                    sb.Append($"<td>{FormatNumber(result["SubjectCreditsSem6"])}</td>");

                    sb.Append($@"
                        <td rowspan='4'
                            style='border:1px solid #444;
                                   vertical-align:middle;'>
                            {result["Result"]}
                        </td>");
                }
                else
                {
                    sb.Append($@"
                        <td colspan='3'
                            rowspan='4'
                            style='border:1px solid #444;
                                   vertical-align:middle;'>
                            {result["Result"]}
                        </td>");
                }

                sb.Append("</tr>");
                #endregion

                #region Credit Earned

                sb.Append("<tr>");
                sb.Append("<td style='text-align:left;border:1px solid #444;'>Credit Earned</td>");
                sb.Append($"<td>{FormatNumber(result["EarnedCreditsSem1"])}</td>");
                sb.Append($"<td>{FormatNumber(result["EarnedCreditsSem2"])}</td>");
                sb.Append($"<td>{FormatNumber(result["EarnedCreditsSem3"])}</td>");
                sb.Append($"<td>{FormatNumber(result["EarnedCreditsSem4"])}</td>");
                if (streamId != 43)
                {
                    sb.Append($"<td>{FormatNumber(result["EarnedCreditsSem5"])}</td>");
                    sb.Append($"<td>{FormatNumber(result["EarnedCreditsSem6"])}</td>");
                }
                sb.Append("</tr>");
                #endregion


                #region SGPA

                sb.Append("<tr>");
                sb.Append("<td style='text-align:left;border:1px solid #444;'>SGPA</td>");
                sb.Append($"<td>{FormatNumber(result["SGPASem1"])}</td>");
                sb.Append($"<td>{FormatNumber(result["SGPASem2"])}</td>");
                sb.Append($"<td>{FormatNumber(result["SGPASem3"])}</td>");
                sb.Append($"<td>{FormatNumber(result["SGPASem4"])}</td>");

                if (streamId != 43)
                {
                    sb.Append($"<td>{FormatNumber(result["SGPASem5"])}</td>");
                    sb.Append($"<td>{FormatNumber(result["SGPASem6"])}</td>");
                }
                sb.Append("</tr>");

                #endregion


                #region CGPA

                sb.Append("<tr>");
                sb.Append("<td style='text-align:left;border:1px solid #444;'>CGPA</td>");
                sb.Append($"<td>{FormatNumber(result["CGPASem1"])}</td>");
                sb.Append($"<td>{FormatNumber(result["CGPASem2"])}</td>");
                sb.Append($"<td>{FormatNumber(result["CGPASem3"])}</td>");
                sb.Append($"<td>{FormatNumber(result["CGPASem4"])}</td>");
                if (streamId != 43)
                {
                    sb.Append($"<td>{FormatNumber(result["CGPASem5"])}</td>");
                    sb.Append($"<td>{FormatNumber(result["CGPASem6"])}</td>");
                }
                sb.Append("</tr>");

                #endregion


                #region Division

                sb.Append($@"
                    <tr>                    
                    <td rowspan='2' style='border:1px solid #444; text-align:left; vertical-align:middle;'>Division Award Details </td>      

                    <td style='border:1px solid #444;text-align:left;'>Total Credit Earned</td>                    
                    <td style='border:1px solid #444;'>{result["TotalEarnedCredits"]}</td>    

                    <td rowspan='2'style='border:1px solid #444;'>Final CGPA</td>
                    <td rowspan='2' style='border:1px solid #444;'>{result["Percentage"]}</td>
                    
                    <td rowspan='2' colspan='2' style='border:1px solid #444;'>Division</td>
                    <td rowspan='2' style='border:1px solid #444;'>{result["Division"]}</td>
                    
                    </tr>
                    
                    <tr>
                    
                    <td style='border:1px solid #444;text-align:left;'>Total Credit Registered</td>
                    <td style='border:1px solid #444;'>{result["TotalSubjectCredits"]}</td>
                    
                    </tr>");

                #endregion

                sb.Append("</table>");

                //Footer
                sb.Append(@"
                    <div style='padding:20px 10px;font-size:11px;line-height:1.6;'>");

                // District
                sb.Append($@"
                    <div style='margin-bottom:5px;'>
                        {student["District"]}
                    </div>");

                // Result Declaration Date
                sb.Append($@"
                    <div style='margin-bottom:15px;'>
                        DATE OF RESULT DECLARATION :
                        {student["ResultDeclarationDate"]}
                    </div>");

                if (ResultType == (int)EnumResultType.MainResult)
                {
                    sb.Append($@"
                        <div style='margin-bottom:3px;'>                        
                        <strong>NOTE:</strong>                        
                        LAST DATE FOR APPLYING ONLINE RE-EVALUATION FORM IS
                        {student["ReEvaluationDate"]}.                        
                        </div>
                        
                        <div style='margin-bottom:3px;'>                        
                        <strong>NOTE:</strong>                        
                        If student applying for Revaluation in final semester,
                        then his/her mark-sheet will be issued after declaration
                        of Revaluation result.                        
                        </div>");
                }

                if (ResultType == (int)EnumResultType.RevaluationResult)
                {
                    if (!string.IsNullOrWhiteSpace(student["ResultDeclarationDate_Reval"].ToString()))
                    {
                        sb.Append($@"
                            <div style='margin-bottom:15px;'>                            
                            Date Of Issue :
                            {student["ResultDeclarationDate_Reval"]}                            
                            </div>                            
                            ");
                    }

                    sb.Append(@"                            
                        <div style='margin-bottom:10px;'>                        
                        <strong>NOTE:</strong>                        
                        Student appeared as Ex student in * marked subject in current session.                        
                        </div>");
                }

                sb.Append(@"
                    <div style='color:#444;text-align:justify;'>                    
                    <strong>Disclaimer:</strong>                    
                    Though utmost care has been taken in providing information about result
                    on this web portal, even if in case of any inadvertent error the
                    information provided from Board of Technical Education Rajasthan,
                    Jodhpur office will be treated as authentic and final.                    
                    </div>                    
                    </div>");

                //Close HTML
                sb.Append(@"
                    </body>
                    </html>");
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating Diploma Result HTML.", ex);
            }

            return sb;
        }

        private string FormatNumber(object value)
        {
            if (value == null || value == DBNull.Value)
                return "-";

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return "-";

            if (decimal.TryParse(value.ToString(), out decimal d))
                return d.ToString("0.00");

            return "-";
        }
        #endregion

        #region GetMarksStatisticsReport

        public async Task<StringBuilder> GetMarksStatisticsReport_GetHtml(DataSet ds, int ResultType, string ActionType)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable dt = ds.Tables[0];

                if (dt == null || dt.Rows.Count == 0)
                    return sb;

                int fixedColumns = 6;
                int subjectCount = dt.Columns.Count - fixedColumns;

                string currentInstitute = "";
                int[] grandTotal = new int[subjectCount];

                sb.Append("<!DOCTYPE html>");
                sb.Append("<html>");
                sb.Append("<head>");
                sb.Append("<meta charset='UTF-8'>");

                sb.Append(@"
<style>
body{
    font-family:Arial;
    font-size:11px;
    margin:20px;
}

table{
    width:100%;
    border-collapse:collapse;
}

th,td{
    border:1px solid #000;
    padding:4px;
    text-align:center;
    vertical-align:middle;
}

thead th{
    background:#efefef;
}

.left{
    text-align:left;
}

.subtotal{
    font-weight:bold;
    background:#f7f7f7;
}
.grandtotal{
    font-weight:bold;
    background:#d9d9d9;
}

</style>");

                sb.Append("</head>");
                sb.Append("<body>");

                foreach (DataRow row in dt.Rows)
                {
                    string institute = row["InstituteNameEnglish"].ToString().Trim();
                    string SemesterName = row["SemesterName"].ToString().Trim();
                    // New Institute
                    if (currentInstitute != institute)
                    {
                        // Close previous institute
                        if (!string.IsNullOrEmpty(currentInstitute))
                        {
                            sb.Append("<tr class='grandtotal'>");
                            sb.Append("<td colspan='4'><b>GRAND TOTAL</b></td>");

                            for (int i = 0; i < subjectCount; i++)
                            {
                                sb.Append("<td><b>" + grandTotal[i] + "</b></td>");
                            }

                            sb.Append("</tr>");
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            sb.Append("<div style='page-break-after:always'></div>");

                            grandTotal = new int[subjectCount];
                        }

                        // IMPORTANT
                        currentInstitute = institute;

                        // Report Header
                        sb.Append("<h3 style='text-align:center'>GOVERNMENT OF RAJASTHAN</h3>");
                        sb.Append("<h2 style='text-align:center'>BOARD OF TECHNICAL EDUCATION RAJASTHAN, JODHPUR</h2>");
                        sb.Append("<h4 style='text-align:center'>" + SemesterName + "</h4>");
                        sb.Append("<h4 style='text-align:center'>Marks Statistics Report</h4>");
                        sb.Append("<h4 style='text-align:center'>" + ActionType + "</h4>");
                        sb.Append("<br/>");

                        sb.Append("<table>");
                        sb.Append("<thead>");

                        sb.Append("<tr>");
                        sb.Append("<th colspan='" + (fixedColumns + subjectCount) + "' class='left'>");
                        sb.Append(institute);
                        sb.Append("</th>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<th rowspan='2'>Branch</th>");
                        sb.Append("<th rowspan='2'>Registered Student</th>");
                        sb.Append("<th rowspan='2'>Present Student</th>");
                        sb.Append("<th rowspan='2'>Statistics</th>");
                        sb.Append("<th colspan='" + subjectCount + "'>Subject Code</th>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");

                        for (int i = fixedColumns; i < dt.Columns.Count; i++)
                        {
                            sb.Append("<th>" + dt.Columns[i].ColumnName + "</th>");
                        }

                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("<tbody>");
                    }

                    bool isSubTotal = row["Branch"].ToString().Trim()
                        .Equals("Sub Total", StringComparison.OrdinalIgnoreCase);

                    sb.Append(isSubTotal ? "<tr class='subtotal'>" : "<tr>");

                    sb.Append("<td class='left'>" + row["Branch"] + "</td>");
                    sb.Append("<td>" + row["RegisteredStudent"] + "</td>");
                    sb.Append("<td>" + row["PresentStudent"] + "</td>");
                    sb.Append("<td>" + row["Statistics"] + "</td>");

                    for (int i = fixedColumns; i < dt.Columns.Count; i++)
                    {
                        sb.Append("<td>" + row[i] + "</td>");
                    }

                    sb.Append("</tr>");

                    // Calculate Institute Grand Total
                    if (isSubTotal)
                    {
                        for (int i = fixedColumns; i < dt.Columns.Count; i++)
                        {
                            int value = 0;
                            int.TryParse(row[i].ToString(), out value);

                            grandTotal[i - fixedColumns] += value;
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                throw new Exception("Error generating Marks Statistics Report HTML.", ex);
            }

            return sb;
        }

        #endregion

        #region Mark Sheet
        public async Task<StringBuilder> GetHtmlOfMarkSheet(DataSet ds)
        {
            try
            {
                DataRow dr_studet = ds.Tables[0].Rows[0]; // student details
                DataRowCollection drs_subject = ds.Tables[1].Rows; // subject details
                DataRow dr_result = ds.Tables[2].Rows[0]; // result details

                // set sign of registrar                
                string reg_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}{dr_studet["RegistrarSignFile"]}";
                byte[] reg_signbytes = System.IO.File.ReadAllBytes(CommonFuncationHelper.IsFileExisitsOrDefault(reg_signFilepath));
                string reg_signbase64 = Convert.ToBase64String(reg_signbytes);
                string reg_signext = Path.GetExtension(reg_signFilepath).ToLower();
                string reg_signmime = reg_signext switch
                {
                    ".png" => "image/png",
                    ".jpg" => "image/jpeg",
                    ".jpeg" => "image/jpeg",
                    ".gif" => "image/gif",
                    _ => "image/png"
                };

                // set html
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html>");

                sb.AppendLine("<head>");
                sb.AppendLine("    <meta charset=\"UTF-8\">");
                sb.AppendLine("    <title>Marksheet</title>");
                sb.AppendLine("</head>");

                sb.AppendLine("<body style=\"margin:0;padding:0;background:#ffffff;font-family:Arial,Helvetica,sans-serif;\">");
                sb.AppendLine(" <div style=\"display:flex;flex-direction:column;padding:10px 15px;box-sizing:border-box;width:100%;\">");
                sb.AppendLine("<div>");


                #region div set according to dep. printer (set margin-top in pixel also in footer that you set in minus here)
                int css_margintopfordept = 25;
                // div set according to dep. printer (set margin-top in pixel also in footer that you set in minus here)
                sb.AppendLine($"<div style='margin-top:{css_margintopfordept}px;'>");
                // srn
                sb.AppendLine("        <div style=\"text-align:right;font-size:16px; font-weight:bold; padding-right:0px;padding-top:25px; height:20px;\">");
                sb.AppendLine($"            {dr_studet["ODNumber"]}");
                sb.AppendLine("        </div>");

                // session
                sb.AppendLine("<div style=\"height:71px; width:100%; float:left;\">");
                sb.AppendLine("        <table style=\"width:100%;border-collapse:collapse;margin:20px 0 25px 0;\">");
                sb.AppendLine("            <tr>");

                sb.AppendLine("                <td style=\"width:65%;vertical-align:top;\"></td>");

                sb.AppendLine("                <td style=\"width:35%;vertical-align:top;\">");

                sb.AppendLine("                    <table style=\"width:100%;border-collapse:collapse;font-size:14px;\">");
                sb.AppendLine("                        <tr>");
                sb.AppendLine("                            <td style=\"text-align:right;font-weight:bold;\">");
                sb.AppendLine($"                                {dr_studet["EndTermName"]}");
                sb.AppendLine("                            </td>");

                sb.AppendLine("                            <td style=\"text-align:right;font-weight:bold;padding-right:10px;\">");
                sb.AppendLine($"                                {dr_studet["AcademicYear"]}");
                sb.AppendLine("                            </td>");
                sb.AppendLine("                        </tr>");
                sb.AppendLine("                    </table>");

                sb.AppendLine("                </td>");

                sb.AppendLine("            </tr>");
                sb.AppendLine("        </table>");
                sb.AppendLine("</div>");

                // name
                sb.AppendLine("<div style=\"width:100%; height: 200px; float:left;\">");
                sb.AppendLine("        <table style=\"width:100%;border-collapse:collapse;\">");

                sb.AppendLine("            <tr>");

                sb.AppendLine("                <td style=\"font-size:11px;padding-bottom:15px; width: 21%;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:12px;padding-bottom:15px; width: 41.5%;\">");
                sb.AppendLine($"                    {dr_studet["StudentName"]}");
                sb.AppendLine("                </td>");

                sb.AppendLine("                <td style=\"font-size:11px;padding-bottom:15px; width: 16.7%;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:12px;text-align:left;padding-bottom:15px; width: 20.8%;\">");
                sb.AppendLine($"                    {dr_studet["EnrollmentNo"]}");
                sb.AppendLine("                </td>");

                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td style=\"font-size:11px;padding:13px 0 ;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:12px;padding:13px 0 ;\">");
                sb.AppendLine($"                    {dr_studet["FatherName"]}");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:11px;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:12px;text-align:left;\">");
                sb.AppendLine($"                    {dr_studet["RollNo"]}");
                sb.AppendLine("                </td>");

                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td style=\"font-size:11px;padding:13px 0 ;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:12px;padding:13px 0 ;\">");
                sb.AppendLine($"                    {dr_studet["MotherName"]}");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:11px;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:12px;text-align:left;\">");
                sb.AppendLine($"                    {dr_studet["Branch"]}");
                sb.AppendLine("                </td>");

                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td style=\"font-size:11px;padding:13px 0 ;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:10px;padding:13px 0 ;\">");
                sb.AppendLine($"                    {dr_studet["StreamName"]}");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:11px;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"font-size:11px;text-align:left;\">");
                sb.AppendLine($"                    {dr_studet["YearSemester"]}");
                sb.AppendLine("                </td>");

                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td style=\"font-size:11px;padding:13px 0;\">");
                sb.AppendLine("                    &nbsp;");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td colspan=\"3\" style=\"font-size:12px;padding:13px 0 ;\">");
                sb.AppendLine($"                    {dr_studet["InstituteName"]}");
                sb.AppendLine("                </td>");
                sb.AppendLine("                ");

                sb.AppendLine("            </tr>");

                sb.AppendLine("        </table>");
                sb.AppendLine("</div>");
                // div set according to dep. printer
                sb.AppendLine("</div>");
                #endregion


                #region subjects
                // subjects
                sb.AppendLine("        <!-- Subject Table -->");

                sb.AppendLine("    <div style=\"height:600px; width:100%; float:left;\">");
                sb.AppendLine("    <table style=\"width:100%;border-collapse:collapse;margin-top:0px;font-size:11px;border:1px solid #000;line-height:100%;height:330px;\">");

                sb.AppendLine("            <tr>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:12%;\">");
                sb.AppendLine("                    CODE");
                sb.AppendLine("                </th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:64%;\">");
                sb.AppendLine("                    SUBJECT(S)");
                sb.AppendLine("                </th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:6%;\">");
                sb.AppendLine("                    REGISTERED<br>CREDIT");
                sb.AppendLine("                </th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:6%;\">");
                sb.AppendLine("                    EARNED<br>CREDIT");
                sb.AppendLine("                </th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:6%;\">");
                sb.AppendLine("                    GRADE<br>AWARDED");
                sb.AppendLine("                </th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:6%;\">");
                sb.AppendLine("                    REMARK");
                sb.AppendLine("                </th>");

                sb.AppendLine("            </tr>");

                //int i = 0;
                //// subjects loop // for 16 max
                //foreach (DataRow dr in drs_subject)
                //{
                //    i++;
                //    if (i == 7)
                //    {
                //        break;
                //    }

                //    sb.AppendLine("            <tr>");

                //    sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;\">{dr["SubjectCode"]}</td>");
                //    sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;\">{dr["SubjectName"]}</td>");

                //    sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;text-align:center;\">{dr["SubjectCredits"]}</td>");
                //    sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;text-align:center;\">{dr["EarnedCredits"]}</td>");
                //    sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;text-align:center;\">{dr["Grade"]}</td>");
                //    sb.AppendLine($"                <td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px 5px;text-align:center;\">{dr["Remarks"]}</td>");

                //    sb.AppendLine("            </tr>");
                //}

                // subjects loop
                foreach (DataRow dr in drs_subject)
                {
                    sb.AppendLine("            <tr>");

                    sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;\">{dr["SubjectCode"]}</td>");
                    sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;\">{dr["SubjectName"]}</td>");

                    // for sca
                    string scacolspan = string.Empty;
                    bool issca = Convert.ToBoolean(dr["IsStudentCenteredActivity"] == DBNull.Value ? false : dr["IsStudentCenteredActivity"]);
                    if (issca)
                    {
                        scacolspan = "colspan=\"4\"";
                        sb.AppendLine($"                <td style=\"border:1px solid #000;padding:3px 5px;text-align:center;\" {scacolspan}>{dr["EarnedCredits"]}</td>");
                    }
                    else
                    {
                        sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;text-align:center;\">{dr["SubjectCredits"]}</td>");
                        sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;text-align:center;\">{dr["EarnedCredits"]}</td>");
                        sb.AppendLine($"                <td style=\"border-left:1px solid #000;padding:3px 5px;text-align:center;\">{dr["Grade"]}</td>");
                        sb.AppendLine($"                <td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:3px 5px;text-align:center;\">{dr["Remarks"]}</td>");
                    }

                    sb.AppendLine("            </tr>");
                }
                sb.AppendLine("        </table>");

                // results
                sb.AppendLine("         <div style=\"text-align:center;font-size:12px;margin:10px 0;font-weight:bold;\">");
                sb.AppendLine("            DETAILS UP TO THIS END TERM EXAMINATION RESULT");
                sb.AppendLine("        </div>");

                sb.AppendLine("        <table style=\"width:100%;border-collapse:collapse;font-size:11px;\">");

                sb.AppendLine("            <tr>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:18%;\">");
                sb.AppendLine("                    Semester");
                sb.AppendLine("                </th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:9%;\">1</th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:9%;\">2</th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:9%;\">3</th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:9%;\">4</th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:9%;\">5</th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:9%;\">6</th>");

                sb.AppendLine("                <th style=\"border:1px solid #000;padding:3px 5px;width:28%;\">");
                sb.AppendLine("                    RESULT");
                sb.AppendLine("                </th>");

                sb.AppendLine("            </tr>");


                // results dr fill
                sb.AppendLine("            <tr>");

                sb.AppendLine("                <td style=\"border:1px solid #000;padding:3px 5px;\">Credit Registered</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;padding:3px 5px;text-align:center;\">{dr_result["SubjectCreditsSem1"]}</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SubjectCreditsSem2"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SubjectCreditsSem3"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SubjectCreditsSem4"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SubjectCreditsSem5"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SubjectCreditsSem6"]}</td>");

                sb.AppendLine("                <td rowspan=\"4\" style=\"border:1px solid #000;text-align:center;font-weight:bold;\">");
                sb.AppendLine($"                    {dr_result["Result"]}");
                sb.AppendLine("                </td>");

                sb.AppendLine("            </tr>");


                sb.AppendLine("            <tr>");

                sb.AppendLine("                <td style=\"border:1px solid #000;padding:3px 5px;\">Credit Earned</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["EarnedCreditsSem1"]}</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["EarnedCreditsSem2"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["EarnedCreditsSem3"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["EarnedCreditsSem4"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["EarnedCreditsSem5"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["EarnedCreditsSem6"]}</td>");

                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");

                sb.AppendLine("                <td style=\"border:1px solid #000;padding:3px 5px;\">SGPA</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SGPASem1"]}</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SGPASem2"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SGPASem3"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SGPASem4"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SGPASem5"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["SGPASem6"]}</td>");

                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");

                sb.AppendLine("                <td style=\"border:1px solid #000;padding:3px 5px;\">CGPA</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["CGPASem1"]}</td>");

                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["CGPASem2"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["CGPASem3"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["CGPASem4"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["CGPASem5"]}</td>");
                sb.AppendLine($"                <td style=\"border:1px solid #000;text-align:center;\">{dr_result["CGPASem6"]}</td>");

                sb.AppendLine("            </tr>");


                sb.AppendLine("            <!-- Division Award Details -->");

                sb.AppendLine("            <tr>");

                sb.AppendLine("<td rowspan =\"2\" style=\"width:18%;border-top:2px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;");
                sb.AppendLine("                            text-align:center;");
                sb.AppendLine("                            ");
                sb.AppendLine("                            padding:25px 10px;\">");

                sb.AppendLine("                                Division Award<br>Details");

                sb.AppendLine("                            </td>");

                sb.AppendLine("                            <td colspan=\"2\" style=\"width:9%;");
                sb.AppendLine("                            border-top:2px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;");
                sb.AppendLine("                            text-align:center;");
                sb.AppendLine("                            ");
                sb.AppendLine("                            padding:3px 5px;\">");

                sb.AppendLine("                                Total Credit<br>Earned");

                sb.AppendLine("                            </td>");
                sb.AppendLine("                            <td style=\"width:9%;");
                sb.AppendLine("                            border-top:2px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;");
                sb.AppendLine("                            text-align:center;");
                sb.AppendLine("                            ");
                sb.AppendLine("                            padding:3px 5px;\">");

                sb.AppendLine($"                                {dr_result["TotalEarnedCredits"]}");

                sb.AppendLine("                            </td>");

                sb.AppendLine("                            <td rowspan=\"2\" style=\"width:9%;");
                sb.AppendLine("                            border-top:2px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;");
                sb.AppendLine("                            text-align:center;");
                sb.AppendLine("                            \">");

                sb.AppendLine("                                Final<br>CGPA");

                sb.AppendLine("                            </td>");
                sb.AppendLine("                            <td rowspan=\"2\" style=\"width:9%;");
                sb.AppendLine("                            border-top:2px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;");
                sb.AppendLine("                            text-align:center;");
                sb.AppendLine("                            \">");

                sb.AppendLine($"                                {dr_result["Percentage"]}");

                sb.AppendLine("                            </td>");

                sb.AppendLine("                            <td rowspan=\"2\" style=\"width:9%;");
                sb.AppendLine("                            border-top:2px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;");
                sb.AppendLine("                            text-align:center;");
                sb.AppendLine("                            \">");

                sb.AppendLine("                                Division");

                sb.AppendLine("                            </td>");
                sb.AppendLine("                            <td rowspan=\"2\" style=\"width:9%;");
                sb.AppendLine("                            border-top:2px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;");
                sb.AppendLine("                            text-align:center;");
                sb.AppendLine("                            \">");

                sb.AppendLine($"                                {dr_result["Division"]}");

                sb.AppendLine("                            </td>");

                sb.AppendLine("                        </tr>");

                sb.AppendLine("                        <tr>");

                sb.AppendLine("                            <td colspan=\"2\" style=\"border-top:1px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000; width:9%;");
                sb.AppendLine("                                   text-align:center;");
                sb.AppendLine("                                   ");
                sb.AppendLine("                                   padding:3px 5px;\">");

                sb.AppendLine("                                Total Credit<br>Registered");

                sb.AppendLine("                            </td>");
                sb.AppendLine("                            <td style=\"border-top:1px solid #000;border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000; width:9%;");
                sb.AppendLine("                                   text-align:center;");
                sb.AppendLine("                                   ");
                sb.AppendLine("                                   padding:3px 5px;\">");

                sb.AppendLine($"                                {dr_result["TotalSubjectCredits"]}");

                sb.AppendLine("                            </td>");

                sb.AppendLine("                        </tr>");

                sb.AppendLine("        </table>");
                sb.AppendLine("</div>");

                sb.AppendLine("</div>");
                #endregion


                #region footer date and sign
                // footer date and sign
                sb.AppendLine($"<div style=\"width:95%; height:100px; margin-top:-5px;\">");

                sb.AppendLine("<div style=\"width:50%;float:left;text-align:right;\">");
                sb.AppendLine($"<div style=\"margin-top:20px;font-size:15px;font-weight:bold;padding-right:100px;\">{(dr_studet["ResultDeclarationDate"] ?? dr_studet["ResultDeclareDate"])}</div>");
                sb.AppendLine($"<div style=\"margin-top:28px;font-size:15px;font-weight:bold;padding-right:160px;\">{(dr_studet["ResultDeclarationDate"] ?? dr_studet["ResultDeclareDate"])}</div>");
                sb.AppendLine("</div>");
                sb.AppendLine($"<div style=\"width:50%;text-align:center;float:right;margin-top:-10px; \">");
                sb.AppendLine($"<img src=\"data:{reg_signmime};base64,{reg_signbase64}\" style=\"width:80px;margin-right:-200px;\" />");
                sb.AppendLine("</div>");

                sb.AppendLine("</div>");
                #endregion

                sb.AppendLine("    </div>");
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                return sb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region GetToppersReport

        //==============================
        // PART 1 : Method & Data Preparation
        //==============================
        public async Task<StringBuilder> GetToppersReport_Html(DataSet ds, int ResultType, string ActionType)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (ds == null || ds.Tables.Count == 0)
                    return sb;

                DataTable dt = ds.Tables[0];

                if (dt == null || dt.Rows.Count == 0)
                    return sb;

                // Sort Branch ASC and Percentage DESC
                DataView dv = dt.DefaultView;
                dv.Sort = "[Stream/Branch] ASC, Percentage DESC";
                dt = dv.ToTable();

                var branchGroups = dt.AsEnumerable()
                                     .GroupBy(x => x["Stream/Branch"].ToString())
                                     .ToList();

                const int PageSize = 30;

                //string program = dt.Columns.Contains("Stream/Branch")
                //                    ? Convert.ToString(dt.Rows[0]["Stream/Branch"])
                //                    : "";

                string session = dt.Columns.Contains("SessionYear")
                                    ? Convert.ToString(dt.Rows[0]["SessionYear"])
                                    : "";

                //==============================
                // PART 2 : HTML Header & CSS
                //==============================



                for (int b = 0; b < branchGroups.Count; b++)
                {
                    var branch = branchGroups[b].ToList();
                    string program = branch[0]["Stream/Branch"].ToString();
                    // Start every branch on a new page except the first
                    if (b > 0)
                    {
                        sb.Append(@"
</tbody>
</table>
</div>

<div style='page-break-before:always;'></div>
");
                    }

                    int sno = 1;

                    sb.Append(@"
<!DOCTYPE html>
<html lang='en'>

<head>

<meta charset='UTF-8'>

<title>Board Of Technical Education Result</title>

<style>

@page{
    size:A4 portrait;
    margin:15mm 12mm 15mm 12mm;
}

*{
    margin:0;
    padding:0;
    box-sizing:border-box;
}

body{
    font-family:Arial,Helvetica,sans-serif;
    font-size:12px;
    color:#000;
    margin:0;
    padding:0;
}

.container{
    width:100%;
    page-break-after:auto;
}

table{
    width:100%;
    border-collapse:collapse;
    border-spacing:0;text-align:left;
}

thead{
    display:table-header-group;
}

tfoot{
    display:table-footer-group;
}

tr{
    page-break-inside:avoid;
}

td,th{
    page-break-inside:avoid;
    vertical-align:middle;
text
}

.header{
    width:100%;
    margin-bottom:8px;
}

.header td{
    text-align:left;
    padding:2px;
}

.govt{
    font-size:15px;
    font-weight:bold;
}

.title{
    font-size:22px;
    font-weight:bold;
    line-height:28px;
}

.info{
    margin-top:5px;
    margin-bottom:8px;
}

.info td{
    padding:5px;
    font-weight:bold;
    border-top:1px solid #000;
    border-bottom:1px solid #000;text-align:left;
}

.left{
    text-align:left;
}

.right{
    text-align:right;
}

.result{
    width:100%;
}

.result thead th{
    font-weight:bold;
    text-align:left;
    padding:6px 4px;
}

.result tbody td{
    padding:5px 4px;text-align:left;
}

.result tr{
    page-break-inside:avoid;
}

.pass{
    text-align:center;
    letter-spacing:2px;
    font-weight:bold;
}

.center{
    text-align:center;
}

.branch{
    font-weight:bold;
    font-size:15px;
    text-align:center;
    background:#efefef;
    padding:6px;
}

.page-break{
    page-break-before:always;
}

</style>

</head>

<body>
");
                    //==============================
                    // PART 3 : Report Header
                    //==============================

                    sb.Append(@"

<div class='container'>

<table class='header'>

<tr>
    <td class='govt' style='text-align:center !important;'>
        GOVERNMENT OF RAJASTHAN
    </td>
</tr>

<tr>
    <td class='title' style='text-align:center !important;'>
        BOARD OF TECHNICAL EDUCATION, RAJASTHAN,<br>
        JODHPUR
    </td>
</tr>

</table>

<table class='info'>

<tr>

<td class='left'>
Program : " + program + @"
</td>

<td class='right'>
Session : " + session + @"
</td>

</tr>

</table>

<table class='result'>

<thead>

<tr>

<th >SNO.</th>

<th >SPN</th>

<th >NAME</th>

<th >CIC</th>

<th colspan='6' class='pass' style='text-align:center !important;'>
---------------- PASSED ----------------
</th>

<th width='8%' class='right'>%</th>

</tr>

<tr>

<th></th>

<th></th>

<th></th>

<th></th>

<th class='center'>Sem 1</th>

<th class='center'>Sem 2</th>

<th class='center'>Sem 3</th>

<th class='center'>Sem 4</th>

<th class='center'>Sem 5</th>

<th class='center'>Sem 6</th>

<th></th>

</tr>

</thead>

<tbody>

");

                    //==============================
                    // PART 5 : Student Loop
                    //==============================

                    for (int i = 0; i < branch.Count; i++)
                    {
                        DataRow dr = branch[i];

                        sb.Append($@"

<tr style='border-bottom:1px solid grey;'>

<td>{sno}</td>

<td>{dr["Enrollment No"]}</td>

<td>{dr["Student Name"]}</td>

<td>{dr["InstituteCode"]}</td>

<td>{dr["EndTermSem1"]}</td>

<td>{dr["EndTermSem2"]}</td>

<td>{dr["EndTermSem3"]}</td>

<td>{dr["EndTermSem4"]}</td>

<td>{dr["EndTermSem5"]}</td>

<td>{dr["EndTermSem6"]}</td>

<td>{dr["Percentage"]}</td>

</tr>

");

                        sno++;

                        //==============================
                        // PART 6 : Page Break
                        //==============================

                        if ((i + 1) % PageSize == 0 && i != branch.Count - 1)
                        {
                            // Repeat complete Header
                        }
                        //==============================
                        // PART 7 : End Student Loop
                        //==============================

                    }

                    //==============================
                    // PART 8 : Branch Page Break
                    //==============================

                    if (b < branchGroups.Count - 1)
                    {
                        // Repeat complete Header
                    }
                    //==============================
                    // PART 9 : End Branch Loop
                    //==============================

                }

                //==============================
                // PART 10 : Close HTML
                //==============================

                sb.Append(@"

</tbody>

</table>

</div>

</body>

</html>

");

                //==============================
                // PART 11 : Return
                //==============================

                return sb;

            }

            catch (Exception ex)
            {
                throw new Exception("Error generating Toppers Report HTML.", ex);
            }


        }
        #endregion


        #region GetProvesionalMeritList
        public async Task<StringBuilder> GetProvesionalMeritList_Html(DataSet ds, int ResultType, string ActionType)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (ds == null || ds.Tables.Count == 0)
                    return sb;

                DataTable dt = ds.Tables[0];

                if (dt == null || dt.Rows.Count == 0)
                    return sb;
                int totalRows = dt.Rows.Count;
                // Sort Branch ASC and Percentage DESC
                DataView dv = dt.DefaultView;
                dv.Sort = "[Stream/Branch] ASC, Percentage DESC";
                dt = dv.ToTable();

                var branchGroups = dt.AsEnumerable()
                                     .GroupBy(x => x["Stream/Branch"].ToString())
                                     .ToList();

                const int PageSize = 8;


                string session = dt.Columns.Contains("SessionYear")
                                    ? Convert.ToString(dt.Rows[0]["SessionYear"])
                                    : "";

                int IsFootercontent = 0;

                if (ActionType == "ProvesionalMeritList")
                {
                    IsFootercontent = 1;
                }
                if (ActionType == "FinalMeritList")
                {
                    IsFootercontent = 2;
                }


                for (int b = 0; b < branchGroups.Count; b++)
                {
                    var branch = branchGroups[b].ToList();
                    string program = branch[0]["Stream/Branch"].ToString();
                    // Start every branch on a new page except the first
                    if (b > 0)
                    {
                        sb.Append(@"
</tbody>
</table>
</div>

<div style='page-break-before:always;'></div>
");
                    }

                    int sno = 1;

                    sb.Append(@"

<!DOCTYPE html>
<html lang='en'>

<head>

<meta charset='UTF-8'>

<title>Merit List</title>

<style>

*{
    margin:0;
    padding:0;
    box-sizing:border-box;
}

body{
    font-family:Arial,Helvetica,sans-serif;
    font-size:12px;
    color:#000;
    padding:20px;
}

.container{
    width:100%;
    border:1px solid #cfcfcf;
    padding:15px;
}

.header{
    text-align:center;
    border-bottom:1px solid #ccc;
    padding-bottom:10px;
    margin-bottom:10px;
}

.header .gov{
    font-size:18px;
    font-weight:bold;
}

.header .title{
    font-size:36px;
    font-weight:500;
    margin:12px 0;
}

.header .sub{
    font-size:18px;
    font-weight:bold;
}

table{
    width:100%;
    border-collapse:collapse;
}

thead th{
    text-align:left;
    padding:8px 5px;
    font-size:14px;
    border-bottom:1px solid #999;
}

tbody td{
    padding:10px 5px;
    border-bottom:1px solid #d8d8d8;
    vertical-align:top;
}

.program td{
    font-weight:bold;
    border-bottom:1px solid #999;
    padding:10px 0;
}

.col-sno{width:70px;}
.col-enroll{width:160px;}
.col-per{width:90px;text-align:center;}
.col-merit{width:70px;text-align:center;}

.name{
    font-weight:bold;
    text-transform:uppercase;
}

.father{
    margin-top:2px;
}

.institute{
    margin-top:4px;
}

.footer{
    margin-top:15px;
}

.note{
    margin-top:10px;
    line-height:22px;
    text-align:justify;
}

.signature{
    text-align:right;
    font-weight:bold;
    margin-top:20px;
}
thead th{
    text-align:left;
    padding:8px 5px;
    font-size:14px;
    border-bottom:1px solid #999;
}

</style>

</head>

<body>

");
                    //==============================
                    // PART 3 : Report Header
                    //==============================

                    sb.Append($@"

<div class='container'>

<div class='header'>

<div class='gov'>
GOVERNMENT OF RAJASTHAN
</div>

<div class='title'>
BOARD OF TECHNICAL EDUCATION,
RAJASTHAN, JODHPUR
</div>

<div class='sub'>
PROVISIONAL MERIT LIST for the Session {session}
</div>

</div>

<table>

<thead>

<tr>

    <th class='col-sno'>Roll No</th>

    <th class='col-enroll'>Enrollment No</th>

    <th>Student Name / Father Name / Institute</th>

    <th class='col-per'>Percentage</th>
    <th class='col-per'>Merit</th>

</tr>

<tr class=""program"">
      <th style=""width:250px;"">Program: {program}</th>
    <th colspan=""4""></th>
</tr>

</thead>

<tbody>

");

                    //==============================
                    // PART 5 : Student Loop
                    //==============================

                    for (int i = 0; i < branch.Count; i++)
                    {
                        DataRow dr = branch[i];

                        sb.Append($@"

<tr>

<td>{dr["Roll No"]}</td>

<td>{dr["Enrollment No"]}</td>

<td>
        {dr["Student Name"]}<br>
        {dr["Father Name"]}<br>
        {dr["Institute Name"]}
</td>


<td>{dr["Percentage"]}</td>
<td>{dr["MeritPosition"]}</td>

</tr>

");

                        sno++;

                        //==============================
                        // PART 6 : Page Break
                        //==============================

                        //==============================
                        // PART 6 : Page Break
                        //==============================
                        if ((i + 1) % PageSize == 0 && i != branch.Count - 1)
                        {
                            sb.Append($@"

        </tbody>
    </table>

    <div class='footer'>
        <div class='eligible'>
            <strong>Eligible for Merit: {totalRows}</strong>
        </div>
    </div>

</div>

<div style='page-break-before:always;'></div>

<div class='container'>

<div class='header'>

<div class='gov'>
GOVERNMENT OF RAJASTHAN
</div>

<div class='title'>
BOARD OF TECHNICAL EDUCATION,
RAJASTHAN, JODHPUR
</div>

<div class='sub'>
PROVISIONAL MERIT LIST for the Session {session}
</div>

</div>

<table>

<thead>

<tr>
    <th class='col-sno'>Roll No</th>
    <th class='col-enroll'>Enrollment No</th>
    <th>Student Name / Father Name / Institute</th>
    <th class='col-per'>Percentage</th>
    <th class='col-per'>Merit</th>
</tr>

<tr class=""program"">
      <th style=""width:250px;"">Program: {program}</th>
    <th colspan=""4""></th>
</tr>


</thead>

<tbody>

");
                        }
                        //==============================
                        // PART 7 : End Student Loop
                        //==============================

                    }

                    //==============================
                    // PART 8 : Branch Page Break
                    //==============================

                    if (b < branchGroups.Count - 1)
                    {
                        // Repeat complete Header
                    }
                    //==============================
                    // PART 9 : End Branch Loop
                    //==============================

                }

                //==============================
                // PART 10 : Close HTML
                //==============================

                sb.Append($@"

</tbody>

</table>

<div class='footer'>

    <div class='eligible'>
        <strong>Eligible for Merit: {totalRows}</strong>
    </div>

");

                if (IsFootercontent == 1)
                {
                    sb.Append(@"

    <div class='note'>
        Any objection regarding the provisional merit should be sent to the Board
        directly so as to reach the Board office latest by
        ................................
        After considering the objections received upto this date the final merit list
        will be declared. In case no objection are received then the provisional merit
        list will be the final merit list.
    </div>

");
                }

                sb.Append(@"

    <div class='signature'>
        REGISTRAR
    </div>

</div>

</div>

</body>

</html>

");

                //==============================
                // PART 11 : Return
                //==============================

                return sb;

            }

            catch (Exception ex)
            {
                throw new Exception("Error generating Toppers Report HTML.", ex);
            }


        }
        #endregion

        #region GetApprenticeshipFresherReports_Html
        public async Task<StringBuilder> GetApprenticeshipFresherReports_Html(DataSet ds, int ResultType)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (ds == null || ds.Tables.Count == 0)
                    return sb;
                DataTable dt = ds.Tables[0];
                if (dt == null || dt.Rows.Count == 0)
                    return sb;
                int totalRows = dt.Rows.Count;
                sb.Append(@"

<!DOCTYPE html>

<html lang='en'>

<head>

<meta charset='UTF-8'>

<meta name='viewport' content='width=device-width, initial-scale=1.0'>

<title>Apprenticeship Registration Report</title>

<style>

@page{
    size:A4 landscape;
    margin:15mm;
}

*{
    margin:0;
    padding:0;
    box-sizing:border-box;
}

body{
    font-family: 'Segoe UI', 'Noto Sans Devanagari', 'Arial Unicode MS',
                 'Nirmala UI', 'Mangal', sans-serif;
    font-size:13px;
    color:#111;
    line-height:1.45;
    -webkit-font-smoothing: antialiased;
    text-rendering: optimizeLegibility;
}


.container{

    width:100%;

}

table{
    width:100%;
    border-collapse:collapse;
    table-layout:fixed;
}

thead{

    display:table-header-group;

}

tfoot{

    display:table-footer-group;

}

tr{

    page-break-inside:avoid;

}

th{
    border:1px solid #000;
    padding:8px;
    font-size:13px;
    font-weight:700;
    background:#f2f2f2;
    vertical-align:middle;
}

td{
    border:1px solid #000;
    padding:7px;
    font-size:12px;
    vertical-align:middle;
    word-break:break-word;
}

.title{

    font-size:24px;

    font-weight:bold;

    text-align:left;

    border:none !important;

    padding-bottom:10px;

}

.title{
    font-size:24px;
    font-weight:700;
    text-align:left;
}

.hindiTitle{
    font-size:20px;
    font-weight:700;
    text-align:center;
}

.formNo{
    font-size:18px;
    font-weight:700;
    text-align:right;
}


.numberRow{

    font-weight:bold;

}

.left{
    text-align:left;
}

.center{
    text-align:center;
}

.right{
    text-align:right;
}

.page-break{

    page-break-before:always;

}

</style>

</head>

<body>

<div class='container'>

");
                sb.Append(@"

<table>

<thead>

<tr>

<th colspan='9' class='title' style='border:none;text-align:left;font-size:22px;'>
Apprenticeship Registration (School/College student)
</th>

<th class='formNo' style='border:none;text-align:right;font-size:18px;'>
(प्रपत्र-य)
</th>

</tr>

<tr>

<th colspan='10'
style='font-size:18px;
font-weight:bold;
text-align:center;
padding:10px;
border:1px solid #000;'>

अन्य संस्थानों / विद्यालयों के युवाओं का फ्रेशर के रूप में पंजीकरण की सूची

</th>

</tr>

<tr>

<th style='width:16%;'>
पंजीकरण करने वाले संस्थान का नाम
</th>

<th style='width:9%;'>
पोर्टल पर पंजीकरण<br/>
करने की तिथि
</th>

<th style='width:8%;'>
पंजीकरण संख्या
</th>

<th style='width:16%;'>
पिता का नाम
</th>

<th style='width:10%;'>
आधार नम्बर
</th>

<th style='width:9%;'>
जन्म तिथि
</th>

<th style='width:14%;'>
पता
</th>

<th style='width:8%;'>
शैक्षिक योग्यता
</th>

<th style='width:10%;'>
वर्तमान में क्या कर रहे हैं
</th>

<th style='width:10%;'>
विशेष विवरण
</th>

</tr>

<tr class='numberRow'>

<th>1</th>
<th>2</th>
<th>3</th>
<th>4</th>
<th>5</th>
<th>6</th>
<th>7</th>
<th>8</th>
<th>9</th>
<th>10</th>

</tr>

</thead>

<tbody>

");

                foreach (DataRow dr in dt.Rows)
                {
                    string regDate = "";
                    string dob = "";
                    if (dr["RegDate"] != DBNull.Value)
                    {
                        regDate = Convert.ToDateTime(dr["RegDate"]).ToString("dd/MM/yyyy");
                    }
                    if (dr["DOB"] != DBNull.Value)
                    {
                        dob = Convert.ToDateTime(dr["DOB"]).ToString("dd/MM/yyyy");
                    }
                    sb.Append($@"

<tr>

<td class='left'>
{dr["Name"]}
</td>

<td>
{regDate}
</td>

<td>
{dr["RegCount"]}
</td>

<td class='left'>

{dr["FatherName"]}
</td>

<td>
{dr["AadharNo"]}
</td>

<td>
{dob}
</td>

<td class='left'>
{dr["Address"]}
</td>

<td>
{dr["EducationalQualification"]}
</td>

<td>
{dr["CurrentOccupation"]}
</td>

<td class='left'>
{dr["Remarks"]}
</td>

</tr>

");
                }
                sb.Append(@"

</tbody>

</table>

</div>

</body>

</html>

");
                return sb;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating Apprenticeship Fresher Report.", ex);
            }
        }
        #endregion


        #region Final Diploma Certificate
        public async Task<StringBuilder> GetHtmlOfDiplomaCertificate(DiplomaCertificateDownloadSearchModel data)
        {
            try
            {
                // set sign of registrar                
                string reg_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}{data.RegistrarSignFile}";
                byte[] reg_signbytes = System.IO.File.ReadAllBytes(CommonFuncationHelper.IsFileExisitsOrDefault(reg_signFilepath));
                string reg_signbase64 = Convert.ToBase64String(reg_signbytes);
                string reg_signext = Path.GetExtension(reg_signFilepath).ToLower();
                string reg_signmime = reg_signext switch
                {
                    ".png" => "image/png",
                    ".jpg" => "image/jpeg",
                    ".jpeg" => "image/jpeg",
                    ".gif" => "image/gif",
                    _ => "image/png"
                };

                // set html
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"en\">");
                sb.AppendLine("<head>");
                sb.AppendLine("<meta charset=\"UTF-8\">");
                sb.AppendLine("<title>Diploma Certificate</title>");
                sb.AppendLine("<style>");
                sb.AppendLine("body {");
                sb.AppendLine("  font-family: Arial, Helvetica, sans-serif;");
                sb.AppendLine("  background: #f2f2f2;");
                sb.AppendLine("  margin: 0;");
                sb.AppendLine("  padding: 40px 0;");
                sb.AppendLine("}");
                sb.AppendLine(".certificate {");
                sb.AppendLine("  max-width: 850px;");
                sb.AppendLine("  min-height: 1100px;");
                sb.AppendLine("  margin: 0 auto;");
                sb.AppendLine("  background: #fff;");
                sb.AppendLine("  padding: 60px 70px;");
                sb.AppendLine("  box-sizing: border-box;");
                sb.AppendLine("  position: relative;");
                sb.AppendLine("  box-shadow: 0 0 8px rgba(0,0,0,0.15);");
                sb.AppendLine("}");
                sb.AppendLine(".header-row {");
                sb.AppendLine("  display: flex;");
                sb.AppendLine("  justify-content: space-between;");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 14px;");
                sb.AppendLine("}");
                sb.AppendLine(".body-content {");
                sb.AppendLine("  margin-top: 260px;");
                sb.AppendLine("  text-align: center;");
                sb.AppendLine("}");
                sb.AppendLine(".name {");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 16px;");
                sb.AppendLine("  margin-bottom: 8px;");
                sb.AppendLine("}");
                sb.AppendLine(".parent {");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 16px;");
                sb.AppendLine("  margin-bottom: 30px;");
                sb.AppendLine("}");
                sb.AppendLine(".diploma-title {");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 16px;");
                sb.AppendLine("  margin-bottom: 40px;");
                sb.AppendLine("  text-align: left;");
                sb.AppendLine("  margin-left: 60px;");
                sb.AppendLine("}");
                sb.AppendLine(".session-row {");
                sb.AppendLine("  display: flex;");
                sb.AppendLine("  justify-content: space-between;");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 15px;");
                sb.AppendLine("  margin: 0 60px 60px 60px;");
                sb.AppendLine("  text-align: left;");
                sb.AppendLine("}");
                sb.AppendLine(".division {");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 15px;");
                sb.AppendLine("  margin-bottom: 10px;");
                sb.AppendLine("}");
                sb.AppendLine(".completion-date {");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 15px;");
                sb.AppendLine("  margin-bottom: 40px;");
                sb.AppendLine("  text-align: left;");
                sb.AppendLine("  margin-left: 40px;");
                sb.AppendLine("}");
                sb.AppendLine(".signature {");
                sb.AppendLine("  font-family: cursive;");
                sb.AppendLine("  font-size: 34px;");
                sb.AppendLine("  text-align: left;");
                sb.AppendLine("  margin-left: 220px;");
                sb.AppendLine("  margin-bottom: 30px;");
                sb.AppendLine("  transform: rotate(-8deg);");
                sb.AppendLine("}");
                sb.AppendLine(".sign-date {");
                sb.AppendLine("  font-weight: bold;");
                sb.AppendLine("  font-size: 15px;");
                sb.AppendLine("  text-align: left;");
                sb.AppendLine("  margin-left: 100px;");
                sb.AppendLine("}");
                sb.AppendLine("</style>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");

                sb.AppendLine("<div class=\"certificate\">");

                sb.AppendLine("  <div class=\"header-row\">");
                sb.AppendLine($"      <span>{data.SRNO}</span>");
                sb.AppendLine($"      <span>{data.EnrollmentNo}</span>");
                sb.AppendLine("  </div>");

                sb.AppendLine("  <div class=\"body-content\">");
                sb.AppendLine($"      <div class=\"name\">{data.StudentName}</div>");
                sb.AppendLine($"      <div class=\"parent\">{data.FatherName}</div>");

                sb.AppendLine($"      <div class=\"diploma-title\">{data.StreamName}</div>");

                sb.AppendLine("      <div class=\"session-row\">");
                sb.AppendLine($"          <span>{data.FinalDiplomaTermName}</span>");
                sb.AppendLine($"          <span style=\"margin-right:180px;\">{data.Division}</span>");
                sb.AppendLine("      </div>");

                sb.AppendLine($"      <div class=\"division\" style=\"margin-left:60px; text-align:left;\">{data.CourseDuration}</div>");
                sb.AppendLine($"      <div class=\"completion-date\" style=\"margin-left:60px; text-align:left;\">{data.ResultDate}</div>");

                // signature 
                sb.AppendLine($"      <div class=\"signature\"><img src=\"data:{reg_signmime};base64,{reg_signbase64}\" style=\"width:80px;\"/></div>");

                sb.AppendLine($"      <div class=\"sign-date\">{data.DiplomaPrintingDate}</div>");
                sb.AppendLine("  </div>");

                sb.AppendLine("</div>");

                sb.AppendLine("</body>");
                sb.AppendLine("</html>");


                return sb;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion



        #region Provisional Diploma Certificate
        //public async Task<StringBuilder> GetHtmlOfProvisionalCertificate(ProvisionalDiplomaCertificateDownloadSearchModel data)
        //{
        //    try
        //    {
        //        // set sign of registrar                
        //        string reg_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}{data.RegistrarSignFile}";
        //        byte[] reg_signbytes = System.IO.File.ReadAllBytes(CommonFuncationHelper.IsFileExisitsOrDefault(reg_signFilepath));
        //        string reg_signbase64 = Convert.ToBase64String(reg_signbytes);
        //        string reg_signext = Path.GetExtension(reg_signFilepath).ToLower();
        //        string reg_signmime = reg_signext switch
        //        {
        //            ".png" => "image/png",
        //            ".jpg" => "image/jpeg",
        //            ".jpeg" => "image/jpeg",
        //            ".gif" => "image/gif",
        //            _ => "image/png"
        //        };

        //        // set html
        //        StringBuilder sb = new StringBuilder();

        //        sb.AppendLine("<!DOCTYPE html>");
        //        sb.AppendLine("<html lang=\"en\">");
        //        sb.AppendLine("<head>");
        //        sb.AppendLine("<meta charset=\"UTF-8\">");
        //        sb.AppendLine("<title>Diploma Certificate</title>");
        //        sb.AppendLine("<style>");
        //        sb.AppendLine("body {");
        //        sb.AppendLine("  font-family: Arial, Helvetica, sans-serif;");
        //        sb.AppendLine("  background: #f2f2f2;");
        //        sb.AppendLine("  margin: 0;");
        //        sb.AppendLine("  padding: 40px 0;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".certificate {");
        //        sb.AppendLine("  max-width: 850px;");
        //        sb.AppendLine("  min-height: 1100px;");
        //        sb.AppendLine("  margin: 0 auto;");
        //        sb.AppendLine("  background: #fff;");
        //        sb.AppendLine("  padding: 60px 70px;");
        //        sb.AppendLine("  box-sizing: border-box;");
        //        sb.AppendLine("  position: relative;");
        //        sb.AppendLine("  box-shadow: 0 0 8px rgba(0,0,0,0.15);");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".header-row {");
        //        sb.AppendLine("  display: flex;");
        //        sb.AppendLine("  justify-content: space-between;");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 14px;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".body-content {");
        //        sb.AppendLine("  margin-top: 260px;");
        //        sb.AppendLine("  text-align: center;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".name {");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 16px;");
        //        sb.AppendLine("  margin-bottom: 8px;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".parent {");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 16px;");
        //        sb.AppendLine("  margin-bottom: 30px;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".diploma-title {");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 16px;");
        //        sb.AppendLine("  margin-bottom: 40px;");
        //        sb.AppendLine("  text-align: left;");
        //        sb.AppendLine("  margin-left: 60px;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".session-row {");
        //        sb.AppendLine("  display: flex;");
        //        sb.AppendLine("  justify-content: space-between;");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 15px;");
        //        sb.AppendLine("  margin: 0 60px 60px 60px;");
        //        sb.AppendLine("  text-align: left;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".division {");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 15px;");
        //        sb.AppendLine("  margin-bottom: 10px;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".completion-date {");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 15px;");
        //        sb.AppendLine("  margin-bottom: 40px;");
        //        sb.AppendLine("  text-align: left;");
        //        sb.AppendLine("  margin-left: 40px;");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".signature {");
        //        sb.AppendLine("  font-family: cursive;");
        //        sb.AppendLine("  font-size: 34px;");
        //        sb.AppendLine("  text-align: left;");
        //        sb.AppendLine("  margin-left: 220px;");
        //        sb.AppendLine("  margin-bottom: 30px;");
        //        sb.AppendLine("  transform: rotate(-8deg);");
        //        sb.AppendLine("}");
        //        sb.AppendLine(".sign-date {");
        //        sb.AppendLine("  font-weight: bold;");
        //        sb.AppendLine("  font-size: 15px;");
        //        sb.AppendLine("  text-align: left;");
        //        sb.AppendLine("  margin-left: 100px;");
        //        sb.AppendLine("}");
        //        sb.AppendLine("</style>");
        //        sb.AppendLine("</head>");
        //        sb.AppendLine("<body>");

        //        sb.AppendLine("<div class=\"certificate\">");

        //        sb.AppendLine("  <div class=\"header-row\">");
        //        sb.AppendLine($"      <span>{data.SRNO}</span>");
        //        sb.AppendLine($"      <span>{data.EnrollmentNo}</span>");
        //        sb.AppendLine("  </div>");

        //        sb.AppendLine("  <div class=\"body-content\">");
        //        sb.AppendLine($"      <div class=\"name\">{data.StudentName}</div>");
        //        sb.AppendLine($"      <div class=\"parent\">{data.FatherName}</div>");

        //        sb.AppendLine($"      <div class=\"diploma-title\">{data.StreamName}</div>");

        //        sb.AppendLine("      <div class=\"session-row\">");
        //        sb.AppendLine($"          <span>{data.FinalDiplomaTermName}</span>");
        //        sb.AppendLine($"          <span style=\"margin-right:180px;\">{data.Division}</span>");
        //        sb.AppendLine("      </div>");

        //        sb.AppendLine($"      <div class=\"division\" style=\"margin-left:60px; text-align:left;\">{data.CourseDuration}</div>");
        //        sb.AppendLine($"      <div class=\"completion-date\" style=\"margin-left:60px; text-align:left;\">{data.ResultDate}</div>");

        //        // signature 
        //        sb.AppendLine($"      <div class=\"signature\"><img src=\"data:{reg_signmime};base64,{reg_signbase64}\" style=\"width:80px;\"/></div>");

        //        sb.AppendLine($"      <div class=\"sign-date\">{data.DiplomaPrintingDate}</div>");
        //        sb.AppendLine("  </div>");

        //        sb.AppendLine("</div>");

        //        sb.AppendLine("</body>");
        //        sb.AppendLine("</html>");


        //        return sb;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public async Task<StringBuilder> GetHtmlOfProvisionalCertificate(ProvisionalDiplomaCertificateDownloadSearchModel data)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html>");
                sb.AppendLine("<head>");
                sb.AppendLine("<meta charset='UTF-8'>");

                sb.AppendLine("<style>");

                // A4 exact size
                sb.AppendLine("@page {");
                sb.AppendLine("    size: A4;");
                sb.AppendLine("    margin: 0;");
                sb.AppendLine("}");

                sb.AppendLine("* {");
                sb.AppendLine("    box-sizing: border-box;");
                sb.AppendLine("}");

                sb.AppendLine("html, body {");
                sb.AppendLine("    margin: 0;");
                sb.AppendLine("    padding: 0;");
                sb.AppendLine("    width: 210mm;");
                sb.AppendLine("    height: 297mm;");
                sb.AppendLine("    background: transparent;");
                sb.AppendLine("}");

                sb.AppendLine("body {");
                sb.AppendLine("    position: relative;");
                sb.AppendLine("    font-family: Arial, Helvetica, sans-serif;");
                sb.AppendLine("}");

                sb.AppendLine(".value {");
                sb.AppendLine("    position: absolute;");
                sb.AppendLine("    white-space: nowrap;");
                sb.AppendLine("    font-size: 14px;");
                sb.AppendLine("    line-height: 1;");
                sb.AppendLine("}");

                sb.AppendLine("</style>");
                sb.AppendLine("</head>");

                sb.AppendLine("<body>");

                // =========================================================
                // 1. S.NO
                // =========================================================
                //
                // Original position:
                // Top-right area
                //
                sb.AppendLine(
                    $"<div class='value' style='left:174mm; top:11.5mm;'>"
                    + $"{data.SRNO}"
                    + "</div>");


                // =========================================================
                // 2. STUDENT NAME
                // =========================================================
                //
                // Original:
                // AASU SINGH
                //
                sb.AppendLine(
                    $"<div class='value' style='left:89mm; top:69mm;'>"
                    + $"{data.StudentName}"
                    + "</div>");


                // =========================================================
                // 3. FATHER / MOTHER NAME
                // =========================================================
                //
                // S/O SANG SINGH
                //
                sb.AppendLine(
                    $"<div class='value' style='left:89mm; top:76mm;'>"
                    + $"{data.FatherName}"
                    + "</div>");


                // =========================================================
                // 4. ENROLLMENT NO
                // =========================================================
                //
                // CE20220001/001
                //
                sb.AppendLine(
                    $"<div class='value' style='left:87mm; top:103mm;'>"
                    + $"{data.EnrollmentNo}"
                    + "</div>");


                // =========================================================
                // 5. ROLL NO
                // =========================================================
                //
                // 6500003
                //
                sb.AppendLine(
                    $"<div class='value' style='left:168mm; top:103mm;'>"
                    + $"{data.RollNo}"
                    + "</div>");


                // =========================================================
                // 6. DIPLOMA NAME
                // =========================================================
                //
                // DIPLOMA IN CIVIL ENGINEERING
                //
                sb.AppendLine(
                    $"<div class='value' style='left:85mm; top:129mm;'>"
                    + $"{data.StreamName}"
                    + "</div>");


                // =========================================================
                // 7. DIPLOMA / EXAM DATE
                // =========================================================
                //
                // May-2024
                //
                sb.AppendLine(
                    $"<div class='value' style='left:48mm; top:149mm;'>"
                    + $"{data.FinalDiplomaTermName}"
                    + "</div>");


                // =========================================================
                // 8. SESSION
                // =========================================================
                //
                // Session 2024
                //
                sb.AppendLine(
                    $"<div class='value' style='left:48mm; top:156mm;'>"
                    + $"Session {data.SessionName}"
                    + "</div>");


                // =========================================================
                // 9. DIVISION
                // =========================================================
                //
                // First (Honours)
                //
                sb.AppendLine(
                    $"<div class='value' style='left:129mm; top:158mm;'>"
                    + $"{data.Division}"
                    + "</div>");


                // =========================================================
                // 10. COURSE DURATION
                // =========================================================
                //
                // 3 Years
                //
                sb.AppendLine(
                    $"<div class='value' style='left:91mm; top:175mm;'>"
                    + $"{data.CourseDuration}"
                    + "</div>");


                // =========================================================
                // 11. DIPLOMA COMPLETION DATE
                // =========================================================
                //
                // 24-04-2025
                //
                sb.AppendLine(
                    $"<div class='value' style='left:82mm; top:224mm;'>"
                    + $"{data.ResultDate}"
                    + "</div>");


                // =========================================================
                // 12. PRINTING DATE
                // =========================================================
                //
                // 27-11-2025
                //
                sb.AppendLine(
                     $"<div class='value' style='left:51mm; top:247mm;'>"
                     + $"{data.DiplomaPrintingDate:dd-MM-yyyy}"
                     + "</div>");


                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                return sb;
            }
            catch
            {
                throw;
            }
        }

        #endregion



        #region GetGuestHouseSlip_Html
        public async Task<StringBuilder> GetGuestHouseSlip_Html(DataSet ds, int ResultType)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (ds == null || ds.Tables.Count == 0)
                    return sb;

                DataTable dt = ds.Tables[0];

                if (dt == null || dt.Rows.Count == 0)
                    return sb;

                DataRow row = dt.Rows[0];

                string name = row["Name"] != DBNull.Value ? row["Name"].ToString() : "";
                string bookNo = row["BookNo"] != DBNull.Value ? row["BookNo"].ToString() : "";
                string receiptNo = row["ReceiptNo"] != DBNull.Value ? row["ReceiptNo"].ToString() : "";
                string date = "";
                if (dt.Columns.Contains("Date") && row["Date"] != DBNull.Value)
                {
                    // Handles both DateTime values and pre-formatted strings like '15-07-2026'
                    DateTime parsedDate;
                    if (DateTime.TryParse(row["Date"].ToString(), out parsedDate))
                        date = parsedDate.ToString("dd-MM-yyyy");
                    else
                        date = row["Date"].ToString();
                }
                string address = row["Address"] != DBNull.Value ? row["Address"].ToString() : "";
                string roomFee = row["RoomFee"] != DBNull.Value ? row["RoomFee"].ToString() : "";
                string stayDays = row["StayDays"] != DBNull.Value ? row["StayDays"].ToString() : "";
                string totalAmount = row["TotalAmount"] != DBNull.Value ? row["TotalAmount"].ToString() : "";
                string amountInWords = row["AmountInWords"] != DBNull.Value ? row["AmountInWords"].ToString() : "";
                string remark = row["Remark"] != DBNull.Value ? row["Remark"].ToString() : "";

                sb.Append(@"

<!DOCTYPE html>
<html lang='hi'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>रोकड़ पत्र</title>
    <style>
        @page {
            size: A4;
            margin: 10mm;
        }
        * {
            box-sizing: border-box;
        }
        body {
            margin: 0;
            font-family: 'Mangal', Arial, sans-serif;
            background: #f5f5f5;
        }
        .receipt-page {
            width: 100%;
            max-width: 190mm;
            margin: 0 auto;
            background: #f6e3b4;
            border: 1px solid #999;
            padding: 10mm;
            color: #000;
            page-break-inside: avoid;
        }
    </style>
</head>
<body>

    <div class='receipt-page'>

        <!-- Header -->
        <div style=""text-align:center; line-height:1.6;"">
            <div style=""font-size:22px; font-weight:bold;"">रोकड़ - पत्र</div>
            <div style=""font-size:20px;"">राजस्थान सरकार</div>
            <div style=""font-size:18px;"">
                कार्यालय संयुक्त निदेशक, प्राविधिक शिक्षा,<br>
                शिक्षक प्रशिक्षण एवं अधिगम संसाधन विकास केंद्र, जोधपुर
            </div>
        </div>
");

                sb.Append($@"
        <!-- Top Details -->
        <div style=""display:flex; justify-content:space-between; margin-top:15px; font-size:14px;"">
            <div>पुस्तक सं. <b>{bookNo}</b></div>
            <div>रसीद सं. <b>{receiptNo}</b></div>
        </div>

        <div style=""display:flex; justify-content:end; margin-bottom:3px; font-size:14px;"">
            <div>दिनांक : <b>{date}</b></div>
        </div>

        <div style=""display:flex; justify-content:space-between; margin-top:7px; font-size:14px;"">
            <div style=""width: 40px;"">नाम : </div>
            <div style=""width: calc(100% - 45px);border-bottom:1px dotted #000; padding:0 10px;""><span>{name}</span></div>
        </div>

        <div style=""display:flex; justify-content:space-between; margin-top:7px; font-size:14px;"">
            <div style=""width: 40px;"">पता : </div>
            <div style=""width: calc(100% - 45px);border-bottom:1px dotted #000; padding:0 10px;""><span>{address}</span></div>
        </div>

        <!-- Table -->
        <table style=""width:100%; border-collapse:collapse; margin-top:15px; font-size:14px;"" border='1'>
            <tr>
                <th style=""padding:8px;"">क्र. सं.</th>
                <th style=""padding:8px;"">विवरण</th>
                <th style=""padding:8px;"">दर</th>
                <th style=""padding:8px;"">दिनों / पुस्तकों की संख्या</th>
                <th style=""padding:8px;"">कुल राशि रुपये</th>
            </tr>

            <tr style=""height:50px;"">
                <td style=""text-align:center;"">1</td>
                <td>अतिथि गृह सुविधा</td>
                <td style=""text-align:center;"">{roomFee}</td>
                <td style=""text-align:center;"">{stayDays}</td>
                <td style=""text-align:center;"">{totalAmount}</td>
            </tr>

            <tr style=""height:50px;"">
                <td style=""text-align:center;"">2</td>
                <td>प्रशिक्षण शुल्क</td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

            <tr style=""height:50px;"">
                <td style=""text-align:center;"">3</td>
                <td>लेब मैनुअल</td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

            <tr style=""height:50px;"">
                <td style=""text-align:center;"">4</td>
                <td>अन्य</td>
                <td></td>
                <td></td>
                <td></td>
            </tr>

            <tr style=""height:100px;"">
                <td colspan='4' style=""padding:10px; vertical-align:bottom;"">
                    (अक्षर रु. : <b>{amountInWords}</b>)
                </td>
                <td style=""text-align:center; font-weight:bold; vertical-align:bottom;"">
                    योग : {totalAmount}
                </td>
            </tr>
        </table>
");

                if (!string.IsNullOrWhiteSpace(remark))
                {
                    sb.Append($@"
        <!-- Remark -->
        <div style=""margin-top:10px; font-size:14px;"">
            टिप्पणी : <b>{remark}</b>
        </div>
");
                }

                sb.Append(@"
        <!-- Footer -->
        <div style=""display:flex; justify-content:space-between; margin-top:30px; font-size:14px;"">
            <div>रा.मु.प्र. 101-2015-16,20 बुक</div>
            <div style=""text-align:center;"">
                हस्ताक्षर
            </div>
        </div>

    </div>

</body>
</html>
");

                return sb;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating Guest House Cash Slip Report.", ex);
            }
        }
        #endregion

        #region Certificate letter

        public async Task<StringBuilder> GetTemporaryDiplomaCertificateHtml(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                // =========================================================
                // VALIDATION
                // =========================================================

                if (ds == null || ds.Tables.Count < 2)
                    return sb;

                DataTable dtHeader = ds.Tables[0];
                DataTable dtData = ds.Tables[1];

                if (dtHeader == null || dtHeader.Rows.Count == 0)
                    return sb;

                if (dtData == null || dtData.Rows.Count == 0)
                    return sb;


                // =========================================================
                // HEADER DATA - TABLE 0
                // =========================================================

                DataRow header = dtHeader.Rows[0];

                string registrationNo =
                    header["RegistrationNo"]?.ToString()?.Trim() ?? "";

                string reportDate =
                    header["Date"]?.ToString()?.Trim() ?? "";

                string instituteName =
                    header["InstituteName"]?.ToString()?.Trim() ?? "";

                string instituteCode =
                    header["InstituteCode"]?.ToString()?.Trim() ?? "";

                string sessionName =
                    header["FinancialYearName"]?.ToString()?.Trim() ?? "";

                string EndTermHindi =
                    header["EndTermHindi"]?.ToString()?.Trim() ?? "";

                string YearName =
                    header["YearName"]?.ToString()?.Trim() ?? "";

                string subject =
                    header["Subject"]?.ToString()?.Trim()
                    ?? "अस्थाई डिप्लोमा प्रमाण पत्र एवं प्रव्रजन प्रमाण पत्र भिजवाने बाबत ।";


                // =========================================================
                // PAGE CONFIGURATION
                // =========================================================

                // Page 1 has letter header, therefore fewer rows.
                int firstPageRows = 29;

                // Page 2 onwards contains only table.
                int otherPageRows = 41;


                // =========================================================
                // TOTAL PAGE COUNT - DYNAMIC
                // =========================================================

                int totalRows = dtData.Rows.Count;

                int pageCount;

                if (totalRows <= firstPageRows)
                {
                    pageCount = 1;
                }
                else
                {
                    int remainingRows = totalRows - firstPageRows;

                    pageCount =
                        1 + (int)Math.Ceiling(
                            (double)remainingRows / otherPageRows
                        );
                }


                // =========================================================
                // HTML
                // =========================================================

                sb.Append(@"
<!DOCTYPE html>
<html lang='hi'>

<head>

<meta charset='UTF-8'>

<title>
अस्थाई डिप्लोमा प्रमाण पत्र एवं प्रव्रजन प्रमाण पत्र
</title>

<style>

@page {
    size: A4;
    margin: 10mm;
}

body {
    margin: 0;
    padding: 0;
    color: #000;
    font-family: Arial, 'Noto Sans Devanagari', sans-serif;
    font-size: 18px;
}

.page {
    width: 100%;
    box-sizing: border-box;
    page-break-after: always;
}

.page:last-child {
    page-break-after: auto;
}


/* =========================================================
   LETTER HEADER
   ========================================================= */

.header {
    width: 100%;
    text-align: center;
}

.reg {
    text-align: right;
    font-size: 18px;
}

.govt {
    font-size: 18px;
    margin-top: 5px;
}

.board {
    font-size: 21px;
    font-weight: bold;
    margin-top: 5px;
}


/* =========================================================
   TOP INFORMATION
   ========================================================= */

.top-table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 22px;
}

.top-table td {
    border: none;
    padding: 0;
    vertical-align: top;
}

.left {
    text-align: left;
}

.right {
    text-align: right;
}

.principal {
    font-size: 18px;
}

.college {
    font-size: 18px;
    margin-top: 8px;
}

.institute-code {
    font-size: 21px;
    font-weight: bold;
    margin-top: 15px;
}


/* =========================================================
   SUBJECT
   ========================================================= */

.subject {
    text-align: left;
    font-size: 18px;
    margin-top: 25px;
}

.body-text {
    text-align: left;
    font-size: 18px;
    margin-top: 10px;
    line-height: 1.5;
}


/* =========================================================
   STUDENT TABLE
   ========================================================= */

.student-table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 8px;
    font-size: 16px;
}

.student-table th,
.student-table td {
    border: 1px solid #b5b5b5;
    padding: 5px 6px;
    vertical-align: middle;
}

.student-table th {
    text-align: center;
    font-weight: bold;
}

.student-table td {
    text-align: left;
}

.center {
    text-align: center !important;
}

.student-table tr {
    page-break-inside: avoid;
}


/* This is useful if PDF engine itself breaks the table */
.student-table thead {
    display: table-header-group;
}

</style>

</head>

<body>
");


                // =========================================================
                // SERIAL NUMBER
                // =========================================================

                int sno = 1;


                // =========================================================
                // LOOP THROUGH DYNAMIC PAGES
                // =========================================================

                for (int pageNo = 1; pageNo <= pageCount; pageNo++)
                {
                    int skip;
                    int take;

                    if (pageNo == 1)
                    {
                        // First page
                        skip = 0;
                        take = firstPageRows;
                    }
                    else
                    {
                        // Other pages
                        skip =
                            firstPageRows +
                            ((pageNo - 2) * otherPageRows);

                        take = otherPageRows;
                    }


                    var pageData = dtData
                        .AsEnumerable()
                        .Skip(skip)
                        .Take(take)
                        .ToList();


                    // =====================================================
                    // PAGE START
                    // =====================================================

                    sb.Append("<div class='page'>");


                    // =====================================================
                    // LETTER HEADER - ONLY FIRST PAGE
                    // =====================================================

                    if (pageNo == 1)
                    {
                        sb.Append($@"

<div class='header'>

    <div class='reg'>
        रजि.
    </div>

    <div class='govt'>
        राजस्थान सरकार
    </div>

    <div class='board'>
        प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर
    </div>

</div>


<table class='top-table'>

<tr>

    <td class='left' style='width:60%;'>
        क्रमांक :
        {System.Net.WebUtility.HtmlEncode(registrationNo)}
    </td>

    <td class='right' style='width:40%;'>
        दिनांक:
        {System.Net.WebUtility.HtmlEncode(reportDate)}
    </td>

</tr>


<tr>

    <td class='left' style='padding-top:25px;'>
        <div class='principle'>
            PRINCIPLE
        </div>
        <div class='college'>
            {System.Net.WebUtility.HtmlEncode(instituteName)}
        </div>

    </td>


    <td class='right' style='padding-top:25px;'>

        <div class='institute-code'>
            संस्थान कोड-{System.Net.WebUtility.HtmlEncode(instituteCode)}
        </div>

    </td>

</tr>

</table>


<div class='subject'>

    विषय :
    {System.Net.WebUtility.HtmlEncode(subject)}

</div>


<div class='body-text'>
    महोदय,
</div>


<div class='body-text' style='padding-left:55px;'>

    इस पत्र के साथ आपको
    {System.Net.WebUtility.HtmlEncode(EndTermHindi)} सत्र {System.Net.WebUtility.HtmlEncode(sessionName)}
    परीक्षा के अस्थाई डिप्लोमा प्रमाण पत्र एवं प्रव्रजन प्रमाण पत्र
    निम्नानुसार भिजवाये जा रहे हैं :-

</div>

");
                    }


                    // =====================================================
                    // TABLE
                    // =====================================================

                    sb.Append(@"
<table class='student-table'>

<thead>

<tr>

    <th rowspan='2' style='width:40px;'>
        क्र.सं.
    </th>

    <th rowspan='2' style='width:150px;'>
        नामांकन नंबर
    </th>

    <th rowspan='2' style='width:220px;'>
        विद्यार्थी का नाम
    </th>

    <th rowspan='2' style='width:130px;'>
        प्रव्रजन प्रमाण पत्र
    </th>

    <th style='width:130px;'>
        अस्थाई डिप्लोमा </br>प्रमाण पत्र
    </th>

</tr>


</thead>

<tbody>
");


                    // =====================================================
                    // TABLE DATA
                    // =====================================================

                    foreach (DataRow row in pageData)
                    {
                        string enrollmentNo =
                            row["EnrollmentNo"]?.ToString()?.Trim() ?? "";

                        string studentName =
                            row["StudentName"]?.ToString()?.Trim() ?? "";

                        string managementCertificateNo =
                            row["ManagementCertificateNo"]?.ToString()?.Trim() ?? "";

                        string temporaryDiplomaCertificateNo =
                            row["TemporaryDiplomaCertificateNo"]?.ToString()?.Trim() ?? "";


                        sb.Append($@"

<tr>

    <td class='center'>
        {sno}
    </td>

    <td>
        {System.Net.WebUtility.HtmlEncode(enrollmentNo)}
    </td>

    <td>
        {System.Net.WebUtility.HtmlEncode(studentName)}
    </td>

    <td class='center'>
        {System.Net.WebUtility.HtmlEncode(managementCertificateNo)}
    </td>

    <td class='center'>
        {System.Net.WebUtility.HtmlEncode(temporaryDiplomaCertificateNo)}
    </td>

</tr>

");

                        sno++;
                    }


                    sb.Append(@"
</tbody>

</table>

</div>
");
                }


                // =========================================================
                // HTML END
                // =========================================================

                sb.Append(@"
</body>
</html>
");


                return sb;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error generating Temporary Diploma Certificate HTML.",
                    ex
                );
            }
        }
        #endregion
    }
}
