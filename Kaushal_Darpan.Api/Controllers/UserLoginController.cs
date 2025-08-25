using AutoMapper;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserLoginController : BaseController
    {
        public override string PageName => "UserLoginController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserLoginController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        /*here post is used for getting the data bcz of sensitiv data(password)*/
        [HttpPost("UserLogin")]
        public async Task<ApiResult<UserSessionModel>> UserLogin(UserLoginModel model)
        {
            return await Task.Run(async () =>
            {
                ActionName = "UserLogin(UserLoginModel model)";
                var result = new ApiResult<UserSessionModel>();
                try
                {
                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    var userEntity = await _unitOfWork.Users.GetUserByUserEmailAndPass(model.Email, model.Password);

                    if (userEntity == null)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
                        return result;
                    }

                    //if want role base
                    var userSessionModel = new UserSessionModel
                    {
                        Email = userEntity.UserEmail,
                        UserName = userEntity.UserName,
                        UserID = userEntity.Id,
                        //RoleIDs = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleId)),
                        //RoleNames = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleName))
                    };

                    // set auth and get token
                    var token = await CreateAuthentication(userSessionModel);
                    Response.Headers.Append("X-AuthToken", token);

                    result.State = EnumStatus.Success;
                    result.Data = userSessionModel;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetUserById")]
        public async Task<ApiResult<UserModel>> GetUserById([FromQuery] int userId)
        {
            return await Task.Run(async () =>
            {
                ActionName = "GetUserById([FromQuery] int userId)";
                var result = new ApiResult<UserModel>();
                try
                {
                    var data = await _unitOfWork.Users.GetUserById(userId);
                    _unitOfWork.SaveChanges();
                    var modelData = _mapper.Map<UserModel>(data);

                    result.State = EnumStatus.Success;
                    result.Data = modelData;
                }
                catch (Exception ex)
                {
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
    }
}
