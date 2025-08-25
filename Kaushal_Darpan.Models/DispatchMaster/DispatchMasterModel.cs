using Kaushal_Darpan.Models.DispatchMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DispatchFormDataModel
{
    public class DispatchFormDataModel
    {
        public int DispatchID { get; set; }
        public string DispatchDate { get; set; }
        public string CompanyName { get; set; }
        public string ChallanNo { get; set; }
        public string SupplierName { get; set; }
        public string SupplierVehicleNo { get; set; }
        public string SupplierMobileNo { get; set; }
        public string SupplierDate { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPost { get; set; }
        public string RecipientMobileNo { get; set; }
        public string RecipientDate { get; set; }

        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public string Remark { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string IPAddress { get; set; }
        public int Status { get; set; }

        public string CenterCode { get; set; }

        public List<BundelDataModel> BundelDataModel { get; set; } = new List<BundelDataModel>();

    }

    public class DispatchSearchModel : RequestBaseModel
    {
        public int DispatchID { get; set; }
        public string? DispatchDate { get; set; }
        public int CourseTypeID { get; set; }
        public int BundelID { get; set; }
        public int InstituteID { get; set; }
        public string? SSOID { get; set; }

        public int Status { get; set; }
        public int ExaminerStatus { get; set; }
        public string? Action { get; set; }




    }


    public class BundelDataModel : RequestBaseModel
    {
        public int BundelID { get; set; }
        public int DispatchID { get; set; }
        public string BundelNo { get; set; }
        public int No { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string IPAddress { get; set; }


        public string ActionType { get; set; }
        public string SearchDate { get; set; }

        public int CourseTypeID { get; set; }


        public string CenterCode { get; set; }
        public string? ExamDate { get; set; }
        public string SubjectCode { get; set; }
        public string BranchCode { get; set; }
        public string ExamShift { get; set; }
        public int TotalPresentStudent { get; set; }
        public int Status { get; set; }

        public string DispatchDate { get; set; }
        public string ChallanNo { get; set; }
        public int InstituteID { get; set; }
        public int ExamShiftID { get; set; }
        public bool selected { get; set; }

        public string CCCode { get; set; }

    }


    public class DispatchReceivedModel
    {
        public int DispatchID { get; set; }
        public string? DispatchDate { get; set; }
        public int CourseTypeID { get; set; }
        public int Status { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int DispatchReceivedID { get; set; }
        public int CreatedBy { get; set; }

        public string? IPAddress { get; set; }



    }

    public class DownloadDispatchReceivedSearchModel
    {
        public int DispatchID { get; set; }
        public string DispatchDate { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int BundelID { get; set; }

        public int Status { get; set; }
        public string Action { get; set; }
    }


    public class DispatchPrincipalGroupCodeDataModel
    {
        public int DPGCID { get; set; }
        public int DispatchGroupID { get; set; }
        public string DispatchDate { get; set; }
        public string CompanyName { get; set; }
        public string ChallanNo { get; set; }
        public string SupplierName { get; set; }
        public string SupplierVehicleNo { get; set; }
        public string SupplierMobileNo { get; set; }
        public string SupplierDate { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPost { get; set; }
        public string RecipientMobileNo { get; set; }
        public string RecipientDate { get; set; }

        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public string Remark { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string IPAddress { get; set; }
        public int Status { get; set; }

        public string CenterCode { get; set; }

        public int InstituteID { get; set; }

        public List<ViewByIDDispatchGroupCodeModel>? groupCodeModels { get; set; }

    }

    public class DispatchPrincipalGroupCodeSearchModel
    {
        public int DPGCID { get; set; }
        public int DispatchGroupID { get; set; }
        public string DispatchDate { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int BundelID { get; set; }
        public int InstituteID { get; set; }

        public int Status { get; set; }
        public string Action { get; set; }
    }

    public class UpdateStatusDispatchPrincipalGroupCodeModel
    {
        public int DPGCID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int BundelID { get; set; }
        public int InstituteID { get; set; }
        public int Status { get; set; }
        public string Action { get; set; }
        public int CreatedBy { get; set; }
    }

    public class ViewByDispatchMasterBundelNo
    {
        public int DispatchID { get; set; }
        public string ExamDate { get; set; }
        public string CenterCode { get; set; }
        public string DispatchDate { get; set; }
        public string ChallanNo { get; set; }
        public string BundalNo { get; set; }


    }


    public class UpdateFileHandovertoExaminerByPrincipalModel
    {
        public int DispatchGroupID { get; set; }
        public string FileName { get; set; }
        public string Remark { get; set; }
        public string Dis_File { get; set; }
        public int CreatedBy { get; set; }
        public string GroupCodeIDs { get; set; }
        public int Status { get; set; }
        public DateTime? DueDate { get; set; }

    }


    public class CompanyDispatchMasterModel
    {
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string SupplierName { get; set; }
        public string SupplierVehicleNo { get; set; }
        public string SupplierMobileNo { get; set; }
        public string SupplierDate { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public string Remark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string IPAddress { get; set; }
        public int Status { get; set; }

    }

    public class CompanyDispatchMasterSearchModel
    {
        public int CompanyID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }

    }



}
