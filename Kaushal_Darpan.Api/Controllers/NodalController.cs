using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.NodalOfficer;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class NodalController : BaseController
    {
        public override string PageName => "NodalController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NodalController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] SearchNodalModel filterModel)
        {
            ActionName = "GetAllData([FromBody] SearchNodalModel filterModel)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.iNodalRepository.GetAllData(filterModel);

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



        [HttpPost("SaveNodalData")]
        public async Task<ApiResult<bool>> SaveNodalData([FromBody] List<NodalModel> request)
        {
            ActionName = "SaveNodalData([FromBody] List<NodalModel> request)";
            var result = new ApiResult<bool>();
            try
            {
                var totalRowsAffected = await _unitOfWork.iNodalRepository.SaveNodalData(request);
                _unitOfWork.SaveChanges();

                if (totalRowsAffected > 0)
                {
                    result.Data = true;
                    result.State = EnumStatus.Success;
                    result.Message = "Saved successfully .!";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_UPDATE_ERROR; 
                }
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



        [HttpGet("GetByID/{PK_ID}")]
        public async Task<ApiResult<DataTable>> GetByID(int PK_ID)
        {
            ActionName = "GetAllData([FromBody] SearchNodalModel filterModel)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.iNodalRepository.GetById(PK_ID);

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



        [HttpPost("DeleteByID")]
        public async Task<ApiResult<bool>> DeleteByID([FromBody]  NodalModel DeleteReq)
        {
            ActionName = "DeleteByID(int ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = new NodalModel
                    {
                        NodalId = DeleteReq.NodalId,
                        ModifyBy = DeleteReq.ModifyBy,
                        SSOID = DeleteReq.SSOID,

                        ActiveStatus = false,
                        DeleteStatus = true,
                    };
                    result.Data = await _unitOfWork.iNodalRepository.DeleteDataByID(mappedData);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
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
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


    }
}
