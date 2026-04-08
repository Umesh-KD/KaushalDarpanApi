using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BTER_EstablishManagement
{
    public class BTER_EMStaffServiceDataModel
    {
    }

    public class StaffTrainingDetailDataModel
    {
        public int? StaffTrainingDetailID { get; set; }
        public string? OrganizinglnstituteName { get; set; }
        public int? CourseType { get; set; }
        public string? CourseName { get; set; }
        public int? DurationUnit { get; set; }
        public int? Duration { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? ModeOfTraining { get; set; }
        public string? Venue { get; set; }
        public int? UserID { get; set; }
        public int? StaffID { get; set; }
        public string? TrainingDoc { get; set; }
        public string? Dis_TrainingDoc { get; set; }

        public string? TrainingCourseType_str { get; set; }
        public string? DurationUnit_str { get; set; }
        public string? ModeOfTraining_str { get; set; }
    }

    public class StaffTrainingDetailSearchData
    {
        public int? StaffTrainingDetailID { get; set; }
        public int? UserID { get; set; }
        public int? StaffID { get; set; }
        public string? Action { get; set; }
    }
}
