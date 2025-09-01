using AspNetCore.Reporting;
using AspNetCore.Reporting.ReportExecutionService;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using iTextSharp.tool.xml.html;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BterApplication;
using Kaushal_Darpan.Models.BterCertificateReport;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.FlyingSquad;
using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.ItiStudentActivities;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.OptionalFormatReport;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.studentve;
using Kaushal_Darpan.Models.TheoryMarks;
using Kaushal_Darpan.Models.TimeTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.Json;
using System.util;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //public ReportController(IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        public ReportController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_emailService = emailService;
        }

        [HttpPost("GetAllDataRpt")]
        public async Task<ApiResult<DataTable>> GetAllDataRpt([FromBody] TheorySearchModel body)
        {
            ActionName = "GetAllData([FromBody] TheorySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetAllDataRpt(body));
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

                            foreach (DataRow row in data.Tables[0].Rows)
                            {
                                var SignatureFileName = Convert.ToString(row["SignatureFile"]);
                                var fullPhotoPath = Path.Combine(ConfigurationHelper.RootPath, "StaticFiles", Convert.ToString(SignatureFileName));

                                //byte[] imageBytes;

                                if (System.IO.File.Exists(fullPhotoPath))
                                {
                                    row["SignatureFile1"] = System.IO.File.ReadAllBytes(fullPhotoPath); // This must be byte[]
                                }
                                else
                                {
                                    var defaultPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder, "default.jpg");
                                    row["SignatureFile1"] = System.IO.File.Exists(defaultPath) ? System.IO.File.ReadAllBytes(defaultPath) : null;
                                }

                                if (row["SignatureFile1"] != DBNull.Value && row["SignatureFile1"] is byte[] photoBytes)
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
                    _unitOfWork.Dispose();

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
            ActionName = "GetStudentAdmitCardBulk(string EnrollmentNo)";
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
                        if (!string.IsNullOrEmpty(StudentExamID))
                        {
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

                                    var newp = "https://kdhteapi.rajasthan.gov.in/Api/StaticFiles/";
                                    string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                                    data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                                    //ConfigurationHelper.StaticFileRootPath
                                    string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                                    data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                                    string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
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
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = Constants.MSG_DATA_NOT_FOUND;
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
                    _unitOfWork.Dispose();
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
            ActionName = "GetStudentAdmitCardBulk(string EnrollmentNo)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            string ipaddress = CommonFuncationHelper.GetIpAddress();
            string iStudentExamID = "";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var ListInsituteData = await _unitOfWork.GenerateAdmitCardRepository.GetGenerateAdmitCardDataBulk_InsituteWise(Model);
                    if (ListInsituteData.Count > 0)
                    {
                        foreach (var childdata in ListInsituteData)
                        {
                            List<GenerateAdmitCardModel> ListData = new List<GenerateAdmitCardModel>();
                            //set data
                            Model.SemesterID = childdata.SemesterID;
                            Model.InstituteID = childdata.InstituteID;
                            Model.DepartmentID = 1;
                            Model.EndTermID = childdata.EndTermID;
                            Model.Eng_NonEng = childdata.Eng_NonEng;
                            Model.TotalRecord = childdata.TotalRecord;
                            //semester wise Data
                            foreach (var StudentExamID in childdata.StudentExamIDs.Split(','))
                            {
                                if (!string.IsNullOrEmpty(StudentExamID))
                                {
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
                                            string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                                            data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));


                                            string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
                                            data.Tables[0].Rows[0]["StudentSign"] = System.IO.File.ReadAllBytes(CheckFileExisits(stusignFilepath));

                                            string registrar_signFilepath = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["RegistrarSignFileName"]}";
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

                                        }
                                    }

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
                                    _unitOfWork.SaveChanges();

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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
        [HttpGet("GetCollegesWiseReports")]
        public async Task<ApiResult<DataTable>> GetCollegesWiseReports()
        {
            ActionName = "GetCollegesWiseReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCollegesWiseReports();
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
                _unitOfWork.Dispose();
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
        [HttpGet("GetStudentFeeReceipt/{EnrollmentNo}")]
        public async Task<ApiResult<string>> GetStudentFeeReceipt(string EnrollmentNo)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentFeeReceipt(EnrollmentNo);
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

                        string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["Studentimg"]}";
                        data.Tables[0].Rows[0]["StudentImgb"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSign"]}";
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



            ActionName = "GetExaminationForm(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetExaminationForm(model);
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
                        string stuimgFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                        data.Tables[0].Rows[0]["StudentImg"] = System.IO.File.ReadAllBytes(CheckFileExisits(stuimgFilepath));

                        //string stuimgFilepath = $"{CommonFuncationHelper.GetStudentFilesForOldBter(iCourseType, bisyearly, istudentId)}/{data.Tables[0].Rows[0]["StudentImgFileName"]}";
                        //data.Tables[0].Rows[0]["StudentImg"] = await GetByteImages(stuimgFilepath);

                        //temp comment
                        string stusignFilepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.StudentsFolder}/{data.Tables[0].Rows[0]["StudentSignFileName"]}";
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
                return result;
            });
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
                return result;
            });
        }



        [HttpPost("DownloadStudentRollNumber_InsituteWise")]
        public async Task<ApiResult<string>> DownloadStudentRollNumber_InsituteWise(DownloadnRollNoModel Request)
        {
            ActionName = "DownloadStudentRollNumber(string EnrollmentNo)";

            string ipaddress = CommonFuncationHelper.GetIpAddress();
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var Model = await _unitOfWork.GenerateRollRepository.GetGenerateRollDataForPrint_Insitute(Request);
                    if (Model.Count > 0)
                    {
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

                                dtStudentExamDetails.Rows.Add(
                                    StudentExamID.StudentType,
                                    StudentExamID.InstituteNameEnglish,
                                    StudentExamID.EndTermName,
                                    StudentExamID.FinancialYearName,
                                    StudentExamID.CenterName,
                                    StudentExamID.BranchCode);
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
                                    result.Message = "Success.";


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
                            }
                            else
                            {
                                result.State = EnumStatus.Error;
                                result.ErrorMessage = "Something went wrong";
                            }
                            #endregion
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
                return result;
            });
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
        //        _unitOfWork.Dispose();
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

                await Task.Run(() =>
                {
                    using (FileStream stream = new FileStream(poutputPath, FileMode.Create))
                    using (Document document = new Document())
                    using (PdfCopy pdfCopy = new PdfCopy(document, stream))
                    {
                        document.Open();

                        foreach (var file in sourceFiles)
                        {
                            using (PdfReader reader = new PdfReader(file))
                            {
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    PdfImportedPage page = pdfCopy.GetImportedPage(reader, i);
                                    pdfCopy.AddPage(page);
                                }
                            }
                        }
                    }
                });
                bRetValue = true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                return result;
            });
        }

        #endregion

        #region Colleges Wise Examination Reports
        [HttpGet("GetCollegesWiseExaminationReports")]
        public async Task<ApiResult<DataTable>> GetCollegesWiseExaminationReports()
        {
            ActionName = "GetCollegesWiseExaminationReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.GetCollegesWiseExaminationReports();
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
                _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();

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
                    _unitOfWork.Dispose();

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
                    if (data?.Tables?.Count == 5)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //  var fileName = $"ITIApplicationForm_{Model.StudentName}_{Model.ApplicationID}.pdf";
                        var fileName = $"ITIApplicationForm_{Model.ApplicationID}_{Guid.NewGuid()}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIApplicationForm.rdlc";

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
                                Model.DepartmentID);
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
                                        Model.DepartmentID);
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
                                    _unitOfWork.SaveChanges();

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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                    var data = await _unitOfWork.ReportRepository.GetStudentMarksheet(student);
                    if (data?.Tables?.Count == 3)
                    {
                        //report
                        var fileName = $"StudentMarksheet_{student.StudentID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/StudentMarksheet.rdlc";

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
                return result;
            });
        }
        #endregion

        #region Download Appeared Passed Institute Wise
        [HttpPost("DownloadAppearedPassedInstitutewise")]
        public async Task<ApiResult<string>> DownloadAppearedPassedInstitutewise(DownloadAppearedPassed model)
        {
            ActionName = "DownloadAppearedPassed(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadAppearedPassedInstitutewise(model);
                    if (data.Rows?.Count > 1)
                    {
                        //report
                        var fileName = $"AppearedPassedInstituteWise.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/AppearedPassedStatisticsInstituteWise.rdlc";

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
                return result;
            });
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
                return result;
            });
        }
        #endregion

        #region Download Blank Report
        [HttpPost("GetBlankReport")]
        public async Task<ApiResult<string>> GetBlankReport(BlankReportModel model)
        {
            ActionName = "GetBlankReport(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetBlankReport(model);
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
                return result;
            });
        }
        #endregion

        #region Paper-Count-Customize-Report-Columns-And-List
        [HttpPost("PaperCountCustomizeReportColumnsAndList")]
        public async Task<ApiResult<DataTable>> PaperCountCustomizeReportColumnsAndList(ReportCustomizeBaseModel model)
        {
            ActionName = "PaperCountCustomizeReportColumns()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ReportRepository.PaperCountCustomizeReportColumnsAndList(model);
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
            ActionName = "GetExaminerReportAndMarksTracking()";
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
                _unitOfWork.Dispose();
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
            ActionName = "GetExaminerReportAndMarksTrackingStudent()";
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                return result;
            });
        }

        #endregion

        #region BTER 13B Attendance Report
        [HttpPost("AttendanceReport13B")]
        public async Task<ApiResult<string>> AttendanceReport13B(AttendanceReport13BDataModel model)
        {
            ActionName = "AttendanceReport13B()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.AttendanceReport13B(model);

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
                return result;
            });
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

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Daily_Report_Bhandar_Form1", data.Tables[0]);
                        localReport.AddDataSource("BhandarForm_DataTabl2", data.Tables[1]);
                        localReport.AddDataSource("Daily_Report_Bhandar_Form_UFM", data.Tables[2]);
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();

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
                    _unitOfWork.SaveChanges();
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
                _unitOfWork.Dispose();

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
                return result;
            });
        }
        #endregion






        [HttpPost("ScaReportAdmin")]
        public async Task<ApiResult<DataTable>> ScaReportAdmin([FromBody] StudentCenteredActivitesMasterSearchModel body)
        {
            ActionName = "GetAllData([FromBody] StudentCenteredActivitesMasterSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.ScaReportAdmin(body));
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
                _unitOfWork.Dispose();
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
            ActionName = "StatisticsInformationReportPdf()";
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
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("Statistical_Information", data.Tables[0]);
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
                    _unitOfWork.Dispose();
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
            ActionName = "TheorymarksReportPdf()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.TheorymarksReportPdf_BTER(filterModel);

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
                                GroupCode = row["GroupCode"],
                                Branch = row["BranchName"],
                                SubjectCode = row["SubjectCode"]
                            });

                        // Initialize a list to store the individual PDF file paths
                        List<string> pdfFiles = new List<string>();
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        foreach (var group in groupedData)
                        {
                            // Get data for this specific group
                            var groupData = group.CopyToDataTable();

                            var fileName = $"{group.Key.GroupCode}_{group.Key.Branch}_{group.Key.SubjectCode}_TheoryReport_{timestamp}.pdf";
                            string filepath = $"{folderPath}/{fileName}";

                            string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Theory_Marks_Report.rdlc";

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
                    _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
            ActionName = "Report33";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.Report23(model);

                    if (data != null)
                    {
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
                return result;
            });
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                return result;
            });
        }

        #endregion

        #region ITI GetITIStudent_Marksheet
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
                        //var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        ////report
                        //var fileName = $"JoiningLetter_{model.UserID}_{model.StaffID}.pdf";
                        //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images
                        data.Tables[0].TableName = "GetITIStudent_Marksheet_SingleDetails";

                        data.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        data.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];
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


                        data.Tables[1].TableName = "GetITIStudent_Marksheet_Details";

                        string devFontSize = "15px";
                        /*default font size for kruti dev*/
                        //string fontSize = "font-size: 10px;";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/ITIMarksheet.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
                        sb1.Append(html);


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);

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
                _unitOfWork.Dispose();
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
                        data.Tables[1].TableName = "Consolidated_Marksheet";
                        decimal Total_Ob = 0;
                        decimal Total_Mx = 0;
                        foreach (DataRow dr in data.Tables[1].Rows)
                        {
                            Total_Ob += Convert.ToDecimal(dr["Total_Ob"].ToString());
                            Total_Mx += Convert.ToDecimal(dr["Total_Mx"].ToString());
                        }
                        data.Tables[0].Rows[0]["Percentage"] = Math.Round(( Total_Ob / Total_Mx * 100),2).ToString();
                        

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ITIMarksheetCONSOLIDATED.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                return result;
            });
        }

        [HttpPost("DownloadResultStatisticsReport")]
        public async Task<ApiResult<string>> DownloadResultStatisticsReport(StatisticsBridgeCourseModel model)
        {
            ActionName = "DownloadAppearedPassed(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.DownloadResultStatisticsReport(model);
                    if (data.Rows?.Count > 0)
                    {
                        //report
                        var fileName = $"ResultStatisticsReports.pdf";
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ResultStatisticsReports.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ResultStatisticsReports", data);
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
                return result;
            });
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
                return result;
            });
        }
        #endregion


        // [RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("GetBterBranchWiseStatisticalReport")]
        public async Task<ApiResult<string>> GetBterBranchWiseStatisticalReport([FromBody] BterStatisticsReportDataModel model)
        {
            ActionName = "GetBterBranchWiseStatisticalReport(BterStatisticsReportDataModel)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetBterBranchWiseStatisticalReport(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "BranchWiseStatistical";



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
                return result;
            });
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                return result;
            });
        }
        #endregion

        #region Apprenticeship  registratuion Passout Report
        [HttpPost("ApprenticeshipPassoutReport")]
        public async Task<ApiResult<string>> ApprenticeshipPassoutReport(ApprenticeshipRegistrationSearchModal model)
        {
            ActionName = "ApprenticeshipPassoutReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.ApprenticeshipPassoutReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"ApprenticeshipPassoutReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ApprenticeshipPassoutReport.rdlc";
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
                return result;
            });
        }
        #endregion

        #region Apprenticeship  registratuion List Report
        [HttpPost("ApprenticeshipReport")]
        public async Task<ApiResult<string>> ApprenticeshipReport(ApprenticeshipRegistrationSearchModal model)
        {
            ActionName = "ApprenticeshipReport()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.ApprenticeshipReport(model);
                    if (data != null)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        //report
                        //var fileName = $"AllotmentFeeReceipt_{EnrollmentNo}.pdf";
                        var fileName = $"ApprenticeshipReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ApprenticeshipReport.rdlc";
                        //
                        var qrcode = CommonFuncationHelper.GenerateQrCode("this is devit");
                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ApprenticeshipListReport", data.Tables[0]);
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
                return result;
            });
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
                        localReport.AddDataSource("PmnamMelaReport", data.Tables[0])    ;
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
        //        _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                _unitOfWork.Dispose();
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
                return result;

            });
        }
        #endregion

        #region Student Duplicate Marksheet
        [HttpPost("GetStudentDuplicateMarksheet")]
        public async Task<ApiResult<string>> GetStudentDuplicateMarksheet([FromBody] MarksheetDownloadSearchModel student)
        {
            ActionName = "GetStudentDuplicateMarksheet([FromBody] MarksheetDownloadSearchModel student)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentDuplicateMarksheet(student);
                    if (data?.Tables?.Count == 3)
                    {
                        //report
                        var fileName = $"StudentMarksheet_{student.StudentID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
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
                _unitOfWork.Dispose();
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



        [HttpPost("GetStudentWithdranSeat")]
        public async Task<ApiResult<DataSet>> GetStudentWithdranSeat([FromBody] AllotmentReportCollegeRequestModel model)
        {
            ActionName = "GetStudentWithdranSeat([FromBody] AllotmentReportCollegeRequestModel model)";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ReportRepository.GetStudentWithdranSeat(model));
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
                _unitOfWork.Dispose();
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



    }
}