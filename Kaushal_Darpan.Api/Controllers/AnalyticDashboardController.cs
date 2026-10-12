using AutoMapper;
using Hangfire.Dashboard;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.AnalyticsDashboard;
using Kaushal_Darpan.Models.CommonFunction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
        [Route("api/[controller]")]
        //[CustomeAuthorize]
        //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
        [ValidationActionFilter]
    public class AnalyticDashboardController : BaseController
    {

        public override string PageName => "AnalyticDashboardController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticDashboardController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAnalyticDashboard")]
        public async Task<ApiResult<AnalyticDashboardModel>> GetDashboardData()
        {
            var result = new ApiResult<AnalyticDashboardModel>();
            var data = new AnalyticDashboardModel
            {
                TotalInstitutions = 1384,
                TotalStudents = 984600,
                TotalStaff = 61200,
                AdmissionsFY = 88900,
                PlacementPercentage = 75.6,
                ActiveUsersToday = 42180,

                InstitutionDistribution = new()
                {
                    new() { District = "Jaipur", Count = 186 },
                    new() { District = "Jodhpur", Count = 142 },
                    new() { District = "Udaipur", Count = 118 },
                    new() { District = "Kota", Count = 104 },
                    new() { District = "Ajmer", Count = 97 },
                    new() { District = "Bikaner", Count = 88 },
                    new() { District = "Alwar", Count = 81 },
                    new() { District = "Bharatpur", Count = 74 },
                    new() { District = "Sikar", Count = 69 },
                    new() { District = "Others", Count = 341 }
                },
                EnrolmentYearWise = new()
                {
                    new() { Year = "2021", Count = 58000 },
                    new() { Year = "2022", Count = 61500 },
                    new() { Year = "2023", Count = 67800 },
                    new() { Year = "2024", Count = 74200 },
                    new() { Year = "2025", Count = 81500 },
                    new() { Year = "2026", Count = 88900 }
                },
                StudentCategoryDistribution = new()
                {
                    new() { Category = "General", Percentage = 28 },
                    new() { Category = "OBC", Percentage = 34 },
                    new() { Category = "SC", Percentage = 17 },
                    new() { Category = "ST", Percentage = 14 },
                    new() { Category = "EWS", Percentage = 7 }
                },
                GenderRatio = new() { MalePercentage = 57, FemalePercentage = 43 },
                PassPercentageBranchWise = new()
                {
                    new() { Branch = "Civil", Percentage = 82 },
                    new() { Branch = "Mech", Percentage = 78 },
                    new() { Branch = "EE", Percentage = 85 },
                    new() { Branch = "ECE", Percentage = 88 },
                    new() { Branch = "CS", Percentage = 91 },
                    new() { Branch = "Chem", Percentage = 80 }
                },
                VacancyPosition = new()
                {
                    new() { Role = "Faculty", Count = 1240 },
                    new() { Role = "Lab Staff", Count = 860 },
                    new() { Role = "Admin", Count = 410 },
                    new() { Role = "Support", Count = 690 }
                }
            };

            result.State = EnumStatus.Success;
            result.Data = data;

            return result;
        }
    }
}
