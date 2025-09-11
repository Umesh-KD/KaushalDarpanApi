
using Kaushal_Darpan.Core.Helper;
using System.Text;

namespace Kaushal_Darpan.Api.HtmlTempleteFile
{
    public class PrintHtmlFile : IPrintHtmlFile
    {
        #region Test
        public string Dummy_CreatePDF()
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

                string html = sb.ToString();

                return html;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Result Tabulation
        public string GetHtmlOfResultTabulation()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"en\">");
                sb.AppendLine("<head>");
                sb.AppendLine("    <meta charset=\"UTF-8\">");
                sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sb.AppendLine("    <title>Tabulation Register</title>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                sb.AppendLine("    <div style=\"width: 98%; margin: auto;\">");
                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"5\" style=\"width:100%; border-collapse:collapse; border: 1px solid #c3c3c3; font-family:Arial, sans-serif; font-size:14px;\">");
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td style=\"width:20%;\"></td>");
                sb.AppendLine("                <td style=\"width:60%; text-align:center; line-height:1.5;\">");
                sb.AppendLine("                    <strong>Government of Rajasthan</strong><br>");
                sb.AppendLine("                    <strong>Board of Technical Education, Rajasthan, Jodhpur</strong><br>");
                sb.AppendLine("                    <strong>Tabulation Register - Second Semester - Diploma Engineering Exam End Term May 2024 Session 2023-2024</strong>");
                sb.AppendLine("                </td>");
                sb.AppendLine("                <td style=\"width:20%; text-align:right; vertical-align:bottom;\">");
                sb.AppendLine("                    <strong>Date of Result Declaration</strong><br>");
                sb.AppendLine("                    <strong>09/08/2024</strong>");
                sb.AppendLine("                </td>");
                sb.AppendLine("            </tr>");
                sb.AppendLine("        </table>");

                sb.AppendLine("        <table cellspacing=\"0\" cellpadding=\"2\" style=\"width:100%; border-collapse:collapse; font-family:Arial, sans-serif; font-size:14px;\">");

                // College & Programme Info
                sb.AppendLine("            <tr style=\"border-bottom: 1px solid #000;\">");
                sb.AppendLine("                <td colspan=\"14\" style=\"padding-left: 0;\"><strong>Govt. Polytechnic College, Ajmer(001)</strong></td>");
                sb.AppendLine("                <td colspan=\"10\"><strong>PROGRAMME : CIVIL ENGINEERING (CE)</strong></td>");
                sb.AppendLine("            </tr>");

                // Main Header Row
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <th>SL.No.</th>");
                sb.AppendLine("                <th>ENROLLMENT NO</th>");
                sb.AppendLine("                <th>SUB.</th>");
                sb.AppendLine("                <th>2001</th>");
                sb.AppendLine("                <th>2002</th>");
                sb.AppendLine("                <th>2003</th>");
                sb.AppendLine("                <th>2004</th>");
                sb.AppendLine("                <th>2005</th>");
                sb.AppendLine("                <th>2006</th>");
                sb.AppendLine("                <th>2007</th>");
                sb.AppendLine("                <th>2008</th>");
                sb.AppendLine("                <th>2009</th>");
                sb.AppendLine("                <th>2010</th>");
                sb.AppendLine("                <th>2222</th>");
                sb.AppendLine("                <th>Total</th>");
                sb.AppendLine("                <th>SCA</th>");
                sb.AppendLine("                <th colspan=\"8\">Result Details</th>");
                sb.AppendLine("            </tr>");

                // Max/Min Theory
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td>Roll No.</td>");
                sb.AppendLine("                <td>NameOfCandidate</td>");
                sb.AppendLine("                <td>Max/Min TH</td>");
                sb.AppendLine("                <td>60/17</td>");
                sb.AppendLine("                <td>60/15</td>");
                sb.AppendLine("                <td>60/14</td>");
                sb.AppendLine("                <td>60/13</td>");
                sb.AppendLine("                <td>60/15</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>I Sem</td>");
                sb.AppendLine("                <td>II Sem</td>");
                sb.AppendLine("                <td>III Sem</td>");
                sb.AppendLine("                <td>IV Sem</td>");
                sb.AppendLine("                <td>V Sem</td>");
                sb.AppendLine("                <td>VI Sem</td>");
                sb.AppendLine("                <td>DIV</td>");
                sb.AppendLine("                <td>Result</td>");
                sb.AppendLine("            </tr>");

                // Max/Min Practical
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td>Cat</td>");
                sb.AppendLine("                <td>Father's Name</td>");
                sb.AppendLine("                <td>MAX/Min PR</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>40/10</td>");
                sb.AppendLine("                <td>40/10</td>");
                sb.AppendLine("                <td>40/10</td>");
                sb.AppendLine("                <td>40/10</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>PRT</td>");
                sb.AppendLine("                <td>EC</td>");
                sb.AppendLine("                <td>EC</td>");
                sb.AppendLine("                <td>EC</td>");
                sb.AppendLine("                <td>EC</td>");
                sb.AppendLine("                <td>EC</td>");
                sb.AppendLine("                <td>EC</td>");
                sb.AppendLine("                <td>PER</td>");
                sb.AppendLine("                <td>(R/P/F)</td>");
                sb.AppendLine("            </tr>");

                // IA Row
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td>Attempt</td>");
                sb.AppendLine("                <td>Mother's Name</td>");
                sb.AppendLine("                <td>IA</td>");
                sb.AppendLine("                <td>40</td>");
                sb.AppendLine("                <td>40</td>");
                sb.AppendLine("                <td>40</td>");
                sb.AppendLine("                <td>40</td>");
                sb.AppendLine("                <td>40</td>");
                sb.AppendLine("                <td>60</td>");
                sb.AppendLine("                <td>60</td>");
                sb.AppendLine("                <td>60</td>");
                sb.AppendLine("                <td>60</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Project</td>");
                sb.AppendLine("                <td>SGPA</td>");
                sb.AppendLine("                <td>SGPA</td>");
                sb.AppendLine("                <td>SGPA</td>");
                sb.AppendLine("                <td>SGPA</td>");
                sb.AppendLine("                <td>SGPA</td>");
                sb.AppendLine("                <td>SGPA</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                // Totals Row
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Gender/Date of Birth</td>");
                sb.AppendLine("                <td>Total</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>100</td>");
                sb.AppendLine("                <td>1000</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>CGPA</td>");
                sb.AppendLine("                <td>CGPA</td>");
                sb.AppendLine("                <td>CGPA</td>");
                sb.AppendLine("                <td>CGPA</td>");
                sb.AppendLine("                <td>CGPA</td>");
                sb.AppendLine("                <td>CGPA</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                // Credit Row
                sb.AppendLine("            <tr style=\"border-bottom: 1px solid #000;\">");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Elective Subjects</td>");
                sb.AppendLine("                <td>Reg Credit</td>");
                sb.AppendLine("                <td>5.00</td>");
                sb.AppendLine("                <td>4.00</td>");
                sb.AppendLine("                <td>3.00</td>");
                sb.AppendLine("                <td>4.00</td>");
                sb.AppendLine("                <td>4.00</td>");
                sb.AppendLine("                <td>1.00</td>");
                sb.AppendLine("                <td>2.00</td>");
                sb.AppendLine("                <td>2.00</td>");
                sb.AppendLine("                <td>1.00</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>2.00</td>");
                sb.AppendLine("                <td>28</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>End Term</td>");
                sb.AppendLine("                <td>End Term</td>");
                sb.AppendLine("                <td>End Term</td>");
                sb.AppendLine("                <td>End Term</td>");
                sb.AppendLine("                <td>End Term</td>");
                sb.AppendLine("                <td>End Term</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                // Student Marks Rows (ALL)
                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td>1</td>");
                sb.AppendLine("                <td>CE20230001/001</td>");
                sb.AppendLine("                <td>TH.</td>");
                sb.AppendLine("                <td>20</td>");
                sb.AppendLine("                <td>08</td>");
                sb.AppendLine("                <td>19</td>");
                sb.AppendLine("                <td>10</td>");
                sb.AppendLine("                <td>20</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>R</td>");
                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td>2200001</td>");
                sb.AppendLine("                <td>Chirag Jadam</td>");
                sb.AppendLine("                <td>PR.</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>30</td>");
                sb.AppendLine("                <td>30</td>");
                sb.AppendLine("                <td>24</td>");
                sb.AppendLine("                <td>30</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td>Reg.</td>");
                sb.AppendLine("                <td>Mahesh Chand Jadam</td>");
                sb.AppendLine("                <td>IA.</td>");
                sb.AppendLine("                <td>18</td>");
                sb.AppendLine("                <td>20</td>");
                sb.AppendLine("                <td>29</td>");
                sb.AppendLine("                <td>30</td>");
                sb.AppendLine("                <td>29</td>");
                sb.AppendLine("                <td>49</td>");
                sb.AppendLine("                <td>37</td>");
                sb.AppendLine("                <td>47</td>");
                sb.AppendLine("                <td>45</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>80</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>18.00</td>");
                sb.AppendLine("                <td>20.00</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Manju Jadam</td>");
                sb.AppendLine("                <td>Total /RMI</td>");
                sb.AppendLine("                <td>38/48</td>");
                sb.AppendLine("                <td>28/35</td>");
                sb.AppendLine("                <td>48/69</td>");
                sb.AppendLine("                <td>40/58</td>");
                sb.AppendLine("                <td>49/62</td>");
                sb.AppendLine("                <td>79/99</td>");
                sb.AppendLine("                <td>67/84</td>");
                sb.AppendLine("                <td>67/84</td>");
                sb.AppendLine("                <td>75/94</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>80/100</td>");
                sb.AppendLine("                <td>571/733</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>4.80</td>");
                sb.AppendLine("                <td>5.29</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Male 17-10-2005</td>");
                sb.AppendLine("                <td>EC/Grade</td>");
                sb.AppendLine("                <td>5/D+</td>");
                sb.AppendLine("                <td>0/F</td>");
                sb.AppendLine("                <td>3/C+</td>");
                sb.AppendLine("                <td>0/F</td>");
                sb.AppendLine("                <td>4/C+</td>");
                sb.AppendLine("                <td>1/A+</td>");
                sb.AppendLine("                <td>2/B+</td>");
                sb.AppendLine("                <td>2/B+</td>");
                sb.AppendLine("                <td>1/A+</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>2/A+</td>");
                sb.AppendLine("                <td>20</td>");
                sb.AppendLine("                <td>A</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                sb.AppendLine("            <tr>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>GP/PS</td>");
                sb.AppendLine("                <td>8.5/42.5</td>");
                sb.AppendLine("                <td>7.5/30</td>");
                sb.AppendLine("                <td>9/27</td>");
                sb.AppendLine("                <td>7.5/30</td>");
                sb.AppendLine("                <td>8.5/34</td>");
                sb.AppendLine("                <td>10/10</td>");
                sb.AppendLine("                <td>10/20</td>");
                sb.AppendLine("                <td>8.5/17</td>");
                sb.AppendLine("                <td>10/10</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>10/20</td>");
                sb.AppendLine("                <td>66/148</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Nov23</td>");
                sb.AppendLine("                <td>May24</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("            </tr>");

                // Final Result Row
                sb.AppendLine("            <tr style=\"border-bottom: 2px dotted #000;\">");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Result</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>Fail</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>Fail</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>Pass</td>");
                sb.AppendLine("                <td>R</td>");
                sb.AppendLine("                <td></td>");
                sb.AppendLine("                <td>R</td>");
                sb.AppendLine("                <td>R</td>");
                sb.AppendLine("                <td colspan=\"6\"><strong>REGULATION:</strong>2002,2004</td>");
                sb.AppendLine("            </tr>");

                sb.AppendLine("        </table>");
                sb.AppendLine("    </div>");
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");

                string html = sb.ToString();

                return html;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
