using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.UploadFileWithPathData;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Headers;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class FileUploadMasterController : BaseController
    {
        public override string PageName => "CommonFunctionController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FileUploadMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("UploadFile"), DisableRequestSizeLimit]
        public async Task<ApiResult<string>> UploadFile([FromForm] UploadFileMasterModel model)
        {
            ActionName = "UploadFile([FromForm] UploadFileMasterModel model)";
            var result = new ApiResult<string>();
            try
            {
                // Step 1: Validate file presence
                if (model.file == null || model.file.Length == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_INVALID_REQUEST;
                    return result;
                }

                // only for page management
                if (model.ForPGMK == true)
                {
                    model.FolderName = "PGMK";
                }

                // Step 2: Create upload folder if not exists
                var uploadFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, (model.FolderName ?? ""));
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Step 3: Save the file to temporary location
                var fileName = Path.Combine(uploadFolder, (model.file.FileName ?? ""));
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await model.file.CopyToAsync(fileStream);
                }

                // Success response
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_FILE_UPLOAD_SUCCESS;
            }
            catch (Exception ex)
            {
                // Dispose resources
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;

                // Log error
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

        [HttpPost("DeleteFile")]
        public async Task<ApiResult<string>> DeleteFile([FromBody] DeleteFileMasterModel model)
        {
            ActionName = "DeleteFile([FromBody] DeleteFileMasterModel model)";
            var result = new ApiResult<string>();
            try
            {
                // only for page management
                if (model.ForPGMK == true)
                {
                    model.FolderName = "PGMK";
                }

                // Step 1: make path
                var filePath = Path.Combine(ConfigurationHelper.StaticFileRootPath, (model.FolderName ?? ""), (model.FileName ?? ""));

                // Step 2: delete
                if (!System.IO.File.Exists(filePath))
                {
                    // warring response
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_FILE_NOT_FOUND;
                    return result;
                }

                // Success response
                System.IO.File.Delete(filePath);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_FILE_DELETE_SUCCESS;
            }
            catch (Exception ex)
            {
                // Dispose resources
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;

                // Log error
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


