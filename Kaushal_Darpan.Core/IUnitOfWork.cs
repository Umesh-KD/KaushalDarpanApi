
namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IUsersRepository Users { get; }
        IErrorLogRepository ErrorLogs { get; }
        ISSORepository SSORepository { get; }
        ICommonFunctionRepository CommonFunctionRepository { get; }
        IRoleMasterRepository RoleMasterRepository { get; }
        IMenuMasterRepository MenuMasterRepository { get; }
        ICollegeMasterRepository CollegeMasterRepository { get; }
        IITICollegeMasterRepository ITICollegeMasterRepository { get; }
        IStreamMasterRepository StreamMasterRepository { get; }
        IExamMasterRepository ExamMasterRepository { get; }
        IRoomsMasterRepository RoomsMasterRepository { get; }
        IDocumentSettingRepository DocumentSettingRepository { get; }
        ISMSMailRepository SMSMailRepository { get; }
        IDesignationMasterRepository DesignationMasterRepository { get; }
        ILevelMasterRepository LevelMasterRepository { get; }
        IGroupMasterRepository GroupMasterRepository { get; }
        ICenterMasterRepository CenterMasterRepository { get; }
        IITICenterMasterRepository ITICenterMasterRepository { get; }
        ICenterAllocationRepository CenterAllocationRepository { get; }
        ITimeTableRepository TimeTableRepository { get; }
        IPreExamStudentRepository PreExamStudentRepository { get; }
        IPromotedStudentRepository PromotedStudentRepository { get; }
        IStudentCenteredActivitesMasterRepository StudentCenteredActivitesMasterRepository { get; }
        IFlyingSquadRepository FlyingSquadRepository { get; }
        IITIFlyingSquadRepository ITIFlyingSquadRepository { get; }
        ISetPaperRepository SetPaperRepository { get; }
        IRoleMenuRightsRepository RoleMenuRightsRepository { get; }        
        IUserMasterRepository UserMasterRepository { get; }
        ICopyCheckerDashboardRepository CopyCheckerDashboardRepository { get; }

        IUserMenuRightsRepository UserMenuRightsRepository { get; }
        //ISSORepository SSO{ get; }
        IPaperMasterRepository PaperMasterRepository { get; }
        IAbcIdStudentDetailsMasterRepository AbcIdStudentDetailsMasterRepository { get; }
        ICommonSubjectMasterRepository CommonSubjectMasterRepository { get; }
        ISubjectCategoryRepository SubjectCategoryRepository { get; }
        ISubjectMasterRepository SubjectMasterRepository { get; }

        IStudentJanaadharRepository StudentJanaadharRepository { get; }
        ICollegeJanaadharRepository CollegeJanaadharRepository { get; }
        ICenterCreationRepository CenterCreationRepository { get; }
        ICreateTpoRepository CreateTpoRepository { get; }
        IPlacementStudentRepository PlacementStudentRepository { get; }
        ICampusPostMasterRepository CampusPostMasterRepository { get; }
        IPlacementVerifiedStudentTPORepository PlacementVerifiedStudentTPORepository { get; }
        IPlacementDashboardRepository PlacementDashboardRepository { get; }
        IPlacementReportRepository PlacementReportRepository { get; }
        IPlacementShortListStudentRepository PlacementShortListStudentRepository { get; }
        IPlacementSelectedStudentRepository PlacementSelectedStudentRepository { get; }

        ICampusDetailsWebRepository CampusDetailsWebRepository { get; }
        IViewPlacedStudentsRepository ViewPlacedStudentsRepository { get; }
        IHrMasterRepository HrMasterRepository { get; }
        ICompanyMasterRepository CompanyMasterRepository { get; }
        IStaffMasterRepository StaffMasterRepository { get; }
        IAssignRoleRightsRepository AssignRoleRightsRepository { get; }
        IStaffDashboardRepository StaffDashboardRepository { get; }
        IAdminDashboardRepository AdminDashboardRepository { get; }

        //IssueTracker
        IAdminDashboardIssueTrackerRepository AdminDashboardIssueTrackerRepository { get; }
        IInvigilatorAppointmentMasterRepository InvigilatorAppointmentMasterRepository { get; }
        ISetExamAttendanceRepository SetExamAttendanceRepository { get; }
        IStudentRepository StudentRepository { get; }
        IDTEApplicationDashboardRepository DTEApplicationDashboardRepository { get; }
        IExaminersRepository ExaminersRepository { get; }
        IMarksheetDownloadRepository MarksheetDownloadRepository { get; }
        IRevaluationRepository RevaluationRepository { get; }
        IAppointExaminerRepository AppointExaminerRepository { get; }
        IReportRepository ReportRepository { get; }
        IResultRepository ResultRepository { get; }
        IGenerateEnrollRepository GenerateEnrollRepository { get; }
        ITheoryMarksRepository TheoryMarksRepository { get; }
        IInternalPracticalStudentRepository InternalPracticalStudentRepository { get; }
        IGenerateRollRepository GenerateRollRepository { get; }
        IITIGenerateRollRepository ITIGenerateRollRepository { get; }
        IStudentDetailUpdateRepository StudentDetailUpdateRepository { get; }
        IDateConfigurationRepository DateConfigurationRepository { get; }
        IGenerateAdmitCardRepository GenerateAdmitCardRepository { get; }
        IBterApplicationRepository BterApplicationRepository { get; }
        IHiringRoleMasterRepository HiringRoleMasterRepository { get; }
        IGroupCodeAllocationRepository GroupCodeAllocationRepository { get; }
        IItiApplicationFormRepository ItiApplicationFormRepository { get; }
        IITIMasterRepository ITIMasterRepository { get; }
        IStudentJanAadharDetailRepository StudentJanAadharDetailRepository { get; }
        IITISeatIntakeMasterRepository ITISeatIntakeMasterRepository { get; }

        ITSPAreaMasterRepository TSPAreaMasterRepository { get; }
        IStudentsJoiningStatusMarksRepository StudentsJoiningStatusMarksRepository { get; }
        


        IITIAdminDashboardRepository ITIAdminDashboardRepository { get; }

        
        IITIFeeRepository ITIFeeRepository { get; }
        ICitizenSuggestionRepository CitizenSuggestionRepository { get; }

        IStudentVerificationRepository StudentVerificationRepository { get; }
        IVerifierMasterRepository VerifierMasterRepository { get; }
        IAssignApplicationMasterRepository AssignApplicationMasterRepository { get; }
        IApplicationStatusRepository ApplicationStatusRepository { get; }
        IITISeatsDistributionsMasterRepository ITISeatsDistributionsMasterRepository { get; }

        IBTERSeatsDistributionsMasterRepository BTERSeatsDistributionsMasterRepository { get; }

        ICertificateRepository CertificateRepository { get; }
        IMarksheetIssueDateRepository MarksheetIssueDateRepository { get; }

        IReportFeesTransactionRepository ReportFeesTransactionRepository { get; }
        IITIReportFeesTransactionRepository ITIReportFeesTransactionRepository { get; }
        IMasterConfigurationRepository MasterConfigurationRepository { get; }

        IAdminUserRepository AdminUserRepository { get; }
        IITIAdminUserRepository ITIAdminUserRepository { get; }

        IReservationRosterMasterRepository ReservationRosterRepository { get; }
        IItiMeritMasterRepository ItiMeritMasterRepository { get; }
        IBterMeritMasterRepository BterMeritMasterRepository { get; }
        IItiExaminerRepository ItiExaminerRepository { get; }


        IITIExaminationRepository ITIExaminationRepository { get; }
        INodalRepository iNodalRepository { get; }
        IITIGenerateEnrollRepository ITIGenerateEnrollRepository { get; }
        IITIPracticalAssesmentRepository ITIPracticalAssesmentRepository { get; }

        IUpwardMovementRepository UpwardMovementRepository { get; }
         IITICenterAllocationRepository ITICenterAllocationRepository { get; }

        IITIIMCAllocationRepository ITIIMCAllocationRepository { get; }
        IBTERIIMCAllocationRepository BTERIMCAllocationRepository { get; }
        IExamShiftMasterRepository ExamShiftMasterRepository { get; }
        IITIAllotmentRepository ITIAllotmentRepository { get; }

        IBTERAllotmentRepository BTERAllotmentRepository { get; }

        IJanaadharRepository JanaadharRepository { get; }
        IItiStudentCenterActivityRepository ItiStudentCenterActivityRepository { get; }
        IItiTheoryMarksRepository ItiTheoryMarksRepository { get; }

        I_ItemCategoryMasterRepository i_ItemCategoryMasterRepository { get; }
        IEquipmentsMasterRepository iEquipmentsMasterRepository { get; }
        I_ItemsMasterRepository i_ItemsMasterRepository { get; }
        IIssuedItemsRepository iIssuedItemsRepository { get; }
        ITradeEquipmentsMappingRepository iTradeEquipmentsMappingRepository { get; }
        IInternalSlidingRepository InternalSlidingRepository { get; }
        IItiDocumentScrutinyRepository ItiDocumentScrutinyRepository { get; }
        IITISeatMatrixRepository ITISeatMatrixRepository { get; }

        IITITimeTableRepository ITITimeTableRepository { get; }
        IBTERInternalSlidingRepository BTERInternalSlidingRepository { get; }
        IBterStudentJoinStatusRepository BterStudentJoinStatusRepository { get; }
        IITICollegeAdmissionRepository i_ITICollegeAdmissionRepository { get; }
        I_ItemUnitMasterRepository i_ItemUnitMasterRepository { get; }
        IITIInventoryRepository i_ITIInventoryRepository { get; }
        IApplyBridgeCourseRepository ApplyBridgeCourseRepository { get; }


        IHostelManagementRepository HostelManagementRepository { get; }
        IStudentRequestsRepository iStudentRequestsRepository { get; }
        IHostelRoomDetailsRepository iHostelRoomDetailsRepository { get; }
        IBTERAllotmentStatusRepository iBTERAllotmentStatusRepository { get; }

        IDTEItemsMasterRepository iDTEItemsMasterRepository { get; }
        IDTEItemCategoryMasterRepository iDTEItemCategoryMasterRepository { get; }
        IDTEEquipmentsMasterRepository iDTEEquipmentsMasterRepository { get; }
        IDTEIssuedItemsRepository iDTEIssuedItemsRepository { get; }
        IDTETradeEquipmentsMappingRepository iDTETradeEquipmentsMappingRepository { get; }
        IDTEItemUnitMasterRepository iDTEItemUnitMasterRepository { get; }
        IDTEInventoryDashboardRepository iDTEInventoryDashboardRepository { get; }


        ICollegeAdmissionSeatAllotmentRepository CollegeAdmissionSeatAllotmentRepository { get; }
        IIndustryInstitutePartnershipMasterRepository iIndustryInstitutePartnershipRepository { get; }
        IPaymentServiceRepository PaymentServiceRepository { get; }
        IGroupCodeAllocationRevalRepository GroupCodeAllocationRevalRepository { get; }

        ICorrectMeritRepository iCorrectMeritRepository { get; }
        IAllotmentConfigurationRepository iAllotmentConfigurationRepository { get; }

        I_ITICompanyMasterRepository i_ITICompanyMasterRepository { get; }
        I_ItiHrMasterRepository i_ItiHrMasterRepository { get; }
        I_ItiCampusPostMasterRepository i_ItiCampusPostMasterRepository { get; }

        IStudentEnrollmentRepository StudentEnrollmentRepository { get; }

        IGrivienceRepository iGrivienceRepository { get; }
        IScholarshipRepository ScholarshipRepository { get; }
        IGuestRoomManagementRepository GuestRoomManagementRepository { get; }
        IPaperSetterRepository PaperSetterRepository { get; }
        ISsoidUpdateRepository SsoidUpdateRepository { get; }
        ICenterObserverRepository CenterObserverRepository { get; }
        I_ITICenterObserverRepository ITICenterObserverRepository { get; }
        ISecretaryJDDashboardRepository SecretaryJDDashboardRepository { get; }
        IDetainedStudentsRepository DetainedStudentsRepository { get; }
        ICheckListRepository CheckListRepository { get; }
        ILeaveMasterRepository LeaveMasterRepository { get; }

        IDispatchRepository DispatchRepository { get; }
        IPolytechnicReportRepository PolytechnicReportRepository { get; }
        IExaminerReportRepository ExaminerReportRepository { get; }
        IBoard_UniversityMasterRepository Board_UniversityMasterRepository { get; }
        IITICollegeProfileRepository ITICollegeProfileRepository { get; }

        IRenumerationExaminerRepository RenumerationExaminerRepository { get; }
        IRenumerationJDRepository RenumerationJDRepository { get; }
        IRenumerationAccounts RenumerationAccounts { get; }
        I_ITI_InspectionRepository ITI_InspectionRepository { get; }
        IITIPrivateEstablishRepository ITIPrivateEstablishRepository { get; }
        IITIGovtEMStaffMasterRepository ITIGovtEMStaffMasterRepository { get; }
        IAadharEsignRepository AadharEsignRepository { get; }
        IUserActivityLoggerRepository UserActivityLoggerRepository { get; }

        IBTERMasterRepository BTERMasterRepository { get; }
        IWebsiteSettingsRepository WebsiteSettingsRepository { get; }
        IITIPapperSetterRepository ITIPapperSetterRepository { get; }
        IITIPracticalExaminerRepository ITIPracticalExaminerRepository { get; }

        IUsersRequestRepository UsersRequest { get; }

        IITIInvigilatorRepository ITIInvigilatorRepository { get; }

        //ISMSSchedulerRepository SMSSchedulerRepository { get; }
        ISMSSchedulerRepository SMSSchedulerRepository { get; }

        IITIDispatchRepository ITIDispatchRepository { get; }
        IGuestHouseRepository IGuestHouseRepository { get; }

        IITIRelievingExamRepository ITIRelievingExamRepository { get; }

        IMasterConfigurationBterRepository MasterConfigurationBterRepository { get; }

        IITINodalOfficerExminerReportRepository ITINodalOfficerExminerReport { get; }
        ITestRepository TestRepository { get; }

        IITINCVTRepository ITINCVTRepository { get; }
        I_ITIFlyingSquadManageRepository ITIFlyingSquadManageRepository { get; }
        IBTER_EstablishManagementRepository BTER_EstablishManagementRepository { get; }
        I_ITIResultRepository ITIResultRepository { get; }

        I_ITICenterSuperitendentExamReportRepository ITICenterSuperitendentExamReportRepository { get; }
        I_ITIStudentEnrollmentRepository ITIStudentEnrollmentRepository { get; }
        IEnrollmentCancelationRepository EnrollmentCancelationRepository { get; }

        I_ITICollegeMarksheetDownloadRepository ITICollegeMarksheetDownloadRepository { get; }
        IProjectMasterRepository ProjectMasterRepository { get; }
        IIssueTrackerRepository IssueTrackerRepository { get; }
        IITINodalReportRepository ITINodalReportRepository { get; }

        I_ITI_InstructorRepository ITI_InstructorRepository { get; }

        IITIApprenticeshipRepository ITI_ApprenticeshipRepository { get; }
        ITheoryMarksRevalRepository TheoryMarksRevalRepository { get; }
        IBudgetHeadManagementRepository BudgetHeadManagementRepository { get; }



        IRevalDispatchRepository RevalDispatchRepository { get; }

        I_ITI_BGTHeadmasterRepository ITI_BGTHeadmasterRepository { get; }
        I_ITICampusDetailsWebRepository ITICampusDetailsWebRepository { get; }
        I_ITIViewPlacedStudentsRepository ITIViewPlacedStudentsRepository { get; }
        I_ITIPlacementStudentRepository ITIPlacementStudentRepository { get; }
        I_ITIPlacementShortListStudentRepository ITIPlacementShortListStudentRepository { get; }

        I_ITIPlacementSelectedStudentRepository ITIPlacementSelectedStudentRepository { get; }

        I_ITI_IIP_TrimashQuaterlyReportRepository ITI_IIP_TrimashQuaterlyReportRepository { get; }

        IITIIIPManageRepository ITIIIPManageRepository { get; }
        void SaveChanges();
    }
}
