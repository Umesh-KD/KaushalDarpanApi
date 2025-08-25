using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.HrMaster;

using Kaushal_Darpan.Models.ITIPapperSetter;
using Kaushal_Darpan.Models.ViewPlacedStudents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ITIPapperSetterController : BaseController
    {
        public override string PageName => "ITIPapperSetterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIPapperSetterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("SavePapperSetAssignData")]
        public async Task<ApiResult<DataTable>> SaveData([FromBody] ITIPapperSetterModel body)
        {

            ActionName = "SavePapperSetAssignData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.SaveData(body);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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


        [HttpGet("GetSubjectList/{TradeID}/{ExamType}")]
        public async Task<ApiResult<DataTable>> GetllSubjectList(int TradeID = 0 , int ExamType =0)
        {

            ActionName = "GetSubjectList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.GetSubjectList(TradeID , ExamType);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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

        [HttpGet("GetProfessorList/{SubjectId}")]
        public async Task<ApiResult<DataTable>> GetProfessorList(int SubjectId = 0)
        {

            ActionName = "GetProfessorList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.GetProfessorList(SubjectId);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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


        [HttpPost("GetAllPaperSeterAssignList")]
        public async Task<ApiResult<List<PaperSetterAssginListModel>>> AllPaperSeterAssignList([FromBody] ITIPapperSetterModel body)
        {
            ActionName = "GetAllPaperSeterAssignList([FromBody] ITIPapperSetterModel body)";
            var result = new ApiResult<List<PaperSetterAssginListModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.GetAllPaperSeterAssignList(body);
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


        [HttpGet("GetAssignListByID/{ID}")]
        public async Task<ApiResult<List<ITIPapperSetterModel>>> GetAssignListByID(int ID = 0 )
        {

            ActionName = "GetAssignListByID(ID)";
            var result = new ApiResult<List<ITIPapperSetterModel>>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.PaperSetterAssignListByID(ID);
                _unitOfWork.SaveChanges();  // Commit changes if everything is ID
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


        [HttpGet("PaperSetterAssignListRemoveByID/{ID}/{Deletedby}/{Roleid}")]
        public async Task<ApiResult<DataTable>> PaperSetterAssignListRemoveByID(int ID = 0 , int Deletedby = 0 , int Roleid = 0)
        {

            ActionName = "GetProfessorList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.PaperSetterAssignListRemoveByID(ID , Deletedby , Roleid);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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


        [HttpGet("GetTradeListByYearTradeID/{YearTradeID}/{CourseTypeID}")]
        public async Task<ApiResult<DataTable>> TradeListByYearTradeID(int YearTradeID = 0 , int CourseTypeID = 0)
        {

            ActionName = "TradeListByYearTradeID(TradeID)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.GetTradeListByYearTradeID(YearTradeID , CourseTypeID);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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

        [HttpGet("GetListForPaperUploadByProfessorID/{ProfessorID}/{SSOID}/{Roleid}/{TypeID}")]
        public async Task<ApiResult<DataTable>> GetListForPaperUploadByProfessorID(int ProfessorID = 0, string SSOID = "" , int Roleid =0 , int TypeID =0)
        {

            ActionName = "GetSubjectList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.GetListForPaperUploadByProfessorID(ProfessorID, SSOID , Roleid , TypeID);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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

        [HttpGet("UpdateUploadedPaperData/{UploadedPaperDocument}/{Remark}/{userid}/{PKID}/{Roleid}")]
        public async Task<ApiResult<DataTable>> UpdateUploadedPaperData(string UploadedPaperDocument ="", string Remark = "" , int userid = 0 , int PKID =0 , int Roleid = 0)
        {

            ActionName = "GetSubjectList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.UpdateUploadedPaperData(UploadedPaperDocument, Remark , userid , PKID , Roleid);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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

        [HttpGet("UpdateSelectedProfessorPaperDetail/{SelectedProfessorID}/{PKID}/{userid}/{roleid}/{ssoid}")]
        public async Task<ApiResult<DataTable>> UpdateSelectedProfessorPaperDetail(int SelectedProfessorID = 0, int PKID = 0 , int userid =0 , int roleid =0 , string ssoid="")
        {

            ActionName = "GetProfessorList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.AutoSelectPaperDetailsUpdate(SelectedProfessorID, PKID , userid , roleid , ssoid);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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


        [HttpGet("ITIProfessorDashboardCountDetail/{userid}/{EndTermID}/{RoleID}/{SSOID}/{para1}")]
        public async Task<ApiResult<DataTable>> ITI_PaperSetterProfessorDashboardCount(int userid = 0, int EndTermID = 0, int RoleID = 0,  string ssoid = "" ,string para1="")
        {

            ActionName = "GetProfessorList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.PaperSetterProfessorDashboardCount(userid, EndTermID, RoleID,  ssoid , para1);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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

        [HttpPost("RevertPaperByExaminer/{ProfessorID}/{PKID}/{userid}/{roleid}/{ssoid}/{RevertReason}")]
        public async Task<ApiResult<DataTable>> PaperRevertByExaminer(int ProfessorID = 0, int PKID = 0, int userid = 0, int roleid = 0, string ssoid = "" , string RevertReason="")
        {

            ActionName = "GetProfessorList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIPapperSetterRepository.PaperRevertByExaminer(ProfessorID, PKID, userid, roleid, ssoid , RevertReason);
                _unitOfWork.SaveChanges();  // Commit changes if everything is successful
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


    }

}
