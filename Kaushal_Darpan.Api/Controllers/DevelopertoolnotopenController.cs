using AspNetCore.Reporting;
using AutoMapper;
using Azure.Core;
using DocumentFormat.OpenXml.Office2016.Excel;
using Kaushal_Darpan.Api.Report.ITI;
using Kaushal_Darpan.Api.Validators;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.AadhaarEsignAuth;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using static System.Net.WebRequestMethods;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopertoolnotopenController : BaseController
    {
        public override string PageName => "DevelopertoolnotopenController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DevelopertoolnotopenController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("devtools-config")]
        public IActionResult GetDevToolsConfig()
        {
            bool _enableDevTools = false; // default restrict the developer tool by write the script

            // put ::1 for localhost and then comma seperated ipaddress those you allowed to open developer tool
            string filePath = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.NotRunDeveloperTool, "NotRunDeveloperTool.txt");

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    string ip = CommonFuncationHelper.GetIpAddress();

                    // text file
                    var fileContents = System.IO.File.ReadAllText(filePath);
                    var allowedIps = fileContents.Split(',');

                    // if ip not found then not open developer tool
                    if (allowedIps.Contains(ip))
                    {
                        _enableDevTools = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Ok(new
            {
                enableDevTools = _enableDevTools
            });
        }

    }
}
