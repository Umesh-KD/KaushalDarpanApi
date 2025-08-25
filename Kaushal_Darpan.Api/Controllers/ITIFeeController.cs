using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIFeeModel;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ValidationActionFilter]
    public class ITIFeeController : BaseController
    {
        public override string PageName => "ITIFeeController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIFeeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] ITIFeeModel request)
        {
            ActionName = "SaveData([FromBody] ITIFeeModel request)";
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
                    result.Data = await _unitOfWork.ITIFeeRepository.SaveITIFeeData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.Id == 0)
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
                        if (request.Id == 0)
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

        [HttpGet("UpdateData/{Id}")]
        public async Task<ApiResult<ITIFeeModel>> UpdateData(int Id)
        {
            ActionName = "UpdateData";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIFeeModel>();
                try
                {
                    var data = await _unitOfWork.ITIFeeRepository.UpdateData(Id);
                    result.Data = data;
                    if (data != null)
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

        [HttpGet("GetByID/{id}")]
        public async Task<ApiResult<ITIFeeModel>> GetByID(int id)
        {
            ActionName = "GetByID(int id)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIFeeModel>();
                try
                {
                    var data = await _unitOfWork.ITIFeeRepository.GetById(id);
                    result.Data = data;
                    if (data != null)
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

        //[HttpDelete("DeleteByID/{ExaminerID:int}/{ModifyBy:int}")]
        //public async Task<ApiResult<bool>> DeleteByID(int ExaminerID, int ModifyBy)
        //{
        //    ActionName = "DeleteByID(int ExaminerID, int ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            var mappedData = new ExaminerMaster
        //            {
        //                ExaminerID = ExaminerID,
        //                ModifyBy = ModifyBy,

        //                //ActiveStatus = false,
        //                //DeleteStatus = true,
        //            };
        //            result.Data = await _unitOfWork.ExaminersRepository.DeleteDataByID(mappedData);
        //            _unitOfWork.SaveChanges();

        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DELETE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_DELETE_ERROR;
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
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}
    }
}
