using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.InkML;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.IO.Compression;
using static Kaushal_Darpan.Core.Helper.CommonFuncationHelper;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class DTEApplicationDashboardController : BaseController
    {
        public override string PageName => "DTEApplicationDashboardController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DTEApplicationDashboardController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetDTEDashboard")]
        public async Task<ApiResult<DataTable>> GetDTEDashboard([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetDTEDashboard()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.DTEApplicationDashboardRepository.GetDTEDashboard(body);
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

        [HttpPost("download-student")]
        public async Task<IActionResult> DownloadStudent(DownloadZipDocumentModel request)
        {
            try
            {
                string targetFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, "Students", "BTER", request.FinancialYearID.ToString(), request.Eng_NonEng.ToString());
                string rootStartPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, "Students");
                string zipFileName = "Students.zip";

                if (!Directory.Exists(targetFolder))
                {
                    return Content("Folder not found!");
                }

                string[] files = Directory.GetFiles(targetFolder, "*.*", SearchOption.AllDirectories);

                //files
                if (files.Length == 0)
                {
                    return Content("File not found!");
                }

                using (MemoryStream zipStream = new MemoryStream())
                {
                    using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var filePath in files)
                        {
                            string relativePath = Path.GetRelativePath(rootStartPath, filePath);
                            zip.CreateEntryFromFile(filePath, relativePath);
                        }
                    }

                    zipStream.Position = 0;

                    HttpContext.Response.Clear();
                    HttpContext.Response.ContentType = "application/zip";
                    HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{zipFileName}\"");

                    await zipStream.CopyToAsync(HttpContext.Response.Body);

                    return new EmptyResult(); // Response is already written
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
                return Content("An error occurred while generating the PDF!");
            }
        }


        [HttpPost("download-zip-folder")]
        public async Task<IActionResult> DownloadZipFolder([FromBody] DownloadFileModel body)
        {
            var uploadFolderName = Path.Combine(
                ConfigurationHelper.StaticFileRootPath,
                "Students", "BTER", body.FinancialYearID.ToString(), body.Eng_NonEng.ToString());

            if (!Directory.Exists(uploadFolderName))
            {
                return NotFound("The directory does not exist.");
            }

            string zipFileName = uploadFolderName + ".zip";

            if (System.IO.File.Exists(zipFileName))
            {
                System.IO.File.Delete(zipFileName);
            }

            ZipFile.CreateFromDirectory(uploadFolderName, zipFileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(zipFileName, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, "application/zip", Path.GetFileName(zipFileName));
        }


        [HttpPost("GetAllApplication")]
        public async Task<ApiResult<DataTable>> GetAllApplication([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetAllApplication()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.DTEApplicationDashboardRepository.GetAllApplication(body);
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

        [HttpPost("GetDTEDashboardReports")]
        public async Task<ApiResult<DataTable>> GetDTEDashboardReports([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetDTEDashboardReports()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.DTEApplicationDashboardRepository.GetDTEDashboardReports(body);
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

        [HttpPost("GetDTEDashboardReportsNew")]
        public async Task<ApiResult<DataTable>> GetDTEDashboardReportsNew([FromBody] DTEAdminDashApplicationSearchModel body)
        {
            ActionName = "GetDTEDashboardReportsNew()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.DTEApplicationDashboardRepository.GetDTEDashboardReportsNew(body);
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

        [HttpPost("DownloadFeePaidStudentDoc")]
        public async Task<IActionResult> DownloadFeePaidStudentDoc(DownloadZipDocumentModel request)
        {
            try
            {
                string targetFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, "Students", "BTER", request.FinancialYear, request.Eng_NonEng.ToString());
                string rootStartPath = Path.Combine(ConfigurationHelper.StaticFileRootPath, "Students");
                string zipFileName = "Students.zip";

                //folder
                if (!Directory.Exists(targetFolder))
                {
                    return Content("Folder not found!");
                }

                List<string> files = new List<string>();
                foreach (var ApplicationID in request.ApplicationIDs)
                {
                    string targetAppIdFolder = Path.Combine(targetFolder, ApplicationID.ToString());
                    if (Directory.Exists(targetAppIdFolder))
                    {
                        var file = Directory.GetFiles(targetAppIdFolder, "*.*", SearchOption.AllDirectories);
                        files.AddRange(file);
                    }
                }

                //files
                if (files.Count == 0)
                {
                    return Content("File not found!");
                }

                using (MemoryStream zipStream = new MemoryStream())
                {
                    using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var filePath in files)
                        {
                            string relativePath = Path.GetRelativePath(rootStartPath, filePath);
                            zip.CreateEntryFromFile(filePath, relativePath);
                        }
                    }

                    zipStream.Position = 0;
                    //return File(zipStream, "application/zip", zipFileName);

                    HttpContext.Response.Clear();
                    HttpContext.Response.ContentType = "application/zip";
                    HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{zipFileName}\"");

                    await zipStream.CopyToAsync(HttpContext.Response.Body);

                    return new EmptyResult(); // Response is already written
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
                return Content("An error occurred while generating the PDF!");
            }
        }

    }
}
