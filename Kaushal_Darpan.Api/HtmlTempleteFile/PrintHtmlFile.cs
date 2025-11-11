
using Kaushal_Darpan.Core.Helper;
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
        public StringBuilder GetHtmlOfHeadingAndTabularForTabulation(DataRow streams_dr, DataTable heading_dt, DataSet tabular_ds)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                                
                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"5\" style=\"width:100%; border-collapse:collapse; border: 1px solid #c3c3c3; font-family:Arial, sans-serif; font-size:14px;\">");
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td style=\"width:20%;\"></td>");
                sb.AppendLine("                <td style=\"width:60%; text-align:center; line-height:1.5;\">");
                sb.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_1"]}</strong><br>");
                sb.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_2"]}</strong><br>");
                sb.AppendLine($"                    <strong>{heading_dt.Rows[0]["Heading_3"]}</strong>");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"width:20%; text-align:right; vertical-align:bottom;\">");
                sb.AppendLine("                    <strong>Date of Result Declaration</strong><br>");
                sb.AppendLine("                    <strong>09/08/2024</strong>");
                sb.AppendLine("                </td>");
                sb.AppendLine("            </tr>");
                sb.AppendLine("        </table>");
                
                // table -1
                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\">");
                sb.AppendLine("            <tr style=\"border-bottom: 1px solid #000;\">");
                sb.AppendLine("                <td colspan=\"14\" style=\"padding-left: 0;\"><strong>Govt. Polytechnic College, Ajmer(001)</strong></td>");
                sb.AppendLine($"                <td colspan=\"10\"><strong>PROGRAMME : ({streams_dr["Code"]}){streams_dr["Name"]}</strong></td>");
                sb.AppendLine("            </tr>");

                //column
                // table -2
                sb.AppendLine("            <tr>");
                foreach (DataColumn dc in tabular_ds.Tables[0].Columns)
                {
                    sb.AppendLine($"                <th style=\"text-align:left;\">{dc.ColumnName}</th>");
                }
                sb.AppendLine("            </tr>");

                //row
                //column data
                int headerRowBlockCount = 4;//header row block separation line count (start from 0)
                int dataRowBlockCount = 7;//data row block separation line count 
                int lineBlockCount = headerRowBlockCount;//set header default 
                int i = 0;
                string seprationCls = string.Empty;
                foreach (DataRow dr in tabular_ds.Tables[0].Rows)
                {
                    sb.AppendLine($"            <tr {seprationCls}>");
                    foreach (DataColumn dc in tabular_ds.Tables[0].Columns)
                    {
                        sb.AppendLine($"                <td>{dr[dc.ColumnName]}</td>");
                    }
                    sb.AppendLine("            </tr>");

                    // for block separation line
                    seprationCls = string.Empty;//reset after print
                    ++i;
                    if (i == lineBlockCount)
                    {
                        seprationCls = $"style=\"border-bottom: 2px dotted #000;\"";//set to data row block separation line count 
                        if (lineBlockCount == headerRowBlockCount)
                        {
                            seprationCls = $"style=\"border-bottom: 2px solid #000;\"";//set to header row block separation line count
                        }
                        i = 0;//reset
                        lineBlockCount = dataRowBlockCount;//shift to data row block separation line count
                    }
                }
                sb.AppendLine("        </table>");

                sb.AppendLine("</br>");

                // table -3
                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\">");

                //column
                // Main Header Row
                sb.AppendLine("            <tr>");
                foreach (DataColumn dc in tabular_ds.Tables[1].Columns)
                {
                    sb.AppendLine($"                <th style=\"text-align:left;\">{dc.ColumnName}</th>");
                }
                sb.AppendLine("            </tr>");

                //row
                //column data
                foreach (DataRow dr in tabular_ds.Tables[1].Rows)
                {
                    sb.AppendLine($"            <tr>");
                    foreach (DataColumn dc in tabular_ds.Tables[1].Columns)
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
        public StringBuilder GetHtmlOfConsolidateForTabulation(DataTable consolidate_dt)
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
    }
}
