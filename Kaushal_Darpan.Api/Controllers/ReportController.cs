using AspNetCore.Reporting;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.tool.xml.html;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Api.Code.PlaywrightPdf;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Api.HtmlTempleteFile;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BterCertificateReport;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CertificateDownload;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.FlyingSquad;
using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.LeaveMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.OptionalFormatReport;
using Kaushal_Darpan.Models.PlacementReport;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using Kaushal_Darpan.Models.TimeTable;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;



namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ReportController : BaseController
    {
        public override string PageName => "ReportController";
        public override string ActionName { get; set; }
        public object ListRoleListPath { get; private set; }
        public object ModInsert { get; private set; }

        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //public ReportController(IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        private readonly IConverter _converter;
        private readonly IPrintHtmlFile _printHtmlFile;
        private readonly IPlaywrightPdfService _pdfService;

        public ReportController(IMapper mapper,
            IUnitOfWork unitOfWork,
            IConverter converter,
            IPrintHtmlFile printHtmlFile,
            IPlaywrightPdfService pdfService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_emailService = emailService;
            _converter = converter;
            _printHtmlFile = printHtmlFile;
            _pdfService = pdfService;
        }

        [HttpPost("GetAllDataRpt")]
        public async Task<ApiResult<DataTable>> GetAllDataRpt([FromBody] TheorySearchModel body)
        {
            ActionName = "GetAllData([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ReportRepository.GetAllDataRpt(body);
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
                await _unitOfWork.DisposeAsync();
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

        #region Admit Card
        //[HttpPost("GetStudentAdmitCard")]
        //public async Task<ApiResult<int>> GetStudentAdmitCard([FromBody] GenerateAdmitCardSearchModel ListData)
        //{
        //    ActionName = "GetStudentAdmitCard(string EnrollmentNo)";
        //    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {
        //            //ListData.ForEach(x =>
        //            //{
        //            //    x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            //});

        //            foreach (var student in ListData)
        //            {
        //                var data = await _unitOfWork.ReportRepository.GetStudentAdmitCard(student);
        //                if (data?.Tables?.Count == 2)
        //                {
        //                    //report
        //                    var fileName = $"AdmitCard_{student.StudentID}_{student.StudentExamID}.pdf";
        //                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentAdmitCard.rdlc";

        //                    student.AdmitCardPath = filepath;
        //                    student.AdmitCard = fileName;
        //                    //provider                      
        //                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                    //images

        //                    string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
        //                    data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

        //                    string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
        //                    data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

        //                    string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
        //                    data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
        //                    //rdlc

        //                    LocalReport localReport = new LocalReport(rdlcpath);
        //                    localReport.AddDataSource("AdmitCard", data.Tables[0]);
        //                    localReport.AddDataSource("AdmitCard_Subject", data.Tables[1]);
        //                    var reportResult = localReport.Execute(RenderType.Pdf);

        //                    //check file exists
        //                    if (!System.IO.Directory.Exists(folderPath))
        //                    {
        //                        Directory.CreateDirectory(folderPath);
        //                    }

        //                    //save
        //                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
        //                    //end report
        //                }
        //                else
        //                {
        //                    result.State = EnumStatus.Warning;
        //                    result.Message = Constants.MSG_DATA_NOT_FOUND;
        //                }
        //            }

        //            //var Issuccess = await _unitOfWork.GenerateAdmitCardRepository.UpdateAdmitCard(ListData);
        //            //if (Issuccess > 0)
        //            //{
        //            //    result.Data = Issuccess.ToString();
        //            //    result.State = EnumStatus.Success;
        //            //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //            //}
        //            //else
        //            //{
        //            //    result.State = EnumStatus.Warning;
        //            //    result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            //}

        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
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
        #endregion

        #region "GetStudentAdmitCard"
        [HttpPost("GetStudentAdmitCard")]
        //public async Task<ApiResult<string>> GetStudentAdmitCard([FromBody] GenerateAdmitCardSearchModel model)
        //{
        //    ActionName = "GetStudentAdmitCard(string EnrollmentNo)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {
        //            var data = await _unitOfWork.ReportRepository.GetStudentAdmitCard(model);
        //            if (data.Tables?.Count > 1)
        //            {
        //                //report

        //                var fileName = $"AdmitCard_{model.StudentID}_{model.StudentExamID}.pdf";
        //                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentAdmitCard.rdlc";

        //                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //                try
        //                {
        //                    //string studentFileName = "Apr012025060950764086.png";
        //                    //string stuimgFilepath = "https://kdhteapi.rajasthan.gov.in/Api/StaticFiles//Students/" + studentFileName + "";
        //                    string stuimgFilepath = $"{ConfigurationHelper.RootPath}StaticFiles/Apr012025060950764086.png";
        //                    Console.WriteLine(stuimgFilepath);


        //                    //byte[] studentPhotoBytes = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

        //                    //// Ensure correct column type
        //                    if (!data.Tables[1].Columns.Contains("StudentPhoto1"))
        //                    {
        //                        data.Tables[1].Columns.Add("StudentPhoto1", typeof(byte[]));
        //                        data.Tables[1].Columns.Add("StudentPhoto2", typeof(string));
        //                    }

        //                    foreach (DataRow row in data.Tables[1].Rows)
        //                    {
        //                        string photoFileName = row["StudentPhoto1"].ToString();
        //                        string fullPhotoPath = Path.Combine(ConfigurationHelper.RootPath, "StaticFiles", "ITIPracticalExam", Convert.ToString(row["StudentPhoto"]));


        //                        //string fullPhotoPath = "https://kdhteapi.rajasthan.gov.in/Api/StaticFiles//Students/Jul042025041326899143.jpeg";
        //                        if (System.IO.File.Exists(fullPhotoPath))
        //                        {
        //                            row["StudentPhoto1"] = System.IO.File.ReadAllBytes(fullPhotoPath); // This must be byte[]

        //                        }
        //                        else
        //                        {
        //                            row["StudentPhoto1"] = System.IO.File.ReadAllBytes(Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg"));
        //                        }

        //                        if (row["StudentPhoto1"] != DBNull.Value && row["StudentPhoto1"] is byte[] photoBytes)
        //                        {
        //                            // Optional: further verify if it's a valid image format
        //                            using (var ms = new MemoryStream(photoBytes))
        //                            {
        //                                try
        //                                {
        //                                    using (var image = System.Drawing.Image.FromStream(ms))
        //                                    {
        //                                        Console.WriteLine("Valid image: " + image.Width + "x" + image.Height);
        //                                        var a = "Valid image: " + image.Width + "x" + image.Height;
        //                                    }
        //                                }
        //                                catch (Exception ex)
        //                                {
        //                                    Console.WriteLine("Invalid image bytes: " + ex.Message);
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("No image found or invalid byte[] type.");
        //                        }
        //                    }
        //                    LocalReport localReport = new LocalReport(rdlcpath);
        //                    localReport.AddDataSource("AdmitCard", data.Tables[0]);
        //                    localReport.AddDataSource("AdmitCard_Subject", data.Tables[1]);//check file exists
        //                                                                                   //localReport.AddDataSource("TimeTableDetails", data.Tables[2]);
        //                    var reportResult = localReport.Execute(RenderType.Pdf);
        //                    if (!System.IO.Directory.Exists(folderPath))
        //                    {
        //                        Directory.CreateDirectory(folderPath);
        //                    }
        //                    //save
        //                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
        //                    //end report
        //                    result.Data = fileName;
        //                    result.State = EnumStatus.Success;
        //                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

        //                }






        //                //LocalReport localReport = new LocalReport(rdlcpath);
        //                //localReport.AddDataSource("AdmitCard", data.Tables[0]);
        //                //localReport.AddDataSource("AdmitCard_Subject", data.Tables[1]);//check file exists
        //                ////localReport.AddDataSource("TimeTableDetails", data.Tables[2]);
        //                //var reportResult = localReport.Execute(RenderType.Pdf);
        //                //if (!System.IO.Directory.Exists(folderPath))
        //                //{
        //                //    Directory.CreateDirectory(folderPath);
        //                //}
        //                ////save
        //                //System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
        //                ////end report
        //                //result.Data = fileName;
        //                //result.State = EnumStatus.Success;
        //                //result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
        //            }


        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
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

        public async Task<ApiResult<string>> GetStudentAdmitCard([FromBody] GenerateAdmitCardSearchModel model)
        {
            ActionName = "GetStudentAdmitCard(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();

                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentAdmitCard(model);

                    if (data.Tables?.Count > 1)
                    {
                        // File paths
                        var fileName = $"AdmitCard_{model.StudentID}_{model.StudentExamID}.pdf";
                        var folderPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.ReportsFolder);
                        var filePath = Path.Combine(folderPath, fileName);
                        var rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderBTER, "StudentAdmitCard.rdlc");

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        try
                        {


                            if (!data.Tables[0].Columns.Contains("SignatureFile1"))
                            {
                                data.Tables[0].Columns.Add("SignatureFile1", typeof(byte[]));
                            }

                            string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                            data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));


                            string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                            data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                            string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
                            data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));

                            // Generate RDLC PDF
                            var localReport = new LocalReport(rdlcPath);
                            localReport.AddDataSource("AdmitCard", data.Tables[0]);
                            localReport.AddDataSource("AdmitCard_Subject", data.Tables[1]);

                            var reportResult = localReport.Execute(RenderType.Pdf);

                            if (!System.IO.Directory.Exists(folderPath))
                                Directory.CreateDirectory(folderPath);

                            System.IO.File.WriteAllBytes(filePath, reportResult.MainStream);

                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
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
                    await _unitOfWork.DisposeAsync();

                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };

                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }

        #endregion

        [HttpPost("GetStudentAdmitCardBulk")]
        public async Task<ApiResult<string>> GetStudentAdmitCardBulk([FromBody] DownloadDataPagingListModel Model)
        {
            ActionName = "GetStudentAdmitCardBulk(DownloadDataPagingListModel Model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            string iStudentExamID = "567399";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    List<GenerateAdmitCardModel> ListData = new List<GenerateAdmitCardModel>();
                    foreach (var StudentExamID in Model.StudentExamIDs.Split(','))
                    {
                        if (string.IsNullOrEmpty(StudentExamID))
                        {
                            continue;
                        }

                        iStudentExamID = StudentExamID;
                        GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                        var data = await _unitOfWork.ReportRepository.GetStudentAdmitCardBulk(Convert.ToInt32(StudentExamID),
                            Model.DepartmentID);
                        if (data?.Tables?.Count >= 2)
                        {
                            try
                            {

                                int studentID = Convert.ToInt32(data.Tables[0].Rows[0]["StudentID"]);
                                //report
                                var fileName = $"AdmitCard_{studentID}_{StudentExamID}.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentAdmitCard.rdlc";

                                #region "Add Object"
                                objStudent.StudentID = studentID;
                                objStudent.AdmitCardPath = filepath;
                                objStudent.AdmitCard = fileName;
                                objStudent.StudentExamID = Convert.ToInt32(StudentExamID);
                                objStudent.IPAddress = ipaddress;
                                ListData.Add(objStudent);
                                #endregion



                                //provider                      
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                //images

                                string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                                data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                                //ConfigurationHelper.StaticFileRootPath
                                string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                                data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                                string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
                                data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
                                //rdlc

                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("AdmitCard", data.Tables[0]);
                                localReport.AddDataSource("AdmitCard_Subject", data.Tables[1]);


                                //localReport.AddDataSource("TimeTableDetails", data.Tables[2]);
                                var reportResult = localReport.Execute(RenderType.Pdf);

                                //check file exists
                                if (!System.IO.Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                                //save
                                //save
                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                //end report


                            }
                            catch (Exception ex)
                            {
                                var nex = new NewException
                                {
                                    PageName = "GetStudentAdmitCardBulk_Debug",
                                    ActionName = string.Format("StudentExamID={0},", iStudentExamID),
                                    Ex = ex,
                                };
                                await CreateErrorLog(nex, _unitOfWork);

                            }
                        }
                    }

                    var Issuccess = await _unitOfWork.GenerateAdmitCardRepository.UpdateAdmitCard(ListData);
                    if (Issuccess > 0)
                    {

                        #region "Save Multiple PDF PAGES"
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        string outputFile = $"AdmitCard_{timestamp}_from_{Model.PageFrom}_To_{Model.PageTo}.pdf";
                        string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                        List<string?> strSoureFiles = ListData.Select(s => s.AdmitCardPath).ToList();
                        if (await MergePdfFilesAsync(strSoureFiles, outputPath))
                        {


                            DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                            ModInsert.FileName = outputFile;
                            ModInsert.PDFType = (int)EnumPdfType.AdmitCard;
                            ModInsert.Status = 11;
                            ModInsert.SemesterID = Model.SemesterID;
                            ModInsert.InstituteID = Model.InstituteID;
                            ModInsert.DepartmentID = Model.DepartmentID;
                            ModInsert.EndTermID = Model.EndTermID;
                            ModInsert.Eng_NonEng = Model.Eng_NonEng;
                            ModInsert.CreatedBy = Model.UserID;
                            ModInsert.TotalStudent = Model.TotalRecord;
                            var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);
                            result.Data = outputFile;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "Something went wrong";
                        }
                        #endregion
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }

                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = iStudentExamID.ToString(),
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "something went wrong please try again";
                    result.Message = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("GetStudentAdmitCardBulk_InstituteWise")]
        public async Task<ApiResult<string>> GetStudentAdmitCardBulk_InstituteWise([FromBody] GenerateAdmitCardSearchModel Model)
        {
            ActionName = "GetStudentAdmitCardBulk_InstituteWise(GenerateAdmitCardSearchModel Model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            string iStudentExamID = "";
            var result = new ApiResult<string>();

            var filename = "GetStudentAdmitCardBulk_InstituteWise";
            // log
            CommonFuncationHelper.WriteTextLog("start : GetStudentAdmitCardBulk_InstituteWise", filename);
            try
            {

                var ListInsituteData = await _unitOfWork.GenerateAdmitCardRepository.GetGenerateAdmitCardDataBulk_InsituteWise(Model);
                // log
                CommonFuncationHelper.WriteTextLog($"1. GetGenerateAdmitCardDataBulk_InsituteWise (data count {ListInsituteData.Count}) done.", filename);
                if (ListInsituteData.Count > 0)
                {
                    int ListInsituteDataLoopCount = 1;
                    foreach (var childdata in ListInsituteData)
                    {
                        // log
                        CommonFuncationHelper.WriteTextLog($"2. ListInsituteData loop count {ListInsituteDataLoopCount} and total ExamIds {childdata.StudentExamIDs.Split(',').Length}.", filename);
                        List<GenerateAdmitCardModel> ListData = new List<GenerateAdmitCardModel>();
                        //set data
                        Model.SemesterID = childdata.SemesterID;
                        Model.InstituteID = childdata.InstituteID;
                        Model.DepartmentID = 1;
                        Model.EndTermID = childdata.EndTermID;
                        Model.Eng_NonEng = childdata.Eng_NonEng;
                        Model.TotalRecord = childdata.TotalRecord;

                        //semester wise Data
                        int StudentExamIDLoopCount = 1;
                        foreach (var StudentExamID in childdata.StudentExamIDs.Split(','))
                        {
                            if (string.IsNullOrEmpty(StudentExamID))
                            {
                                // log
                                CommonFuncationHelper.WriteTextLog($"3. StudentExamIDs (blank) loop count {StudentExamIDLoopCount} for ExamId - {StudentExamID}.", filename);
                                continue;
                            }

                            iStudentExamID = StudentExamID;
                            GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                            var data = await _unitOfWork.ReportRepository.GetStudentAdmitCardBulk(Convert.ToInt32(StudentExamID),
                            Model.DepartmentID);
                            // log
                            CommonFuncationHelper.WriteTextLog($"3. StudentExamIDs (GetStudentAdmitCardBulk : table count {data?.Tables?.Count}) loop count {StudentExamIDLoopCount} for ExamId - {StudentExamID}.", filename);
                            if (data?.Tables?.Count >= 2)
                            {
                                try
                                {

                                    int studentID = Convert.ToInt32(data.Tables[0].Rows[0]["StudentID"]);
                                    //report
                                    var fileName = $"AdmitCard_{studentID}_{StudentExamID}.pdf";
                                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentAdmitCard.rdlc";


                                    #region "Add Object"
                                    objStudent.StudentID = studentID;
                                    objStudent.AdmitCardPath = filepath;
                                    objStudent.AdmitCard = fileName;
                                    objStudent.StudentExamID = Convert.ToInt32(StudentExamID);
                                    objStudent.IPAddress = ipaddress;
                                    ListData.Add(objStudent);
                                    #endregion

                                    //provider                      
                                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                    //images
                                    string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                                    data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));


                                    string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                                    data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                                    string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
                                    data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
                                    //rdlc

                                    LocalReport localReport = new LocalReport(rdlcpath);
                                    localReport.AddDataSource("AdmitCard", data.Tables[0]);
                                    localReport.AddDataSource("AdmitCard_Subject", data.Tables[1]);
                                    //localReport.AddDataSource("TimeTableDetails", data.Tables[2]);
                                    var reportResult = localReport.Execute(RenderType.Pdf);

                                    //check file exists
                                    if (!System.IO.Directory.Exists(folderPath))
                                    {
                                        Directory.CreateDirectory(folderPath);
                                    }
                                    //save
                                    //save
                                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                    //end report

                                }
                                catch (Exception ex)
                                {
                                    var nex = new NewException
                                    {
                                        PageName = "GetStudentAdmitCardBulk_Debug",
                                        ActionName = $"EndTermID{Model.EndTermID}InstituteID={Model.InstituteID}SemesterID=={Model.SemesterID}",
                                        Ex = ex,
                                    };
                                    await CreateErrorLog(nex, _unitOfWork);
                                    // log
                                    CommonFuncationHelper.WriteTextLog($"3. StudentExamIDs (error : {ex.Message}) loop count {StudentExamIDLoopCount}  for ExamId - {StudentExamID}.", filename);

                                }
                            }
                            StudentExamIDLoopCount++;
                        }

                        // save
                        var Issuccess = await _unitOfWork.GenerateAdmitCardRepository.UpdateAdmitCard(ListData);
                        // log
                        CommonFuncationHelper.WriteTextLog($"4. ListInsituteData (UpdateAdmitCard ({Issuccess}) done.) loop count {ListInsituteDataLoopCount}.", filename);
                        if (Issuccess > 0)
                        {

                            #region "Save Multiple PDF PAGES"
                            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                            string guid = Guid.NewGuid().ToString().ToUpper();
                            string outputFile = $"AdmitCard_{guid}.pdf";
                            string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                            List<string?> strSoureFiles = ListData.Select(s => s.AdmitCardPath).ToList();
                            if (await MergePdfFilesAsync(strSoureFiles, outputPath))
                            {
                                DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                                ModInsert.FileName = outputFile;
                                ModInsert.PDFType = (int)EnumPdfType.AdmitCard;
                                ModInsert.Status = 11;
                                ModInsert.SemesterID = Model.SemesterID;
                                ModInsert.InstituteID = Model.InstituteID;
                                ModInsert.DepartmentID = Model.DepartmentID;
                                ModInsert.EndTermID = Model.EndTermID;
                                ModInsert.Eng_NonEng = Model.Eng_NonEng;
                                ModInsert.CreatedBy = Model.UserID;
                                ModInsert.TotalStudent = Model.TotalRecord;

                                var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);
                                await _unitOfWork.SaveChangesAsync();
                                // log
                                CommonFuncationHelper.WriteTextLog($"5. ListInsituteData (SaveRollNumbePDFData done.) loop count {ListInsituteDataLoopCount}.", filename);

                                result.Data = outputFile;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_SAVE_SUCCESS;
                            }
                            else
                            {
                                result.State = EnumStatus.Error;
                                result.ErrorMessage = "Something went wrong";
                            }
                            #endregion
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }

                        ListInsituteDataLoopCount++;
                    }
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }

                // log
                CommonFuncationHelper.WriteTextLog("end : GetStudentAdmitCardBulk_InstituteWise", filename);
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                // Write error log
                var nex = new NewException
                {
                    PageName = iStudentExamID.ToString(),
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                result.State = EnumStatus.Error;
                result.ErrorMessage = "something went wrong please try again";
                result.Message = ex.Message;

                // log
                CommonFuncationHelper.WriteTextLog($"end error : GetStudentAdmitCardBulk_InstituteWise : {ex.Message}", filename);
            }
            return result;
        }


        #region Colleges Wise Reports
        [HttpPost("GetStudentEnrollmentReports")]
        public async Task<ApiResult<DataTable>> GetStudentEnrollmentReports([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetStudentEnrollmentReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetStudentEnrollmentReports(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region Principle Dashboard Report
        [HttpPost("GetPrincipleDashboardReport")]
        public async Task<ApiResult<DataTable>> GetPrincipleDashboardReport([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetPrincipleDashboardReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetPrincipleDashboardReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region Colleges Nodal Reports
        [HttpPost("GetCollegeNodalReportsData")]
        public async Task<ApiResult<DataTable>> GetCollegeNodalReportsData([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetStudentEnrollmentReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCollegeNodalReportsData(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region Colleges Wise Reports
        [HttpPost("GetCollegesWiseReports")]
        public async Task<ApiResult<DataTable>> GetCollegesWiseReports(CollegesWiseExaminationRptSearchModel model)
        {
            ActionName = "GetCollegesWiseReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCollegesWiseReports(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion



        #region Student Fee Receipt
        [HttpGet("GetStudentFeeReceipt/{EnrollmentNo}/{StudentExamID}")]
        public async Task<ApiResult<string>> GetStudentFeeReceipt(string EnrollmentNo, int StudentExamID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentFeeReceipt(EnrollmentNo, StudentExamID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"EnrolledFeeReceipt_{EnrollmentNo}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentEnrolledFeeReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Student Fee Receipt
        [HttpGet("GetStudentApplicationFeeReceipt/{EnrollmentNo}")]
        public async Task<ApiResult<string>> GetStudentApplicationFeeReceipt(string EnrollmentNo)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentApplicationFeeReceipt(EnrollmentNo);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ApplicationFeeReceipt_{EnrollmentNo}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFeeReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        #region Student Allotment Fee Receipt
        [HttpGet("GetStudentAllotmentFeeReceipt/{EnrollmentNo}")]
        public async Task<ApiResult<string>> GetStudentAllotmentFeeReceipt(string EnrollmentNo)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentAllotmentFeeReceipt(EnrollmentNo);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/AllotmentFeeReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Passout Student Report
        [HttpPost("GetPassoutStudentReport")]
        public async Task<ApiResult<string>> GetPassoutStudentReport(PassoutStudentReport model)
        {
            ActionName = "GetPassoutStudentReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetPassoutStudentReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"PassoutStudentReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/PassoutReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("PassoutReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        //#region Internal Assessment Student Report
        //[HttpPost("GetInternalAssessmentStudentReport")]
        //public async Task<ApiResult<string>> GetInternalAssessmentStudentReport(InternalAssessmentStudentReport model)
        //{
        //    ActionName = "GetInternalAssessmentStudentReport()";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {
        //            var data = await _unitOfWork.ReportRepository.GetInternalAssessmentStudentReport(model);
        //            if (data != null)
        //            {
        //                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //                //report
        //                //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
        //                var fileName = $"InternalAssessmentStudentReport.pdf";
        //                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/InternalAssessmentReport.rdlc";
        //                //
        //                var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
        //                //
        //                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                LocalReport localReport = new LocalReport(rdlcpath);
        //                localReport.AddDataSource("InternalAssessmentStudent", data.Tables[0]);
        //                var reportResult = localReport.Execute(RenderType.Pdf);

        //                //check file exists
        //                if (!System.IO.Directory.Exists(folderPath))
        //                {
        //                    Directory.CreateDirectory(folderPath);
        //                }
        //                //save


        //                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
        //                //end report

        //                result.Data = fileName;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
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
        //#endregion

        //#region Exam Letter Report
        //[HttpPost("GetExamLetterReportBkp")]
        //public async Task<ApiResult<string>> GetExamLetterReport(ExamLetterReport model)
        //{
        //    ActionName = "GetExamLetterReport()";
        //    return await Task.Run(async () =>
        //    {
        //        List<string> ListRoleListPath = new List<string>();
        //        var result = new ApiResult<string>();
        //        try
        //        {
        //            var data = await _unitOfWork.ReportRepository.GetExamLetterReport(model);
        //            if (data != null)
        //            {

        //                var groupedData = data.Tables[0]
        //                .AsEnumerable()
        //                //.GroupBy(r => r.Field<int>("GroupCode"))
        //                .GroupBy(r => new
        //                {
        //                    GroupCode = r.Field<int>("GroupCode"),
        //                    SubjectCode = r.Field<string>("SubjectCode") // or string, depending on your DB type
        //                })
        //                //.Select(g => g.Key)
        //                .Select(g => new
        //                {
        //                    g.Key.GroupCode,
        //                    g.Key.SubjectCode,
        //                    Items = g.ToList() // Optional: keeps the list of rows for each group
        //                })
        //                .ToList();



        //                foreach (var group in groupedData)
        //                {

        //                    //var filteredRows = data.Tables[0]
        //                    //    .AsEnumerable()
        //                    //    //.Where(r => r.Field<int>("GroupCode") == group)
        //                    //    .Where(r => r.Field<int>("GroupCode") == group.GroupCode && r.Field<string>("SubjectCode") == group.SubjectCode)
        //                    //    .ToList();

        //                    var filteredRows = data.Tables[0].AsEnumerable()
        //                            .Where(r => r.Field<int>("GroupCode") == group.GroupCode &&
        //                                        r.Field<string>("SubjectCode") == group.SubjectCode)
        //                            .ToList();


        //                    //DataTable filteredTable = filteredRows.SelectMany(g => g).Any()
        //                    //        ? filteredRows.SelectMany(g => g).CopyToDataTable()
        //                    //        : data.Tables[0].Clone();

        //                    //if (filteredRows.Any())
        //                    //        {
        //                    //            filteredTable = filteredRows.CopyToDataTable();
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            // No rows matched → create empty table with same schema
        //                    //            filteredTable = data.Tables[0].Clone();
        //                    //        }

        //                    DataTable filteredTable;

        //                    if (filteredRows.Any())
        //                    {
        //                        filteredTable = filteredRows.CopyToDataTable();
        //                    }
        //                    else
        //                    {
        //                        filteredTable = data.Tables[0].Clone();
        //                    }


        //                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //                    var fileName = $"ExamLetterReport_{group}.pdf";
        //                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ExamLetter.rdlc";
        //                    //
        //                    var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
        //                    //
        //                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                    LocalReport localReport = new LocalReport(rdlcpath);
        //                    localReport.AddDataSource("ExamLetterReport", filteredTable);
        //                    //localReport.AddDataSource("ExamLetterReport", data.Tables[0]);
        //                    var reportResult = localReport.Execute(RenderType.Pdf);
        //                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
        //                    ListRoleListPath.Add(filepath);
        //                    result.Data = fileName;
        //                    result.State = EnumStatus.Success;
        //                    result.Message = "Success.";

        //                    //check file exists
        //                    if (!System.IO.Directory.Exists(folderPath))
        //                    {
        //                        Directory.CreateDirectory(folderPath);
        //                    }
        //                    //save



        //                    //end report

        //                    result.Data = fileName;
        //                    result.State = EnumStatus.Success;
        //                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

        //                }
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            }


        //            #region "Save Multiple PDF PAGES"
        //            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //            string guid = Guid.NewGuid().ToString().ToUpper();
        //            string outputFile = $"{guid}_{timestamp}.pdf";
        //            string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";

        //            if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
        //            {
        //                try
        //                {
        //                    //delete files
        //                    await DeleteFiles(ListRoleListPath);
        //                }
        //                catch (Exception exd)
        //                {
        //                }
        //                result.Data = outputFile;
        //                result.State = EnumStatus.Success;
        //                result.Message = "Success.";

        //                await _unitOfWork.SaveChangesAsync();


        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "Something went wrong";
        //            }
        //            #endregion



        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
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






        #region Exam Letter Report
        [HttpPost("GetExamLetterReport")]
        public async Task<ApiResult<string>> GetExamLetterReport(ExamLetterReport model)
        {
            ActionName = "GetExamLetterReport()";
            return await Task.Run(async () =>
            {
                List<string> ListRoleListPath = new List<string>();
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetExamLetterReport(model);
                    if (data != null)
                    {

                        var groupedData = data.Tables[0]
                        .AsEnumerable()
                        .GroupBy(r => r.Field<int>("GroupCode"))
                        .Select(g => g.Key)
                        .ToList();

                        foreach (var group in groupedData)
                        {

                            var filteredRows = data.Tables[0]
                                .AsEnumerable()
                                .Where(r => r.Field<int>("GroupCode") == group)
                                .ToList();

                            DataTable filteredTable = filteredRows.Any()
                                ? filteredRows.CopyToDataTable()
                                 : data.Tables[0].Clone();

                            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                            var fileName = $"ExamLetterReport_{group}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ExamLetter.rdlc";
                            //
                            var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                            //
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("ExamLetterReport", filteredTable);
                            //localReport.AddDataSource("ExamLetterReport", data.Tables[0]);
                            var reportResult = localReport.Execute(RenderType.Pdf);
                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                            ListRoleListPath.Add(filepath);
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = "Success.";

                            //check file exists
                            if (!System.IO.Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }
                            //save



                            //end report

                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }


                    #region "Save Multiple PDF PAGES"
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string guid = Guid.NewGuid().ToString().ToUpper();
                    string outputFile = $"{guid}_{timestamp}.pdf";
                    string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";

                    if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                    {
                        try
                        {
                            //delete files
                            await DeleteFiles(ListRoleListPath);
                        }
                        catch (Exception exd)
                        {
                        }
                        result.Data = outputFile;
                        result.State = EnumStatus.Success;
                        result.Message = "Success.";

                        await _unitOfWork.SaveChangesAsync();


                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                    }
                    #endregion



                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }




        #endregion

        #region Student Admission Challan Receipt
        [HttpGet("GetStudentApplicationChallanReceipt/{ApplicationID}")]
        public async Task<ApiResult<string>> GetStudentApplicationChallanReceipt(int ApplicationID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentApplicationChallanReceipt(ApplicationID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ChallanReceipt_{ApplicationID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationChallanReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Student Allotment Letter Receipt
        [HttpGet("GetStudentAllotmentReceipt/{ApplicationID}")]
        public async Task<ApiResult<string>> GetStudentAllotmentReceipt(int ApplicationID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentAllotmentReceipt(ApplicationID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"Allotment_Letter_{ApplicationID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/AllotmentLetter.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Student Reporting Certificate
        [HttpGet("GetStudentReportingCertificate/{ApplicationID}")]
        public async Task<ApiResult<string>> GetStudentReportingCertificate(int ApplicationID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentReportingCertificate(ApplicationID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"Reporting_Certificate_{ApplicationID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Reporting_Certificate.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Student Fee GetPrincipalIssueCertificate
        [HttpPost("GetPrincipalIssueCertificate")]
        public async Task<ApiResult<string>> GetPrincipalIssueCertificate(PrincipalIssueCertificateModel model)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    //Create Temp Database
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Designation");
                    dt.Columns.Add("InsitituteName");
                    dt.Columns.Add("IssueDate");
                    dt.Rows.Add(model.Name, model.Designation, model.InstituteName, DateTime.Now.ToShortDateString());

                    if (dt != null)
                    {
                        //report
                        var fileName = $"PrincipalIssueCertificate_{model.UserID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/PrincipalIssueCertificate.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("PrincipalIssueCertificate", dt);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Student Enrolled Form
        [HttpPost("GetStudentEnrolledForm")]
        public async Task<ApiResult<string>> GetStudentEnrolledForm(ReportBaseModel model)
        {
            ActionName = "GetStudentEnrolledForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentEnrolledForm(model);
                    if (data?.Tables?.Count == 3)
                    {
                        //report
                        var fileName = $"EnrolledForm_{model.StudentID}.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentEnrolledmentForm.rdlc";
                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images

                        string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["Studentimg"]}";
                        data.Tables[0].Rows[0]["StudentImgb"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentSign"]}";
                        data.Tables[0].Rows[0]["StudentSignb"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                        string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSign"]}";
                        data.Tables[0].Rows[0]["RegistrarSignb"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
                        //rdlc

                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("StudentEnrolledmentForm", data.Tables[0]);
                        localReport.AddDataSource("Student_QualificationDetails", data.Tables[1]);
                        localReport.AddDataSource("StudentEnrollmentFeeDetails", data.Tables[2]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Examination form
        [HttpPost("GetExaminationForm")]
        public async Task<ApiResult<string>> GetExaminationForm(ReportBaseModel model)
        {
            int istudentId = 0;
            bool bisyearly = false;
            int iCourseType = 0;

            ActionName = "GetExaminationForm(ReportBaseModel model)";
            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => _unitOfWork.ReportRepository.GetExaminationForm(model));
                if (data != null)
                {

                    iCourseType = Convert.ToInt32(data.Tables[0].Rows[0]["CourseType"]);
                    bisyearly = Convert.ToBoolean(data.Tables[0].Rows[0]["IsYearly"]);
                    istudentId = Convert.ToInt32(data.Tables[0].Rows[0]["StudentID"]);




                    //report
                    var fileName = $"StudentExaminationForm_{model.StudentID}_{model.EndTermID}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentExaminationForm.rdlc";

                    //temp comment
                    string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                    data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                    //string stuimgFilepath = $"{CommonFuncationHelper.GetStudentFilesForOldBter(iCourseType, bisyearly, istudentId)}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                    //data.Tables[0].Rows[0]["StudentImg"] = await GetByteImages(stuimgFilepath);

                    //temp comment
                    string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                    data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                    //string stusignFilepath = $"{CommonFuncationHelper.GetStudentFilesForOldBter(iCourseType, bisyearly, istudentId)}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                    //data.Tables[0].Rows[0]["StudentSign"] = await GetByteImages(stusignFilepath);


                    //
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcpath);

                    localReport.AddDataSource("StudentExaminationForm", data.Tables[0]);
                    localReport.AddDataSource("StudentExaminationSubject", data.Tables[1]);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report

                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }
        #endregion

        #region "Roll Number Download"
        [RoleActionFilter(EnumRole.ACP, EnumRole.ACP_NonEng)]
        [HttpPost("DownloadStudentRollNumber")]
        public async Task<ApiResult<string>> DownloadStudentRollNumber([FromBody] List<DownloadnRollNoModel> Model)
        {
            ActionName = "DownloadStudentRollNumber(string EnrollmentNo)";
            List<string?> ListRoleListPath = new List<string?>();
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    foreach (var StudentExamID in Model)
                    {
                        DataTable dtStudentExamDetails = new DataTable();
                        dtStudentExamDetails.Columns.Add("StudentType");
                        dtStudentExamDetails.Columns.Add("InstituteName");
                        dtStudentExamDetails.Columns.Add("ProgrammeName");
                        dtStudentExamDetails.Columns.Add("SessionName");
                        dtStudentExamDetails.Columns.Add("CenterName");
                        dtStudentExamDetails.Columns.Add("BranchCode");

                        dtStudentExamDetails.Rows.Add(StudentExamID.StudentType, StudentExamID.InstituteNameEnglish, StudentExamID.EndTermName, StudentExamID.FinancialYearName, StudentExamID.CenterName, StudentExamID.BranchCode);
                        GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                        var data = await _unitOfWork.ReportRepository.GetStudentRollNoList(StudentExamID);
                        if (data != null)
                        {

                            //report
                            var fileName = $"StudentRollList_{Guid.NewGuid()}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentRollnumberListNew.rdlc";

                            //
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("StudentExamDetails", dtStudentExamDetails);
                            localReport.AddDataSource("StudentRollNumberList", data);
                            var reportResult = localReport.Execute(RenderType.Pdf);
                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                            //end report

                            ListRoleListPath.Add(filepath);
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }
                    }
                    #region "Save Multiple PDF PAGES"
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string guid = Guid.NewGuid().ToString().ToUpper();
                    string outputFile = $"{guid}_{Model.FirstOrDefault()?.EndTermName}_Sem_{Model.FirstOrDefault()?.SemesterID}_Ins{Model.FirstOrDefault()?.InstituteID}.pdf";
                    string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                    if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                    {
                        //delete files
                        await DeleteFiles(ListRoleListPath);
                        result.Data = outputFile;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        DownloadnRollNoModel ModInsert = Model.FirstOrDefault() ?? new DownloadnRollNoModel();
                        ModInsert.FileName = outputFile;
                        ModInsert.PDFType = (int)EnumPdfType.RollList;
                        ModInsert.Status = 11;
                        ModInsert.TotalStudent = Model.Sum(f => f.Totalstudent);
                        var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                    }
                    #endregion
                }


                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }



        [HttpPost("DownloadStudentRollNumber_InsituteWise")]
        public async Task<ApiResult<string>> DownloadStudentRollNumber_InsituteWise(DownloadnRollNoModel Request)
        {
            ActionName = "DownloadStudentRollNumber_InsituteWise(DownloadnRollNoModel Request)";

            string ipaddress = CommonFuncationHelper.GetIpAddress();
            var result = new ApiResult<string>();

            string filename = "DownloadStudentRollNumber_InsituteWise";
            // log
            CommonFuncationHelper.WriteTextLog("start : DownloadStudentRollNumber_InsituteWise", filename);
            try
            {
                var Model = await _unitOfWork.GenerateRollRepository.GetGenerateRollDataForPrint_Insitute(Request);
                // log
                CommonFuncationHelper.WriteTextLog($"1. : GetGenerateRollDataForPrint_Insitute (data count {Model.Count}) done.", filename);
                if (Model.Count > 0)
                {
                    int RollListDetailsLoopCount = 1;
                    foreach (var RollListDetails in Model.GroupBy(f => new { f.InstituteID, f.SemesterID }))
                    {
                        // log
                        CommonFuncationHelper.WriteTextLog($"2. : RollListDetails loop count {RollListDetailsLoopCount}", filename);

                        DownloadnRollNoModel ModInsert = RollListDetails.FirstOrDefault() ?? new DownloadnRollNoModel();
                        ModInsert.TotalStudent = RollListDetails.Sum(f => f.Totalstudent);

                        int StudentExamIDLoopCount = 1;
                        List<string?> ListRoleListPath = new List<string?>();
                        foreach (var StudentExamID in RollListDetails)
                        {
                            // log
                            CommonFuncationHelper.WriteTextLog($"3. : StudentExamIDLoopCount loop count {StudentExamIDLoopCount}", filename);

                            DataTable dtStudentExamDetails = new DataTable();
                            dtStudentExamDetails.Columns.Add("StudentType");
                            dtStudentExamDetails.Columns.Add("InstituteName");
                            dtStudentExamDetails.Columns.Add("ProgrammeName");
                            dtStudentExamDetails.Columns.Add("SessionName");
                            dtStudentExamDetails.Columns.Add("CenterName");
                            dtStudentExamDetails.Columns.Add("BranchCode");

                            dtStudentExamDetails.Rows.Add(
                                StudentExamID.StudentType,
                                StudentExamID.InstituteNameEnglish,
                                StudentExamID.EndTermName,
                                StudentExamID.FinancialYearName,
                                StudentExamID.CenterName,
                                StudentExamID.BranchCode);
                            GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();

                            var data = await _unitOfWork.ReportRepository.GetStudentRollNoList(StudentExamID);
                            // log
                            CommonFuncationHelper.WriteTextLog($"3. : StudentExamIDLoopCount (GetStudentRollNoList done.) loop count {StudentExamIDLoopCount}", filename);

                            if (data != null)
                            {
                                //report
                                var fileName = $"StudentRollList_{Guid.NewGuid()}.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentRollnumberListNew.rdlc";
                                //
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("StudentExamDetails", dtStudentExamDetails);
                                localReport.AddDataSource("StudentRollNumberList", data);
                                var reportResult = localReport.Execute(RenderType.Pdf);
                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                //end report
                                ListRoleListPath.Add(filepath);
                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = "Success.";


                            }
                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }

                            StudentExamIDLoopCount++;
                        }

                        #region "Save Multiple PDF PAGES"
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string guid = Guid.NewGuid().ToString().ToUpper();
                        string outputFile = $"{guid}_{timestamp}.pdf";
                        string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                        if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                        {
                            try
                            {
                                //delete files
                                await DeleteFiles(ListRoleListPath);
                            }
                            catch (Exception exd)
                            {
                            }
                            result.Data = outputFile;
                            result.State = EnumStatus.Success;
                            result.Message = "Success.";
                            ModInsert.FileName = outputFile;
                            ModInsert.PDFType = (int)EnumPdfType.RollList;
                            ModInsert.Status = 11;
                            var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);
                            await _unitOfWork.SaveChangesAsync();
                            // log
                            CommonFuncationHelper.WriteTextLog($"4. : RollListDetails (SaveRollNumbePDFData done) loop count {RollListDetailsLoopCount}", filename);

                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "Something went wrong";
                        }
                        #endregion

                        RollListDetailsLoopCount++;
                    }

                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }

                // log
                CommonFuncationHelper.WriteTextLog("end : DownloadStudentRollNumber_InsituteWise", filename);
                CommonFuncationHelper.WriteTextLog("end : DownloadStudentRollNumber_InsituteWise", filename);
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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

                // log
                CommonFuncationHelper.WriteTextLog($"end error : DownloadStudentRollNumber_InsituteWise ({ex.Message})", filename);
            }
            return result;
        }
        #endregion

        #region "Function Common helper for report"
        private string CheckFileExisits(string pFileName)
        {
            string strFileName = "";
            try
            {
                if (System.IO.File.Exists(pFileName))
                {
                    strFileName = pFileName;
                }
                else
                {
                    strFileName = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg");
                }
            }
            catch (Exception ex)
            {
                strFileName = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg");
            }
            return strFileName;
        }
        #endregion

        //[HttpPost]
        //private   string  MergePdfFiles(List<GenerateAdmitCardModel> ListData)
        //{

        //    try
        //    {

        //        string SourcePdfPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //        string[] filenames = System.IO.Directory.GetFiles(SourcePdfPath);
        //        string outputFileName = "Merge.pdf";
        //        string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFileName}";
        //        Document doc = new Document();
        //        PdfCopy writer = new PdfCopy(doc, new FileStream(outputPath, FileMode.Create));
        //        if (writer == null)
        //        {
        //            return "";
        //        }
        //        doc.Open();
        //        foreach (var filename in ListData)
        //        {
        //            PdfReader reader = new PdfReader(filename.AdmitCardPath);
        //            reader.ConsolidateNamedDestinations();
        //            for (int i = 1; i <= reader.NumberOfPages; i++)
        //            {
        //                PdfImportedPage page = writer.GetImportedPage(reader, i);
        //                writer.AddPage(page);
        //            }
        //            reader.Close();
        //        }
        //        writer.Close();
        //        doc.Close();
        //        return   outputPath??"";
        //    }
        //    catch (Exception ex)
        //    {
        //        await _unitOfWork.DisposeAsync();
        //        // Write error log
        //        var nex = new NewException
        //        {
        //            PageName = "PrintPDf",
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //         CreateErrorLog(nex, _unitOfWork);
        //    }
        //}


        [HttpPost("MergePdfFilesAsync")]
        public async Task<bool> MergePdfFilesAsync(List<string?> sourceFiles, string poutputPath = "")
        {

            bool bRetValue = false;
            try
            {
                if (sourceFiles == null || sourceFiles.Count == 0)
                    throw new ArgumentException("No source files provided.");

                //await Task.Run(() =>
                //{
                //    using (FileStream stream = new FileStream(poutputPath, FileMode.Create))
                //    using (iTextSharp.text.Document document = new iTextSharp.text.Document())
                //    using (PdfCopy pdfCopy = new PdfCopy(document, stream))
                //    {
                //        document.Open();

                //        foreach (var file in sourceFiles)
                //        {
                //            using (PdfReader reader = new PdfReader(file))
                //            {
                //                for (int i = 1; i <= reader.NumberOfPages; i++)
                //                {
                //                    PdfImportedPage page = pdfCopy.GetImportedPage(reader, i);
                //                    pdfCopy.AddPage(page);
                //                }
                //            }
                //        }
                //    }
                //});


                using PdfSharpCore.Pdf.PdfDocument outputDocument = new PdfSharpCore.Pdf.PdfDocument();

                foreach (var file in sourceFiles)
                {
                    if (!System.IO.File.Exists(file))
                        throw new System.IO.FileNotFoundException($"File not found: {file}");

                    using PdfSharpCore.Pdf.PdfDocument inputDocument =
                        PdfSharpCore.Pdf.IO.PdfReader.Open(file, PdfSharpCore.Pdf.IO.PdfDocumentOpenMode.Import);

                    for (int i = 0; i < inputDocument.PageCount; i++)
                    {
                        outputDocument.AddPage(inputDocument.Pages[i]);
                    }
                }

                outputDocument.Save(poutputPath);

                bRetValue = true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = "MergePdfFilesAsync",
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return bRetValue;
        }


        [HttpPost("DeleteFiles")]
        public async Task<bool> DeleteFiles(List<string?> sourceFiles)
        {

            bool bRetValue = false;
            try
            {
                await Task.Run(() =>
                {

                    foreach (var item in sourceFiles)
                    {
                        if (System.IO.File.Exists(item))
                        {

                            System.IO.File.Delete(item);
                        }
                    }
                });
                bRetValue = true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = "DeleteFiles",
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return bRetValue;
        }

        #region ITI Application Form Preview
        [HttpPost("GetITIApplicationFormPreview")]
        public async Task<ApiResult<string>> GetITIApplicationFormPreview([FromBody] ItiApplicationSearchModel Model)
        {
            ActionName = "GetITIApplicationFormPreview(string ApplicationID)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    //ListData.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    //});

                    var data = await _unitOfWork.ReportRepository.GetITIApplicationFormPreview(Model);
                    if (data?.Tables?.Count == 5)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ITIApplicationFormPreview_{Model.StudentName}_{Model.ApplicationID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ITIApplicationFormPreview.rdlc";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images

                        string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentPhoto"]}";
                        data.Tables[0].Rows[0]["Studentimg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["SignaturePhoto"]}";
                        data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                        //string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
                        //data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
                        //rdlc

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Student_Personal_Details", data.Tables[0]);
                        localReport.AddDataSource("Student_Qualification_Details", data.Tables[1]);
                        localReport.AddDataSource("Student_Option_Details", data.Tables[2]);
                        localReport.AddDataSource("Student_Uploaded_Documents", data.Tables[4]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Application Form Preview
        [HttpPost("GetApplicationFormPreview")]
        public async Task<ApiResult<string>> GetApplicationFormPreview([FromBody] BterSearchModel student)
        {
            ActionName = "GetApplicationFormPreview(string ApplicationID)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.ReportRepository.GetApplicationFormPreview(student);
                    if (data?.Tables?.Count == 6)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ApplicationFormPreview_{student.StudentName}_{student.ApplicationId}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images
                        string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentPhoto"]}";
                        data.Tables[0].Rows[0]["Studentimg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["SignaturePhoto"]}";
                        data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));



                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Student_Personal_Details", data.Tables[0]);
                        localReport.AddDataSource("Student_Qualification_Details", data.Tables[1]);
                        localReport.AddDataSource("Student_Option_Details", data.Tables[2]);
                        localReport.AddDataSource("Student_Uploaded_Documents", data.Tables[4]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Examiner Details Receipt
        [HttpGet("GetExaminerDetails/{StaffID}/{DepartmentID}")]
        public async Task<ApiResult<string>> GetExaminerDetails(int StaffID, int DepartmentID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetExaminerDetails(StaffID, DepartmentID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ExaminerDetails_{StaffID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ExaminersDetails.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ExaminersDetails", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Theory Marks Absent Report
        //[HttpGet("GetAbsentReport")]
        //public async Task<ApiResult<string>> GetAbsentReport([FromBody] List<DownloadnRollNoModel> Model)
        //{
        //    ActionName = "GetAbsentReport()";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {
        //            foreach (var StudentExamID in Model)
        //            {
        //                DataTable dtStudentExamDetails = new DataTable();
        //                dtStudentExamDetails.Columns.Add("StudentType");
        //                dtStudentExamDetails.Columns.Add("InstituteName");
        //                dtStudentExamDetails.Columns.Add("ProgrammeName");
        //                dtStudentExamDetails.Columns.Add("SessionName");
        //                dtStudentExamDetails.Columns.Add("CenterName");
        //                dtStudentExamDetails.Columns.Add("BranchCode");

        //                dtStudentExamDetails.Rows.Add(StudentExamID.StudentType, StudentExamID.InstituteNameEnglish, StudentExamID.EndTermName, StudentExamID.FinancialYearName, StudentExamID.CenterName, StudentExamID.BranchCode);
        //                GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
        //                var data = await _unitOfWork.ReportRepository.GetStudentRollNoList(StudentExamID);
        //                if (data != null)
        //                {

        //                    //report
        //                    var fileName = $"StudentRollList_{Guid.NewGuid()}.pdf";
        //                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentRollnumberListNew.rdlc";

        //                    //
        //                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                    LocalReport localReport = new LocalReport(rdlcpath);
        //                    localReport.AddDataSource("StudentExamDetails", dtStudentExamDetails);
        //                    localReport.AddDataSource("StudentRollNumberList", data);
        //                    var reportResult = localReport.Execute(RenderType.Pdf);
        //                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
        //                    //end report

        //                    ListRoleListPath.Add(filepath);
        //                    result.Data = fileName;
        //                    result.State = EnumStatus.Success;
        //                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




        //                }
        //                else
        //                {
        //                    result.State = EnumStatus.Warning;
        //                    result.Message = Constants.MSG_DATA_NOT_FOUND;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
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

        [HttpPost("TheoryMarkListReport")]
        public async Task<ApiResult<string>> TheoryMarkListReport(ReportCustomizeBaseModel model)
        {
            ActionName = "GetAbsentReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    List<ExamResultViewModel> ListData = new List<ExamResultViewModel>();
                    var data = await _unitOfWork.ReportRepository.TheoryMarkListReport(model);
                    if (data.Tables[0].Rows.Count > 1)
                    {
                        ListData = CommonFuncationHelper.ConvertDataTable<List<ExamResultViewModel>>(data.Tables[0]);
                    }
                    if (ListData.Count > 0)
                    {

                        foreach (var item in ListData.GroupBy(f => f.StreamCode))
                        {




                            DataTable dt = data.Tables[0].AsEnumerable()
                                                     .Where(row => row.Field<string>("StreamCode") == item.Key)
                                                     .CopyToDataTable();


                            if (dt != null)
                            {
                                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                                //report
                                var fileName = $"Theory_Marks_Report.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Theory_Marks_Absent_Report.rdlc";
                                //
                                var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                                //
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("Theory_Marks_Report", dt);
                                var reportResult = localReport.Execute(RenderType.Pdf);
                                //check file exists
                                if (!System.IO.Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                                //save


                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                //end report

                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            }

                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Colleges Wise Examination Reports
        [HttpPost("GetCollegesWiseExaminationReports")]
        public async Task<ApiResult<DataTable>> GetCollegesWiseExaminationReports(CollegesWiseExaminationRptSearchModel model)
        {
            ActionName = "GetCollegesWiseExaminationReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCollegesWiseExaminationReports(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        //#region "TimeTable"
        //[HttpPost("DownloadTimeTable")]
        //public async Task<ApiResult<string>> DownloadTimeTable(ReportBaseModel model)
        //{
        //    ActionName = "GetStudentEnrolledForm(string EnrollmentNo)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {
        //            List<TimeTableHeaderModel> objList = new List<TimeTableHeaderModel>();
        //            model.Action = "_GetTimeTableHeader";
        //            var dataList = await _unitOfWork.ReportRepository.DownloadTimeTable(model);
        //            if (dataList != null)
        //            {
        //                objList = CommonFuncationHelper.ConvertDataTable<List<TimeTableHeaderModel>>(dataList.Tables[0]);
        //            }
        //            if (objList.Count > 0)
        //            {
        //                List<string> Timettable = new List<string>();
        //                foreach (var item in objList)
        //                {
        //                    ReportBaseModel objTimeTableList = new ReportBaseModel();
        //                    objTimeTableList.Action = "_TimeTableList";
        //                    objTimeTableList.SemesterID = item.SemesterID;
        //                    objTimeTableList.EndTermID = item.EndTermID;
        //                    objTimeTableList.ExamType = model.ExamType;
        //                    objTimeTableList.Eng_NonEng = model.Eng_NonEng;
        //                    objTimeTableList.CommonSubjectText = item.CommonSubjectText;
        //                    var data = await _unitOfWork.ReportRepository.DownloadTimeTable(objTimeTableList);

        //                    //time tester
        //                    DataTable dtTimeTableHeader = new DataTable();
        //                    dtTimeTableHeader.Columns.Add("OrderNumber");
        //                    dtTimeTableHeader.Columns.Add("EndTermName");
        //                    dtTimeTableHeader.Columns.Add("FinancialYearName");
        //                    dtTimeTableHeader.Columns.Add("CurrentDate");
        //                    dtTimeTableHeader.Columns.Add("CourseTypeName");
        //                    dtTimeTableHeader.Columns.Add("YearName");
        //                    dtTimeTableHeader.Columns.Add("CourseTypeNameFull");
        //                    dtTimeTableHeader.Columns.Add("ExamName");
        //                    dtTimeTableHeader.Columns.Add("ExamScheme");
        //                    dtTimeTableHeader.Columns.Add("CommonSubjectText");
        //                    dtTimeTableHeader.Rows.Add(item.OrderNo,
        //                        item.EndTermName, item.FinancialYearName, item.CurrentDate, item.CourseTypeName, item.YearName
        //                        , item.CourseTypeNameFull, item.ExamName, item.ExamScheme, item.CommonSubjectText);

        //                    if (data.Tables?.Count > 0)
        //                    {
        //                        //report

        //                        var fileName = $"TimeTable_{model.FinancialYearID}_{model.EndTermID}_{Guid.NewGuid()}.pdf";
        //                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/TimeTableOrder.rdlc";

        //                        LocalReport localReport = new LocalReport(rdlcpath);
        //                        localReport.AddDataSource("TimeTableDetails", data.Tables[0]);
        //                        localReport.AddDataSource("TimeTableHeader", dtTimeTableHeader);

        //                        var reportResult = localReport.Execute(RenderType.Pdf);

        //                        //check file exists
        //                        if (!System.IO.Directory.Exists(folderPath))
        //                        {
        //                            Directory.CreateDirectory(folderPath);
        //                        }
        //                        //save
        //                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

        //                        Timettable.Add(filepath);
        //                        //end report
        //                        result.Data = fileName;
        //                        result.State = EnumStatus.Success;
        //                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //                    }
        //                    else
        //                    {
        //                        result.State = EnumStatus.Warning;
        //                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
        //                    }
        //                }

        //                #region "Save Multiple PDF PAGES"
        //                string outputFile = $"MergePDF_TimeTable{Guid.NewGuid().ToString().ToUpper()}.pdf";
        //                string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
        //                List<string?> strSoureFiles = Timettable?.ToList();
        //                if (strSoureFiles?.Count > 0)
        //                {
        //                    if (await MergePdfFilesAsync(strSoureFiles, outputPath))
        //                    {
        //                        result.Data = outputFile;
        //                        result.State = EnumStatus.Success;
        //                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

        //                        DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
        //                        ModInsert.FileName = outputFile;
        //                        ModInsert.PDFType = (int)EnumPdfType.TimeTable;
        //                        ModInsert.Status = 11;
        //                        ModInsert.DepartmentID = 1;
        //                        ModInsert.Eng_NonEng = model.Eng_NonEng.Value;
        //                        ModInsert.EndTermID = model.EndTermID;
        //                        if (model.SemesterID == 1)
        //                        {
        //                            ModInsert.SemesterID = 0;
        //                        }
        //                        else
        //                        {
        //                            ModInsert.SemesterID = 1;
        //                        }


        //                        var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);


        //                    }
        //                    else
        //                    {
        //                        result.State = EnumStatus.Error;
        //                        result.ErrorMessage = "Something went wrong";
        //                    }
        //                }
        //                else
        //                {
        //                    result.State = EnumStatus.Error;
        //                    result.ErrorMessage = "Something went wrong";
        //                }

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "No Record Found";

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
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

        //#endregion


        [HttpPost("DownloadTimeTable")]
        public async Task<ApiResult<string>> DownloadTimeTable(ReportBaseModel model)
        {
            ActionName = "GetStudentEnrolledForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    List<TimeTableHeaderModel> objList = new List<TimeTableHeaderModel>();
                    model.Action = "_GetTimeTableHeader";
                    var dataList = await _unitOfWork.ReportRepository.DownloadTimeTable(model);

                    if (dataList != null)
                    {
                        objList = CommonFuncationHelper.ConvertDataTable<List<TimeTableHeaderModel>>(dataList.Tables[0]);
                    }

                    if (objList.Count > 0)
                    {
                        List<string> Timettable = new List<string>();
                        foreach (var item in objList)
                        {
                            ReportBaseModel objTimeTableList = new ReportBaseModel
                            {
                                Action = "_TimeTableList",
                                SemesterID = item.SemesterID,
                                EndTermID = item.EndTermID,
                                ExamType = model.ExamType,
                                Eng_NonEng = model.Eng_NonEng,
                                CommonSubjectText = item.CommonSubjectText
                            };

                            var data = await _unitOfWork.ReportRepository.DownloadTimeTable(objTimeTableList);


                            // Prepare header table
                            DataTable dtTimeTableHeader = new DataTable();
                            dtTimeTableHeader.Columns.Add("OrderNumber");
                            dtTimeTableHeader.Columns.Add("EndTermName");
                            dtTimeTableHeader.Columns.Add("FinancialYearName");
                            dtTimeTableHeader.Columns.Add("CurrentDate");
                            dtTimeTableHeader.Columns.Add("CourseTypeName");
                            dtTimeTableHeader.Columns.Add("YearName");
                            dtTimeTableHeader.Columns.Add("CourseTypeNameFull");
                            dtTimeTableHeader.Columns.Add("ExamName");
                            dtTimeTableHeader.Columns.Add("ExamScheme");
                            dtTimeTableHeader.Columns.Add("CommonSubjectText");
                            dtTimeTableHeader.Columns.Add("SignatureFile", typeof(byte[]));

                            // Determine file type and extension
                            RenderType renderType = model.FileType?.ToLower() == "word" ? RenderType.Word : RenderType.Pdf;
                            string fileExtension = renderType == RenderType.Word ? "docx" : "pdf";

                            var mimeType = renderType == RenderType.Word ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/pdf";

                            var fileName = $"TimeTable_{model.FinancialYearID}_{model.EndTermID}_{Guid.NewGuid()}.{fileExtension}";
                            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                            var filepath = $"{folderPath}/{fileName}";
                            var rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/TimeTableOrder.rdlc";

                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            string stuimgFilepath = $"{ConfigurationHelper.RootPath}StaticFiles/Apr012025060950764086.png";
                            Console.WriteLine(stuimgFilepath);

                            string photoFileName = item.SignatureFile;
                            string fullPhotoPath = Path.Combine(ConfigurationHelper.RootPath, "StaticFiles", Convert.ToString(item.SignatureFile));
                            byte[] photo;

                            if (System.IO.File.Exists(fullPhotoPath))
                            {
                                photo = System.IO.File.ReadAllBytes(fullPhotoPath); // This must be byte[]

                            }
                            else
                            {
                                photo = System.IO.File.ReadAllBytes(Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg"));
                            }

                            dtTimeTableHeader.Rows.Add(item.OrderNo, item.EndTermName, item.FinancialYearName, item.CurrentDate,
                                item.CourseTypeName, item.YearName, item.CourseTypeNameFull, item.ExamName, item.ExamScheme, item.CommonSubjectText, photo);

                            if (data.Tables?.Count > 0)
                            {
                                //// Determine file type and extension
                                //RenderType renderType = model.FileType?.ToLower() == "word" ? RenderType.Word : RenderType.Pdf;
                                //string fileExtension = renderType == RenderType.Word ? "docx" : "pdf";

                                //var mimeType = renderType == RenderType.Word ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/pdf";




                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("TimeTableDetails", data.Tables[0]);
                                localReport.AddDataSource("TimeTableHeader", dtTimeTableHeader);

                                var reportResult = localReport.Execute(RenderType.Pdf);

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                Timettable.Add(filepath);

                                // Set result for each individual file
                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            }
                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                            }
                        }

                        // Merge only PDFs
                        if (model.FileType?.ToLower() == "pdf" && Timettable?.Count > 0)
                        {
                            string outputFile = $"MergePDF_TimeTable_{Guid.NewGuid().ToString().ToUpper()}.pdf";
                            string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";

                            if (await MergePdfFilesAsync(Timettable, outputPath))
                            {
                                result.Data = outputFile;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                                DownloadnRollNoModel ModInsert = new DownloadnRollNoModel
                                {
                                    FileName = outputFile,
                                    PDFType = (int)EnumPdfType.TimeTable,
                                    Status = 11,
                                    DepartmentID = 1,
                                    Eng_NonEng = model.Eng_NonEng.Value,
                                    EndTermID = model.EndTermID,
                                    SemesterID = model.SemesterID == 1 ? 0 : 1
                                };

                                var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);
                            }
                            else
                            {
                                result.State = EnumStatus.Error;
                                result.ErrorMessage = "Something went wrong while merging PDF files.";
                            }
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "No Record Found";
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();

                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }
        //word cannot open the file because the file format does not match the file extension.


        [HttpPost("ItiDownloadTimeTable")]
        public async Task<ApiResult<string>> ItiDownloadTimeTable(ReportBaseModel model)
        {
            ActionName = "GetStudentEnrolledForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    TimeTableHeaderModel objList = new TimeTableHeaderModel();
                    model.Action = "_TimeTableList";
                    var dataList = await _unitOfWork.ReportRepository.ItiDownloadTimeTable(model);

                    if (dataList != null)
                    {
                        objList = CommonFuncationHelper.ConvertDataTable<TimeTableHeaderModel>(dataList.Tables[0]);

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        string stuimgFilepath = $"{ConfigurationHelper.RootPath}StaticFiles/Apr012025060950764086.png";
                        Console.WriteLine(stuimgFilepath);

                        if (!dataList.Tables[0].Columns.Contains("SignatureFile1"))
                        {
                            dataList.Tables[0].Columns.Add("SignatureFile1", typeof(byte[]));
                        }
                        string photoFileName = dataList.Tables[0].Rows[0]["SignatureFile"].ToString();
                        string fullPhotoPath = Path.Combine(ConfigurationHelper.RootPath, "StaticFiles", Convert.ToString(photoFileName));

                        if (System.IO.File.Exists(fullPhotoPath))
                        {
                            dataList.Tables[0].Rows[0]["SignatureFile1"] = System.IO.File.ReadAllBytes(fullPhotoPath); // This must be byte[]

                        }
                        else
                        {
                            dataList.Tables[0].Rows[0]["SignatureFile1"] = System.IO.File.ReadAllBytes(Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg"));
                        }
                    }

                    string filepath = ""; string fileName = "";
                    if (objList != null)
                    {
                        List<string> Timettable = new List<string>();
                        // Prepare header table
                        DataTable dtTimeTableHeader = new DataTable();
                        dtTimeTableHeader.Columns.Add("OrderNumber");
                        dtTimeTableHeader.Columns.Add("EndTermName");
                        dtTimeTableHeader.Columns.Add("FinancialYearName");
                        dtTimeTableHeader.Columns.Add("CurrentDate");
                        dtTimeTableHeader.Columns.Add("CourseTypeName");
                        dtTimeTableHeader.Columns.Add("YearName");
                        dtTimeTableHeader.Columns.Add("CourseTypeNameFull");
                        dtTimeTableHeader.Columns.Add("ExamName");
                        dtTimeTableHeader.Columns.Add("ExamScheme");
                        dtTimeTableHeader.Columns.Add("CommonSubjectText");
                        dtTimeTableHeader.Columns.Add("SignatureFile1");

                        dtTimeTableHeader.Rows.Add(objList.OrderNo, objList.EndTermName, objList.FinancialYearName, objList.CurrentDate,
                            objList.CourseTypeName, objList.YearName, objList.CourseTypeNameFull, objList.ExamName, objList.ExamScheme, objList.CommonSubjectText, objList.SignatureFile);

                        if (dataList?.Tables?.Count > 0)
                        {

                            // Determine file type and extension
                            RenderType renderType = model.FileType?.ToLower() == "word" ? RenderType.Word : RenderType.Pdf;
                            string fileExtension = renderType == RenderType.Word ? "docx" : "pdf";

                            var mimeType = renderType == RenderType.Word ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/pdf";

                            fileName = $"TimeTable_{model.FinancialYearID}_{model.EndTermID}_{Guid.NewGuid()}.{fileExtension}";
                            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder_ITI}{Constants.TimeTableFolder_ITI}";
                            filepath = $"{folderPath}/{fileName}";
                            var rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/TimeTableOrder.rdlc";

                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("TimeTableDetails", dataList.Tables[1]);
                            localReport.AddDataSource("TimeTableHeader", dataList.Tables[0]);

                            var reportResult = localReport.Execute(renderType);

                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                            Timettable.Add(filepath);

                            // Set result for each individual file
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                        }


                        // Merge only PDFs
                        DownloadnRollNoModel ModInsert = new DownloadnRollNoModel
                        {
                            FileName = fileName,
                            PDFType = (int)EnumPdfType.TimeTable,
                            Status = 11,
                            DepartmentID = 2,
                            Eng_NonEng = model.Eng_NonEng.Value,
                            EndTermID = model.EndTermID,
                            SemesterID = 0
                        };
                        var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "No Record Found";
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();

                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }

        [HttpPost("DownloadTimeTable_New")]
        public async Task<ApiResult<string>> DownloadTimeTable_BackUp(ReportBaseModel model)
        {
            ActionName = "GetStudentEnrolledForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try

                {
                    model.Action = "_GetTimeTableHeader";
                    var PublistSemesterList = await _unitOfWork.ReportRepository.DownloadTimeTable(model);


                    var data = await _unitOfWork.ReportRepository.DownloadTimeTable(model);
                    if (data != null)
                    {
                        //report
                        var fileName = $"TimeTable_{model.FinancialYearID}_{model.EndTermID}.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/TimeTableOrder.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("TimeTableDetails", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        //#endregion

        #region "Download Student Profile Details"
        [HttpPost("DownloadStudentProfileDetails")]
        public async Task<ApiResult<string>> DownloadStudentProfileDetails(ReportBaseModel model)
        {
            ActionName = "GetStudentEnrolledForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadStudentProfileDetails(model);
                    if (data.Rows?.Count > 1)
                    {
                        //report
                        var fileName = $"StudentProfileDetails{model.FinancialYearID}_{model.EndTermID}.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentProfileDetails.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentProfileData", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region ITI Student Fee Receipt
        [HttpGet("GetITIStudentFeeReceipt/{EnrollmentNo}")]
        public async Task<ApiResult<string>> GetITIStudentFeeReceipt(string EnrollmentNo)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIStudentFeeReceipt(EnrollmentNo);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ITIFeeReceipt_{EnrollmentNo}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIFeeReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region ITI Student Reveal Fee Receipt
        [HttpGet("GetITIStudentRevealFeeReceipt/{EnrollmentNo}")]
        public async Task<ApiResult<string>> GetITIStudentRevealFeeReceipt(string EnrollmentNo)
        {
            ActionName = "GetITIStudentRevealFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIStudentRevealFeeReceipt(EnrollmentNo);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ITIFeeReceipt_{EnrollmentNo}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIFeeRevalReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        #region ITI Application Form 
        [HttpPost("GetITIApplicationForm")]
        public async Task<ApiResult<string>> GetITIApplicationForm([FromBody] ItiApplicationSearchModel Model)
        {
            ActionName = "GetITIApplicationForm(string ApplicationID)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    //ListData.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    //});

                    var data = await _unitOfWork.ReportRepository.GetITIApplicationForm(Model);
                    if (data?.Tables?.Count > 0)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //  var fileName = $"ITIApplicationForm_{Model.StudentName}_{Model.ApplicationID}.pdf";
                        var fileName = $"ITIApplicationForm_{Model.ApplicationID}_{Guid.NewGuid()}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = "";
                        int admissionType = data.Tables[0].Rows[0].Field<int?>("DirectAdmissionType") ?? 0;
                        if (admissionType == 1)
                        {
                            rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIApplicationDirectAdmission.rdlc";
                        }
                        else if (admissionType == 9)// for direct admission private 
                        {
                            rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIApplicationDirectAdmissionPrivate.rdlc";
                        }
                        else
                        {
                            rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIApplicationForm.rdlc";
                        }
                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images
                        string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentPhoto"]}";
                        data.Tables[0].Rows[0]["Studentimg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["SignaturePhoto"]}";
                        data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                        //string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
                        //data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
                        //rdlc

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Student_Personal_Details", data.Tables[0]);
                        localReport.AddDataSource("Student_Qualification_Details", data.Tables[1]);
                        localReport.AddDataSource("Student_Option_Details", data.Tables[2]);
                        localReport.AddDataSource("Student_Uploaded_Documents", data.Tables[4]);

                        if (admissionType == 9)
                        {
                            localReport.AddDataSource("Student_ExpDetails", data.Tables[5]);
                        }

                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region ITI Admit Card
        [HttpPost("GetITIStudentAdmitCard")]
        public async Task<ApiResult<string>> GetITIStudentAdmitCard([FromBody] List<GenerateAdmitCardModel> ListData)
        {
            ActionName = "GetStudentAdmitCard(string EnrollmentNo)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    ListData.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });

                    foreach (var student in ListData)
                    {
                        var data = await _unitOfWork.ReportRepository.GetITIStudentAdmitCard(student);
                        if (data?.Tables?.Count == 2)
                        {
                            //report
                            var fileName = $"ITIAdmitCard_{student.StudentID}_{student.StudentExamID}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIAdmitCard.rdlc";

                            student.AdmitCardPath = filepath;
                            student.AdmitCard = fileName;
                            //provider                      
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            //images

                            //string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                            //data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                            //string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                            //data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                            //string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
                            //data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
                            //rdlc

                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("ITIStudentAdmitCard", data.Tables[0]);
                            localReport.AddDataSource("ITIStudentAdmitCard_Subject", data.Tables[1]);
                            var reportResult = localReport.Execute(RenderType.Pdf);

                            //check file exists
                            if (!System.IO.Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            //save
                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                            //end report
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }

                    }

                    var Issuccess = await _unitOfWork.GenerateAdmitCardRepository.UpdateAdmitCard(ListData);
                    if (Issuccess > 0)
                    {
                        result.Data = Issuccess.ToString();
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }

                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        [HttpPost("GetITIStudentAdmitCardBulk")]
        public async Task<ApiResult<string>> GetITIStudentAdmitCardBulk([FromBody] DownloadDataPagingListModel Model)
        {
            ActionName = "GetStudentAdmitCardBulk(string EnrollmentNo)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder_ITI}{Constants.AdmitCardFolder_ITI}";
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    List<GenerateAdmitCardModel> ListData = new List<GenerateAdmitCardModel>();

                    if (!string.IsNullOrEmpty(Model.StudentExamIDs))
                    {
                        foreach (var StudentExamID in Model.StudentExamIDs.Split(','))
                        {
                            GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                            var data = await _unitOfWork.ReportRepository.GetITIStudentAdmitCardBulk(Convert.ToInt32(StudentExamID),
                                Model.DepartmentID, Model.EndTermID);
                            if (data?.Tables?.Count == 2)
                            {
                                if (data.Tables[0].Rows.Count > 0)
                                {


                                    int studentID = Convert.ToInt32(data.Tables[0].Rows[0]["StudentID"]);
                                    //report
                                    var fileName = $"ITIAdmitCard_{studentID}_{StudentExamID}_{data.Tables[0].Rows[0]["RollNo"]}.pdf";
                                    string filepath = $"{folderPath}/{fileName}";
                                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIAdmitCard.rdlc";


                                    #region "Add Object"
                                    objStudent.StudentID = studentID;
                                    objStudent.AdmitCardPath = filepath;
                                    objStudent.AdmitCard = fileName;
                                    objStudent.StudentExamID = Convert.ToInt32(StudentExamID);
                                    objStudent.IPAddress = ipaddress;
                                    objStudent.DepartmentID = Model.DepartmentID;
                                    ListData.Add(objStudent);
                                    #endregion



                                    //provider                      
                                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                    //images

                                    string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentPhoto"]}";
                                    data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                                    string stuimgFilepath1 = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["Registrar_Signature"]}";
                                    data.Tables[0].Rows[0]["NodalSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath1));


                                    LocalReport localReport = new LocalReport(rdlcpath);
                                    localReport.AddDataSource("ITIStudentAdmitCard", data.Tables[0]);
                                    localReport.AddDataSource("ITIStudentAdmitCard_Subject", data.Tables[1]);
                                    var reportResult = localReport.Execute(RenderType.Pdf);

                                    //check file exists
                                    if (!System.IO.Directory.Exists(folderPath))
                                    {
                                        Directory.CreateDirectory(folderPath);
                                    }
                                    //save
                                    //save
                                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                    //end report
                                }
                                else
                                {
                                    result.State = EnumStatus.Warning;
                                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                                }
                            }
                        }

                        var Issuccess = await _unitOfWork.GenerateAdmitCardRepository.UpdateAdmitCard(ListData);
                        if (Issuccess > 0)
                        {

                            #region "Save Multiple PDF PAGES"
                            string outputFile = $"MergePDF_{Model.InstituteID}_{Model.SemesterID}_{Model.EndTermID}.pdf";
                            string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                            List<string?> strSoureFiles = ListData.Select(s => s.AdmitCardPath).ToList();
                            if (await MergePdfFilesAsync(strSoureFiles, outputPath))
                            {
                                result.Data = outputFile;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            }
                            else
                            {
                                result.State = EnumStatus.Error;
                                result.ErrorMessage = "Something went wrong";
                            }
                            #endregion
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                        result.ErrorMessage = Convert.ToString(Model.StudentExamIDs);
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        [HttpPost("GetITIStudentAdmitCardBulk_CollegeWise")]
        public async Task<ApiResult<string>> GetITIStudentAdmitCardBulk_CollegeWise([FromBody] GenerateAdmitCardSearchModel Model)
        {
            ActionName = "GetITIStudentAdmitCardBulk_CollegeWise(GenerateAdmitCardSearchModel Model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder_ITI}{Constants.AdmitCardFolder_ITI}";
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            string iStudentExamID = "";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    //List<GenerateAdmitCardModel> ListData = new List<GenerateAdmitCardModel>();
                    var ListInsituteData = await _unitOfWork.GenerateAdmitCardRepository.GetITIGenerateAdmitCardDataBulk_CollegeWise(Model);
                    if (ListInsituteData.Count > 0)
                    {
                        foreach (var childdata in ListInsituteData)
                        {
                            List<GenerateAdmitCardModel> ListData = new List<GenerateAdmitCardModel>();
                            //set data
                            Model.SemesterID = childdata.SemesterID;
                            Model.InstituteID = childdata.InstituteID;
                            Model.DepartmentID = 2;
                            Model.EndTermID = childdata.EndTermID;
                            Model.Eng_NonEng = childdata.Eng_NonEng;
                            Model.TotalRecord = childdata.TotalRecord;
                            //semester wise Data
                            foreach (var StudentExamID in childdata.StudentExamIDs.Split(','))
                            {
                                if (!string.IsNullOrEmpty(StudentExamID))
                                {

                                    GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                                    var data = await _unitOfWork.ReportRepository.GetITIStudentAdmitCardBulk(Convert.ToInt32(StudentExamID),
                                        Model.DepartmentID, Model.EndTermID);
                                    if (data?.Tables?.Count == 2)
                                    {
                                        if (data.Tables[0].Rows.Count > 0)
                                        {
                                            var row = data.Tables[0].Rows[0];

                                            string qrText = $"Student Name : {row["StudentName"]}\n" +
                                                             $"Roll No      : {row["RollNo"]}\n" +
                                                             $"Stream       : {row["StreamName"]}\n" +
                                                             $"Father Name  : {row["FatherName"]}";

                                            //var text = "Enrollment No : 123456";
                                            var qrcode = CommonFuncationHelper.GenerateQrCode(qrText);
                                            int studentID = Convert.ToInt32(data.Tables[0].Rows[0]["StudentID"]);
                                            //report
                                            var fileName = $"ITIAdmitCard_{studentID}_{StudentExamID}_{data.Tables[0].Rows[0]["RollNo"]}.pdf";
                                            string filepath = $"{folderPath}/{fileName}";
                                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIAdmitCard.rdlc";
                                            #region "Add Object"
                                            objStudent.StudentID = studentID;
                                            objStudent.AdmitCardPath = filepath;
                                            objStudent.AdmitCard = fileName;
                                            objStudent.StudentExamID = Convert.ToInt32(StudentExamID);
                                            objStudent.IPAddress = ipaddress;
                                            objStudent.DepartmentID = Model.DepartmentID;
                                            ListData.Add(objStudent);
                                            #endregion
                                            //provider                      
                                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                            //images

                                            string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentPhoto"]}";
                                            data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                                            string stuimgFilepath1 = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["Registrar_Signature"]}";
                                            data.Tables[0].Rows[0]["NodalSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath1));



                                            // QR Code - bind directly onto the same table as the other images
                                            //if (!data.Tables[0].Columns.Contains("QRCode"))
                                            //    data.Tables[0].Columns.Add("QRCode", typeof(byte[]));

                                            //data.Tables[0].Rows[0]["QRCode"] = qrcode;
                                            //var dataQR = new DataTable();
                                            //dataQR.Columns.Add("qrcode", typeof(byte[]));
                                            //var row = dataQR.NewRow();
                                            //row["qrcode"] = qrcode;
                                            //dataQR.Rows.Add(row);

                                            var dataQR = new DataTable();
                                            dataQR.Columns.Add("qrcode", typeof(byte[]));
                                            var qrRow = dataQR.NewRow();       // renamed from "row" to avoid confusion with report rows
                                            qrRow["qrcode"] = qrcode;
                                            dataQR.Rows.Add(qrRow);

                                            LocalReport localReport = new LocalReport(rdlcpath);
                                            localReport.AddDataSource("ITIStudentAdmitCard", data.Tables[0]);
                                            localReport.AddDataSource("ITIStudentAdmitCard_Subject", data.Tables[1]);
                                            localReport.AddDataSource("test", dataQR);
                                            var reportResult = localReport.Execute(RenderType.Pdf);

                                            //check file exists
                                            if (!System.IO.Directory.Exists(folderPath))
                                            {
                                                Directory.CreateDirectory(folderPath);
                                            }
                                            //save
                                            //save
                                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                            //end report
                                        }
                                        else
                                        {
                                            result.State = EnumStatus.Warning;
                                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                                        }
                                    }
                                }
                                else
                                {
                                    result.State = EnumStatus.Warning;
                                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                                    //result.ErrorMessage = Convert.ToString(Model.StudentExamIDs);
                                }
                            }
                            var Issuccess = await _unitOfWork.GenerateAdmitCardRepository.UpdateAdmitCard(ListData);
                            if (Issuccess > 0)
                            {

                                #region "Save Multiple PDF PAGES"
                                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                string guid = Guid.NewGuid().ToString().ToUpper();
                                string outputFile = $"MergePDF_{Model.InstituteID}_{Model.SemesterID}_{Model.EndTermID}.pdf";
                                string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                                List<string?> strSoureFiles = ListData.Select(s => s.AdmitCardPath).ToList();
                                if (await MergePdfFilesAsync(strSoureFiles, outputPath))
                                {
                                    DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                                    ModInsert.FileName = outputFile;
                                    ModInsert.PDFType = (int)EnumPdfType.AdmitCard;
                                    ModInsert.Status = 11;
                                    ModInsert.SemesterID = Model.SemesterID;
                                    ModInsert.InstituteID = Model.InstituteID;
                                    ModInsert.DepartmentID = Model.DepartmentID;
                                    ModInsert.EndTermID = Model.EndTermID;
                                    ModInsert.Eng_NonEng = Model.Eng_NonEng;
                                    ModInsert.CreatedBy = Model.UserID;
                                    ModInsert.TotalStudent = Model.TotalRecord;

                                    var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);
                                    await _unitOfWork.SaveChangesAsync();

                                    result.Data = outputFile;
                                    result.State = EnumStatus.Success;
                                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                                }
                                else
                                {
                                    result.State = EnumStatus.Error;
                                    result.ErrorMessage = "Something went wrong";
                                }
                                #endregion
                            }
                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }







        [HttpPost("DownloadITIStudentRollNumberBulk_CenterWise")]
        public async Task<ApiResult<string>> DownloadITIStudentRollNumberBulk_CenterWise([FromBody] DownloadnRollNoModel Request)
        {
            ActionName = "DownloadITIStudentRollNumber(string EnrollmentNo)";
            List<string?> ListRoleListPath = new List<string?>();
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var Model = await _unitOfWork.GenerateRollRepository.GetGenerateRollData_Centerwise(Request);
                    //var Model = _unitOfWork.GenerateRollRepository.GetITIGenerateRollDataForPrint_Collegewise(Request);
                    List<string?> ListRoleListPath = new List<string?>();
                    foreach (var RollListDetails in Model.GroupBy(f => new { f.InstituteID, f.SemesterID }))
                    {
                        DownloadnRollNoModel ModInsert = RollListDetails.FirstOrDefault() ?? new DownloadnRollNoModel();
                        ModInsert.TotalStudent = RollListDetails.Sum(f => f.Totalstudent);


                        foreach (var StudentExamID in RollListDetails)
                        {


                            DataTable dtStudentExamDetails = new DataTable();
                            dtStudentExamDetails.Columns.Add("StudentType");
                            dtStudentExamDetails.Columns.Add("InstituteName");
                            dtStudentExamDetails.Columns.Add("ProgrammeName");
                            dtStudentExamDetails.Columns.Add("SessionName");
                            dtStudentExamDetails.Columns.Add("CenterName");
                            dtStudentExamDetails.Columns.Add("BranchCode");



                            dtStudentExamDetails.Rows.Add(StudentExamID.StudentType, StudentExamID.InstituteNameEnglish, StudentExamID.EndTermName, StudentExamID.FinancialYearName, StudentExamID.CenterName, StudentExamID.BranchCode);
                            GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                            var data = await _unitOfWork.ReportRepository.GetITIStudentRollNoList_centerwise(StudentExamID);
                            if (data != null)
                            {

                                //report
                                var fileName = $"ITIStudentRollList_{Guid.NewGuid()}.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIStudentRollnumberList.rdlc";

                                //
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("StudentExamDetails", dtStudentExamDetails);
                                localReport.AddDataSource("StudentRollNumberList", data);
                                var reportResult = localReport.Execute(RenderType.Pdf);
                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                //end report

                                ListRoleListPath.Add(filepath);
                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




                            }
                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }
                        }

                    }

                    //#region "Save Multiple PDF PAGES"    // old Code 
                    //string outputFile = $"MergePDFRollList_{Model.FirstOrDefault()?.InstituteID}.pdf";
                    //string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                    //if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                    //{
                    //    //delete files
                    //    await DeleteFiles(ListRoleListPath);
                    //    result.Data = outputFile;
                    //    result.State = EnumStatus.Success;
                    //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Something went wrong";
                    //}
                    //#endregion



                    #region "Save Multiple PDF PAGES"
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string guid = Guid.NewGuid().ToString().ToUpper();
                    string outputFile = $"{guid}_{timestamp}.pdf";
                    string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                    if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                    {
                        try
                        {
                            //delete files
                            // await DeleteFiles(ListRoleListPath);
                        }
                        catch (Exception exd)
                        {
                        }
                        result.Data = outputFile;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                        //var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                    }
                    #endregion




                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }





        #endregion

        #region Download Enrollment List
        [HttpPost("GetEnrollmentList")]
        public async Task<ApiResult<string>> GetEnrollmentList(ReportBaseModel model)
        {
            ActionName = "GetEnrollmentList(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetEnrollmentList(model);
                    if (data.Rows?.Count > 1)
                    {
                        //report
                        var fileName = $"EnrollmentList{model.InstituteID}.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/EnrollmentList.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("EnrollmentList", data);
                        localReport.AddDataSource("EnrollmentListProgram", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region "ITI Student Roll Number Download"

        [HttpPost("DownloadITIStudentRollNumber")]
        public async Task<ApiResult<string>> DownloadITIStudentRollNumber([FromBody] List<DownloadnRollNoModel> Model)
        {
            ActionName = "DownloadITIStudentRollNumber(string EnrollmentNo)";
            List<string?> ListRoleListPath = new List<string?>();
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    foreach (var StudentExamID in Model)
                    {
                        DataTable dtStudentExamDetails = new DataTable();
                        dtStudentExamDetails.Columns.Add("StudentType");
                        dtStudentExamDetails.Columns.Add("InstituteName");
                        dtStudentExamDetails.Columns.Add("ProgrammeName");
                        dtStudentExamDetails.Columns.Add("SessionName");
                        dtStudentExamDetails.Columns.Add("CenterName");
                        dtStudentExamDetails.Columns.Add("BranchCode");

                        dtStudentExamDetails.Rows.Add(StudentExamID.StudentType, StudentExamID.InstituteNameEnglish, StudentExamID.EndTermName, StudentExamID.FinancialYearName, StudentExamID.CenterName, StudentExamID.BranchCode);
                        GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                        var data = await _unitOfWork.ReportRepository.GetITIStudentRollNoList(StudentExamID);
                        if (data != null)
                        {

                            //report
                            var fileName = $"ITIStudentRollList_{Guid.NewGuid()}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIStudentRollnumberList.rdlc";

                            //
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("StudentExamDetails", dtStudentExamDetails);
                            localReport.AddDataSource("StudentRollNumberList", data);
                            var reportResult = localReport.Execute(RenderType.Pdf);
                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                            //end report

                            ListRoleListPath.Add(filepath);
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }
                    }
                    #region "Save Multiple PDF PAGES"
                    string outputFile = $"MergePDFRollList_{Model.FirstOrDefault()?.InstituteID}.pdf";
                    string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                    if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                    {
                        //delete files
                        await DeleteFiles(ListRoleListPath);
                        result.Data = outputFile;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                    }
                    #endregion
                }


                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpPost("DownloadITIStudentRollNumber_CollageWise")]
        public async Task<ApiResult<string>> DownloadITIStudentRollNumber_CollageWise([FromBody] DownloadnRollNoModel Request)
        {
            ActionName = "DownloadITIStudentRollNumber(string EnrollmentNo)";
            List<string?> ListRoleListPath = new List<string?>();
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var Model = await _unitOfWork.GenerateRollRepository.GetGenerateRollData_Collegewise(Request);
                    //var Model = _unitOfWork.GenerateRollRepository.GetITIGenerateRollDataForPrint_Collegewise(Request);

                    foreach (var RollListDetails in Model.GroupBy(f => new { f.InstituteID, f.SemesterID }))
                    {
                        DownloadnRollNoModel ModInsert = RollListDetails.FirstOrDefault() ?? new DownloadnRollNoModel();
                        ModInsert.TotalStudent = RollListDetails.Sum(f => f.Totalstudent);
                        List<string?> ListRoleListPath = new List<string?>();

                        foreach (var StudentExamID in RollListDetails)
                        {


                            DataTable dtStudentExamDetails = new DataTable();
                            dtStudentExamDetails.Columns.Add("StudentType");
                            dtStudentExamDetails.Columns.Add("InstituteName");
                            dtStudentExamDetails.Columns.Add("ProgrammeName");
                            dtStudentExamDetails.Columns.Add("SessionName");
                            dtStudentExamDetails.Columns.Add("CenterName");
                            dtStudentExamDetails.Columns.Add("BranchCode");

                            dtStudentExamDetails.Rows.Add(StudentExamID.StudentType, StudentExamID.InstituteNameEnglish, StudentExamID.EndTermName, StudentExamID.FinancialYearName, StudentExamID.CenterName, StudentExamID.BranchCode);
                            GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                            var data = await _unitOfWork.ReportRepository.GetITIStudentRollNoList_collegewise(StudentExamID);
                            if (data != null)
                            {

                                //report
                                var fileName = $"ITIStudentRollList_{Guid.NewGuid()}.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIStudentRollnumberList.rdlc";

                                //
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("StudentExamDetails", dtStudentExamDetails);
                                localReport.AddDataSource("StudentRollNumberList", data);
                                var reportResult = localReport.Execute(RenderType.Pdf);
                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                //end report

                                ListRoleListPath.Add(filepath);
                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




                            }
                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }
                        }

                        //#region "Save Multiple PDF PAGES"    // old Code 
                        //string outputFile = $"MergePDFRollList_{Model.FirstOrDefault()?.InstituteID}.pdf";
                        //string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                        //if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                        //{
                        //    //delete files
                        //    await DeleteFiles(ListRoleListPath);
                        //    result.Data = outputFile;
                        //    result.State = EnumStatus.Success;
                        //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.State = EnumStatus.Error;
                        //    result.ErrorMessage = "Something went wrong";
                        //}
                        //#endregion



                        #region "Save Multiple PDF PAGES"
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string guid = Guid.NewGuid().ToString().ToUpper();
                        string outputFile = $"{guid}_{timestamp}.pdf";
                        string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                        if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                        {
                            try
                            {
                                //delete files
                                // await DeleteFiles(ListRoleListPath);
                            }
                            catch (Exception exd)
                            {
                            }
                            result.Data = outputFile;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            ModInsert.FileName = outputFile;
                            ModInsert.PDFType = (int)EnumPdfType.RollList;
                            ModInsert.Status = 11;
                            ModInsert.Eng_NonEng = 2;
                            var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "Something went wrong";
                        }
                        #endregion

                    }


                }


                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpPost("DownloadITIStudentRollNumberBulk_CenterWise_new")]
        public async Task<ApiResult<string>> DownloadITIStudentRollNumberBulk_CenterWise_new([FromBody] DownloadnRollNoModel Request)
        {
            ActionName = "DownloadITIStudentRollNumberBulk_CenterWise_new(string EnrollmentNo)";
            List<string?> ListRoleListPath = new List<string?>();
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var Model = await _unitOfWork.GenerateRollRepository.GetGenerateRollData_Collegewise(Request);
                    //var Model = _unitOfWork.GenerateRollRepository.GetITIGenerateRollDataForPrint_Collegewise(Request);

                    foreach (var RollListDetails in Model.GroupBy(f => new { f.InstituteID, f.SemesterID }))
                    {
                        DownloadnRollNoModel ModInsert = RollListDetails.FirstOrDefault() ?? new DownloadnRollNoModel();
                        ModInsert.TotalStudent = RollListDetails.Sum(f => f.Totalstudent);
                        List<string?> ListRoleListPath = new List<string?>();

                        foreach (var StudentExamID in RollListDetails)
                        {


                            DataTable dtStudentExamDetails = new DataTable();
                            dtStudentExamDetails.Columns.Add("StudentType");
                            dtStudentExamDetails.Columns.Add("InstituteName");
                            dtStudentExamDetails.Columns.Add("ProgrammeName");
                            dtStudentExamDetails.Columns.Add("SessionName");
                            dtStudentExamDetails.Columns.Add("CenterName");
                            dtStudentExamDetails.Columns.Add("BranchCode");

                            dtStudentExamDetails.Rows.Add(StudentExamID.StudentType, StudentExamID.InstituteNameEnglish, StudentExamID.EndTermName, StudentExamID.FinancialYearName, StudentExamID.CenterName, StudentExamID.BranchCode);
                            GenerateAdmitCardModel objStudent = new GenerateAdmitCardModel();
                            var data = await _unitOfWork.ReportRepository.GetITIStudentRollNoList_collegewise(StudentExamID);
                            if (data != null)
                            {

                                //report
                                var fileName = $"ITIStudentRollList_{Guid.NewGuid()}.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIStudentRollnumberList.rdlc";

                                //
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("StudentExamDetails", dtStudentExamDetails);
                                localReport.AddDataSource("StudentRollNumberList", data);
                                var reportResult = localReport.Execute(RenderType.Pdf);
                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                //end report

                                ListRoleListPath.Add(filepath);
                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




                            }
                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }
                        }

                        //#region "Save Multiple PDF PAGES"    // old Code 
                        //string outputFile = $"MergePDFRollList_{Model.FirstOrDefault()?.InstituteID}.pdf";
                        //string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                        //if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                        //{
                        //    //delete files
                        //    await DeleteFiles(ListRoleListPath);
                        //    result.Data = outputFile;
                        //    result.State = EnumStatus.Success;
                        //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.State = EnumStatus.Error;
                        //    result.ErrorMessage = "Something went wrong";
                        //}
                        //#endregion



                        #region "Save Multiple PDF PAGES"
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string guid = Guid.NewGuid().ToString().ToUpper();
                        string outputFile = $"{guid}_{timestamp}.pdf";
                        string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                        if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                        {
                            try
                            {
                                //delete files
                                // await DeleteFiles(ListRoleListPath);
                            }
                            catch (Exception exd)
                            {
                            }
                            result.Data = outputFile;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            ModInsert.FileName = outputFile;
                            ModInsert.PDFType = (int)EnumPdfType.RollList;
                            ModInsert.Status = 11;
                            ModInsert.Eng_NonEng = 2;
                            var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "Something went wrong";
                        }
                        #endregion

                    }


                }


                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }



        #endregion




        #region GetStudent Customizet Reports Columns
        [HttpPost("GetStudentCustomizetReportsColumns")]
        public async Task<ApiResult<DataTable>> GetStudentCustomizetReportsColumns()
        {
            ActionName = "GetStudentCustomizetReportsColumns()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetStudentCustomizetReportsColumns();
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region CustomizeReport List
        [HttpPost("GetStudentCustomizeList")]
        public async Task<ApiResult<DataTable>> GetStudentCustomizeList(ReportCustomizeBaseModel model)
        {
            ActionName = "GetStudentCustomizeList()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetStudentCustomizetReports(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region CustomizeReport List Use DDL

        [HttpGet("GetGender")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetGender()
        {
            ActionName = "GetGender()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetGender();
                    if (data != null)
                    {



                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }

        [HttpGet("GetBlock")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetBlock()
        {
            ActionName = "GetBlock()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetBlock();
                    if (data != null)
                    {



                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }
        [HttpGet("GetCourseType/{DepartmentID?}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCourseType(int DepartmentID = 0)
        {
            ActionName = "GetCourseType()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetCourseType(DepartmentID);
                    if (data != null)
                    {



                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }
        [HttpGet("GetInstitute")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetInstitute()
        {
            ActionName = "GetInstitute()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetInstitute();
                    if (data != null)
                    {



                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }
        [HttpGet("GetEndTerm")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetEndTerm()
        {
            ActionName = "GetEndTerm()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetEndTerm();
                    if (data != null)
                    {



                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }

        #endregion

        #region EnrollmentReceipt

        [HttpGet("GetAllotmentReceipt/{AllotmentId}")]
        public async Task<ApiResult<string>> GetAllotmentReceipt(string AllotmentId)
        {
            ActionName = "GetAllotmentReceipt(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetAllotmentReceipt(AllotmentId);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.AllotmentReciept}";
                        //report
                        var fileName = $"AllotmentReceipt_{AllotmentId}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.AllotmentReciept}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIIMCSeatAllotmentReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ITIIMCSeatAllotmentRcpt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion EnrollmentReceipt

        #region Iti Colleges Wise Reports
        [HttpPost("GetItiStudentEnrollmentReports")]
        public async Task<ApiResult<DataTable>> GetItiStudentEnrollmentReports([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetItiStudentEnrollmentReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetItiStudentEnrollmentReports(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetIitStudentExamReports")]
        public async Task<ApiResult<DataTable>> GetIitStudentExamReports([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetIitStudentExamReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetIitStudentExamReports(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region Student Marksheet
        [HttpPost("GetStudentMarksheet")]
        public async Task<ApiResult<string>> GetStudentMarksheet([FromBody] MarksheetDownloadSearchModel student)
        {
            ActionName = "GetStudentMarksheet([FromBody] MarksheetDownloadSearchModel student)";
            var Session = student.SessionName;
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/BTER/Marksheet/{Session}";
            //var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    List<StudentDownloadInfo> DownloadList = new List<StudentDownloadInfo>();

                    var data = await _unitOfWork.ReportRepository.GetStudentMarksheet(student);
                    if (data?.Tables?.Count == 3)
                    {
                        //report
                        string timestamp_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        var fileName = $"StudentMarksheet_{student.RollNo}_{timestamp_str}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/BTER/Marksheet/{Session}/{fileName}";
                        string strmName = data.Tables[0].Rows[0]["StreamName"].ToString();
                        string rdlcpath = "";
                        //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        if (strmName.Length > 37)
                        {
                            rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentMarksheetOther.rdlc";
                        }
                        else
                        {
                            rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentMarksheet.rdlc";
                        }

                        student.MarksheetPath = filepath;
                        student.Marksheet = fileName;

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentDetailsForMarksheet", data.Tables[0]);
                        localReport.AddDataSource("StudentMarksheetSubjectDetails", data.Tables[1]);
                        localReport.AddDataSource("ResultDetails", data.Tables[2]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        StudentDownloadInfo downloadInfo = new StudentDownloadInfo
                        {
                            RollNo = student.RollNo,
                            MarksheetID = student.MarksheetID,
                            MarksheetFile = fileName,
                            MarksheetFilePath = filepath
                        };
                        DownloadList.Add(downloadInfo);

                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //result.Data = fileName;
                        //result.State = EnumStatus.Success;
                        //result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        //end report

                        if (DownloadList.Count > 0)
                        {
                            var updateData = new ApiResult<int>();
                            updateData.Data = await _unitOfWork.MarksheetDownloadRepository.UpdateMarksheetFile(DownloadList);
                            await _unitOfWork.SaveChangesAsync();

                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Registration Form For Hostel
        [HttpPost("GetStudentHostelallotment")]
        public async Task<ApiResult<string>> GetStudentHostelallotment([FromBody] MarksheetDownloadSearchModel student)
        {
            ActionName = "GetStudentMarksheet(string EnrollmentNo)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    //ListData.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    //});

                    //foreach (var student in ListData)
                    //{
                    var data = await _unitOfWork.ReportRepository.GetStudentHostelallotment(student);
                    if (data?.Tables?.Count > 1)
                    {
                        //report
                        var fileName = $"StudentHostelRegistrationForm{student.StudentID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RegistrationFormHostelAllotment.rdlc";

                        student.MarksheetPath = filepath;
                        student.Marksheet = fileName;
                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images

                        //string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                        //data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        //string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                        //data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                        //string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
                        //data.Tables[0].Rows[0]["RegistrarSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(registrar_signFilepath));
                        ////rdlc

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("HostelAllotment", data.Tables[0]);
                        localReport.AddDataSource("HostelAllotment1", data.Tables[1]);
                        localReport.AddDataSource("HostelAllotment2", data.Tables[2]);
                        localReport.AddDataSource("HostelAllotment3", data.Tables[3]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        //end report
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                    //}

                    //var Issuccess = await _unitOfWork.GenerateAdmitCardRepository.UpdateAdmitCard(student);
                    //if (Issuccess > 0)
                    //{
                    //    result.Data = Issuccess.ToString();
                    //    result.State = EnumStatus.Success;
                    //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    //}

                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Download Appeared Passed
        [HttpPost("DownloadAppearedPassed")]
        public async Task<ApiResult<string>> DownloadAppearedPassed(DownloadAppearedPassed model)
        {
            ActionName = "DownloadAppearedPassed(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadAppearedPassed(model);
                    if (data.Rows?.Count > 1)
                    {
                        //report
                        var fileName = $"AppearedPassed.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/AppearedPassedStatistics.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("AppearedPassedStatistics", data);
                        localReport.AddDataSource("AppearedPassedDetails", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Download Appeared Passed Institute Wise
        [HttpPost("DownloadAppearedPassedInstitutewise")]
        public async Task<ApiResult<string>> DownloadAppearedPassedInstitutewise(DownloadAppearedPassed model)
        {
            ActionName = "DownloadAppearedPassedInstitutewise(DownloadAppearedPassed model)";
            var result = new ApiResult<string>();
            try
            {
                var data = await _unitOfWork.ReportRepository.DownloadAppearedPassedInstitutewise(model);
                if (data.Tables.Count > 1 && data.Tables[0].Rows?.Count > 1)
                {
                    //report
                    var fileName = $"AppearedPassedInstituteWise.pdf";
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/AppearedPassedStatisticsInstituteWise.rdlc";

                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("AppearedPassedStatistics", data.Tables[0]);
                    localReport.AddDataSource("AppearedPassedDetails", data.Tables[0]);
                    localReport.AddDataSource("AppearedPassedDetailsTotal", data.Tables[1]);// total
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                }


            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }
        #endregion

        #region Download Branch Wise Statistics
        [HttpPost("DownloadBranchWiseStatistics")]
        public async Task<ApiResult<string>> DownloadBranchWiseStatistics(DownloadAppearedPassed model)
        {
            ActionName = "DownloadAppearedPassed(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadBranchWiseStatistics(model);
                    if (data.Rows?.Count > 1)
                    {
                        //report
                        var fileName = $"AppearedPassed.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/BranchWiseStatistics.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("AppearedPassedStatistics", data);
                        localReport.AddDataSource("AppearedPassedDetails", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Download Institute Branch Wise Statistics
        [HttpPost("DownloadInstituteBranchWiseStatisticsReport")]
        public async Task<ApiResult<string>> DownloadInstituteBranchWiseStatisticsReport(DownloadAppearedPassed model)
        {
            ActionName = "DownloadAppearedPassed(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadInstituteBranchWiseStatisticsReport(model);
                    if (data.Rows?.Count > 1)
                    {
                        //report
                        var fileName = $"InstituteBranchWiseStatisticsReport.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/InstituteBranchWiseStatisticsReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("AppearedPassedStatistics", data);
                        localReport.AddDataSource("AppearedPassedDetails", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Download Blank Report
        [HttpPost("GetBlankReport")]
        public async Task<ApiResult<string>> GetBlankReport(BlankReportModel model)
        {
            ActionName = "GetBlankReport(string EnrollmentNo)";
            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => _unitOfWork.ReportRepository.GetBlankReport(model));
                if (data.Rows?.Count >= 1)
                {
                    //report
                    var fileName = $"BlankReport{model.InstituteID}.pdf";
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Blank_Report.rdlc";

                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("Blank_Report", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                }


            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }
        #endregion

        #region Paper-Count-Customize-Report-Columns-And-List
        [HttpPost("PaperCountCustomizeReportColumnsAndList")]
        public async Task<ApiResult<DataTable>> PaperCountCustomizeReportColumnsAndList(ReportCustomizeBaseModel model)
        {
            ActionName = "PaperCountCustomizeReportColumnsAndList(ReportCustomizeBaseModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.PaperCountCustomizeReportColumnsAndList(model));
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region PaperCountCustomizeReportList
        [HttpPost("PaperCountCustomizeReportList")]
        public async Task<ApiResult<DataTable>> PaperCountCustomizeReportList(ReportCustomizeBaseModel model)
        {
            ActionName = "GetStudentCustomizeList()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.PaperCountCustomizeReportList(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region GetGroupCenterMappingReports
        [HttpPost("GetGroupCenterMappingReports")]
        public async Task<ApiResult<DataTable>> GetGroupCenterMappingReports([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetStudentEnrollmentReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetGroupCenterMappingReports(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region GetCenterDailyReports
        [HttpPost("GetCenterDailyReports")]
        public async Task<ApiResult<DataTable>> GetCenterDailyReports([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetStudentEnrollmentReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCenterDailyReports(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetCenterDailyReport")]
        public async Task<ApiResult<DataTable>> GetCenterDailyReport([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetStudentEnrollmentReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCenterDailyReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region GetDownloadCenterDailyReports
        [HttpPost("GetDownloadCenterDailyReports")]
        public async Task<ApiResult<string>> GetDownloadCenterDailyReports([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetStudentEnrollmentReports()";
            var result = new ApiResult<string>();
            try
            {
                // Pass the entire model to the repository
                var data = await _unitOfWork.ReportRepository.GetDownloadCenterDailyReports(body);
                if (data.Rows?.Count >= 1)
                {
                    //report
                    var fileName = $"Center_DailyReport{body.CenterCode}.pdf";
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Center_Daily_ReportList.rdlc";

                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("CenterDailyReport", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region GetExaminerReportAndMarksTracking
        [HttpPost("GetExaminerReportAndMarksTracking")]
        public async Task<ApiResult<DataTable>> GetExaminerReportAndMarksTracking([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetExaminerReportAndMarksTracking([FromBody] GroupCenterMappingModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetExaminerReportAndMarksTracking(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region 
        [HttpPost("GetExaminerReportAndMarksTrackingStudent")]
        public async Task<ApiResult<DataTable>> GetExaminerReportAndMarksTrackingStudent([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetExaminerReportAndMarksTrackingStudent([FromBody] GroupCenterMappingModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetExaminerReportAndMarksTrackingStudent(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetExaminerReportAndPresentTrackingStudent")]
        public async Task<ApiResult<DataTable>> GetExaminerReportAndPresentTrackingStudent([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetExaminerReportAndPresentTrackingStudent()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetExaminerReportAndPresentTrackingStudent(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("GetExaminerReportAndMarksDownload")]
        public async Task<ApiResult<string>> GetExaminerReportAndMarksDownload([FromBody] GroupCenterMappingModel body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetExaminerReportAndMarksDownload(body);

                    if (data != null)
                    {
                        //report
                        var fileName = $"ExaminerReportAndMarks.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ExaminerReportAndMarks.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ExaminerReportAndMarks", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });

        }


        #endregion

        #region GetStaticsReportProvideByExaminer
        [HttpPost("GetStaticsReportProvideByExaminer")]
        public async Task<ApiResult<DataTable>> GetStaticsReportProvideByExaminer([FromBody] GroupCenterMappingModel body)
        {

            ActionName = "GetStaticsReportProvideByExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetStaticsReportProvideByExaminer(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region GetExaminerWithGroupCodeList
        [HttpPost("GetExaminerWithGroupCodeList")]
        public async Task<ApiResult<DataTable>> GetExaminerWithGroupCodeList([FromBody] MiscellaneousModel body)
        {

            ActionName = "GetExaminerWithGroupCodeList()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetExaminerWithGroupCodeList(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("UnlockExaminerWithGroupCode")]
        public async Task<ApiResult<bool>> UnlockExaminerWithGroupCode([FromBody] MiscellaneousModel request)
        {
            ActionName = " UnlockExaminerWithGroupCode([FromBody] MiscellaneousModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ReportRepository.UnlockExaminerWithGroupCode(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                        //if (request.StaffID == 0)
                        //{
                        //    result.Message = Constants.MSG_SAVE_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.Message = Constants.MSG_UPDATE_SUCCESS;
                        //}
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        //if (request.StaffID == 0)
                        //{
                        //    result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        //}
                        //else
                        //{
                        //    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        //}
                    }
                }
                catch (System.Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }

        #endregion

        #region GetOnlineReportProvideByExaminer
        [HttpPost("GetOnlineReportProvideByExaminer")]
        public async Task<ApiResult<DataTable>> GetOnlineReportProvideByExaminer([FromBody] OnlineMarkingSearchModel body)
        {

            ActionName = "GetStaticsReportProvideByExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetOnlineReportProvideByExaminer(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region GetCenterWiseSubjectCountReportColumnsAndList
        [HttpPost("GetCenterWiseSubjectCountReportColumnsAndList")]
        public async Task<ApiResult<DataTable>> GetCenterWiseSubjectCountReportColumnsAndList(ReportCustomizeBaseModel model)
        {
            ActionName = "GetCenterWiseSubjectCountReportColumnsAndList()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCenterWiseSubjectCountReportColumnsAndList(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region ITI Examination form
        [HttpPost("GetITIExaminationForm")]
        public async Task<ApiResult<string>> GetITIExaminationForm(ReportBaseModel model)
        {
            ActionName = "GetExaminationForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIExaminationForm(model);
                    if (data != null)
                    {
                        //report
                        var fileName = $"ITIStudentExaminationForm_{model.StudentID}_{model.EndTermID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";

                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIStudentExaminationForm.rdlc";


                        string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                        data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                        data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));
                        //

                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("StudentExaminationForm", data.Tables[0]);
                        localReport.AddDataSource("StudentExaminationSubject", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Optional Format Report
        [HttpPost("GetOptionalFormatReportData")]
        public async Task<ApiResult<DataTable>> GetOptionalFormatReportData(OptionalFromatReportSearchModel model)
        {
            ActionName = "GetOptionalFormatReportData(OptionalFromatReportSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetOptionalFormatReportData(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region Non-Elective-Form-Filling-Report
        [HttpPost("GetNonElectiveFormFillingReportData")]
        public async Task<ApiResult<DataTable>> GetNonElectiveFormFillingReportData(NonElectiveFormFillingReportSearchModel model)
        {
            ActionName = "GetNonElectiveFormFillingReportData(NonElectiveFormFillingReportSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetNonElectiveFormFillingReportData(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region BTER Flying Squad Duty Order
        [HttpPost("GetFlyingSquadDutyOrder")]
        public async Task<ApiResult<string>> GetFlyingSquadDutyOrder(GetFlyingSquadDutyOrder model)
        {
            ActionName = "GetFlyingSquadDutyOrder()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetFlyingSquadDutyOrder(model);

                    if (data != null)
                    {
                        //report
                        var fileName = $"FlyingSquadDutyOrder.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/FlyingSquadDutyOrderReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("FlyingSquadReports", data.Tables[0]);
                        localReport.AddDataSource("FlyingSquadMembers", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        if (model.Status < 4)
                        {
                            // Build email body
                            string email = WebUtility.HtmlEncode("umesh.rajpoot@devitpl.com");

                            string emailBody = $@"
                     <!DOCTYPE html>
                     <html>
                     <head>
                         <meta charset='UTF-8'>
                         <style>
                             body {{
                                 font-family: Arial, sans-serif;
                                 background-color: #f4f4f4;
                                 margin: 0;
                                 padding: 0;
                             }}
                             .container {{
                                 background-color: #ffffff;
                                 max-width: 600px;
                                 margin: 40px auto;
                                 padding: 20px;
                                 border-radius: 8px;
                                 box-shadow: 0 0 10px rgba(0,0,0,0.1);
                             }}
                             .header {{
                                 background-color: #007bff;
                                 color: white;
                                 padding: 10px 20px;
                                 border-radius: 8px 8px 0 0;
                                 font-size: 20px;
                             }}
                             .content {{
                                 padding: 20px;
                                 color: #333;
                             }}
                             .footer {{
                                 font-size: 12px;
                                 color: #999;
                                 text-align: center;
                                 padding: 10px 20px;
                                 border-top: 1px solid #eee;
                             }}
                         </style>
                     </head>
                     <body>
                         <div class='container'>
                             <div class='header'>Kaushal Darpan Flying Squad Order</div>
                             <div class='content'>
                                 <p>Hello, <strong>Please View Flying Squad Order Attechment</strong></p>
                             </div>
                             <div class='footer'>
                                 &copy; 2025 Kaushal Darpan. All rights reserved.
                             </div>
                         </div>
                     </body>
                     </html>";

                            // Send email
                            //await _emailService.SendEmail(emailBody, email, "Kaushal Darpan Flying Squad Order", filepath);
                        }


                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        [HttpPost("GetAllFlyingSquadDutyOrder")]
        public async Task<ApiResult<List<string>>> GetFlyingSquadDutyOrder(List<GetFlyingSquadDutyOrder> listmodel)
        {
            ActionName = "GetFlyingSquadDutyOrder()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<string>>();
                result.Data = new List<string>();

                try
                {
                    int counter = 1;
                    foreach (var model in listmodel)
                    {
                        var data = await _unitOfWork.ReportRepository.GetFlyingSquadDutyOrder(model);

                        if (data != null)
                        {
                            // Unique file name
                            var fileName = $"FlyingSquadDutyOrder_{counter}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/FlyingSquadDutyOrderReport.rdlc";

                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("FlyingSquadReports", data.Tables[0]);
                            localReport.AddDataSource("FlyingSquadMembers", data.Tables[1]);
                            var reportResult = localReport.Execute(RenderType.Pdf);

                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                            if (model.Status < 4)
                            {
                                // Build email body
                                string email = WebUtility.HtmlEncode("umesh.rajpoot@devitpl.com");

                                string emailBody = $@"
                     <!DOCTYPE html>
                     <html>
                     <head>
                         <meta charset='UTF-8'>
                         <style>
                             body {{
                                 font-family: Arial, sans-serif;
                                 background-color: #f4f4f4;
                                 margin: 0;
                                 padding: 0;
                             }}
                             .container {{
                                 background-color: #ffffff;
                                 max-width: 600px;
                                 margin: 40px auto;
                                 padding: 20px;
                                 border-radius: 8px;
                                 box-shadow: 0 0 10px rgba(0,0,0,0.1);
                             }}
                             .header {{
                                 background-color: #007bff;
                                 color: white;
                                 padding: 10px 20px;
                                 border-radius: 8px 8px 0 0;
                                 font-size: 20px;
                             }}
                             .content {{
                                 padding: 20px;
                                 color: #333;
                             }}
                             .footer {{
                                 font-size: 12px;
                                 color: #999;
                                 text-align: center;
                                 padding: 10px 20px;
                                 border-top: 1px solid #eee;
                             }}
                         </style>
                     </head>
                     <body>
                         <div class='container'>
                             <div class='header'>Kaushal Darpan Flying Squad Order</div>
                             <div class='content'>
                                 <p>Hello, <strong>Please View Flying Squad Order Attechment</strong></p>
                             </div>
                             <div class='footer'>
                                 &copy; 2025 Kaushal Darpan. All rights reserved.
                             </div>
                         </div>
                     </body>
                     </html>";

                                // Send email
                                //await _emailService.SendEmail(emailBody, email, "Kaushal Darpan Flying Squad Order", filepath);
                            }

                            result.Data.Add(fileName);
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            counter++;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }


        [HttpPost("GetFlyingSquadOrderReports")]
        public async Task<ApiResult<string>> GetFlyingSquadOrderReports(GetFlyingSquadDutyOrder model)
        {
            ActionName = "GetFlyingSquadOrderReports()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data1 = await _unitOfWork.ReportRepository.GetFlyingSquadDutyOrder(model);
                    var data = await _unitOfWork.ReportRepository.GetFlyingSquadReports(model);
                    if (data != null)
                    {
                        //report
                        var fileName = $"FlyingSquadReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/FlyingSquadReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("FlyingSquadCenter", data.Tables[0]);
                        localReport.AddDataSource("FlyingSquadQuestion", data.Tables[1]);
                        localReport.AddDataSource("FlyingSquadReports", data1.Tables[0]);
                        localReport.AddDataSource("FlyingSquadIncharge", data1.Tables[2]);
                        localReport.AddDataSource("FlyingSquadMembers", data1.Tables[3]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpPost("GetFlyingSquadReports")]
        public async Task<ApiResult<DataTable>> GetFlyingSquadReports([FromBody] GetFlyingSquadModal body)
        {
            ActionName = "GetStudentEnrollmentReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetFlyingSquadReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetFlyingSquadTeamReports")]
        public async Task<ApiResult<DataTable>> GetFlyingSquadTeamReports([FromBody] GetFlyingSquadModal body)
        {
            ActionName = "GetStudentEnrollmentReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetFlyingSquadTeamReports(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region ITI Flying Squad Duty Order
        [HttpPost("GetITIFlyingSquadDutyOrder")]
        public async Task<ApiResult<string>> GetITIFlyingSquadDutyOrder(GetFlyingSquadDutyOrder model)
        {
            ActionName = "GetITIFlyingSquadDutyOrder()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIFlyingSquadDutyOrder(model);

                    if (data != null)
                    {
                        //report
                        var fileName = $"FlyingSquadDutyOrder.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/FlyingSquadDutyOrderReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("FlyingSquadReports", data.Tables[0]);
                        localReport.AddDataSource("FlyingSquadMembers", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        [HttpPost("GetAllITIFlyingSquadDutyOrder")]
        public async Task<ApiResult<List<string>>> GetITIFlyingSquadDutyOrder(List<GetFlyingSquadDutyOrder> listmodel)
        {
            ActionName = "GetITIFlyingSquadDutyOrder()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<string>>();
                result.Data = new List<string>();

                try
                {
                    int counter = 1;
                    foreach (var model in listmodel)
                    {
                        var data = await _unitOfWork.ReportRepository.GetITIFlyingSquadDutyOrder(model);

                        if (data != null)
                        {
                            // Unique file name
                            var fileName = $"FlyingSquadDutyOrder_{counter}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/FlyingSquadDutyOrderReport.rdlc";

                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("FlyingSquadReports", data.Tables[0]);
                            localReport.AddDataSource("FlyingSquadMembers", data.Tables[1]);
                            var reportResult = localReport.Execute(RenderType.Pdf);

                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                            result.Data.Add(fileName);
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            counter++;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }


        [HttpPost("GetITIFlyingSquadOrderReports")]
        public async Task<ApiResult<string>> GetITIFlyingSquadOrderReports(GetFlyingSquadDutyOrder model)
        {
            ActionName = "GetITIFlyingSquadOrderReports()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data1 = await _unitOfWork.ReportRepository.GetITIFlyingSquadDutyOrder(model);
                    var data = await _unitOfWork.ReportRepository.GetITIFlyingSquadReports(model);
                    if (data != null)
                    {
                        //report
                        var fileName = $"FlyingSquadReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/FlyingSquadReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("FlyingSquadCenter", data.Tables[0]);
                        localReport.AddDataSource("FlyingSquadQuestion", data.Tables[1]);
                        localReport.AddDataSource("FlyingSquadReports", data1.Tables[0]);
                        localReport.AddDataSource("FlyingSquadIncharge", data1.Tables[2]);
                        localReport.AddDataSource("FlyingSquadMembers", data1.Tables[3]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpPost("GetITIFlyingSquadReports")]
        public async Task<ApiResult<DataTable>> GetITIFlyingSquadReports([FromBody] GetFlyingSquadModal body)
        {
            ActionName = "GetITIFlyingSquadReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetITIFlyingSquadReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetITIFlyingSquadTeamReports")]
        public async Task<ApiResult<DataTable>> GetITIFlyingSquadTeamReports([FromBody] GetFlyingSquadModal body)
        {
            ActionName = "GetITIFlyingSquadTeamReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetITIFlyingSquadTeamReports(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region "Read server file"

        [HttpGet("GetByteImages")]
        public async Task<byte[]> GetByteImages(string imgUrl)
        {
            try
            {

                HttpClient client = new HttpClient();
                byte[] fileBytes = await client.GetByteArrayAsync(imgUrl);
                return fileBytes;
            }
            catch (Exception ex)
            {
                return System.IO.File.ReadAllBytes(Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg"));
            }

        }
        #endregion

        #region Dispatch Group Details Receipt
        [HttpGet("GetDispatchGroupDetails/{ID}/{EndTermID}/{CourseTypeID}")]
        public async Task<ApiResult<string>> GetDispatchGroupDetails(int ID, int EndTermID, int CourseTypeID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetDispatchGroupDetails(ID, EndTermID, CourseTypeID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"DispatchGroupDetails_{ID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Dispatch_GroupList.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Dispatch_Bundle", data.Tables[0]);
                        localReport.AddDataSource("Dispatch_Bundle_Table", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;



                        //bool Issuccess = await _unitOfWork.DispatchRepository.UpdateDownloadFileDispatchMaster(fileName, ID);
                        //if (Issuccess)
                        //{
                        //    result.Data = fileName;
                        //    result.State = EnumStatus.Success;
                        //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.State = EnumStatus.Warning;
                        //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                        //}


                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }



                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Dispatch Group Details Certificate
        [HttpGet("DownloadDispatchGroupCertificate/{ID}/{StaffID}/{DepartmentID}")]
        public async Task<ApiResult<string>> DownloadDispatchGroupCertificate(int ID, int StaffID, int DepartmentID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadDispatchGroupCertificate(ID, StaffID, DepartmentID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"DispatchGroupCertificate_{ID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Dispatch_Undertacking.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Dispatch_Undertaking", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }



                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region BTER 13B Attendance Report
        [HttpPost("AttendanceReport13B")]
        public async Task<ApiResult<string>> AttendanceReport13B(AttendanceReport13BDataModel model)
        {
            ActionName = "AttendanceReport13B(AttendanceReport13BDataModel model)";
            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => _unitOfWork.ReportRepository.AttendanceReport13B(model));

                if (data != null)
                {
                    //report
                    var fileName = $"Report_13-B(attendance_report).pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Report_13-B(attendance_report).rdlc";

                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("Report_13_B", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report

                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }
        #endregion

        #region BTER Report33
        [HttpPost("Report33")]
        public async Task<ApiResult<string>> Report33(AttendanceReport13BDataModel model)
        {
            ActionName = "Report33";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.Report33(model);

                    if (data != null)
                    {
                        //report
                        var fileName = $"Report_33.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Report_33.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Report33", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region BTER DailyReport_BhandarForm1
        [HttpPost("DailyReport_BhandarForm1")]
        public async Task<ApiResult<string>> DailyReport_BhandarForm1(AttendanceReport13BDataModel model)
        {
            ActionName = "Daily_Report(Bhandar_Form1)()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DailyReport_BhandarForm1(model);

                    if (data != null)
                    {
                        //report
                        var fileName = $"Daily_Report(Bhandar_Form1).pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/daily_report(Bhandar_form1).rdlc";

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        LocalReport localReport = new LocalReport(rdlcpath);

                        foreach (DataRow row in data.Tables[0].Rows)
                        {
                            string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{row["FileName"]}";
                            row["moharImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));
                        }


                        localReport.AddDataSource("Daily_Report_Bhandar_Form1", data.Tables[0]);
                        localReport.AddDataSource("BhandarForm_DataTabl2", data.Tables[1]);
                        localReport.AddDataSource("Daily_Report_Bhandar_Form_UFM", data.Tables[2]);
                        localReport.AddDataSource("Daily_Report_WithoutAdmitcard", data.Tables[3]);
                        localReport.AddDataSource("Daily_Report_AdmitCard", data.Tables[4]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Dispatch Principal Group Code Details Receipt
        [HttpGet("GetDispatchPrincipalGroupCodeDetails/{ID}/{DepartmentID}")]
        public async Task<ApiResult<string>> GetDispatchPrincipalGroupCodeDetails(int ID, int DepartmentID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetDispatchPrincipalGroupCodeDetails(ID, DepartmentID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"DispatchPrincipalGroupCodeDetails_{ID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/DispatchPrincipalGroupCodeDetails_.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("DispatchPrincipalGroupCodeDetails_", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;



                        bool Issuccess = await _unitOfWork.DispatchRepository.UpdateDispatchPrincipalGroupCodefile(fileName, ID);
                        if (Issuccess)
                        {
                            result.Data = Issuccess.ToString();
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Get Dispatch Superintendent Rpt Report
        [HttpGet("GetDispatchSuperintendentRptReport/{ID}/{DepartmentID}")]
        public async Task<ApiResult<string>> GetDispatchSuperintendentRptReport(int ID, int DepartmentID)
        {
            ActionName = "GetDispatchSuperintendentRptReport(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetDispatchSuperintendentRptReport(ID, DepartmentID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"DispatchSuperintendentDetails_{ID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Dispatch_Bundle.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Dispatch_Bundle", data.Tables[0]);
                        localReport.AddDataSource("Dispatch_Bundle_Table", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;



                        bool Issuccess = await _unitOfWork.DispatchRepository.UpdateDownloadFileDispatchMaster(fileName, ID);
                        if (Issuccess)
                        {
                            result.Data = Issuccess.ToString();
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Get Dispatch Superintendent Rpt Report
        [HttpGet("GetDispatchSuperintendentRptReport1/{ID}/{DepartmentID}")]
        public async Task<ApiResult<string>> GetDispatchSuperintendentRptReport1(int ID, int DepartmentID)
        {
            ActionName = "GetDispatchSuperintendentRptReport(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetDispatchSuperintendentRptReport1(ID, DepartmentID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"True.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Dispatch_Bundle.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Dispatch_Bundle", data.Tables[0]);
                        localReport.AddDataSource("Dispatch_Bundle_Table", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        bool Issuccess = await _unitOfWork.DispatchRepository.UpdateDownloadFileDispatchMaster(fileName, ID);
                        if (Issuccess)
                        {
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region "GetTestRDLC"
        [HttpPost("GetTestRDLC")]
        public async Task<ApiResult<string>> GetTestRDLC([FromBody] GenerateAdmitCardSearchModel model)
        {
            ActionName = "GetTestRDLC(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetTestRDLC(model);
                    if (data.Tables?.Count >= 1)
                    {
                        //report

                        var fileName = $"AdmitCard_{model.StudentID}_{model.StudentExamID}.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/TestNiranjanSir.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("TestNiranjanSir", data.Tables[0]);
                        //localReport.AddDataSource("AdmitCard_Subject", data.Tables[1]);//check file exists
                        //localReport.AddDataSource("TimeTableDetails", data.Tables[2]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region GetCenterWiseSubjectCountReportNew
        [HttpPost("GetCenterWiseSubjectCountReportNew")]
        public async Task<ApiResult<DataTable>> GetCenterWiseSubjectCountReportNew(ReportCustomizeBaseModel model)
        {
            ActionName = "GetCenterWiseSubjectCountReportNew()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCenterWiseSubjectCountReportNew(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        [HttpPost("GetRport33Data")]
        public async Task<ApiResult<DataTable>> GetRport33Data([FromBody] Report33DataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetRport33Data(body));
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("DailyReportBhandarForm")]
        public async Task<ApiResult<DataTable>> DailyReportBhandarForm([FromBody] Report33DataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.DailyReportBhandarForm(body));
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
                await _unitOfWork.DisposeAsync();
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

        #region Renumeration Examiner 
        [HttpPost("GenerateAndViewPdf")]
        [RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<IActionResult> GenerateAndViewPdf([FromBody] RenumerationExaminerRequestModel filterModel)
        {
            ActionName = "GenerateAndViewPdf([FromBody] RenumerationExaminerRequestModel filterModel)";
            try
            {
                var data = await _unitOfWork.RenumerationExaminerRepository.GetDataForGeneratePdf(filterModel);
                if (data?.Rows?.Count > 0)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderBTER, "RemunerationExaminer.rdlc");
                    //save file
                    var newFileName = $"RemunerationExaminer_{DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}.pdf";
                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    //file stream
                    return File(reportResult.MainStream, "application/pdf", newFileName);
                }
                else
                {
                    return Content("No data available to generate the PDF.");
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                return Content("An error occurred while generating the PDF.");
            }
        }

        [HttpPost("SavePDFSubmitAndForwardToJD")]
        [RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<ApiResult<bool>> SavePDFSubmitAndForwardToJD([FromBody] RenumerationExaminerRequestModel filterModel)
        {
            ActionName = "SavePDFSubmitAndForwardToJD([FromBody] RenumerationExaminerRequestModel filterModel)";
            var result = new ApiResult<bool>();
            try
            {
                var data = await _unitOfWork.RenumerationExaminerRepository.GetDataForGeneratePdf(filterModel);
                var objData = CommonFuncationHelper.ConvertDataTable<RenumerationExaminerPDFModel>(data);
                if (objData != null)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderBTER, "RemunerationExaminer.rdlc");
                    //save file
                    var newFileName = $"RemunerationExaminer_{DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}.pdf";
                    var folderPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.ReportsFolder);
                    var filepath = Path.Combine(folderPath, newFileName);

                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //file stream
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    //save in folder
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                    //save in db
                    objData.IPAddress = CommonFuncationHelper.GetIpAddress();
                    objData.FileName = newFileName;

                    var isSave = await _unitOfWork.RenumerationExaminerRepository.SaveDataSubmitAndForwardToJD(objData);
                    await _unitOfWork.SaveChangesAsync();
                    if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_UPDATE_ERROR;
                    }
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
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
        #endregion

        #region Student ITI Admission Challan Receipt
        [HttpGet("GetITIStudentApplicationChallanReceipt/{ApplicationID}")]
        public async Task<ApiResult<string>> GetITIStudentApplicationChallanReceipt(int ApplicationID)
        {
            ActionName = "GetITIStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIStudentApplicationChallanReceipt(ApplicationID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ITIChallanReceipt_{ApplicationID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ITIChallanReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ITIChallan", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region ITI Student Application Fee Receipt
        [HttpGet("GetITIStudentApplicationFeeReceipt/{EnrollmentNo}")]
        public async Task<ApiResult<string>> GetITIStudentApplicationFeeReceipt(string EnrollmentNo)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIStudentApplicationFeeReceipt(EnrollmentNo);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ITIApplicationFeeReceipt_{EnrollmentNo}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIApplicationFeeReceipt.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region ITI College Profile Receipt
        [HttpGet("GetITICollegeProfile/{CollegeId}")]
        public async Task<ApiResult<string>> GetITICollegeProfile(int CollegeId)
        {
            ActionName = "GetITICollegeProfile(string CollegeId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITICollegeProfile(CollegeId);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"College_Profile{CollegeId}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/College_Profile.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ITI_College_Profile", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion






        [HttpPost("ScaReportAdmin")]
        public async Task<ApiResult<DataTable>> ScaReportAdmin([FromBody] StudentCenteredActivitesMasterSearchModel body)
        {
            ActionName = "ScaReportAdmin([FromBody] StudentCenteredActivitesMasterSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ReportRepository.ScaReportAdmin(body);
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
                await _unitOfWork.DisposeAsync();
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


        #region Examiner Appoint Letter
        [HttpGet("GetExaminerAppointLetter/{ExaminerID}/{DepartmentID}/{InstituteID}/{EndTermID}")]
        public async Task<ApiResult<string>> GetExaminerAppointLetter(int ExaminerID, int DepartmentID, int InstituteID, int EndTermID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetExaminerAppointLetter(ExaminerID, DepartmentID, InstituteID, EndTermID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ExaminerAppointLetter_{ExaminerID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ExaminerAppointLetter.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Examiner_Appoint_Letter", data.Tables[0]);
                        localReport.AddDataSource("Examiner_AnswerBook_List", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion


        #region Institute Master Report
        [HttpPost("InstituteMasterReport")]
        public async Task<ApiResult<string>> InstituteMasterReport()
        {
            ActionName = "InstituteMasterReport(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.InstituteMasterReport();
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"InstituteReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/InstituteMasterReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("InstituteMasterReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Teacher Wise Report
        [HttpPost("TeacherWiseReportPdf")]
        public async Task<ApiResult<string>> TeacherWiseReportPdf()
        {
            ActionName = "TeacherWiseReportPdf()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.TeacherWiseReportPdf();
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"TeacherReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/TeacherWiseReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("TeacherWiseReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Subject Wise Report
        [HttpPost("SubjectWiseReportPdf")]
        public async Task<ApiResult<string>> SubjectWiseReportPdf()
        {
            ActionName = "SubjectWiseReportPdf()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.SubjectWiseReportPdf();
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"SubjectReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/SubjectWiseReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("SubjectWiseReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        #region CenterSuperintendentStudentReport
        [HttpPost("GetCenterSuperintendentStudentReport")]
        public async Task<ApiResult<DataTable>> GetCenterSuperintendentStudentReport([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetCenterSuperintendentStudentReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCenterSuperintendentStudentReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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
        #endregion

        #region Statistics Information Report
        [HttpPost("StatisticsInformationReportPdf")]
        public async Task<ApiResult<string>> StatisticsInformationReportPdf([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "StatisticsInformationReportPdf([FromBody] GroupCenterMappingModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.ReportRepository.StatisticsInformationReportPdf(body);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"StatisticsReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Statistical_Information.rdlc";


                        string singimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[1].Rows[0]["SignPhoto"]}";
                        data.Tables[1].Rows[0]["SignImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(singimgFilepath));

                        string masscopyimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[1].Rows[0]["MassCopyDocument"]}";
                        data.Tables[1].Rows[0]["MassCopyImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(masscopyimgFilepath));

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Statistical_Information", data.Tables[0]);
                        localReport.AddDataSource("Statistical_report_Information", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Theory Marks Report
        [HttpPost("TheorymarksReportPdf")]
        public async Task<ApiResult<string>> TheorymarksReportPdf(TheorySearchModel filterModel)
        {
            ActionName = "TheorymarksReportPdf(TheorySearchModel filterModel)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.TheorymarksReportPdf(filterModel);

                    if (data != null)
                    {

                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"TheoryReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Theory_Marks_Report.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("TheoryMarksReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpPost("TheorymarksReportPdf_ITI")]
        public async Task<ApiResult<string>> TheorymarksReportPdf_ITI(TheorySearchModel filterModel)
        {
            ActionName = "TheorymarksReportPdf()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.TheorymarksReportPdf(filterModel);

                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";

                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Group data by GroupCode, Branch, and SubjectCode
                        var groupedData = data.Tables[0].AsEnumerable()
                            .GroupBy(row => new
                            {
                                SubjectCode = row["SubjectCode"]
                            });

                        // Initialize a list to store the individual PDF file paths
                        List<string> pdfFiles = new List<string>();
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        foreach (var group in groupedData)
                        {
                            // Get data for this specific group
                            var groupData = group.CopyToDataTable();

                            var fileName = $"{group.Key.SubjectCode}_TheoryReport_{timestamp}.pdf";
                            string filepath = $"{folderPath}/{fileName}";

                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITI_Theory_Marks_Report.rdlc";

                            var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");

                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("TheoryMarksReport", groupData);
                            var reportResult = localReport.Execute(RenderType.Pdf);

                            // Save the report for this group
                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                            // Add this PDF file to the list of PDFs to merge
                            pdfFiles.Add(filepath);
                        }

                        // Now merge all individual PDFs into a single PDF
                        string mergedFilePath = $"Merged_TheoryMarksReport_{timestamp}.pdf";
                        string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{mergedFilePath}";

                        bool mergeSuccess = await MergePdfFilesAsync(pdfFiles, outputPath);
                        if (mergeSuccess)
                        {
                            result.Data = mergedFilePath;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "Something went wrong while merging the PDFs.";
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
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "something went wrong please try again";
                }

                return result;
            });
        }



        #endregion

        #region Theory Marks Report BTER
        [HttpPost("TheorymarksReportPdf_BTER")]
        public async Task<ApiResult<string>> TheorymarksReportPdf_BTER(TheorySearchModel filterModel)
        {
            ActionName = "TheorymarksReportPdf_BTER(TheorySearchModel filterModel)";
            var result = new ApiResult<string>();
            try
            {
                var data = await _unitOfWork.ReportRepository.TheorymarksReportPdf_BTER(filterModel);

                if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                var sb = await _printHtmlFile.TheoryMarksReports_GetHtml(data, filterModel.IsReval);
                var _html = sb.ToString();

                // remove last blank page
                string endTag = "<div class='page-break'></div></body></html>";
                if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    _html = _html.Substring(0, _html.Length - endTag.Length)
                                 + "</body></html>";
                }

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(doc));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion


        #region UFM Category Report BTER
        [HttpPost("UFMCategoryReportPdf_BTER")]
        public async Task<ApiResult<string>> UFMCategoryReportPdf_BTER(UFMCategoryUpdateModel filterModel)
        {
            ActionName = "TheorymarksReportPdf_BTER(TheorySearchModel filterModel)";
            var result = new ApiResult<string>();
            try
            {
                var data = await _unitOfWork.ReportRepository.UFMCategoryReportPdf_BTER(filterModel);

                if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                var sb = await _printHtmlFile.UFMCategoryReportPdf_BTER_GetHtml(data);
                var _html = sb.ToString();

                // remove last blank page
                string endTag = "<div class='page-break'></div></body></html>";
                if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    _html = _html.Substring(0, _html.Length - endTag.Length)
                                 + "</body></html>";
                }

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(doc));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        [HttpPost("UFM_Collegwise_CategoryReportPdf_BTER")]
        public async Task<ApiResult<string>> UFM_Collegwise_CategoryReportPdf_BTER(UFMCategoryUpdateModel filterModel)
        {
            ActionName = "UFM_Collegwise_CategoryReportPdf_BTER(TheorySearchModel filterModel)";
            var result = new ApiResult<string>();
            try
            {
                var data = await _unitOfWork.ReportRepository.UFM_Collegwise_CategoryReportPdf_BTER(filterModel);

                if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                var sb = await _printHtmlFile.Collegwise_UFMCategoryReportPdf_BTER_GetHtml(data);
                var _html = sb.ToString();

                // remove last blank page
                string endTag = "<div class='page-break'></div></body></html>";
                if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    _html = _html.Substring(0, _html.Length - endTag.Length)
                                 + "</body></html>";
                }

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(doc));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                // Write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        #endregion


        [HttpPost("TheoryMarkListPDFReport")]
        public async Task<ApiResult<string>> TheoryMarkListPDFReport(ReportCustomizeBaseModel model)
        {
            ActionName = "GetAbsentReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    List<ExamResultViewModel> ListData = new List<ExamResultViewModel>();
                    var data = await _unitOfWork.ReportRepository.TheoryMarkListPDFReport(model);
                    if (data.Tables[0].Rows.Count > 1)
                    {
                        ListData = CommonFuncationHelper.ConvertDataTable<List<ExamResultViewModel>>(data.Tables[0]);
                    }
                    if (ListData.Count > 0)
                    {

                        foreach (var item in ListData.GroupBy(f => f.StreamCode))
                        {




                            DataTable dt = data.Tables[0].AsEnumerable()
                                                     .Where(row => row.Field<string>("StreamCode") == item.Key)
                                                     .CopyToDataTable();


                            if (dt != null)
                            {
                                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                                //report
                                var fileName = $"Theory_Marks_Report.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Theory_Marks_Absent_Report.rdlc";
                                //
                                var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                                //
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                LocalReport localReport = new LocalReport(rdlcpath);
                                localReport.AddDataSource("Theory_Marks_Report", dt);
                                var reportResult = localReport.Execute(RenderType.Pdf);
                                //check file exists
                                if (!System.IO.Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }
                                //save


                                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                                //end report

                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            }

                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        #region GetITISearchRepot
        [HttpPost("GetITISearchRepot")]
        public async Task<ApiResult<DataTable>> GetITISearchRepot([FromBody] ITISearchDataModel body)
        {

            ActionName = "GetStaticsReportProvideByExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetITISearchRepot(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion



        [HttpPost("DownloadApplication")]
        public FileResult DownloadApplication(int StudentId)
        {
            try
            {
                var fileName = "ApplicationForm" + StudentId + ".pdf";
                string devFontSize = "15px";
                string fontSize = "font-size: 10px;";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                string filepath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ApplicationForm.html";





                DataTable orgTable = new DataTable("Org");
                orgTable.Columns.Add("LogoLeft");
                orgTable.Rows.Add("images/logo-left.png");

                DataTable admissionTable = new DataTable("Admission");
                admissionTable.Columns.Add("Type");
                admissionTable.Columns.Add("FinYear");
                admissionTable.Rows.Add("Polytechnic", "2024-25");

                DataSet ds = new DataSet();
                ds.Tables.Add(orgTable);
                ds.Tables.Add(admissionTable);

                string html = Utility.PDFWorks.GetHtml("template.html", ds);

                System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>")));

                var pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "");

                return File(pdfBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            }
            catch (Exception ex)
            {
                //ErrorLogs.LogError("Admission", "DownloadApplication(" + StudentId + ")", ex);
                return null;
            }

        }

        [HttpPost("GetApplicationFormPreview1")]
        public async Task<ApiResult<string>> GetApplicationFormPreview1([FromBody] BterSearchModel student)
        {
            ActionName = "GetApplicationFormPreview1(string ApplicationID)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.ReportRepository.GetApplicationFormPreview(student);
                    if (data?.Tables?.Count == 6)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ApplicationFormPreview_{student.ApplicationId + DateTime.Now.ToString("ddMMyyyhhss")}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{data.Tables[4].Rows[0]["FolderName"]}/{fileName}";
                        //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images


                        string[] Folders = data.Tables[4].Rows[0]["FolderName"].ToString().Split("/");
                        string parentFolder = "";
                        for (int i = 0; i < Folders.Length; i++)
                        {
                            if (!System.IO.Directory.Exists($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}"))
                            {
                                System.IO.Directory.CreateDirectory($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}");
                            }
                            parentFolder = parentFolder + "/" + Folders[i];
                        }

                        data.Tables[0].Rows[0]["LogoLeft"] = $"{ConfigurationHelper.StaticFileRootPath}/bter_logo.png";

                        data.Tables[0].Rows[0]["LogoRight"] = $"{ConfigurationHelper.StaticFileRootPath}/CEGlogo21.png";


                        DataTable filteredTable = data.Tables[4].AsEnumerable().Where(row => row.Field<string>("ColumnName").Contains("StudentPhoto")).CopyToDataTable();

                        DataTable filteredTable1 = data.Tables[4].AsEnumerable().Where(row => row.Field<string>("ColumnName").Contains("StudentSign")).CopyToDataTable();

                        //data.Tables[0].Rows[0]["StudentPhoto"] = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{filteredTable.Rows[0]["FileName"]}";

                        //data.Tables[0].Rows[0]["StudentSignIMG"] = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{filteredTable1.Rows[0]["FileName"]}";


                        data.Tables[0].Rows[0]["StudentPhoto"] = $"{ConfigurationHelper.StaticFileRootPath}{filteredTable.Rows[0]["FolderName"]}/{filteredTable.Rows[0]["FileName"]}";

                        data.Tables[0].Rows[0]["StudentSignIMG"] = $"{ConfigurationHelper.StaticFileRootPath}{filteredTable.Rows[0]["FolderName"]}/{filteredTable1.Rows[0]["FileName"]}";


                        /*define table name for read and replace column from table*/
                        data.Tables[0].TableName = "Personal_Details";
                        data.Tables[1].TableName = "Qualification_Details";
                        data.Tables[2].TableName = "Option_Details";
                        data.Tables[3].TableName = "Uploaded_Documents";

                        DataTable dt10thQua = new DataTable("Qualification10_Details");
                        DataTable dtHighthQua = new DataTable("QualificationHigh_Details");
                        DataTable dtlLateralQua = new DataTable("QualificationLateral_Details");
                        DataTable dtEnglishQua = new DataTable("EnglishQualification_Details");


                        if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") == "10").Count() > 0)
                        {
                            dt10thQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") == "10").CopyToDataTable();
                        }

                        if (data.Tables[0].Rows[0]["CourseTypeID"].ToString() == "2" && data.Tables[0].Rows[0]["IsHighestQualification"].ToString() == "1")
                        {
                            // this.HighestQualificationView = this.request.QualificationViewDetails.filter(function(dat: any) { return dat.QualificationID != '10' });
                            if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").Count() > 0)
                            {
                                dtHighthQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").CopyToDataTable();
                            }
                        }

                        if (data.Tables[0].Rows[0]["CourseTypeID"].ToString() == "3")
                        {
                            if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").Count() > 0)
                            {
                                dtlLateralQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").CopyToDataTable();
                            }
                        }

                        if (data.Tables[0].Rows[0]["CourseTypeID"].ToString() == "4")
                        {
                            if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").Count() > 0)
                            {
                                dtlLateralQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "English" && row.Field<string>("QualificationID") != "10").CopyToDataTable();
                            }

                            // Check if there are any rows with QualificationID = "English" before executing the code
                            var englishRows = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") == "English");
                            if (englishRows.Count() > 0)
                            {
                                // If there are rows with "English", copy them to dtEnglishQua
                                dtEnglishQua = englishRows.CopyToDataTable();
                            }
                        }

                        if (data.Tables[0].Rows[0]["CourseTypeID"].ToString() == "5")
                        {
                            if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").Count() > 0)
                            {
                                dtlLateralQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").CopyToDataTable();
                            }
                        }


                        data.Tables.Add(dt10thQua);
                        data.Tables[6].TableName = "Qualification10_Details";
                        data.Tables.Add(dtHighthQua);
                        data.Tables[7].TableName = "QualificationHigh_Details";
                        data.Tables.Add(dtlLateralQua);
                        data.Tables[8].TableName = "QualificationLateral_Details";
                        data.Tables.Add(dtEnglishQua);
                        data.Tables[9].TableName = "EnglishQualification_Details";

                        string devFontSize = "15px";
                        /*default font size for kruti dev*/
                        //string fontSize = "font-size: 10px;";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ApplicationForm.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        if (System.IO.File.Exists(filepath))
                        {
                            System.IO.File.Delete(filepath);
                        }
                        if (Utility.PDFWorks.GeneratePDF(sb1, filepath, ""))
                        {
                            //byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                            //string file_Name = filepath.Split('/')[filepath.Split('/').Length - 1];
                            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file_Name);
                        }
                        else
                        {
                            //return null;
                        }


                        ////check file exists
                        //if (!System.IO.Directory.Exists(folderPath))
                        //{
                        //    Directory.CreateDirectory(folderPath);
                        //}

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpGet("GetApplicationFormPreview2/{StudentId}")]
        public async Task<ApiResult<string>> GetApplicationFormPreview2(int StudentId)
        {
            BterSearchModel student = new BterSearchModel();
            student.ApplicationId = StudentId;
            ActionName = "GetApplicationFormPreview1(string ApplicationID)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();


                var data = await _unitOfWork.ReportRepository.GetApplicationFormPreview(student);
                if (data?.Tables?.Count == 6)
                {
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    //report
                    var fileName = $"ApplicationFormPreview_{student.ApplicationId}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{data.Tables[4].Rows[0]["FolderName"]}/{fileName}";
                    //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

                    //provider                      
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    //images


                    string[] Folders = data.Tables[4].Rows[0]["FolderName"].ToString().Split("/");
                    string parentFolder = "";
                    for (int i = 0; i < Folders.Length; i++)
                    {
                        if (!System.IO.Directory.Exists($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}"))
                        {
                            System.IO.Directory.CreateDirectory($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}");
                        }
                        parentFolder = parentFolder + "/" + Folders[i];
                    }

                    data.Tables[0].Rows[0]["LogoLeft"] = $"{ConfigurationHelper.StaticFileRootPath}/bter_logo.png";

                    data.Tables[0].Rows[0]["LogoRight"] = $"{ConfigurationHelper.StaticFileRootPath}/bter_logo.jpg";


                    DataTable filteredTable = data.Tables[4].AsEnumerable().Where(row => row.Field<string>("ColumnName").Contains("StudentPhoto")).CopyToDataTable();

                    DataTable filteredTable1 = data.Tables[4].AsEnumerable().Where(row => row.Field<string>("ColumnName").Contains("StudentSign")).CopyToDataTable();

                    //data.Tables[0].Rows[0]["StudentPhoto"] = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{filteredTable.Rows[0]["FileName"]}";

                    //data.Tables[0].Rows[0]["StudentSignIMG"] = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{filteredTable1.Rows[0]["FileName"]}";


                    data.Tables[0].Rows[0]["StudentPhoto"] = $"{ConfigurationHelper.StaticFileRootPath}{filteredTable.Rows[0]["FolderName"]}/{filteredTable.Rows[0]["FileName"]}";

                    data.Tables[0].Rows[0]["StudentSignIMG"] = $"{ConfigurationHelper.StaticFileRootPath}{filteredTable.Rows[0]["FolderName"]}/{filteredTable1.Rows[0]["FileName"]}";


                    /*define table name for read and replace column from table*/
                    data.Tables[0].TableName = "Personal_Details";
                    data.Tables[1].TableName = "Qualification_Details";
                    data.Tables[2].TableName = "Option_Details";
                    data.Tables[3].TableName = "Uploaded_Documents";

                    DataTable dt10thQua = new DataTable("Qualification10_Details");
                    DataTable dtHighthQua = new DataTable("QualificationHigh_Details");
                    DataTable dtlLateralQua = new DataTable("QualificationLateral_Details");


                    if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") == "10").Count() > 0)
                    {
                        dt10thQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") == "10").CopyToDataTable();
                    }

                    if (data.Tables[0].Rows[0]["CourseTypeID"].ToString() == "2" && data.Tables[0].Rows[0]["IsHighestQualification"].ToString() == "1")
                    {
                        // this.HighestQualificationView = this.request.QualificationViewDetails.filter(function(dat: any) { return dat.QualificationID != '10' });
                        if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").Count() > 0)
                        {
                            dtHighthQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").CopyToDataTable();
                        }
                    }

                    if (data.Tables[0].Rows[0]["CourseTypeID"].ToString() == "3")
                    {
                        if (data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").Count() > 0)
                        {
                            dtlLateralQua = data.Tables[1].AsEnumerable().Where(row => row.Field<string>("QualificationID") != "10").CopyToDataTable();
                        }
                    }


                    data.Tables.Add(dt10thQua);
                    data.Tables[6].TableName = "Qualification10_Details";
                    data.Tables.Add(dtHighthQua);
                    data.Tables[7].TableName = "QualificationHigh_Details";
                    data.Tables.Add(dtlLateralQua);
                    data.Tables[8].TableName = "QualificationLateral_Details";

                    string devFontSize = "15px";
                    /*default font size for kruti dev*/
                    //string fontSize = "font-size: 10px;";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();


                    string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ApplicationForm.html";

                    string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                    System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                    html = Utility.PDFWorks.ReplaceCustomTag(html);

                    sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                    if (Utility.PDFWorks.GeneratePDF(sb1, filepath, ""))
                    {
                        //byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                        //string file_Name = filepath.Split('/')[filepath.Split('/').Length - 1];
                        //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file_Name);
                    }
                    else
                    {
                        //return null;
                    }


                    ////check file exists
                    //if (!System.IO.Directory.Exists(folderPath))
                    //{
                    //    Directory.CreateDirectory(folderPath);
                    //}

                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = "Success";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }

                return result;
            });
        }

        [HttpGet("UpdateDOC/{sourceFolderPath}/{destinationFolderPath}")]
        public async Task<ApiResult<string>> UpdateDOC(string sourceFolderPath, string destinationFolderPath)
        {
            BterSearchModel student = new BterSearchModel();
            //student.ApplicationId = ApplicationID;
            ActionName = "GetApplicationFormPreview1(string ApplicationID)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            //sourceFolderPath = "/StaticFiles/Students/BTER/0/3/";
            //destinationFolderPath = "/StaticFiles/Students/BTER/2025/3/";

            sourceFolderPath = sourceFolderPath.Replace("-", "/");
            destinationFolderPath = destinationFolderPath.Replace("-", "/");

            sourceFolderPath = $"{ConfigurationHelper.StaticFileRootPath}{sourceFolderPath}";
            destinationFolderPath = $"{ConfigurationHelper.StaticFileRootPath}{destinationFolderPath}";

            return await Task.Run(async () =>
            {
                var data = await _unitOfWork.ReportRepository.GetApplicationFormPreview(new BterSearchModel() { ApplicationId = 0, DepartmentID = 0 });
                var result = new ApiResult<string>();
                try
                {
                    // Ensure destination root folder exists
                    if (!Directory.Exists(destinationFolderPath))
                    {
                        Directory.CreateDirectory(destinationFolderPath);
                    }

                    // Get all subdirectories in the source folder
                    foreach (string dirPath in Directory.GetDirectories(sourceFolderPath, "*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            string relativePath = dirPath.Substring(sourceFolderPath.Length);
                            string newDirPath = Path.Combine(destinationFolderPath, relativePath);
                            Directory.CreateDirectory(newDirPath);
                        }
                        catch (Exception ex) { }
                    }

                    // Move all files
                    foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            string relativePath = filePath.Substring(sourceFolderPath.Length);
                            string destinationFilePath = Path.Combine(destinationFolderPath, relativePath);

                            // Ensure destination directory exists
                            string destinationDir = Path.GetDirectoryName(destinationFilePath);
                            if (!Directory.Exists(destinationDir))
                            {
                                Directory.CreateDirectory(destinationDir);
                            }

                            System.IO.File.Copy(filePath, destinationFilePath);
                        }
                        catch (Exception ex) { }
                    }

                    Console.WriteLine("All files moved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while moving files: " + ex.Message);
                }
                return result;
            });
        }



        #region BTER Report23
        [HttpPost("Report23")]
        public async Task<ApiResult<string>> Report23(AttendanceReport23DataModel model)
        {
            ActionName = "Report33(AttendanceReport23DataModel model)";
            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => _unitOfWork.ReportRepository.Report23(model));

                if (data == null || data?.Tables?.Count < 2 || data?.Tables[0]?.Rows?.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                //report
                var fileName = $"BTERExam_{data.Tables[0].Rows[0]["CenterCode"]}_{data.Tables[0].Rows[0]["PaperCode"]}_Report_23.pdf";
                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Report_23.rdlc";

                LocalReport localReport = new LocalReport(rdlcpath);
                localReport.AddDataSource("Report23_Header", data.Tables[0]);
                localReport.AddDataSource("Report23_DataTable", data.Tables[1]);
                var reportResult = localReport.Execute(RenderType.Pdf);

                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                //end report

                result.Data = fileName;
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                //
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;

                // Write error log
                await _unitOfWork.DisposeAsync();
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
        #endregion



        #region examinations-reports-menu-wise

        [HttpPost("examinations-reports-menu-wise")]
        public async Task<ApiResult<DataTable>> ExaminationsReportsMenuWise([FromBody] ExaminationsReportsMenuWiseModel body)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.ExaminationsReportsMenuWise(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("download-student-enrollment-details")]
        public async Task<ApiResult<DataTable>> DownloadStudentEnrollmentDetails([FromBody] DownloadStudentEnrollmentDetailsModel body)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.DownloadStudentEnrollmentDetails(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("download-student-change-enrollment-details")]
        public async Task<ApiResult<DataTable>> DownloadStudentChangeEnrollmentDetails([FromBody] DownloadStudentChangeEnrollmentDetailsModel body)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.DownloadStudentChangeEnrollmentDetails(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("optional-format-report")]
        public async Task<ApiResult<DataTable>> DownloadOptionalFormatReport([FromBody] OptionalFormatReportModel body)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.DownloadOptionalFormatReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("date-wise-attendance-report")]
        public async Task<ApiResult<DataTable>> DateWiseAttendanceReport([FromBody] DateWiseAttendanceReport body)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.DateWiseAttendanceReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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


        #endregion

        [HttpPost("GetExaminerReportOfPresentAndAbsentDownload")]
        public async Task<ApiResult<string>> GetExaminerReportOfPresentAndAbsentDownload([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetExaminerReportOfPresentAndAbsentDownload([FromBody] GroupCenterMappingModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetExaminerReportOfPresentAndAbsentDownload(body);

                    if (data != null)
                    {
                        //report
                        var fileName = $"ExaminerPresentAndMarks.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ExaminerPresentAndMarks.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ExaminerPresentAndMarks", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });

        }

        #region Get ITI Dispatch Superintendent Rpt Report
        [HttpGet("GetITIDispatchSuperintendentRptReport1/{ID}/{DepartmentID}")]
        public async Task<ApiResult<string>> GetITIDispatchSuperintendentRptReport1(int ID, int DepartmentID)
        {
            ActionName = "GetDispatchSuperintendentRptReport(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIDispatchSuperintendentRptReport1(ID, DepartmentID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"True.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITI_Dispatch_Bundle.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ITI_Dispatch_Bundle", data.Tables[0]);
                        localReport.AddDataSource("ITI_Dispatch_Bundle_Table", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        bool Issuccess = await _unitOfWork.DispatchRepository.UpdateDownloadFileDispatchMaster(fileName, ID);
                        if (Issuccess)
                        {
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region College Payment Fee Receipt
        [HttpGet("GetCollegePaymentFeeReceipt/{TransactionId}")]
        public async Task<ApiResult<string>> GetCollegePaymentFeeReceipt(string TransactionId)
        {
            ActionName = "GetCollegePaymentFeeReceipt(string TransactionId)";
            var result = new ApiResult<string>();
            try
            {
                var data = await _unitOfWork.ReportRepository.GetCollegePaymentFeeReceipt(TransactionId);
                if (data != null)
                {
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    //report
                    var fileName = $"CollegePaymentFeeReceipt_{TransactionId}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/CollegePaymentFeeReceipt.rdlc";
                    //
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("StudentFeeReceipt", data.Tables[0]);
                    var reportResult = localReport.Execute(RenderType.Pdf);


                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save


                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report

                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }
        #endregion





        #region ITI Dispatch Group Details Certificate
        [HttpPost("GetITI_Dispatch_ShowbundleByExaminerToAdminData")]
        public async Task<ApiResult<string>> GetITI_Dispatch_ShowbundleByExaminerToAdminData(ITI_DispatchAdmin_ByExaminer_RptSearchModel model)
        {
            ActionName = "GetITI_Dispatch_ShowbundleByExaminerToAdminData(ITI_DispatchAdmin_ByExaminer_RptSearchModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITI_Dispatch_ShowbundleByExaminerToAdminData(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"ITIDispatchExaminerCertificate_{model.ExaminerID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIDispatch_Undertacking.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("GetEndTermName_ITI_DispatchGroupAdmin_Rpt", data.Tables[0]);
                        localReport.AddDataSource("ITIDispatch_ExaminerUndertaking", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }



                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region ITI GetITIStudent_Marksheet
        //[HttpPost("GetITIStudent_Marksheet")]
        //public async Task<ApiResult<string>> GetITIStudent_Marksheet(StudentMarksheetSearchModel model)
        //{
        //    ActionName = "GetITIStudent_Marksheet(StudentMarksheetSearchModel model)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {

        //            var data = await _unitOfWork.ReportRepository.GetITIStudent_Marksheet(model);
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
        //                data.Tables[0].Rows[0]["HeadLogo"] = $"{ConfigurationHelper.StaticFileRootPath + "/" + data.Tables[0].Rows[0]["HeadLogo"]}";

        //                data.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
        //                data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];
        //                /*define table name for read and replace column from table*/
        //                //DataTable marksheetTable = data.Tables[0];
        //                //marksheetTable.TableName = "GetITIStudent_Marksheet_SingleDetails";

        //                //if (marksheetTable.Rows.Count > 0)
        //                //{
        //                //    DataRow row = marksheetTable.Rows[0];

        //                //    // Make sure the columns exist before assigning values
        //                //    if (marksheetTable.Columns.Contains("logobg"))
        //                //        row["logobg"] = $"{ConfigurationHelper.StaticFileRootPath}/logobg.png";

        //                //    if (marksheetTable.Columns.Contains("ITILogo"))
        //                //        row["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";

        //                //    if (marksheetTable.Columns.Contains("NE100"))
        //                //        row["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";

        //                //    if (marksheetTable.Columns.Contains("NE"))
        //                //        row["NE"] = $"{ConfigurationHelper.StaticFileRootPath}/NE.png";
        //                //}


        //                data.Tables[1].TableName = "GetITIStudent_Marksheet_Details";

        //                string devFontSize = "15px";
        //                /*default font size for kruti dev*/
        //                //string fontSize = "font-size: 10px;";
        //                System.Text.StringBuilder sb = new System.Text.StringBuilder();


        //                string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/ITIMarksheet.html";

        //                string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

        //                System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

        //                html = Utility.PDFWorks.ReplaceCustomTag(html);

        //                html = html.Replace("class=\"IsRowBold_2\"", "style=\"font-weight:bold\"");
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
        //            await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetITIStudent_Marksheet")]
        public async Task<ApiResult<string>> GetITIStudent_Marksheet(StudentMarksheetSearchModel model)
        {
            ActionName = "GetITIStudent_Marksheet(StudentMarksheetSearchModel model)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();

                try
                {
                    var data = await _unitOfWork.ReportRepository.GetITIStudent_Marksheet(model);

                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        data.Tables[0].TableName = "GetITIStudent_Marksheet_SingleDetails";
                        data.Tables[1].TableName = "GetITIStudent_Marksheet_Details";

                        data.Tables[0].Rows[0]["ITILogo"] =
                            $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";

                        data.Tables[0].Rows[0]["HeadLogo"] =
                            $"{ConfigurationHelper.StaticFileRootPath}/" +
                            data.Tables[0].Rows[0]["HeadLogo"];

                        data.Tables[0].Rows[0]["NE100"] =
                            $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";

                        data.Tables[0].Rows[0]["signlogo"] =
                            $"{ConfigurationHelper.StaticFileRootPath}/" +
                            data.Tables[0].Rows[0]["signlogo"];

                        string rollNo = data.Tables[0].Rows[0]["RollNo"].ToString();

                        //string dob = data.Tables[0].Rows[0]["DOB"].ToString();
                        //string sessionId = Convert.ToString(data.Tables[0].Rows[0]["EndTermId"]);
                        //string QRScanerURL = Convert.ToString(data.Tables[0].Rows[0]["QRScanerURL"]);


                        //string qrData = "http://localhost:4200/iti-Examination-public-info?rollNo="+ rollNo + "&dob=" + dob + "&sessionId=" + sessionId;


                        // string dob = Convert.ToString(data.Tables[0].Rows[0]["DOB"]);
                        string dob = "";
                        if (data.Tables[0].Rows[0]["QRDOB"] != DBNull.Value)
                        {
                            DateTime dobDate = Convert.ToDateTime(data.Tables[0].Rows[0]["QRDOB"]);
                            dob = dobDate.ToString("yyyy-MM-dd");
                        }
                        string sessionId = Convert.ToString(data.Tables[0].Rows[0]["EndTermId"]);
                        string QRScanerURL = Convert.ToString(data.Tables[0].Rows[0]["QRScanerURL"]);
                        string qrData = $"{QRScanerURL}?rollNo={rollNo}&dob={dob}&sessionId={sessionId}";

                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        string qrFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, "TempQR");
                        if (!Directory.Exists(qrFolder))
                            Directory.CreateDirectory(qrFolder);

                        string qrFileName = "QR_" + rollNo + ".png";
                        string qrFullPath = Path.Combine(qrFolder, qrFileName);

                        using (Bitmap qrBitmap = qrCode.GetGraphic(5))
                        {
                            qrBitmap.Save(qrFullPath, System.Drawing.Imaging.ImageFormat.Png);
                        }

                        if (!data.Tables[0].Columns.Contains("QRCode"))
                            data.Tables[0].Columns.Add("QRCode");

                        data.Tables[0].Rows[0]["QRCode"] =
                            ConfigurationHelper.StaticFileRootPath + "/TempQR/" + qrFileName;

                        string htmlTemplatePath =
                            $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/ITIMarksheet.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        html = html.Replace("class=\"IsRowBold_2\"", "style=\"font-weight:bold\"");

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                        sb1.Append(html);

                        var watermarkImagePath =
                            $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes =
                            Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();

                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };

                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }
        #endregion


        [HttpPost("ITIStateTradeCertificateReport")]
        public async Task<ApiResult<string>> ITIStateTradeCertificateReport([FromBody] ITIStateTradeCertificateModel model)
        {
            ActionName = "ITIStateTradeCertificateReport(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.ITIStateTradeCertificateReport(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "StateTradeCertificate";

                        data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];
                        data.Tables[0].Rows[0]["HeadLogo"] = $"{ConfigurationHelper.StaticFileRootPath + "/" + data.Tables[0].Rows[0]["HeadLogo"]}";

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();



                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/StateTradeCertificateReport.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);
                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";


                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }




        #region ITI GetITIStudent_MarksheetList
        [HttpPost("GetITIStudent_MarksheetList")]
        public async Task<ApiResult<DataSet>> GetITIStudent_MarksheetList(StudentMarksheetSearchModel model)
        {
            ActionName = "GetAllData([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIStudent_MarksheetList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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
        #endregion


        #region ITI GetITIStudent_PassList
        [HttpPost("GetITIStudent_PassList")]
        public async Task<ApiResult<string>> GetITIStudent_PassList(StudentMarksheetSearchModel model)
        {
            ActionName = "GetITIStudent_Marksheet(StudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.ReportRepository.GetITIStudent_PassList(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        //var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        ////report
                        //var fileName = $"JoiningLetter_{model.UserID}_{model.StaffID}.pdf";
                        //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images
                        data.Tables[0].TableName = "Student_PassListSemester1";
                        //data.Tables[1].TableName = "Student_PassListSemester2";

                        //data.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        /*define table name for read and replace column from table*/
                        //DataTable marksheetTable = data.Tables[0];
                        //marksheetTable.TableName = "GetITIStudent_Marksheet_SingleDetails";

                        //if (marksheetTable.Rows.Count > 0)
                        //{
                        //    DataRow row = marksheetTable.Rows[0];

                        //    // Make sure the columns exist before assigning values
                        //    if (marksheetTable.Columns.Contains("logobg"))
                        //        row["logobg"] = $"{ConfigurationHelper.StaticFileRootPath}/logobg.png";

                        //    if (marksheetTable.Columns.Contains("ITILogo"))
                        //        row["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";

                        //    if (marksheetTable.Columns.Contains("NE100"))
                        //        row["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";

                        //    if (marksheetTable.Columns.Contains("NE"))
                        //        row["NE"] = $"{ConfigurationHelper.StaticFileRootPath}/NE.png";
                        //}


                        //data.Tables[1].TableName = "GetITIStudent_Marksheet_Details";

                        string devFontSize = "15px";
                        /*default font size for kruti dev*/
                        //string fontSize = "font-size: 10px;";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.PassStudentRreport}/PassStudentRreport.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landscap");

                        // Example: Send in API
                        //return File(pdfBytes, "application/pdf", "Generated.pdf");


                        ///string dataUri = "data:application/pdf;base64," + base64String;
                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region "Practical Exam Format"
        [HttpPost("PracticalExamReport")]
        public async Task<ApiResult<string>> PracticalExamReport(BlankReportModel Model)
        {
            ActionName = "GetCollegePaymentFeeReceipt(string TransactionId)";
            var result = new ApiResult<string>();
            try
            {

                var data = await _unitOfWork.ReportRepository.GetPracticalExaminerMark(Model);
                if (data != null)
                {
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    //report
                    string guid = Guid.NewGuid().ToString().ToUpper();

                    var fileName = $"PracticalExamReport{guid}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIPracticalExaminerAttendanceReportFormat.rdlc";
                    //
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcpath);

                    localReport.AddDataSource("ExaminerHeaderDetails", data.Tables[0]);
                    localReport.AddDataSource("ExaminerStudentList", data.Tables[1]);

                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save

                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }


        #endregion




        [HttpPost("StateTradeCertificateDetails")]
        public async Task<ApiResult<DataTable>> StateTradeCertificateDetails([FromBody] ITIStateTradeCertificateModel body)
        {

            ActionName = "StateTradeCertificateDetails()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.StateTradeCertificateDetails(body);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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



        [HttpPost("ITIMarksheetConsolidated")]
        public async Task<ApiResult<string>> ITIMarksheetConsolidated([FromBody] ITIStateTradeCertificateModel model)
        {
            ActionName = "ITIMarksheetConsolidated(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.ITIMarksheetConsolidated(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "MarksheetConsolidated";

                        data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];

                        data.Tables[0].Rows[0]["mainlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        data.Tables[0].Rows[0]["HeadLogo"] = $"{ConfigurationHelper.StaticFileRootPath + "/" + data.Tables[0].Rows[0]["HeadLogo"]}";
                        data.Tables[1].TableName = "Consolidated_Marksheet";
                        decimal Total_Ob = 0;
                        decimal Total_Mx = 0;
                        foreach (DataRow dr in data.Tables[1].Rows)
                        {
                            Total_Ob += Convert.ToDecimal(dr["Total_Ob"].ToString());
                            Total_Mx += Convert.ToDecimal(dr["Total_Mx"].ToString());
                        }
                        data.Tables[0].Rows[0]["Percentage"] = Math.Round((Total_Ob / Total_Mx * 100), 2).ToString();


                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ITIMarksheetCONSOLIDATED.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);
                        html = html.Replace("class=\"IsRowBold_1\"", "style=\"font-weight:bold;text-align:center\"");

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        sb1.Append(html);

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landscap", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #region ITI GetITIStudent_PassDataList
        [HttpPost("GetITIStudent_PassDataList")]
        public async Task<ApiResult<DataSet>> GetITIStudent_PassDataList(StudentMarksheetSearchModel model)
        {
            ActionName = "GetITIStudent_PassDataList([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIStudent_PassDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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
        #endregion





        #region "Practical Exam Attendence Format"
        [HttpPost("PracticalExamAttendenceReport")]
        public async Task<ApiResult<string>> PracticalExamAttendenceReport(BlankReportModel Model)
        {
            ActionName = "GetCollegePaymentFeeReceipt(string TransactionId)";
            var result = new ApiResult<string>();
            try
            {

                var data = await _unitOfWork.ReportRepository.GetPracticalExaminerAttendence(Model);
                if (data != null)
                {
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    //report
                    string guid = Guid.NewGuid().ToString().ToUpper();
                    var fileName = $"PracticalExaminerAttendenceReport_{guid}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIPracticalExaminerAttendancePhotoReport.rdlc";
                    //

                    //foreach (DataRow row in data.Tables[1].Rows)
                    //{
                    //    row["StudentPhoto"] = "Jul042025041326899143.jpeg";
                    //}

                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    try
                    {
                        //string studentFileName = "Apr012025060950764086.png";
                        //string stuimgFilepath = "https://kdhteapi.rajasthan.gov.in/Api/StaticFiles//Students/" + studentFileName + "";
                        string stuimgFilepath = $"{ConfigurationHelper.RootPath}StaticFiles/Apr012025060950764086.png";
                        Console.WriteLine(stuimgFilepath);


                        //byte[] studentPhotoBytes = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        //// Ensure correct column type
                        if (!data.Tables[1].Columns.Contains("StudentPhoto1"))
                        {
                            data.Tables[1].Columns.Add("StudentPhoto1", typeof(byte[]));
                            data.Tables[1].Columns.Add("StudentPhoto2", typeof(string));
                        }

                        foreach (DataRow row in data.Tables[1].Rows)
                        {
                            string photoFileName = row["StudentPhoto1"].ToString();
                            string fullPhotoPath = Path.Combine(ConfigurationHelper.RootPath, "StaticFiles", "ITIPracticalExam", Convert.ToString(row["StudentPhoto"]));


                            //string fullPhotoPath = "https://kdhteapi.rajasthan.gov.in/Api/StaticFiles//Students/Jul042025041326899143.jpeg";
                            if (System.IO.File.Exists(fullPhotoPath))
                            {
                                row["StudentPhoto1"] = System.IO.File.ReadAllBytes(fullPhotoPath); // This must be byte[]

                            }
                            else
                            {
                                row["StudentPhoto1"] = System.IO.File.ReadAllBytes(Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg"));
                            }

                            if (row["StudentPhoto1"] != DBNull.Value && row["StudentPhoto1"] is byte[] photoBytes)
                            {
                                // Optional: further verify if it's a valid image format
                                using (var ms = new MemoryStream(photoBytes))
                                {
                                    try
                                    {
                                        using (var image = System.Drawing.Image.FromStream(ms))
                                        {
                                            Console.WriteLine("Valid image: " + image.Width + "x" + image.Height);
                                            var a = "Valid image: " + image.Width + "x" + image.Height;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Invalid image bytes: " + ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No image found or invalid byte[] type.");
                            }
                        }
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ExaminerHeaderDetails", data.Tables[0]);
                        localReport.AddDataSource("ExaminerStudentList", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
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
                await _unitOfWork.DisposeAsync();
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
            return result;
        }


        #endregion



        #region "Practical Exam Marking Format"
        [HttpPost("PracticalExamMarkingReport")]
        public async Task<ApiResult<string>> PracticalExamMarksReport(BlankReportModel Model)
        {
            ActionName = "GetCollegePaymentFeeReceipt(string TransactionId)";
            var result = new ApiResult<string>();
            try
            {

                var data = await _unitOfWork.ReportRepository.GetPracticalExaminerMarksReport(Model);
                if (data != null)
                {
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    //report
                    string guid = Guid.NewGuid().ToString().ToUpper();
                    var fileName = $"PracticalExaminerMarksReport_{guid}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIPracticalExaminerMarkReportFormat.rdlc";
                    //
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("ExaminerHeaderDetails", data.Tables[0]);
                    localReport.AddDataSource("ExaminerStudentList", data.Tables[1]);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save

                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }


        #endregion






        #region "Invigilator Theory List"
        [HttpPost("DownloadTheoryStudentITI")]
        public async Task<ApiResult<string>> DownloadTheoryStudentITI(ItiTheoryStudentMaster Model)
        {
            ActionName = "GetCollegePaymentFeeReceipt(string TransactionId)";
            var result = new ApiResult<string>();
            try
            {

                var data = await _unitOfWork.ReportRepository.DownloadTheoryStudentITI(Model);
                if (data != null)
                {
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    //report
                    string guid = Guid.NewGuid().ToString().ToUpper();
                    var fileName = $"TheoryExamAttendenceReport_{guid}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIPracticalExaminerReportFormat.rdlc";
                    //
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("ExaminerHeaderDetails", data.Tables[0]);
                    localReport.AddDataSource("ExaminerStudentList", data.Tables[1]);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save

                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }


        #endregion






        [HttpPost("ITITradeWiseResult")]
        public async Task<ApiResult<string>> ITITradeWiseResult([FromBody] ITIStateTradeCertificateModel model)
        {
            ActionName = "ITITradeWiseResult(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.ITITradeWiseResult(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "TradeWiseResult";

                        //data.Tables[0].Rows[0]["logo"]=$"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ITITradeWiseResult.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(html);

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", "");

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }



        [HttpPost("GetITITradeWiseResultDataList")]
        public async Task<ApiResult<DataSet>> GetITITradeWiseResultDataList(ITIStateTradeCertificateModel model)
        {
            ActionName = "GetITITradeWiseResultDataList([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITITradeWiseResultDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("GetITIAddmissionStatisticsDataList")]
        public async Task<ApiResult<DataSet>> GetITIAddmissionStatisticsDataList(ITIAddmissionReportSearchModel model)
        {
            ActionName = "GetITIAddmissionStatisticsDataList([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIAddmissionStatisticsDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetITISeatUtilizationStatusDataList")]
        public async Task<ApiResult<DataSet>> GetITISeatUtilizationStatusDataList(ITIAddmissionReportSearchModel model)
        {
            ActionName = "GetITISeatUtilizationStatusDataList([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITISeatUtilizationStatusDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetZoneDistrictSeatUtilization")]
        public async Task<ApiResult<DataSet>> GetZoneDistrictSeatUtilization([FromBody] ZoneDistrictSeatUtilizationRequestModel model)
        {
            ActionName = "GetZoneDistrictSeatUtilization([FromBody] ZoneDistrictSeatUtilizationRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetZoneDistrictSeatUtilization(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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



        [HttpPost("GetZoneDistrictSeatUtilization_ByGender")]
        public async Task<ApiResult<DataSet>> GetZoneDistrictSeatUtilization_ByGender([FromBody] ZoneDistrictSeatUtilizationByGenderRequestModel model)
        {
            ActionName = "GetZoneDistrictSeatUtilization_ByGender([FromBody] ZoneDistrictSeatUtilizationRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetZoneDistrictSeatUtilization_ByGender(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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



        [HttpPost("GetFinalAdmissionGenderWise")]
        public async Task<ApiResult<DataSet>> GetFinalAdmissionGenderWise([FromBody] FinalAdmissionGenderWiseRequestModel model)
        {
            ActionName = "GetFinalAdmissionGenderWise([FromBody] FinalAdmissionGenderWiseRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetFinalAdmissionGenderWise(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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


        [HttpPost("GetVacantSeatReport")]
        public async Task<ApiResult<DataSet>> GetVacantSeatReport([FromBody] VacantSeatReportRequestModel model)
        {
            ActionName = "GetVacantSeatReport([FromBody] VacantSeatReportRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetVacantSeatReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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

        [HttpPost("GetITIAdmissionsInWomenWingDataList")]
        public async Task<ApiResult<DataSet>> GetITIAdmissionsInWomenWingDataList(ITIAddmissionWomenReportSearchModel model)
        {
            ActionName = "GetITIAdmissionsInWomenWingDataList([FromBody] ITIAddmissionWomenReportSearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIAdmissionsInWomenWingDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetITITradeWiseAdmissionStatusDataList")]
        public async Task<ApiResult<DataSet>> GetITITradeWiseAdmissionStatusDataList(ITIAddmissionWomenReportSearchModel model)
        {
            ActionName = "GetITITradeWiseAdmissionStatusDataList([FromBody] ITIAddmissionWomenReportSearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITITradeWiseAdmissionStatusDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("GetITIPlaningDetailsDataList")]
        public async Task<ApiResult<DataSet>> GetITIPlaningDetailsDataList(ITIAddmissionWomenReportSearchModel model)
        {
            ActionName = "GetITIPlaningDetailsDataList([FromBody] ITIAddmissionWomenReportSearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIPlaningDetailsDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("CenterWiseTradeStudentCount")]
        public async Task<ApiResult<DataTable>> CenterWiseTradeStudentCount([FromBody] CenterStudentSearchModel body)
        {
            ActionName = "CenterWiseTradeStudentCount([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.CenterWiseTradeStudentCount(body));
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetITICategoryWiseSeatUtilizationDataList")]
        public async Task<ApiResult<DataSet>> GetITICategoryWiseSeatUtilizationDataList(ITIAddmissionReportSearchModel model)
        {
            ActionName = "GetITICategoryWiseSeatUtilizationDataList([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITICategoryWiseSeatUtilizationDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("showQRCode")]
        public async Task<ApiResult<string>> showQRCode(CampusPostQRDetail model)
        {
            ActionName = "showQRCode(CampusPostMasterModel model)";
            var result = new ApiResult<string>();

            try
            {
                // 1. Format model data as table-like string (for QR readability)
                var tableText = new StringBuilder();
                tableText.AppendLine("Campus Post QR Detail:");
                tableText.AppendLine($"https://kd.devitsandbox.com/singlepost?post={model.PostID}");
                // ... other table lines here ...

                // 2. Generate QR code using only the URL (for scanner-friendly clickable link)
                var qrUrl = $"https://kd.devitsandbox.com/singlepost?post={model.PostID}";
                byte[] qrBytes = CommonFuncationHelper.GenerateQrCode(qrUrl);

                // 3. Save QR code as PNG
                var fileName = $"QRCode_{model.PostID}_{DateTime.UtcNow.Ticks}.png";
                var folderPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.ReportsFolder);
                var filePath = Path.Combine(folderPath, fileName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                await System.IO.File.WriteAllBytesAsync(filePath, qrBytes);

                result.Data = fileName;
                result.State = EnumStatus.Success;
                result.Message = "QR code generated successfully.";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                await CreateErrorLog(new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                }, _unitOfWork);

                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        [HttpPost("GetAllotmentReportCollege")]
        public async Task<ApiResult<DataSet>> GetAllotmentReportCollege([FromBody] AllotmentReportCollegeRequestModel model)
        {
            ActionName = "GetAllotmentReportCollege([FromBody] AllotmentReportCollegeRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetAllotmentReportCollege(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }


        [HttpPost("GetAllotmentReportCollegeforAdmin")]
        public async Task<ApiResult<DataSet>> GetAllotmentReportCollegeforAdmin([FromBody] AllotmentReportCollegeForAdminRequestModel model)
        {
            ActionName = "GetAllotmentReportCollegeforAdmin([FromBody] AllotmentReportCollegeForAdminRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetAllotmentReportCollegeForAdmin(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }


        [HttpPost("GetBterCertificateReport")]
        public async Task<ApiResult<DataTable>> GetBterCertificateReport([FromBody] BterCertificateReportDataModel body)
        {
            ActionName = "GetStudentEnrollmentReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetBterCertificateReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("BterCertificateReportDownload")]
        public async Task<ApiResult<string>> BterCertificateReportDownload([FromBody] BterCertificateReportDataModel model)
        {
            ActionName = "BterCertificateReportDownload(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    string htmlTemplatePath = "";
                    string devFontSize = "15px";
                    if (model.Action == "provisional-certificate")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ProvisionalCertificate.html";
                        model.Action = "provisional-certificate-download";
                    }
                    if (model.Action == "migration-certificate")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/MigrationCertificate.html";
                        model.Action = "migration-certificate-download";
                    }

                    if (model.Action == "Cancel-Enrollment-migration")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/MigrationCertificate.html";
                        model.Action = "Cancel-Enrollment-migration-certificate-download";
                    }

                    var data = await _unitOfWork.ReportRepository.BterCertificateReportDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "BterCertificate";

                        data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/BTER-logo-black.jpg";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/BTER-logo-black.jpg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpPost("BterDiplomaBulkReportDownload")]
        public async Task<ApiResult<string>> BterDiplomaBulkReportDownload([FromBody] BterCertificateReportDataModel model)
        {
            ActionName = "BterDiplomaBulkReportDownload(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    string htmlTemplatePath = "";
                    string devFontSize = "15px";
                    if (model.Action == "diploma-report")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/DiplomaReport.html";
                        model.Action = "diploma-report-download";
                    }

                    var data = await _unitOfWork.ReportRepository.BterDiplomaReportDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "DiplomaReport";

                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));



                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", "");

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }



        [HttpPost("AppearedPassedStatisticsReportDownload")]
        public async Task<ApiResult<string>> AppearedPassedStatisticsReportDownload([FromBody] BterCertificateReportDataModel model)
        {
            ActionName = "AppearedPassedStatisticsReportDownload(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    string htmlTemplatePath = "";
                    string devFontSize = "15px";
                    if (model.Action == "Appeared-Passed-Statistics")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/AppearedPassedStatisticsReport.html";
                        model.Action = "Appeared-Passed-Statistics";
                    }

                    var data = await _unitOfWork.ReportRepository.AppearedPassedStatisticsReportDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AppearedPassedStatisticsReport";

                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));



                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", "");

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }



        [HttpPost("AppearedPassedInstituteWiseDownload")]
        public async Task<ApiResult<string>> AppearedPassedInstituteWiseDownload([FromBody] BterCertificateReportDataModel model)
        {
            ActionName = "AppearedPassedInstituteWiseDownload(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    string htmlTemplatePath = "";
                    string devFontSize = "15px";
                    if (model.Action == "Appeared-Passed-Statistics-InstituteWise")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/AppearedPassedInstituteWiseReport.html";
                        model.Action = "Appeared-Passed-Statistics-InstituteWise";
                    }

                    var data = await _unitOfWork.ReportRepository.AppearedPassedInstituteWiseDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AppearedPassedInstituteWiseReport";

                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));



                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", "");

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }


        [HttpPost("BterCertificateBulkReportDownload")]
        public async Task<ApiResult<string>> BterCertificateBulkReportDownload([FromBody] List<BterCertificateReportDataModel> models)
        {
            ActionName = "BterCertificateBulkReportDownload(List<BterCertificateReportDataModel>)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    System.Text.StringBuilder fullHtmlBuilder = new System.Text.StringBuilder();
                    string htmlTemplatePath = "";
                    string watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/BTER-logo-black.jpg";
                    string devFontSize = "20px";

                    foreach (var model in models)
                    {
                        if (model.Action == "provisional-certificate")
                        {
                            model.Action = "provisional-certificate-download";
                            htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ProvisionalCertificate.html";
                        }
                        else if (model.Action == "migration-certificate")
                        {
                            model.Action = "migration-certificate-download";
                            htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/MigrationCertificate.html";
                        }

                        else if (model.Action == "diploma-report")
                        {
                            model.Action = "diploma-report-download";
                            htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/DiplomaReport.html";
                        }

                        var data = await _unitOfWork.ReportRepository.BterCertificateReportDownload(model);
                        if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                        {
                            data.Tables[0].TableName = "BterCertificate";

                            // Set logo path
                            data.Tables[0].Rows[0]["logo"] = watermarkImagePath;

                            string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);
                            html = Utility.PDFWorks.ReplaceCustomTag(html);

                            string convertedHtml = UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize);

                            fullHtmlBuilder.Append(convertedHtml);

                        }
                    }

                    if (fullHtmlBuilder.Length > 0)
                    {
                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(
                            new System.Text.StringBuilder(fullHtmlBuilder.ToString()),
                            "",
                            watermarkImagePath
                        );

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }


        [HttpPost("BterCertificatePrePrintedBulkReportDownload")]
        public async Task<ApiResult<string>> BterCertificatePrePrintedBulkReportDownload([FromBody] List<BterCertificateReportDataModel> models)
        {
            ActionName = "BterCertificatePrePrintedBulkReportDownload(List<BterCertificateReportDataModel>)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    System.Text.StringBuilder fullHtmlBuilder = new System.Text.StringBuilder();
                    string htmlTemplatePath = "";
                    string watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/BTERCertificateSign.png";
                    string devFontSize = "15px";

                    foreach (var model in models)
                    {
                        if (model.Action == "provisional-certificate")
                        {
                            model.Action = "provisional-certificate-download";
                            htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ProvisionalCertificatePrePrinted.html";
                        }

                        else if (model.Action == "migration-certificate")
                        {
                            model.Action = "migration-certificate-download";
                            htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/MigrationCertificatePrePrinted.html";
                        }

                        var data = await _unitOfWork.ReportRepository.BterCertificateReportDownload(model);
                        if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                        {
                            data.Tables[0].TableName = "BterCertificate";

                            // Set logo path
                            data.Tables[0].Rows[0]["SignLogo"] = watermarkImagePath;

                            string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);
                            html = Utility.PDFWorks.ReplaceCustomTag(html);

                            string convertedHtml = UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize);

                            fullHtmlBuilder.Append(convertedHtml);

                        }
                    }

                    if (fullHtmlBuilder.Length > 0)
                    {
                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(new System.Text.StringBuilder(fullHtmlBuilder.ToString()), "", "");

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);

                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }

        #region  Student Age Between 15 and 29

        [HttpPost("GetStudentDataAgeBetween15And29")]
        public async Task<ApiResult<DataSet>> GetStudentDataAgeBetween15And29([FromBody] StudentDataAgeBetween15And29RequestModel model)
        {
            ActionName = "GetStudentDataAgeBetween15And29([FromBody] StudentDataAgeBetween15And29RequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetStudentDataAgeBetween15And29(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)

            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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

        #endregion


        [HttpPost("Get_ITIStudentjanaadharDetailReport")]
        public async Task<ApiResult<DataTable>> GetStudentjanaadharDetailReport([FromBody] StudentItiSearchModel model)
        {
            ActionName = "GetStudentjanaadharDetailReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetStudentjanaadharDetailReport(model));
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
                await _unitOfWork.DisposeAsync();
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

        #region Direct Admission Report

        [HttpPost("GetDirectAdmissionReport")]
        public async Task<ApiResult<DataSet>> GetDirectAdmissionReport([FromBody] DirectAdmissionReportRequestModel model)
        {
            ActionName = "GetDirectAdmissionReport([FromBody] DirectAdmissionReportRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetDirectAdmissionReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)

            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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

        #endregion

        #region IMC Allotment Report
        [HttpPost("GetIMCAllotmentReport")]
        public async Task<ApiResult<DataSet>> GetIMCAllotmentReport([FromBody] IMCAllotmnentReportRequestModel model)
        {
            ActionName = "GetIMCAllotmentReport([FromBody] IMCAllotmnentReportRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetIMCAllotmentReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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

        #endregion

        [HttpPost("Get_ITIInstitutejanaadharDetailReport")]
        public async Task<ApiResult<DataTable>> GetInstitutejanaadharDetailReport()
        {
            ActionName = "GetInstitutejanaadharDetailReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetInstitutejanaadharDetailReport());
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
                await _unitOfWork.DisposeAsync();
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


        [HttpGet("GetDropOutStudentListby_instituteID/{InstituteID}")]
        public async Task<ApiResult<DataTable>> GetDropOutStudentListbyinstituteID(int InstituteID = 0)
        {
            ActionName = "GetInstitutejanaadharDetailReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetDropOutStudentListbyinstituteID(InstituteID));
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
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("GetInternalSlidingForAdminReport")]
        public async Task<ApiResult<DataSet>> GetInternalSlidingForAdminReport([FromBody] InternalSlidingForAdminReport model)
        {
            ActionName = "GetInternalSlidingForAdminReport([FromBody] InternalSlidingForAdminReport model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetInternalSlidingForAdminReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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


        [HttpPost("GetSwappingForAdminReport")]
        public async Task<ApiResult<DataSet>> GetSwappingForAdminReport([FromBody] SwappingForAdminReport model)
        {
            ActionName = "GetSwappingForAdminReport([FromBody] SwappingForAdminReport model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetSwappingForAdminReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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


        [HttpPost("GetEstablishManagementStaffReport")]
        public async Task<ApiResult<DataTable>> GetEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model)
        {
            ActionName = "GetInstitutejanaadharDetailReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetEstablishManagementStaffReport(model));
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
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("GetBterStatisticsReport")]
        public async Task<ApiResult<DataTable>> GetBterStatisticsReport(BterStatisticsReportDataModel model)
        {
            ActionName = "GetBterStatisticsReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetBterStatisticsReport(model));
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
                await _unitOfWork.DisposeAsync();
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

        //[RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("GetBterBridgeCourseReport")]
        public async Task<ApiResult<string>> GetBterBridgeCourseReport([FromBody] BterStatisticsReportDataModel model)
        {
            ActionName = "GetBterBridgeCourseReport(BterStatisticsReportDataModel)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetBterBridgeCourseReport(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "BridgeCourse";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/BridgeCourse.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", "");

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        //[RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("GetMassCoppingReport")]
        public async Task<ApiResult<DataTable>> GetMassCoppingReport(BterStatisticsReportDataModel model)
        {
            ActionName = "GetMassCoppingReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetMassCoppingReport(model));
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
                await _unitOfWork.DisposeAsync();
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

        #region Download Institute Branch Wise Statistics
        [HttpPost("DownloadResultStatisticsBridgeCourseReport")]
        public async Task<ApiResult<string>> DownloadResultStatisticsBridgeCourseReport(StatisticsBridgeCourseModel model)
        {
            ActionName = "DownloadAppearedPassed(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.ResultStatisticsBridgeCourseReport(model);
                    if (data.Rows?.Count > 0)
                    {
                        //report
                        var fileName = $"ResultStatisticsBridgeCourseReport.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ResultStatisticsBridgeCourseReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("AppearedPassedStatistics", data);
                        localReport.AddDataSource("AppearedPassedDetails", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        [HttpPost("DownloadResultStatisticsReport")]
        public async Task<ApiResult<string>> DownloadResultStatisticsReport(StatisticsBridgeCourseModel model)
        {
            ActionName = "DownloadResultStatisticsReport(StatisticsBridgeCourseModel model)";
            var result = new ApiResult<string>();
            try
            {
                var data = new DataSet();
                if (model.ResultType == (int)EnumResultType.RwhResult || model.ResultType == (int)EnumResultType.RwhRevalEffected)
                {
                    data = await Task.Run(() => _unitOfWork.ReportRepository.DownloadResultStatisticsReportRWH(model));
                }
                else
                {
                    data = await Task.Run(() => _unitOfWork.ReportRepository.DownloadResultStatisticsReport(model));
                }
                //
                if (data.Tables.Count > 1 && data.Tables[0].Rows?.Count > 0)
                {
                    //report
                    var fileName = $"ResultStatisticsReports.pdf";
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ResultStatisticsReports.rdlc";

                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("ResultStatisticsReports", data.Tables[0]);
                    localReport.AddDataSource("ResultStatisticsReportsTotal", data.Tables[1]);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                }


            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }

        [HttpPost("ResultStatisticsBridgeCourseStreamWiseReport")]
        public async Task<ApiResult<string>> ResultStatisticsBridgeCourseStreamWiseReport(StatisticsBridgeCourseModel model)
        {
            ActionName = "DownloadAppearedPassed(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadResultStatisticsBridgeCourseStreamWiseReport(model);
                    if (data.Rows?.Count > 0)
                    {
                        //report
                        var fileName = $"ResultStatisticsBridgeCourseStreamWiseReport.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ResultStatisticsBridgeCourseStreamWiseReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("AppearedPassedStatistics", data);
                        localReport.AddDataSource("AppearedPassedDetails", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                    }


                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        // [RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("GetBterBranchWiseStatisticalReport")]
        public async Task<ApiResult<string>> GetBterBranchWiseStatisticalReport([FromBody] BterStatisticsReportDataModel model)
        {
            ActionName = "GetBterBranchWiseStatisticalReport(BterStatisticsReportDataModel)";

            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => _unitOfWork.ReportRepository.GetBterBranchWiseStatisticalReport(model));
                if (data?.Tables?.Count > 1 && data.Tables[0].Rows.Count > 0)
                {

                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    data.Tables[0].TableName = "BranchWiseStatistical";
                    data.Tables[1].TableName = "BranchWiseStatisticalHeading";



                    string lastBranchName = null;
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        string currentBranch = row["BranchName"]?.ToString();

                        if (currentBranch == lastBranchName || currentBranch == "Grand Total")
                        {
                            row["BranchName"] = ""; // Hide duplicate
                        }
                        else
                        {
                            lastBranchName = currentBranch;
                        }
                    }

                    string devFontSize = "15px";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/BranchWiseStatisticalReport.html";

                    string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                    System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                    html = Utility.PDFWorks.ReplaceCustomTag(html);

                    sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                    byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", " ");

                    result.Data = Convert.ToBase64String(pdfBytes);
                    result.State = EnumStatus.Success;
                    result.Message = "Success";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }

        #region College Information Report

        [HttpPost("GetCollegeInformationReport")]
        public async Task<ApiResult<DataTable>> GetCollegeInformationReport(CollegeInformationReportSearchModel model)
        {
            ActionName = "GetCollegeInformationReport(CollegeInformationReportSearchModel model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetCollegeInformationReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion

        #region EWS Report

        [HttpPost("GetEWSReport")]
        public async Task<ApiResult<DataTable>> GetEWSReport(EWSReportSearchModel model)
        {
            ActionName = "GetEWSReport(EWSReportSearchModel model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetEwsReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion

        #region UFM Student Report

        [HttpPost("GetUFMStudentReport")]
        public async Task<ApiResult<DataTable>> GetUFMStudentReport(UFMStudentReportSearchModel model)
        {

            ActionName = "GetUFMStudentReport(UFMStudentReportSearchModel model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetUFMStudentReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion

        #region Sessional Fail Student Report

        [HttpPost("GetSessionalFailStudentReport")]
        public async Task<ApiResult<DataTable>> GetSessionalFailStudentReport(GetSessionalFailStudentReport model)
        {

            ActionName = "GetSessionalFailStudentReport(GetSessionalFailStudentReport model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetSessionalFailStudentReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion


        [HttpPost("GetInstituteStudentReport")]
        public async Task<ApiResult<DataTable>> GetInstituteStudentReport(InstituteStudentReport model)
        {
            ActionName = "GetInstituteStudentReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetInstituteStudentReport(model));
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
                await _unitOfWork.DisposeAsync();
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


        #region RMI Fail Student Report

        [HttpPost("GetRMIFailStudentReport")]
        public async Task<ApiResult<DataTable>> GetRMIFailStudentReport(RMIFailStudentReport model)
        {

            ActionName = "GetRMIFailStudentReport(RMIFailStudentReport model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetRMIFailStudentReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion


        #region Relieving Letter Report
        [HttpPost("RelievingLetterReport")]
        public async Task<ApiResult<string>> RelievingLetterReport(RelievingLetterSearchModel model)
        {
            ActionName = "RelievingLetterReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.RelievingLetterReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"RelievingLetterReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RelievingReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("RelievingReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Apprenticeship  registratuion Fresher Report
        [HttpPost("ApprenticeshipFresherReport")]
        public async Task<ApiResult<string>> ApprenticeshipFresherReport(ApprenticeshipRegistrationSearchModal model)
        {
            ActionName = "ApprenticeshipFresherReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.ApprenticeshipFresherReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"ApprenticeshipFresherReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ApprenticeshipFresherReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ApprenticeshipReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        [HttpPost("ApprenticeshipPassoutReport")]
        public async Task<IActionResult> ApprenticeshipPassoutReport(ApprenticeshipRegistrationSearchModal model)
        {
            try
            {
                DataSet ds = await Task.Run(() => _unitOfWork.ReportRepository.ApprenticeshipPassoutReport(model));

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return BadRequest("No record found.");

                DataTable dt = ds.Tables[0];

                StringBuilder sb = new StringBuilder();

                sb.Append(@"
<!DOCTYPE html>
<html>
<head>
<meta charset='UTF-8'>

<style>
@page{
    size:A4 landscape;
    margin:12mm;
}

*{
    margin:0;
    padding:0;
    box-sizing:border-box;
}
body{
    font-family:'Nirmala UI','Mangal','Segoe UI',Arial,sans-serif;
    font-size:12px;
    color:#000;
}

.formNo{
    font-size:18px;
    font-weight:bold;
    text-align:right;
    border:none !important;
}

.subTitle{
    font-size:18px;
    font-weight:bold;
    text-align:center;
    padding:8px;
}
.title{
    font-size:24px;
    font-weight:bold;
    text-align:left;
    border:none !important;
    padding-bottom:6px;
}
.report-title{
    font-family:""Segoe UI"",Arial,sans-serif;
    text-align:center;
    font-size:26px;
    font-weight:700;
    margin-bottom:4px;
}

.report-subtitle{
    font-family:""Nirmala UI"",""Mangal"",sans-serif;
    text-align:center;
    font-size:18px;
    font-weight:700;
    margin-bottom:15px;
}




table,th,td{
    border:1px solid #000;
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

th,td{
    border:1px solid #000;
}

th{
    background:#f2f2f2;
    font-weight:bold;
    text-align:center;
    vertical-align:middle;
    padding:6px 4px;
    font-size:11px;
    line-height:18px;
}

td{
    padding:5px;
    vertical-align:top;
    font-size:10px;
    word-wrap:break-word;
}

.numberRow th{
    font-size:12px;
    padding:3px;
}

.left{
    text-align:left;
}

.center{
    text-align:center;
}

.small{
    font-size:9px;
}

.col1{width:4%;}
.col2{width:15%;}
.col3{width:8%;}
.col4{width:8%;}
.col5{width:13%;}
.col6{width:9%;}
.col7{width:9%;}
.col8{width:14%;}
.col9{width:6%;}
.col10{width:7%;}
.col11{width:9%;}

</style>

</head>

<body>

<div class=""container"">

<table>

<thead>

<tr>

<th colspan=""10"" class=""title""
style=""border:none !important;
text-align:left;
font-size:24px;
padding-bottom:8px;"">

Apprenticeship Registration (ITI Pass Out)

</th>

<th class=""formNo""
style=""border:none !important;
text-align:right;
font-size:18px;"">

(प्रपत्र-ट)

</th>

</tr>

<tr>

<th colspan=""11""
style=""
font-size:18px;
font-weight:bold;
text-align:center;
padding:10px;
border:1px solid #000;"">

आईटीआई पासआउट के पंजीकरण की सूची

</th>

</tr>

<tr>

<th class=""col1"">
S.No.
</th>

<th class=""col2"">
पंजीकरण करने वाले<br/>
संस्थान का नाम
</th>

<th class=""col3"">
पोर्टल पर पंजीकरण<br/>
करने की तिथि
</th>

<th class=""col4"">
पंजीकरण संख्या
</th>

<th class=""col5"">
नाम / पिता का नाम
</th>

<th class=""col6"">
आधार नम्बर
</th>

<th class=""col7"">
व्यवसाय का नाम
</th>

<th class=""col8"">
संस्थान, जहाँ से<br/>
आईटीआई उत्तीर्ण की है
</th>

<th class=""col9"">
आईटीआई उत्तीर्ण<br/>
करने का वर्ष
</th>

<th class=""col10"">
NCVT / SCVT
</th>

<th class=""col11"">
विशेष विवरण
</th>

</tr>

<tr class=""numberRow"">

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

<th>11</th>

</tr>

</thead>

<tbody>");

                int sr = 1;

                foreach (DataRow row in dt.Rows)
                {
                    string regDate = "";

                    if (row["RegDate"] != DBNull.Value)
                        regDate = Convert.ToDateTime(row["RegDate"]).ToString("dd-MM-yyyy");

                    sb.Append($@"

<tr>

<td class='center'>
{sr}
</td>

<td>
{row["Name"]}
</td>

<td class='center'>
{regDate}
</td>

<td class='center'>
{row["RegCount"]}
</td>

<td>
<b>{row["StudentName"]}</b><br/>
Father : {row["FatherName"]}
</td>

<td class='center'>
{row["Aadhar"]}
</td>

<td>
{row["TradeName"]}
</td>

<td>
{row["PassItiName"]}
</td>

<td class='center'>
{row["PassYear"]}
</td>

<td class='center'>
{row["TradeSchemeName"]}
</td>

<td>
{row["Remarks"]}
</td>

</tr>

");

                    sr++;
                }

                sb.Append(@"

</tbody>

</table>

</body>

</html>");

                var pdf = new HtmlToPdfDocument
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Landscape,
                        PaperSize = PaperKind.A4,
                        DPI = 300,
                        DocumentTitle = "Apprenticeship Passout Report",

                        Margins = new MarginSettings
                        {
                            Top = 12,
                            Bottom = 12,
                            Left = 8,
                            Right = 8
                        }
                    },

                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = sb.ToString(),

                    PagesCount = true,

                    WebSettings =
                    {
                        DefaultEncoding = "utf-8",
                        LoadImages = true,
                        PrintMediaType = true
                    },

                    FooterSettings =
                    {
                        FontName = "Arial",
                        FontSize = 8,
                        Left = "Printed On : [date]",
                        Right = "Page [page] of [toPage]",
                        Line = true,
                        Spacing = 3
                    }
                }
            }
                };

                byte[] pdfBytes = _converter.Convert(pdf);

                return File(
                    pdfBytes,
                    "application/pdf",
                    "ApprenticeshipPassoutReport.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region Quarterly Progress Report
        [HttpPost("QuarterlyProgressReport")]
        public async Task<ApiResult<string>> QuarterlyProgressReport(ITIApprenticeshipWorkshop model)
        {
            ActionName = "QuarterlyProgressReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.QuarterlyProgressReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"QuarterlyProgressReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/QuarterlyProgressReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("QuarterlyProgressReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Apprenticeship  registratuion List Report
        [HttpPost("ApprenticeshipReport")]
        public async Task<IActionResult> ApprenticeshipReport(ApprenticeshipRegistrationSearchModal model)
        {
            try
            {
                DataSet ds = await Task.Run(() => _unitOfWork.ReportRepository.ApprenticeshipReport(model));

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return BadRequest("No record found.");

                DataTable dt = ds.Tables[0];

                StringBuilder sb = new StringBuilder();

                sb.Append(@"
<!DOCTYPE html>

<html>

<head>

<meta charset='UTF-8'>

<style>

@page{
    size:A4 landscape;
    margin:12mm;
}

*{
    margin:0;
    padding:0;
    box-sizing:border-box;
}

body{
    font-family:'Nirmala UI','Mangal','Segoe UI',Arial,sans-serif;
    font-size:12px;
    color:#000;
    line-height:1.35;
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

th,td{
    border:1px solid #000;
}

th{
    background:#f2f2f2;
    text-align:center;
    vertical-align:middle;
    padding:6px 4px;
    font-size:11px;
    font-weight:bold;
    line-height:17px;
}

td{
    padding:5px;
    font-size:10px;
    vertical-align:top;
    word-break:break-word;
}

.title{
    border:none !important;
    text-align:left;
    font-size:24px;
    font-weight:bold;
    padding-bottom:8px;
}

.formNo{
    border:none !important;
    text-align:right;
    font-size:18px;
    font-weight:bold;
}

.subTitle{
    font-size:18px;
    font-weight:bold;
    text-align:center;
    padding:10px;
}

.numberRow th{
    padding:3px;
    font-size:11px;
}

.left{
    text-align:left;
}

.center{
    text-align:center;
}

.small{
    font-size:9px;
}

/* Column Width */

.col1{width:4%;}
.col2{width:8%;}
.col3{width:16%;}
.col4{width:8%;}
.col5{width:10%;}
.col6{width:8%;}
.col7{width:12%;}
.col8{width:12%;}
.col9{width:8%;}
.col10{width:8%;}
.col11{width:8%;}
.col12{width:6%;}

</style>

</head>

<body>

<div class='container'>

<table>

<thead>

<tr>

<th colspan=""10"" class=""title"">

Apprenticeship Registration Report

</th>

<th class='formNo'>

(प्रपत्र-ट)

</th>

</tr>

<tr>

<th colspan=""11"" class=""subTitle"">

शिक्षुओं के पोर्टल पंजीकरण की प्रगति रिपोर्ट

</th>

</tr>
"); sb.Append(@"

<tr>

<th rowspan='2' class='col1'>
Sr.<br/>No.
</th>

<th rowspan='2' class='col2'>
आवृत्ति
</th>

<th rowspan='2' class='col3'>
संस्थान का नाम
</th>

<th rowspan='2' class='col4'>
पोर्टल पर<br/>
पंजीकरण करने<br/>
की तिथि
</th>

<th rowspan='2' class='col5'>
व्यवसाय का नाम
</th>

<th rowspan='2' class='col6'>
व्यवसाय मे कुल<br/>
प्रशिक्षणार्थियों<br/>
की संख्या
</th>

<th colspan='2' class='col7'>
पोर्टल पर पंजीकृत किये शिक्षुओं का नाम व पंजीकरण संख्या
</th>

<th rowspan='2' class='col10'>
दस्तावेज़
</th>

<th rowspan='2' class='col11'>
विविध विवरण
</th>

<th rowspan='2' class='col12'>
कार्रवाई
</th>

</tr>

<tr>

<th style='width:16%;'>
नाम
</th>

<th style='width:12%;'>
पंजीकरण संख्या
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

"); int sr = 1;

                foreach (DataRow row in dt.Rows)
                {
                    string regDate = "";
                    string apprenticeNames = "";
                    string registrationNos = "";
                    string document = "";

                    if (row["Dateofregistration"] != DBNull.Value)
                    {
                        regDate = Convert.ToDateTime(row["Dateofregistration"])
                            .ToString("dd-MM-yyyy");
                    }

                    if (row["Nameofapprentices"] != DBNull.Value)
                    {
                        apprenticeNames = row["Nameofapprentices"]
                            .ToString()
                            .Replace("|", "<br/>")
                            .Replace(",", "<br/>");
                    }

                    if (row["Numberofapprentices"] != DBNull.Value)
                    {
                        registrationNos = row["Numberofapprentices"]
                            .ToString()
                            .Replace("|", "<br/>")
                            .Replace(",", "<br/>");
                    }



                    sb.Append($@"

<tr>

<td class='center'>
{sr}
</td>

<td class='center'>
{row["TypeName"]}
</td>

<td class='left'>
{row["Nameofinstitute"]}
</td>

<td class='center'>
{regDate}
</td>

<td class='left'>
{row["BusinessName"]}
</td>

<td class='center'>
{row["NumberofTrainees"]}
</td>

<td class='left'>
{apprenticeNames}
</td>

<td class='left'>
{registrationNos}
</td>


<td class='left'>
{row["Remarks"]}
</td>

<td class='center'>
-
</td>

</tr>

");

                    sr++;
                }// Close HTML
                sb.Append(@"

</tbody>

</table>

</div>

</body>

</html>

");

                // Generate PDF
                var pdf = new HtmlToPdfDocument
                {
                    GlobalSettings = new GlobalSettings
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Landscape,
                        PaperSize = PaperKind.A4,
                        DPI = 300,
                        DocumentTitle = "Apprenticeship Registration Report",

                        Margins = new MarginSettings
                        {
                            Top = 12,
                            Bottom = 12,
                            Left = 8,
                            Right = 8
                        }
                    },

                    Objects =
    {
        new ObjectSettings
        {
            HtmlContent = sb.ToString(),

            PagesCount = true,

            WebSettings =
            {
                DefaultEncoding = "utf-8",
                LoadImages = true,
                PrintMediaType = true
            },

            HeaderSettings =
            {
                FontName = "Arial",
                FontSize = 8,
                Line = false,
                Spacing = 3
            },

            FooterSettings =
            {
                FontName = "Arial",
                FontSize = 8,
                Left = "Printed On : [date]",
                Center = "",
                Right = "Page [page] of [toPage]",
                Line = true,
                Spacing = 3
            }
        }
    }
                };

                byte[] pdfBytes = _converter.Convert(pdf);

                // Return PDF directly
                return File(
                    pdfBytes,
                    "application/pdf",
                    "ApprenticeshipReport.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region Workshop Progress Report
        [HttpPost("WorkshopProgressReport")]
        public async Task<ApiResult<string>> WorkshopProgressReport(WorkshopProgressRPTSearchModal model)
        {
            ActionName = "WorkshopProgressReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.WorkshopProgressReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"WorkshopReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/WorkshopProgressReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("WorkshopReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region PMNAM Mela Report
        [HttpPost("PmnamMelaReport")]
        public async Task<ApiResult<string>> PmnamMelaReport([FromBody] ITIPMNAM_Report_SearchModal body)
        {
            ActionName = "PmnamMelaReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.PmnamMelaReport(body);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"PmnamMelaReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/PmnamMelaReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("PmnamMelaReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        [HttpPost("PmnamMelaReportnodelOfficer")]
        public async Task<ApiResult<string>> PmnamMelaReportnodelOfficer([FromBody] ITIPMNAM_Report_SearchModal body)
        {
            ActionName = "PmnamMelaReportnodelOfficer()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.PmnamMelaReportnodelOfficer(body);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"PmnamMelaReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/PmnamMelaReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("PmnamMelaReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Mela Report
        [HttpPost("MelaReport")]
        public async Task<ApiResult<string>> MelaReport(ITIPMNAM_Report_SearchModal model)
        {
            ActionName = "MelaReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.MelaReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"MelaReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/MelaReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("MelaReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Reval Dispatch Group Details Receipt
        [HttpGet("GetRevalDispatchGroupDetails/{ID}/{EndTermID}/{CourseTypeID}")]
        public async Task<ApiResult<string>> GetRevalDispatchGroupDetails(int ID, int EndTermID, int CourseTypeID)
        {
            ActionName = "GetRevalDispatchGroupDetails(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetRevalDispatchGroupDetails(ID, EndTermID, CourseTypeID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"RevalDispatchGroupDetails_{ID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RevalDispatch_GroupList.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Dispatch_Bundle", data.Tables[0]);
                        localReport.AddDataSource("Dispatch_Bundle_Table", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;



                        //bool Issuccess = await _unitOfWork.DispatchRepository.UpdateDownloadFileDispatchMaster(fileName, ID);
                        //if (Issuccess)
                        //{
                        //    result.Data = fileName;
                        //    result.State = EnumStatus.Success;
                        //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.State = EnumStatus.Warning;
                        //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                        //}


                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }



                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Reval Dispatch Group Details Certificate
        [HttpGet("DownloadRevalDispatchGroupCertificate/{ID}/{StaffID}/{DepartmentID}")]
        public async Task<ApiResult<string>> DownloadRevalDispatchGroupCertificate(int ID, int StaffID, int DepartmentID)
        {
            ActionName = "DownloadRevalDispatchGroupCertificate(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadRevalDispatchGroupCertificate(ID, StaffID, DepartmentID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"RevalDispatchGroupCertificate_{ID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RevalDispatch_Undertacking.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Dispatch_Undertaking", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;




                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }



                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion


        #region Theory Fail Student Report

        [HttpPost("GetTheoryFailStudentReport")]
        public async Task<ApiResult<DataTable>> GetTheoryFailStudentReport(TheoryFailStudentReport model)
        {

            ActionName = "GetTheoryFailStudentReport(TheoryFailStudentReport model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetTheoryFailStudentReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion

        #region ITI Allotment Report
        [HttpPost("GetITIAllotmentReport")]
        public async Task<ApiResult<DataSet>> GetITIAllotmentReport([FromBody] IMCAllotmnentReportRequestModel model)
        {
            ActionName = "GetITIAllotmentReport([FromBody] IMCAllotmnentReportRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIAllotmentReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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

        #endregion

        #region Revaluation Student Detail Report

        [HttpPost("GetRevaluationStudentDetailReport")]
        public async Task<ApiResult<DataTable>> GetRevaluationStudentDetailReport(RevaluationStudentDetailReport model)
        {

            ActionName = "GetRevaluationStudentDetailReport(RevaluationStudentDetailReport model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetRevaluationStudentDetailsReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion

        #region Center Superintendent Attendance Report
        [RoleActionFilter(EnumRole.ACP, EnumRole.ACP_NonEng, EnumRole.JDConfidential_Eng, EnumRole.JDConfidential_NonEng, EnumRole.Registrar, EnumRole.Registrar_NonEng)]

        [HttpPost("GetCenterSuperintendentAttendanceReport")]
        public async Task<ApiResult<DataTable>> GetCenterSuperintendentAttendanceReport(searchCenterSuperintendentAttendance model)
        {

            ActionName = "GetCenterSuperinstendentAttendanceReport(RevaluationStudentDetailReport model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetCenterSuperinstendentAttendanceReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion


        #region Student Examiner Detail Report

        [HttpPost("GetStudentExaminerDetailReport")]
        public async Task<ApiResult<DataTable>> GetStudentExaminerDetailReport(StudentExaminerDetailReport model)
        {

            ActionName = "GetStudentExaminerDetailReport(StudentExaminerDetailReport model)";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetStudentExaminerDetailsReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Error log
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

        #endregion

        //StudentSeatAllotment

        [HttpPost("GetITIStudentSeatAllotmentDataList")]
        public async Task<ApiResult<DataSet>> GetITIStudentSeatAllotmentDataList(ITIAddmissionWomenReportSearchModel model)
        {
            ActionName = "GetITIStudentSeatAllotmentDataList([FromBody] ITIAddmissionWomenReportSearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIStudentSeatAllotmentDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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

        //Withdraw Report
        [HttpPost("GetITIStudentSeatWithdrawDataList")]
        public async Task<ApiResult<DataSet>> GetITIStudentSeatWithdrawDataList(ITIAddmissionWomenReportSearchModel model)
        {
            ActionName = "GetITIStudentSeatWithdrawDataList([FromBody] ITIAddmissionWomenReportSearchModel body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIStudentSeatWithdrawDataList(model));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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

        //[HttpPost("GetStudentFailTheoryReport")]
        //public async Task<ApiResult<DataSet>> GetStudentFailTheoryReport([FromBody] StudentFailTheoryReportModel model)
        //{
        //    ActionName = "GetStudentFailTheoryReport([FromBody] StudentFailTheoryReportModel model)";
        //    var result = new ApiResult<DataSet>();

        //    try
        //    {
        //        result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetStudentFailTheoryReport(model));
        //        result.State = EnumStatus.Success;

        //        if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
        //        {
        //            result.Message = "No record found.!";
        //            return result;
        //        }

        //        result.Message = "Data loaded successfully.!";
        //    }
        //    catch (Exception ex)
        //    {
        //        await _unitOfWork.DisposeAsync();
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = ex.Message;

        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }

        //    return result;
        //}


        [HttpPost("GetITIEstablishManagementStaffReport")]
        public async Task<ApiResult<DataTable>> GetITIEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model)
        {
            ActionName = "GetInstitutejanaadharDetailReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITIEstablishManagementStaffReport(model));
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetBterDuplicateCertificateReport")]
        public async Task<ApiResult<DataTable>> GetBterDuplicateCertificateReport([FromBody] BterCertificateReportDataModel body)
        {
            ActionName = "GetStudentEnrollmentReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetBterDuplicateCertificateReport(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #region ITI Allotted & Reported Count
        [HttpPost("GetAllottedAndReportedCountByITI")]
        public async Task<ApiResult<DataSet>> GetAllottedAndReportedCountByITI([FromBody] AllottedReportedRequestModel model)
        {
            ActionName = "GetAllottedAndReportedCountByITI([FromBody] AllottedReportedRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetAllottedAndReportedCountByITI(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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
        #endregion

        #region Bulk Student Marksheet 
        [HttpPost("GetStudentMarksheetBulk")]
        public async Task<ApiResult<string>> GetStudentMarksheetBulk([FromBody] List<MarksheetDownloadSearchModel> Model)
        {
            ActionName = "GetStudentMarksheetBulk([FromBody] List<MarksheetDownloadSearchModel> Model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    List<GenerateMarksheetModel> ListData = new List<GenerateMarksheetModel>();
                    foreach (var student in Model)
                    {
                        GenerateMarksheetModel objStudent = new GenerateMarksheetModel();
                        var data = await _unitOfWork.ReportRepository.GetStudentMarksheet(student);
                        if (data?.Tables?.Count == 3)
                        {
                            var fileName = $"StudentMarksheet_{student.StudentID}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentMarksheet.rdlc";

                            #region "Add Object"
                            objStudent.StudentID = student.StudentID;
                            objStudent.MarksheetPath = filepath;
                            objStudent.MarksheetFile = fileName;
                            ListData.Add(objStudent);
                            #endregion

                            student.MarksheetPath = filepath;
                            student.Marksheet = fileName;

                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("StudentDetailsForMarksheet", data.Tables[0]);
                            localReport.AddDataSource("StudentMarksheetSubjectDetails", data.Tables[1]);
                            localReport.AddDataSource("ResultDetails", data.Tables[2]);
                            var reportResult = localReport.Execute(RenderType.Pdf);

                            //check file exists
                            if (!System.IO.Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            //save
                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                            //result.Data = fileName;
                            //result.State = EnumStatus.Success;
                            //result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }
                    }
                    #region "Save Multiple PDF PAGES"
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string outputFile = $"Marksheet_{timestamp}.pdf";
                    string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                    List<string?> strSoureFiles = ListData.Select(s => s.MarksheetPath).ToList();
                    if (await MergePdfFilesAsync(strSoureFiles, outputPath))
                    {
                        result.Data = outputFile;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;

            });
        }
        #endregion

        #region Student Duplicate Marksheet
        [HttpPost("GetStudentDuplicateMarksheet")]
        public async Task<ApiResult<string>> GetStudentDuplicateMarksheet([FromBody] MarksheetDownloadSearchModel student)
        {
            ActionName = "GetStudentDuplicateMarksheet([FromBody] MarksheetDownloadSearchModel student)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/BTER/DuplicateDocument";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    if (student.DocumentID.HasValue && student.DocumentID.Value == (int)EnumDuplicateDocumentType.Duplicate_Marksheet)
                    {

                        var data = await _unitOfWork.ReportRepository.GetStudentDuplicateMarksheet(student);
                        //if (!string.IsNullOrWhiteSpace(data.Tables[0].MarksheetFile))
                        //{
                        //    string physicalPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, data.Tables[0].MarksheetPath.TrimStart('/', '\\'));

                        //    if (System.IO.File.Exists(physicalPath))
                        //    {
                        //        result.Data = data.Tables[0].MarksheetFile;   // File name
                        //        result.State = EnumStatus.Success;
                        //        result.Message = "Duplicate marksheet already generated.";

                        //        return result;
                        //    }
                        //}

                        if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                        {
                            string marksheetFile = Convert.ToString(data.Tables[0].Rows[0]["MarksheetFile"]);
                            string marksheetPath = Convert.ToString(data.Tables[0].Rows[0]["MarksheetPath"]);
                            if (!string.IsNullOrWhiteSpace(marksheetPath))
                            {
                                string physicalPath = Path.Combine(
                                    ConfigurationHelper.StaticFileRootPath,
                                    marksheetPath.TrimStart('/', '\\'));
                                if (System.IO.File.Exists(physicalPath))
                                {
                                    result.Data = marksheetFile;
                                    result.State = EnumStatus.Success;
                                    result.Message = "Duplicate marksheet already generated.";
                                    return result;
                                }
                            }
                        }

                        if (data?.Tables?.Count == 3)
                        {
                            //report
                            string timestamp_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                            var fileName = $"StudentMarksheet_{student.StudentID}_{timestamp_str}.pdf";
                            //var fileName = $"StudentDuplicateMarksheet_{student.StudentID}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/BTER/DuplicateDocument/{fileName}";
                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentMarksheet.rdlc";

                            student.MarksheetPath = filepath;
                            student.Marksheet = fileName;
                            //provider                      
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                            LocalReport localReport = new LocalReport(rdlcpath);
                            localReport.AddDataSource("StudentDetailsForMarksheet", data.Tables[0]);
                            localReport.AddDataSource("StudentMarksheetSubjectDetails", data.Tables[1]);
                            localReport.AddDataSource("ResultDetails", data.Tables[2]);
                            var reportResult = localReport.Execute(RenderType.Pdf);

                            //check file exists
                            if (!System.IO.Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            //save
                            System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                            string relativePath = $"{Constants.ReportsFolder}/BTER/DuplicateDocument/{fileName}";
                            // Save generated file path in database
                            int res = await _unitOfWork.ApplyDuplicateDocumentRepository.UpdateDuplicateMarksheetPath(
                                 student.ReqId.Value, // Request ID
                                 relativePath,
                                 fileName,
                                 "_updateMarksheetPath"
                             );
                            await _unitOfWork.SaveChangesAsync();

                            if (res > 0)
                            {
                                result.Data = fileName;
                                result.State = EnumStatus.Success;
                                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                            }
                            //end report
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Student Duplicate document Certificate download
        [HttpPost("BterDuplicateCertificateDownload")]
        public async Task<ApiResult<string>> BterDuplicateCertificateDownload([FromBody] BterDuplicateCertificateReportDataModel model)
        {
            ActionName = "BterDuplicateCertificateDownload([FromBody] BterCertificateReportDataModel model)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    string htmlTemplatePath = "";
                    string devFontSize = "15px";
                    if (model.Action == "duplicate-provisional-certificate")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ProvisionalCertificate.html";
                        //model.Action = "duplicate-provisional-certificate-download";
                    }
                    if (model.Action == "duplicate-migration-certificate")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/MigrationCertificate.html";
                        //model.Action = "duplicate-migration-certificate-download";
                    }
                    //if (model.Action == "duplicate-diploma-report")
                    //{
                    //    devFontSize = "20px";
                    //    htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/DiplomaReport.html";
                    //   // model.Action = "diploma-report-download";
                    //}

                    var data = await _unitOfWork.ReportRepository.BterDuplicateCertificateDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "BterCertificate";

                        data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/BTER-logo-black.jpg";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/BTER-logo-black.jpg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }

        #endregion

        #region Duplicate Diploma Certificate
        [HttpPost("GetDuplicateDiplomaCertificate")]
        public async Task<ApiResult<string>> GetDuplicateDiplomaCertificate(BterDuplicateCertificateReportDataModel model)
        {
            ActionName = "GetDuplicateDiplomaCertificate()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.BterDuplicateCertificateDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        string DocumentFilename = Convert.ToString(data.Tables[0].Rows[0]["DocumentFilename"]);
                        string DocumentPath = Convert.ToString(data.Tables[0].Rows[0]["DocumentPath"]);
                        if (!string.IsNullOrWhiteSpace(DocumentPath))
                        {
                            string physicalPath = Path.Combine(
                                ConfigurationHelper.StaticFileRootPath,
                                DocumentPath.TrimStart('/', '\\'));
                            if (System.IO.File.Exists(physicalPath))
                            {
                                result.Data = DocumentFilename;
                                result.State = EnumStatus.Success;
                                result.Message = "Duplicate Diploma already generated.";
                                return result;
                            }
                        }
                    }

                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/BTER/DuplicateDocument";
                        string timestamp_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        var fileName = $"DiplomaCertificate_{model.StudentID}_{timestamp_str}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/BTER/DuplicateDocument/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/DiplomaCertificate.rdlc";
                        //var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("DiplomaCertificate", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                        string relativePath = $"{Constants.ReportsFolder}/BTER/DuplicateDocument/{fileName}";
                        // Save generated file path in database
                        int res = await _unitOfWork.ApplyDuplicateDocumentRepository.UpdateDuplicateMarksheetPath(
                             model.ReqId.Value, // Request ID
                             relativePath,
                             fileName,
                             "_updateDuplicateDiplomaPath"
                         );
                        await _unitOfWork.SaveChangesAsync();

                        if (res > 0)
                        {
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        #region Student Duplicate Provisional Certificate
        [HttpPost("BterDuplicateProvisionalCertificateDownload")]
        public async Task<ApiResult<string>> BterDuplicateProvisionalCertificateDownload([FromBody] BterCertificateReportDataModel model)
        {
            ActionName = "BterDuplicateProvisionalCertificateDownload([FromBody] BterCertificateReportDataModel model)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    string htmlTemplatePath = "";
                    string devFontSize = "15px";
                    if (model.Action == "duplicate-provisional-certificate")
                    {
                        devFontSize = "20px";
                        htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ProvisionalCertificate.html";
                        model.Action = "provisional-certificate-download";
                    }


                    var data = await _unitOfWork.ReportRepository.BterDuplicateProvisionalCertificateDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "BterCertificate";

                        data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/BTER-logo-black.jpg";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/BTER-logo-black.jpg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        [HttpPost("GetCentarlSupridententDistrictReportDataListReport")]
        public async Task<ApiResult<DataSet>> GetCentarlSupridententDistrictReportDataListReport([FromBody] CentarlSupridententDistrictRequestModel model)
        {
            ActionName = "GetCentarlSupridententDistrictReportDataListReport([FromBody] CentarlSupridententDistrictRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetCentarlSupridententDistrictReportDataListReport(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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



        [HttpPost("GetApplicantReportForAdmin")]
        public async Task<ApiResult<DataSet>> GetApplicantReportForAdmin([FromBody] ApplicantStudentReport body)
        {
            ActionName = "GetApplicantReportForAdmin([FromBody] ApplicantStudentReport body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetApplicantReportForAdmin(body));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("ReportedStudentReport")]
        public async Task<ApiResult<DataSet>> ReportedStudentReport([FromBody] ReportedStudentReport body)
        {
            ActionName = "ReportedStudentReport([FromBody] ReportedStudentReport body)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.ReportedStudentReport(body));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count == 0)
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
                await _unitOfWork.DisposeAsync();
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



        //tabluation umesh
        [HttpPost("TabulationDataReport")]
        public async Task<ApiResult<string>> TabulationDataReport([FromBody] TabluationDataModel body)
        {
            ActionName = "TabulationDataReport([FromBody] TabluationDataModel body)";
            var result = new ApiResult<string>();
            try
            {
                // check for principal role has publish 
                var hasPublishBody = new HasResultPublishModel
                {
                    RoleID = body.RoleID,
                    ResultTypeId = body.ResultTypeId,
                    SemesterID = body.SemesterID,
                    EndTermID = body.EndTermID,
                    Eng_NonEng = body.Eng_NonEng,
                    DepartmentID = body.DepartmentID,
                    SchemeID = body.SchemeID,
                    EffectiveEndTermId = body.EffectiveFromEndTermId
                };
                var resultPublishModel = await Task.Run(() => _unitOfWork.CommonFunctionRepository.HasResultPublishedForRoleAndOtherInfo(hasPublishBody));
                if (resultPublishModel.ResultPublished == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = "Result not publish yet!";
                    return result;
                }

                // get all streams 
                var streams_data = await Task.Run(() => _unitOfWork.ReportRepository.GetStreamResultRptTabulation(body));

                if (streams_data?.Rows?.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                // main
                StringBuilder sb = new StringBuilder();

                // start
                sb.AppendLine("<!DOCTYPE html>");
                sb.AppendLine("<html lang=\"en\">");
                sb.AppendLine("<head>");
                sb.AppendLine("    <meta charset=\"UTF-8\">");
                sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                sb.AppendLine("    <title>Tabulation Register</title>");
                sb.AppendLine("</head>");
                sb.AppendLine("<style>");
                sb.AppendLine(".page-break {page-break-after: always; }");
                sb.AppendLine("</style>");
                sb.AppendLine("<body>");
                sb.AppendLine("    <div style=\"width: 98%; margin: auto;\">");


                DataTable heading_data = new DataTable();
                // all streams loop 1 by 1
                foreach (DataRow dr in streams_data.Rows)
                {
                    // set streamid
                    body.StreamID = Convert.ToInt32(dr["StreamID"] ?? 0);

                    // get main heading of report
                    heading_data = await Task.Run(() => _unitOfWork.ReportRepository.GetHeadingResultRptTabulation(body));
                    if (heading_data?.Rows.Count == 0)
                    {
                        continue;
                    }

                    // get tabular details
                    var tabular_data = new DataSet();
                    if (body.ResultTypeId == (int)EnumResultType.MainResult)
                    {
                        tabular_data = await Task.Run(() => _unitOfWork.ReportRepository.GetTabularDetailsResultRptTabulation(body));
                        //tabular_data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.Dummy_GetTestUspDataByAction("_get_data_to_test"));
                    }
                    else if (body.ResultTypeId == (int)EnumResultType.RwhResult || body.ResultTypeId == (int)EnumResultType.RwhRevalEffected)
                    {
                        tabular_data = await Task.Run(() => _unitOfWork.ReportRepository.GetTabularDetailsResultRptTabulationRWH(body));
                    }
                    else if (body.ResultTypeId == (int)EnumResultType.RevaluationResult)
                    {
                        tabular_data = await Task.Run(() => _unitOfWork.ReportRepository.GetTabularDetailsResultRptTabulationReval(body));
                    }
                    else if (body.ResultTypeId == (int)EnumResultType.Ufm)
                    {
                        tabular_data = await Task.Run(() => _unitOfWork.ReportRepository.GetTabularDetailsResultRptTabulationufm(body));
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_INVALID_REQUEST;
                        return result;
                    }

                    int headerRowBlockCount = 5;// get only top header 
                    if (tabular_data?.Tables?.Count < 2 || tabular_data?.Tables[0]?.Rows.Count == 0 || tabular_data?.Tables[0]?.Rows.Count == headerRowBlockCount)
                    {
                        continue;
                    }

                    // get detail html
                    var _sb = _printHtmlFile.GetHtmlOfHeadingAndTabularForTabulation(dr, heading_data, tabular_data, resultPublishModel, body);
                    sb.AppendJoin("</br>", _sb);
                }
                // end stream loop


                // get consolidate summary of tabular details
                var consolidate_data = await Task.Run(() => _unitOfWork.ReportRepository.GetConsolidatedDetailsResultRptTabulation(body));
                if (consolidate_data?.Rows.Count > 0)
                {
                    //get html
                    var _sb1 = _printHtmlFile.GetHtmlOfConsolidateForTabulation(consolidate_data, heading_data, resultPublishModel, body);
                    sb.AppendJoin("</br>", _sb1);
                }

                // end main
                sb.AppendLine("    </div>");
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");


                var htmlContent = sb.ToString();// all contents
                // remove last blank page
                string endTag = "<div class='page-break'></div></body></html>";
                if (htmlContent.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    htmlContent = htmlContent.Substring(0, htmlContent.Length - endTag.Length)
                                 + "</body></html>";
                }

                // page setting
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings = {
                        PaperSize = PaperKind.A3,
                        Orientation = Orientation.Landscape
                    },
                    Objects = {
                        new ObjectSettings()
                        {
                            HtmlContent = htmlContent,
                            WebSettings = {
                                DefaultEncoding = "utf-8"
                            },
                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = false,
                                //Spacing = 1
                            }
                        }
                    }
                };


                byte[] pdfBytes = await Task.Run(() => _converter.Convert(doc));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                //return File(pdfBytes, "application/pdf", "tabulationresult.pdf");
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //return StatusCode(500, ex.Message);
            }
            return result;
        }



        [HttpPost("GetstudentWithdrawnList")]
        public async Task<ApiResult<DataSet>> GetstudentWithdrawnList([FromBody] AllotmentReportCollegeRequestModel model)
        {
            ActionName = "GetstudentWithdrawnList([FromBody] AllotmentReportCollegeRequestModel model)";
            var result = new ApiResult<DataSet>();

            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetstudentWithdrawnList(model));
                result.State = EnumStatus.Success;

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                result.Message = "Data loaded successfully.!";
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }


        [HttpPost("GetTimeTableInWord")]
        public async Task<ApiResult<string>> GetTimeTableInWord(ReportBaseModel model)
        {
            ActionName = "GetTimeTableInWord(ReportBaseModel model)";
            var result = new ApiResult<string>();
            try
            {
                StringBuilder sb = new StringBuilder();

                // get time table data
                List<TimeTableHeaderModel> objList = new List<TimeTableHeaderModel>();
                model.Action = "_GetTimeTableHeader";
                var dataList = await _unitOfWork.ReportRepository.DownloadTimeTable(model);

                if (dataList != null)
                {
                    objList = CommonFuncationHelper.ConvertDataTable<List<TimeTableHeaderModel>>(dataList.Tables[0]);
                }

                if (objList?.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                // table loop
                if (objList.Count > 0)
                {
                    List<string> Timettable = new List<string>();
                    int loopIndex = 1;
                    foreach (var item in objList)
                    {
                        ReportBaseModel objTimeTableList = new ReportBaseModel
                        {
                            Action = "_TimeTableList",
                            SemesterID = item.SemesterID,
                            EndTermID = item.EndTermID,
                            ExamType = model.ExamType,
                            Eng_NonEng = model.Eng_NonEng,
                            CommonSubjectText = item.CommonSubjectText
                        };

                        // get details
                        var data = await _unitOfWork.ReportRepository.DownloadTimeTable(objTimeTableList);


                        // Prepare header table
                        DataTable dtTimeTableHeader = new DataTable();
                        dtTimeTableHeader.Columns.Add("OrderNumber");
                        dtTimeTableHeader.Columns.Add("EndTermName");
                        dtTimeTableHeader.Columns.Add("FinancialYearName");
                        dtTimeTableHeader.Columns.Add("CurrentDate");
                        dtTimeTableHeader.Columns.Add("CourseTypeName");
                        dtTimeTableHeader.Columns.Add("YearName");
                        dtTimeTableHeader.Columns.Add("CourseTypeNameFull");
                        dtTimeTableHeader.Columns.Add("ExamName");
                        dtTimeTableHeader.Columns.Add("ExamScheme");
                        dtTimeTableHeader.Columns.Add("CommonSubjectText");
                        dtTimeTableHeader.Columns.Add("SignatureFile", typeof(byte[]));

                        string stuimgFilepath = $"{ConfigurationHelper.RootPath}StaticFiles/Apr012025060950764086.png";

                        string photoFileName = item.SignatureFile;
                        string fullPhotoPath = Path.Combine(ConfigurationHelper.RootPath, "StaticFiles", Convert.ToString(item.SignatureFile));
                        byte[] photo;

                        if (System.IO.File.Exists(fullPhotoPath))
                        {
                            photo = System.IO.File.ReadAllBytes(fullPhotoPath); // This must be byte[]

                        }
                        else
                        {
                            photo = System.IO.File.ReadAllBytes(Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg"));
                        }

                        dtTimeTableHeader.Rows.Add(item.OrderNo, item.EndTermName, item.FinancialYearName, item.CurrentDate,
                            item.CourseTypeName, item.YearName, item.CourseTypeNameFull, item.ExamName, item.ExamScheme, item.CommonSubjectText, photo);

                        // make html by data and add in sb
                        var _sb = _printHtmlFile.GetHtmlOfTimeTable(loopIndex, dtTimeTableHeader, data.Tables[0]);
                        sb.Append(_sb);
                        loopIndex++;
                    }
                    // add end html
                    sb.AppendLine("</body>");
                    sb.AppendLine("</html>");

                    string htmlContent = sb.ToString();
                    // convert in word
                    var ms = new MemoryStream();

                    using (var wordDoc = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Create(
                        ms,
                        DocumentFormat.OpenXml.WordprocessingDocumentType.Document,
                        true))
                    {
                        var mainPart = wordDoc.AddMainDocumentPart();
                        mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(
                            new DocumentFormat.OpenXml.Wordprocessing.Body()
                        );

                        var converter = new HtmlToOpenXml.HtmlConverter(mainPart);


                        // html utf-8
                        await converter.ParseHtml(htmlContent);

                        // Force Hindi font if needed
                        wordDoc.ForceHindiFont();
                    }

                    ms.Position = 0;


                    byte[] pdfBytes = ms.ToArray();

                    result.Data = Convert.ToBase64String(pdfBytes);
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    //return File(
                    //    ms,
                    //    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    //    "timetable.docx"
                    //);
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        private string RenderColumnTable(List<GroupCodeAllocationAddEditModel> list)
        {
            var sb = new StringBuilder();

            // Group by GroupCode
            var groupedByGroupCode = list
                .GroupBy(x => x.GroupCode) // <-- change property if needed
                .ToList();

            foreach (var group in groupedByGroupCode)
            {
                int present = 0;
                int total = 0;

                sb.Append(@"
<div class='group-block' style='page-break-inside:avoid; margin-bottom:8px;'>
<table>
<thead>
<tr>
    <th colspan='2' style='background:#f0f0f0;font-weight:bold;'>
        Group Code : " + group.Key + @"
    </th>
</tr>
<tr>
    <th>CCode/Code/Group</th>
    <th>Present/Total</th>
</tr>
</thead>
<tbody>
");

                foreach (var item in group)
                {
                    sb.Append($@"
<tr>
    <td>{item.centergroupcode}</td>
    <td>{item.IsPresentTotal}/{item.Total}</td>
</tr>");

                    present += item.IsPresentTotal;
                    total += item.Total;
                }

                sb.Append($@"
<tr class='total-row'>
    <td>Total</td>
    <td>{present}/{total}</td>
</tr>
</tbody>
</table>
</div>
");
            }

            return sb.ToString();
        }

        [HttpPost("GetGroupCodeMasterReport")]

        public async Task<IActionResult> GetGroupCodeMasterReport([FromBody] GroupCodeAllocationAddEditModel filterModel)
        {
            try
            {
                // data
                var streams_data = await _unitOfWork.ReportRepository.GetGroupCodeMasterReport(filterModel);

                // data list
                var dataList = CommonFuncationHelper.ConvertDataTable<List<GroupCodeAllocationAddEditModel>>(streams_data.Tables[0]);

                // validate
                if (dataList == null || !dataList.Any())
                    return BadRequest("No data found");

                // get the exam name once
                string examName = dataList.First().ExamName ?? "";

                // start html with exam name main heading
                string headerHtml = $@"
                                    <!DOCTYPE html>
                                    <html>
                                    <head>
                                    <style>
                                        body {{
                                            font-family: Arial, Helvetica, sans-serif;
                                            margin: 0;
                                            padding: 10px 20px;
                                            font-size: 13px;
                                        }}
                                        .center {{ 
                                            text-align: center; 
                                        }}
                                        .title {{ 
                                            font-weight: bold; 
                                            font-size: 17px; 
                                        }}
                                        .subtitle {{ 
                                            font-size: 14px; 
                                            margin-top: 3px; 
                                        }}
                                        .subject-title {{
                                            text-align: center;
                                            font-weight: bold;
                                            font-size: 15px;
                                            margin: 10px 0 6px 0;
                                        }}

                                        .row {{
                                            width: 100%;
                                            display: table;
                                            table-layout: fixed;
                                        }}
                                        .col {{
                                            display: table-cell;
                                            vertical-align: top;
                                            padding: 4px;
                                        }}
                                        table {{
                                            width: 100%;
                                            border-collapse: collapse;
                                            border: 1px solid #000;
                                        }}
                                        th, td {{
                                            border: 1px solid #000;
                                            padding: 6px;
                                            text-align: center;
                                        }}
                                        th {{
                                            background-color: #f2f2f2;
                                            font-weight: bold;
                                        }}
                                        tr {{
                                            page-break-inside: avoid;
                                        }}
                                        .total-row td {{
                                            font-weight: bold;
                                            background-color: #e6e6e6;
                                        }}
                                        .page-break {{
                                            page-break-after: always;
                                        }}
                                    </style>
                                    </head>
                                    <body>
                                        <div class='center'>
                                            <div>Government of Rajasthan</div>
                                            <div class='title'>Board of Technical Education of Rajasthan, Jodhpur</div>
                                            <div class='subtitle'>
                                                Details of Examiner Group Code Diploma {examName}
                                            </div>
                                        </div>";

                // html store
                var sb = new StringBuilder();

                // get distinct subjects for filter
                var distinct_SubjectCodes = dataList.Select(x => (x.SubjectCode, x.SubjectName)).Distinct();

                // each subject code loop
                foreach (var distinct_SubjectCode in distinct_SubjectCodes)
                {
                    // get filtered list of each subject code
                    var filtered_SubjectCodes = dataList.Where(x => x.SubjectCode == distinct_SubjectCode.SubjectCode)
                                                        .OrderBy(x => x.GroupCode)
                                                        .ToList();

                    // heading
                    sb.Append(headerHtml);

                    // subject heading
                    sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode.SubjectCode} &nbsp; {distinct_SubjectCode.SubjectName}
                            </div>
                            ");

                    // group
                    sb.Append("<div class='row'>");

                    // filtered subject loop
                    int present = 0;
                    int total = 0;
                    int? prevgroupCode = 0;
                    int? currentgroupCode = 0;
                    int? nextgroupCode = 0;
                    int pageHeightCount = 37;
                    int pageHeightLoop = 0;
                    int pageColumnCount = 3;
                    int pageColumnLoop = 0;
                    bool isTotalTableFooterAdded = false;
                    for (int i = 0; i < filtered_SubjectCodes.Count; i++)
                    {
                        // set current group code
                        currentgroupCode = filtered_SubjectCodes[i].GroupCode;

                        // set prev group code
                        if (i > 0)
                        {
                            prevgroupCode = filtered_SubjectCodes[i - 1].GroupCode;
                        }
                        // set next group code
                        if (i + 1 < filtered_SubjectCodes.Count)
                        {
                            nextgroupCode = filtered_SubjectCodes[i + 1].GroupCode;
                        }

                        // column divided loop
                        if (pageHeightLoop == 0)
                        {
                            isTotalTableFooterAdded = false;
                            sb.Append("<div class='col'>");

                            // group code
                            sb.Append(@"
                                    <div class='group-block' style='page-break-inside:avoid; margin-bottom:8px;'>
                                    <table>
                                    <thead>                                    
                                    <tr>
                                        <th>CCode/Code/Group</th>
                                        <th>Present/Total</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                ");
                        }

                        // total
                        sb.Append($@"
                                <tr>
                                    <td>{filtered_SubjectCodes[i].centergroupcode}</td>
                                    <td>{filtered_SubjectCodes[i].IsPresentTotal}/{filtered_SubjectCodes[i].Total}</td>
                                </tr>");

                        // grand total                        
                        present += filtered_SubjectCodes[i].IsPresentTotal;
                        total += filtered_SubjectCodes[i].Total;
                        if (filtered_SubjectCodes.Count == i + 1 || nextgroupCode != currentgroupCode)
                        {
                            sb.Append($@"
                                <tr class='total-row'>
                                    <td>Total</td>
                                    <td>{present}/{total}</td>
                                </tr>
                            ");

                            // reset
                            present = 0;
                            total = 0;

                            isTotalTableFooterAdded = true;
                            pageHeightLoop++;
                        }

                        // column divided loop
                        pageHeightLoop++;
                        if (pageHeightCount < pageHeightLoop + 1 || filtered_SubjectCodes.Count + 1 == pageHeightLoop + 1)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");

                            sb.Append("</div>");
                            pageHeightLoop = 0;
                            pageColumnLoop++;
                        }

                        // row changed
                        if (pageColumnLoop >= pageColumnCount)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");
                            sb.Append("</div>");
                            // group
                            sb.Append("</div>");
                            sb.Append("<div class='page-break'></div>");

                            // heading
                            sb.Append(headerHtml);

                            // subject heading
                            sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode.SubjectCode} &nbsp; {distinct_SubjectCode.SubjectName}
                            </div>
                            ");

                            // group
                            sb.Append("<div class='row'>");

                            pageColumnLoop = 0;
                        }
                    }

                    sb.Append(@"
                            </tbody>
                            </table>
                            </div>
                        ");
                    sb.Append("</div>");
                    // group
                    sb.Append("</div>");
                    sb.Append("<div class='page-break'></div>");

                    // end html 
                    sb.Append(@"
                        </body>
                        </html>
                    ");
                }

                var _html = sb.ToString();

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = _converter.Convert(doc);
                return File(pdfBytes, "application/pdf", "Group_Code_Master_Report_SubjectWise.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private string RenderTable(List<GroupCodeAllocationAddEditModel> list)
        {
            var sb = new StringBuilder();

            // Group by GroupCode
            var groupedByGroupCode = list
                .GroupBy(x => x.GroupCode) // <-- change property if needed
                .ToList();

            foreach (var group in groupedByGroupCode)
            {
                int present = 0;
                int total = 0;

                sb.Append(@"
<div class='group-block' style='page-break-inside:avoid; margin-bottom:8px;'>
<table>
<thead>
<tr>
    <th colspan='2' style='background:#f0f0f0;font-weight:bold;'>
        Group Code : " + group.Key + @"
    </th>
</tr>
<tr>
    <th>CCode/Group/Branch</th>
    <th>Present/Total</th>
</tr>
</thead>
<tbody>
");

                foreach (var item in group)
                {
                    sb.Append($@"
<tr>
    <td>{item.centergroupcode}</td>
    <td>{item.IsPresentTotal}/{item.Total}</td>
</tr>");

                    present += item.IsPresentTotal;
                    total += item.Total;
                }

                sb.Append($@"
<tr class='total-row'>
    <td>Total</td>
    <td>{present}/{total}</td>
</tr>
</tbody>
</table>
</div>
");
            }

            return sb.ToString();
        }


        [HttpPost("GetGroupCodeMasterReportBranchwise")]

        public async Task<IActionResult> GetGroupCodeMasterReportBranchwise([FromBody] GroupCodeAllocationAddEditModel filterModel)
        {
            try
            {
                // data
                var streams_data = await _unitOfWork.ReportRepository.GetGroupCodeMasterReportBranchwise(filterModel);

                // data list
                var dataList = CommonFuncationHelper.ConvertDataTable<List<GroupCodeAllocationAddEditModel>>(streams_data.Tables[0]);

                // validate
                if (dataList == null || !dataList.Any())
                    return BadRequest("No data found");

                // get the exam name once
                string examName = dataList.First().ExamName ?? "";

                // start html with exam name main heading
                string headerHtml = $@"
                                    <!DOCTYPE html>
                                    <html>
                                    <head>
                                    <style>
                                        body {{
                                            font-family: Arial, Helvetica, sans-serif;
                                            margin: 0;
                                            padding: 10px 20px;
                                            font-size: 13px;
                                        }}
                                        .center {{ 
                                            text-align: center; 
                                        }}
                                        .title {{ 
                                            font-weight: bold; 
                                            font-size: 17px; 
                                        }}
                                        .subtitle {{ 
                                            font-size: 14px; 
                                            margin-top: 3px; 
                                        }}
                                        .subject-title {{
                                            text-align: center;
                                            font-weight: bold;
                                            font-size: 15px;
                                            margin: 10px 0 6px 0;
                                        }}

                                        .row {{
                                            width: 100%;
                                            display: table;
                                            table-layout: fixed;
                                        }}
                                        .col {{
                                            display: table-cell;
                                            vertical-align: top;
                                            padding: 4px;
                                        }}
                                        table {{
                                            width: 100%;
                                            border-collapse: collapse;
                                            border: 1px solid #000;
                                        }}
                                        th, td {{
                                            border: 1px solid #000;
                                            padding: 6px;
                                            text-align: center;
                                        }}
                                        th {{
                                            background-color: #f2f2f2;
                                            font-weight: bold;
                                        }}
                                        tr {{
                                            page-break-inside: avoid;
                                        }}
                                        .total-row td {{
                                            font-weight: bold;
                                            background-color: #e6e6e6;
                                        }}
                                        .page-break {{
                                            page-break-after: always;
                                        }}
                                    </style>
                                    </head>
                                    <body>
                                        <div class='center'>
                                            <div>Government of Rajasthan</div>
                                            <div class='title'>Board of Technical Education of Rajasthan, Jodhpur</div>
                                            <div class='subtitle'>
                                                Details of Examiner Group Code Diploma {examName}
                                            </div>
                                        </div>";

                // html store
                var sb = new StringBuilder();

                // get distinct subjects for filter
                var distinct_SubjectCodes = dataList.Select(x => x.SubjectCode).Distinct();

                // each subject code loop
                foreach (var distinct_SubjectCode in distinct_SubjectCodes)
                {
                    // get filtered list of each subject code
                    var filtered_SubjectCodes = dataList.Where(x => x.SubjectCode == distinct_SubjectCode)
                                                        .OrderBy(x => x.CCCode)
                                                        .ToList();

                    // heading
                    sb.Append(headerHtml);

                    // subject heading
                    sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode} 
                            </div>
                            ");

                    // group
                    sb.Append("<div class='row'>");

                    // filtered subject loop
                    int present = 0;
                    int total = 0;
                    int? prevgroupCode = 0;
                    int? currentgroupCode = 0;
                    int? nextgroupCode = 0;
                    int pageHeightCount = 35;
                    int pageHeightLoop = 0;
                    int pageColumnCount = 3;
                    int pageColumnLoop = 0;
                    bool isTotalTableFooterAdded = false;
                    for (int i = 0; i < filtered_SubjectCodes.Count; i++)
                    {
                        // set current group code
                        currentgroupCode = filtered_SubjectCodes[i].CCCode;

                        // set prev group code
                        if (i > 0)
                        {
                            prevgroupCode = filtered_SubjectCodes[i - 1].CCCode;
                        }
                        // set next group code
                        if (i + 1 < filtered_SubjectCodes.Count)
                        {
                            nextgroupCode = filtered_SubjectCodes[i + 1].CCCode;
                        }

                        // column divided loop
                        if (pageHeightLoop == 0)
                        {
                            isTotalTableFooterAdded = false;
                            sb.Append("<div class='col'>");

                            // group code
                            sb.Append(@"
                                    <div class='group-block' style='page-break-inside:avoid; margin-bottom:8px;'>
                                    <table>
                                    <thead>                                    
                                    <tr>
                                        <th>CCode/Code/Group/Branch</th>
                                        <th>Present/Total</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                ");
                        }

                        // total
                        sb.Append($@"
                                <tr>
                                    <td>{filtered_SubjectCodes[i].centergroupcode}</td>
                                    <td>{filtered_SubjectCodes[i].IsPresentTotal}/{filtered_SubjectCodes[i].Total}</td>
                                </tr>");

                        // grand total                        
                        present += filtered_SubjectCodes[i].IsPresentTotal;
                        total += filtered_SubjectCodes[i].Total;
                        if (filtered_SubjectCodes.Count == i + 1 || nextgroupCode != currentgroupCode)
                        {
                            sb.Append($@"
                                <tr class='total-row'>
                                    <td>Total</td>
                                    <td>{present}/{total}</td>
                                </tr>
                            ");

                            // reset
                            present = 0;
                            total = 0;

                            isTotalTableFooterAdded = true;
                            pageHeightLoop++;
                        }

                        // column divided loop
                        pageHeightLoop++;
                        if (pageHeightCount < pageHeightLoop + 1 || filtered_SubjectCodes.Count + 1 == pageHeightLoop + 1)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");

                            sb.Append("</div>");
                            pageHeightLoop = 0;
                            pageColumnLoop++;
                        }

                        // row changed
                        if (pageColumnLoop >= pageColumnCount)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");
                            sb.Append("</div>");
                            // group
                            sb.Append("</div>");
                            sb.Append("<div class='page-break'></div>");

                            // heading
                            sb.Append(headerHtml);

                            // subject heading
                            sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode}
                            </div>
                            ");

                            // group
                            sb.Append("<div class='row'>");

                            pageColumnLoop = 0;
                        }
                    }

                    sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");
                    sb.Append("</div>");
                    // group
                    sb.Append("</div>");
                    sb.Append("<div class='page-break'></div>");

                    // end html 
                    sb.Append(@"
                        </body>
                        </html>
                    ");
                }

                var _html = sb.ToString();

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = _converter.Convert(doc);
                return File(pdfBytes, "application/pdf", "Group_Code_Master_Report_SubjectWise.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //  Sample Annexture

        private string GenerateStudentTableRows(List<StudentSubjectModel> students, int maxSubjects = 15)
        {
            var sb = new StringBuilder();
            int srNo = 1;
            int studentcount = students.Count;

            if (students == null || students.Count == 0)
            {
                sb.Append("<tr style='page-break-inside:avoid;'>");
                sb.Append("<td colspan='19' style='text-align:center; font-weight:bold;'>");
                sb.Append("NIL");
                sb.Append("</td>");
                sb.Append("</tr>");

                return sb.ToString(); // VERY IMPORTANT
            }


            foreach (var s in students)
            {
                sb.Append("<tr style='page-break-inside:avoid;'>");

                sb.Append($"<td>{srNo++}</td>");
                sb.Append($"<td>{s.RollNo}</td>");
                sb.Append($"<td>{s.StudentName}</td>");
                sb.Append($"<td>{s.Subject1}</td>");
                sb.Append($"<td>{s.Subject2}</td>");
                sb.Append($"<td>{s.Subject3}</td>");
                sb.Append($"<td>{s.Subject4}</td>");
                sb.Append($"<td>{s.Subject5}</td>");
                sb.Append($"<td>{s.Subject6}</td>");
                sb.Append($"<td>{s.Subject7}</td>");
                sb.Append($"<td>{s.Subject8}</td>");
                sb.Append($"<td>{s.Subject9}</td>");
                sb.Append($"<td>{s.Subject10}</td>");
                sb.Append($"<td>{s.Subject11}</td>");
                sb.Append($"<td>{s.Subject12}</td>");
                sb.Append($"<td>{s.Subject13}</td>");
                sb.Append($"<td>{s.Subject14}</td>");
                sb.Append($"<td>{s.Subject15}</td>");

                sb.Append("<td></td>");
                sb.Append("</tr>");
            }


            return sb.ToString();
        }


        [HttpPost("GetSampleAnnexture")]
        public async Task<IActionResult> GetSampleAnnexture([FromBody] AnnextureModel model)
        {
            try
            {

                var mainData = await _unitOfWork.ReportRepository.GetSampleAnnexture(model);
                var dataList = CommonFuncationHelper
                                .ConvertDataTable<List<AnnextureModel>>(mainData.Tables[0]);
                var above = CommonFuncationHelper
                                .ConvertDataTable<List<StudentSubjectModel>>(mainData.Tables[1]);
                var below = CommonFuncationHelper
                                .ConvertDataTable<List<StudentSubjectModel>>(mainData.Tables[2]);
                if (dataList == null || !dataList.Any())
                    return BadRequest("No data found");

                var instituteName = dataList.First().InstituteName;
                var instituteCode = dataList.First().InstituteCode;
                var endTermName = dataList.First().EndTermName;

                var aboveRows = GenerateStudentTableRows(above);
                var belowRows = GenerateStudentTableRows(below);

                string html = $@"
<!DOCTYPE html>
<html lang='hi'>
<head>
<meta charset='UTF-8'>
<style>
    body, table {{
        font-family: Arial;
        font-size: 14px;
        line-height: 22px;
    }}
    table {{
        border-collapse: collapse;
        width: 100%;
    }}
    th, td {{
        border: 1px solid #ccc;;
        padding: 5px;
       
    }}
.no-border,
    .no-border td,
    .no-border tr {{border: none;
        border-collapse: collapse;
    }}


</style>
</head>
<body>
<div style='max-width:1200px;margin:0px auto;font-size:14px;padding:15px 45px;'>
            
    <tr>
        <td>
             <table class=""no-border"">
                <tr>
                    <td class=""left"">
                       
                    </td>

                    <td class=""right"" style=""float: right;"">
                       परिशिष्ट-32
                    </td>
                </tr>
                <tr>
                    <td class=""left"">
                        प्रेषक<br>
                        प्रधानाचार्य,<br>
                        पॉलिटेक्निक महाविद्यालय<br>
                        नाम :- {instituteName}<br>
                        संस्थान कोड संख्या :- {instituteCode}
                    </td>

                    <td class=""right"" style=""float: right;"">
                        प्रेषित<br>
                        संयुक्त निदेशक (गोपनीय)<br>
                        प्राविधिक शिक्षा मण्डल, जोधपुर
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;विषय :- आंतरिक मूल्यांकन में 85% से अधिक एवं 45% से कम प्राप्तांक के विद्यार्थियों का रिकॉर्ड सत्यापन रिपोर्ट।</p>
        </td>
    </tr>
    <tr>
        <td>
            <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;संदर्भ :- परीक्षा : {endTermName}</p>
        </td>
    </tr>
    <tr>
        <td>
            <h3 style=""text-align:center;margin-top:0px;text-decoration:underline;"">संस्थान स्तर पर प्राप्त अंकों का प्रमाण पत्र</h3>
        </td>
    </tr>
    <tr>
        <td>
            <p>विषयान्तर्गत निम्न विद्यार्थियों को उनके अर्जित अंकों विवरण अनुसार मण्डल कार्यालय में आंतरिक मूल्यांकन के सम्बन्ध में निम्न प्रमाणीकरण प्रस्तुत है :-।</p>
            <p>(अ) 85 प्रतिशत से अधिक प्राप्तांक प्राप्त किये गये।</p>
        </td>
    </tr>


<table>
<thead>
<tr>
<th>क्र.सं.</th>
<th>रोल नं / एस.पी.एन. </th>
<th>नाम</th>
<th colspan='15'>85 प्रतिशत से अधिक प्राप्तांक विषय कोड</th>
<th>शिक्षक के हस्ताक्षर</th>
</tr>
</thead>
<tbody>
{aboveRows}
</tbody>
</table>

<div style='page-break-after:always;'></div>

<p><b>(ब) आंतरिक मूल्यांकन में 45 प्रतिशत से कम प्राप्तांक के विद्यार्थियों का विवरण:</b></p>

<table>
<thead>
<tr>
<th>क्र.सं.</th>
<th>रोल नं / एस.पी.एन. </th>
<th>नाम</th>
<th colspan='15'>45 प्रतिशत से कम प्राप्तांक विषय कोड</th>
<th>शिक्षक के हस्ताक्षर</th>
</tr>
</thead>
<tbody>
{belowRows}
</tbody>
</table>

<p>
प्रमाणित किया जाता है कि उपरोक्त विद्यार्थियों के मण्डल द्वारा निर्धारित समस्त रिकार्डों की जांच की गई है, विद्यार्थियों का निम्नानुसार प्रमाणीकरण किया जाता है।
                    </p>
                    <ol>
                        <li>उपरोक्त विद्यार्थियों के प्रदत्त अंकों से सम्बन्धित एवं विद्यार्थियों के प्रत्येक रिकार्ड की व्यक्तिगत जांच की गई, विद्यार्थी उपरोक्त प्रदत्त अंक के योग्य है। </li>
                        <li>मण्डल कार्यालय द्वारा रिकार्ड मंगवाने पर समयबद्ध रूप से रिकार्ड मण्डल कार्यालय में उपलब्ध करवा दिया जायेगा। </li>
                        <li>प्रदत्त अंकों में अनियमितता पर अधोहस्ताक्षरकर्ता व्यक्तिगत रूप से उत्तरदायी होंगे।</li>
                    </ol>
                    <p>
                        उपरोक्त प्रमाणीकरण ऑनलाइन अंकों को दर्ज करते समय करें एवं एक प्रति <b>Email: conf.bter@gmail.com</b> पर भिजवायें।
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <table class=""no-border"">
                        <tr>
                            <td>
                                <b>विभागाध्यक्ष</b><br />
                                हस्ताक्षर........................................<br />
                                नाम..............................................<br />
                                पद...............................................
                            </td>
                            <td  style=""float: right;"">
                                <b>प्रधानाचार्य</b><br />
                                हस्ताक्षर........................................<br />
                                नाम..............................................<br />
                                पद...............................................<br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style=""padding-top:20px;"">
                    <table class=""no-border"">
                        <tr>
                            <td>
                                आवश्यक कार्यवाही हेतु प्रस्तुत है।
                            </td>
                                                   </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>";


                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = html,
                            WebSettings = { DefaultEncoding = "utf-8" },
                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 9,
                                Center = " [page] / [toPage]",
                                
                                //Line = true 
                            }
                        }
                    }
                };

                byte[] pdf = _converter.Convert(doc);
                return File(pdf, "application/pdf", "Annexure32.pdf");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("UploadAnnexture32")]
        public async Task<ApiResult<bool>> UploadAnnexture32([FromBody] AnnextureModel request)
        {
            ActionName = "SaveData([FromBody] AppointExaminerModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}


                    result.Data = await _unitOfWork.ReportRepository.UploadAnnexture32(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.InstituteID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.InstituteID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else
                        {
                            result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }


        [HttpPost("GetUploadAnnexture32")]
        public async Task<ApiResult<DataTable>> GetUploadAnnexture32([FromBody] AnnextureModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ReportRepository.GetUploadAnnexture32(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("GetInternalAssessmentStudentReport")]
        public async Task<ApiResult<string>> GetInternalAssessmentStudentReport(InternalAssessmentStudentReport model)
        {
            ActionName = "GetInternalAssessmentStudentReport(InternalAssessmentStudentReport model)";
            var result = new ApiResult<string>();

            try
            {
                StringBuilder sb = new StringBuilder();

                string[] streamids = [model.StreamID.ToString()];
                if (model.StreamID == 0)
                {
                    streamids = model.StreamIDs.Split(',');
                }

                foreach (var streamid in streamids)
                {
                    try
                    {
                        model.StreamID = int.Parse(streamid);
                        var dataSet = await _unitOfWork.ReportRepository
                            .GetInternalAssessmentStudentReport(model);

                        // log 
                        var logfilename = "InternalMarksReportCollegeWise_log";
                        CommonFuncationHelper.WriteTextLog($"1 streamid : {model.StreamID}", logfilename);
                        CommonFuncationHelper.WriteTextLog($"2 table count : {dataSet?.Tables?.Count}", logfilename);

                        // validating
                        if (dataSet == null || dataSet.Tables.Count < 1)
                        {
                            continue;
                        }
                        if (dataSet.Tables[0].Rows.Count == 0 || dataSet.Tables[1].Rows.Count == 0)
                        {
                            continue;
                        }

                        //
                        var _sb = _printHtmlFile.InternalAssessmentStudent_GetHtml(dataSet, model.TypeID);
                        sb.Append(_sb);

                    }
                    catch (Exception ex1)
                    {
                        // handal exception
                        await _unitOfWork.DisposeAsync();

                        await CreateErrorLog(new NewException
                        {
                            PageName = PageName,
                            ActionName = ActionName,
                            Ex = ex1
                        }, _unitOfWork);
                    }
                }

                // print
                string htmlContent = sb.ToString();

                // remove last blank page
                string endTag = "<div class='page-break'></div></body></html>";
                if (htmlContent.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    htmlContent = htmlContent.Substring(0, htmlContent.Length - endTag.Length)
                                 + "</body></html>";
                }


                // validate
                if (string.IsNullOrWhiteSpace(htmlContent))
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }


                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Landscape,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = htmlContent,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                byte[] arrbyte = await Task.Run(() => _converter.Convert(doc));
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                result.Data = Convert.ToBase64String(arrbyte);
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;

                await CreateErrorLog(new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                }, _unitOfWork);
            }

            return result;
            //return File(result.Data, "application/pdf", "HindiDinkToPdf.pdf");
        }


        #region GetMiscellaneousReport
        [HttpPost("GetMiscellaneousReport")]
        public async Task<ApiResult<DataTable>> GetMiscellaneousReport(MiscellaneousModel model)
        {
            ActionName = "GetMiscellaneousReport(MiscellaneousModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetMiscellaneousReport(model);
                //var action = "_get_data_to_test";
                //var ds = await _unitOfWork.CommonFunctionRepository.Dummy_GetTestUspDataByAction(action);
                //result.Data = ds.Tables[0];
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region Certificate Letter Report
        [HttpPost("GetCertificateLetterReport")]
        public async Task<ApiResult<string>> GetCertificateLetterReport(CertificateReportModel model)
        {
            ActionName = "GetCertificateLetterReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetCertificateLetterReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"CertificateLetterReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/CertificateLetterNew.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("CertificateLetter", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion


        [HttpPost("GetITIAllDataExcelReport")]
        public async Task<ApiResult<DataTable>> GetITIAllDataExcelReport([FromBody] ITIPlacementReportSearch filterModel)
        {
            ActionName = "GetITIAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetITIAllDataExcelReport(filterModel);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #region Provisional Certificate Report
        [HttpPost("GetProvisionalCertificateReport")]
        public async Task<ApiResult<string>> GetProvisionalCertificateReport(ProvisionalCertificateModel model)
        {
            ActionName = "GetProvisionalCertificateReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetProvisionalCertificateReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        var fileName = $"ProvisionalCertificate.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ProvisionalCertificate.rdlc";
                        //var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ProvisionalCertificate", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Examiner Static Report Feedback form

        [HttpPost("SaveExaminerStaticReportFeedbackForm")]
        public async Task<ApiResult<int>> SaveExaminerStaticReportFeedbackForm([FromBody] ExaminerStaticReportFeedbackDataModel request)
        {
            ActionName = " SaveExaminerStaticReportFeedbackForm([FromBody] ExaminerStaticReportFeedbackDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ReportRepository.SaveExaminerStaticReportFeedbackForm(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (System.Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }

        #endregion

        #region Get Statics Report Examiner Marks Data
        [HttpPost("GetStaticsReportExaminerMarksData")]
        public async Task<ApiResult<DataTable>> GetStaticsReportExaminerMarksData([FromBody] GroupCenterMappingModel body)
        {
            ActionName = "GetStaticsReportExaminerMarksData([FromBody] GroupCenterMappingModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetStaticsReportExaminerMarksData(body);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
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

        #endregion

        #region

        [HttpPost("DiplomaTest")]
        public async Task<IActionResult> DiplomaTest()
        {
            try
            {
                var sb = new StringBuilder();

                sb.Append(@"
        <!DOCTYPE html>
        <html>
        <head>
            <p><strong>विषय – NOC for Increase in Intake &amp; Addition of Course.</strong></p><p>महोदय ,</p><p>उपर्युक्त विषयान्तर्गत निर्देशानुसार लेख है कि आपके द्वारा प्रस्तुत प्रस्ताव एवं निदेशक, तकनीकी शिक्षा, जोधपुर से प्राप्त अभिशंसानुसार सत्र 2025-26 से आपके संस्थान में संचालित पाठ्यक्रम में सीट वृद्धि एवं नये पाठ्यक्रमों का संचालन किये जाने की अनुमति निम्नानुसार प्रदान की जाती है –</p><p><strong>Programs - Engineering and Technology</strong></p><h3>1. Increase in Intake</h3><figure class=""table""><table><thead><tr><th>Sr No.</th><th>Programme Name</th><th>Course Level</th><th>Course</th><th>Previous Intake</th><th>Updated Intake</th></tr></thead><tbody><tr><td>1</td><td>Engineering and Technology</td><td>UG</td><td>Computer Science and Engineering</td><td>30</td><td>60</td></tr><tr><td>2</td><td>Computer Application</td><td>UG</td><td>BCA</td><td>30</td><td>60</td></tr></tbody></table></figure><h3>2. Closure of Course</h3><figure class=""table""><table><thead><tr><th>Sr No.</th><th>Programme Name</th><th>Course Level</th><th>Course</th><th>Current Intake</th><th>Reduced Intake</th></tr></thead><tbody><tr><td>1</td><td>Engineering and Technology</td><td>UG</td><td>MECHATRONICS</td><td>30</td><td>30</td></tr></tbody></table></figure><p>उक्त अनुमति इस शर्त के साथ प्रदान की जाती है कि समस्त विद्यार्थियों (नियमित/स्वयंपाठी) के अध्ययन, फीस एवं लीगल सम्बंधित तथा स्टाफ से सम्बंधित समस्त जिम्मेदारी स्वयं संस्थान की रहेगी तथा एआईसीटीई, बीटीयू बीकानेर / आरटीयू कोटा / बीटीडीआर, जोधपुर एवं राज्य स्तरीय शुल्क निर्धारण समिति द्वारा जारी गाइडलाइन एवं आदेशों की पालन भी सुनिश्चित की जाए।</p><p>यह सक्षम स्तर से अनुमोदित है।</p><p style=""text-align:right;"">भवदीय<br>संयुक्त शासन सचिव</p><p><strong>प्रतिलिपि निम्नलिखित को सूचनार्थ एवं आवश्यक कार्यवाही हेतु प्रेषित है:</strong></p><ol><li>विशिष्ट सहायक, माननीय उपमुख्यमंत्री महोदय, तकनीकी शिक्षा विभाग।</li><li>निजी सचिव, अतिरिक्त शासन सचिव, तकनीकी शिक्षा विभाग।</li><li>निजी सचिव, अध्यक्ष राज्य स्तरीय शुल्क निर्धारण समिति, राजकीय महिला पॉलिटेक्निक महाविद्यालय, गाँधी नगर, जयपुर।</li><li>अध्यक्ष, अखिल भारतीय तकनीकी शिक्षा परिषद, नई दिल्ली।</li><li>क्षेत्रीय अधिकारी, अखिल भारतीय तकनीकी शिक्षा परिषद, चंडीगढ़।</li><li>कुलसचिव, राजस्थान तकनीकी विश्वविद्यालय, कोटा।</li><li>कुलसचिव, बीकानेर तकनीकी विश्वविद्यालय, बीकानेर।</li><li>निदेशक, तकनीकी शिक्षा निदेशालय, जोधपुर।</li><li>निदेशक, सेंटर फॉर ई-गवर्नेंस, जयपुर।</li><li>रक्षित प्रति।</li></ol></p>
        </body>
        </html>
        ");

                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings = new GlobalSettings
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 0,
                            Bottom = 0,
                            Left = 0,
                            Right = 0
                        }
                    },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8" },
                    FooterSettings = new FooterSettings
                    {
                        FontName = "Arial",
                        FontSize = 9,
                        Right = "Page [page] of [toPage]",
                        Left = "Printed on: [date]",
                        Line = true
                    }
                }
            }
                };

                byte[] pdfBytes = _converter.Convert(doc);

                return File(pdfBytes, "application/pdf", "Diploma.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        #endregion


        [HttpPost("GetExamResultStudentStaticsReport")]
        public async Task<ApiResult<DataTable>> GetExamResultStudentStaticsReport(ExamResultStudentStaticsModel model)
        {
            ActionName = "GetExamResultStudentStaticsReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetExamResultStudentStaticsReport(model));
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetSubjectTheoryParcticalMarkStaticsReport")]
        public async Task<ApiResult<DataTable>> GetSubjectTheoryParcticalMarkStaticsReport(ExamResultStudentStaticsModel model)
        {
            ActionName = "GetExamResultStudentStaticsReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetSubjectTheoryParcticalMarkStaticsReport(model));
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
                await _unitOfWork.DisposeAsync();
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

        #region downloadResultAppearedPassedStatisticsReport

        [HttpPost("getResultAppearedPassedStatisticsReport")]
        public async Task<IActionResult> getResultAppearedPassedStatisticsReport([FromBody] ResultAppearedPassedStatisticsReportModel data)
        {
            try
            {
                var main_data = new DataSet();
                if (data.ResultType == (int)EnumResultType.RwhResult || data.ResultType == (int)EnumResultType.RwhRevalEffected)
                {
                    main_data = await Task.Run(() => _unitOfWork.ReportRepository.downloadResultAppearedPassedStatisticsReportRWH(data));
                }
                else
                {
                    main_data = await Task.Run(() => _unitOfWork.ReportRepository.downloadResultAppearedPassedStatisticsReport(data));
                }
                //
                if (main_data == null || main_data.Tables.Count < 2)
                {
                    throw new Exception("Data not found for the given parameters.");
                }

                //heading
                var dt_heading = main_data.Tables[1].Rows[0];

                var dataList = CommonFuncationHelper.ConvertDataTable<List<ResultAppearedPassedStatisticsReportModel>>(main_data.Tables[0]);
                if (dataList == null) dataList = new List<ResultAppearedPassedStatisticsReportModel>();

                var streamHtml = string.Join("", dataList
                .GroupBy(x => x.StreamName)
                .Select(stream => $@"
                     <div class='stream-section'>
                    <div style='border-top:1px solid #000;border-bottom:1px solid #000;padding:5px;font-weight:bold;text-transform:uppercase;'>
                    {stream.Key}
                    </div>

                {string.Join("", stream
                        .GroupBy(x => x.Division)
                        .Select(div => $@"

                    <div style='padding:5px;'>

                        <div style='font-weight:bold;margin-top:10px;'>
                            {div.Key} 
                        </div>
                        <div>
                            {string.Join(", ", div.Select(x => x.RollNos).OrderBy(x => x))}
                        </div>
                    </div>
                "))}

                    <table style='width:100%;border-collapse:collapse;border:1px solid #000;margin-top:10px;'>
                        <tr>
                            <td style='border:1px solid #000;padding:4px;'>
                                <b>Total Appeared</b> : {stream.First().TotalAppeared}
                            </td>
                            <td style='border:1px solid #000;padding:4px;'>
                                <b>Total Passed</b> : {stream.First().PassStudent}
                            </td>
                            <td style='border:1px solid #000;padding:4px;'>
                                <b>Total Grace</b> : {stream.First().GraceStudent}
                            </td>
                            <td style='border:1px solid #000;padding:4px;'>
                                <b>{(stream.First().SemesterID == 6 ||
                                    (stream.First().SemesterID == 4 && stream.First().StreamName == "Beauty Culture") ? "Total Failed" : "Total Regulation")}</b> : {stream.First().FailStudent}
                            </td>
                            <td style='border:1px solid #000;padding:4px;'>
                                <b>Total UFM</b> : {stream.First().TotalUFM}
                            </td>
                            <td style='border:1px solid #000;padding:4px;'>
                                <b>Total RWH</b> : {stream.First().TotalRWH}
                            </td>
                            <td style='border:1px solid #000;padding:4px;'>
                                <b>Total RWH(Prev. Sem. Not Cleared)</b> : {stream.First().TotalRWHPrevSemNotCleared}
                            </td>
                        </tr>
                    </table>
                </div>
                "));
                var sb = new StringBuilder();

                sb.Append($@"
                    <!DOCTYPE html>
                    <html>
                    <head>

                    <style>

                    body {{
                    font-family: Arial;
                    font-size: 11px;
                    }}

                    .main-border {{
                    border:1px solid #000;
                    padding:10px;
                    }}

                    .stream-section {{
                    margin-bottom:15px;
                    }}

                    </style>

                    </head>

                    <body>

                    <div class='main-border'>

                    <table style='width:100%; border-collapse:collapse; border:1px solid #000;'>

                    <tr>
                    <td style='width:20%; padding:5px; border-bottom:1px solid #000;'>
                    NO. : {data.FileNo1}
                    </td>

                    <td style='width:60%; text-align:center; font-weight:bold; border-bottom:1px solid #000;'>
                    {dt_heading["Heading_1"]}
                    </td>

                    <td style='width:20%; text-align:right; padding:5px; border-bottom:1px solid #000;'>
                    Date : {data.FileDate:dd MMMM yyyy}
                    </td>
                    </tr>

                    <tr>
                    <td colspan='3' style='text-align:center; font-weight:bold; padding:5px; border-bottom:1px solid #000;'>
                    {dt_heading["Heading_2"]}
                    </td>
                    </tr>

                    <tr>
                    <td colspan='3' style='text-align:center; padding:5px; border-bottom:1px solid #000;'>
                    {dt_heading["Heading_3"]}
                    </td>
                    </tr>

                    <tr>
                    <td colspan='3' style='text-align:center; padding:5px;'>
                    Result Sheet (Passed Students Roll No.)
                    </td>
                    </tr>

                    </table>

                    {streamHtml}
                    <div style='margin-top:10px;padding:5px;'>
                    <p>                    
                    NO. : {data.FileNo2}
                    <span style='float:right;'>
                    Date : {data.FileDate:dd MMMM yyyy}
                    </span>
                    </p>
                    <p>Copy for information and necessary action to:</p>
                    1. Jt.Director confidential Board of Technical Education, Rajasthan, Jodhpur<br>
                    2. Incharge, Computer section for upload result<br>
                    3. Examination section Board of Technical Education, Rajasthan, Jodhpur 
                    <div style='margin-top:20px;'>
                    <span style='float:left;font-weight:bold;'>
                    Date of Declaration : {data.FileDate:dd MMMM yyyy}
                    </span>
                    <br/>
                    <br/>
                    <br/>                    
                    <span style='float:right;font-weight:bold;'>
                    REGISTRAR
                    </span>
                    </div>
                    </div>
                    </div>
                    </body>
                    </html>
                    ");

                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings = new GlobalSettings
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 5,
                            Bottom = 5,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects ={
                        new ObjectSettings
                        {
                            HtmlContent = sb.ToString(),
                            WebSettings = { DefaultEncoding = "utf-8" },
                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Right = "Page [page] of [toPage]",
                                Left = "Printed on: [date]",
                                Line = true
                            }
                        }
                    }
                };

                byte[] pdfBytes = _converter.Convert(doc);

                return File(pdfBytes, "application/pdf", "Result_Statistics_Report.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        #endregion


        [HttpPost("GetExamWiseStreamPapersreport")]
        public async Task<ApiResult<DataTable>> GetExamWiseStreamPapersreport(ExamWiseStreamPapersReportModel model)
        {
            ActionName = "GetExamWiseStreamPapersreport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetExamWiseStreamPapersreport(model));
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetStudentAllMarksReport")]
        public async Task<ApiResult<DataTable>> GetStudentAllMarksReport(StudentAllMarksReportModel model)
        {
            ActionName = "GetStudentAllMarksReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetStudentAllMarksReport(model));
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
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetMarksheetCorrectionHistoryReport")]
        public async Task<ApiResult<DataTable>> GetMarksheetCorrectionHistoryReport(MarksheetCorrectionHistoryModel model)
        {
            ActionName = "GetMarksheetCorrectionHistoryReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetMarksheetCorrectionHistoryReport(model));
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
                await _unitOfWork.DisposeAsync();
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


        [HttpPost("DiplomaTest2")]
        public async Task<IActionResult> DiplomaTest2()
        {
            try
            {
                var sb = new StringBuilder();

                sb.Append(@"

<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>Transport Bill</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .bill-container {
            width: 800px;
            margin: auto;
            border: 1px solid #ccc;
            padding: 20px;
        }
        .header {
            text-align: center;
            background: #ddd;
            padding: 10px;
            font-weight: bold;
        }
        .sub-header {
            text-align: center;
            font-size: 14px;
            margin-bottom: 10px;
        }
        .row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }
        .box {
            width: 48%;
            border: 1px solid #ccc;
            padding: 10px;
        }
        .section-title {
            font-weight: bold;
            margin: 15px 0 5px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        table, th, td {
            border: 1px solid #ccc;
        }
        th, td {
            padding: 8px;
            text-align: left;
        }
        .total {
            text-align: right;
            font-weight: bold;
        }
        .footer {
            margin-top: 30px;
            display: flex;
            justify-content: space-between;
        }
        .signature {
            width: 45%;
            text-align: center;
        }
    </style>
</head>
<body>

<div class=""bill-container"">

    <div class=""header"">TRANSPORT BILL</div>
    <div class=""sub-header"">Baba Motors Transport Services</div>

    <div class=""row"">
        <div>Date: 27/02/2026</div>
        <div>Bill No: 001</div>
    </div>

    <div class=""section-title"">DELIVERY DETAILS</div>
    <div class=""row"">
        <div class=""box"">
            <b>FROM:</b><br>
            Fortis Escorts Hospital Jaipur<br>
            to Professional Health Care
        </div>
        <div class=""box"">
            <b>DELIVERY ADDRESS:</b><br>
            Plot No. 37, Bank Colony Rd, Ext. B,<br>
            Krishna Vihar, Mahesh Nagar,<br>
            Gopal Pura Mode, Jaipur,<br>
            Rajasthan - 302015
        </div>
    </div>

    <div class=""section-title"">VEHICLE & DRIVER DETAILS</div>
    <div class=""row"">
        <div class=""box"">
            <b>Vehicle Type:</b> Pick Up<br>
            <b>Driver Name:</b> Jitendra Dhawan<br>
            <b>Service:</b> Transport of Items
        </div>
        <div class=""box"">
            <b>Vehicle No:</b> RJ-14 GU-2587<br>
            <b>Mobile No:</b> 9079028486
        </div>
    </div>

    <div class=""section-title"">FARE DETAILS</div>
    <table>
        <thead>
            <tr>
                <th>S.No</th>
                <th>Description</th>
                <th>Qty</th>
                <th>Amount (Rs.)</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>1</td>
                <td>Transport Charges - Pick Up Vehicle (RJ-14 GU-2587)</td>
                <td>1</td>
                <td>2800.00</td>
            </tr>
            <tr>
                <td colspan=""3"" class=""total"">TOTAL</td>
                <td><b>Rs. 2,800/-</b></td>
            </tr>
        </tbody>
    </table>

    <p><b>Amount in Words:</b> Rupees Two Thousand Eight Hundred Only</p>

    <div class=""footer"">
        <div class=""signature"">
            Receiver's Signature<br><br>
            __________________
        </div>
        <div class=""signature"">
            For Baba Motors Transport<br><br>
            Authorized Signatory
        </div>
    </div>

</div>

</body>
</html>
       


        ");

                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings = new GlobalSettings
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 0,
                            Bottom = 0,
                            Left = 0,
                            Right = 0
                        }
                    },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8" },
                    FooterSettings = new FooterSettings
                    {
                        FontName = "Arial",
                        FontSize = 9,
                        Right = "Page [page] of [toPage]",
                        Left = "Printed on: [date]",
                        Line = true
                    }
                }
            }
                };

                byte[] pdfBytes = _converter.Convert(doc);

                return File(pdfBytes, "application/pdf", "Diploma.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region Student reval Fee payment Receipt
        [HttpGet("GetStudentRevalFeePaymentReceipt/{TransactionId}/{StudentExamID}")]
        public async Task<ApiResult<string>> GetStudentRevalFeePaymentReceipt(string TransactionId, int StudentExamID)
        {
            ActionName = "GetStudentRevalFeePaymentReceipt(string EnrollmentNo, int StudentExamID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentRevalFeePaymentReceipt(TransactionId, StudentExamID);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        var fileName = $"RevalFeeReceipt_{TransactionId}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RevalFeeReceipt.rdlc";
                        //
                        // var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("StudentRevalFeePaymentReceipt", data.Tables[0]);
                        localReport.AddDataSource("StudentRevalFeePaymentReceipt_SubDetails", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);


                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save


                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region Diploma Certificate
        [HttpPost("GetDiplomaCertificate")]
        public async Task<ApiResult<string>> GetDiplomaCertificate(DiplomaCertificateModel model)
        {
            ActionName = "GetDiplomaCertificate()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetDiplomaCertificate(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        var fileName = $"DiplomaCertificate.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/DiplomaCertificate.rdlc";
                        //var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("DiplomaCertificate", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
                return result;
            });
        }
        #endregion

        #region UFM Letter
        [HttpPost("GetUFMLetter")]
        public async Task<ApiResult<string>> GetUFMLetter(UFMLetterModel model)
        {
            ActionName = "GetUFMLetter()";
            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => _unitOfWork.ReportRepository.GetUFMLetter(model));
                if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";

                var fileName = $"UFMLetter.pdf";

                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/UFMLetter.rdlc";

                string JDSignFilePath = $"{ConfigurationHelper.StaticFileRootPath}{data.Tables[0].Rows[0]["JDSignFileName"]}";
                data.Tables[0].Rows[0]["JDSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(JDSignFilePath));

                //var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                LocalReport localReport = new LocalReport(rdlcpath);
                localReport.AddDataSource("UFMLetter", data.Tables[0]);

                var reportResult = localReport.Execute(RenderType.Pdf);

                if (!System.IO.Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                result.Data = fileName;
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }
        #endregion

        #region Reval Exam Letter Report
        [HttpPost("GetRevalExamLetterReport")]
        public async Task<ApiResult<string>> GetRevalExamLetterReport(ExamLetterReport model)
        {
            ActionName = "GetRevalExamLetterReport(ExamLetterReport model)";
            List<string> ListRoleListPath = new List<string>();
            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => _unitOfWork.ReportRepository.GetRevalExamLetterReport(model));
                if (data != null)
                {

                    var groupedData = data.Tables[0]
                    .AsEnumerable()
                    .GroupBy(r => r.Field<string>("GroupCode"))
                    .Select(g => g.Key)
                    .ToList();

                    foreach (var group in groupedData)
                    {

                        var filteredRows = data.Tables[0]
                            .AsEnumerable()
                            .Where(r => r.Field<string>("GroupCode") == group)
                            .ToList();

                        DataTable filteredTable = filteredRows.Any()
                            ? filteredRows.CopyToDataTable()
                             : data.Tables[0].Clone();

                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        var fileName = $"RevalExamLetterReport_{group}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RevalExamLetter.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ExamLetterReport", filteredTable);
                        //localReport.AddDataSource("ExamLetterReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);
                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        ListRoleListPath.Add(filepath);
                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = "Success.";

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        //save



                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    }
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }


                #region "Save Multiple PDF PAGES"
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string guid = Guid.NewGuid().ToString().ToUpper();
                string outputFile = $"{guid}_{timestamp}.pdf";
                string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";

                if (await MergePdfFilesAsync(ListRoleListPath, outputPath))
                {
                    try
                    {
                        //delete files
                        await DeleteFiles(ListRoleListPath);
                    }
                    catch (Exception exd)
                    {
                    }
                    result.Data = outputFile;
                    result.State = EnumStatus.Success;
                    result.Message = "Success.";

                    await _unitOfWork.SaveChangesAsync();


                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "Something went wrong";
                }
                #endregion

            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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
            return result;
        }
        #endregion

        [HttpPost("GetRevalGroupCodeMasterReportBranchwise")]
        public async Task<IActionResult> GetRevalGroupCodeMasterReportBranchwise([FromBody] GroupCodeAllocationAddEditModel_Reval filterModel)
        {
            ActionName = "GetRevalGroupCodeMasterReportBranchwise([FromBody] GroupCodeAllocationAddEditModel_Reval filterModel)";
            try
            {
                // data
                var streams_data = await _unitOfWork.ReportRepository.GetRevalGroupCodeMasterReportBranchwise(filterModel);

                // data list
                var dataList = CommonFuncationHelper.ConvertDataTable<List<GroupCodeAllocationAddEditModel_Reval>>(streams_data.Tables[0]);

                // validate
                if (dataList == null || !dataList.Any())
                    return BadRequest("No data found");

                // get the exam name once
                string examName = dataList.First().ExamName ?? "";

                // start html with exam name main heading
                string headerHtml = $@"
                                    <!DOCTYPE html>
                                    <html>
                                    <head>
                                    <style>
                                        body {{
                                            font-family: Arial, Helvetica, sans-serif;
                                            margin: 0;
                                            padding: 10px 20px;
                                            font-size: 13px;
                                        }}
                                        .center {{ 
                                            text-align: center; 
                                        }}
                                        .title {{ 
                                            font-weight: bold; 
                                            font-size: 17px; 
                                        }}
                                        .subtitle {{ 
                                            font-size: 14px; 
                                            margin-top: 3px; 
                                        }}
                                        .subject-title {{
                                            text-align: center;
                                            font-weight: bold;
                                            font-size: 15px;
                                            margin: 10px 0 6px 0;
                                        }}

                                        .row {{
                                            width: 100%;
                                            display: table;
                                            table-layout: fixed;
                                        }}
                                        .col {{
                                            display: table-cell;
                                            vertical-align: top;
                                            padding: 4px;
                                        }}
                                        table {{
                                            width: 100%;
                                            border-collapse: collapse;
                                            border: 1px solid #000;
                                        }}
                                        th, td {{
                                            border: 1px solid #000;
                                            padding: 6px;
                                            text-align: center;
                                        }}
                                        th {{
                                            background-color: #f2f2f2;
                                            font-weight: bold;
                                        }}
                                        tr {{
                                            page-break-inside: avoid;
                                        }}
                                        .total-row td {{
                                            font-weight: bold;
                                            background-color: #e6e6e6;
                                        }}
                                        .page-break {{
                                            page-break-after: always;
                                        }}
                                    </style>
                                    </head>
                                    <body>
                                        <div class='center'>
                                            <div>Government of Rajasthan</div>
                                            <div class='title'>Board of Technical Education of Rajasthan, Jodhpur</div>
                                            <div class='subtitle'>
                                                Details of Examiner Group Code Diploma {examName}
                                            </div>
                                        </div>";

                // html store
                var sb = new StringBuilder();

                // get distinct subjects for filter
                var distinct_SubjectCodes = dataList.Select(x => x.SubjectCode).Distinct();

                // each subject code loop
                foreach (var distinct_SubjectCode in distinct_SubjectCodes)
                {
                    // get filtered list of each subject code
                    var filtered_SubjectCodes = dataList.Where(x => x.SubjectCode == distinct_SubjectCode)
                                                        .OrderBy(x => x.CCCode)
                                                        .ToList();

                    // heading
                    sb.Append(headerHtml);

                    // subject heading
                    sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode} 
                            </div>
                            ");

                    // group
                    sb.Append("<div class='row'>");

                    // filtered subject loop
                    int present = 0;
                    int total = 0;
                    int? prevgroupCode = 0;
                    int? currentgroupCode = 0;
                    int? nextgroupCode = 0;
                    int pageHeightCount = 35;
                    int pageHeightLoop = 0;
                    int pageColumnCount = 3;
                    int pageColumnLoop = 0;
                    bool isTotalTableFooterAdded = false;
                    for (int i = 0; i < filtered_SubjectCodes.Count; i++)
                    {
                        // set current group code
                        currentgroupCode = filtered_SubjectCodes[i].CCCode;

                        // set prev group code
                        if (i > 0)
                        {
                            prevgroupCode = filtered_SubjectCodes[i - 1].CCCode;
                        }
                        // set next group code
                        if (i + 1 < filtered_SubjectCodes.Count)
                        {
                            nextgroupCode = filtered_SubjectCodes[i + 1].CCCode;
                        }

                        // column divided loop
                        if (pageHeightLoop == 0)
                        {
                            isTotalTableFooterAdded = false;
                            sb.Append("<div class='col'>");

                            // group code
                            sb.Append(@"
                                    <div class='group-block' style='page-break-inside:avoid; margin-bottom:8px;'>
                                    <table>
                                    <thead>                                    
                                    <tr>
                                        <th>CCode/Code/Group/Branch</th>
                                        <th>Present/Total</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                ");
                        }

                        // total
                        sb.Append($@"
                                <tr>
                                    <td>{filtered_SubjectCodes[i].centergroupcode}</td>
                                    <td>{filtered_SubjectCodes[i].IsPresentTotal}/{filtered_SubjectCodes[i].Total}</td>
                                </tr>");

                        // grand total                        
                        present += filtered_SubjectCodes[i].IsPresentTotal;
                        total += filtered_SubjectCodes[i].Total;
                        if (filtered_SubjectCodes.Count == i + 1 || nextgroupCode != currentgroupCode)
                        {
                            sb.Append($@"
                                <tr class='total-row'>
                                    <td>Total</td>
                                    <td>{present}/{total}</td>
                                </tr>
                            ");

                            // reset
                            present = 0;
                            total = 0;

                            isTotalTableFooterAdded = true;
                            pageHeightLoop++;
                        }

                        // column divided loop
                        pageHeightLoop++;
                        if (pageHeightCount < pageHeightLoop + 1 || filtered_SubjectCodes.Count + 1 == pageHeightLoop + 1)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");

                            sb.Append("</div>");
                            pageHeightLoop = 0;
                            pageColumnLoop++;
                        }

                        // row changed
                        if (pageColumnLoop >= pageColumnCount)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");
                            sb.Append("</div>");
                            // group
                            sb.Append("</div>");
                            sb.Append("<div class='page-break'></div>");

                            // heading
                            sb.Append(headerHtml);

                            // subject heading
                            sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode}
                            </div>
                            ");

                            // group
                            sb.Append("<div class='row'>");

                            pageColumnLoop = 0;
                        }
                    }

                    sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");
                    sb.Append("</div>");
                    // group
                    sb.Append("</div>");
                    sb.Append("<div class='page-break'></div>");

                    // end html 
                    sb.Append(@"
                        </body>
                        </html>
                    ");
                }

                var _html = sb.ToString();

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = _converter.Convert(doc);
                return File(pdfBytes, "application/pdf", "RevalGroup_Code_Master_Report_SubjectWise.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("GetRevalGroupCodeMasterReport")]
        public async Task<IActionResult> GetRevalGroupCodeMasterReport([FromBody] GroupCodeAllocationAddEditModel_Reval filterModel)
        {
            ActionName = "GetRevalGroupCodeMasterReport([FromBody] GroupCodeAllocationAddEditModel_Reval filterModel)";
            try
            {
                // data
                var streams_data = await _unitOfWork.ReportRepository.GetRevalGroupCodeMasterReport(filterModel);

                // data list
                var dataList = CommonFuncationHelper.ConvertDataTable<List<GroupCodeAllocationAddEditModel_Reval>>(streams_data.Tables[0]);

                // validate
                if (dataList == null || !dataList.Any())
                    return BadRequest("No data found");

                // get the exam name once
                string examName = dataList.First().ExamName ?? "";

                // start html with exam name main heading
                string headerHtml = $@"
                                    <!DOCTYPE html>
                                    <html>
                                    <head>
                                    <style>
                                        body {{
                                            font-family: Arial, Helvetica, sans-serif;
                                            margin: 0;
                                            padding: 10px 20px;
                                            font-size: 13px;
                                        }}
                                        .center {{ 
                                            text-align: center; 
                                        }}
                                        .title {{ 
                                            font-weight: bold; 
                                            font-size: 17px; 
                                        }}
                                        .subtitle {{ 
                                            font-size: 14px; 
                                            margin-top: 3px; 
                                        }}
                                        .subject-title {{
                                            text-align: center;
                                            font-weight: bold;
                                            font-size: 15px;
                                            margin: 10px 0 6px 0;
                                        }}

                                        .row {{
                                            width: 100%;
                                            display: table;
                                            table-layout: fixed;
                                        }}
                                        .col {{
                                            display: table-cell;
                                            vertical-align: top;
                                            padding: 4px;
                                        }}
                                        table {{
                                            width: 100%;
                                            border-collapse: collapse;
                                            border: 1px solid #000;
                                        }}
                                        th, td {{
                                            border: 1px solid #000;
                                            padding: 6px;
                                            text-align: center;
                                        }}
                                        th {{
                                            background-color: #f2f2f2;
                                            font-weight: bold;
                                        }}
                                        tr {{
                                            page-break-inside: avoid;
                                        }}
                                        .total-row td {{
                                            font-weight: bold;
                                            background-color: #e6e6e6;
                                        }}
                                        .page-break {{
                                            page-break-after: always;
                                        }}
                                    </style>
                                    </head>
                                    <body>
                                        <div class='center'>
                                            <div>Government of Rajasthan</div>
                                            <div class='title'>Board of Technical Education of Rajasthan, Jodhpur</div>
                                            <div class='subtitle'>
                                                Details of Examiner Group Code Diploma {examName}
                                            </div>
                                        </div>";

                // html store
                var sb = new StringBuilder();

                // get distinct subjects for filter
                var distinct_SubjectCodes = dataList.Select(x => (x.SubjectCode, x.SubjectName)).Distinct();

                // each subject code loop
                foreach (var distinct_SubjectCode in distinct_SubjectCodes)
                {
                    // get filtered list of each subject code
                    var filtered_SubjectCodes = dataList.Where(x => x.SubjectCode == distinct_SubjectCode.SubjectCode)
                                                        .OrderBy(x => x.GroupCode)
                                                        .ToList();

                    // heading
                    sb.Append(headerHtml);

                    // subject heading
                    sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode.SubjectCode} &nbsp; {distinct_SubjectCode.SubjectName}
                            </div>
                            ");

                    // group
                    sb.Append("<div class='row'>");

                    // filtered subject loop
                    int present = 0;
                    int total = 0;
                    string? prevgroupCode = "";
                    string? currentgroupCode = "";
                    string? nextgroupCode = "";
                    int pageHeightCount = 37;
                    int pageHeightLoop = 0;
                    int pageColumnCount = 3;
                    int pageColumnLoop = 0;
                    bool isTotalTableFooterAdded = false;
                    for (int i = 0; i < filtered_SubjectCodes.Count; i++)
                    {
                        // set current group code
                        currentgroupCode = filtered_SubjectCodes[i].GroupCode;

                        // set prev group code
                        if (i > 0)
                        {
                            prevgroupCode = filtered_SubjectCodes[i - 1].GroupCode;
                        }
                        // set next group code
                        if (i + 1 < filtered_SubjectCodes.Count)
                        {
                            nextgroupCode = filtered_SubjectCodes[i + 1].GroupCode;
                        }

                        // column divided loop
                        if (pageHeightLoop == 0)
                        {
                            isTotalTableFooterAdded = false;
                            sb.Append("<div class='col'>");

                            // group code
                            sb.Append(@"
                                    <div class='group-block' style='page-break-inside:avoid; margin-bottom:8px;'>
                                    <table>
                                    <thead>                                    
                                    <tr>
                                        <th>CCode/Code/Group</th>
                                        <th>Present/Total</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                ");
                        }

                        // total
                        sb.Append($@"
                                <tr>
                                    <td>{filtered_SubjectCodes[i].centergroupcode}</td>
                                    <td>{filtered_SubjectCodes[i].IsPresentTotal}/{filtered_SubjectCodes[i].Total}</td>
                                </tr>");

                        // grand total                        
                        present += filtered_SubjectCodes[i].IsPresentTotal;
                        total += filtered_SubjectCodes[i].Total;
                        if (filtered_SubjectCodes.Count == i + 1 || nextgroupCode != currentgroupCode)
                        {
                            sb.Append($@"
                                <tr class='total-row'>
                                    <td>Total</td>
                                    <td>{present}/{total}</td>
                                </tr>
                            ");

                            // reset
                            present = 0;
                            total = 0;

                            isTotalTableFooterAdded = true;
                            pageHeightLoop++;
                        }

                        // column divided loop
                        pageHeightLoop++;
                        if (pageHeightCount < pageHeightLoop + 1 || filtered_SubjectCodes.Count + 1 == pageHeightLoop + 1)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");

                            sb.Append("</div>");
                            pageHeightLoop = 0;
                            pageColumnLoop++;
                        }

                        // row changed
                        if (pageColumnLoop >= pageColumnCount)
                        {
                            sb.Append(@"
                                    </tbody>
                                    </table>
                                    </div>
                                ");
                            sb.Append("</div>");
                            // group
                            sb.Append("</div>");
                            sb.Append("<div class='page-break'></div>");

                            // heading
                            sb.Append(headerHtml);

                            // subject heading
                            sb.Append($@"
                            <div class='subject-title'>
                                Subject Code: {distinct_SubjectCode.SubjectCode} &nbsp; {distinct_SubjectCode.SubjectName}
                            </div>
                            ");

                            // group
                            sb.Append("<div class='row'>");

                            pageColumnLoop = 0;
                        }
                    }

                    sb.Append(@"
                            </tbody>
                            </table>
                            </div>
                        ");
                    sb.Append("</div>");
                    // group
                    sb.Append("</div>");
                    sb.Append("<div class='page-break'></div>");

                    // end html 
                    sb.Append(@"
                        </body>
                        </html>
                    ");
                }

                var _html = sb.ToString();

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            //HeaderSettings = new HeaderSettings
                            //{
                            //    HtmUrl = headerFilePath,
                            //    Spacing = 3
                            //},

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = _converter.Convert(doc);
                return File(pdfBytes, "application/pdf", "RevalGroup_Code_Master_Report_SubjectWise.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region Renumeration Examiner Reval
        [HttpPost("GenerateAndViewPdf_Reval")]
        [RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<IActionResult> GenerateAndViewPdf_Reval([FromBody] RenumerationExaminerRequestModel filterModel)
        {
            ActionName = "GenerateAndViewPdf_Reval([FromBody] RenumerationExaminerRequestModel filterModel)";
            try
            {
                var data = await _unitOfWork.RenumerationExaminerRepository.GetDataForGeneratePdf_Reval(filterModel);
                if (data?.Rows?.Count > 0)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderBTER, "RemunerationExaminerReval.rdlc");
                    //save file
                    var newFileName = $"RemunerationExaminerReval_{DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}.pdf";
                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    //file stream
                    return File(reportResult.MainStream, "application/pdf", newFileName);
                }
                else
                {
                    return Content("No data available to generate the PDF.");
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                return Content("An error occurred while generating the PDF.");
            }
        }

        [HttpPost("SavePDFSubmitAndForwardToJD_Reval")]
        [RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<ApiResult<bool>> SavePDFSubmitAndForwardToJD_Reval([FromBody] RenumerationExaminerRequestModel filterModel)
        {
            ActionName = "SavePDFSubmitAndForwardToJD_Reval([FromBody] RenumerationExaminerRequestModel filterModel)";
            var result = new ApiResult<bool>();
            try
            {
                var data = await _unitOfWork.RenumerationExaminerRepository.GetDataForGeneratePdf_Reval(filterModel);
                var objData = CommonFuncationHelper.ConvertDataTable<RenumerationExaminerPDFModel>(data);
                if (objData != null)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderBTER, "RemunerationExaminerReval.rdlc");
                    //save file
                    var newFileName = $"RemunerationExaminerReval_{DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}.pdf";
                    var folderPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.ReportsFolder);
                    var filepath = Path.Combine(folderPath, newFileName);

                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //file stream
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    //save in folder
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                    //save in db
                    objData.IPAddress = CommonFuncationHelper.GetIpAddress();
                    objData.FileName = newFileName;

                    var isSave = await _unitOfWork.RenumerationExaminerRepository.SaveDataSubmitAndForwardToJD_Reval(objData);
                    await _unitOfWork.SaveChangesAsync();
                    if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_UPDATE_ERROR;
                    }
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
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
        #endregion

        #region Bulk Student Marksheet Chunk
        [HttpPost("StudentMarksheetDownloadChunk")]
        public async Task<ApiResult<string>> StudentMarksheetDownloadChunk([FromBody] List<MarksheetDownloadSearchModel> Model)
        {
            ActionName = "StudentMarksheetDownloadChunk([FromBody] List<MarksheetDownloadSearchModel> Model)";

            var result = new ApiResult<string>();
            var logfilename = "_StudentMarksheetDownload";
            var Session = string.Empty;
            try
            {
                Session = Model[0].SessionName;
                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{Constants.MarksheetFolder}/{Session}";

                // store students that have filename success for merge file for shwoing marksheet
                List<GenerateMarksheetModel> ListData = new List<GenerateMarksheetModel>();
                // store students that dont have filename failed any resion
                List<GenerateMarksheetModel> notGenerateStudents = new List<GenerateMarksheetModel>();

                int i = 1;
                // passed students list in chunks
                foreach (var student in Model)
                {
                    CommonFuncationHelper.WriteTextLog($"--------------------- main loop start: {i} ------------------------", logfilename);
                    try
                    {
                        CommonFuncationHelper.WriteTextLog($"1. model student loop : {student.RollNo}", logfilename);
                        GenerateMarksheetModel objStudent = new GenerateMarksheetModel();
                        // already have saved file
                        if (student.MarksheetFile != "")
                        {
                            CommonFuncationHelper.WriteTextLog($"1.1. already file exists in if : {student.RollNo}", logfilename);
                            // for merge
                            objStudent.StudentID = student.StudentID;
                            objStudent.RollNo = student.RollNo;
                            objStudent.MarksheetPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{Constants.MarksheetFolder}/{student.MarksheetFilePath}";
                            objStudent.MarksheetFile = student.MarksheetFile;
                            // add
                            ListData.Add(objStudent);
                        }
                        else
                        {
                            CommonFuncationHelper.WriteTextLog($"1.2. dosenot file exists in else : {student.RollNo}", logfilename);

                            // set for student model
                            var studentModel = new StudentResultSearchModel
                            {
                                DOB = student.DOB,
                                EndTermID = student.EndTermID,
                                RollNo = student.RollNo,
                                SemesterID = student.SemesterID,
                                ResultType = student.ResultTypeID,
                                EffectiveEndTermID = student.EffectiveEndTermID
                            };

                            // get mark sheet data for each student
                            DataSet data = new DataSet();
                            if (student.ResultTypeID == (int)EnumResultType.MainResult)
                            {
                                data = await _unitOfWork.MarksheetDownloadRepository.GetStudentResult_public(studentModel);
                            }
                            else if (student.ResultTypeID == (int)EnumResultType.RevaluationResult)
                            {
                                data = await _unitOfWork.MarksheetDownloadRepository.GetStudentResultReval_public(studentModel);
                            }
                            else if (student.ResultTypeID == (int)EnumResultType.RwhResult ||
                                student.ResultTypeID == (int)EnumResultType.RwhRevalEffected)
                            {
                                data = await _unitOfWork.MarksheetDownloadRepository.GetStudentResultRWH_public(studentModel);
                            }
                            else if (student.ResultTypeID == (int)EnumResultType.Ufm)
                            {
                                throw new Exception("Invalid Request!");
                            }
                            else
                            {
                                throw new Exception("Invalid Request!");
                            }


                            if (data?.Tables?.Count == 3 && data.Tables[0].Rows.Count > 0)
                            {
                                CommonFuncationHelper.WriteTextLog($"1.3. all data found: {student.RollNo}", logfilename);

                                //create folder
                                if (!System.IO.Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                string timestamp_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                                var fileName = $"StudentMarksheet_{student.RollNo}_{timestamp_str}.pdf";
                                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{Constants.MarksheetFolder}/{Session}/{fileName}";

                                //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentMarksheet.rdlc";


                                //// rdlc generate
                                //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                                //LocalReport localReport = new LocalReport(rdlcpath);
                                //localReport.AddDataSource("StudentDetailsForMarksheet", data.Tables[0]);
                                //localReport.AddDataSource("StudentMarksheetSubjectDetails", data.Tables[1]);
                                //localReport.AddDataSource("ResultDetails", data.Tables[2]);
                                //var reportResult = localReport.Execute(RenderType.Pdf);

                                //save in folder
                                //System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                                // get html
                                var sb = await _printHtmlFile.GetHtmlOfMarkSheet(data);
                                var _html = sb.ToString();

                                // remove last blank page
                                string endTag = "<div class='page-break'></div></body></html>";
                                if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                                {
                                    _html = _html.Substring(0, _html.Length - endTag.Length)
                                                 + "</body></html>";
                                }

                                var pdfBytes = await _pdfService.GenerateAsync(_html,
                                    new PdfOptions
                                    {
                                        Format = "A4",
                                        MarginTop = "10mm",
                                        MarginBottom = "0mm",
                                        MarginLeft = "10mm",
                                        MarginRight = "10mm",
                                        PrintBackground = true
                                    });

                                await System.IO.File.WriteAllBytesAsync(filepath, pdfBytes);

                                CommonFuncationHelper.WriteTextLog($"1.4. save file in folder: {student.RollNo}", logfilename);

                                // create an object for new record
                                MarksheetSaveDataModel objMarksheet = new MarksheetSaveDataModel();
                                // table 1 for marksheet
                                DataRow studentData = data.Tables[0].Rows[0];

                                objMarksheet.MarkSheetID = student.MarksheetID ?? 0;// pk

                                objMarksheet.StudentName = studentData["StudentName"].ToString();
                                objMarksheet.FatherName = studentData["FatherName"].ToString();
                                objMarksheet.MotherName = studentData["MotherName"].ToString();
                                objMarksheet.MotherName = studentData["MotherName"].ToString();
                                objMarksheet.Gender = studentData["Gender"].ToString();
                                objMarksheet.EnrollmentNo = studentData["EnrollmentNo"].ToString();
                                objMarksheet.RollNo = studentData["RollNo"].ToString();
                                objMarksheet.DOB = studentData["DOB"].ToString();
                                objMarksheet.InstituteName = studentData["InstituteName"].ToString();
                                objMarksheet.StreamName = studentData["StreamName"].ToString();
                                objMarksheet.StreamCode = studentData["StreamCode"].ToString();
                                objMarksheet.EndTerm = studentData["EndTermName"].ToString();
                                objMarksheet.Session = studentData["Session"].ToString();
                                objMarksheet.ResultDate = studentData["ResultDate"].ToString();
                                objMarksheet.SrNo = student.SRNO;

                                objMarksheet.CourseType = (int)studentData["CourseType"];
                                objMarksheet.StudentID = (int)studentData["StudentID"];
                                objMarksheet.StudentExamID = (int)studentData["StudentExamID"];
                                objMarksheet.SemesterID = (int)studentData["SemesterID"];
                                objMarksheet.InstituteID = (int)studentData["InstituteID"];
                                objMarksheet.StreamId = (int)studentData["StreamId"];
                                objMarksheet.Result = (int)studentData["Result"];
                                objMarksheet.CreatedBy = (int)student.ModifyBy;
                                objMarksheet.ModifyBy = (int)student.ModifyBy;
                                objMarksheet.Type = (int)student.StudentTypeID;

                                objMarksheet.IsUFM = (int)studentData["IsUFM"];
                                objMarksheet.IsRWH = (int)studentData["IsRWH"];
                                objMarksheet.IsReval = (int)studentData["IsReval"];
                                objMarksheet.IsBridge = (int)studentData["IsBridge"];
                                objMarksheet.IsRWHResult = (int)studentData["IsRWHResult"];
                                objMarksheet.IsLiteral = (int)studentData["IsLiteral"];
                                objMarksheet.EndTermID = (int)studentData["EndTermID"];
                                objMarksheet.ResultTypeId = (int)studentData["ResultTypeId"];
                                objMarksheet.IssueDate = studentData["IssueDate"].ToString();
                                objMarksheet.MarksheetYear = Convert.ToInt32(studentData["MarksheetYear"]);
                                objMarksheet.Year = Convert.ToInt32(studentData["Year"]);
                                objMarksheet.EffectiveEndTermID = Convert.ToInt32(student.EffectiveEndTermID);

                                objMarksheet.MarksheetFile = fileName;
                                objMarksheet.MarksheetFilePath = $"{Session}/{fileName}";


                                // table 2 for subjects
                                if (data.Tables[1].Rows.Count > 0)
                                {
                                    CommonFuncationHelper.WriteTextLog($"1.5. subject data: {student.RollNo}", logfilename);
                                    foreach (DataRow row in data.Tables[1].Rows)
                                    {
                                        // log
                                        CommonFuncationHelper.WriteTextLog($"1.6. add subject dedails in loop: {student.RollNo}, {row.Field<string>("SubjectCode") ?? string.Empty}", logfilename);

                                        MarksheetSubjectDataModel marksheetSub = new MarksheetSubjectDataModel
                                        {
                                            StudentName = row.Field<string>("StudentName") ?? string.Empty,
                                            StudentID = row.Field<int>("StudentID"),
                                            SubjectCode = row.Field<string>("SubjectCode") ?? string.Empty,
                                            SubjectName = row.Field<string>("SubjectName") ?? string.Empty,
                                            SubjectCredits = row.Field<string>("SubjectCredits"),
                                            EarnedCredits = row.Field<string>("EarnedCredits"),
                                            Grade = row.Field<string>("Grade") ?? string.Empty,
                                            Remarks = row.Field<string>("Remarks") ?? string.Empty,
                                            IsStudentCenteredActivity = row.Field<bool>("IsStudentCenteredActivity"),
                                            IsExCurrent = row.Field<int>("IsExCurrent")
                                        };

                                        // Add the newly mapped object to your list
                                        objMarksheet.SubjectDetails.Add(marksheetSub);
                                    }
                                }

                                // table 3 for results (cgpa, sgpa etc...)
                                if (data.Tables[2].Rows.Count > 0)
                                {
                                    // log
                                    CommonFuncationHelper.WriteTextLog($"1.7. result data: {student.RollNo}", logfilename);

                                    DataRow row = data.Tables[2].Rows[0];


                                    MarksheetResultDataModel marksheetResult = new MarksheetResultDataModel();
                                    // --- Flags ---
                                    marksheetResult.IsReval = (bool)row["IsReval"];
                                    marksheetResult.IsLiteral = (bool)row["IsLiteral"];
                                    marksheetResult.ResultTypeId = Convert.ToInt32(row["ResultTypeId"]);

                                    // --- Semester 1 ---
                                    marksheetResult.SubjectCreditsSem1 = row["SubjectCreditsSem1"].ToString();
                                    marksheetResult.EarnedCreditsSem1 = row["EarnedCreditsSem1"].ToString();
                                    marksheetResult.CGPASem1 = row["CGPASem1"].ToString();
                                    marksheetResult.SGPASem1 = row["SGPASem1"].ToString();

                                    // --- Semester 2 ---
                                    marksheetResult.SubjectCreditsSem2 = row["SubjectCreditsSem2"].ToString();
                                    marksheetResult.EarnedCreditsSem2 = row["EarnedCreditsSem2"].ToString();
                                    marksheetResult.CGPASem2 = row["CGPASem2"].ToString();
                                    marksheetResult.SGPASem2 = row["SGPASem2"].ToString();

                                    // --- Semester 3 ---
                                    marksheetResult.SubjectCreditsSem3 = row["SubjectCreditsSem3"].ToString();
                                    marksheetResult.EarnedCreditsSem3 = row["EarnedCreditsSem3"].ToString();
                                    marksheetResult.CGPASem3 = row["CGPASem3"].ToString();
                                    marksheetResult.SGPASem3 = row["SGPASem3"].ToString();

                                    // --- Semester 4 ---
                                    marksheetResult.SubjectCreditsSem4 = row["SubjectCreditsSem4"].ToString();
                                    marksheetResult.EarnedCreditsSem4 = row["EarnedCreditsSem4"].ToString();
                                    marksheetResult.CGPASem4 = row["CGPASem4"].ToString();
                                    marksheetResult.SGPASem4 = row["SGPASem4"].ToString();

                                    // --- Semester 5 ---
                                    marksheetResult.SubjectCreditsSem5 = row["SubjectCreditsSem5"].ToString();
                                    marksheetResult.EarnedCreditsSem5 = row["EarnedCreditsSem5"].ToString();
                                    marksheetResult.CGPASem5 = row["CGPASem5"].ToString();
                                    marksheetResult.SGPASem5 = row["SGPASem5"].ToString();

                                    // --- Semester 6 ---
                                    marksheetResult.SubjectCreditsSem6 = row["SubjectCreditsSem6"].ToString();
                                    marksheetResult.EarnedCreditsSem6 = row["EarnedCreditsSem6"].ToString();
                                    marksheetResult.CGPASem6 = row["CGPASem6"].ToString();
                                    marksheetResult.SGPASem6 = row["SGPASem6"].ToString();

                                    // --- Final Summaries & Results ---
                                    marksheetResult.Percentage = row["Percentage"].ToString();
                                    marksheetResult.Result = row["Result"].ToString();
                                    marksheetResult.ResultDeclareDate = row.Table.Columns.Contains("ResultDeclareDate") ? row["ResultDeclareDate"].ToString() : string.Empty;
                                    marksheetResult.DiplomaFinalResult = row["DiplomaFinalResult"].ToString();
                                    marksheetResult.Division = row["Division"].ToString();
                                    marksheetResult.TotalSubjectCredits = row["TotalSubjectCredits"].ToString();
                                    marksheetResult.TotalEarnedCredits = row["TotalEarnedCredits"].ToString();

                                    // set in main model
                                    objMarksheet.ResultDetails = marksheetResult;
                                }

                                await _unitOfWork.MarksheetDownloadRepository.AddUpdateMarksheet(objMarksheet);
                                await _unitOfWork.SaveChangesAsync();

                                CommonFuncationHelper.WriteTextLog($"1.8. save student done : {student.RollNo}", logfilename);


                                // for merge
                                objStudent.StudentID = student.StudentID;
                                objStudent.RollNo = student.RollNo;
                                objStudent.MarksheetPath = filepath;
                                objStudent.MarksheetFile = fileName;
                                // add
                                ListData.Add(objStudent);
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        await _unitOfWork.DisposeAsync();

                        // add in list
                        notGenerateStudents.Add(new GenerateMarksheetModel
                        {
                            StudentID = student.StudentID,
                            RollNo = student.RollNo
                        });

                        CommonFuncationHelper.WriteTextLog($"2. loop error for student : {student.RollNo}", logfilename);
                        CommonFuncationHelper.WriteTextLog($"2.1. loop error : {ex1.Message}", logfilename);
                    }

                    CommonFuncationHelper.WriteTextLog($"--------------------- main loop end: {i} ------------------------", logfilename);
                    i++;
                }// end student loop

                #region "Save Multiple PDF PAGES"
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string outputFile = $"Marksheet_{timestamp}.pdf";
                string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                List<string?> strSoureFiles = ListData.Select(s => s.MarksheetPath)?.ToList();

                // merge all files
                var ismerged = await MergePdfFilesAsync(strSoureFiles, outputPath);
                CommonFuncationHelper.WriteTextLog($"3. merge done with file count : {strSoureFiles?.Count == 0} and flage : {ismerged}", logfilename);
                if (ismerged)
                {
                    result.Data = outputFile;
                    result.State = EnumStatus.Success;
                    var msg = Constants.MSG_DATA_LOAD_SUCCESS;
                    if (notGenerateStudents?.Count > 0)
                    {
                        msg = $"{Constants.MSG_FILE_DOWNLOAD_SUCCESS}, Except these students (Roll No.):<br/> {string.Join(", ", notGenerateStudents.Select(s => s.RollNo))}";
                    }
                    result.Message = msg;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    var msg = Constants.MSG_ERROR_IN_MERGING_FILES;
                    if (strSoureFiles == null || strSoureFiles?.Count == 0)
                    {
                        msg = Constants.MSG_FILE_NOT_FOUND;
                    }
                    result.Message = msg;
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonFuncationHelper.WriteTextLog($"4. main error : {ex.Message}", logfilename);

                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
                // Write error log
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
        #endregion


        #region Trn ITI StudentExamsFeeMark

        [HttpPost("SaveTrn_ITI_StudentExamsFeeMark")]
        public async Task<ApiResult<int>> SaveTrn_ITI_StudentExamsFeeMark([FromBody] Trn_ITI_StudentExamsFeeMarkDataModel request)
        {
            ActionName = "SaveTrn_ITI_StudentExamsFeeMark([FromBody] Trn_ITI_StudentExamsFeeMarkDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ReportRepository.SaveTrn_ITI_StudentExamsFeeMark(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (System.Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
            });
        }

        #endregion

        [HttpPost("GetCenterStudents")]
        public async Task<IActionResult> GetCenterStudents([FromBody] CenterStudentSearchModel body)
        {
            try
            {
                DataSet ds = await _unitOfWork.ReportRepository.GetCenterStudents(body);

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return BadRequest("No data found.");

                DataTable dt = ds.Tables[0];

                // ── Extract common header values from first row ──────────────────────
                string centerName = dt.Rows[0]["InstituteName"]?.ToString() ?? "";
                string centerCode = dt.Rows[0]["DgetCode"]?.ToString() ?? "";
                string examMonth = dt.Rows[0]["examMonth"]?.ToString() ?? "";

                // ── Group students by StreamName (Trade) + SemesterName ──────────────
                var tradeGroups = dt.AsEnumerable()
                    .GroupBy(r => new
                    {
                        StreamName = r["StreamName"]?.ToString() ?? "",
                        SemesterName = r["SemesterName"]?.ToString() ?? ""
                    })
                    .ToList();

                // ── Build one score-sheet block per trade/semester group ─────────────
                var allSheets = new StringBuilder();
                int groupIndex = 0;

                foreach (var group in tradeGroups)
                {
                    groupIndex++;
                    string tradeName = group.Key.StreamName;
                    string semesterName = group.Key.SemesterName;
                    var rows = group.ToList();

                    // Render every student for this trade/semester continuously —
                    // no artificial row cap, no blank filler rows. A new page-block
                    // (and page break) only happens when the trade/semester changes.
                    var studentRows = new StringBuilder();
                    for (int i = 0; i < rows.Count; i++)
                    {
                        string sNo = (i + 1).ToString();
                        string rollNo = rows[i]["RollNo"]?.ToString() ?? "";
                        studentRows.AppendLine($@"
                    <tr>
                        <td style='height:14px'>{sNo}</td>
                        <td>{rollNo}</td>
                        <td></td>
                        <td></td>
                    </tr>");
                    }

                    string headerBlock = $@"
                <table class='header-table'>
                    <tr>
                        <td colspan='3'><b>Center Name:</b> {centerName}</td>
                    </tr>
                    <tr>
                        <td colspan='4'>
                            <b>NCVT CTS Main Exam:</b> {semesterName}
                            <b>Trade</b> {examMonth}
                        </td>
                    </tr>
                    <tr>
                        <td><b>Trade Name:</b></td>
                        <td colspan='3'>{tradeName}</td>
                       
                    </tr>
                    <tr>
                         <td><b>Subject:</b></td>
                        <td>Practical</td>
                        <td><b>Center Code:</b></td>
                        <td>{centerCode}</td>
                       
                    </tr>
                    <tr>
                        <td><b>Examiner Code:</b></td>
                        <td>____________</td>
                        <td><b>Maximum Marks:</b></td>
                        <td>250</td>
                    </tr>
                </table>";

                    string marksTable = $@"
                <table class='marks-table'>
                    <tr>
                        <th rowspan='2' style='width:40px'>S.No.</th>
                        <th rowspan='2' style='width:70px'>Roll No</th>
                        <th colspan='2'>Marks Obtained</th>
                    </tr>
                    <tr>
                        <th style='width:65%;'>In Words</th>
                        <th style='width:25%;'>In Fig.</th>
                    </tr>
                    {studentRows}
                </table>";

                    // ── Left cell content = Practical Examiner footer ─────────────────
                    string leftSheet = $@"
                {headerBlock}
                {marksTable}
                <table class='footer-table'>
                    <tr>
                        <td colspan='2' class='center'>
                            <b><u>Practical Examiner</u></b>
                        </td>
                    </tr>
                    <tr><td>Name: __________________</td><td>Date: __________________</td></tr>
                    <tr><td>Post: __________________</td><td></td></tr>
                    <tr><td>Mobile No: __________________</td><td>Signature: __________________</td></tr>
                </table>";

                    // ── Right cell content = Center Superintendent footer ─────────────
                    string rightSheet = $@"
                {headerBlock}
                {marksTable}
                <table class='footer-table'>
                    <tr>
                        <td colspan='2' class='center'>
                            <b><u>Center Superintendent/Co-ordinator</u></b>
                        </td>
                    </tr>
                    <tr><td>Name: __________________</td><td>Date: __________________</td></tr>
                    <tr><td>Post: __________________</td><td></td></tr>
                    <tr><td>Mobile No: __________________</td><td>Signature: __________________</td></tr>
                </table>";

                    // A page break happens only when we move to the next trade/semester
                    // group — not based on row count. Last group gets no trailing break.
                    // NOTE: left/right sheets are placed in a real <table> row (not
                    // floated divs). wkhtmltopdf/QtWebKit's printing engine has a known
                    // bug where page-break-after combined with float:left containers
                    // inserts a phantom blank page — using a table row for the two-
                    // column layout avoids that entirely.
                    bool isLastGroup = groupIndex == tradeGroups.Count;
                    string pageBreakStyle = isLastGroup ? "page-break-after:auto;" : "page-break-after:always;";

                    allSheets.AppendLine($@"
            <table class='page-layout' style='{pageBreakStyle}'>
                <tr>
                    <td class='sheet-cell sheet-left'>{leftSheet}</td>
                    <td class='divider-cell'></td>
                    <td class='sheet-cell sheet-right'>{rightSheet}</td>
                </tr>
            </table>");
                }

                // ── Full HTML ─────────────────────────────────────────────────────────
                string html = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'/>
        <style>
            * {{ box-sizing: border-box; }}

            body {{
                font-family: Arial, sans-serif;
                font-size: 11px;
                margin: 0;
                padding: 0;
            }}

            .page-layout {{
                width: 100%;
                border-collapse: collapse;
                table-layout: fixed;
                margin-bottom: 6px;
            }}
            .page-layout td.sheet-cell {{
                width: 49.5%;
                vertical-align: top;
                padding: 0 4px;
            }}
            .page-layout td.divider-cell {{
                width: 2px;
                background: #000;
                padding: 0;
            }}

            /* ── Header info table (fixed widths => Subject/Practical never drift) ── */
            .header-table {{
                width: 100%;
                border-collapse: collapse;
                table-layout: fixed;
                margin-bottom: 4px;
            }}
            .header-table td {{
                border: none;
                padding: 3px 4px;
                text-align: left;
                vertical-align: top;
                line-height: 1.3;
                font-size: 11px;
                word-wrap: break-word;
            }}
            .header-table .lbl  {{ font-weight: bold; white-space: nowrap; width: 18%; }}
            .header-table .val  {{ width: 26%; }}
            .header-table .lbl2 {{ font-weight: bold; white-space: nowrap; width: 26%; }}
            .header-table .val2 {{ width: 30%; }}
            .header-table .full {{ padding: 3px 4px; }}

            /* ── Marks table (narrow Roll No, wide In Words, narrow In Fig) ────── */
            .marks-table {{
                width: 100%;
                border-collapse: collapse;
                table-layout: fixed;
                margin-bottom: 6px;
            }}
            .marks-table th {{
                border: 1px solid #000;
                padding: 3px;
                font-size: 11px;
                text-align: center;
            }}
            .marks-table td {{
                border: 1px solid #000;
                padding: 2px 4px;
                font-size: 10.5px;
                height: 17px;
                text-align: center;
            }}
            .marks-table tr {{
                page-break-inside: avoid;
            }}
            .marks-table col.snoCol   {{ width: 28px; }}
            .marks-table col.rollCol  {{ width: 80px; }}
            .marks-table col.wordsCol {{ width: auto; }}
            .marks-table col.figCol   {{ width: 50px; }}
            .marks-table td:nth-child(2) {{ text-align: left; }}
            .marks-table td:nth-child(3) {{ text-align: left; }}

            /* ── Footer table — kept as one unbroken block across page breaks ── */
            .footer-table {{
                width: 100%;
                border-collapse: collapse;
                margin-top: 15px;
                table-layout: fixed;
                page-break-inside: avoid;
            }}
            .footer-table tr {{
                page-break-inside: avoid;
            }}
            .footer-table td {{
                border: none;
                padding: 3px 2px;
                font-size: 10.5px;
                text-align: left;
            }}
            .center {{ text-align: center; }}
        </style>
    </head>
    <body>
        {allSheets}
    </body>
    </html>";

                // ── Convert to PDF ────────────────────────────────────────────────────
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize   = PaperKind.A4,
                Orientation = Orientation.Portrait,
                Margins     = new MarginSettings { Top = 5, Bottom = 5, Left = 5, Right = 5 }
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent  = html,
                    WebSettings  = { DefaultEncoding = "utf-8" },
                    FooterSettings =
                    {
                        FontSize = 7,
                        Center   = "Page [page] of [toPage]",
                        Line     = true
                    }
                }
            }
                };

                byte[] pdfBytes = _converter.Convert(doc);

                return File(pdfBytes, "application/pdf",
                    $"ScoreSheet_{DateTime.Now:yyyyMMddHHmmss}.pdf");  // ← must return application/pdf
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        #region GetGetMarksStatisticsReport

        [HttpPost("GetGetMarksStatisticsReport")]
        public async Task<ApiResult<string>> GetGetMarksStatisticsReport(GetMarksStatisticsModel model)
        {
            ActionName = "GetGetMarksStatisticsReport(GetMarksStatisticsModel model)";
            var result = new ApiResult<string>();
            string ActionType = "";
            try
            {
                // Get report data
                DataTable table = await Task.Run(() =>
     _unitOfWork.ReportRepository.GetGetMarksStatisticsReport(model));

                DataSet data = new DataSet();
                data.Tables.Add(table);

                if (data == null || data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                if (model.Action == "_getMarksStatistics_IA_Report")
                {
                    ActionType = "Internal Assessment";
                }
                if (model.Action == "_getMarksStatistics_Practical_Report")
                {
                    ActionType = "Practical";
                }
                // Generate HTML
                var sb = await _printHtmlFile.GetMarksStatisticsReport_GetHtml(data, 0, ActionType);
                string html = sb.ToString();

                // Remove last page break if present
                string endTag = "<div class='page-break'></div></body></html>";
                if (html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    html = html.Substring(0, html.Length - endTag.Length) + "</body></html>";
                }

                // Create PDF document
                var document = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Landscape,
                Margins = new MarginSettings
                {
                    Top = 10,
                    Bottom = 10,
                    Left = 5,
                    Right = 5
                }
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = html,
                    WebSettings =
                    {
                        DefaultEncoding = "utf-8"
                    },
                    FooterSettings = new FooterSettings
                    {
                        FontName = "Arial",
                        FontSize = 7,
                        Center = "Page [page] of [toPage]",
                        Line = true
                    }
                }
            }
                };

                // Convert HTML to PDF
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(document));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var newException = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(newException, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #endregion

        #region
        [HttpPost("Get85and45percentageStudentIAReport")]
        public async Task<IActionResult> Get85and45percentageStudentIAReport([FromBody] IAReportModel model)
        {
            try
            {

                var mainData = await _unitOfWork.ReportRepository.Get85and45percentageStudentIAReport(model);
                var dataList = CommonFuncationHelper
                                .ConvertDataTable<List<IAReportModel>>(mainData.Tables[0]);
                var above = CommonFuncationHelper
                                .ConvertDataTable<List<StudentSubjectModel>>(mainData.Tables[1]);
                var below = CommonFuncationHelper
                                .ConvertDataTable<List<StudentSubjectModel>>(mainData.Tables[2]);
                if (dataList == null || !dataList.Any())
                    return BadRequest("No data found");

                var endTermName = dataList.First().EndTermName;
                var SemesterName = above.First().SemesterName;
                var SingDate = above.First().SingDate;

                string SemesterNameHindi = "";

                switch (SemesterName)
                {
                    case "1st Semester":
                        SemesterNameHindi = "प्रथम सेमेस्टर";
                        break;

                    case "2nd Semester":
                        SemesterNameHindi = "द्वितीय सेमेस्टर";
                        break;

                    case "3rd Semester":
                        SemesterNameHindi = "तृतीय सेमेस्टर";
                        break;

                    case "4th Semester":
                        SemesterNameHindi = "चतुर्थ सेमेस्टर";
                        break;

                    case "5th Semester":
                        SemesterNameHindi = "पंचम सेमेस्टर";
                        break;

                    case "6th Semester":
                        SemesterNameHindi = "षष्ठम सेमेस्टर";
                        break;

                    case "1st Year":
                        SemesterNameHindi = "प्रथम वर्ष";
                        break;

                    case "2nd Year":
                        SemesterNameHindi = "द्वितीय वर्ष";
                        break;

                    case "3rd Year":
                        SemesterNameHindi = "तृतीय वर्ष";
                        break;

                    default:
                        SemesterNameHindi = SemesterName;
                        break;
                }

                // Group Institute
                var institutes = dataList
                    .GroupBy(x => new
                    {
                        x.InstituteID,
                        x.InstituteCode,
                        x.InstituteName
                    })
                    .ToList();

                StringBuilder reportHtml = new StringBuilder();


                reportHtml.Append($@"
<!DOCTYPE html>
<html lang='hi'>
<head>
<meta charset='UTF-8'>
<title>Report</title>

<style>
body{{
    font-family:'Mangal','Nirmala UI','Arial Unicode MS',sans-serif;
    font-size:14px;
    color:#000000;
    font-weight:600;
    -webkit-font-smoothing:antialiased;
    text-rendering:optimizeLegibility;
    margin:30px;
}}

table{{
    width:100%;
    border-collapse:collapse;
}}

td{{
    vertical-align:top;
    color:#000000;
    font-weight:600;
}}

.center{{ text-align:center; }}
.right{{ text-align:right; }}
.left{{ text-align:left; }}
.bold{{ font-weight:bold; }}

.list td{{
    padding:4px 0;
    font-weight:600;
}}

.footer td{{
    padding-top:40px;
    font-weight:600;
}}
.page-break{{
    page-break-before:always;
    break-before:page;
}}

b{{
    font-weight:800;
}}
</style>

</head>

<body>
");
                int instituteIndex = 1;

                foreach (var institute in institutes)
                {
                    reportHtml.Append($@"


<table>
<tr>


<td width='80%' class='center'>
<b>राजस्थान सरकार</b><br/>
प्राविधिक शिक्षा मण्डल, राजस्थान, जोधपुर W-6 Residency Road, Jodhpur<br/>
Phone : (0291)-2430440,2636572
</td>



</tr>
</table>
<table>
<tr>
<td width='49%' class='left'>
Email : conf.bter@gmail.com
</td>



<td width='49%' class='right'>
Web Site : www.techedu.rajasthan.gov.in
</td>
</tr>
</table>


<table>
<tr>

<td width='50%'>
क्रमांक : एफ (6/14) / गोप/प्रशिम  /{endTermName}/
</td>

<td width='50%' class='right'>
दिनांक :- {SemesterName},{endTermName}
</td>

</tr>
</table>


<table>
<tr>

<td>
<b>पॉलीटेक्निक महाविद्यालय</b><br/>
{institute.Key.InstituteCode} - {institute.Key.InstituteName}
</td>

</tr>
</table>



<table>
<tr>
<td>
<b>
विषय:
</b>
</td>
<td>
<b>
 {SemesterNameHindi}  संकलित सेशनल अंकों में प्रदत्त 85% से अधिक एवं 45% से कम प्राप्तांक वाले विद्यार्थियों का रिकॉर्ड प्रस्तुत करने हेतु।
</b>
</td>
</tr>
</table>

<br/>

<table>
<tr>
<td style='text-align:justify; line-height:24px;'>
विषयान्तर्गत, आपके द्वारा प्रेषित {SemesterNameHindi}, {endTermName} के सेशनल अंकों में 85% से अधिक एवं 45% से कम प्राप्तांक वाले विद्यार्थियों के सेशनल रिकॉर्ड की संस्था स्तर पर पुनः जाँच कर लें। संस्था ऑनलाइन दर्ज अंकों से संतुष्ट होने पर निम्नानुसार रिकॉर्ड प्रस्तुत करें, जिससे प्राप्तांकों का प्रमाणीकरण किया जा सके। यथा
</td>
</tr>
</table>

<br/>

<table class='list'>

<tr>
<td width='50%'>1. Online अंक प्रविष्टि प्रक्रिया</td>
<td width='50%'>2. Online उपस्थिति का आधार</td>
</tr>

<tr>
<td>3. Online कक्षा टेस्ट की उत्तर पुस्तिका</td>
<td>4. Online प्रैक्टिकल पत्रावली</td>
</tr>

<tr>
<td>5. Online  सब्जेक्ट ब्रेकअप</td>
<td>6. उपरोक्त के अलावा अन्य कोई प्रमाण हो</td>
</tr>

</table>

<br/><br/>
");

                    // Branch Wise Students
                    var instituteStudents = above
                        .Where(x => x.InstituteName == institute.Key.InstituteName)
                        .ToList();

                    var branches = instituteStudents
     .GroupBy(x => x.Branch)
     .OrderBy(x => x.Key)
     .ToList();

                    if (!branches.Any() || !branches.SelectMany(x => x).Any())
                    {
                        reportHtml.Append(@"
<div style='text-align:center;  font-weight:bold; margin:20px;'>
    Data not found
</div>");
                    }
                    else
                    {
                        foreach (var branch in branches)
                        {
                            reportHtml.Append($@"
<div style='font-weight:bold; text-align:center; margin:10px 0; font-size:16px;'>
    {branch.Key ?? ""}
</div>");

                            foreach (var student in branch.OrderBy(x => x.RollNo))
                            {
                                reportHtml.Append($@"
<div style='margin-bottom:5px;'>
    <b>{student.RollNo}</b> :
    {string.Join(", ", new[]
                        {
        student.Subject1,
        student.Subject2,
        student.Subject3,
        student.Subject4,
        student.Subject5,
        student.Subject6,
        student.Subject7,
        student.Subject8,
        student.Subject9,
        student.Subject10,
        student.Subject11,
        student.Subject12,
        student.Subject13,
        student.Subject14,
        student.Subject15
    }.Where(x => !string.IsNullOrWhiteSpace(x)))}
</div>");
                            }

                            reportHtml.Append("<br/>");
                        }
                    }
                    //=====================
                    // Footer
                    //=====================

                    reportHtml.Append($@"

<table>

<tr>

<td style='line-height:24px; text-align:justify;'>

उपरोक्त रिकॉर्ड संस्था प्रतिनिधि के माध्यम से दिनांक {SingDate} को निम्न हस्ताक्षरकर्ता के समक्ष प्रस्तुत करें।

</td>

</tr>

</table>

<br/>

<table>

<tr>

<td style='line-height:24px; text-align:justify;'>

रिकॉर्ड प्रस्तुत करने वाले प्रतिनिधि को रिकॉर्ड के संबंध में पूर्ण जानकारी होनी चाहिए, जिससे जाँच के दौरान पूछे गए प्रश्नों का संतोषजनक उत्तर एवं आवश्यक स्पष्टीकरण प्रस्तुत किया जा सके।

निर्धारित तिथि तक उक्त रिकॉर्ड संस्था के प्रतिनिधि के माध्यम से अनिवार्य रूप से भिजवाया जाए, अन्यथा आपकी संस्था का परिणाम रोक दिया जाएगा, जिसकी सम्पूर्ण जिम्मेदारी आपकी की होगी।

</td>

</tr>

</table>

<table class='footer'>

<tr>

<td width='50%'>
<b>(गोपनीय)</b>
</td>

<td width='50%' class='right'>
<b>(रघुनाथ सिंह)</b><br/>
संयुक्त निदेशक (गोपनीय)
</td>

</tr>

</table>



");

                    if (instituteIndex > 0)
                    {
                        reportHtml.Append("<div class='page-break'></div>");
                    }
                    instituteIndex++;
                }

                reportHtml.Append(@"
</body>
</html>
");

                string html = reportHtml.ToString();


                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings =
                            {
                                PaperSize = PaperKind.A4,
                                Orientation = Orientation.Portrait
                            },
                    Objects =
                            {
                                new ObjectSettings
                                {
                                    HtmlContent = html,
                                    WebSettings = { DefaultEncoding = "utf-8" },
                                    FooterSettings = new FooterSettings
                                    {
                                        FontName = "Arial",
                                        FontSize = 9,
                                        Center = " [page] / [toPage]",

                                        Line = true
                                    }
                                }
                            }
                };

                byte[] pdf = _converter.Convert(doc);
                return File(pdf, "application/pdf", "Get85and45percentageStudentIAReport.pdf");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        #endregion

        #region GetToppersReport

        [HttpPost("GetToppersReport")]
        public async Task<ApiResult<string>> GetToppersReport(ToppersModel model)
        {
            ActionName = "GetToppersReport(ToppersModel model)";
            var result = new ApiResult<string>();
            string ActionType = "";
            try
            {
                // Get report data
                DataTable table = await Task.Run(() =>
     _unitOfWork.ReportRepository.GetToppersReport(model));

                DataSet data = new DataSet();
                data.Tables.Add(table);

                if (data == null || data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                // Generate HTML
                var sb = await _printHtmlFile.GetToppersReport_Html(data, 0, ActionType);
                string html = sb.ToString();

                // Remove last page break if present
                string endTag = "<div class='page-break'></div></body></html>";
                if (html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    html = html.Substring(0, html.Length - endTag.Length) + "</body></html>";
                }

                // Create PDF document
                var document = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Landscape,
                Margins = new MarginSettings
                {
                    Top = 10,
                    Bottom = 10,
                    Left = 5,
                    Right = 5
                }
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = html,
                    WebSettings =
                    {
                        DefaultEncoding = "utf-8"
                    },
                    FooterSettings = new FooterSettings
                    {
                        FontName = "Arial",
                        FontSize = 7,
                        Center = "Page [page] of [toPage]",
                        Line = true
                    }
                }
            }
                };

                // Convert HTML to PDF
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(document));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var newException = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(newException, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #endregion


        #region GetProvesionalMeritList

        [HttpPost("GetProvesionalMeritList")]
        public async Task<ApiResult<string>> GetProvesionalMeritList(GetProvesionalMeritModel model)
        {
            ActionName = "GetProvesionalMeritList(ToppersModel model)";
            var result = new ApiResult<string>();

            try
            {
                // Get report data
                DataTable table = await Task.Run(() =>
     _unitOfWork.ReportRepository.GetProvesionalMeritList(model));

                DataSet data = new DataSet();
                data.Tables.Add(table);

                if (data == null || data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                // Generate HTML
                var sb = await _printHtmlFile.GetProvesionalMeritList_Html(data, 0, model.Action);
                string html = sb.ToString();

                // Remove last page break if present
                string endTag = "<div class='page-break'></div></body></html>";
                if (html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    html = html.Substring(0, html.Length - endTag.Length) + "</body></html>";
                }

                // Create PDF document
                var document = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Landscape,
                Margins = new MarginSettings
                {
                    Top = 10,
                    Bottom = 10,
                    Left = 5,
                    Right = 5
                }
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = html,
                    WebSettings =
                    {
                        DefaultEncoding = "utf-8"
                    },
                    FooterSettings = new FooterSettings
                    {
                        FontName = "Arial",
                        FontSize = 7,
                        Center = "Page [page] of [toPage]",
                        Line = true
                    }
                }
            }
                };

                // Convert HTML to PDF
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(document));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var newException = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(newException, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #endregion
        #region GetCheck_Merit_List
        [HttpPost("GetCheck_Merit_List")]
        public async Task<ApiResult<DataTable>> GetCheck_Merit_List(GetProvesionalMeritModel model)
        {
            ActionName = "GetCheck_Merit_List(ToppersModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetCheck_Merit_List(model));
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
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                var newException = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(newException, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion

        #region ApprenticeshipFresherReports

        [HttpPost("ApprenticeshipFresherReports")]
        public async Task<ApiResult<string>> ApprenticeshipFresherReports(ApprenticeshipRegistrationSearchModal model)
        {
            ActionName = "ApprenticeshipFresherReports(ApprenticeshipRegistrationSearchModal model)";
            var result = new ApiResult<string>();
            try
            {
                // Get report data
                DataTable table = await Task.Run(() =>
     _unitOfWork.ReportRepository.ApprenticeshipFresherReports(model));

                DataSet data = new DataSet();
                data.Tables.Add(table);

                if (data == null || data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                // Generate HTML
                var sb = await _printHtmlFile.GetApprenticeshipFresherReports_Html(data, 0);
                string html = sb.ToString();

                // Remove last page break if present
                string endTag = "<div class='page-break'></div></body></html>";
                if (html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    html = html.Substring(0, html.Length - endTag.Length) + "</body></html>";
                }

                // Create PDF document
                var document = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Landscape,
                Margins = new MarginSettings
                {
                    Top = 10,
                    Bottom = 10,
                    Left = 5,
                    Right = 5
                }
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = html,
                   WebSettings =
{
    DefaultEncoding = "utf-8",
    PrintMediaType = true,
    LoadImages = true,
    EnableIntelligentShrinking = false
},
                }
            }
                };

                // Convert HTML to PDF
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(document));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var newException = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(newException, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #endregion


        #region Bulk Student Diploma Certificate
        [HttpPost("StudentDiplomaCertificateDownloadChunk")]
        public async Task<ApiResult<string>> StudentDiplomaCertificateDownloadChunk([FromBody] List<DiplomaCertificateDownloadSearchModel> Model)
        {
            ActionName = "StudentDiplomaCertificateDownloadChunk([FromBody] List<DiplomaCertificateDownloadSearchModel> Model)";

            var result = new ApiResult<string>();
            var logfilename = "_DiplomaCertificateDownload";
            var Session = string.Empty;
            try
            {
                Session = Model[0].SessionName;
                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{Constants.FinalDiplomaFolder}/{Session}";

                // store students that have filename success for merge file for shwoing marksheet
                List<GenerateFinalDiplomaCertificateModel> ListData = new List<GenerateFinalDiplomaCertificateModel>();
                // store students that dont have filename failed any resion
                List<GenerateFinalDiplomaCertificateModel> notGenerateStudents = new List<GenerateFinalDiplomaCertificateModel>();

                int i = 1;
                // passed students list in chunks
                foreach (var student in Model)
                {
                    CommonFuncationHelper.WriteTextLog($"--------------------- main loop start: {i} ------------------------", logfilename);
                    try
                    {
                        CommonFuncationHelper.WriteTextLog($"1. model student loop : {student.StudentName}", logfilename);
                        GenerateFinalDiplomaCertificateModel objStudent = new GenerateFinalDiplomaCertificateModel();
                        // already have saved file
                        if (student.Dis_FileName != "")
                        {
                            CommonFuncationHelper.WriteTextLog($"1.1. already file exists in if : {student.StudentName}", logfilename);
                            // for merge
                            objStudent.StudentID = student.StudentID;
                            objStudent.RollNo = student.RollNo;
                            objStudent.FileName = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{Constants.FinalDiplomaFolder}/{student.FileName}";
                            objStudent.Dis_FileName = student.Dis_FileName;
                            // add
                            ListData.Add(objStudent);
                        }
                        else
                        {
                            CommonFuncationHelper.WriteTextLog($"1.2. dosenot file exists in else : {student.StudentName}", logfilename);

                             //create folder
                            if (!System.IO.Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            string timestamp_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                            var fileName = $"StudentDiplomaCertificate_{student.StudentName}_{timestamp_str}.pdf";
                            string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{Constants.DepartmentBterFolder}/{Constants.FinalDiplomaFolder}/{Session}/{fileName}";

                            // get html
                            var sb = await _printHtmlFile.GetHtmlOfDiplomaCertificate(student);
                            var _html = sb.ToString();

                            // remove last blank page
                            string endTag = "<div class='page-break'></div></body></html>";
                            if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                            {
                                _html = _html.Substring(0, _html.Length - endTag.Length)
                                             + "</body></html>";
                            }

                            var pdfBytes = await _pdfService.GenerateAsync(_html,
                                new PdfOptions
                                {
                                    Format = "A4",
                                    MarginTop = "10mm",
                                    MarginBottom = "0mm",
                                    MarginLeft = "10mm",
                                    MarginRight = "10mm",
                                    PrintBackground = true
                                });

                            await System.IO.File.WriteAllBytesAsync(filepath, pdfBytes);

                            CommonFuncationHelper.WriteTextLog($"1.4. save file in folder: {student.StudentName}", logfilename);

                            // create an object for new record
                            FinalDiplomaCertificateSaveDataModel objFinalDiploma = new FinalDiplomaCertificateSaveDataModel();

                            objFinalDiploma.FinalDiploma = student.FinalDiplomaID ?? 0;// pk

                            objFinalDiploma.Enrollment = Convert.ToString(student.EnrollmentNo) ?? string.Empty;
                            objFinalDiploma.InstituteId = Convert.ToInt32(student.InstituteID);
                            //objFinalDiploma.SrDiploma = Convert.ToInt32(student.SrDiploma);
                            objFinalDiploma.SRNO = Convert.ToString(student.SRNO);
                            objFinalDiploma.PublishDate = Convert.ToString(student.PublishDate);
                            objFinalDiploma.IsLocked = Convert.ToByte(student.IsLocked);
                            objFinalDiploma.DiplomaPrintingDate = Convert.ToString(student.DiplomaPrintingDate);
                            objFinalDiploma.IsRwhResult = Convert.ToByte(student.IsRWHResult);
                            objFinalDiploma.RwhResultId = Convert.ToInt32(student.RWHResultID);
                            objFinalDiploma.IsReval = Convert.ToByte(student.IsReval);
                            objFinalDiploma.IsRevisedIssueDate = Convert.ToByte(student.IsRevisedIssueDate);
                            objFinalDiploma.ResultId = Convert.ToInt32(student.ExamResultID);
                            objFinalDiploma.RevisedId = Convert.ToInt32(student.RevisedId);
                            objFinalDiploma.IsBlock = Convert.ToByte(student.IsBlock);
                            objFinalDiploma.StudentId = Convert.ToInt32(student.StudentID);
                            objFinalDiploma.IsDiploma = Convert.ToByte(student.IsDiploma);
                            objFinalDiploma.IsDuplicate = Convert.ToByte(student.IsDuplicate);
                            objFinalDiploma.DuplicateDiplomaId = Convert.ToInt32(student.DuplicateDiplomaId);
                            objFinalDiploma.RequestId = Convert.ToInt32(student.RequestId);
                            objFinalDiploma.IsIssued = Convert.ToByte(student.IsIssued);
                            objFinalDiploma.ResultTypeID = Convert.ToInt32(student.ResultTypeID);
                            objFinalDiploma.EndTermID = Convert.ToInt32(student.EndTermID);
                            objFinalDiploma.EffectiveEndTermID = Convert.ToInt32(student.EffectiveEndTermID);
                            objFinalDiploma.IsRevised = Convert.ToBoolean(student.IsRevised);
                            objFinalDiploma.SemesterID = Convert.ToInt32(student.SemesterID);
                            objFinalDiploma.IPAddress = CommonFuncationHelper.GetIpAddress();
                            objFinalDiploma.ModifyBy = Convert.ToInt32(student.ModifyBy);

                            objFinalDiploma.Dis_FileName = fileName;
                            objFinalDiploma.FileName = $"{Session}/{fileName}";


                            // save
                            await _unitOfWork.MarksheetDownloadRepository.AddUpdateFinalDiplomaCertificate(objFinalDiploma);
                            await _unitOfWork.SaveChangesAsync();

                            CommonFuncationHelper.WriteTextLog($"1.8. save student done : {student.StudentName}", logfilename);


                            // for merge
                            objStudent.StudentID = student.StudentID;
                            objStudent.RollNo = student.RollNo;
                            objStudent.EnrollmentNo = student.EnrollmentNo;
                            objStudent.FileName = filepath;
                            objStudent.Dis_FileName = fileName;
                            // add
                            ListData.Add(objStudent);

                        }
                    }
                    catch (Exception ex1)
                    {
                        await _unitOfWork.DisposeAsync();

                        // add in list
                        notGenerateStudents.Add(new GenerateFinalDiplomaCertificateModel
                        {
                            StudentID = student.StudentID,
                            EnrollmentNo = student.EnrollmentNo
                        });

                        CommonFuncationHelper.WriteTextLog($"2. loop error for student : {student.StudentName}", logfilename);
                        CommonFuncationHelper.WriteTextLog($"2.1. loop error : {ex1.Message}", logfilename);
                    }

                    CommonFuncationHelper.WriteTextLog($"--------------------- main loop end: {i} ------------------------", logfilename);
                    i++;
                }// end student loop

                #region "Save Multiple PDF PAGES"
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string outputFile = $"DiplomaCertificate_{timestamp}.pdf";
                string outputPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{outputFile}";
                List<string?> strSoureFiles = ListData.Select(s => s.FileName)?.ToList();

                // merge all files
                var ismerged = await MergePdfFilesAsync(strSoureFiles, outputPath);
                CommonFuncationHelper.WriteTextLog($"3. merge done with file count : {strSoureFiles?.Count == 0} and flage : {ismerged}", logfilename);
                if (ismerged)
                {
                    result.Data = outputFile;
                    result.State = EnumStatus.Success;
                    var msg = Constants.MSG_DATA_LOAD_SUCCESS;
                    if (notGenerateStudents?.Count > 0)
                    {
                        msg = $"{Constants.MSG_FILE_DOWNLOAD_SUCCESS}, Except these students (Enrollment No.):<br/> {string.Join(", ", notGenerateStudents.Select(s => s.EnrollmentNo))}";
                    }
                    result.Message = msg;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    var msg = Constants.MSG_ERROR_IN_MERGING_FILES;
                    if (strSoureFiles == null || strSoureFiles?.Count == 0)
                    {
                        msg = Constants.MSG_FILE_NOT_FOUND;
                    }
                    result.Message = msg;
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonFuncationHelper.WriteTextLog($"4. main error : {ex.Message}", logfilename);

                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
                // Write error log
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
        #endregion


        #region GetGuestHouseSlip

        [HttpPost("GetGuestHouseSlip")]
        public async Task<ApiResult<string>> GetGuestHouseSlip(GeustHouseSlipModule model)
        {
            ActionName = "GetGuestHouseSlip(GeustHouseSlipModule model)";
            var result = new ApiResult<string>();
            try
            {
                DataTable table = await Task.Run(() =>
     _unitOfWork.ReportRepository.GetGuestHouseSlip(model));
                DataSet data = new DataSet();
                data.Tables.Add(table);
                if (data == null || data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                // Generate HTML
                var sb = await _printHtmlFile.GetGuestHouseSlip_Html(data, 0);
                string html = sb.ToString();
                // Remove last page break if present
                string endTag = "<div class='page-break'></div></body></html>";
                if (html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    html = html.Substring(0, html.Length - endTag.Length) + "</body></html>";
                }
                // Create PDF document
                var document = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Landscape,
                Margins = new MarginSettings
                {
                    Top = 10,
                    Bottom = 10,
                    Left = 5,
                    Right = 5
                }
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = html,
                    WebSettings =
                    {
                        DefaultEncoding = "utf-8"
                    },
                    FooterSettings = new FooterSettings
                    {
                        FontName = "Arial",
                        FontSize = 7,
                        Center = "Page [page] of [toPage]",
                        Line = true
                    }
                }
            }
                };

                // Convert HTML to PDF
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(document));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var newException = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(newException, _unitOfWork);

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #endregion


        #region GetITI_FinalReport
        [HttpPost("GetITI_FinalReport")]
        public async Task<ApiResult<DataTable>> GetITI_FinalReport(ITI_FinalReportModule model)
        {
            ActionName = "GetITI_FinalReport(ITI_FinalReportModule model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetITI_FinalReport(model));
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
                await _unitOfWork.DisposeAsync();
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
        #endregion
    }

}
