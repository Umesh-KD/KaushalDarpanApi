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
        public int? TrainingTypeID { get; set; }
        public int? TrainingStatus { get; set; }
        public string? ComplitionTrainingDoc { get; set; }
        public string? Dis_complitionTrainingDoc { get; set; }
        public string? Remark { get; set; }
    }

    public class StaffTrainingDetailSearchData
    {
        public int? StaffTrainingDetailID { get; set; }
        public int? UserID { get; set; }
        public int? StaffID { get; set; }
        public int? StatusID { get; set; }
        public string? Action { get; set; }
    }


    public class StaffTrainingStatusUpdateDataModel
    {
        public int? StaffTrainingDetailID { get; set; }
        public int? TrainingStatus { get; set; }
        public string? Remark { get; set; }
        public int? CreatedBy { get; set; }
        public string? jsonData { get; set; }
    }


    //// BTER Staff Transfer System

    public class BTER_GetStaffPersonalDetailsModel
    {
        public int? StaffUserID { get; set; }
        public int? StaffID { get; set; }
        public string? SSOID { get; set; }
        public string Remark { get; set; }
        public int? ModifyBy { get; set; }
    }


}


