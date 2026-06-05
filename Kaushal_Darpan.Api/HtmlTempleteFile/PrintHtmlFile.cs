
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.TheoryMarks;
using Org.BouncyCastle.Utilities;
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
                            else if (colval?.ToLower()?.StartsWith("regul. sub.") == true)
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
                                            CenterCode = row["CenterCode"],
                                            BranchName = row["BranchName"],
                                            SubjectName = row["SubjectName"],
                                            SubjectCode = row["SubjectCode"],
                                            MaximumMarks = row["MAXIMUM_MARKS"], // FIXED
                                            ExaminerName = row["ExaminerName"],
                                            MobileNo = row["MobileNo"],
                                            Designation = row["Designation"],
                                            SessionName = row["SessionName"]
                                        })
                                        .OrderBy(g => g.Key.ExaminerCode)
                                        .ThenBy(g => g.Key.GroupCode)
                                        .ThenBy(g => g.Key.CenterCode)
                                        .ThenBy(g => g.Key.BranchName)
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

                    _snoKeyCodeOrg = $"{header.GroupCode}-{header.CenterCode}";
                    // group code different then reset
                    if (_snoKeyCodeOrg != _snoKeyCodeDiff)
                    {
                        sno = 1;
                    }
                    _snoKeyCodeDiff = _snoKeyCodeOrg;

                    // pagging
                    int pageSize = 20;
                    int totalRecords = group.Count();
                    int pageCount = (int)Math.Ceiling((double)totalRecords / pageSize);

                    var orderedData = group
                        .OrderBy(x => x["RollNo"])
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
                            <div>CC Code : <b>{header.CenterCode}</b></div>
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
                            <th style='border: 1px solid #ccc; padding: 5px; width: 150px;'>Roll No</th>
                            <th colspan='2' style='border: 1px solid #ccc; padding: 5px;'>MARKS OBTAINED</th>
                            </tr>
                            <tr>
                            <th style='border: 1px solid #ccc; padding: 5px;'></th>
                            <th style='border: 1px solid #ccc; padding: 5px;'></th>
                            <th style='border: 1px solid #ccc; padding: 5px; width: 50%;'>In Words</th>
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

        public async Task<StringBuilder> UFMCategoryReportPdf_BTER_GetHtml(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable dt = ds.Tables[0];

                var groupedData = dt.AsEnumerable()
                    .GroupBy(x => new
                    {
                        UFMCategory = x["UFMCategory"].ToString(),
                        UFMCategoryName = x["UFMCategoryName"].ToString()
                    })
                    .OrderBy(x => Convert.ToInt32(x.Key.UFMCategory))
                    .ToList();

                sb.Append(@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='utf-8'>
                        <style>
                            body{
                                font-family:'Nirmala UI','Mangal',Arial;
                                font-size:14px;
                                margin:30px;
                                line-height:1.7;
                            }

                            .header{
                                text-align:center;
                                font-weight:bold;
                                font-size:18px;
                            }

                            .office-order{
                                text-align:center;
                                font-weight:bold;
                                font-size:20px;
                                margin-top:20px;
                                margin-bottom:20px;
                            }

                            .roll-table{
                                width:100%;
                                border-collapse:collapse;
                                margin-top:10px;
                                margin-bottom:20px;
                            }

                            .roll-table td{
                                padding:6px;
                                text-align:center;
                                font-weight:bold;
                                width:25%;
                            }

                            .signature{
                                text-align:right;
                                margin-top:40px;
                                font-weight:bold;
                            }
                        </style>
                    </head>
                    <body>");

                // Header
                sb.Append(@"
                            <div class='header'>
                                राजस्थान सरकार<br/>
                                प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर
                            </div>

                            <div class='office-order'>
                                कार्यालय आदेश
                            </div>");

                // Category Wise Data
                foreach (var group in groupedData)
                {
                    sb.Append($@"
                            <div style='margin-top:20px; text-align:justify;'>
                                सत्र 2024-25 के अनुचित साधन के मामलों की समिति द्वारा लिये गये
                                निर्णयानुसार निम्नांकित परीक्षार्थियों को दण्ड सारणी श्रेणी
                                <b>{group.Key.UFMCategoryName}</b>
                                के अन्तर्गत दण्डित किया जाता है :-
                            </div>");

                    sb.Append("<table class='roll-table'>");

                    int count = 0;

                    foreach (var row in group)
                    {
                        if (count % 4 == 0)
                        {
                            sb.Append("<tr>");
                        }

                        sb.Append($@"
                        <td>
                            {row["RollNo"]}
                        </td>");

                        count++;

                        if (count % 4 == 0)
                        {
                            sb.Append("</tr>");
                        }
                    }

                    if (count % 4 != 0)
                    {
                        sb.Append("</tr>");
                    }

                    sb.Append("</table>");
                }

                // Signature
                sb.Append(@"
                    <div class='signature'>
                        <br/><br/>
                        (रघुनाथ सिंह)<br/>
                        संयुक्त निदेशक (गोपनीय)
                    </div>

                    </body>
                    </html>");

                return sb;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating UFM Officer Order HTML", ex);
            }
        }
        #endregion

    }
}
