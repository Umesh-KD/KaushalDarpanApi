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
        public int? RoleID { get; set; }
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
        public int? RoleID { get; set; }
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

    public class BTER_EM_TransferSystemModule
    {
        public int TransferSystemID { get; set; }
        public int UserID { get; set; }
        public int StaffID { get; set; }
        public string? SSOID { get; set; }
        public int TransferCategoryID { get; set; }
        public string? ReasonDescription { get; set; }
        public string? SupportingDocuments { get; set; }
        public string? SupportingDocumentsDis { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public int TransferStatus { get; set; }
        public int NonGazettedID { get; set; }

        public string? EmployeeDesignation { get; set; }
        public string? EmployeeName { get; set; }
        public string? NonGazetteName { get; set; }
        public int RoleID { get; set; }
        public List<BTER_EM_TransferSystemExtModule>? TransferExtDetails { get; set; }

    }

    public class BTER_EM_TransferSystemExtModule
    {
        public int ID { get; set; }
        public int TransferSystemID { get; set; }
        public int OfficeID { get; set; }
        public int PostID { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int Priority { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public int FinalApproveStatus { get; set; }
    }

    public class EM_TransferSystemSearchModel
    {
        public string? Action { get; set; }
        public string? Remark { get; set; }
        public int TransferSystemID { get; set; }
        public int StaffID { get; set; }
        public int ActionBy { get; set; }
        public int StatusID { get; set; }
        public int EmployeeType { get; set; }
        public int InstituteID { get; set; }
        public int CategoryID { get; set; }
        public int RoleID { get; set; }

        public string? RelievingDoc { get; set; }
        public string? RelievingDoc_Dis { get; set; }
        public string? RelievingDate { get; set; }

    }


    public class TransferSystemUpdateDataModel
    {
        public int? TransferSystemID { get; set; }
        public int? TrainingStatus { get; set; }
        public string? Remark { get; set; }
        public int? CreatedBy { get; set; }
        public string? jsonData { get; set; }
        public int? ID { get; set; }
        public int? RoleID { get; set; }
    }


    public class BTERStaffManualRequestModel
    {
        public string? EmployeeDesignation { get; set; }
        public string? EmployeeName { get; set; }
        public string? NonGazetteName { get; set; }

        public int? StaffID { get; set; }
        public int? TransfercateID { get; set; }

        public string? ReasonDescription { get; set; }
        public string? SupportingDocuments { get; set; }
        public string? SupportingDocuments_Dis { get; set; }
        public string? vReasonDescription { get; set; }

        public int? NonGazettedID { get; set; }
        public int? OfficeID { get; set; }
        public int? DistrictID { get; set; }
        public int? InstituteID { get; set; }
        public int? PostID { get; set; }
        public int? PriorityID { get; set; }

        public int? CreatedBy { get; set; }
        public int? UserID { get; set; }
        public string? SSOID { get; set; }

        public int? To_OfficeID { get; set; }
        public int? To_PostID { get; set; }
        public int? To_ddlDistrictID { get; set; }
        public int? To_ddlCollege { get; set; }
        public int? RoleID { get; set; }
    }

    public class TransferSystemShowDataModel
    {
        public int TransferSystemID { get; set; }

        public string? Name { get; set; }

        public string? SSOID { get; set; }

        public string? TransferCategory { get; set; }

        public string? ReasonDescription { get; set; }

        public string? OrderSupportingDocument { get; set; }

        public string? OrderSupportingDocument_Dis { get; set; }

        public string? CreatedDate { get; set; }

        public string? RelievingStatusName { get; set; }

        public bool ISNonGazetted { get; set; }

        public int StatusID { get; set; }

        public string? OfficeName { get; set; }

        public string? DesignationName { get; set; }

        public string? DistrictName { get; set; }

        public string? InstituteName { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public string? EmployeeID { get; set; }
        public string? NAME { get; set; }
        public string? DateOfBirth { get; set; }
        public string? MobileNumber { get; set; }
        public string? LastPostName { get; set; }
        public string? RequestRemarks { get; set; }
        public string? TransferPostName { get; set; }
        public string? TransferOfficeName { get; set; }
        public string? RequestDate { get; set; }
        public string? RelivingTime { get; set; }
        public string? ApproveName { get; set; }
        public string? RelievingDate { get; set; }
    }
}


