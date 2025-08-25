using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities.Encoders;
using System.Data;
using System.IO.Compression;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidationActionFilter]
    public class ITICollegeMarksheetDownloadController : BaseController
    {
        public override string PageName => "ITICollegeMarksheetDownloadController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITICollegeMarksheetDownloadController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region ITI GetITICollegeStudent_Marksheet
        //[HttpPost("GetITICollegeStudent_Marksheet")]
        //public async Task<ApiResult<string>> GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)
        //{
        //    ActionName = "GetITICollegeStudent_Marksheet(StudentMarksheetSearchModel model)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {

        //            var data = await _unitOfWork.ITICollegeMarksheetDownloadRepository.GetITICollegeStudent_Marksheet(model);
        //            if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
        //            {
        //                //var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //                ////report
        //                //var fileName = $"JoiningLetter_{model.UserID}_{model.StaffID}.pdf";
        //                //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

        //                //provider                      
        //                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                //images
        //                data.Tables[0].TableName = "GetITIStudent_Marksheet_SingleDetails";

        //                data.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
        //                data.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
        //                data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


        //                data.Tables[1].TableName = "GetITIStudent_Marksheet_Details";

        //                string devFontSize = "15px";
        //                /*default font size for kruti dev*/
        //                //string fontSize = "font-size: 10px;";
        //                System.Text.StringBuilder sb = new System.Text.StringBuilder();


        //                string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/ITIMarksheet.html";

        //                string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

        //                System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

        //                html = Utility.PDFWorks.ReplaceCustomTag(html);

        //                //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
        //                sb1.Append(html);


        //                var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

        //                byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);

        //                // Example: Send in API
        //                //return File(pdfBytes, "application/pdf", "Generated.pdf");


        //                ///string dataUri = "data:application/pdf;base64," + base64String;
        //                result.Data = Convert.ToBase64String(pdfBytes); ;
        //                result.State = EnumStatus.Success;
        //                result.Message = "Success";
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            //
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}


        //[HttpPost("GetITICollegeStudent_Marksheet")]
        //public async Task<IActionResult> GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)
        //{
        //    ActionName = "GetITICollegeStudent_Marksheet(StudentMarksheetSearchModel model)";
        //    //return await Task.Run(async () =>
        //    //{
        //    var result = new ApiResult<string>();
        //    try
        //    {
        //        var data = await _unitOfWork.ITICollegeMarksheetDownloadRepository.GetRollNumberOfStudentOfCollege(model);
        //        if (data?.Rows?.Count > 0)
        //        {
        //            List<string> rollnumbers = new List<string>();
        //            string folderPath = "";
        //            for (int i = 0; i < data.Rows.Count; i++)
        //            {
        //                var row = data.Rows[i];
        //                rollnumbers.Add(row["RollNumber"].ToString());
        //                model.RollNumber = row["RollNumber"].ToString();
        //                DataSet dataSet = await _unitOfWork.ITICollegeMarksheetDownloadRepository.GetITICollegeStudent_Marksheet(model);
        //                if (dataSet?.Tables?.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        //                {
        //                    //var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //                    ////report
        //                    //var fileName = $"JoiningLetter_{model.UserID}_{model.StaffID}.pdf";
        //                    //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                    //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

        //                    //provider                      
        //                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                    //images
        //                    dataSet.Tables[0].TableName = "GetITIStudent_Marksheet_SingleDetails";

        //                    dataSet.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
        //                    dataSet.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
        //                    dataSet.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + dataSet.Tables[0].Rows[0]["signlogo"];


        //                    dataSet.Tables[1].TableName = "GetITIStudent_Marksheet_Details";

        //                    string devFontSize = "15px";
        //                    /*default font size for kruti dev*/
        //                    //string fontSize = "font-size: 10px;";
        //                    System.Text.StringBuilder sb = new System.Text.StringBuilder();


        //                    string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/ITIMarksheet.html";

        //                    string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, dataSet);

        //                    System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

        //                    html = Utility.PDFWorks.ReplaceCustomTag(html);

        //                    //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
        //                    sb1.Append(html);


        //                    var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

        //                    byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);

        //                    folderPath = $"{ConfigurationHelper.StaticFileRootPath}/Report/ITI/ResultTest";
        //                    if (!Directory.Exists(folderPath))
        //                    {
        //                        Directory.CreateDirectory(folderPath);
        //                    }
        //                    if (System.IO.File.Exists($"{folderPath}/{model.RollNumber}.pdf"))
        //                    {
        //                        System.IO.File.Delete($"{folderPath}/{model.RollNumber}.pdf");
        //                    }
        //                    await System.IO.File.WriteAllBytesAsync($"{folderPath}/{model.RollNumber}.pdf", pdfBytes);

        //                }
        //            }
        //            string targetPath = $"{folderPath}/{model.InstituteID}.zip";
        //            if (System.IO.File.Exists(targetPath))
        //            {
        //                System.IO.File.Delete(targetPath);
        //            }

        //            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
        //            using (MemoryStream zipStream = new MemoryStream())
        //            {
        //                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
        //                {
        //                    foreach (var filePath in files)
        //                    {
        //                        string relativePath = Path.GetRelativePath(folderPath, filePath);
        //                        zip.CreateEntryFromFile(filePath, relativePath);
        //                    }
        //                }

        //                zipStream.Position = 0;



        //                HttpContext.Response.Clear();
        //                HttpContext.Response.ContentType = "application/zip";
        //                HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{model.InstituteID}.zip\"");

        //                await zipStream.CopyToAsync(HttpContext.Response.Body);
        //                //await HttpContext.Response.Body.FlushAsync();

        //                return new EmptyResult();

        //            }

        //            //ZipFile.CreateFromDirectory(folderPath, targetPath);

        //            //byte[] zipByte = System.IO.File.ReadAllBytes(targetPath);
        //            //result.Message = Convert.ToBase64String(zipByte);
        //            //result.State = EnumStatus.Success;
        //            //result.Message = "Success";
        //        }
        //        else
        //        {
        //            result.State = EnumStatus.Warning;
        //            result.Message = Constants.MSG_DATA_NOT_FOUND;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _unitOfWork.Dispose();
        //        // Write error log
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //        //
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return Content("An error occurred while generating the PDF!");
        //    //});
        //}


        [HttpPost("GetITICollegeStudent_Marksheet")]
        public async Task<IActionResult> GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)
        {
            ActionName = "GetITICollegeStudent_Marksheet(StudentMarksheetSearchModel model)";
            //return await Task.Run(async () =>
            //{
            var result = new ApiResult<string>();
            string folderPath = "";
            try
            {
                var data = await _unitOfWork.ITICollegeMarksheetDownloadRepository.GetITICollegeStudent_Marksheet(model);
                folderPath = $"{ConfigurationHelper.StaticFileRootPath}/Report/ITI/Result_MarkSheet{model.CollegeCode}";
               
                if (System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.Delete(folderPath, true);
                } 
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                    {
                        string rollNumber = data.Tables[0].Rows[i]["RollNo"].ToString();
                        DataTable headerDataTable = data.Tables[0].AsEnumerable().Where(row => row["RollNo"].ToString() == rollNumber).CopyToDataTable();
                        DataTable semDataTable = data.Tables[1].AsEnumerable().Where(row => row["RollNo"].ToString() == rollNumber).CopyToDataTable();

                        int srNo = 1;

                        for (int j = 0; j < semDataTable.Rows.Count; j++)
                        {
                            if (!semDataTable.Rows[j]["StreamName"].ToString().Contains("Formative") && !semDataTable.Rows[j]["StreamName"].ToString().Contains("Total"))
                            {
                                semDataTable.Rows[j]["SrNo"] = srNo;
                                srNo++;
                            }
                        }


                        // To remove a column by name:
                        semDataTable.Columns.Remove("RollNo");
                        DataSet studentData = new DataSet();
                        DataTable studentHeader = new DataTable();
                        //studentHeader.Rows.Add(data.Tables[0].Rows[i]);
                        studentData.Tables.Add(headerDataTable);
                        studentData.Tables.Add(semDataTable);
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        studentData.Tables[0].TableName = "GetITIStudent_Marksheet_SingleDetails";

                        studentData.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        studentData.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        studentData.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + studentData.Tables[0].Rows[0]["signlogo"];
                        studentData.Tables[1].TableName = "GetITIStudent_Marksheet_Details";

                        string devFontSize = "15px";
                        /*default font size for kruti dev*/
                        //string fontSize = "font-size: 10px;";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/ITIMarksheet.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, studentData);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
                        sb1.Append(html);


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);

                        if (System.IO.File.Exists($"{folderPath}/{rollNumber}.pdf"))
                        {
                            System.IO.File.Delete($"{folderPath}/{rollNumber}.pdf");
                        }
                        await System.IO.File.WriteAllBytesAsync($"{folderPath}/{rollNumber}.pdf", pdfBytes);

                    }
                    string targetPath = $"{folderPath}/Marksheet_{model.CollegeCode}.zip";
                    if (System.IO.File.Exists(targetPath))
                    {
                        System.IO.File.Delete(targetPath);
                    }

                    string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                    using (MemoryStream zipStream = new MemoryStream())
                    {
                        using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                        {
                            foreach (var filePath in files)
                            {
                                string relativePath = Path.GetRelativePath(folderPath, filePath);
                                zip.CreateEntryFromFile(filePath, relativePath);
                            }
                        }

                        zipStream.Position = 0;



                        HttpContext.Response.Clear();
                        HttpContext.Response.ContentType = "application/zip";
                        HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=Marksheet_\"{model.CollegeCode}.zip\"");

                        await zipStream.CopyToAsync(HttpContext.Response.Body);
                        //await HttpContext.Response.Body.FlushAsync();

                        return new EmptyResult();

                    }

                    //ZipFile.CreateFromDirectory(folderPath, targetPath);

                    //byte[] zipByte = System.IO.File.ReadAllBytes(targetPath);
                    //result.Message = Convert.ToBase64String(zipByte);
                    //result.State = EnumStatus.Success;
                    //result.Message = "Success";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }

            return Content("An error occurred while generating the PDF!");
        }


        [HttpPost("GetITICollege")]
        public async Task<ApiResult<DataTable>> GetITICollege([FromBody] ITICollegeStudentMarksheetSearchModel body)
        {
            ActionName = "GetITICollege([FromBody] ITICollegeStudentMarksheetSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITICollegeMarksheetDownloadRepository.GetITICollegeList(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }


        [HttpPost("GetITICollegeStudent_Consolidate")]
        public async Task<IActionResult> GetITICollegeStudent_Consolidate(ITICollegeStudentMarksheetSearchModel model)
        {
            ActionName = "GetITICollegeStudent_Marksheet(StudentMarksheetSearchModel model)";
            //return await Task.Run(async () =>
            //{
            var result = new ApiResult<string>();
            string folderPath = "";
            try
            {
                var data = await _unitOfWork.ITICollegeMarksheetDownloadRepository.GetITIConsolidateCertificate(model);
                folderPath = $"{ConfigurationHelper.StaticFileRootPath}/Report/ITI/Result_ConsolidateMarkSheet{model.CollegeCode}";

                if (System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.Delete(folderPath, true);
                }
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                    {
                        string rollNumber = data.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                        DataTable headerDataTable = data.Tables[0].AsEnumerable().Where(row => row.Field<string>("EnrollmentNo") == rollNumber).CopyToDataTable();
                        DataTable semDataTable = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("enrollment") == rollNumber).CopyToDataTable();
                        DataSet studentData = new DataSet();
                        DataTable studentHeader = new DataTable();
                        //studentHeader.Rows.Add(data.Tables[0].Rows[i]);
                        studentData.Tables.Add(headerDataTable);
                        studentData.Tables.Add(semDataTable);
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        studentData.Tables[0].TableName = "MarksheetConsolidated";

                        studentData.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        studentData.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + studentData.Tables[0].Rows[0]["signlogo"];
                        studentData.Tables[0].Rows[0]["mainlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        studentData.Tables[1].TableName = "Consolidated_Marksheet";
                        decimal Total_Ob = 0;
                        decimal Total_Mx = 0;
                        foreach (DataRow dr in studentData.Tables[1].Rows)
                        {
                            Total_Ob += Convert.ToDecimal(dr["Total_Ob"].ToString());
                            Total_Mx += Convert.ToDecimal(dr["Total_Mx"].ToString());
                        }
                        studentData.Tables[0].Rows[0]["Percentage"] = Math.Round((Total_Ob / Total_Mx * 100), 2).ToString();


                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ITIMarksheetCONSOLIDATED.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, studentData);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        sb1.Append(html);

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";
                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);
                        //folderPath = $"{ConfigurationHelper.StaticFileRootPath}/Report/ITI/ResultTestConsidate";
                        //if (!Directory.Exists(folderPath))
                        //{
                        //    Directory.CreateDirectory(folderPath);
                        //}
                        if (System.IO.File.Exists($"{folderPath}/{rollNumber}.pdf"))
                        {
                            System.IO.File.Delete($"{folderPath}/{rollNumber}.pdf");
                        }
                        await System.IO.File.WriteAllBytesAsync($"{folderPath}/{rollNumber}.pdf", pdfBytes);
                    }
                    string targetPath = $"{folderPath}/Consolidated_Marksheet_{model.CollegeCode}.zip";
                    if (System.IO.File.Exists(targetPath))
                    {
                        System.IO.File.Delete(targetPath);
                    }

                    string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                    using (MemoryStream zipStream = new MemoryStream())
                    {
                        using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                        {
                            foreach (var filePath in files)
                            {
                                string relativePath = Path.GetRelativePath(folderPath, filePath);
                                zip.CreateEntryFromFile(filePath, relativePath);
                            }
                        }

                        zipStream.Position = 0;



                        HttpContext.Response.Clear();
                        HttpContext.Response.ContentType = "application/zip";
                        HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=Consolidated_Marksheet_\"{model.CollegeCode}.zip\"");

                        await zipStream.CopyToAsync(HttpContext.Response.Body);
                        //await HttpContext.Response.Body.FlushAsync();

                        return new EmptyResult();


                    }
                    //ZipFile.CreateFromDirectory(folderPath, targetPath);

                    //byte[] zipByte = System.IO.File.ReadAllBytes(targetPath);
                    //result.Message = Convert.ToBase64String(zipByte);
                    //result.State = EnumStatus.Success;
                    //result.Message = "Success";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            return Content("An error occurred while generating the PDF!");
            //});
        }


        [HttpPost("GetITIStudent_SCVTCertificate")]
        public async Task<IActionResult> GetITIStudent_SCVTCertificate(ITICollegeStudentMarksheetSearchModel model)
        {
            ActionName = "GetITIStudent_SCVTCertificate(ITICollegeStudentMarksheetSearchModel model)";
            var result = new ApiResult<string>();
            string folderPath = "";
            try
            {
                var data = await _unitOfWork.ITICollegeMarksheetDownloadRepository.ITIStateTradeCertificateReport(model);
                folderPath = $"{ConfigurationHelper.StaticFileRootPath}/Report/ITI/Result_SCVTCertificate{model.CollegeCode}";

                if (System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.Delete(folderPath, true);
                }
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                    {
                        string EnrollmentNo = data.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                        DataTable headerDataTable = data.Tables[0].AsEnumerable().Where(row => row.Field<string>("EnrollmentNo") == EnrollmentNo).CopyToDataTable();
                        DataSet studentData = new DataSet();
                        DataTable studentHeader = new DataTable();
                        //studentHeader.Rows.Add(data.Tables[0].Rows[i]);
                        studentData.Tables.Add(headerDataTable);

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        studentData.Tables[0].TableName = "StateTradeCertificate";

                        studentData.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        studentData.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + studentData.Tables[0].Rows[0]["signlogo"];

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/StateTradeCertificateReport.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, studentData);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        //html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(html);

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        //folderPath = $"{ConfigurationHelper.StaticFileRootPath}/Report/ITI/ResultTestConsidate";
                        //if (!Directory.Exists(folderPath))
                        //{
                        //    Directory.CreateDirectory(folderPath);
                        //}
                        if (System.IO.File.Exists($"{folderPath}/{EnrollmentNo}.pdf"))
                        {
                            System.IO.File.Delete($"{folderPath}/{EnrollmentNo}.pdf");
                        }
                        await System.IO.File.WriteAllBytesAsync($"{folderPath}/{EnrollmentNo}.pdf", pdfBytes);
                    }
                    string targetPath = $"{folderPath}/Certificate_{model.CollegeCode}.zip";
                    if (System.IO.File.Exists(targetPath))
                    {
                        System.IO.File.Delete(targetPath);
                    }

                    string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                    using (MemoryStream zipStream = new MemoryStream())
                    {
                        using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                        {
                            foreach (var filePath in files)
                            {
                                string relativePath = Path.GetRelativePath(folderPath, filePath);
                                zip.CreateEntryFromFile(filePath, relativePath);
                            }
                        }

                        zipStream.Position = 0;



                        HttpContext.Response.Clear();
                        HttpContext.Response.ContentType = "application/zip";
                        HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=Certificate_\"{model.CollegeCode}.zip\"");

                        await zipStream.CopyToAsync(HttpContext.Response.Body);
                        //await HttpContext.Response.Body.FlushAsync();

                        return new EmptyResult();
                    }
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            return Content("An error occurred while generating the PDF!");
            //});
        }

        #endregion
    }
}
