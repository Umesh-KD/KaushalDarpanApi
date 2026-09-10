using AutoMapper;
using Hangfire;
using Kaushal_Darpan.Api.Controllers;
using Kaushal_Darpan.Api.HangFireServices;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.Student;

namespace Kaushal_Darpan.Api.Code.Hangfire
{
    public class HangfireJob
    {
        //private readonly IConfiguration _configuration;

        private readonly IUnitOfWork _unitOfWork;
        private readonly ISidhDataExport _sidhDataExport;
       
        public HangfireJob(IUnitOfWork unitOfWork, ISidhDataExport sidhDataExport)
        {
            _unitOfWork = unitOfWork;
            _sidhDataExport = sidhDataExport;
        }
        public async Task SidhDataExport()
        {
            // Call your stored procedure / service here


            var Result = await _sidhDataExport.ProcessSidhData();
            

            await Task.CompletedTask;
        }
    }
    

   
}
