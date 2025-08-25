using Kaushal_Darpan.Core.Interfaces;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _dbContext;
        private bool disposedValue;

        public UnitOfWork()
        {
            _dbContext = new DBContext();
        }

        #region UOW
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            // TODO: dispose managed state (managed objects)
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            //GC.SuppressFinalize(this);
        }
        #endregion

        #region Add Repository Property 
        private IProductRepository _products;
        public IProductRepository Products
        {
            get
            {
                return _products ??= new ProductRepository(_dbContext);
            }
        }

        private IUsersRepository _users;
        public IUsersRepository Users
        {
            get
            {
                return _users ??= new UsersRepository(_dbContext);
            }
        }

        private IErrorLogRepository _errorLogs;
        public IErrorLogRepository ErrorLogs
        {
            get
            {
                return _errorLogs ??= new ErrorLogRepository(_dbContext);
            }
        }

        private ISSORepository _sSO;
        public ISSORepository SSORepository
        {
            get
            {
                return _sSO ??= new SSORepository(_dbContext);
            }
        }

        private ICommonFunctionRepository _commonFunctionRepository;
        public ICommonFunctionRepository CommonFunctionRepository
        {
            get
            {
                return _commonFunctionRepository ??= new CommonFunctionRepository(_dbContext);
            }
        }

        private IRoleMasterRepository _roleMasterRepository;
        public IRoleMasterRepository RoleMasterRepository
        {
            get
            {
                return _roleMasterRepository ??= new RoleMasterRepository(_dbContext);
            }
        }
        private IMenuMasterRepository _menuMasterRepository;
        public IMenuMasterRepository MenuMasterRepository
        {
            get
            {
                return _menuMasterRepository ??= new MenuMasterRepository(_dbContext);
            }
        }

        private ISMSMailRepository _sMSMailRepository;
        public ISMSMailRepository SMSMailRepository
        {
            get
            {
                return _sMSMailRepository ??= new SMSMailRepository(_dbContext);
            }
        }
        private ICollegeMasterRepository _collegeMasterRepository;
        public ICollegeMasterRepository CollegeMasterRepository
        {
            get
            {
                return _collegeMasterRepository ??= new CollegeMasterRepository(_dbContext);
            }
        }
        private IITICollegeMasterRepository _iticollegeMasterRepository;
        public IITICollegeMasterRepository ITICollegeMasterRepository
        {
            get
            {
                return _iticollegeMasterRepository ??= new ITICollegeMasterRepository(_dbContext);
            }
        }
        private IStreamMasterRepository _streamMasterRepository;
        public IStreamMasterRepository StreamMasterRepository
        {
            get
            {
                return _streamMasterRepository ??= new StreamMasterRepository(_dbContext);
            }



        }
        private IExamMasterRepository _examMasterRepository;
        public IExamMasterRepository ExamMasterRepository
        {
            get
            {
                return _examMasterRepository ??= new ExamMasterRepository(_dbContext);
            }



        }
        private IRoomsMasterRepository _roomsMasterRepository;
        public IRoomsMasterRepository RoomsMasterRepository
        {
            get
            {
                return _roomsMasterRepository ??= new RoomsMasterRepository(_dbContext);
            }



        }

        private IDesignationMasterRepository _designationMasterRepository;
        public IDesignationMasterRepository DesignationMasterRepository
        {
            get
            {
                return _designationMasterRepository ??= new DesignationMasterRepository(_dbContext);
            }


        }
        private IStudentCenteredActivitesMasterRepository _studentCenteredActivitesMasterRepository;
        public IStudentCenteredActivitesMasterRepository StudentCenteredActivitesMasterRepository
        {
            get
            {
                return _studentCenteredActivitesMasterRepository ??= new StudentCenteredActivitesMasterRepository(_dbContext);
            }


        }
        private ILevelMasterRepository _levelMasterRepository;
        public ILevelMasterRepository LevelMasterRepository
        {
            get
            {
                return _levelMasterRepository ??= new LevelMasterRepository(_dbContext);
            }


        }
        private IGroupMasterRepository _groupMasterRepository;
        public IGroupMasterRepository GroupMasterRepository
        {
            get
            {
                return _groupMasterRepository ??= new GroupMasterRepository(_dbContext);
            }


        }
        private ICenterMasterRepository _centerMasterRepository;
        public ICenterMasterRepository CenterMasterRepository
        {
            get
            {
                return _centerMasterRepository ??= new CenterMasterRepository(_dbContext);
            }

        }

        private IITICenterMasterRepository _ITIcenterMasterRepository;
        public IITICenterMasterRepository ITICenterMasterRepository
        {
            get
            {
                return _ITIcenterMasterRepository ??= new ITICenterMasterRepository(_dbContext);
            }

        }

        private ICenterAllocationRepository _centerAllocationRepository;
        public ICenterAllocationRepository CenterAllocationRepository
        {
            get
            {
                return _centerAllocationRepository ??= new CenterAllocationRepository(_dbContext);
            }

        }

        private ITimeTableRepository _timeTableRepository;
        public ITimeTableRepository TimeTableRepository
        {
            get
            {
                return _timeTableRepository ??= new TimeTableRepository(_dbContext);
            }

        }

        private IPreExamStudentRepository _preExamStudentRepository;
        public IPreExamStudentRepository PreExamStudentRepository
        {
            get
            {
                return _preExamStudentRepository ??= new PreExamStudentRepository(_dbContext);
            }

        }
        private IPromotedStudentRepository _promotedStudentRepository;
        public IPromotedStudentRepository PromotedStudentRepository
        {
            get
            {
                return _promotedStudentRepository ??= new PromotedStudentRepository(_dbContext);
            }

        }
        private ICopyCheckerDashboardRepository _copyCheckerDashboardRepository;
        public ICopyCheckerDashboardRepository CopyCheckerDashboardRepository
        {
            get
            {
                return _copyCheckerDashboardRepository ??= new CopyCheckerDashboardRepository(_dbContext);
            }

        }

        private IPaperMasterRepository _paperMasterRepository;
        public IPaperMasterRepository PaperMasterRepository
        {
            get
            {
                return _paperMasterRepository ??= new PaperMasterRepository(_dbContext);
            }

        }

        private IAbcIdStudentDetailsMasterRepository _abcIdStudentDetailsMasterRepository;
        public IAbcIdStudentDetailsMasterRepository AbcIdStudentDetailsMasterRepository
        {
            get
            {
                return _abcIdStudentDetailsMasterRepository ??= new AbcIdStudentDetailsMasterRepository(_dbContext);
            }

        }

        private ICommonSubjectMasterRepository _CommonSubjectMasterRepository;
        public ICommonSubjectMasterRepository CommonSubjectMasterRepository
        {
            get
            {
                return _CommonSubjectMasterRepository ??= new CommonSubjectMasterRepository(_dbContext);
            }

        }

        private ISubjectCategoryRepository _subjectCategoryRepository;
        public ISubjectCategoryRepository SubjectCategoryRepository
        {
            get
            {
                return _subjectCategoryRepository ??= new SubjectCategoryRepository(_dbContext);
            }
        }

        private ISubjectMasterRepository _subjectMasterRepository;
        public ISubjectMasterRepository SubjectMasterRepository
        {
            get
            {
                return _subjectMasterRepository ??= new SubjectMasterRepository(_dbContext);
            }
        }

        private IStudentJanaadharRepository _studentJanaadharRepository;
        public IStudentJanaadharRepository StudentJanaadharRepository
        {
            get
            {
                return _studentJanaadharRepository ??= new StudentJanaadharRepository(_dbContext);
            }
        }


        private ICollegeJanaadharRepository _collegeJanaadharRepository;
        public ICollegeJanaadharRepository CollegeJanaadharRepository
        {
            get
            {
                return _collegeJanaadharRepository ??= new CollegeJanaadharRepository(_dbContext);
            }
        }

        private ICenterCreationRepository _centerCreationRepository;
        public ICenterCreationRepository CenterCreationRepository
        {
            get
            {
                return _centerCreationRepository ??= new CenterCreationRepository(_dbContext);
            }
        }

        private ICreateTpoRepository _createTpoRepository;
        public ICreateTpoRepository CreateTpoRepository
        {
            get
            {
                return _createTpoRepository ??= new CreateTpoRepository(_dbContext);
            }
        }

        private IPlacementStudentRepository _placementStudentRepository;
        public IPlacementStudentRepository PlacementStudentRepository
        {
            get
            {
                return _placementStudentRepository ??= new PlacementStudentRepository(_dbContext);
            }
        }
        private ICampusPostMasterRepository _campusPostMasterRepository;
        public ICampusPostMasterRepository CampusPostMasterRepository
        {
            get
            {
                return _campusPostMasterRepository ??= new CampusPostMasterRepository(_dbContext);
            }
        }

        private IPlacementVerifiedStudentTPORepository _placementVerifiedStudentTPORepository;
        public IPlacementVerifiedStudentTPORepository PlacementVerifiedStudentTPORepository
        {
            get
            {
                return _placementVerifiedStudentTPORepository ??= new PlacementVerifiedStudentTPORepository(_dbContext);
            }
        }

        private IRoleMenuRightsRepository _RoleMenuRightsRepository;
        public IRoleMenuRightsRepository RoleMenuRightsRepository
        {
            get
            {
                return _RoleMenuRightsRepository ??= new RoleMenuRightsRepository(_dbContext);
            }

        }

        private ISetPaperRepository _SetPaperRepository;
        public ISetPaperRepository SetPaperRepository
        {
            get
            {
                return _SetPaperRepository ??= new SetPaperRepository(_dbContext);
            }

        }



        private IUserMasterRepository _userMasterRepository;
        public IUserMasterRepository UserMasterRepository
        {
            get
            {
                return _userMasterRepository ??= new UserMasterRepository(_dbContext);
            }
        }

        private IUserMenuRightsRepository _userMenuRightsRepository;
        public IUserMenuRightsRepository UserMenuRightsRepository
        {
            get
            {
                return _userMenuRightsRepository ??= new UserMenuRightsRepository(_dbContext);
            }
        }
        private IPlacementDashboardRepository _placementDashboardRepository;
        public IPlacementDashboardRepository PlacementDashboardRepository
        {
            get
            {
                return _placementDashboardRepository ??= new PlacementDashboardRepository(_dbContext);
            }
        }
        private IPlacementReportRepository _placementReportRepository;
        public IPlacementReportRepository PlacementReportRepository
        {
            get
            {
                return _placementReportRepository ??= new PlacementReportRepository(_dbContext);
            }
        }

        private IPlacementShortListStudentRepository _placementShortListStudentRepository;
        public IPlacementShortListStudentRepository PlacementShortListStudentRepository
        {
            get
            {
                return _placementShortListStudentRepository ??= new PlacementShortListStudentRepository(_dbContext);
            }
        }


        private IPlacementSelectedStudentRepository _placementSelectedStudentRepository;
        public IPlacementSelectedStudentRepository PlacementSelectedStudentRepository
        {
            get
            {
                return _placementSelectedStudentRepository ??= new PlacementSelectedStudentRepository(_dbContext);
            }
        }
        private ICampusDetailsWebRepository _campusDetailsWebRepository;
        public ICampusDetailsWebRepository CampusDetailsWebRepository
        {
            get
            {
                return _campusDetailsWebRepository ??= new CampusDetailsWebRepository(_dbContext);
            }
        }




        private IViewPlacedStudentsRepository _viewPlacedStudentsRepository;
        public IViewPlacedStudentsRepository ViewPlacedStudentsRepository
        {
            get
            {
                return _viewPlacedStudentsRepository ??= new ViewPlacedStudentsRepository(_dbContext);
            }
        }
        private IHrMasterRepository _hrMasterRepository;
        public IHrMasterRepository HrMasterRepository
        {
            get
            {
                return _hrMasterRepository ??= new HrMasterRepository(_dbContext);
            }
        }
        private ICompanyMasterRepository _companyMasterRepository;
        public ICompanyMasterRepository CompanyMasterRepository
        {
            get
            {
                return _companyMasterRepository ??= new CompanyMasterRepository(_dbContext);
            }
        }
        private IStaffMasterRepository _staffMasterRepository;
        public IStaffMasterRepository StaffMasterRepository
        {
            get
            {
                return _staffMasterRepository ??= new StaffMasterRepository(_dbContext);
            }
        }
        private IStaffDashboardRepository _staffDashboardRepository;
        public IStaffDashboardRepository StaffDashboardRepository
        {
            get
            {
                return _staffDashboardRepository ??= new StaffDashboardRepository(_dbContext);
            }
        }
        private IAdminDashboardRepository _adminDashboardRepository;
        public IAdminDashboardRepository AdminDashboardRepository
        {
            get
            {
                return _adminDashboardRepository ??= new AdminDashboardRepository(_dbContext);
            }
        }


        private IInvigilatorAppointmentMasterRepository _invigilatorAppointmentMasterRepository;
        public IInvigilatorAppointmentMasterRepository InvigilatorAppointmentMasterRepository
        {
            get
            {
                return _invigilatorAppointmentMasterRepository ??= new InvigilatorAppointmentMasterRepository(_dbContext);
            }
        }

        private ISetExamAttendanceRepository _setExamAttendanceRepository;
        public ISetExamAttendanceRepository SetExamAttendanceRepository
        {
            get
            {
                return _setExamAttendanceRepository ??= new SetExamAttendanceRepository(_dbContext);
            }
        }

        private IStudentRepository _studentRepository;
        public IStudentRepository StudentRepository
        {
            get
            {
                return _studentRepository ??= new StudentRepository(_dbContext);
            }
        }

        private IFlyingSquadRepository _flyingSquadRepository;
        public IFlyingSquadRepository FlyingSquadRepository
        {
            get
            {
                return _flyingSquadRepository ??= new FlyingSquadRepository(_dbContext);
            }
        }

        private IITIFlyingSquadRepository _itiFlyingSquadRepository;
        public IITIFlyingSquadRepository ITIFlyingSquadRepository
        {
            get
            {
                return _itiFlyingSquadRepository ??= new ITIFlyingSquadRepository(_dbContext);
            }
        }

        private IDTEApplicationDashboardRepository _dTEApplicationDashboardRepository;
        public IDTEApplicationDashboardRepository DTEApplicationDashboardRepository
        {
            get
            {
                return _dTEApplicationDashboardRepository ??= new DTEApplicationDashboardRepository(_dbContext);
            }
        }


        private IAssignRoleRightsRepository _assignRoleRightsRepository;
        public IAssignRoleRightsRepository AssignRoleRightsRepository
        {
            get
            {
                return _assignRoleRightsRepository ??= new AssignRoleRightsRepository(_dbContext);
            }
        }

        private IExaminersRepository _examinersRepository;
        public IExaminersRepository ExaminersRepository
        {
            get
            {
                return _examinersRepository ??= new ExaminersRepository(_dbContext);
            }
        }
        private IAppointExaminerRepository _appointExaminerRepository;
        public IAppointExaminerRepository AppointExaminerRepository
        {
            get
            {
                return _appointExaminerRepository ??= new AppointExaminerRepository(_dbContext);
            }
        }

        private IReportRepository _reportRepository;
        public IReportRepository ReportRepository
        {
            get
            {
                return _reportRepository ??= new ReportRepository(_dbContext);
            }
        }

        private IResultRepository _resultRepository;
        public IResultRepository ResultRepository
        {
            get
            {
                return _resultRepository ??= new ResultRepository(_dbContext);
            }
        }


        private IGenerateEnrollRepository _generateEnrollRepository;
        public IGenerateEnrollRepository GenerateEnrollRepository
        {
            get
            {
                return _generateEnrollRepository ??= new GenerateEnrollRepository(_dbContext);
            }
        }

        private IGenerateRollRepository _generateRollRepository;
        public IGenerateRollRepository GenerateRollRepository
        {
            get
            {
                return _generateRollRepository ??= new GenerateRollRepository(_dbContext);
            }
        }

        private IITIGenerateRollRepository _iTIGenerateRollRepository;
        public IITIGenerateRollRepository ITIGenerateRollRepository
        {
            get
            {
                return _iTIGenerateRollRepository ??= new ITIGenerateRollRepository(_dbContext);
            }
        }


        private ITheoryMarksRepository _theoryMarksRepository;
        public ITheoryMarksRepository TheoryMarksRepository
        {
            get
            {
                return _theoryMarksRepository ??= new TheoryMarksRepository(_dbContext);
            }
        }

        private IGenerateAdmitCardRepository _generateAdmitCardRepository;
        public IGenerateAdmitCardRepository GenerateAdmitCardRepository
        {
            get
            {
                return _generateAdmitCardRepository ??= new GenerateAdmitCardRepository(_dbContext);
            }
        }

        private IInternalPracticalStudentRepository _internalPracticalStudentRepository;
        public IInternalPracticalStudentRepository InternalPracticalStudentRepository
        {
            get
            {
                return _internalPracticalStudentRepository ??= new InternalPracticalStudentRepository(_dbContext);
            }
        }

        private IStudentDetailUpdateRepository _studentDetailUpdateRepository;
        public IStudentDetailUpdateRepository StudentDetailUpdateRepository
        {
            get
            {
                return _studentDetailUpdateRepository ??= new StudentDetailUpdateRepository(_dbContext);
            }
        }

        private IDateConfigurationRepository _dateConfigurationRepository;
        public IDateConfigurationRepository DateConfigurationRepository
        {
            get
            {
                return _dateConfigurationRepository ??= new DateConfigurationRepository(_dbContext);
            }
        }
        private IBterApplicationRepository _bterApplicationRepository;
        public IBterApplicationRepository BterApplicationRepository
        {
            get
            {
                return _bterApplicationRepository ??= new BterApplicationRepository(_dbContext);
            }
        }


        private IHiringRoleMasterRepository _hiringRoleMasterRepository;
        public IHiringRoleMasterRepository HiringRoleMasterRepository
        {
            get
            {
                return _hiringRoleMasterRepository ??= new HiringRoleMasterRepository(_dbContext);
            }
        }

        private IGroupCodeAllocationRepository _groupCodeAllocationRepository;
        public IGroupCodeAllocationRepository GroupCodeAllocationRepository
        {
            get
            {
                return _groupCodeAllocationRepository ??= new GroupCodeAllocationRepository(_dbContext);
            }
        }


        private IGroupCodeAllocationRevalRepository _groupCodeAllocationRevalRepository;
        public IGroupCodeAllocationRevalRepository GroupCodeAllocationRevalRepository
        {
            get
            {
                return _groupCodeAllocationRevalRepository ??= new GroupCodeAllocationRevalRepository(_dbContext);
            }
        }

        private IItiApplicationFormRepository _itiApplicationFormRepository;
        public IItiApplicationFormRepository ItiApplicationFormRepository
        {
            get
            {
                return _itiApplicationFormRepository ??= new ItiApplicationFormRepository(_dbContext);
            }
        }




        private IITIMasterRepository _iitiMasterRepository;
        public IITIMasterRepository ITIMasterRepository
        {
            get
            {
                return _iitiMasterRepository ??= new ITIMasterRepository(_dbContext);
            }
        }


        private IStudentJanAadharDetailRepository _iStudentJanAadharDetailRepository;
        public IStudentJanAadharDetailRepository StudentJanAadharDetailRepository
        {
            get
            {
                return _iStudentJanAadharDetailRepository ??= new StudentJanAadharDetailRepository(_dbContext);
            }
        }

        private IITISeatIntakeMasterRepository _iTISeatIntakeMasterRepository;
        public IITISeatIntakeMasterRepository ITISeatIntakeMasterRepository
        {
            get
            {
                return _iTISeatIntakeMasterRepository ??= new ITISeatIntakeMasterRepository(_dbContext);
            }
        }




        private IITIFeeRepository _iitiFeeRepository;
        public IITIFeeRepository ITIFeeRepository
        {
            get
            {
                return _iitiFeeRepository ??= new ITIFeeRepository(_dbContext);
            }
        }


        private ITSPAreaMasterRepository _itspAreaMasterRepository;
        public ITSPAreaMasterRepository TSPAreaMasterRepository
        {
            get

            {
                return _itspAreaMasterRepository ??= new TspAreaMaster(_dbContext);
            }
        }

        private IITIAdminDashboardRepository _itiadminDashboardRepository;
        public IITIAdminDashboardRepository ITIAdminDashboardRepository
        {
            get
            {
                return _itiadminDashboardRepository ??= new ITIAdminDashboardRepository(_dbContext);
            }
        }

        private ICitizenSuggestionRepository _iCitizenSuggestionRepository;
        public ICitizenSuggestionRepository CitizenSuggestionRepository
        {
            get
            {
                return _iCitizenSuggestionRepository ??= new CitizenSuggestionRepository(_dbContext);
            }
        }


        private IStudentVerificationRepository _iStudentVerificationRepository;
        public IStudentVerificationRepository StudentVerificationRepository
        {
            get
            {
                return _iStudentVerificationRepository ??= new StudentVerificationRepository(_dbContext);
            }
        }

        private IVerifierMasterRepository _verifierMasterRepository;
        public IVerifierMasterRepository VerifierMasterRepository
        {
            get
            {
                return _verifierMasterRepository ??= new VerifierMasterRepository(_dbContext);
            }
        }

        private IAssignApplicationMasterRepository _assignApplicationMasterRepository;
        public IAssignApplicationMasterRepository AssignApplicationMasterRepository
        {
            get
            {
                return _assignApplicationMasterRepository ??= new AssignApplicationMasterRepository(_dbContext);
            }
        }

        private IApplicationStatusRepository _applicationStatusRepository;
        public IApplicationStatusRepository ApplicationStatusRepository
        {
            get
            {
                return _applicationStatusRepository ??= new ApplicationStatusRepository(_dbContext);
            }
        }
        private IITISeatsDistributionsMasterRepository _itiseatsDistributionsMasterRepository;
        public IITISeatsDistributionsMasterRepository ITISeatsDistributionsMasterRepository
        {
            get
            {
                return _itiseatsDistributionsMasterRepository ??= new ITISeatsDistributionsMasterRepository(_dbContext);
            }
        }

        private IBTERSeatsDistributionsMasterRepository _bterseatsDistributionsMasterRepository;
        public IBTERSeatsDistributionsMasterRepository BTERSeatsDistributionsMasterRepository
        {
            get
            {
                return _bterseatsDistributionsMasterRepository ??= new BTERSeatsDistributionsMasterRepository(_dbContext);
            }
        }


        private IMarksheetDownloadRepository _marksheetDownloadRepository;
        public IMarksheetDownloadRepository MarksheetDownloadRepository
        {
            get
            {
                return _marksheetDownloadRepository ??= new MarksheetDownloadRepository(_dbContext);
            }
        }
        private IRevaluationRepository _revaluationRepository;
        public IRevaluationRepository RevaluationRepository
        {
            get
            {
                return _revaluationRepository ??= new RevaluationRepository(_dbContext);
            }
        }
        private IDocumentSettingRepository documentSettingRepository;
        public IDocumentSettingRepository DocumentSettingRepository
        {
            get
            {
                return documentSettingRepository ??= new DocumentSettingRepository(_dbContext);
            }
        }

        private IReportFeesTransactionRepository _reportfeestransactionRepository;
        public IReportFeesTransactionRepository ReportFeesTransactionRepository
        {
            get
            {
                return _reportfeestransactionRepository ??= new ReportFeesTransactionRepository(_dbContext);
            }
        }

        private IITIReportFeesTransactionRepository _itireportfeestransactionRepository;
        public IITIReportFeesTransactionRepository ITIReportFeesTransactionRepository
        {
            get
            {
                return _itireportfeestransactionRepository ??= new ITIReportFeesTransactionRepository(_dbContext);
            }
        }


        private ICertificateRepository _iCertificateRepository;
        public ICertificateRepository CertificateRepository
        {
            get
            {
                return _iCertificateRepository ??= new CertificateRepository(_dbContext);
            }
        }

        private IMarksheetIssueDateRepository _iMarksheetIssueDateRepository;
        public IMarksheetIssueDateRepository MarksheetIssueDateRepository
        {
            get
            {
                return _iMarksheetIssueDateRepository ??= new MarksheetIssueDateRepository(_dbContext);
            }
        }
        private IMasterConfigurationRepository _iMasterConfigurationRepository;
        public IMasterConfigurationRepository MasterConfigurationRepository
        {
            get
            {
                return _iMasterConfigurationRepository ??= new MasterConfigurationRepository(_dbContext);
            }
        }


        private IAdminUserRepository _iAdminUserRepository;
        public IAdminUserRepository AdminUserRepository
        {
            get
            {
                return _iAdminUserRepository ??= new AdminUserRepository(_dbContext);
            }
        }

        private IITIAdminUserRepository _iitiAdminUserRepository;
        public IITIAdminUserRepository ITIAdminUserRepository
        {
            get
            {
                return _iitiAdminUserRepository ??= new ITIAdminUserRepository(_dbContext);
            }
        }

        private IReservationRosterMasterRepository _ireservationRosterMasterRepository;
        public IReservationRosterMasterRepository ReservationRosterRepository
        {
            get
            {
                return _ireservationRosterMasterRepository ??= new ReservationRosterRepository(_dbContext);
            }
        }

        private IItiMeritMasterRepository _itiMeritMasterRepository;
        public IItiMeritMasterRepository ItiMeritMasterRepository
        {
            get
            {
                return _itiMeritMasterRepository ??= new ItiMeritMasterRepository(_dbContext);
            }
        }

        private IBterMeritMasterRepository _bterMeritMasterRepository;
        public IBterMeritMasterRepository BterMeritMasterRepository
        {
            get
            {
                return _bterMeritMasterRepository ??= new BterMeritMasterRepository(_dbContext);
            }
        }

        private IStudentsJoiningStatusMarksRepository _studentsJoiningStatusMarksRepository;
        public IStudentsJoiningStatusMarksRepository StudentsJoiningStatusMarksRepository
        {
            get
            {
                return _studentsJoiningStatusMarksRepository ??= new StudentsJoiningStatusMarks(_dbContext);
            }
        }

        private IITIExaminationRepository _iITIExaminationRepository;
        public IITIExaminationRepository ITIExaminationRepository
        {
            get
            {
                return _iITIExaminationRepository ??= new ITIExaminationRepository(_dbContext);
            }
        }

        private INodalRepository _iNodalRepository;
        public INodalRepository iNodalRepository
        {
            get
            {
                return _iNodalRepository ??= new NodalRepository(_dbContext);
            }
        }

        private IITIGenerateEnrollRepository _ITIGenerateEnrollRepository;
        public IITIGenerateEnrollRepository ITIGenerateEnrollRepository
        {
            get
            {
                return _ITIGenerateEnrollRepository ??= new ITIGenerateEnrollRepository(_dbContext);
            }
        }

        private IUpwardMovementRepository _upwardMovementRepository;
        public IUpwardMovementRepository UpwardMovementRepository
        {
            get
            {
                return _upwardMovementRepository ??= new UpwardMovementRepository(_dbContext);
            }
        }


        private IITIPracticalAssesmentRepository _ITIPracticalAssesmentRepository;
        public IITIPracticalAssesmentRepository ITIPracticalAssesmentRepository
        {
            get
            {
                return _ITIPracticalAssesmentRepository ??= new ITIPracticalAssesmentRepository(_dbContext);
            }
        }


        private IITICenterAllocationRepository _ITICenterAllocationRepository;
        public IITICenterAllocationRepository ITICenterAllocationRepository
        {
            get
            {
                return _ITICenterAllocationRepository ??= new ITICenterAllocationRepository(_dbContext);
            }
        }



        private IITIIMCAllocationRepository _itiIMCAllocationRepository;
        public IITIIMCAllocationRepository ITIIMCAllocationRepository
        {
            get
            {
                return _itiIMCAllocationRepository ??= new ITIIMCAllocation(_dbContext);
            }
        }

        private IBTERIIMCAllocationRepository _bterIMCAllocationRepository;
        public IBTERIIMCAllocationRepository BTERIMCAllocationRepository
        {
            get
            {
                return _bterIMCAllocationRepository ??= new BTERIMCAllocationRepository(_dbContext);
            }
        }

        private IItiExaminerRepository _itiExaminerRepository;
        public IItiExaminerRepository ItiExaminerRepository
        {
            get
            {
                return _itiExaminerRepository ??= new ItiExaminerRepository(_dbContext);
            }

        }

        private IExamShiftMasterRepository _examShiftMasterRepository;
        public IExamShiftMasterRepository ExamShiftMasterRepository
        {
            get
            {
                return _examShiftMasterRepository ??= new ExamShiftMasterRepository(_dbContext);
            }

        }

        private IITIAllotmentRepository _iITIAllotmentRepository;
        public IITIAllotmentRepository ITIAllotmentRepository
        {
            get
            {
                return _iITIAllotmentRepository ??= new ITIAllotmentRepository(_dbContext);
            }

        }


        private IItiStudentCenterActivityRepository _itiStudentCenterActivityRepository;
        public IItiStudentCenterActivityRepository ItiStudentCenterActivityRepository
        {
            get
            {
                return _itiStudentCenterActivityRepository ??= new ItiStudentCenterActivityRepository(_dbContext);
            }

        }
        private IItiTheoryMarksRepository _ItitheoryMarksRepository;
        public IItiTheoryMarksRepository ItiTheoryMarksRepository
        {
            get
            {
                return _ItitheoryMarksRepository ??= new ItiTheoryMarksRepository(_dbContext);
            }

        }



        private I_ItemCategoryMasterRepository _itemCategoryMasterRepository;
        public I_ItemCategoryMasterRepository i_ItemCategoryMasterRepository
        {
            get
            {
                return _itemCategoryMasterRepository ??= new ItemCategoryMasterRepository(_dbContext);
            }
        }

        private IEquipmentsMasterRepository _iEquipmentsMasterRepository;
        public IEquipmentsMasterRepository iEquipmentsMasterRepository
        {
            get
            {
                return _iEquipmentsMasterRepository ??= new EquipmentsMasterRepository(_dbContext);
            }
        }

        private I_ItemsMasterRepository _iItemsMasterRepository;
        public I_ItemsMasterRepository i_ItemsMasterRepository
        {
            get
            {
                return _iItemsMasterRepository ??= new ItemsMasterRepository(_dbContext);
            }
        }

        private IIssuedItemsRepository _iIssuedItemsRepository;
        public IIssuedItemsRepository iIssuedItemsRepository
        {
            get
            {
                return _iIssuedItemsRepository ??= new IssuedItemsRepository(_dbContext);
            }
        }

        private ITradeEquipmentsMappingRepository _iTradeEquipmentsMappingRepository;
        public ITradeEquipmentsMappingRepository iTradeEquipmentsMappingRepository
        {
            get
            {
                return _iTradeEquipmentsMappingRepository ??= new TradeEquipmentsMappingRepository(_dbContext);
            }
        }

        private IInternalSlidingRepository _iInternalSlidingRepository;
        public IInternalSlidingRepository InternalSlidingRepository
        {
            get
            {
                return _iInternalSlidingRepository ??= new InternalSlidingRepository(_dbContext);
            }

        }

        private IITITimeTableRepository _itiTimeTableRepository;
        public IITITimeTableRepository ITITimeTableRepository
        {
            get
            {
                return _itiTimeTableRepository ??= new ITITimeTableRepository(_dbContext);
            }

        }

        private IItiDocumentScrutinyRepository _itiDocumentScrutinyRepository;
        public IItiDocumentScrutinyRepository ItiDocumentScrutinyRepository
        {
            get
            {
                return _itiDocumentScrutinyRepository ??= new ItiDocumentScrutinyRepository(_dbContext);
            }

        }

        private IITISeatMatrixRepository _iITISeatMatrixRepository;
        public IITISeatMatrixRepository ITISeatMatrixRepository
        {
            get
            {
                return _iITISeatMatrixRepository ??= new ITISeatMatrixRepository(_dbContext);
            }

        }


        private IBTERInternalSlidingRepository __bTERInternalSlidingRepository;
        public IBTERInternalSlidingRepository BTERInternalSlidingRepository
        {
            get
            {
                return __bTERInternalSlidingRepository ??= new BTERInternalSlidingRepository(_dbContext);
            }

        }

        private IBterStudentJoinStatusRepository _bterStudentJoinStatusRepository;
        public IBterStudentJoinStatusRepository BterStudentJoinStatusRepository
        {
            get
            {
                return _bterStudentJoinStatusRepository ??= new BterStudentJoinStatusRepository(_dbContext);
            }

        }



        private IITICollegeAdmissionRepository _i_ITICollegeAdmissionRepository;
        public IITICollegeAdmissionRepository i_ITICollegeAdmissionRepository
        {
            get
            {
                return _i_ITICollegeAdmissionRepository ??= new ITICollegeAdmissionRepository(_dbContext);
            }
        }


        private IBTERAllotmentRepository _bterAllotmentRepository;
        public IBTERAllotmentRepository BTERAllotmentRepository
        {
            get
            {
                return _bterAllotmentRepository ??= new BTERAllotmentRepository(_dbContext);
            }


        }


        private IApplyBridgeCourseRepository _applyBridgeCourseRepository;
        public IApplyBridgeCourseRepository ApplyBridgeCourseRepository
        {
            get
            {
                return _applyBridgeCourseRepository ??= new ApplyBridgeCourseRepository(_dbContext);
            }
        }


        private I_ItemUnitMasterRepository _i_ItemUnitMasterRepository;

        public I_ItemUnitMasterRepository i_ItemUnitMasterRepository

        {

            get

            {

                return _i_ItemUnitMasterRepository ??= new ItemUnitMasterRepository(_dbContext);

            }

        }

        private IITIInventoryRepository _i_ITIInventoryRepository;

        public IITIInventoryRepository i_ITIInventoryRepository

        {

            get

            {

                return _i_ITIInventoryRepository ??= new ITIInventoryRepository(_dbContext);

            }

        }

        private IStudentRequestsRepository _iStudentRequestsRepository;
        public IStudentRequestsRepository iStudentRequestsRepository
        {
            get
            {
                return _iStudentRequestsRepository ??= new StudentRequestsRepository(_dbContext);
            }
        }


        private IHostelManagementRepository _i_HostelManagementRepository;
        public IHostelManagementRepository HostelManagementRepository
        {
            get
            {
                return _i_HostelManagementRepository ??= new HostelManagementRepository(_dbContext);
            }
        }



        private IHostelRoomDetailsRepository _iHostelRoomDetailsRepository;
        public IHostelRoomDetailsRepository iHostelRoomDetailsRepository
        {
            get
            {
                return _iHostelRoomDetailsRepository ??= new HostelRoomDetailsRepository(_dbContext);
            }
        }

        private IBTERAllotmentStatusRepository _iBTERAllotmentStatusRepository;
        public IBTERAllotmentStatusRepository iBTERAllotmentStatusRepository
        {
            get
            {
                return _iBTERAllotmentStatusRepository ??= new BTERAllotmentStatusRepository(_dbContext);
            }
        }

        private IIndustryInstitutePartnershipMasterRepository _iIndustryInstitutePartnershipMasterRepository;
        public IIndustryInstitutePartnershipMasterRepository iIndustryInstitutePartnershipRepository
        {
            get
            {
                return _iIndustryInstitutePartnershipMasterRepository ??= new IndustryInstitutePartnershipMasterRepository(_dbContext);
            }
        }


        private IDTEItemsMasterRepository _iDTEItemsMasterRepository;
        public IDTEItemsMasterRepository iDTEItemsMasterRepository
        {
            get
            {
                return _iDTEItemsMasterRepository ??= new DTEItemsMasterRepository(_dbContext);
            }
        }

        private IDTEItemCategoryMasterRepository _iDTEItemCategoryMasterRepository;
        public IDTEItemCategoryMasterRepository iDTEItemCategoryMasterRepository
        {
            get
            {
                return _iDTEItemCategoryMasterRepository ??= new DTEItemCategoryMasterRepository(_dbContext);
            }
        }

        private IDTEEquipmentsMasterRepository _iDTEEquipmentsMasterRepository;
        public IDTEEquipmentsMasterRepository iDTEEquipmentsMasterRepository
        {
            get
            {
                return _iDTEEquipmentsMasterRepository ??= new DTEEquipmentsMasterRepository(_dbContext);
            }
        }

        private IDTEIssuedItemsRepository _iDTEIssuedItemsRepository;
        public IDTEIssuedItemsRepository iDTEIssuedItemsRepository
        {
            get
            {
                return _iDTEIssuedItemsRepository ??= new DTEIssuedItemsRepository(_dbContext);
            }
        }

        private IDTETradeEquipmentsMappingRepository _iDTETradeEquipmentsMappingRepository;
        public IDTETradeEquipmentsMappingRepository iDTETradeEquipmentsMappingRepository
        {
            get
            {
                return _iDTETradeEquipmentsMappingRepository ??= new DTETradeEquipmentsMappingRepository(_dbContext);
            }
        }

        private IDTEItemUnitMasterRepository _iDTEItemUnitMasterRepository;
        public IDTEItemUnitMasterRepository iDTEItemUnitMasterRepository
        {
            get
            {
                return _iDTEItemUnitMasterRepository ??= new DTEItemUnitMasterRepository(_dbContext);
            }
        }

        private IDTEInventoryDashboardRepository _iDTEInventoryDashboardRepository;
        public IDTEInventoryDashboardRepository iDTEInventoryDashboardRepository
        {
            get
            {
                return _iDTEInventoryDashboardRepository ??= new DTEInventoryDashboardRepository(_dbContext);
            }
        }


        private ICollegeAdmissionSeatAllotmentRepository _collegeAdmissionSeatAllotmentRepository;
        public ICollegeAdmissionSeatAllotmentRepository CollegeAdmissionSeatAllotmentRepository
        {
            get
            {
                return _collegeAdmissionSeatAllotmentRepository ??= new CollegeAdmissionSeatAllotmentRepository(_dbContext);
            }
        }

        private ICorrectMeritRepository _iCorrectMeritRepository;
        public ICorrectMeritRepository iCorrectMeritRepository
        {
            get
            {
                return _iCorrectMeritRepository ??= new CorrectMeritRepository(_dbContext);
            }
        }

        private IAllotmentConfigurationRepository _iAllotmentConfigurationRepository;
        public IAllotmentConfigurationRepository iAllotmentConfigurationRepository
        {
            get
            {
                return _iAllotmentConfigurationRepository ??= new AllotmentConfigurationRepository(_dbContext);
            }
        }


        private I_ITICompanyMasterRepository _iITICompanyMasterRepository;
        public I_ITICompanyMasterRepository i_ITICompanyMasterRepository
        {
            get
            {
                return _iITICompanyMasterRepository ??= new ItiCompanyMasterRepository(_dbContext);
            }
        }

        private I_ItiHrMasterRepository _iItiHrMasterRepository;
        public I_ItiHrMasterRepository i_ItiHrMasterRepository
        {
            get
            {
                return _iItiHrMasterRepository ??= new ItiHrMasterRepository(_dbContext);
            }
        }

        private I_ItiCampusPostMasterRepository _itiCampusPostMasterRepository;
        public I_ItiCampusPostMasterRepository i_ItiCampusPostMasterRepository
        {
            get
            {
                return _itiCampusPostMasterRepository ??= new ItiCampusPostMasterRepository(_dbContext);
            }
        }

        private IStudentEnrollmentRepository _StudentEnrollmentRepository;
        public IStudentEnrollmentRepository StudentEnrollmentRepository
        {
            get
            {
                return _StudentEnrollmentRepository ??= new StudentEnrollmentRepository(_dbContext);
            }
        }


        private IPaymentServiceRepository _paymentServiceRepository;
        public IPaymentServiceRepository PaymentServiceRepository
        {
            get
            {
                return _paymentServiceRepository ??= new PaymentServiceRepository(_dbContext);
            }
        }




        private IGrivienceRepository _iGrivienceRepository;
        public IGrivienceRepository iGrivienceRepository
        {
            get
            {
                return _iGrivienceRepository ??= new GrivienceRepository(_dbContext);
            }
        }

        private IDetainedStudentsRepository _detainedStudentsRepository;
        public IDetainedStudentsRepository DetainedStudentsRepository
        {
            get
            {
                return _detainedStudentsRepository ??= new DetainedStudentsRepository(_dbContext);
            }
        }

        private IScholarshipRepository _scholarshipRepository;
        public IScholarshipRepository ScholarshipRepository
        {
            get
            {
                return _scholarshipRepository ??= new ScholarshipRepository(_dbContext);
            }
        }

        private IGuestRoomManagementRepository _i_GuestRoomManagementRepository;
        public IGuestRoomManagementRepository GuestRoomManagementRepository
        {
            get
            {
                return _i_GuestRoomManagementRepository ??= new GuestRoomManagementRepository(_dbContext);
            }
        }

        private IPaperSetterRepository _i_PaperSetterRepository;
        public IPaperSetterRepository PaperSetterRepository
        {
            get
            {
                return _i_PaperSetterRepository ??= new PaperSetterRepository(_dbContext);
            }
        }

        private ISsoidUpdateRepository _ssoidUpdateRepository;
        public ISsoidUpdateRepository SsoidUpdateRepository
        {
            get
            {
                return _ssoidUpdateRepository ??= new SsoidUpdateRepository(_dbContext);
            }
        }
        private ILeaveMasterRepository _leaveMasterRepository;
        public ILeaveMasterRepository LeaveMasterRepository
        {
            get
            {
                return _leaveMasterRepository ??= new LeaveMasterRepository(_dbContext);
            }
        }

        private ICenterObserverRepository _centerObserverRepository;
        public ICenterObserverRepository CenterObserverRepository
        {
            get
            {
                return _centerObserverRepository ??= new CenterObserverRepository(_dbContext);
            }
        }


        private ICheckListRepository _checkListRepository;
        public ICheckListRepository CheckListRepository
        {
            get
            {
                return _checkListRepository ??= new CheckListRepository(_dbContext);
            }
        }


        private IDispatchRepository _dispatchRepository;
        public IDispatchRepository DispatchRepository
        {
            get
            {
                return _dispatchRepository ??= new DispatchRepository(_dbContext);
            }
        }

        private IPolytechnicReportRepository _polytechnicReportRepository;
        public IPolytechnicReportRepository PolytechnicReportRepository
        {
            get
            {
                return _polytechnicReportRepository ??= new PolytechnicReportRepository(_dbContext);
            }
        }

        private IExaminerReportRepository _examinerReportRepository;
        public IExaminerReportRepository ExaminerReportRepository
        {
            get
            {
                return _examinerReportRepository ??= new ExaminerReportRepository(_dbContext);
            }
        }


        private I_ITICenterObserverRepository _itiCenterObserverRepository;
        public I_ITICenterObserverRepository ITICenterObserverRepository
        {
            get
            {
                return _itiCenterObserverRepository ??= new ITICenterObserverRepository(_dbContext);
            }
        }

        private ISecretaryJDDashboardRepository _secretaryJDDashboardRepository;
        public ISecretaryJDDashboardRepository SecretaryJDDashboardRepository
        {
            get
            {
                return _secretaryJDDashboardRepository ??= new SecretaryJDDashboardRepository(_dbContext);
            }
        }


        private IBoard_UniversityMasterRepository _IBoard_UniversityMasterRepository;
        public IBoard_UniversityMasterRepository Board_UniversityMasterRepository
        {
            get
            {
                return _IBoard_UniversityMasterRepository ??= new Board_UniversityMasterRepository(_dbContext);
            }
        }
        private IITICollegeProfileRepository _IITICollegeProfileRepository;
        public IITICollegeProfileRepository ITICollegeProfileRepository
        {
            get
            {
                return _IITICollegeProfileRepository ??= new ITICollegeProfileRepository(_dbContext);
            }
        }

        private IRenumerationExaminerRepository _iRenumerationExaminerRepository;
        public IRenumerationExaminerRepository RenumerationExaminerRepository
        {
            get
            {
                return _iRenumerationExaminerRepository ??= new RenumerationExaminerRepository(_dbContext);
            }
        }

        private IRenumerationJDRepository _iRenumerationJDRepository;
        public IRenumerationJDRepository RenumerationJDRepository
        {
            get
            {
                return _iRenumerationJDRepository ??= new RenumerationJDRepository(_dbContext);
            }
        }

        private IRenumerationAccounts _iRenumerationAccounts;
        public IRenumerationAccounts RenumerationAccounts
        {
            get
            {
                return _iRenumerationAccounts ??= new RenumerationAccountsRepository(_dbContext);
            }
        }

        private I_ITI_InspectionRepository _ITI_InspectionRepository;
        public I_ITI_InspectionRepository ITI_InspectionRepository
        {
            get
            {
                return _ITI_InspectionRepository ??= new ITI_InspectionRepository(_dbContext);
            }
        }


        private IITIApprenticeshipRepository _ITI_ApprenticeshipRepository;
        public IITIApprenticeshipRepository ITI_ApprenticeshipRepository
        {
            get
            {
                return _ITI_ApprenticeshipRepository ??= new ITI_ApprenticeshipRepository(_dbContext);
            }
        }



        private IITIPrivateEstablishRepository _ITIPrivateEstablishRepository;
        public IITIPrivateEstablishRepository ITIPrivateEstablishRepository
        {
            get
            {
                return _ITIPrivateEstablishRepository ??= new ITIPrivateEstablishRepository(_dbContext);
            }
        }

        private IITIGovtEMStaffMasterRepository _ITIGovtEMStaffMasterRepository;
        public IITIGovtEMStaffMasterRepository ITIGovtEMStaffMasterRepository
        {
            get
            {
                return _ITIGovtEMStaffMasterRepository ??= new ITIGovtEMStaffMasterRepository(_dbContext);
            }
        }

        private IBTERMasterRepository _IBTERMasterRepository;
        public IBTERMasterRepository BTERMasterRepository
        {
            get
            {
                return _IBTERMasterRepository ??= new BTERMasterRepository(_dbContext);
            }
        }


        private IITIPapperSetterRepository _ITIPapperSetterRepository;
        public IITIPapperSetterRepository ITIPapperSetterRepository
        {
            get
            {
                return _ITIPapperSetterRepository ??= new ITIPapperSetterRepository(_dbContext);
            }
        }

        private IJanaadharRepository _JanaadharRepository;
        public IJanaadharRepository JanaadharRepository
        {
            get
            {
                return _JanaadharRepository ??= new JanaadharRepository(_dbContext);
            }
        }

        private IWebsiteSettingsRepository _websiteSettingsRepository;
        public IWebsiteSettingsRepository WebsiteSettingsRepository
        {
            get
            {
                return _websiteSettingsRepository ??= new WebsiteSettingsRepository(_dbContext);
            }
        }

        private IAadharEsignRepository _aadharEsignRepository;
        public IAadharEsignRepository AadharEsignRepository
        {
            get
            {
                return _aadharEsignRepository ??= new AadharEsignRepository(_dbContext);
            }
        }

        private IUserActivityLoggerRepository _userActivityLoggerRepository;
        public IUserActivityLoggerRepository UserActivityLoggerRepository
        {
            get
            {
                return _userActivityLoggerRepository ??= new UserActivityLoggerRepository(_dbContext);
            }
        }

        private IITIPracticalExaminerRepository _ITIPracticalExaminerRepository;
        public IITIPracticalExaminerRepository ITIPracticalExaminerRepository
        {
            get
            {
                return _ITIPracticalExaminerRepository ??= new ITIPracticalExaminerRepository(_dbContext);
            }
        }

        private IUsersRequestRepository _userRequest;
        public IUsersRequestRepository UsersRequest
        {
            get
            {
                return _userRequest ??= new UsersRequestRepository(_dbContext);
            }
        }

        private ISMSSchedulerRepository _SMSSchedulerRepository;
        public ISMSSchedulerRepository SMSSchedulerRepository
        {
            get
            {
                return _SMSSchedulerRepository ??= new SMSSchedulerRepository(_dbContext);
            }
        }

        private IITIInvigilatorRepository _ititnvigilatorRepository;
        public IITIInvigilatorRepository ITIInvigilatorRepository
        {
            get
            {
                return _ititnvigilatorRepository ??= new ITIInvigilatorRepository(_dbContext);
            }
        }

        private IITIDispatchRepository _TIIDispatchRepository;
        public IITIDispatchRepository ITIDispatchRepository
        {
            get
            {
                return _TIIDispatchRepository ??= new ITIDispatchRepository(_dbContext);
            }
        }

        private IITIRelievingExamRepository _iTIRelievingExamRepository;
        public IITIRelievingExamRepository ITIRelievingExamRepository
        {
            get
            {
                return _iTIRelievingExamRepository ??= new ITIRelievingExamRepository(_dbContext);
            }
        }

        private IMasterConfigurationBterRepository _masterConfigurationBterRepository;
        public IMasterConfigurationBterRepository MasterConfigurationBterRepository
        {
            get
            {
                return _masterConfigurationBterRepository ??= new MasterConfigurationBterRepository(_dbContext);
            }
        }
        private IGuestHouseRepository _GuestHouseRepository;
        public IGuestHouseRepository IGuestHouseRepository
        {
            get
            {
                return _GuestHouseRepository ??= new GuestHouseRepository(_dbContext);
            }
        }



        private IITINodalOfficerExminerReportRepository _ITINodalOfficerExminerReport;
        public IITINodalOfficerExminerReportRepository ITINodalOfficerExminerReport
        {
            get
            {
                return _ITINodalOfficerExminerReport ??= new ITINodalOfficerExminerReportRepository(_dbContext);
            }
        }
        private ITestRepository _TestRepository;
        public ITestRepository TestRepository
        {
            get
            {
                return _TestRepository ??= new TestRepository(_dbContext);
            }
        }

        private IITINCVTRepository _ITINCVTRepository;
        public IITINCVTRepository ITINCVTRepository
        {
            get
            {
                return _ITINCVTRepository ??= new ITINCVTRepository(_dbContext);
            }
        }

        private I_ITIFlyingSquadManageRepository _ITIFlyingSquadManageRepository;
        public I_ITIFlyingSquadManageRepository ITIFlyingSquadManageRepository
        {
            get
            {
                return _ITIFlyingSquadManageRepository ??= new ITIFlyingSquadManageRepository(_dbContext);
            }
        }

        private IBTER_EstablishManagementRepository _BTER_EstablishManagementRepository;
        public IBTER_EstablishManagementRepository BTER_EstablishManagementRepository
        {
            get
            {
                return _BTER_EstablishManagementRepository ??= new BTER_EstablishManagementRepository(_dbContext);
            }
        }

        private I_ITIResultRepository _ITIResultRepository;
        public I_ITIResultRepository ITIResultRepository
        {
            get
            {
                return (_ITIResultRepository ??= new ITIResultRepository(_dbContext));
            }
        }


        private I_ITICenterSuperitendentExamReportRepository _ITICenterSuperitendentExamReportRepository;
        public I_ITICenterSuperitendentExamReportRepository ITICenterSuperitendentExamReportRepository
        {
            get
            {
                return (_ITICenterSuperitendentExamReportRepository ??= new ITICenterSuperitendentExamReportRepository(_dbContext));
            }
        }

        private I_ITIStudentEnrollmentRepository _ITIStudentEnrollmentRepository;
        public I_ITIStudentEnrollmentRepository ITIStudentEnrollmentRepository
        {
            get
            {
                return _ITIStudentEnrollmentRepository ??= new ITIStudentEnrollmentRepository(_dbContext);
            }
        }

        private IIssueTrackerRepository _issueTrackerRepository;


        public IIssueTrackerRepository IssueTrackerRepository
        {
            get
            {
                return _issueTrackerRepository ??= new IssueTrackerRepository(_dbContext);
            }
        }

        // issue Tracker Dashboard

        private IAdminDashboardIssueTrackerRepository _adminDashboardIssueTrackerRepository;
        public IAdminDashboardIssueTrackerRepository AdminDashboardIssueTrackerRepository
        {
            get
            {
                return _adminDashboardIssueTrackerRepository ??= new AdminDashboardIssueTrackerRepository(_dbContext);
            }
        }
        private IEnrollmentCancelationRepository _EnrollmentCancelationRepository;
        public IEnrollmentCancelationRepository EnrollmentCancelationRepository
        {
            get
            {
                return _EnrollmentCancelationRepository ??= new EnrollmentCancelationRepository(_dbContext);
            }
        }


        private I_ITICollegeMarksheetDownloadRepository _ITICollegeMarksheetDownloadRepository;
        public I_ITICollegeMarksheetDownloadRepository ITICollegeMarksheetDownloadRepository
        {
            get
            {
                return _ITICollegeMarksheetDownloadRepository ??= new ITICollegeMarksheetDownloadRepository(_dbContext);

            }
        }

        private IProjectMasterRepository _projectMasterRepository;
        public IProjectMasterRepository ProjectMasterRepository
        {
            get
            {
                return _projectMasterRepository ??= new ProjectMasterRepository(_dbContext);
            }



        }

        private IITINodalReportRepository _IITINodalReportRepository;
        public IITINodalReportRepository ITINodalReportRepository
        {
            get
            {
                return _IITINodalReportRepository ??= new ITINodalReportRepository(_dbContext);
            }



        }

        private I_ITI_InstructorRepository _ITI_InstructorRepository;
        public I_ITI_InstructorRepository ITI_InstructorRepository
        {
            get
            {
                return (_ITI_InstructorRepository ??= new ITI_InstructorRepository(_dbContext));
            }
        }

        private ITheoryMarksRevalRepository _theoryMarksRevalRepository;
        public ITheoryMarksRevalRepository TheoryMarksRevalRepository
        {
            get
            {
                return _theoryMarksRevalRepository ??= new TheoryMarksRevalRepository(_dbContext);
            }
        }



        private IRevalDispatchRepository _revalDispatchRepository;
        public IRevalDispatchRepository RevalDispatchRepository
        {
            get
            {
                return _revalDispatchRepository ??= new RevalDispatchRepository(_dbContext);
            }
        }

        private IBudgetHeadManagementRepository _BudgetHeadManagementRepository;
        public IBudgetHeadManagementRepository BudgetHeadManagementRepository
        {
            get
            {
                return _BudgetHeadManagementRepository ??= new BudgetHeadManagementRepository(_dbContext);
            }
        }


        private I_ITI_BGTHeadmasterRepository _ITI_BGTHeadmasterRepository;
        public I_ITI_BGTHeadmasterRepository ITI_BGTHeadmasterRepository
        {
            get
            {
                return (_ITI_BGTHeadmasterRepository ??= new ITI_BGTHeadmasterRepository(_dbContext));
            }
        }

        private I_ITICampusDetailsWebRepository _ITICampusDetailsWebRepository;
        public I_ITICampusDetailsWebRepository ITICampusDetailsWebRepository
        {
            get
            {
                return _ITICampusDetailsWebRepository ??= new ITICampusDetailsWebRepository(_dbContext);
            }
        }

        private I_ITIViewPlacedStudentsRepository _ITIviewPlacedStudentsRepository;
        public I_ITIViewPlacedStudentsRepository ITIViewPlacedStudentsRepository
        {
            get
            {
                return _ITIviewPlacedStudentsRepository ??= new ITIViewPlacedStudentsRepository(_dbContext);
            }
        }

        private I_ITIPlacementSelectedStudentRepository _ITIplacementSelectedStudentRepository;
        public I_ITIPlacementSelectedStudentRepository ITIPlacementSelectedStudentRepository
        {
            get
            {
                return _ITIplacementSelectedStudentRepository ??= new ITIPlacementSelectedStudentRepository(_dbContext);
            }
        }

        private I_ITIPlacementShortListStudentRepository _ITIplacementShortListStudentRepository;
        public I_ITIPlacementShortListStudentRepository ITIPlacementShortListStudentRepository
        {
            get
            {
                return _ITIplacementShortListStudentRepository ??= new ITIPlacementShortListStudentRepository(_dbContext);
            }
        }

        private I_ITIPlacementStudentRepository _ITIplacementStudentRepository;
        public I_ITIPlacementStudentRepository ITIPlacementStudentRepository
        {
            get
            {
                return _ITIplacementStudentRepository ??= new ITIPlacementStudentRepository(_dbContext);
            }
        }


        private IITIIIPManageRepository _ITIIIPManagenRepository;
        public IITIIIPManageRepository ITIIIPManageRepository
        {
            get
            {
                return _ITIIIPManagenRepository ??= new ITIIIPManageRepository(_dbContext);
            }
        }
        #endregion
        private I_ITI_IIP_TrimashQuaterlyReportRepository _ITI_IIP_TrimashQuaterlyReportRepository;
        public I_ITI_IIP_TrimashQuaterlyReportRepository ITI_IIP_TrimashQuaterlyReportRepository
        {
            get
            {
                return _ITI_IIP_TrimashQuaterlyReportRepository ?? new ITI_IIP_TrimashQuaterlyReportRepository(_dbContext);
            }
        }

    }
}

