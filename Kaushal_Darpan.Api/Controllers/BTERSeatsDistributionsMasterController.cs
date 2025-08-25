using AutoMapper;
using ExcelDataReader;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.BTEReatsDistributionsMaster;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITISeatsDistributionsMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Kaushal_Darpan.Models.UploadFileWithPathData;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]
    public class BTERSeatsDistributionsMaster : BaseController
    {
        public override string PageName => "BTERSeatsDistributionsMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BTERSeatsDistributionsMaster(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /*put is used to full update the existing record*/


        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] BTERSeatsDistributionsSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetAllData(body));
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



        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] BTERSeatsDistributionsDataModel request)
        {
            ActionName = "SaveData([FromBody] GroupMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BTERSeatsDistributionsMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.id == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.id == 0)
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


        [HttpGet("GetByID/{id:int}")]
        public async Task<ApiResult<BTERSeatsDistributionsDataModel>> GetByID(int id)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<BTERSeatsDistributionsDataModel>();
                try
                {
                    var data = await _unitOfWork.ITISeatsDistributionsMasterRepository.GetById(id);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<BTERSeatsDistributionsDataModel>(data);
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


        [HttpPost("GetSeatMetrixData")]
        public async Task<ApiResult<DataTable>> GetSeatMetrixData([FromBody] BTERSeatsDistributionsDataModel body)
        {
            ActionName = "GetSeatMetrixData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetSeatMetrixData(body));
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


        [HttpPost("SaveSeatsDistributions")]
        public async Task<ApiResult<bool>> SaveSeatsDistributions([FromBody] List<BTERSeatMetrixModel> request)
        {
            ActionName = "SaveSeatsDistributions([FromBody] List<BTERSeatMetrixModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.BTERSeatsDistributionsMasterRepository.SaveSeatsDistributions(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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


        [HttpPost("SaveSeatsMatrixlist")]
        public async Task<ApiResult<DataTable>> SaveSeatsMatrixlist([FromBody] List<BTERSeatMetrixSaveModel> request)
        {
            ActionName = "SaveSeatsDistributions([FromBody] List<BTERSeatMetrixModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    result.Data = await _unitOfWork.BTERSeatsDistributionsMasterRepository.SaveSeatsMatrixlist(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

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

        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>

        [HttpPost("GetBTERCollegesByManagementType")]
        public async Task<ApiResult<DataTable>> GetBTERCollegesByManagementType([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "GetITICollegesByManagementType()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetBTERCollegesByManagementType(body));
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

        [HttpPost("getBTERTrade")]
        public async Task<ApiResult<DataTable>> getBTERTrade([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "getITITrade()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.getBTERTrade(body));
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

        [HttpPost("GetDirectTradeAndColleges")]
        public async Task<ApiResult<DataTable>> GetDirectTradeAndColleges([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "GetDirectTradeAndColleges()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetDirectTradeAndColleges(body));
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

        [HttpPost("GetTradeAndColleges")]
        public async Task<ApiResult<DataTable>> GetTradeAndColleges([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "GetTradeAndColleges()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetTradeAndColleges(body));
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

        [HttpPost("GetSampleTradeAndColleges")]
        public async Task<ApiResult<DataTable>> GetSampleTradeAndColleges([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "GetSampleTradeAndColleges()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetSampleTradeAndColleges(body));
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


        [HttpPost("GetSampleSeatmatrixAndColleges")]
        public async Task<ApiResult<DataTable>> GetSampleSeatmatrixAndColleges([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "GetSampleSeatmatrixAndColleges()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetSampleSeatmatrixAndColleges(body));
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



        [HttpPost("BTERManagementType")]
        public async Task<ApiResult<DataTable>> BTERManagementType()
        {
            ActionName = "BTERManagementType()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.BTERManagementType());
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

        [HttpPost("BTERTradeScheme")]
        public async Task<ApiResult<DataTable>> BTERTradeScheme()
        {
            ActionName = "ITITradeScheme()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.BTERTradeScheme());
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




        [HttpPost("CollegeBranches")]
        public async Task<ApiResult<DataTable>> CollegeBranches([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "CollegeBranches()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.CollegeBranches(body));
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

        [HttpPost("SaveCollegeBranches")]
        public async Task<ApiResult<DataTable>> SaveCollegeBranches([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "CollegeBranches()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.SaveCollegeBranches(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Save Successfully";
                    return result;
                }
                else if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "Duplicate Entry";
                    return result;
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

        [HttpPost("GetBranchesStreamTypeWise")]
        public async Task<ApiResult<DataTable>> GetBranchesStreamTypeWise([FromBody] BranchStreamTypeWiseSearchModel body)
        {
            ActionName = "CollegeBranches()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetBranchesStreamTypeWise(body));
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


        [HttpPost("PublishSeatMatrix")]
        public async Task<ApiResult<DataTable>> PublishSeatMatrix([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "CollegeBranches()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.PublishSeatMatrix(body));
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


        [HttpGet("GetCollegeBrancheByID/{BranchID}")]
        public async Task<ApiResult<DataTable>> GetCollegeBrancheByID(int BranchID)
        {
            ActionName = "CollegeBranches()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.GetCollegeBrancheByID(BranchID));
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

        [HttpGet("DeleteCollegeBrancheByID/{BranchID}/{UserID}")]
        public async Task<ApiResult<DataTable>> DeleteCollegeBrancheByID(int BranchID, int UserID)
        {
            ActionName = "CollegeBranches()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.DeleteCollegeBrancheByID(BranchID, UserID));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Deleted Successfully!";
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

        [HttpGet("StatusChangeByID/{BranchID}/{ActiveStatus}/{UserID}")]
        public async Task<ApiResult<DataTable>> StatusChangeByID(int BranchID,int ActiveStatus, int UserID)
        {
            ActionName = "CollegeBranches()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.StatusChangeByID(BranchID, ActiveStatus, UserID));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Updated Successfully!";
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

        [HttpPost("ImportExcelFile"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<ITISeatMetrixModel>>> ImportExcelFile([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<ITISeatMetrixModel>>();
            try
            {
                // Step 1: Validate file presence
                if (model.file == null || model.file.Length == 0)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                    return result;
                }

                // Step 2: Read the Excel file
                using (var stream = model.file.OpenReadStream())
                {
                    // Prepare StringWriter for logging or debugging purposes (Optional)
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    StringWriter swSQL = new StringWriter(sb);

                    // Register CodePagesEncodingProvider for reading older Excel formats
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    // Read Excel file into DataSet
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Treat first row as headers
                            }
                        });

                        // Get the first sheet as DataTable
                        DataTable dt = ds.Tables[0];

                        // Step 3: Convert DataTable to your specific model list
                        var data = new List<ITISeatMetrixModel>();
                        data = CommonFuncationHelper.ConvertDataTable<List<ITISeatMetrixModel>>(dt);

                        // Step 4: Map the converted data to the result model
                        var uploadFileDataModels = new List<UploadFileWithPathDataModel>();

                        // Step 5: Set result with the uploaded file data
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                }
            }
            catch (Exception ex)
            {
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
            finally
            {
                // Dispose resources
                _unitOfWork?.Dispose();
            }

            return result;
        }

        [HttpPost("SampleImportExcelFile"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<Dictionary<string, string>>>> SampleImportExcelFile([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<Dictionary<string, string>>>();

            try
            {
                // Step 1: Validate file presence
                if (model?.file == null || model.file.Length == 0)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                    return result;
                }

                // Step 2: Read the Excel file
                using (var stream = model.file.OpenReadStream())
                {
                    // Register CodePagesEncodingProvider for older Excel formats (needed for .xls files)
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    // Use ExcelReader to read the Excel file and convert to DataSet
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true // Treat the first row as headers
                            }
                        });

                        // Get the first sheet from the DataSet as a DataTable
                        DataTable dataTable = dataSet.Tables[0];

                        // Step 3: Convert the DataTable to a List<Dictionary<string, string>>
                        var data = new List<Dictionary<string, string>>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            var rowDict = new Dictionary<string, string>();

                            // Iterate through each column in the row and map it to the header
                            for (int col = 0; col < dataTable.Columns.Count; col++)
                            {
                                string header = dataTable.Columns[col].ColumnName;
                                string value = row[col].ToString();
                                rowDict[header] = value; // Add to dictionary
                            }

                            data.Add(rowDict); // Add the row dictionary to the list
                        }

                        // Step 4: Set the result with the uploaded file data
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling and logging
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;

                // Log the exception details
                var exceptionDetails = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(exceptionDetails, _unitOfWork);
            }
            finally
            {
                // Dispose resources properly
                _unitOfWork?.Dispose();
            }

            return result;
        }

        [HttpPost("SeatMatixSecondAllotment")]
        public async Task<ApiResult<DataTable>> SeatMatixSecondAllotment([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "SeatMatixSecondAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.SeatMatixSecondAllotment(body));
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

        [HttpPost("SeatMatixInternalSliding")]
        public async Task<ApiResult<DataTable>> SeatMatixInternalSliding([FromBody] BTERCollegeTradeSearchModel body)
        {
            ActionName = "SeatMatixInternalSliding()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BTERSeatsDistributionsMasterRepository.SeatMatixInternalSliding(body));
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


        //[HttpPost("SampleImportExcelFile"), DisableRequestSizeLimit]
        //public async Task<ApiResult<List<ITISeatMetrixModel>>> ImportExcelFile([FromForm] UploadFileModel model)
        //{
        //    ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
        //    var result = new ApiResult<List<ITISeatMetrixModel>>();
        //    try
        //    {
        //        // Step 1: Validate file presence
        //        if (model.file == null || model.file.Length == 0)
        //        {
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
        //            return result;
        //        }

        //        // Step 2: Read the Excel file
        //        using (var stream = model.file.OpenReadStream())
        //        {
        //            // Prepare StringWriter for logging or debugging purposes (Optional)
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            StringWriter swSQL = new StringWriter(sb);

        //            // Register CodePagesEncodingProvider for reading older Excel formats
        //            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //            // Read Excel file into DataSet
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                {
        //                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                    {
        //                        UseHeaderRow = true // Treat first row as headers
        //                    }
        //                });

        //                // Get the first sheet as DataTable
        //                DataTable dt = ds.Tables[0];

        //                // Step 3: Convert DataTable to your specific model list
        //                var data = new List<ITISeatMetrixModel>();
        //                data = CommonFuncationHelper.ConvertDataTable<List<ITISeatMetrixModel>>(dt);

        //                // Step 4: Map the converted data to the result model
        //                var uploadFileDataModels = new List<UploadFileWithPathDataModel>();

        //                // Step 5: Set result with the uploaded file data
        //                result.Data = data;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.State = EnumStatus.Error;
        //        result.Message = Constants.MSG_ERROR_OCCURRED;
        //        result.ErrorMessage = ex.Message;

        //        // Log error
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }
        //    finally
        //    {
        //        // Dispose resources
        //        _unitOfWork?.Dispose();
        //    }

        //    return result;
        //}
    }
}
