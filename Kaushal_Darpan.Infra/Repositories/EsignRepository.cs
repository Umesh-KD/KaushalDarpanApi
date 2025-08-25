using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AadhaarEsignAuth;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.TimeTable;
using Kaushal_Darpan.Models.UserMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    //public class EsignService(IUnitOfWork IUnitOfWorkRepository, HttpClient httpClient) : IEsignService
    //{
    //    private readonly DBContext _dbContext;
    //    private string _sqlQuery;
    //    private readonly HttpClient _httpClient = httpClient;
    //    private readonly string objClientCode = "KAUSHAL_D_BTER_TEST_RLTBNJ";
    //    private readonly IUnitOfWork _IUnitOfWorkRepository = IUnitOfWorkRepository;

    //    public EsignService(DBContext dbContext)
    //    {
    //        _dbContext = dbContext;
            
    //    }


    //    public async Task<EsignModel> GetSignedXML(GetSignedXMLModel objModel)
    //    {
    //        var objTxn = objClientCode + "_" + System.DateTime.Now.ToString("ddMMyyyyHHmmssfffff");
    //        try
    //        {
    //            var url = "";
    //            var requestBody = new
    //            {
    //                pdfFile1 = "BASE64OFPDFFILE", //objModel.Base64PdfFile,
    //                signatureOnPageNumber = objModel.SignatureOnPageNumber,
    //                clientCode = objClientCode,
    //                xcord = objModel.Xcord,
    //                ycord = objModel.Ycord,
    //                responseUrl = objModel.ResponseUrl,
    //                txn = objTxn,
    //                designation = objModel.Designation,
    //                location = objModel.Location,
    //                sigsize = objModel.Sigsize,
    //            };
    //            var jsonBody = JsonConvert.SerializeObject(requestBody);
    //            var request = new HttpRequestMessage(HttpMethod.Post, url)
    //            {
    //                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
    //            };
    //            var response = await _httpClient.SendAsync(request);
    //            response.EnsureSuccessStatusCode();
    //            var responseData = response.Content.ReadAsStringAsync().Result;
    //            var data = JsonConvert.DeserializeObject<GetSignedXMLResponseModel>(responseData);
    //            //if(data!=null && !string.IsNullOrEmpty(data.signedXMLData))
    //            //    data.signedXMLData = DecriptFromBase64(data.signedXMLData).Replace("\\", "");
    //            return new EsignModel()
    //            {
    //                Status = "true",
    //                Message = "",
    //                Data = data
    //            };
    //        }
    //        catch (Exception Ex)
    //        {
    //            return new EsignModel()
    //            {
    //                Status = "false",
    //                Message = Ex.Message
    //            };
    //        }
    //    }

    //    public async Task<EsignModel> GetSignedPDF(GetSignedPDFModel objModel)
    //    {
    //        try
    //        {
    //            var dataFromDB = _IUnitOfWorkRepository.EsignRepository.GetEsignXMLFromDB(objModel.Txn).Result.Data;
    //            var url = "https://apitest.sewadwaar.rajasthan.gov.in/app/live/rajEsign/rajApi/esignApi/GetSignedPDF?client_id=0df7e4099e5fad031ff871400dc07152";
    //            var requestBody = new
    //            {
    //                esignResponse = dataFromDB?.esignData,
    //                clientCode = objClientCode,
    //                txn = objModel.Txn,
    //                username = objModel.UserNameInAadhar,
    //            };
    //            var jsonBody = JsonConvert.SerializeObject(requestBody);
    //            var request = new HttpRequestMessage(HttpMethod.Post, url)
    //            {
    //                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
    //            };
    //            var response = await _httpClient.SendAsync(request);
    //            response.EnsureSuccessStatusCode();
    //            var responseData = response.Content.ReadAsStringAsync().Result;
    //            var data = JsonConvert.DeserializeObject<GetSignedPDFResponseModel>(responseData);
    //            try
    //            {
    //                if (data?.responseCode == "REA_001" && !string.IsNullOrEmpty(data?.signedPDFUrl))
    //                {
    //                    string fileUrl = data.signedPDFUrl;
    //                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/EsignPdf/");
    //                    if (!Directory.Exists(filePath))
    //                        Directory.CreateDirectory(filePath);
    //                    string fileName = "Uploads/EsignPdf/" + objModel.Category + "_" + objModel.Txn + ".pdf";
    //                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
    //                    await DownloadFileAsync(fileUrl, savePath);
    //                    var objDbUpdateData = new DbUpdateEsignPdfFileModel()
    //                    {
    //                        CategoryId = objModel.Category,
    //                        Id = objModel.Id,
    //                        signedPDFUrl = fileName,
    //                        txn = data?.txn
    //                    };
    //                    var objDbUpdate = _IUnitOfWorkRepository.EsignRepository.DbUpdateEsignPdfFile(objDbUpdateData);
    //                }
    //            }
    //            catch
    //            {
    //                if (data?.responseCode == "REA_001" && !string.IsNullOrEmpty(data?.signedPDFUrl))
    //                {
    //                    var objDbUpdateData = new DbUpdateEsignPdfFileModel()
    //                    {
    //                        CategoryId = objModel.Category,
    //                        Id = objModel.Id,
    //                        signedPDFUrl = data.signedPDFUrl,
    //                        txn = data?.txn
    //                    };
    //                    var objDbUpdate = _IUnitOfWorkRepository.EsignRepository.DbUpdateEsignPdfFile(objDbUpdateData);
    //                }
    //            }
    //            return new EsignModel()
    //            {
    //                Status = true,
    //                Message = "",
    //                Data = data
    //            };
    //        }
    //        catch (Exception Ex)
    //        {
    //            return new EsignModel()
    //            {
    //                Status = false,
    //                Message = Ex.Message
    //            };
    //        }
    //    }

    //    public async Task<DbEsignDataResponseModel> GetEsignXMLFromDB(string txn)
    //    {
    //        try
    //        {
    //            var data = _IUnitOfWorkRepository.EsignRepository.GetEsignXMLFromDB(txn);
    //            return await data;
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //    }
    //    public async Task<DataTable> GetEsignXMLFromDB(CenterObserverSearchModel body)
    //    {
            
    //        try
    //        {
    //            return await Task.Run(async () =>
    //            {
    //                DataTable dataTable = new DataTable();
    //                using (var command = _dbContext.CreateCommand())
    //                {
    //                    command.CommandType = CommandType.StoredProcedure;
    //                    command.CommandText = "USP_CenterObserver";
    //                    command.Parameters.AddWithValue("@Action", "GetAllData");
    //                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
    //                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
    //                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
    //                    command.Parameters.AddWithValue("@DeploymentStatus", body.DeploymentStatus);
    //                    command.Parameters.AddWithValue("@ExamDate", body.ExamDate);
    //                    command.Parameters.AddWithValue("@TeamName", body.TeamName);
    //                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
    //                    dataTable = await command.FillAsync_DataTable();
    //                }
    //                return dataTable;
    //            });
    //        }
    //        catch (Exception ex)
    //        {
    //            var errorDesc = new ErrorDescription
    //            {
    //                Message = ex.Message,
    //                PageName = _pageName,
    //                ActionName = _actionName,
    //                SqlExecutableQuery = _sqlQuery
    //            };
    //            var errordetails = CommonFuncationHelper.MakeError(errorDesc);
    //            throw new Exception(errordetails, ex);
    //        }
    //    }

    //    private string EncodeToBase64(string objData)
    //    {
    //        if (string.IsNullOrEmpty(objData))
    //            return objData;
    //        var bytes = Encoding.UTF8.GetBytes(objData);
    //        objData = Convert.ToBase64String(bytes);
    //        return objData;
    //    }

    //    private string DecriptFromBase64(string objData)
    //    {
    //        if (string.IsNullOrEmpty(objData))
    //            return objData;
    //        var bytes = Convert.FromBase64String(objData);
    //        objData = Encoding.UTF8.GetString(bytes);
    //        return objData;
    //    }

    //    private static async Task DownloadFileAsync(string fileUrl, string savePath)
    //    {
    //        using (HttpClient client = new HttpClient())
    //        {
    //            // Get the file as a byte array
    //            byte[] fileBytes = await client.GetByteArrayAsync(fileUrl);

    //            // Write the byte array to the specified file path
    //            await File.WriteAllBytesAsync(savePath, fileBytes);
    //        }
    //    }
    //}


}
