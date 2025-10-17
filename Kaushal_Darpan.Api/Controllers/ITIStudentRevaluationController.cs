using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIFeeModel;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.RevaluationDataModel;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Kaushal_Darpan.Models.ItiCompanyMaster;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ValidationActionFilter]
    public class ITIStudentRevaluationController : BaseController
    {
        public override string PageName => "ITIStudentRevaluationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ITIStudentRevaluationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetStudentRevaluationDetails")]
        public async Task<ApiResult<DataTable>> GetStudentRevaluationDetails([FromBody] ITIStudentRevaluationDataModel body)
        {
            ActionName = "GetStudentRevaluationDetails()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetStudentRevaluationDetails(body));
               
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
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

        [HttpPost("GetAllStudentRevaluation")]
        public async Task<ApiResult<DataTable>> GetAllStudentRevaluation([FromBody] StudentDetailsByRollNoModel body)
        {
            ActionName = "GetExaminerData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetAllStudentRevaluation(body));
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




        #region iti student reval request details 

        [HttpPost("GetAllRevalRequestDetails")]
        public async Task<ApiResult<DataTable>> GetAllRevalRequestDetails([FromBody] ITIRevalRequestStudentDetailsModel body)
        {
            ActionName = "GetStudentRevaluationDetails()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetAllRevalRequestDetails(body));

                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
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


        [HttpPost("SaveRVLPaymentData")]
        public async Task<ApiResult<DataTable>> SaveRVLPaymentData([FromBody] RVLStudentDetailsModel body)
        {
            ActionName = "SaveRVLPaymentData()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIStudentRevaluationRepository.SaveRVLPaymentData(body);

                result.State = EnumStatus.Success;

                if (result.Data == null || result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found!";
                }
                else
                {
                    var firstRow = result.Data.Rows[0];

                    var state = firstRow["State"]?.ToString();
                    var message = firstRow["Message"]?.ToString();

                    if (string.Equals(state, "Error", StringComparison.OrdinalIgnoreCase))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = message ?? "Failed to save data.";
                    }
                    else
                    {
                        result.State = EnumStatus.Success;
                        result.Message = message ?? "Student data saved successfully!";
                    }
                }
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

        [HttpPost("GetRVLDetailByStudentApplicationNo")]
        public async Task<ApiResult<DataTable>> GetRVLDetailByStudentApplicationNo([FromBody] RVLStudentRevalRequestModel body)
        {
            ActionName = "GetRVLDetailByStudentApplicationNo()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetRVLDetailByStudentApplicationNo(body));

                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
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

        //[HttpPost("UploadDocument")]
        //public async Task<ApiResult<bool>> UploadDocument([FromBody]  )
        //{
        //    ActionName = "UploadDocument([FromBody] CompanyMasterModels request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {

        //            if (!ModelState.IsValid)
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "Validation failed!";
        //                return result;
        //            }


        //            result.Data = await _unitOfWork.CompanyMasterRepository.SaveData(request);
        //            await _unitOfWork.SaveChangesAsync();
        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                if (request.ID == 0)
        //                {
        //                    result.Message = Constants.MSG_SAVE_SUCCESS;
        //                }
        //                else
        //                {
        //                    result.Message = Constants.MSG_UPDATE_SUCCESS;
        //                }
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.ID == 0)
        //                {
        //                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //                }
        //                else
        //                {
        //                    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
        //                }
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost]
        //[Route("UploadDocument")]
        //public async Task<IActionResult> UploadDocument([FromBody] ITIRevalRequestStudentDetailsModel model)
        //{
        //    try
        //    {
        //        // Example: Save to DB or validate
        //        foreach (var item in model.StudentOptionList)
        //        {
        //            // Access item.UploadedCopy, item.SubjectCode etc.
        //            // Save or process each file reference
        //        }

        //        return Ok(new
        //        {
        //            State = true,
        //            Message = "Documents uploaded successfully.",
        //            ErrorMessage = ""
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            State = false,
        //            Message = "Failed to upload documents.",
        //            ErrorMessage = ex.Message
        //        });
        //    }
        //}


        [HttpPost("UploadDocument")]
        public async Task<ApiResult<bool>> UploadDocument([FromBody] ITIRevalRequestStudentDetailsModel request)
        {
            ActionName = "UploadDocument([FromBody] ITIRevalRequestStudentDetailsModel request)";
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


                    result.Data = await _unitOfWork.ITIStudentRevaluationRepository.UploadDocument(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        //if (request.ID == 0)
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
                        //if (request.ID == 0)
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
    }
}
