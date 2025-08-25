using Kaushal_Darpan.Api.Code.Security;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase, IDisposable
    {
        private bool disposedValue;

        public abstract string PageName { get; }
        public abstract string ActionName { get; set; }

        public BaseController()
        {
        }
        public CustomPrincipal CustomPrincipal => new CustomPrincipal(User);

        #region Common Function
        protected async Task<string> CreateAuthentication(UserSessionModel model)
        {
            if (model != null)
            {
                return await GenrateJwtToken(model);
            }
            return null;
        }
        protected async Task<string> GenrateJwtToken(UserSessionModel model)
        {
            return await Task.Run(() =>
            {
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,model.UserID.ToString()),
                    new Claim(ClaimTypes.Name,model.UserName),
                    new Claim(ClaimTypes.Email,model.Email),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                    new Claim(nameof(model.RoleIDs),model.RoleIDs),
                    new Claim(nameof(model.RoleNames),model.RoleNames),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.JwtSecret));

                var token = new JwtSecurityToken(
                    issuer: ConfigurationHelper.JwtIssuer,
                    audience: ConfigurationHelper.JwtAudience,
                    expires: DateTime.Now.AddMinutes(ConfigurationHelper.SessionTime),
                    claims: authClaim,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }
        protected async Task CreateErrorLog(NewException nex, IUnitOfWork unitOfWork)
        {
            await Task.Run(async () =>
            {
                var data = new Tbl_Trn_ErrorLog();
                if (nex.Ex.Message.Contains("@SqlExecutableQuery = "))
                {
                    data.ErrorDescription = nex.Ex.Message;
                    data.CreatedDate = DateTime.Now;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Error = {nex.Ex.Message}");
                    sb.AppendLine($"Page = {nex.PageName}.{nex.ActionName}");
                    data.ErrorDescription = sb.ToString();
                    data.CreatedDate = DateTime.Now;
                }
                await unitOfWork.ErrorLogs.AddErrorLog(data);
                unitOfWork.SaveChanges();
            });
        }
        #endregion

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseController()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        
        #endregion
    }
}
