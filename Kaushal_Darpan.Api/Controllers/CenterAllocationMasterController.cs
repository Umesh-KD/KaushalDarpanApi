using AspNetCore.Reporting;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.NetworkInformation;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class CenterAllocationController : BaseController
    {
        public override string PageName => "CenterAllocationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CenterAllocationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] CenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CenterAllocationRepository.GetAllData(filterModel);
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


        [HttpPost("GetCenterSuperintendent")]
        public async Task<ApiResult<DataTable>> GetCenterSuperintendent([FromBody] CenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CenterAllocationRepository.CenterSuperintendent(filterModel);
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

        [HttpPost("DownloadCenterSuperintendent")]
        public async Task<ApiResult<string>> DownloadCenterSuperintendent([FromBody] CenterAllocationSearchFilter filterModel)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.CenterAllocationRepository.DownloadCenterSuperintendent(filterModel); 
                    if (data != null)
                    {
                        //report
                        var fileName = $"CenterSuperintendentReport{filterModel.Eng_NonEng}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/CenterSuperintendentReport.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("CenterSuperintendentReport", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;


                        DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                        ModInsert.FileName = fileName;
                        ModInsert.PDFType = (int)EnumPdfType.CenterSuperintendent;
                        ModInsert.Status = 11;
                        ModInsert.DepartmentID=1;
                        ModInsert.Eng_NonEng=filterModel.Eng_NonEng;
                        ModInsert.EndTermID=filterModel.EndTermID;
                      


                        var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("GenerateCenterSuperintendentOrder")]
        public async Task<ApiResult<string>> GenerateCenterSuperintendentOrder([FromBody] DownloadnRollNoModel filterModel)
        {

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                    ModInsert.PDFType = (int)EnumPdfType.CenterSuperintendent;
                    ModInsert.Status = 12;
                    ModInsert.DepartmentID = 1;
                    ModInsert.FileName = filterModel.FileName;
                    ModInsert.Eng_NonEng = filterModel.Eng_NonEng;
                    ModInsert.EndTermID = filterModel.EndTermID;
                    var data = await _unitOfWork.CenterAllocationRepository.SaveRollNumbePDFData(ModInsert);
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });

        }

        [HttpGet("GetStatusCenterSuperintendentOrder/{status}/{coursetype}")]
        public async Task<ApiResult<DataTable>> GetStatusCenterSuperintendentOrder(int status,int coursetype)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.CenterAllocationRepository.GetRollCenterSuperintendentOrder(status,coursetype);
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("GetInstituteByCenterID")]
        public async Task<ApiResult<DataTable>> GetInstituteByCenterID([FromBody] CenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetInstituteByCenterID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CenterAllocationRepository.GetInstituteByCenterID(filterModel);
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




        [HttpPost("SaveAllData")]
        public async Task<ApiResult<bool>> SaveAllData([FromBody] List<CenterAllocationtDataModel> request)
        {
            ActionName = "SaveAllData([FromBody] List<PlacementShortListStudentResponseModel> request)";
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
                    var isSave = await _unitOfWork.CenterAllocationRepository.SaveData(request);
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




        [HttpGet("GetByID/{PK_ID}")]
        public async Task<ApiResult<CenterMasterModel>> GetByID(int PK_ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CenterMasterModel>();
                try
                {
                    var data = await _unitOfWork.CenterMasterRepository.GetById(PK_ID);
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
        [HttpDelete("DeleteDataByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new CenterMasterModel
                    {
                        CenterID = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.CenterMasterRepository.DeleteDataByID(DeleteData_Request);
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


