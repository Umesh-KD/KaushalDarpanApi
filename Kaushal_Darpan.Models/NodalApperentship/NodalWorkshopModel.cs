using Kaushal_Darpan.Models.ITIPapperSetter;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.NodalApperentship
{
    public class ITIApprenticeshipWorkshop
    {
        public int ID { get; set; }
        public int QuaterID { get; set; } = 0;
        public int DistrictID { get; set; } = 0;
        public string ParticipateSsoid { get; set; } = string.Empty;
        public string ParticipateName { get; set; } = string.Empty;
        public DateTime? WorkshopeDate { get; set; }
        public string WorkshopDetail { get; set; } = string.Empty;
        public string? BeforeEstablishmentNo { get; set; }
        public string? BeforeEstablishmentSeat { get; set; }
        public string? BeforeStudentCount { get; set; }
        public string? AfterEstablishmentNo { get; set; }
        public string? AfterEstablishmentSeat { get; set; }
        public string? AfterStudentCount { get; set; }
        public string? RegisterStudentPdf { get; set; }
        public string? DisRegisterStudentPdf { get; set; }
        public string? DisWorkshopPdf { get; set; }
        public string? WorkshopPdf { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }


        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; } = 0;
        public string? QuaterIncreaseEstablishment { get; set; }
        public string? QuaterIncreaseSeat { get; set; }
        public string? QuaterIncreaseStudent { get; set; }
        public string? Remarks { get; set; }

        public List<ITIAAA_SSODetailsModel>? ApprenticeshipWorkshopMembersList { get; set; }
    }

    public class ITIPMNAM_MelaReportBeforeAfterModal
    {
        public int EstablishmentsRegisterNoBefore { get; set; }
        public int NumberofSeatBefore { get; set; }
        public int NumberofEmployedStudentBefore { get; set; }
        public int EstablishmentsRegisterNoAfter { get; set; }
        public int NumberofSeatAfter { get; set; }
        public int NumberofEmployedStudentAfter { get; set; }

        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Createdby { get; set; }
        public int PKID { get; set; }
        public string? BeforeDate { get; set; }
        public string? AfterDate { get; set; }

    }

    public class ITIPMNAM_Report_SearchModal
    {
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Createdby { get; set; }
        public int PKID { get; set; }
    }

    public class ITIPMNAMAppApprenticeshipReportEntity
    {
        public int ID { get; set; }
        public string? PoliticalEstablishmentspartNo { get; set; }
        public string? PrivateEstablishmentspartNo { get; set; }
        public string? PoliticalEstablishmentscontactedNo { get; set; }
        public string? PrivateEstablishmentscontactedNo { get; set; }
        public string? CandidatespresentMaleNo { get; set; }
        public string? CandidatespresentFemaleNo { get; set; }
        public string? CandidatessselectedMaleNo { get; set; }
        public string? CandidatessselectedFemaleNo { get; set; }
        public int UserId { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Createdby { get; set; }
        public int IsActive { get; set; }
        public int InstituteID { get; set; }


    }


    public class ITI_workshopProgressReportSaveModel
    {
      public List<workshopProgressRPTList> WrokshopListModel { get; set; }
    }


    public class workshopProgressRPTList
    {
        public int PKID { get; set; }
        public DateTime? _workshopDate { get; set; }
        public int? _OrganisedDistrictID { get; set; }
        public string? _SelectedDistrictListIDs { get; set; }
        public string _establishmentName { get; set; } = "";
        public string _establishmentAddress { get; set; } = "";
        public string _representativeName { get; set; } = "";
        public string _representativedesignation { get; set; } = "";
        public string _representativeMobile { get; set; } = "";
        public string _Remars { get; set; } = "";
        public int _EndTermID { get; set; }
        public int _DepartmentID { get; set; }
        public int _RoleID { get; set; }
        public int _Createdby { get; set; }

        public int IsActive { get; set; } = 0;

        public string establishmentNameAddress { get; set; } = "";
        public string _ParticipatedDistrictListname { get; set; } = "";
        public string _representativeNameAddressMobileno { get; set; } = "";
        public string _ORG_districtName { get; set; } = "";


    }
    public class ApprenticeshipEntryDto
    {
        public string? Nameofinstitute { get; set; }
        public DateTime? Dateofregistration { get; set; }
        public List<string>? BusinessName { get; set; }
        public int NumberofTrainees { get; set; }
        public string? Numberofapprentices { get; set; }
        public string? Remarks { get; set; }
        public int UserId { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Createdby { get; set; }
        public int PKID { get; set; }
        
        public string? NumberOfRegistrationDoc { get; set; }
    }

    public class ApprenticeshipEntriesList
    {
        public List<ApprenticeshipEntryDto> ApprenticeshipEntries { get; set; } = new();
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Createdby { get; set; }
        public int PKID { get; set; }
    }

    public class  WorkshopProgressRPTSearchModal
    {
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Createdby { get; set; }
        public int PKID { get; set; }
        public int SearchDistrictID { get; set; }
    }

    public class ApprenticeshipRegistrationSearchModal
    {
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Createdby { get; set; }
        public int PKID { get; set; }
        public int InstituteID { get; set; }
        public int UserID { get; set; }
    }

    public class ITIApprenticeshipRegPassOutModel : RequestBaseModel
    {
        public int ID { get; set; } = 0;
        public int InstituteID { get; set; } = 0;
        public int PKID { get; set; } = 0;
        public string RegDate { get; set; } = string.Empty;
        public string RegCount { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Dis_FilePath { get; set; } = string.Empty;
    }


    public class ITIAAA_SSODetailsModel
    {
        public string SSOID { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailID { get; set; }
        public string Name { get; set; }
    }


}