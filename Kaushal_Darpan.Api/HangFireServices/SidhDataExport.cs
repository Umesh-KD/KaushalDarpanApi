using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.Student;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Api.HangFireServices
{
    public class SidhDataExport : ISidhDataExport
    {

        private readonly IUnitOfWork _unitOfWork;

        public SidhDataExport(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResult<DataTable>> ProcessSidhData()
        {
            var Outerresult = new ApiResult<DataTable>();
            var Inner_result = new ApiResult<DataTable>();
            var Fresult = new ApiResult<RootUploadStatusCheckDataModel>();
            int ResultSet = 0;

            List<NCVTUploadStatusCheckDataModel> request = new List<NCVTUploadStatusCheckDataModel>();
            UploadTrainee_LogsModel body = new UploadTrainee_LogsModel();
            Outerresult.Data = await _unitOfWork.ITIDataMasterRepository.GetTraineeLogsList(body);

            var apidetails = await _unitOfWork.ITIDataMasterRepository.GetNcvt_APIDetails();
            NCVT_APIDetailsModel resultList = CommonFuncationHelper.ConvertDataTable<NCVT_APIDetailsModel>(apidetails);
            var token = await ThirdPartyServiceHelper.GetAccessTokenAsync(resultList);

            if (resultList == null)
            {
                Outerresult.State = EnumStatus.Error;
                Outerresult.ErrorMessage = "Service details not found.";
                //return result;
            }
            else if (token == null || token.status != "success" || token.data == null)
            {
                Outerresult.State = EnumStatus.Error;
                Outerresult.ErrorMessage = "Failed to generate access token.";
                Outerresult.Message = token?.message ?? "No token response.";
                //return result;
            }
            else
            {
                if (Outerresult.Data != null)
                {
                    var responseDataList = new List<ResponseData>();
                    foreach (DataRow Dr in Outerresult.Data.Rows)
                    {
                        resultList.log_Id = Dr["LogID"].ToString();
                        resultList.TokenNo = token.data;
                        var response = await ThirdPartyServiceHelper.CheckUploadStatusNew(resultList);
                        Fresult.Data = response!;

                        if (Fresult?.Data?.results != null)
                        {
                            foreach (var item in Fresult.Data.results)
                            {
                                ResultSet = 1;
                                responseDataList.Add(new ResponseData
                                {
                                    ErrorDescription = item.ErrorDescription,
                                    MISITICode = item.MISITICode,
                                    MobileNumber = item.MobileNumber,
                                    RecordStatus = item.RecordStatus,
                                    Shift = item.Shift,
                                    StateRegNumber = item.StateRegNumber,
                                    Trade = item.Trade,
                                    TraineeName = item.TraineeName,
                                    Unit = item.Unit
                                });
                            }
                        }
                    }

                    if (responseDataList.Count > 0)
                    {
                        foreach (ResponseData item in responseDataList)
                        {
                            var InnerresponseDataList = new List<ResponseData>();

                            InnerresponseDataList.Add(item);

                            var saveResult = await _unitOfWork.ITIStudentEnrollmentRepository.updateOnResponseData(InnerresponseDataList);

                            if (saveResult > 0)
                                Inner_result.Message = "Response data updated successfully.";
                            else
                                Inner_result.Message = "No data updated.";

                            break;
                        }
                    }
                    else
                    {
                        Inner_result.Message = "No records found in API response.";
                    }

                    Inner_result.State = EnumStatus.Success;
                }
                else
                {
                    Outerresult.State = EnumStatus.Error;
                    Outerresult.ErrorMessage = "nodata found";
                }
            }

            Outerresult = ResultSet == 1 ? Inner_result : Outerresult;

            return Outerresult;
        }

        public async Task<ApiResult<DataTable>> UploadNcvtStudentData()
        {
            var result = new ApiResult<DataTable>();
            var Newresult = new ApiResult<DataTable>();

            ChunksSearchModel model = new ChunksSearchModel();
            //Get All Principal UserID,RoleID,pageSize,GetNCVTChunksList,AcedmicYearID,IsAadharData

            //=========================Start Data=========================
            result.Data = await _unitOfWork.ITIStudentEnrollmentRepository.GetNcvtStudentData_Chunks(model);
            result.State = EnumStatus.Success;
            if (result.Data.Rows.Count == 0)
            {
                result.State = EnumStatus.Success;
                result.Message = "No record found.!";
                return result;
            }
            else
            {
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            //=========================EnD Data=========================

            if (result.Data.Rows.Count > 0)
            {
                var apidetails = await _unitOfWork.ITIStudentEnrollmentRepository.GetNcvt_APIDetails();

                NCVT_APIDetailsModel resultList = CommonFuncationHelper.ConvertDataTable<NCVT_APIDetailsModel>(apidetails);

                if (resultList != null)
                {
                    foreach (DataRow DR in result.Data.Rows)
                    //   foreach (var record in result.Data)
                    {
                        //get token 
                        NCVTChunkInfoDataModel obj = new NCVTChunkInfoDataModel();

                        obj.AIDS = DR["AIDS"].ToString();
                        var token = await ThirdPartyServiceHelper.GetAccessTokenAsync(resultList);

                        CommonFuncationHelper.WriteTextLog($"[tokendATA] token: {token}");
                        if (token != null && token.status == "success" && token.data != null)
                        {
                            var dataresult = await _unitOfWork.ITIStudentEnrollmentRepository.GetNCVTStudentData(obj);
                            List<ITITraineeUploadModel> request = new List<ITITraineeUploadModel>();
                            //get data from database 
                            var response = await ThirdPartyServiceHelper.UploadTraineeData(dataresult, "", token.data, resultList?.DataPushApiUrl);

                            CommonFuncationHelper.WriteTextLog($"[responseaPI] Error: {response}");


                            if (response.State == EnumStatus.Success)
                            {
                                var apirespose = JsonConvert.DeserializeObject<RootDataModel>(response.Data);
                                if (apirespose.log_id != null)
                                {
                                    #region "LOGS"
                                    try
                                    {
                                        UploadTrainee_LogsModel logsData = new UploadTrainee_LogsModel();
                                        logsData.Response = response.Data;
                                        logsData.log_id = apirespose.log_id;
                                        var log = await _unitOfWork.ITIStudentEnrollmentRepository.SaveUploadTraineeLogs(logsData);
                                        await _unitOfWork.SaveChangesAsync();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    #endregion
                                    //result.Data = apirespose;
                                    result.State = EnumStatus.Warning;
                                    result.Message = apirespose?.Message;
                                    try
                                    {
                                        obj.Log_id = apirespose.log_id;
                                        var isSave = await _unitOfWork.ITIStudentEnrollmentRepository.updateLogIdOnData(obj);
                                        await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful
                                    }
                                    catch (Exception ex)
                                    {


                                    }

                                }
                                else
                                {

                                    //result.Data = apirespose;
                                    result.State = EnumStatus.Warning;
                                    result.Message = "Something went Wrong";
                                }
                            }
                            else
                            {
                                result.State = EnumStatus.Error;
                                result.Message = response?.Data;
                            }
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "something went wrong when generate token";
                            result.Message = token.message;

                        }
                    }
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "service details not found.";
                }

                result.Data = await _unitOfWork.ITIStudentEnrollmentRepository.GetNcvtStudentData_Chunks(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }

                return result;
            }
            return result;
        }
    }
}
