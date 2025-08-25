using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text.pdf;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Api.Validators;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CenterSuperitendent;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.Results;
using Kaushal_Darpan.Models.SSOUserDetails;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.SubjectMaster;
using Kaushal_Darpan.Models.UploadFileWithPathData;
using Kaushal_Darpan.Models.UserMaster;
using Kaushal_Darpan.Models.ViewStudentDetailsModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.CommonFunction.ItiTradeAndCollegesDDL;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class CommonFunctionController : BaseController
    {
        public override string PageName => "CommonFunctionController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommonFunctionController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetLevelMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetLevelMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetLevelMaster();
                    result.State = EnumStatus.Success;
                    result.Data = data;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("ParentMenu/{departmentId}")]
        public async Task<ApiResult<DataTable>> ParentMenu(int departmentId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ParentMenu(departmentId);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("CityMasterDistrictWise/{DistrictID}")]
        public async Task<ApiResult<DataTable>> CityMasterDistrictWise(int DistrictID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.CityMasterDistrictWise(DistrictID);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("ExamStudentStatus/{roleid}/{type}")]
        public async Task<ApiResult<DataTable>> ExamStudentStatus(int roleId, int type)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ExamStudentStatus(roleId, type);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("ITIExamStudentStatus/{roleid}/{type}")]
        public async Task<ApiResult<DataTable>> ITIExamStudentStatus(int roleId, int type)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ITIExamStudentStatus(roleId, type);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetDesignationMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDesignationMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDesignationMaster();
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("PWDCategory")]
        public async Task<ApiResult<List<CommonDDLModel>>> PWDCategory()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.PWDCategory();
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }
        [HttpGet("GetDistrictMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDistrictMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDistrictMaster();
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetParliamentMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetParliamentMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetParliamentMaster();
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetDivisionMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDivisionMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDivisionMaster();
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetTehsilMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetTehsilMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTehsilMaster();
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetManagmentTypes/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> ManagementType(int DepartmentID = 0)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ManagementType(DepartmentID);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }
        [HttpGet("InstitutionCategory")]
        public async Task<ApiResult<DataTable>> InstitutionCategory()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.InstitutionCategory();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }
        [HttpGet("GetStreamType")]
        public async Task<ApiResult<DataTable>> StreamType()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StreamType();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("InstituteMaster/{DepartmentID}/{Eng_NonEng}/{EndTermId}")]
        public async Task<ApiResult<DataTable>> InstituteMaster(int DepartmentID, int Eng_NonEng, int EndTermId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.InstituteMaster(DepartmentID, Eng_NonEng, EndTermId);
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


        [HttpGet("Iticollege/{DepartmentID}/{Eng_NonEng}/{EndTermId}/{InsutiteId}")]
        public async Task<ApiResult<DataTable>> Iticollege(int DepartmentID, int Eng_NonEng, int EndTermId , int InsutiteId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.Iticollege(DepartmentID, Eng_NonEng, EndTermId , InsutiteId);
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


        [HttpGet("IticenterColleges/{DepartmentID}/{Eng_NonEng}/{EndTermId}/{InstituteID}")]
        public async Task<ApiResult<DataTable>> IticenterColleges(int DepartmentID, int Eng_NonEng, int EndTermId,int InstituteID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.IticenterColleges(DepartmentID, Eng_NonEng, EndTermId, InstituteID);
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




        [HttpGet("StreamMaster/{DepartmetnID}/{StreamType}/{EndTermId}")]
        public async Task<ApiResult<DataTable>> StreamMaster(int DepartmetnID = 0, int StreamType = 0, int EndTermId = 0)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StreamMaster(DepartmetnID, StreamType, EndTermId);
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

        [HttpGet("StreamMasterwithcount/{DepartmetnID}/{StreamType}/{EndTermId}")]
        public async Task<ApiResult<DataTable>> StreamMasterwithcount(int DepartmetnID = 0, int StreamType = 0, int EndTermId = 0)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StreamMasterwithcount(DepartmetnID, StreamType, EndTermId);
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

        [HttpGet("StreamMasterByCampus/{CampusPostID}/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> StreamMasterByCampus(int CampusPostID, int DepartmentID, int EndTermId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StreamMasterByCampus(CampusPostID, DepartmentID, EndTermId);
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

        [HttpGet("SemesterMaster/{ShowAllSemester}")]
        public async Task<ApiResult<DataTable>> SemesterMaster(int ShowAllSemester)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.SemesterMaster(ShowAllSemester);
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

        [HttpGet("SemesterGenerateMaster")]
        public async Task<ApiResult<DataTable>> SemesterGenerateMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.SemesterGenerateMaster();
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

        [HttpGet("StudentType")]
        public async Task<ApiResult<List<CommonDDLModel>>> StudentType()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StudentType();
                    if (data.Count > 0)
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

        //[HttpGet("StudentStatus")]
        //public async Task<ApiResult<List<CommonDDLModel>>> StudentStatus()
        //{
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<List<CommonDDLModel>>();
        //        try
        //        {
        //            var data = await _unitOfWork.CommonFunctionRepository.StudentStatus();
        //            if (data.Count > 0)
        //            {
        //                result.Data = data;
        //                result.State = EnumStatus.Success;
        //                result.Message = "Data load successfully .!";

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = "No record found.!";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
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

        [HttpGet("ExamCategory")]
        public async Task<ApiResult<DataTable>> ExamCategory()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ExamCategory();
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

        [HttpGet("CasteCategoryA")]
        public async Task<ApiResult<DataTable>> CasteCategoryA()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.CasteCategoryA();
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
        [HttpGet("CasteCategoryB")]
        public async Task<ApiResult<DataTable>> CasteCategoryB()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.CasteCategoryB();
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

        [HttpPost("ViewStudentDetails")]
        public async Task<ApiResult<ViewStudentDetailsModel>> ViewStudentDetails(ViewStudentDetailsRequestModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ViewStudentDetailsModel>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ViewStudentDetails(model);
                    if (data != null)
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

        [HttpPost("ViewStudentAdmittedDetails")]
        public async Task<ApiResult<DataTable>> ViewStudentAdmittedDetails(ViewStudentDetailsRequestModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ViewStudentAdmittedDetails(model);
                    if (data != null)
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

        [HttpPost("ITIViewStudentDetails")]
        public async Task<ApiResult<ViewStudentDetailsModel>> ITIViewStudentDetails(ViewStudentDetailsRequestModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ViewStudentDetailsModel>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ITIViewStudentDetails(model);
                    if (data != null)
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

        [HttpGet("GetPaperMaster")]
        public async Task<ApiResult<DataTable>> GetPaperMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetPaperList();
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

        [HttpGet("GetSubjectList/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> GetSubjectList(int DepartmentID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetSubjectList(DepartmentID);
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

        [HttpGet("GetExamShift/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> GetExamShift(int DepartmentID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetExamShift(DepartmentID);
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

        [HttpGet("GetFinancialYear")]
        public async Task<ApiResult<DataTable>> GetFinancialYear()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetFinancialYear();
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

        [HttpGet("GetMonths")]
        public async Task<ApiResult<DataTable>> GetMonths()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetMonths();
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

        [HttpGet("GetExamType")]
        public async Task<ApiResult<DataTable>> GetExamType()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetExamType();
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

        [HttpGet("Board_UniversityMaster")]
        public async Task<ApiResult<DataTable>> Board_UniversityMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.Board_UniversityMaster();
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
        [HttpGet("CommonMasterDataByCode/{MasterCode}/{DepartmentID}/{CourseTypeID?}")]
        public async Task<ApiResult<DataTable>> CommonMasterDataByCode(string MasterCode, int DepartmentID, int CourseTypeID = 0)

        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCommonMasterData(MasterCode, DepartmentID, CourseTypeID);
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

        [HttpGet("GetPaperType")]
        public async Task<ApiResult<DataTable>> GetPaperType()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetPaperType();
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
        [HttpGet("PreExam_StudentMaster/{StudentID}/{statusId}/{DepartmentID}/{Eng_NonEng}/{EndTermID}/{StudentExamID}")]
        public async Task<ApiResult<StudentMasterModel>> PreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndTermID, int StudentExamID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<StudentMasterModel>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.PreExam_StudentMaster(StudentID, statusId, DepartmentID, Eng_NonEng, status, EndTermID, StudentExamID);
                    if (data != null)
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

        [HttpGet("ITIPreExam_StudentMaster/{StudentID}/{statusId}/{DepartmentID}/{Eng_NonEng}/{EndTermID}/{StudentExamID}")]
        public async Task<ApiResult<StudentMasterModel>> ITIPreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndTermID, int StudentExamID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<StudentMasterModel>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ITIPreExam_StudentMaster(StudentID, statusId, DepartmentID, Eng_NonEng, status, EndTermID, StudentExamID);
                    if (data != null)
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

        [HttpGet("PassingYear")]
        public async Task<ApiResult<DataTable>> PassingYear()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.PassingYear();
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


        [HttpGet("AdmissionPassingYear")]
        public async Task<ApiResult<DataTable>> AdmissionPassingYear()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.AdmissionPassingYear();
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




        [HttpGet("GetSubjectMasterDDL/{DepartmentID:int}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetSubjectMasterDDL(int DepartmentID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetSubjectMasterDDL(DepartmentID);
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

        [HttpGet("GetCommonMasterDDLByType/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCommonMasterDDLByType(string type)
        {
            ActionName = "GetCommonMasterDDLByType(string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCommonMasterDDLByType(type);
                    if (data.Count > 0)
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

        [HttpGet("GetCampusPostMasterDDL/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCampusPostMasterDDL(int DepartmentID)
        {
            ActionName = "GetCampusPostMasterDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCampusPostMasterDDL(DepartmentID);
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

        [HttpGet("GetCategoryDMasterDDL/{MeritalStatus}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCategoryDMasterDDL(int MeritalStatus)
        {
            ActionName = "GetCategoryDMasterDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCategoryDMasterDDL(MeritalStatus);
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


        [HttpGet("GetCampusWiseHiringRoleDDL/{campusPostId}/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCampusWiseHiringRoleDDL(int campusPostId, int DepartmentID)
        {
            ActionName = "GetCampusWiseHiringRoleDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCampusWiseHiringRoleDDL(campusPostId, DepartmentID);
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

        [HttpGet("PlacementCompanyMaster/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> PlacementCompanyMaster(int DepartmentID)
        {
            ActionName = "PlacementCompanyMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.PlacementCompanyMaster(DepartmentID);
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

        [HttpGet("PrefentialCategoryMaster/{DepartmentID}/{CourseTypeId}/{PrefentialCategoryType}")]
        public async Task<ApiResult<List<CommonDDLModel>>> PrefentialCategoryMaster(int DepartmentID, int CourseTypeId, int PrefentialCategoryType)
        {
            ActionName = "PrefentialCategoryMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.PrefentialCategoryMaster(DepartmentID, CourseTypeId, PrefentialCategoryType);
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

        [HttpGet("PlacementCompanyMaster_IDWise/{ID}/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> PlacementCompanyMaster_IDWise(int ID, int DepartmentID)
        {
            ActionName = "PlacementCompanyMaster_IDWise(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.PlacementCompanyMaster_IDWise(ID, DepartmentID);
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

        [HttpGet("CollegeType")]
        public async Task<ApiResult<DataTable>> CollegeType()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.CollegeType();
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

        [HttpGet("GetStateMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetStateMaster()
        {
            ActionName = "GetStateMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetStateMaster();
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


        [HttpGet("DistrictMaster_StateIDWise/{StateID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> DistrictMaster_StateIDWise(int StateID)
        {
            ActionName = " DistrictMaster_StateIDWise(int StateID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DistrictMaster_StateIDWise(StateID);
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

        [HttpGet("DistrictMaster_DivisionIDWise/{DivisionID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> DistrictMaster_DivisionIDWise(int DivisionID)
        {
            ActionName = " DistrictMaster_DivisionIDWise(int DivisionID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DistrictMaster_DivisionIDWise(DivisionID);
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



        [HttpGet("TehsilMaster_DistrictIDWise/{DistrictID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> TehsilMaster_DistrictIDWise(int DistrictID)
        {
            ActionName = "TehsilMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.TehsilMaster_DistrictIDWise(DistrictID);
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


        [HttpGet("SubDivisionMaster_DistrictIDWise/{DistrictID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> SubDivisionMaster_DistrictIDWise(int DistrictID)
        {
            ActionName = "SubDivisionMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.SubDivisionMaster_DistrictIDWise(DistrictID);
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

        [HttpGet("AssemblyMaster_DistrictIDWise/{DistrictID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> AssemblyMaster_DistrictIDWise(int DistrictID)
        {
            ActionName = "AssemblyMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.AssemblyMaster_DistrictIDWise(DistrictID);
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

        [HttpGet("GetHiringRoleMaster")]


        public async Task<ApiResult<List<CommonDDLModel>>> GetHiringRoleMaster()
        {
            ActionName = "GetStateMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetHiringRoleMaster();
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



        [HttpGet("GetRoleMasterDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetRoleMasterDDL(int? DepartmentID = 0, int? EngNonEng = 0)
        {
            ActionName = "GetRoleMasterDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetRoleMasterDDL(DepartmentID.HasValue ? DepartmentID.Value : 0, EngNonEng.HasValue ? EngNonEng.Value : 0);
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

        [HttpPost("GetParentSubjectDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetParentSubjectDDL([FromBody] SubjectSearchModel body)
        {
            ActionName = "GetRoleMasterDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetParentSubjectDDL(body);
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

        [HttpGet("GetStaffTypeDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetStaffTypeDDL()
        {
            ActionName = "GetStaffTypeDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetStaffTypeDDL();
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

        [HttpPost("GetGroupCode")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetGroupCode(CommonDDLSubjectMasterModel model)
        {
            ActionName = "GetGroupCode(CommonDDLSubjectMasterModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetGroupCode(model);
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


        [HttpPost("GetExaminerGroupCode")]
        public async Task<ApiResult<DataTable>> GetExaminerGroupCode(CommonDDLExaminerGroupCodeModel model)
        {
            ActionName = "GetStudentStatusByRole(int roleId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetExaminerGroupCode(model);
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
        
        [HttpPost("GetExaminerGroupCode_Reval")]
        public async Task<ApiResult<DataTable>> GetExaminerGroupCode_Reval(CommonDDLExaminerGroupCodeModel model)
        {
            ActionName = "GetExaminerGroupCode_Reval(CommonDDLExaminerGroupCodeModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetExaminerGroupCode_Reval(model);
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



        //[HttpGet("GetExamName")]
        //public async Task<ApiResult<List<CommonDDLModel>>> GetExamName()
        //{
        //    ActionName = "GetStaffTypeDDL()";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<List<CommonDDLModel>>();
        //        try
        //        {
        //            var data = await _unitOfWork.CommonFunctionRepository.GetExamName();
        //            if (data != null)
        //            {
        //                result.Data = data;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
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
        [HttpGet("GetExamName")]
        public async Task<ApiResult<DataTable>> GetExamName()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetExamName();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("Get_InvigilatorSSOID")]
        public async Task<ApiResult<DataTable>> DDL_InvigilatorSSOID(DDL_InvigilatorSSOID_DataModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DDL_InvigilatorSSOID(model);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("SubjectMaster_SemesterIDWise/{SemesterID}/{DepartmentID:int}")]
        public async Task<ApiResult<List<CommonDDLModel>>> SubjectMaster_SemesterIDWise(int SemesterID, int DepartmentID)
        {
            ActionName = " SubjectMaster_SemesterIDWise(int SemesterID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.SubjectMaster_SemesterIDWise(SemesterID, DepartmentID);
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

        [HttpGet("SubjectMaster_SubjectCode_SemesterIDWise/{SemesterID}/{DepartmentID:int}/{SubjectCode}")]
        public async Task<ApiResult<List<CommonDDLModel>>> SubjectMaster_SubjectCode_SemesterIDWise(int SemesterID, int DepartmentID, string SubjectCode)
        {
            ActionName = " SubjectMaster_SubjectCode_SemesterIDWise(int SemesterID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.SubjectMaster_SubjectCode_SemesterIDWise(SemesterID, DepartmentID, SubjectCode);
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

        [HttpGet("SubjectMaster_StreamIDWise/{StreamID}/{DepartmentID}/{SemesterID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> SubjectMaster_StreamIDWise(int StreamID, int DepartmentID, int SemesterID)
        {
            ActionName = "SubjectMaster_StreamIDWise(int StreamID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.SubjectMaster_StreamIDWise(StreamID, DepartmentID, SemesterID);
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

        [HttpGet("Examiner_SSOID/{DepartmentID:int}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetExamerSSoidDDL(int DepartmentID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetExamerSSoidDDL(DepartmentID);
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


        [HttpGet("GetConfigurationType/{RoleId}/{TypeID?}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetConfigurationType(int RoleId = 0, int? TypeID = null)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetConfigurationType(RoleId, TypeID.HasValue ? TypeID.Value : 0);
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

        [HttpGet("GetStudentStatusByRole/{roleid}/{type}")]
        public async Task<ApiResult<DataTable>> GetStudentStatusByRole(int roleId, int type)
        {
            ActionName = "GetStudentStatusByRole(int roleId, string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetStudentStatusByRole(roleId, type);
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

        [HttpGet("GetEnrollmentCancelStatusByRole/{roleid}/{type}")]
        public async Task<ApiResult<DataTable>> GetEnrollmentCancelStatusByRole(int roleId, int type)
        {
            ActionName = "GetStudentStatusByRole(int roleId, string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetEnrollmentCancelStatusByRole(roleId, type);
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


        [HttpGet("ITIGetStudentStatusByRole/{roleid}/{type}")]
        public async Task<ApiResult<DataTable>> ItiGetStudentStatusByRole(int roleId, int type)
        {
            ActionName = "GetStudentStatusByRole(int roleId, string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ItiGetStudentStatusByRole(roleId, type);
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


        [HttpPost("GetCenterMasterDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCenterMasterDDL(RequestBaseModel request)
        {
            ActionName = "GetCenterMasterDDL(RequestBaseModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCenterMasterDDL(request);
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

        [HttpPost("GetSubjectMasterDDL_New")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetSubjectMasterDDL_New(CommonDDLSubjectMasterModel request)
        {
            ActionName = "GetSubjectMasterDDL_New(CommonDDLSubjectMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetSubjectMasterDDL_New(request);
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


        [HttpGet("GetCollegeTypeList")]
        public async Task<ApiResult<DataTable>> GetCollegeTypeList()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCollegeTypeList();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("GetSerialMasterData")]
        public async Task<ApiResult<List<CommonSerialMasterResponseModel>>> GetSerialMasterData(CommonSerialMasterRequestModel request)
        {
            ActionName = "GetSerialMasterData(CommonSerialMasterRequestModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonSerialMasterResponseModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetSerialMasterData(request);
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

        [HttpGet("GetTradeTypesList")]
        public async Task<ApiResult<DataTable>> GetTradeTypesList()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTradeTypesList();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("ItiCollegesGetAllData")]
        public async Task<ApiResult<DataTable>> ItiCollegesGetAllData(ItiCollegesSearchModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ItiCollegesGetAllData(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("BterCollegesGetAllData")]
        public async Task<ApiResult<DataTable>> BterCollegesGetAllData(BterCollegesSearchModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.BterCollegesGetAllData(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("TradeListGetAllData")]
        public async Task<ApiResult<DataTable>> TradeListGetAllData(ItiTradeSearchModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.TradeListGetAllData(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetTradeLevelList")]
        public async Task<ApiResult<DataTable>> GetTradeLevelList()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTradeLevelList();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetInstituteMasterByTehsilWise/{TehsilID}/{EndTermId}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetInstituteMasterByTehsilWise(int TehsilID, int EndTermId)
        {
            ActionName = " DistrictMaster_StateIDWise(int TehsilID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetInstituteMasterByTehsilWise(TehsilID, EndTermId);
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

        [HttpGet("GetInstituteMasterByDistrictWise/{DistrictID}/{EndTermId}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetInstituteMasterByDistrictWise(int DistrictID, int EndTermId)
        {
            ActionName = " DistrictMaster_StateIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetInstituteMasterByDistrictWise(DistrictID, EndTermId);
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

        [HttpGet("GetSubCasteCategoryA/{CasteCategoryID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetSubCasteCategoryA(int CasteCategoryID)
        {
            ActionName = "GetSubCasteCategoryA(int CasteCategoryID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    result.Data = await _unitOfWork.CommonFunctionRepository.GetSubCasteCategoryA(CasteCategoryID);
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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

        [HttpGet("InsituteMaster_DistrictIDWise/{DistrictID}/{EndTermId}")]
        public async Task<ApiResult<List<CommonDDLModel>>> InsituteMaster_DistrictIDWise(int DistrictID, int EndTermId)
        {
            ActionName = " InsituteMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.InsituteMaster_DistrictIDWise(DistrictID, EndTermId);
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


        [HttpGet("InsituteMaster_DistrictIDWise/{DistrictID}/{EndTermId}/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> InsituteMaster_DistrictIDWise(int DistrictID, int EndTermId, int DepartmentID)
        {
            ActionName = " InsituteMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.InsituteMaster_DistrictIDWise(DistrictID, EndTermId, DepartmentID);
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

    
        [HttpGet("CheckSSOIDExists/{SSOID}/{RoleID}/{InstituteID}")]
        public async Task<ApiResult<UserRequestModel>> CheckSSOIDExists(string SSOID, string RoleID, string InstituteID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<UserRequestModel>();
                try
                {
                    result.Data = await _unitOfWork.CommonFunctionRepository.CheckSSOIDExists(SSOID, RoleID, InstituteID);
                    if (result.Data != null)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Exists.!";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Not Exists.!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        #region document 
        [HttpPost("UploadDocument"), DisableRequestSizeLimit]
        [ActionName("UploadDocument")]
        public async Task<ApiResult<List<UploadFileWithPathDataModel>>> UploadDocument([FromForm] UploadFileModel model)
        {
            ActionName = "UploadDocument([FromForm] UploadFileModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<UploadFileWithPathDataModel>>();
                try
                {
                    //required file
                    if (model.file == null || model.file.Length == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                        return result;
                    }

                    //type(extention)
                    if (!string.IsNullOrWhiteSpace(model.FileExtention))
                    {
                        var fileExtensions = Path.GetExtension(model.file.FileName).ToLower();
                        if (model.FileExtention?.Split().Any(x => x.ToLower() == fileExtensions) == true)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid extension, allowed only {string.Join(", ", model.FileExtention)}";
                            return result;
                        }
                    }

                    //Special Char
                    var (isValid, message) = FileValidationHelper.IsValidFileName(model.file.FileName);
                    if (!isValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = $"{message}";
                        return result;
                    }

                    //min size
                    if (!string.IsNullOrWhiteSpace(model.MinFileSize) && int.TryParse(model.MinFileSize.Replace("mb", string.Empty).Replace("kb", string.Empty), out int acceptMinFileSize) == true)
                    {
                        decimal fileSize = 0;
                        if (model.MinFileSize.ToLower().EndsWith("mb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024 / 1024);
                        }
                        else if (model.MinFileSize.ToLower().EndsWith("kb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024);
                        }
                        if (fileSize < acceptMinFileSize)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid file size, Min allowed only {model.MinFileSize}";
                            return result;
                        }
                    }

                    //max size
                    if (!string.IsNullOrWhiteSpace(model.MaxFileSize) && int.TryParse(model.MaxFileSize.Replace("mb", string.Empty).Replace("kb", string.Empty), out int acceptMaxFileSize) == true)
                    {
                        decimal fileSize = 0;
                        if (model.MaxFileSize.ToLower().EndsWith("mb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024 / 1024);
                        }
                        else if (model.MaxFileSize.ToLower().EndsWith("kb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024);
                        }
                        if (fileSize > acceptMaxFileSize)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid file size, Max allowed only {model.MaxFileSize}";
                            return result;
                        }
                    }

                    //set and create the folder
                    var uploadFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, model.FolderName ?? "");
                    if (!System.IO.Directory.Exists(uploadFolder))
                    {
                        System.IO.Directory.CreateDirectory(uploadFolder);
                    }

                    //read the file name of the received file
                    var OrgfileName = ContentDispositionHeaderValue.Parse(model.file.ContentDisposition).FileName.Trim('"');

                    // save the file on Path
                    var FileName = $"{System.DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}{Path.GetExtension(OrgfileName)}";
                    var finalPathSave = Path.Combine(uploadFolder, FileName);

                    //model
                    List<UploadFileWithPathDataModel> uploadFileDataModels = new List<UploadFileWithPathDataModel>();
                    UploadFileWithPathDataModel uploadFileDataModel = new UploadFileWithPathDataModel();
                    uploadFileDataModel.Dis_FileName = OrgfileName;
                    uploadFileDataModel.FileName = FileName;
                    uploadFileDataModel.FilePath = Path.Combine(model.FolderName ?? "", FileName);
                    uploadFileDataModel.FolderName = model.FolderName ?? "";
                    uploadFileDataModels.Add(uploadFileDataModel);
                    result.Data = uploadFileDataModels;

                    //physical save
                    var finalPath = Path.Combine(uploadFolder, FileName);
                    using (var fileStream = new FileStream(finalPath, FileMode.Create))
                    {
                        model.file.CopyTo(fileStream);
                    }

                    //resize
                    using (FileStream pngStream = new FileStream(finalPath, FileMode.OpenOrCreate))
                    {
                        CommonFuncationHelper.ResizeImage(pngStream, finalPathSave, 0, 0);
                    }

                    //success
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }

                return result;
            });
        }

        [HttpPost("DeleteDocument")]
        public async Task<ApiResult<bool>> DeleteDocument([FromBody] DeleteDocumentDetailsModel model)//filename
        {
            ActionName = "DeleteDocument([FromBody] DeleteDocumentDetailsModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var filePath = Path.Combine(ConfigurationHelper.StaticFileRootPath, model.FolderName ?? "", model.FileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_FILE_NOT_FOUND;
                        return result;
                    }
                    System.IO.File.Delete(filePath);
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DELETE_SUCCESS;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }
                return result;
            });
        }
        #endregion

        #region bter document 
        [HttpPost("UploadBTERDocument"), DisableRequestSizeLimit]
        [ActionName("UploadBTERDocument")]
        public async Task<ApiResult<List<UploadFileWithPathDataModel>>> UploadBTERDocument([FromForm] UploadBTERFileModel model)
        {
            ActionName = "UploadDocument([FromForm] UploadFileModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<UploadFileWithPathDataModel>>();
                try
                {
                    //required file
                    if (model.file == null || model.file.Length == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                        return result;
                    }

                    //type(extention)
                    if (!string.IsNullOrWhiteSpace(model.FileExtention))
                    {
                        var fileExtensions = Path.GetExtension(model.file.FileName).ToLower();
                        if (model.FileExtention?.Split().Any(x => x.ToLower() == fileExtensions) == true)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid extension, allowed only {string.Join(", ", model.FileExtention)}";
                            return result;
                        }
                    }

                    //Special Char
                    var (isValid, message) = FileValidationHelper.IsValidFileName(model.file.FileName);
                    if (!isValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = $"{message}";
                        return result;
                    }

                    //min size
                    if (!string.IsNullOrWhiteSpace(model.MinFileSize) && int.TryParse(model.MinFileSize.Replace("mb", string.Empty).Replace("kb", string.Empty), out int acceptMinFileSize) == true)
                    {
                        decimal fileSize = 0;
                        if (model.MinFileSize.ToLower().EndsWith("mb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024 / 1024);
                        }
                        else if (model.MinFileSize.ToLower().EndsWith("kb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024);
                        }
                        if (fileSize < acceptMinFileSize)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid file size, Min allowed only {model.MinFileSize}";
                            return result;
                        }
                    }

                    //max size
                    if (!string.IsNullOrWhiteSpace(model.MaxFileSize) && int.TryParse(model.MaxFileSize.Replace("mb", string.Empty).Replace("kb", string.Empty), out int acceptMaxFileSize) == true)
                    {
                        decimal fileSize = 0;
                        if (model.MaxFileSize.ToLower().EndsWith("mb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024 / 1024);
                        }
                        else if (model.MaxFileSize.ToLower().EndsWith("kb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024);
                        }
                        if (fileSize > acceptMaxFileSize)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid file size, Max allowed only {model.MaxFileSize}";
                            return result;
                        }
                    }


                    string[] Folders = model.FolderName.Split("/");
                    string parentFolder = "";
                    for (int i = 0; i < Folders.Length; i++)
                    {
                        if (!System.IO.Directory.Exists($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}"))
                        {
                            System.IO.Directory.CreateDirectory($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}");
                        }
                        parentFolder = parentFolder + "/" + Folders[i];
                    }

                    ////set and create the folder
                    //var uploadFolderName = Path.Combine(ConfigurationHelper.StaticFileRootPath, "Students" ?? "");
                    //if (!System.IO.Directory.Exists(uploadFolderName))
                    //{
                    //    System.IO.Directory.CreateDirectory(uploadFolderName);
                    //}

                    //var uploadDeptFolder = Path.Combine(uploadFolderName, "BTER");
                    //if (!System.IO.Directory.Exists(uploadDeptFolder))
                    //{
                    //    System.IO.Directory.CreateDirectory(uploadDeptFolder);
                    //}

                    //var uploadAcademicYearFolder = Path.Combine(uploadDeptFolder, Convert.ToString(model.AcademicYear) ?? "");
                    //if (!System.IO.Directory.Exists(uploadAcademicYearFolder))
                    //{
                    //    System.IO.Directory.CreateDirectory(uploadAcademicYearFolder);
                    //}

                    //var uploadEng_NonEngFolder = Path.Combine(uploadAcademicYearFolder, Convert.ToString(model.Eng_NonEng) ?? "");
                    //if (!System.IO.Directory.Exists(uploadEng_NonEngFolder))
                    //{
                    //    System.IO.Directory.CreateDirectory(uploadEng_NonEngFolder);
                    //}

                    //var uploadApplicationIDFolder = Path.Combine(uploadEng_NonEngFolder, Convert.ToString(model.ApplicationID) ?? "");
                    //if (!System.IO.Directory.Exists(uploadApplicationIDFolder))
                    //{
                    //    System.IO.Directory.CreateDirectory(uploadApplicationIDFolder);
                    //}

                    var uploadFolder = $"{ConfigurationHelper.StaticFileRootPath}{parentFolder}";
                    //read the file name of the received file
                    var OrgfileName = ContentDispositionHeaderValue.Parse(model.file.ContentDisposition).FileName.Trim('"');

                    // save the file on Path
                    //var FileName = $"{System.DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}{Path.GetExtension(OrgfileName)}";
                    var FileName = $"{model.FileName}{Path.GetExtension(OrgfileName)}";
                    var finalPathSave = Path.Combine(uploadFolder, FileName);

                    //model
                    List<UploadFileWithPathDataModel> uploadFileDataModels = new List<UploadFileWithPathDataModel>();
                    UploadFileWithPathDataModel uploadFileDataModel = new UploadFileWithPathDataModel();
                    uploadFileDataModel.Dis_FileName = OrgfileName;
                    uploadFileDataModel.FileName = FileName;
                    uploadFileDataModel.FilePath = Path.Combine(uploadFolder ?? "", FileName);
                    uploadFileDataModel.FolderName = uploadFolder;
                    uploadFileDataModels.Add(uploadFileDataModel);

                    // copy the old file and past in same location
                    if (model.IsCopy == true)
                    {
                        var files = Directory.GetFiles(uploadFolder);
                        var matchedFile = files.FirstOrDefault(file =>
                                            string.Equals(Path.GetFileNameWithoutExtension(file), model.FileName, StringComparison.OrdinalIgnoreCase)
                                        );

                        if (!string.IsNullOrWhiteSpace(matchedFile))
                        {
                            var FileName_old = $"{model.FileName}_old_{System.DateTime.Now:MMMddyyyyhhmmssffffff}{Path.GetExtension(matchedFile)}";
                            var finalPathSave_old = Path.Combine(uploadFolder, FileName_old);

                            using (var sourceStream = new FileStream(matchedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (var destinationStream = new FileStream(finalPathSave_old, FileMode.Create, FileAccess.Write))
                            {
                                await sourceStream.CopyToAsync(destinationStream);
                            }

                            uploadFileDataModel.OldFileName = FileName_old;
                        }
                        else
                        {
                            var matchedOldFile = files.FirstOrDefault(file =>
                                Path.GetFileNameWithoutExtension(file)
                                    .StartsWith($"{model.FileName}_old_", StringComparison.OrdinalIgnoreCase)
                            );

                            uploadFileDataModel.OldFileName = !string.IsNullOrWhiteSpace(matchedOldFile) ? Path.GetFileName(matchedOldFile) : "";
                        }
                    }

                    result.Data = uploadFileDataModels;

                    //physical save
                    var finalPath = Path.Combine(uploadFolder, FileName);

                    if (System.IO.File.Exists(finalPath))
                    {
                        System.IO.File.Delete(finalPath);
                    }

                    using (var fileStream = new FileStream(finalPath, FileMode.Create))
                    {
                        model.file.CopyTo(fileStream);
                    }

                    //resize
                    using (FileStream pngStream = new FileStream(finalPath, FileMode.OpenOrCreate))
                    {
                        CommonFuncationHelper.ResizeImage(pngStream, finalPathSave, 0, 0);
                    }

                    //success

                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }

                return result;
            });
        }

        [HttpPost("DeleteBTERDocument")]
        public async Task<ApiResult<bool>> DeleteBTERDocument([FromBody] DeleteDocumentDetailsModel model)//filename
        {
            ActionName = "DeleteDocument([FromBody] DeleteDocumentDetailsModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var filePath = Path.Combine(ConfigurationHelper.StaticFileRootPath, model.FolderName ?? "", model.FileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_FILE_NOT_FOUND;
                        return result;
                    }
                    System.IO.File.Delete(filePath);
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DELETE_SUCCESS;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }
                return result;
            });
        }

        [HttpPost("UploadBTEROriginalDocument"), DisableRequestSizeLimit]
        [ActionName("UploadBTEROriginalDocument")]
        public async Task<ApiResult<List<UploadOriginalFileWithPathDataModel>>> UploadBTEROriginalDocument([FromForm] UploadBTERFileModel model)
        {
            ActionName = "UploadDocument([FromForm] UploadFileModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<UploadOriginalFileWithPathDataModel>>();
                try
                {
                    //required file
                    if (model.file == null || model.file.Length == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                        return result;
                    }

                    //type(extention)
                    if (!string.IsNullOrWhiteSpace(model.FileExtention))
                    {
                        var fileExtensions = Path.GetExtension(model.file.FileName).ToLower();
                        if (model.FileExtention?.Split().Any(x => x.ToLower() == fileExtensions) == true)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid extension, allowed only {string.Join(", ", model.FileExtention)}";
                            return result;
                        }
                    }

                    //Special Char
                    var (isValid, message) = FileValidationHelper.IsValidFileName(model.file.FileName);
                    if (!isValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = $"{message}";
                        return result;
                    }

                    //min size
                    if (!string.IsNullOrWhiteSpace(model.MinFileSize) && int.TryParse(model.MinFileSize.Replace("mb", string.Empty).Replace("kb", string.Empty), out int acceptMinFileSize) == true)
                    {
                        decimal fileSize = 0;
                        if (model.MinFileSize.ToLower().EndsWith("mb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024 / 1024);
                        }
                        else if (model.MinFileSize.ToLower().EndsWith("kb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024);
                        }
                        if (fileSize < acceptMinFileSize)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid file size, Min allowed only {model.MinFileSize}";
                            return result;
                        }
                    }

                    //max size
                    if (!string.IsNullOrWhiteSpace(model.MaxFileSize) && int.TryParse(model.MaxFileSize.Replace("mb", string.Empty).Replace("kb", string.Empty), out int acceptMaxFileSize) == true)
                    {
                        decimal fileSize = 0;
                        if (model.MaxFileSize.ToLower().EndsWith("mb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024 / 1024);
                        }
                        else if (model.MaxFileSize.ToLower().EndsWith("kb"))
                        {
                            fileSize = Math.Round((decimal)model.file.Length / 1024);
                        }
                        if (fileSize > acceptMaxFileSize)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = $"Invalid file size, Max allowed only {model.MaxFileSize}";
                            return result;
                        }
                    }


                    string[] Folders = model.FolderName.Split("/");
                    string parentFolder = "";
                    for (int i = 0; i < Folders.Length; i++)
                    {
                        if (!System.IO.Directory.Exists($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}"))
                        {
                            System.IO.Directory.CreateDirectory($"{ConfigurationHelper.StaticFileRootPath}{parentFolder}/{Folders[i]}");
                        }
                        parentFolder = parentFolder + "/" + Folders[i];
                    }

                    var uploadFolder = $"{ConfigurationHelper.StaticFileRootPath}{parentFolder}";
                    //read the file name of the received file
                    var OrgfileName = ContentDispositionHeaderValue.Parse(model.file.ContentDisposition).FileName.Trim('"');

                    // save the file on Path
                    //var FileName = $"{System.DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}{Path.GetExtension(OrgfileName)}";
                    var FileName = $"{model.FileName}{Path.GetExtension(OrgfileName)}";
                    var finalPathSave = Path.Combine(uploadFolder, FileName);

                    //model
                    List<UploadOriginalFileWithPathDataModel> uploadFileDataModels = new List<UploadOriginalFileWithPathDataModel>();
                    UploadOriginalFileWithPathDataModel uploadFileDataModel = new UploadOriginalFileWithPathDataModel();
                    uploadFileDataModel.DocumentMasterID = model.DocumentMasterID;
                    uploadFileDataModel.Dis_FileName = OrgfileName;
                    uploadFileDataModel.FileName = FileName;
                    uploadFileDataModel.FilePath = Path.Combine(uploadFolder ?? "", FileName);
                    uploadFileDataModel.FolderName = uploadFolder;
                    uploadFileDataModels.Add(uploadFileDataModel);

                    // copy the old file and past in same location
                    if (model.IsCopy == true)
                    {
                        var files = Directory.GetFiles(uploadFolder);
                        var matchedFile = files.FirstOrDefault(file =>
                                            string.Equals(Path.GetFileNameWithoutExtension(file), model.FileName, StringComparison.OrdinalIgnoreCase)
                                        );

                        if (!string.IsNullOrWhiteSpace(matchedFile))
                        {
                            var FileName_old = $"{model.FileName}_old_{System.DateTime.Now:MMMddyyyyhhmmssffffff}{Path.GetExtension(matchedFile)}";
                            var finalPathSave_old = Path.Combine(uploadFolder, FileName_old);

                            using (var sourceStream = new FileStream(matchedFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            using (var destinationStream = new FileStream(finalPathSave_old, FileMode.Create, FileAccess.Write))
                            {
                                await sourceStream.CopyToAsync(destinationStream);
                            }

                            uploadFileDataModel.OldFileName = FileName_old;
                        }
                        else
                        {
                            var matchedOldFile = files.FirstOrDefault(file =>
                                Path.GetFileNameWithoutExtension(file)
                                    .StartsWith($"{model.FileName}_old_", StringComparison.OrdinalIgnoreCase)
                            );

                            uploadFileDataModel.OldFileName = !string.IsNullOrWhiteSpace(matchedOldFile) ? Path.GetFileName(matchedOldFile) : "";
                        }
                    }

                    result.Data = uploadFileDataModels;

                    //physical save
                    var finalPath = Path.Combine(uploadFolder, FileName);

                    if (System.IO.File.Exists(finalPath))
                    {
                        System.IO.File.Delete(finalPath);
                    }

                    using (var fileStream = new FileStream(finalPath, FileMode.Create))
                    {
                        model.file.CopyTo(fileStream);
                    }

                    //resize
                    using (FileStream pngStream = new FileStream(finalPath, FileMode.OpenOrCreate))
                    {
                        CommonFuncationHelper.ResizeImage(pngStream, finalPathSave, 0, 0);
                    }
                    uploadFileDataModel.ApplicationID = model.ApplicationID;
                    uploadFileDataModel.EndTermID = model.EndTermID;
                    uploadFileDataModel.DepartmentID = model.DepartmentID;
                    uploadFileDataModel.Eng_NonEng = model.Eng_NonEng;
                    uploadFileDataModel.AcademicYear = model.AcademicYear;

                    var data = await _unitOfWork.CommonFunctionRepository.UploadBTEROriginalDocument(uploadFileDataModel);
                    //success

                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }

                return result;
            });
        }

        [HttpPost("GetBTEROriginalDocument")]
        public async Task<ApiResult<DataTable>> GetBTEROriginalDocument(GetBTEROriginalListModel body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetBTEROriginalDocument(body);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        #endregion

        [HttpGet("GetCastCategory")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCastCategory()
        {
            ActionName = "GetCastCategory()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCastCategory();
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

        [HttpGet("GetOptionalSubjectsByStudentID/{StudentID}/{DepartmentID}/{StudentExamID}")]
        public async Task<ApiResult<DataSet>> GetOptionalSubjectsByStudentID(Int32 StudentID, Int32 DepartmentID, int StudentExamID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataSet>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetOptionalSubjectsByStudentID(StudentID, DepartmentID, StudentExamID);
                    if (data?.Tables?.Count == 2)
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

        [HttpPost("GetCommonSubjectDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCommonSubjectDDL(CommonDDLCommonSubjectModel model)
        {
            ActionName = "GetCommonSubjectDDL(CommonDDLCommonSubjectModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCommonSubjectDDL(model);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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

        [HttpGet("GetActiveTabList/{DepartmentID}/{ApplicationID}/{RoleID}")]
        public async Task<ApiResult<int[]>> GetActiveTabList(Int32 DepartmentID, Int32 ApplicationID, Int32 RoleID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int[]>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetActiveTabList(DepartmentID, ApplicationID, RoleID);
                    if (data.Rows.Count > 0)
                    {

                        var d = CommonFuncationHelper.ConvertDataTable<List<ActiveTabModel>>(data);
                        result.Data = d.Select(f => Convert.ToInt32(f.ActiveTab)).ToArray();
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

        [HttpGet("GetQualificationDDL/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetQualificationDDL(string type)
        {
            ActionName = "GetQualificationDDL(string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetQualificationDDL(type);
                    if (data.Count > 0)
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

        [HttpPost("GetDataItiStudentApplication")]
        public async Task<ApiResult<List<EmitraApplicationstatusModel>>> GetDataItiStudentApplication([FromBody] ItiStuAppSearchModelUpward body)
        {
            ActionName = " GetDataItiStudentApplication([FromBody] StudentSearchModel body)";
            var result = new ApiResult<List<EmitraApplicationstatusModel>>();
            try
            {
                result.Data = await _unitOfWork.CommonFunctionRepository.GetDataItiStudentApplication(body);
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

        [HttpGet("GetCategaryCastDDL/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCategaryCastDDL(string type)
        {
            ActionName = "GetCategaryCastDDL(string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCategaryCastDDL(type);
                    if (data.Count > 0)
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

        [HttpPost("GetSubjectCodeMasterDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)
        {
            ActionName = "GetSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetSubjectCodeMasterDDL(request);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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


        [HttpPost("GetTimeTableSubjectCodeMasterDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetTimeTableSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)
        {
            ActionName = "GetTimeTableSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTimeTableSubjectCodeMasterDDL(request);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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

        [HttpGet("Subjects_Semester_SubjectCodeWise/{SemesterID}/{DepartmentID:int}/{SubjectCode}/{EndTerm}/{CourseTypeID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> Subjects_Semester_SubjectCodeWise(int SemesterID, int DepartmentID, string SubjectCode, int EndTerm, int CourseTypeID)
        {
            ActionName = " Subjects_Semester_SubjectCodeWise(int SemesterID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.Subjects_Semester_SubjectCodeWise(SemesterID, DepartmentID, SubjectCode, EndTerm, CourseTypeID);
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

        [HttpGet("CategoryBDDLData/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> CategoryBDDLData(int DepartmentID)
        {
            ActionName = "CategoryBDDLData(int DepartmentID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.CategoryBDDLData(DepartmentID);
                    if (data.Count > 0)
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

        [HttpPost("UploadFile"), DisableRequestSizeLimit]
        [ActionName("UploadFile")]
        public async Task<ApiResult<List<UploadFileWithPathDataModel>>> UploadFile([FromForm] UploadFileModel model)
        {
            ActionName = "UploadDocument([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<UploadFileWithPathDataModel>>();
            try
            {
                // Step 1: Validate file presence
                if (model.file == null || model.file.Length == 0)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                    return result;
                }

                // Step 2: Validate file extension
                if (!string.IsNullOrWhiteSpace(model.FileExtention))
                {
                    var allowedExtensions = model.FileExtention.Split(',').Select(x => x.Trim().ToLower());
                    var fileExtension = Path.GetExtension(model.file.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = $"Invalid extension, allowed only {string.Join(", ", allowedExtensions)}";
                        return result;
                    }
                }

                // Step 3: Validate file sizes (Min and Max)
                if (!await ValidateFileSize(model.file.Length, model.MinFileSize, "Min", result) ||
                    !await ValidateFileSize(model.file.Length, model.MaxFileSize, "Max", result))
                {
                    return result;
                }

                // Step 4: Create upload folder if not exists
                var uploadFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, model.FolderName ?? "");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Step 5: Save file to temporary location
                var OrgfileName = ContentDispositionHeaderValue.Parse(model.file.ContentDisposition).FileName.Trim('"');
                var TempFileName = $"{DateTime.Now:MMMddyyyyhhmmssffffff}{Path.GetExtension(OrgfileName)}";
                var tempFilePath = Path.Combine(uploadFolder, "temp_" + TempFileName);  // Temporary location



                // Step 6: Save the file to temporary location
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await model.file.CopyToAsync(fileStream);
                }

                // Step 7: Process the file based on its extension
                if (Path.GetExtension(OrgfileName).ToLower() == ".pdf")
                {
                    string password = model.Password; // PDF password
                    string encryptedPdfPath = Path.Combine(uploadFolder, TempFileName);  // Final path after encryption
                    EncryptPdf(tempFilePath, encryptedPdfPath, password);
                    tempFilePath = encryptedPdfPath;  // Update path to the encrypted version
                }
                else if (new[] { ".png", ".jpg" }.Contains(Path.GetExtension(OrgfileName).ToLower()))
                {
                    string resizedImagePath = Path.Combine(uploadFolder, TempFileName); // Final path after resizing
                    using (var pngStream = new FileStream(tempFilePath, FileMode.OpenOrCreate))
                    {
                        CommonFuncationHelper.ResizeImage(pngStream, resizedImagePath, 0, 0); // Resize image
                    }
                    tempFilePath = resizedImagePath;  // Update path to the resized version
                }

                // Step 8: Rename and move file to final destination (After protection)
                var finalFileName = $"{DateTime.Now:MMMddyyyyhhmmssffffff}{Path.GetExtension(OrgfileName)}";
                var finalFilePath = Path.Combine(uploadFolder, finalFileName);
                System.IO.File.Move(tempFilePath, finalFilePath); // Move processed file to the final location

                var uploadFileDataModels = new List<UploadFileWithPathDataModel>
                {
                    new UploadFileWithPathDataModel
                    {
                        Dis_FileName = OrgfileName,
                        FileName = finalFileName,
                        FilePath = Path.Combine(model.FolderName ?? "", TempFileName),
                        FolderName = model.FolderName ?? ""
                    }
                };
                result.Data = uploadFileDataModels;
                // Success response
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_SAVE_SUCCESS;
                tempFilePath = Path.Combine(uploadFolder, "temp_" + TempFileName);
                if (System.IO.File.Exists(tempFilePath)) // Check if the temporary file still exists before trying to delete
                {
                    System.IO.File.Delete(tempFilePath); // Delete the temp file
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

        private async Task<bool> ValidateFileSize(long fileLength, string size, string type, ApiResult<List<UploadFileWithPathDataModel>> result)
        {
            if (!string.IsNullOrWhiteSpace(size) && int.TryParse(size.Replace("mb", string.Empty).Replace("kb", string.Empty), out int acceptSize))
            {
                decimal fileSize = 0;
                if (size.ToLower().EndsWith("mb"))
                {
                    fileSize = Math.Round((decimal)fileLength / 1024 / 1024);
                }
                else if (size.ToLower().EndsWith("kb"))
                {
                    fileSize = Math.Round((decimal)fileLength / 1024);
                }

                if ((type == "Min" && fileSize < acceptSize) || (type == "Max" && fileSize > acceptSize))
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = $"Invalid file size, {type} allowed only {size}";
                    return false;
                }
            }
            return true;
        }

        private void EncryptPdf(string inputPdf, string outputPdf, string password)
        {
            try
            {
                // Introduce a small delay to ensure the file is no longer being accessed
                Task.Delay(500).Wait(); // 500 ms delay, adjust if needed

                // Use PdfReader within a using block to ensure proper disposal
                using (PdfReader reader = new PdfReader(inputPdf))
                {
                    // Open the output PDF and create a FileStream with FileShare.ReadWrite
                    using (FileStream fs = new FileStream(outputPdf, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        // Create the PdfStamper (this is where we write the encrypted PDF to the output file)
                        using (PdfStamper stamper = new PdfStamper(reader, fs))
                        {
                            // Convert password string to byte array using UTF-8 encoding
                            byte[] userPassword = Encoding.UTF8.GetBytes(password);
                            byte[] ownerPassword = Encoding.UTF8.GetBytes(password);

                            // Set the password and permissions
                            stamper.SetEncryption(
                                userPassword,  // User password (in byte array)
                                ownerPassword,  // Owner password (same as user password, or different)
                                PdfWriter.ALLOW_PRINTING, // Permissions: Allow printing
                                PdfWriter.ENCRYPTION_AES_128  // Encryption algorithm: AES-128
                            );
                        }

                        Console.WriteLine("PDF encrypted and saved to: " + outputPdf);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encrypting PDF: " + ex.Message);
            }
        }

        [HttpGet("GetExamStudentStatusApprovalReject/{roleid}/{type}")]
        public async Task<ApiResult<DataTable>> GetExamStudentStatusApprovalReject(int roleId, int type)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ExamStudentStatusApprovalReject(roleId, type);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("GetITITradeNameDDl")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITITradeNameDDl(CommonDDLSubjectCodeMasterModel request)
        {
            ActionName = "GetITITradeNameDDl(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITITradeNameDDl(request);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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


        [HttpPost("GetITISubjectNameDDl")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITISubjectNameDDl(CommonDDLSubjectCodeMasterModel request)
        {
            ActionName = "GetITISubjectNameDDl(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITISubjectNameDDl(request);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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


        [HttpPost("GetITISubjectCodeDDl")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITISubjectCodeDDl(CommonDDLSubjectCodeMasterModel request)
        {
            ActionName = "GetITISubjectCodeDDl(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITISubjectCodeDDl(request);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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
        [HttpPost("StreamDDL_InstituteWise")]
        public async Task<ApiResult<DataTable>> StreamDDL_InstituteWise(StreamDDL_InstituteWiseModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StreamDDL_InstituteWise(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("StreamDDLInstituteIdWise")]
        public async Task<ApiResult<DataTable>> StreamDDLInstituteIdWise(StreamDDL_InstituteWiseModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StreamDDLInstituteIdWise(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("GetDateConfigSetting")]
        public async Task<ApiResult<DataTable>> GetDateConfigSetting(DateSettingConfigModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDateSetting(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("QualificationDDL")]
        public async Task<ApiResult<DataTable>> QualificationDDL(QualificationDDLDataModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.QualificationDDL(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }



        [HttpGet("GetReletionDDL/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetReletionDDL(string type)
        {
            ActionName = "GetReletionDDL(string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetReletionDDL(type);
                    if (data.Count > 0)
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



        [HttpGet("GetRoomTypeDDL/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetRoomTypeDDL(string type)
        {
            ActionName = "GetRoomTypeDDL(string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetRoomTypeDDL(type);
                    if (data.Count > 0)
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


        [HttpGet("GetRoomTypeDDLByHostel/{type}/{HostelID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetRoomTypeDDLByHostel(string type, int HostelID)
        {
            ActionName = "GetRoomTypeDDLByHostel(string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetRoomTypeDDLByHostel(type, HostelID);
                    if (data.Count > 0)
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



        [HttpPost("SessionConfiguration")]
        public async Task<ApiResult<DataTable>> SessionConfiguration(SessionConfigModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.SessionConfiguration(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("GetHostelDDL/{DepartmentID}/{InstituteID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetHostelDDL(int DepartmentID, int InstituteID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetHostelDDL(DepartmentID, InstituteID);
                    result.State = EnumStatus.Success;
                    result.Data = data;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("GetTechnicianDDL/{StaffParentID}")]

        public async Task<ApiResult<List<CommonDDLModel>>> GetTechnicianDDL(int StaffParentID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTechnicianDDL(StaffParentID);
                    result.State = EnumStatus.Success;
                    result.Data = data;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("GetHOD_DDL/{CourseID}")]

        public async Task<ApiResult<List<CommonDDLModel>>> GetHOD_DDL(int CourseID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetHOD_DDL(CourseID);
                    result.State = EnumStatus.Success;
                    result.Data = data;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("ITIPlacementCompanyMaster/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> ITIPlacementCompanyMaster(int DepartmentID)
        {
            ActionName = "ITIPlacementCompanyMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ITIPlacementCompanyMaster(DepartmentID);
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

        [HttpGet("GetSubjectForCitizenSugg/{selectedOption}")]

        public async Task<ApiResult<List<CommonDDLModel>>> GetSubjectForCitizenSugg(int selectedOption)

        {

            ActionName = "ITIPlacementCompanyMaster()";

            return await Task.Run(async () =>

            {

                var result = new ApiResult<List<CommonDDLModel>>();

                try

                {

                    var data = await _unitOfWork.CommonFunctionRepository.GetSubjectForCitizenSugg(selectedOption);

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

        [HttpGet("GetManageHostelWardenRole/{SSOID}/{RoleID=0}")]
        public async Task<ApiResult<DataTable>> GetManageHostelWardenRole(string SSOID, int RoleID = 0)
        {
            ActionName = "GetManageHostelWardenRole()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetManageHostelWardenRole(SSOID, RoleID));
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


        [HttpPost("GetSubjectTheoryParctical")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetSubjectTheoryParctical(CommonDDLSubjectCodeMasterModel request)
        {
            ActionName = "GetSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetSubjectTheoryParctical(request);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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


        [HttpPost("GetBackSubjectList")]
        public async Task<ApiResult<List<SubjectMaster>>> GetBackSubjectList(CommonDDLSubjectCodeMasterModel request)
        {
            ActionName = "GetBackSubjectList(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<SubjectMaster>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetBackSubjectList(request);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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



        [HttpPost("GetPrintRollAdmitCardSetting")]
        public async Task<ApiResult<DataTable>> GetPrintRollAdmitCardSetting(CollegeMasterSearchModel model)
        {
            ActionName = "GetPrintRollAdmitCardSetting()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetPrintRollAdmitCardSetting(model));
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


        [HttpPost("Get_ITIPrintRollAdmitCardSetting")]
        public async Task<ApiResult<DataTable>> Get_ITIPrintRollAdmitCardSetting(CollegeMasterSearchModel model)
        {
            ActionName = "GetPrintRollAdmitCardSetting()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.Get_ITIPrintRollAdmitCardSetting(model));
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



        [HttpGet("GetDteCategory_BranchWise/{ID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDteCategory_BranchWise(int ID)
        {
            ActionName = " GetCategory_TradeWise(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDteCategory_BranchWise(ID);
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



        [HttpGet("GetDteEquipment_CategoryWise/{ID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDteEquipment_CategoryWise(int ID)
        {
            ActionName = " GetEquipment_CategoryWise(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDteEquipment_CategoryWise(ID);
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


        [HttpGet("GetCategory_TradeWise/{ID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCategory_TradeWise(int ID)
        {
            ActionName = " GetCategory_TradeWise(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCategory_TradeWise(ID);
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



        [HttpGet("GetEquipment_CategoryWise/{ID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetEquipment_CategoryWise(int ID)
        {
            ActionName = " GetEquipment_CategoryWise(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetEquipment_CategoryWise(ID);
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
        [HttpGet("GetDDl_StatusForGrivience")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDDl_StatusForGrivience()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDDl_StatusForGrivience();
                    result.State = EnumStatus.Success;
                    result.Data = data;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("TradeListTradeTypeWise")]
        public async Task<ApiResult<DataTable>> TradeListTradeTypeWise(ItiTradeSearchModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.TradeListTradeTypeWise(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("GetITICenterDDL/{EndTermId}/{CourseType}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITICenterDDL(int EndTermID,int CourseType)
        {
            ActionName = " GetITICenterDDL(int EndTermID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITICenterDDL(EndTermID,CourseType);
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



        [HttpGet("GetCategory_BranchWise/{ID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCategory_BranchWise(int ID)
        {
            ActionName = " GetCategory_BranchWise(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCategory_BranchWise(ID);
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


        [HttpGet("GetALLEquipmentCategory")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetALLEquipmentCategory()
        {
            ActionName = " GetALLEquipmentCategory()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetALLEquipmentCategory();
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

        [HttpPost("GetCenter_DistrictWise")]
        public async Task<ApiResult<DataTable>> GetCenter_DistrictWise([FromBody] CenterMasterDDLDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetCenter_DistrictWise(body));
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

        [HttpPost("GetStaff_InstituteWise")]
        public async Task<ApiResult<DataTable>> GetStaff_InstituteWise([FromBody] StaffMasterDDLDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetStaff_InstituteWise(body));
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

        [HttpPost("GetITIStaffInstituteWise")]
        public async Task<ApiResult<DataTable>> GetITIStaffInstituteWise([FromBody] StaffMasterDDLDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetITIStaffInstituteWise(body));
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




        [HttpPost("getexamdate")]
        public async Task<ApiResult<DataTable>> GetExamDate([FromBody] CenterMasterDDLDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetExamDate(body));
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

        [HttpGet("GetCenterCodeInstituteWise/{ID}")]
        public async Task<ApiResult<DataTable>> GetCenterCodeInstituteWise(int ID)
        {
            ActionName = "GetCenterCodeInstituteWise(string Type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCenterCodeInstituteWise(ID);
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

        [HttpGet("GetddlCenterDownloadOrReceived/{Type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetddlCenterDownloadOrReceived(string Type)
        {
            ActionName = " GetddlCenterDownloadOrReceived(string Type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetddlCenterDownloadOrReceived(Type);
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


        [HttpGet("GetDDLDispatchNo/{DepartmentID}/{CourseTypeID}/{EndTermID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            ActionName = " GetDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDDLDispatchNo(DepartmentID, CourseTypeID, EndTermID);
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

        [HttpGet("GetRevalDDLDispatchNo/{DepartmentID}/{CourseTypeID}/{EndTermID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetRevalDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            ActionName = " GetRevalDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetRevalDDLDispatchNo(DepartmentID, CourseTypeID, EndTermID);
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

        [HttpGet("GovtInstitute_DistrictWise/{DistrictID}/{EndTermId}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GovtInstitute_DistrictWise(int DistrictID, int EndTermId)
        {
            ActionName = " DistrictMaster_StateIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GovtInstitute_DistrictWise(DistrictID, EndTermId);
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

        [HttpGet("GetCurrentAdmissionSession/{DepartmentId}")]
        public async Task<ApiResult<DataTable>> GetCurrentAdmissionSession(int DepartmentId)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetCurrentAdmissionSession(DepartmentId));
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


        [HttpGet("GetDDLCompanyName/{DepartmentID}/{CourseTypeID}/{EndTermID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            ActionName = "GetDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDDLCompanyName(DepartmentID, CourseTypeID, EndTermID);
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


        [HttpPost("GetITIOptionFormData")]
        public async Task<ApiResult<DataTable>> GetITIOptionFormData([FromBody] ItiTradeSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetITIOptionFormData(body));
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

        [HttpPost("DDL_GroupCode_ExaminerWise")]
        public async Task<ApiResult<DataTable>> DDL_GroupCode_ExaminerWise(DDL_GroupCode_ExaminerWiseModel model)
        {
            ActionName = "DDL_GroupCode_ExaminerWise(DDL_GroupCode_ExaminerWiseModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DDL_GroupCode_ExaminerWise(model);
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


        [HttpGet("DDL_CampusPostTypeMaster/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> DDL_CampusPostTypeMaster(string type)
        {
            ActionName = "DDL_CampusPostTypeMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DDL_CampusPostTypeMaster(type);
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



        [HttpGet("PanchayatSamiti/{DistrictID}")]
        public async Task<ApiResult<DataTable>> PanchayatSamiti(int DistrictID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.PanchayatSamiti(DistrictID);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }




        [HttpGet("GramPanchayat/{TehsilID}")]
        public async Task<ApiResult<DataTable>> GramPanchayatSamiti(int TehsilID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GramPanchayatSamiti(TehsilID);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("villageMaster/{ID}")]
        public async Task<ApiResult<DataTable>> villageMaster(int ID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.VillageMaster(ID);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("PublicInfo")]
        public async Task<ApiResult<DataTable>> PublicInfo([FromBody] PublicInfoModel body)
        {
            ActionName = "PublicInfo";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.PublicInfo(body));
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

        [HttpGet("GetLateralQualificationBoard/{ExamType}")]
        public async Task<ApiResult<DataTable>> GetLateralQualificationBoard(string ExamType)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetLateralQualificationBoard(ExamType);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("GetApplicationSubmittedSteps/{AppplicationId}")]
        public async Task<ApiResult<DataTable>> GetApplicationSubmittedSteps(string AppplicationId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetApplicationSubmittedSteps(AppplicationId);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("DDL_OfficeMaster/{DepartmentID}/{LevelID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> DDL_OfficeMaster(int DepartmentID, int LevelID)
        {
            ActionName = "DDL_OfficeMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DDL_OfficeMaster(DepartmentID, LevelID);
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

        [HttpGet("DDL_PostMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> DDL_PostMaster()
        {
            ActionName = "DDL_PostMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DDL_PostMaster();
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


        [HttpGet("AllDDlManageByTypeCommanMaster/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> AllDDlManageByTypeCommanMaster(string type)
        {
            ActionName = "AllDDlManageByTypeCommanMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.AllDDlManageByTypeCommanMaster(type);
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


        [HttpGet("AllDDlCenterMaster/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> AllDDlCenterMaster(string type)
        {
            ActionName = "AllDDlCenterMaster()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.AllDDlCenterMaster(type);
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



        [HttpGet("GetDesignationAndPostMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDesignationAndPostMaster()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDesignationAndPostMaster();
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("CenterSuperitendentDDL")]
        public async Task<ApiResult<DataTable>> CenterSuperitendentDDL([FromBody] CenterSuperitendentDDL body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.CenterSuperitendentDDL(body));
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


        [HttpPost("CommonVerifierApiSSOIDGetSomeDetails")]
        public async Task<ApiResult<string>> CommonVerifierApiSSOIDGetSomeDetails([FromBody] CommonVerifierApiDataModel request)
        {
            ActionName = "CommonVerifierApiSSOIDGetSomeDetails([FromBody] CommonVerifierApiDataModel request)";
            var result = new ApiResult<string>();

            try
            {
                // Call service that returns raw JSON string
                result.Data = await _unitOfWork.CommonFunctionRepository.CommonVerifierApiSSOIDGetSomeDetails(request);
                _unitOfWork.SaveChanges();

                if (!string.IsNullOrWhiteSpace(result.Data))
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ERROR_OCCURRED;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Write error log
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


        [HttpGet("GetDteEquipment_Branch_Wise_CategoryWise/{Category}")]
        public async Task<ApiResult<DataTable>> GetDteEquipment_Branch_Wise_CategoryWise(int Category)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetDteEquipment_Branch_Wise_CategoryWise(Category);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("GetITIDDLCompanyName/{DepartmentID}/{CourseTypeID}/{EndTermID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITIDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            ActionName = "GetITIDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITIDDLCompanyName(DepartmentID, CourseTypeID, EndTermID);
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

        [HttpGet("GetITIddlCenterDownloadOrReceived/{Type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITIddlCenterDownloadOrReceived(string Type)
        {
            ActionName = " GetITIddlCenterDownloadOrReceived(string Type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITIddlCenterDownloadOrReceived(Type);
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






        [HttpGet("GetTables")]
        public async Task<ApiResult<DataTable>> GetTables()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTables();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }



        [HttpGet("GetSPs")]
        public async Task<ApiResult<DataTable>> GetSPs()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetSPs();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetTableColumn/{Table}")]
        public async Task<ApiResult<DataTable>> GetTableColumn(string Table)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTableColumn(Table);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetTableRecordCount/{Table}")]
        public async Task<ApiResult<DataTable>> GetTableRecordCount(string Table)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTableRecordCount(Table);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetTableRows/{Table}/{PageNumber}/{PageSize}")]
        public async Task<ApiResult<DataTable>> GetTableRows(string Table, string PageNumber, string PageSize)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetTableRows(Table, PageNumber, PageSize);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }



        [HttpGet("AddTableRecords/{Table}")]
        public async Task<ApiResult<string>> AddTableRecords(string Table)
        {


            await _unitOfWork.CommonFunctionRepository.TruncateTableRow(Table);
            bool isAll = false;
            int PageNumber = 1;
            int PageSize = 2000;
            while (!isAll)
            {
                var data = SelectTableInsertScript(Table, PageNumber, PageSize);


                if (data == null || data.Rows.Count == 0)
                {
                    isAll = true;
                }
                else
                {
                    data.TableName = Table;
                    await _unitOfWork.CommonFunctionRepository.AddTableRows(data);
                    //InsertRow(chk.Text, data);
                    ++PageNumber;
                }

            }

            return new ApiResult<string>() { };

        }


        protected DataTable SelectTableInsertScript(string Table, int PageNumber, int PageSize)
        {
            DataTable dt = new DataTable();
            // Create a request for the URL.


            //WebRequest request = WebRequest.Create("https://localhost:44368/api/Service/GetDataFromProd?Table=" + Table + "&PageNumber=" + PageNumber + "&PageSize=" + PageSize);

            WebRequest request = WebRequest.Create("https://kdhteapi.rajasthan.gov.in/Api/api/CommonFunction/GetTableRows/" + Table + "/" + PageNumber + "/" + PageSize);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            string dataResult = "";
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                // Parse JSON
                var jObject = JObject.Parse(responseFromServer);
                var dataArray = jObject["Data"];

                // Convert to DataTable
                dt = JsonConvert.DeserializeObject<DataTable>(dataArray.ToString());

                //ds = JsonConvert.DeserializeObject<DataTable>(responseFromServer);

            }
            // Close the response.
            response.Close();
            return dt;
        }


        [HttpGet("GovtITICollege_DistrictWise/{DistrictID}/{EndTermId}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GovtITICollege_DistrictWise(int DistrictID, int EndTermId)
        {
            ActionName = " DistrictMaster_StateIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GovtITICollege_DistrictWise(DistrictID, EndTermId);
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

        [HttpPost("ITIGetStaff_InstituteWise")]
        public async Task<ApiResult<DataTable>> ITIGetStaff_InstituteWise([FromBody] StaffMasterDDLDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.ITIGetStaff_InstituteWise(body));
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


       
        #region  GetStatusFor RollNo And ENrollNo

        [HttpGet("GetCommonMasterDDLStatusByType/{type}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCommonMasterDDLStatusByType(string type)
        {
            ActionName = "GetCommonMasterDDLStatusByType(string type)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCommonMasterDDLStatusByType(type);
                    if (data.Count > 0)
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

        #endregion



        [HttpPost("NodalCenterList")]
        public async Task<ApiResult<DataTable>> NodalCenterList([FromBody] NodalCenterModel body)
        {
            ActionName = " NodalCenterList([FromBody] NodalCenterModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CommonFunctionRepository.NodalCenterList(body);
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

        [HttpPost("NodalCenterCreate")]
        public async Task<ApiResult<DataTable>> NodalCenterCreate([FromBody] NodalCenterModel body)
        {
            ActionName = " NodalCenterCreate([FromBody] NodalCenterModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                var Data = await _unitOfWork.CommonFunctionRepository.NodalCenterCreate(body);
                _unitOfWork.SaveChanges();
                if (Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DUPLICATE_CENTER;
                }
                else if (Data == -2)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DUPLICATE_CENTER_CODE;
                }
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
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

        [HttpPost("GetNodalCenter/{InstituteID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetNodalCenter(int InstituteID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetNodalCenter(InstituteID);
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("ItiTrade/{DepartmetnID}/{StreamType}/{EndTermId}/{InstituiteID}")]
        public async Task<ApiResult<DataTable>> ItiTrade(int DepartmetnID = 0, int StreamType = 0, int EndTermId = 0,int InstituiteID=0)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ItiTrade(DepartmetnID, StreamType, EndTermId,InstituiteID);
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

        [HttpGet("DC2ndYear_BranchesDDL/{CourseType}/{CoreBranch}")]
        public async Task<ApiResult<DataTable>> DC2ndYear_BranchesDDL(int CourseType, int CoreBranch)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.DC2ndYear_BranchesDDL(CourseType, CoreBranch));
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

        [HttpGet("ITI_SemesterMaster/{Parameter1}/{Parameter2}")]
        public async Task<ApiResult<DataTable>> ITI_SemesterMaster(int Parameter1 =0 , string Parameter2 ="")
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ITI_SemesterMaster(Parameter1 , Parameter2);
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

        [HttpPost("ExamSessionConfiguration")]
        public async Task<ApiResult<DataTable>> ExamSessionConfiguration(SessionConfigModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ExamSessionConfiguration(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("UnPublishData")]
        public async Task<ApiResult<DataTable>> UnPublishData(UnPublishDataModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.UnPublishData(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetCollegeDetails/{collegeID}")]
        public async Task<ApiResult<DataTable>> GetCollegeDetails(int collegeID)
        {
            ActionName = "GetCollegeDetails(int collegeID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCollegeDetails(collegeID);
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


        [HttpPost("BterGetBranchbyCollege")]
        public async Task<ApiResult<DataTable>> BterGetBranchbyCollege(BterCollegesSearchModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.BterGetBranchbyCollege(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("GetCommonSubjectDetailsDDL")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetCommonSubjectDetailsDDL(CommonDDLCommonSubjectModel model)
        {
            ActionName = "GetCommonSubjectDetailsDDL(CommonDDLCommonSubjectModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetCommonSubjectDetailsDDL(model);
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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

        [HttpPost("GetAllotmentMaster")]
        public async Task<ApiResult<DataTable>> GetAllotmentMaster(CommonDDLCommonSubjectModel request)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetAllotmentMaster(request);
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetExamResultType")]
        public async Task<ApiResult<DataTable>> GetExamResultType()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetExamResultType();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpGet("ItiShiftUnitDDL/{ID}/{FinancialYearID}/{CourseTypeID}/{InstituteID}")]
        public async Task<ApiResult<DataTable>> ItiShiftUnitDDL(int ID , int FinancialYearID, int CourseTypeID, int InstituteID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ItiShiftUnitDDL(ID, FinancialYearID, CourseTypeID, InstituteID);
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


        [HttpPost("NodalInstituteList/{InstituteID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> NodalInstituteList(int InstituteID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.NodalInstituteList(InstituteID);
                    if (data.Count > 0)
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("DDL_GroupCode_ExaminerWise_Reval")]
        public async Task<ApiResult<DataTable>> DDL_GroupCode_ExaminerWise_Reval(DDL_GroupCode_ExaminerWiseModel model)
        {
            ActionName = "DDL_GroupCode_ExaminerWise_Reval(DDL_GroupCode_ExaminerWiseModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DDL_GroupCode_ExaminerWise_Reval(model);
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

        [RoleActionFilter(EnumRole.CenterSuperintendent_Eng, EnumRole.CenterSuperintendent_NonEng)]
        [HttpPost("StudentListForAdmitCard_CS")]
        public async Task<ApiResult<DataTable>> StudentListForAdmitCard_CS(StudentAdmitCardDownloadModel model)
        {
            ActionName = "DDL_GroupCode_ExaminerWise_Reval(DDL_GroupCode_ExaminerWiseModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.StudentListForAdmitCard_CS(model);
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


        [HttpGet("GetITICampusPostMasterDDL/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITICampusPostMasterDDL(int DepartmentID)
        {
            ActionName = "GetCampusPostMasterDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITICampusPostMasterDDL(DepartmentID);
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

        [HttpGet("GetITICampusWiseHiringRoleDDL/{campusPostId}/{DepartmentID}")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetITICampusWiseHiringRoleDDL(int campusPostId, int DepartmentID)
        {
            ActionName = "GetITICampusWiseHiringRoleDDL()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetITICampusWiseHiringRoleDDL(campusPostId, DepartmentID);
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


        [HttpGet("GetPublicInfoStatus/{DepartmentId}")]
        public async Task<ApiResult<DataTable>> GetPublicInfoStatus(int DepartmentId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetPublicInfoStatus(DepartmentId);
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

        [HttpGet("ITIStreamMasterByCampus/{CampusPostID}/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> ITIStreamMasterByCampus(int CampusPostID, int DepartmentID, int EndTermId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.ITIStreamMasterByCampus(CampusPostID, DepartmentID, EndTermId);
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

        [HttpPost("DDL_RWHEffectedEndTerm")]
        public async Task<ApiResult<DataTable>> DDL_RWHEffectedEndTerm(DDL_RWHEffectedEndTermModel model)
        {
            ActionName = "DDL_RWHEffectedEndTerm(DDL_RWHEffectedEndTermModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.DDL_RWHEffectedEndTerm(model);
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


        [HttpGet("GetMigrationType")]
        public async Task<ApiResult<DataTable>> GetMigrationType()
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CommonFunctionRepository.GetMigrationType();
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
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("ITI_DeirectAdmissionOptionFormData")]
        public async Task<ApiResult<DataTable>> ITI_DeirectAdmissionOptionFormData([FromBody] ItiTradeSearchModel body)
        {
            ActionName = "ITI_DeirectAdmissionOptionFormData([FromBody] ItiTradeSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CommonFunctionRepository.ITI_DeirectAdmissionOptionFormData(body));
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

    }
}


