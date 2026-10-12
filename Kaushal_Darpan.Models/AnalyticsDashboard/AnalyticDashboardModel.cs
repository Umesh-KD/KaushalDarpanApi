using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.AnalyticsDashboard
{
    public class AnalyticDashboardModel
    {
        public int TotalInstitutions { get; set; }
        public int TotalStudents { get; set; }
        public int TotalStaff { get; set; }
        public int AdmissionsFY { get; set; }
        public double PlacementPercentage { get; set; }
        public int ActiveUsersToday { get; set; }

        public List<DistrictDistribution> InstitutionDistribution { get; set; } = new();
        public List<EnrollmentTrend> EnrolmentYearWise { get; set; } = new();
        public List<CategoryDistribution> StudentCategoryDistribution { get; set; } = new();
        public GenderRatioData GenderRatio { get; set; } = new();
        public List<BranchPassPercentage> PassPercentageBranchWise { get; set; } = new();
        public List<VacancyPosition> VacancyPosition { get; set; } = new();
    }

    public class DistrictDistribution { public string District { get; set; } = string.Empty; public int Count { get; set; } }
    public class EnrollmentTrend { public string Year { get; set; } = string.Empty; public int Count { get; set; } }
    public class CategoryDistribution { public string Category { get; set; } = string.Empty; public int Percentage { get; set; } }
    public class GenderRatioData { public int MalePercentage { get; set; } public int FemalePercentage { get; set; } }
    public class BranchPassPercentage { public string Branch { get; set; } = string.Empty; public int Percentage { get; set; } }
    public class VacancyPosition { public string Role { get; set; } = string.Empty; public int Count { get; set; } }
}
