using Kaushal_Darpan.Models.DispatchFormDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DispatchMaster
{
    public  class DispatchGroupModel
    {
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
        public int InstituteID { get; set; }
        public List<DispatchGroupCodeModel>? GroupDataModel { get; set; } 
    }


    public class DispatchGroupSearchModel
    {
        public int DispatchGroupID { get; set; }

        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int InstituteID { get; set; }

        public int Status { get; set; }
    }
    public class DispatchGroupCodeModel
    {
        public int InstituteID { get; set; }
        public string? GroupCode { get; set; }
        public int GroupID { get; set; }
        public int DispatchGroupID { get; set; }

        public string Name { get; set; }
        public string ExaminerCode { get; set; }
        public string InstituteName { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
       
    }

    public class InstituteGroupDetail
    {


        public string? PrincipleName { get; set; }
        public string? Post { get; set; }
        public string? MobileNo { get; set; }

        public List<DispatchGroupCodeModel>? GroupDataModel { get; set; }
    }


    public class DispatchStatusUpdate
    {

        public int DPGCID { get; set; }
        public int Status { get; set; }
        public int ModifyBy { get; set; }
        public int DispatchGroupID{ get; set; }
        public int GroupCodeID { get; set; }
    }


    public class ViewByIDDispatchGroupCodeModel
    {
        public int Id { get; set; }
        public int DispatchGroupID { get; set; }
        public int ExaminerStatusID { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string GroupCode { get; set; }
        public string DispatchNo { get; set; }
        public string DispatchDate { get; set; }
        public string ExaminerStatus { get; set; }
        public int GroupID { get; set; }
    }

    public class DispatchMasterStatusUpdate
    {

        public int DispatchID { get; set; }
        public int Status { get; set; }
        public int ModifyBy { get; set; }
     
    }


    public class CheckDateDispatchSearchModel 
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public string SPName { get; set; }

    }
    //--------bter-reval-----------

    public class RevalDispatchGroupModel
    {
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
        public int InstituteID { get; set; }
        public List<RevalDispatchGroupCodeModel>? GroupDataModel { get; set; }
    }


    public class RevalDispatchGroupCodeModel
    {
        public int InstituteID { get; set; }
        public string? GroupCode { get; set; }
        public int GroupID { get; set; }
        public int DispatchGroupID { get; set; }
        public string? ExaminerName { get; set; }
        public string Name { get; set; }
        public string ExaminerCode { get; set; }
        public string InstituteName { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int ExaminerID { get; set; }
        public int SubjectID { get; set; }
        public int GroupCodeID { get; set; }

    }

    public class RevalDeleteDispatchPrincipalGroupCodeCModel
    {
        public int ID { get; set; }
        public int ModifyBy { get; set; }
    }


    }
