using System.Data;
using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.Attendance;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMeritIInfoModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentCancelationController : BaseController
    {
        public override string PageName => "EnrollmentCancelationController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EnrollmentCancelationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost("ChangeEnRollNoStatus")]
        public async Task<ApiResult<int>> ChangeEnRollNoStatus([FromBody] StudentEnrolmentCancelModel request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.EnrollmentCancelationRepository.ChangeEnRollNoStatus(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful
                    if (isSave == -1)
                    {
                        result.Data = isSave;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = isSave;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = ex.Message;

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



        [HttpPost("GetEnrollCancelationData")]
        public async Task<ApiResult<DataTable>> GetEnrollCancelationData(StudentEnrolmentCancelModel model)
        {
            ActionName = "GetEnrollCancelationData(StudentEnrolmentCancelModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.EnrollmentCancelationRepository.GetEnrollCancelationData(model));
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


        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<StudentDetailsModel>>> GetAllData([FromBody] StudentSearchModel body)
        {
            ActionName = "GetAllData([FromBody] StudentSearchModel body)";
            var result = new ApiResult<List<StudentDetailsModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.EnrollmentCancelationRepository.GetAllData(body);
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



        [HttpPost("GetEnrollmentCancelList")]
        public async Task<ApiResult<List<StudentDetailsModel>>> GetEnrollmentCancelList([FromBody] StudentSearchModel body)
        {
            ActionName = "GetAllData([FromBody] StudentSearchModel body)";
            var result = new ApiResult<List<StudentDetailsModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.EnrollmentCancelationRepository.GetEnrollmentCancelList(body);
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


        [HttpPost("CancelEnrolment")]
        public async Task<ApiResult<int>> CancelEnrolment([FromBody] StudentEnrolmentCancelModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    var data = await _unitOfWork.EnrollmentCancelationRepository.CancelEnrolment(model);
                    _unitOfWork.SaveChanges();
                    if (data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Data = data;
                        result.Message = "Student Mapped Successfully";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "This Enrollment No Is Already Cancel";
                        result.Data = data;
                    }
                    return result;
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
            });
        }

    }
}
