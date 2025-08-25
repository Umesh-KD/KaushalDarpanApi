using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_InstructorModel
{
    public class ITI_InstructorModel
    {

        // Personal Details
        public int? id { get; set; }
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public string? FatherOrHusbandName { get; set; }
        public string? MotherName { get; set; }
        public string? Dob { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Category { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }

        // Permanent Address
        public string? PlotHouseBuildingNo { get; set; }
        public string? StreetRoadLane { get; set; }
        public string? AreaLocalitySector { get; set; }
        public string? LandMark { get; set; }
        public string? DdlState { get; set; }
        public string? DdlDistrict { get; set; }
        public string? PropTehsilID { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }

        // Correspondence Address
        public string? Correspondence_PlotHouseBuildingNo { get; set; }
        public string? Correspondence_StreetRoadLane { get; set; }
        public string? Correspondence_AreaLocalitySector { get; set; }
        public string? Correspondence_LandMark { get; set; }
        public string? Correspondence_ddlState { get; set; }
        public string? Correspondence_ddlDistrict { get; set; }
        public string? Correspondence_PropTehsilID { get; set; }
        public string? Correspondence_City { get; set; }
        public string? Correspondence_Pincode { get; set; }

        // Educational Qualification
        public string? Education_Exam { get; set; }
        public string? Education_Board { get; set; }
        public string? Education_Year { get; set; }
        public string? Education_Subjects { get; set; }
        public double? Education_Percentage { get; set; }

        // Technical Qualification
        public string? Tech_Exam { get; set; }
        public string? Tech_Board { get; set; }
        public string? Tech_Subjects { get; set; }
        public string? Tech_Year { get; set; }
        public double? Tech_Percentage { get; set; }

        // Employment Details
        public string? Pan_No { get; set; }
        public string? Employee_Type { get; set; }
        public string? Employer_Name { get; set; }
        public string? Employer_Address { get; set; }
        public string? Tan_No { get; set; }
        public string? Employment_From { get; set; }
        public string? Employment_To { get; set; }
        public double? Basic_Pay { get; set; }

        // Extra Fields
        public string? CreatedBy { get; set; }
        public int? DepartmentID { get; set; }
        public int? InstituteID { get; set; }

        public bool? IsDomicile { get; set; }  // Nullable for BIT
        public string? Aadhar { get; set; }
        public string? JanAadhar { get; set; }
    }


    public class ITI_InstructorDataSearchModel
    {
        // Personal Details
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public int? DepartmentID { get; set; }

        public int? RoleID { get; set; }

        public string? ApplicationNo { get; set; }
    }


    public class ITI_InstructorBindDataSearchModel
    {
        // Personal Details
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public int? DepartmentID { get; set; }

        //public string? ApplicationNo { get; set; }
    }

    public class ITI_InstructorApplicationNoDataSearchModel
    {
        public string? ApplicationID { get; set; }
    }

}

