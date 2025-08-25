
using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.UserMenuRights;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumUser.Admin,EnumUser.Guest)]
    //[ValidationActionFilter]
    public class UserMenuRightsController : BaseController
    {
        public override string PageName => "UserMenuRightsController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserMenuRightsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData()
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.UserMenuRightsRepository.GetAllData());
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


        [HttpPost("GetByID")]
        public async Task<ApiResult<List<UserMenuRightsModel>>> GetByID(UserAndRoleMenuModel model)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<UserMenuRightsModel>>();
                try
                {
                    var data = await _unitOfWork.UserMenuRightsRepository.GetById(model);
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




        [HttpPost("PrincipleMenu")]
        public async Task<ApiResult<List<UserMenuRightsModel>>> PrincipleMenu(UserAndRoleMenuModel model)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<UserMenuRightsModel>>();
                try
                {
                    var data = await _unitOfWork.UserMenuRightsRepository.PrincipleMenu(model);
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



        [HttpPost("SaveUserMenuRight")]
        public async Task<ApiResult<bool>> SaveUserMenuRight(List<UserMenuRightsModel> request)
        {
            ActionName = "SaveUserMenuRight(List<UserMenuRightsModel> request)";
            var result = new ApiResult<bool>();

            try
            {
                request.ForEach(x => { x.IPAddress = CommonFuncationHelper.GetIpAddress(); });
                result.Data = await Task.Run(() => _unitOfWork.UserMenuRightsRepository.SaveUserMenuRight(request));
                if (result.Data)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Saved successfully .!";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "There was an error adding data.!";
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
        }
    }
}
