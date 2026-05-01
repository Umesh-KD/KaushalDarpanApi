using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.PostMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostMasterController : BaseController
    {
        public override string PageName => "DesignationMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PostMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // ================= GET ALL =================
        [HttpPost("GetAllPosts")]
        public async Task<ApiResult<DataTable>> GetAllPosts([FromBody] PostMasterModel request)
        {
            var result = new ApiResult<DataTable>();

            try
            {
                var data = await _unitOfWork.PostMasterRepository.GetAllData(request);

                result.Data = data;

                if (data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data loaded successfully!";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No records found!";
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }

            return result;
        }

        // ================= GET BY ID =================
        [HttpGet("GetByID/{postID}")]
        public async Task<ApiResult<PostMasterModel>> GetByID(int postID)
        {
            var result = new ApiResult<PostMasterModel>();

            try
            {
                var data = await _unitOfWork.PostMasterRepository.GetById(postID);

                if (data != null)
                {
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = "Data loaded successfully!";
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found!";
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }

            return result;
        }

        // ================= SAVE (ADD/UPDATE) =================
        [HttpPost("SavePost")]
        public async Task<ApiResult<bool>> SavePost([FromBody] PostMasterModel request)
        {
            var result = new ApiResult<bool>();

            try
            {
                result.Data = await _unitOfWork.PostMasterRepository.SaveData(request);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data)
                {
                    result.State = EnumStatus.Success;
                    if (request.PostID == 0)
                    {
                        result.Message = "Saved successfully!";
                    }
                    else
                    {
                        result.Message = "Updated successfully!";
                    }

                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (request.PostID == 0)
                        result.ErrorMessage = "Error adding data!";
                    else
                        result.ErrorMessage = "Error updating data!";
                }
            }
            catch (Exception ex)
            {
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = "SavePost",
                    Ex = ex,
                };

                await CreateErrorLog(nex, _unitOfWork);

                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }

            return result;
        }




        // ================= DELETE =================
        [HttpDelete("DeletePostByID/{postID}/{modifyBy}")]
        public async Task<ApiResult<bool>> DeletePostByID(int postID, int modifyBy)
        {
            var result = new ApiResult<bool>();

            try
            {
                var request = new PostMasterModel
                {
                    PostID = postID,
                    ModifyBy = modifyBy
                };

                result.Data = await _unitOfWork.PostMasterRepository.DeleteDataById(request);
                await _unitOfWork.SaveChangesAsync();

                if (result.Data)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Deleted successfully!";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "Delete failed!";
                }
            }
            catch (Exception ex)
            {
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = "DeletePostByID",
                    Ex = ex,
                };

                await CreateErrorLog(nex, _unitOfWork);

                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }

            return result;
        }
    }
}