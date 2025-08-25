using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIPlanning
{
    public class ITI_PlanningColleges
    {
        public int PlanningID { get; set; }
        public int? CollegeId { get; set; }
        public int InstitutionCategoryId { get; set; }
        public int InstituteManagementId { get; set; }
        public int TrustSociety { get; set; }
        public string? TrustSocietyName { get; set; }
        public string? CollegeName { get; set; }
        public string? CollegeCode { get; set; }
        public string? Bill_DisFilename { get; set; }
        public string? Bill_Filename { get; set; }
        public string? MISC { get; set; }
        public string? RegNo { get; set; }
        public string? KNo { get; set; }
        public string? RegDate { get; set; }
        public string? ManageRegOffice { get; set; }
        public int RegOfficeStateID { get; set; }
        public int RegOfficeDistrictID { get; set; }
        public string? RegFileName { get; set; }
        public string? RegDisFileName { get; set; }
        public string? LastElectionDate { get; set; }
        public string? LastElectionValidUpTo { get; set; }
        public string? MemberIdProofName { get; set; }
        public string? MemberIdDisProofName { get; set; }
        public int OwnerShipID { get; set; }
        public int IsRented { get; set; }   
        public string? AgreementLeaseDate { get; set; }
        public string? ValidUpToLeaseDate { get; set; }
        public string? InstituteRegOffice { get; set; }
        public int InstituteStateID { get; set; }
        public int RegDistrictID { get; set; }
        public int InstituteDistrictID { get; set; }
        public int RegStateID { get; set; }   
        public string? AgreementFileName { get; set; }
        public string? AgreementDisFileName { get; set; }
        public int IsOwnRented { get; set; }
        public string? PlotHouseBuildingNo { get; set; }
        public string? StreetRoadLane { get; set; }
        public string? AreaLocalitySector { get; set; }
        public string? LandMark { get; set; }
        public int InstituteDivisionID { get; set; }
        public int InstituteSubDivisionID { get; set; }
        public int PropDistrictID { get; set; }
        public int PropTehsilID { get; set; }
        public int PropUrbanRural { get; set; }
        public int AdministrativeBodyId { get; set; }
        public int VillageID{ get; set; }
        public int PanchayatSamiti { get; set; }
        public int GramPanchayatSamiti { get; set; }

        public int CityID { get; set; }
        public string? WardNo { get; set; }
        public string? KhasraKhataNo { get; set; }
        public string? BighaYard { get; set; }
        public string? LatLongFileName { get; set; }
        public string? LatLongDisFileName { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? AlternateEmail { get; set; }
        public string? Website { get; set; }
        public string? ConsumerName { get; set; }
        public int ConnectionType { get; set; }
        public string? SanctionLoad { get; set; }
        public string? ContractDemand { get; set; }
        public int DISCOM { get; set; }
        public string? SubDivOffice { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
     
        public int ModifyBy { get; set; }
        public int Status { get; set; }
      
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public List<ItiAffiliationList>? ItiAffiliationList { get; set; }
        public List<ItiMembersModel>? ItiMembersModel { get; set; }

        public string? CategoryName { get; set; }
        public string? ManagementTypeName { get; set; }
        public string? StateName { get; set; }
        public string? DistrictName { get; set; }   
        public string? OwnerShipName { get; set; }
        public string? RegistrationStateName { get; set; }
        public string? RegistrationDistirctName { get; set; }
        public string? InsDivisionName { get; set; }
        public string? InsDistrictName { get; set; }
        public string? InsTehsil { get; set; }
        public string? InsUrbanName { get; set; }
        public string? InsCityName { get; set; }
        public string? AdministrativeBodyName { get; set; }


    }



    public class ItiAffiliationList
        {
        public int AffiliationID { get; set; }
        public int CollegeID { get; set; }
        public string? OrderNo { get; set; }
        public string? SerialNo { get; set; }
        public string? PageNo { get; set; }
        public string? OrderDate { get; set; }
        public string? EffectFrom { get; set; }
        public string? FileName { get; set; }
        public string? Dis_Filename { get; set; }
        

    }

    public class ItiMembersModel
    {
        public int MemberId { get; set; }
        public int CollegeID { get; set; }
        public int PostID { get; set; }
        public string? MemberName { get; set; }
        public string? PostName { get; set; }
        public string? ContactNo { get; set; }
        public string? IDFileName { get; set; }
        public string? IDdis_Filename { get; set; }
     
    }

    public class ItiVerificationModel
    {
        public int InstituteID { get; set; }
        public int Status { get; set; }
        public int UserID { get; set ; }
        public string? Remarks { get; set ; }
    }


}
