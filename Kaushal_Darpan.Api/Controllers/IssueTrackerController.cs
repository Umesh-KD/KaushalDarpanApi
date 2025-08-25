using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.IssueTrackerMasters;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ScholarshipMaster;
using Kaushal_Darpan.Models.SubjectMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    [ValidationActionFilter]
    public class IssueTrackerController : BaseController
    {
        public override string PageName => "IssueTrackerController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public IssueTrackerController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }




        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] IssueTrackerListSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.IssueTrackerRepository.GetAllData(model);
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

        //GetUserList
        [HttpPost("GetUserList/{RoleID}")]
        public async Task<ApiResult<DataTable>> GetUserList(int RoleID)
        {
            ActionName = "GetUserList()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.IssueTrackerRepository.GetUserList(RoleID);

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


        [HttpGet("GetByID/{IssueID:int}")]
        public async Task<ApiResult<IssueTrackerMaster>> GetByID(int IssueID)
        {
            //Change Action Name
            ActionName = "GetByID(int IssueID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<IssueTrackerMaster>();
                try
                {
                    var data = await _unitOfWork.IssueTrackerRepository.GetById(IssueID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<IssueTrackerMaster>(data);
                        result.Data = mappedData;
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
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }



        [HttpPost("saveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] IssueSaveData request)
        {
            ActionName = "SaveData([FromBody] IssueTrackerMaster request)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    int returnVal = await _unitOfWork.IssueTrackerRepository.SaveData(request);
                    _unitOfWork.SaveChanges();

                    if (returnVal > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = request.IssueID != 0
                            ? Constants.MSG_UPDATE_SUCCESS
                            : Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Data = false;
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = request.IssueID != 0
                            ? Constants.MSG_UPDATE_ERROR
                            : Constants.MSG_ADD_ERROR;
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
            });
        }




        [HttpDelete("DeleteByID/{IssueID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int IssueID, string ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new IssueTrackerMaster
                    {
                        IssueID = IssueID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.IssueTrackerRepository.DeleteDataByID(DeleteData_Request);
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


        // Assign To
        [HttpPost("AssignIssure")]
        public async Task<ApiResult<bool>> AssignIssure([FromBody] List<IssueTrackerListSearchModel> request)
        {
            ActionName = "SaveAllData([FromBody] List<IssueTrackerListSearchModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.IssueTrackerRepository.AssignIssure(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_NO_DATA_UPDATE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
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

                    // Log the error
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

    }


}


