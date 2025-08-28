using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CommonFunction;

//using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITICampusPostMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class ItiCampusPostMasterController : BaseController
    {
        public override string PageName => "ItiCampusPostMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItiCampusPostMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpGet("GetAllData/{SSOID}/{DepartmentID:int}")]
        //public async Task<ApiResult<DataTable>> GetAllData(string SSOID, int DepartmentID)
        //{
        //    ActionName = "GetAllData()";
        //    var result = new ApiResult<DataTable>();
        //    try
        //    {
        //        result.Data = await Task.Run(() => _unitOfWork.i_ItiCampusPostMasterRepository.GetAllData(SSOID, DepartmentID));
        //        result.State = EnumStatus.Success;
        //        if (result.Data.Rows.Count == 0)
        //        {
        //            result.State = EnumStatus.Success;
        //            result.Message = "No record found.!";
        //            return result;
        //        }
        //        result.State = EnumStatus.Success;
        //        result.Message = "Data load successfully .!";
        //    }
        //    catch (System.Exception ex)
        //    {
        //        _unitOfWork.Dispose();
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = ex.Message;
        //        // write error log
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


        [HttpGet("GetAllData/{CompanyID}/{CollegeID}/{Status}/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> GetAllData(int CompanyID, int CollegeID, string Status, int DepartmentID)
        {
            ActionName = "CampusValidationList(int CollegeID,string Status)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.i_ItiCampusPostMasterRepository.GetAllData(CompanyID, CollegeID, Status, DepartmentID));
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





        [HttpGet("GetNameWiseData/{PK_ID}/{DepartmentID}")]
        public async Task<ApiResult<List<ItiCampusPostMasterModel>>> GetNameWiseData(int PK_ID, int DepartmentID)
        {
            ActionName = "GetNameWiseData()";
            var result = new ApiResult<List<ItiCampusPostMasterModel>>();
            try
            {
                result.Data = await _unitOfWork.i_ItiCampusPostMasterRepository.GetNameWiseData(PK_ID, DepartmentID);
                if (result.Data.Count > 0)
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

        [HttpGet("GetByID/{PK_ID}")]
        public async Task<ApiResult<ItiCampusPostMasterModel>> GetByID(int PK_ID)
        {

            ActionName = " GetByID(int PK_ID)";
            var result = new ApiResult<ItiCampusPostMasterModel>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.i_ItiCampusPostMasterRepository.GetById(PK_ID));
                result.State = EnumStatus.Success;
                if (result.Data == null)
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

        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] ItiCampusPostMasterModel request)
        {
            ActionName = "SaveData([FromBody] ItiCampusPostMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.i_ItiCampusPostMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.PostID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.RoleID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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
            });
        }
        [HttpPost("Save_CampusValidation_NodalAction")]
        public async Task<ApiResult<bool>> Save_CampusValidation_NodalAction([FromBody] ItiCampusPostMaster_Action request)
        {
            ActionName = "Save_CampusValidation_NodalAction([FromBody] ItiCampusPostMaster_Action request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.i_ItiCampusPostMasterRepository.Save_CampusValidation_NodalAction(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

                        result.ErrorMessage = "There was an error updating data.!";
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
            });
        }

        /*put is used to full update the existing record*/
        [HttpPut("UpdateData")]
        public async Task<ApiResult<bool>> UpdateData(ItiCampusPostMasterModel request)
        {
            ActionName = "UpdateData(ItiCampusPostMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.i_ItiCampusPostMasterRepository.UpdateData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.RoleID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.RoleID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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
            });
        }

        /*delete is used to remove the existing record*/
        [HttpDelete("DeleteDataByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new ItiCampusPostMasterModel
                    {
                        PostID = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.i_ItiCampusPostMasterRepository.DeleteDataByID(DeleteData_Request);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Deleted successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error deleting data.!";
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
            });
        }


        [HttpGet("CampusValidationList/{CompanyID}/{CollegeID}/{Status}/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID)
        {
            ActionName = "CampusValidationList(int CollegeID,string Status)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.i_ItiCampusPostMasterRepository.CampusValidationList(CompanyID, CollegeID, Status, DepartmentID));
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

        [HttpGet("GetHiringRoleMaster")]


        public async Task<ApiResult<List<CommonDDLModel>>> GetHiringRoleMaster()
        {
            ActionName = "GetStateMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.i_ItiCampusPostMasterRepository.GetHiringRoleMaster();
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

        [HttpGet("Iticollege/{DepartmentID}/{EndTermId}")]
        public async Task<ApiResult<DataTable>> Iticollege(int DepartmentID, int EndTermId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.i_ItiCampusPostMasterRepository.Iticollege(DepartmentID, EndTermId);
                    if (data.Rows.Count > 0)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
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




    }
}
