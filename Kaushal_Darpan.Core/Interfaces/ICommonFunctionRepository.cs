using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CenterSuperitendent;
using Kaushal_Darpan.Models.CitizenSuggestion;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.EgrassPayment;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Results;
using Kaushal_Darpan.Models.RPPPayment;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.SubjectMaster;
using Kaushal_Darpan.Models.Test;
using Kaushal_Darpan.Models.UploadFileWithPathData;
using Kaushal_Darpan.Models.UserMaster;
using Kaushal_Darpan.Models.ViewStudentDetailsModel;
using System.Data;
using System.Reflection;
using static Kaushal_Darpan.Models.CommonFunction.ItiTradeAndCollegesDDL;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICommonFunctionRepository
    {
        Task<List<CommonDDLModel>> GetLevelMaster();
        Task<List<CommonDDLModel>> GetDesignationMaster();
        Task<List<CommonDDLModel>> PWDCategory();
        Task<List<CommonDDLModel>> GetDistrictMaster();
        Task<List<CommonDDLModel>> GetParliamentMaster();
        Task<List<CommonDDLModel>> GetNodalCenter(int InstituteID);
        Task<List<CommonDDLModel>> GetNodalExamCenterDistrict(int District, int EndTermID);
        Task<List<CommonDDLModel>> NodalInstituteList(int InstituteID);
        Task<List<CommonDDLModel>> GetTehsilMaster();
        Task<List<CommonDDLModel>> GetDivisionMaster();
        Task<DataTable> InstitutionCategory();

        Task<DataTable> ManagementType(int DepartmentID = 0);
        Task<DataTable> StreamType();
        Task<DataTable> InstituteMaster(int DepartmentID, int Eng_NonEng, int EndTermId);
        Task<DataTable> Iticollege(int DepartmentID, int Eng_NonEng, int EndTermId, int InsutiteId);
        Task<DataTable> IticenterColleges(int DepartmentID, int Eng_NonEng, int EndTermId, int InstituteID);
        Task<DataTable> StreamMaster(int DepartmentID = 0, int StreamType = 0, int EndTermId = 0);
        Task<DataTable> ItiTrade(int DepartmentID = 0, int StreamType = 0, int EndTermId = 0, int InstituiteID = 0, int DivisionId = 0);
        Task<DataTable> ItiShiftUnitDDL(int ID = 0, int FinancialYearID = 0, int CourseTypeID = 0, int InstituteID = 0);
        Task<DataTable> StreamMasterwithcount(int DepartmentID = 0, int StreamType = 0, int EndTermId = 0);
        Task<DataTable> StreamMasterByCampus(int CampusPostID, int DepartmentID, int EndTermId);
        Task<DataTable> SemesterMaster(int ShowAllSemester = 0);
        Task<DataTable> SemesterGenerateMaster();
        Task<List<CommonDDLModel>> StudentType();
        //Task<List<CommonDDLModel>> StudentStatus();
        Task<DataTable> ExamCategory();
        Task<DataTable> GetPaperList();
        Task<DataTable> GetSubjectList(int DepartmentID);
        Task<DataTable> GetExamShift(int DepartmentID);
        Task<DataTable> GetFinancialYear();
        Task<DataTable> GetMonths();
        Task<DataTable> GetExamType();
        Task<DataTable> GetPaperType();
        Task<DataTable> CasteCategoryA();
        Task<DataTable> CasteCategoryB();
        Task<DataTable> Board_UniversityMaster();
        Task<DataTable> PassingYear();
        Task<DataTable> AdmissionPassingYear();

        Task<ViewStudentDetailsModel> ViewStudentDetails(ViewStudentDetailsRequestModel model);
        Task<DataTable> ViewStudentAdmittedDetails(ViewStudentDetailsRequestModel model);
        Task<ViewStudentDetailsModel> ITIViewStudentDetails(ViewStudentDetailsRequestModel model);
        Task<StudentMasterModel> PreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndtermID, int StudentExamID);
        Task<StudentMasterModel> ITIPreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndtermID, int StudentExamID);
        Task<DataTable> UploadFilePath();
        Task<DataTable> CollegeType();
        Task<List<CommonDDLModel>> GetSubjectMasterDDL(int DepartmentID);
        Task<List<CommonDDLModel>> GetCommonMasterDDLByType(string type);
        Task<List<CommonDDLModel>> GetCampusPostMasterDDL(int DepartmentID);

        Task<List<CommonDDLModel>> GetCategoryDMasterDDL(int MeritalStatus);
        Task<List<CommonDDLModel>> GetCampusWiseHiringRoleDDL(int campusPostId, int DepartmentID);

        Task<List<CommonDDLModel>> PlacementCompanyMaster(int DepartmentID);
        Task<List<CommonDDLModel>> PrefentialCategoryMaster(int DepartmentID, int CourseTypeId, int PrefentialCategoryType);
        Task<DataTable> PlacementCompanyMaster_IDWise(int ID, int DepartmentID);
        Task<List<CommonDDLModel>> GetStateMaster();
        Task<List<CommonDDLModel>> GetCastCategory();

        Task<List<CommonDDLModel>> DistrictMaster_StateIDWise(int StateID);
        Task<List<CommonDDLModel>> DistrictMaster_DivisionIDWise(int DivisionID);
        Task<List<CommonDDLModel>> TehsilMaster_DistrictIDWise(int DistrictID);
        Task<List<CommonDDLModel>> SubDivisionMaster_DistrictIDWise(int DistrictID);
        Task<List<CommonDDLModel>> AssemblyMaster_DistrictIDWise(int DistrictID);
        Task<List<CommonDDLModel>> GetHiringRoleMaster();
        Task<List<CommonDDLModel>> GetExamerSSoidDDL(int DepartmentID);

        Task<M_AadharCardServiceMaster> GetAadharCardServiceMaster();
        Task<bool> RPPCreatePaymentRequest(RPPPaymentRequestModel request);
        Task<RPPPaymentGatewayDataModel> RPPGetpaymentGatewayDetails(RPPPaymentGatewayDataModel model);
        Task<List<RPPResponseParametersModel>> RPPGetPaymentListIDWise(string TransactionID);
        Task<DataTable> GetRPPTransactionList(RPPTransactionSearchFilterModelModel Model);
        Task<bool> RPPSaveData(RPPPaymentResponseModel request);
        Task<bool> RPPUpdateRefundStatus(RPPPaymentResponseModel request);
        Task<bool> RPPUpdateRefundTransactionStatus(RPPRefundTransactionDataModel request);
        Task<EmitraRequstParametersModel> GetEmitraServiceDetails(EmitraRequestDetailsModel Model);
        Task<EmitraTransactionsModel> CreateEmitraTransation(EmitraTransactionsModel Model);
        Task<EmitraTransactionsModel> CreateEmitraTransationITI(EmitraTransactionsModel Model);
        Task<EmitraTransactionsModel> CreateEmitraApplicationTransation(EmitraTransactionsModel Model);
        Task<List<RPPResponseParametersModel>> GetPreviewPaymentDetails(int CollegeID);
        Task<DataTable> GetTransactionDetailsActionWise(StudentSearchModel model);


        Task<bool> UpdateEmitraPaymentStatus(EmitraResponseParametersModel request);
        Task<Int64> UpdateEmitraApplicationPaymentStatus(EmitraResponseParametersModel request);
        Task<DataTable> GetEmitraTransactionDetails(string PRN);
        Task<DataTable> GetEmitraITITransactionDetails(string PRN);

        Task<DataTable> GetEmitraApplicationTransactionDetails(string PRN);
        Task<DataTable> GetEgrassDetails_DepartmentWise(int DepartmentID, string PaymentType);
        Task<int> EGrassPaymentDetails_Req_Res(EGrassPaymentDetails_Req_ResModel request);
        Task<DataTable> GetOfflinePaymentDetails(int CollegeID);
        Task<DataTable> GetEGrass_AUIN_Verify_Data(int EGrassPaymentAID);
        Task<int> GRAS_GetPaymentStatus_Req_Res(EGrassPaymentDetails_Req_ResModel req_Res);
        Task<DataTable> ParentMenu(int DepartmentID);
        Task<DataTable> CityMasterDistrictWise(int DistrictID);
        Task<DataTable> PanchayatSamiti(int DistrictID);
        Task<DataTable> GramPanchayatSamiti(int TehsilID);
        Task<DataTable> VillageMaster(int ID);
        Task<DataTable> ExamStudentStatus(int roleId, int type);
        Task<DataTable> ITIExamStudentStatus(int roleId, int type);
        Task<List<CommonDDLModel>> GetRoleMasterDDL(int DepartmentID = 0, int EngNonEng = 0);
        Task<List<CommonDDLModel>> GetStaffTypeDDL();
        Task<List<CommonDDLModel>> GetGroupCode(CommonDDLSubjectMasterModel model);
        Task<DataTable> GetExaminerGroupCode(CommonDDLExaminerGroupCodeModel model);
        Task<DataTable> GetExaminerGroupCode_Reval(CommonDDLExaminerGroupCodeModel model);
        Task<List<CommonDDLModel>> GetConfigurationType(int RoleId = 0, int TypeID = 0);

        Task<DataTable> GetExamName();
        Task<DataTable> DDL_InvigilatorSSOID(DDL_InvigilatorSSOID_DataModel model);
        Task<List<CommonDDLModel>> GetParentSubjectDDL(SubjectSearchModel req);
        Task<List<CommonDDLModel>> SubjectMaster_SemesterIDWise(int SemesterID, int DepartmentID);
        Task<List<CommonDDLModel>> SubjectMaster_SubjectCode_SemesterIDWise(int SemesterID, int DepartmentID, string SubjectCode);
        Task<List<CommonDDLModel>> SubjectMaster_StreamIDWise(int StreamID, int DepartmentID, int SemesterID);
        Task<DataTable> GetStudentStatusByRole(int roleId, int type);
        Task<DataTable> GetEnrollmentCancelStatusByRole(int roleId, int type);
        Task<DataTable> ItiGetStudentStatusByRole(int roleId, int type);
        Task<DataTable> GetCommonMasterData(string MasterCode, int DepartmentID, int CourseTypeID = 0);
        Task<List<CommonDDLModel>> GetCenterMasterDDL(RequestBaseModel request);
        Task<List<CommonDDLModel>> GetSubjectMasterDDL_New(CommonDDLSubjectMasterModel request);

        Task<DataTable> GetCollegeTypeList();
        Task<DataTable> GetTradeTypesList();
        Task<DataTable> GetTradeLevelList();
        Task<DataTable> TradeListGetAllData(ItiTradeSearchModel request);
        Task<DataTable> ItiCollegesGetAllData(ItiCollegesSearchModel request);
        Task<DataTable> BterCollegesGetAllData(BterCollegesSearchModel request);

        Task<List<CommonSerialMasterResponseModel>> GetSerialMasterData(CommonSerialMasterRequestModel request);

        Task<List<CommonDDLModel>> GetInstituteMasterByTehsilWise(int TehsilID, int EndTermId);
        Task<List<CommonDDLModel>> GetInstituteMasterByDistrictWise(int DistrictID, int EndTermId);
        Task<List<CommonDDLModel>> GovtInstitute_DistrictWise(int DistrictID, int EndTermId);

        Task<List<CommonDDLModel>> GetSubCasteCategoryA(int CasteCategoryID);
        Task<List<CommonDDLModel>> InsituteMaster_DistrictIDWise(int DistrictID, int EndTermId);
        Task<List<CommonDDLModel>> InsituteMaster_DistrictIDWise(int DistrictID, int EndTermId, int DepartmentID);
        Task<UserRequestModel> CheckSSOIDExists(string SSOID, string RoleID, string InstituteID);
        Task<DataSet> GetOptionalSubjectsByStudentID(Int32 StudentID, Int32 DepartmentID, int StudentExamID);
        Task<List<CommonDDLModel>> GetQualificationDDL(string type);
        Task<List<CommonDDLModel>> GetCategaryCastDDL(string type);
        Task<List<CommonDDLModel>> GetCommonSubjectDDL(CommonDDLCommonSubjectModel model);
        Task<List<CommonDDLModel>> GetCommonSubjectDetailsDDL(CommonDDLCommonSubjectModel model);
        Task<DataTable> GetActiveTabList(int DepartmentID, int ApplicationID, int RoleID);
        Task<List<CommonDDLModel>> GetSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request);
        Task<List<CommonDDLModel>> GetTimeTableSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request);
        Task<bool> UpwardMomentUpdate(UpwardMoment model);
        Task<List<EmitraApplicationstatusModel>> GetDataItiStudentApplication(ItiStuAppSearchModelUpward filterModel);
        Task<List<CommonDDLModel>> Subjects_Semester_SubjectCodeWise(int SemesterID, int DepartmentID, string SubjectCode, int EndTerm, int CourseTypeID);

        Task<bool> UpdateITIEmitraPaymentStatus(EmitraResponseParametersModel request);
        Task<List<CommonDDLModel>> CategoryBDDLData(int DepartmentID);
        Task<DataTable> StreamDDL_InstituteWise(StreamDDL_InstituteWiseModel request);

        Task<DataTable> ExamStudentStatusApprovalReject(int roleId, int type);


        Task<List<CommonDDLModel>> GetITITradeNameDDl(CommonDDLSubjectCodeMasterModel request);

        Task<List<CommonDDLModel>> GetITISubjectNameDDl(CommonDDLSubjectCodeMasterModel request);

        Task<List<CommonDDLModel>> GetITISubjectCodeDDl(CommonDDLSubjectCodeMasterModel request);
        Task<DataTable> StreamDDLInstituteIdWise(StreamDDL_InstituteWiseModel request);
        Task<DataTable> QualificationDDL(QualificationDDLDataModel request);
        Task<DataTable> GetDateSetting(DateSettingConfigModel request);
        Task<List<CommonDDLModel>> GetReletionDDL(string type);
        Task<List<CommonDDLModel>> GetRoomTypeDDL(string type);
        Task<List<CommonDDLModel>> GetRoomTypeDDLByHostel(string type, int HostelID);

        Task<DataTable> SessionConfiguration(SessionConfigModel request);

        Task<List<CommonDDLModel>> GetHostelDDL(int DepartmentID, int InstituteID);
        Task<List<CommonDDLModel>> GetTechnicianDDL(int StaffParentID);
        Task<List<CommonDDLModel>> GetHOD_DDL(int CourseID);
        Task<List<CommonDDLModel>> ITIPlacementCompanyMaster(int DepartmentID);
        Task<List<CommonDDLModel>> GetSubjectForCitizenSugg(int selectedOption);
        Task<DataTable> GetManageHostelWardenRole(string SSOID, int RoleID = 0);
        Task<List<CommonDDLModel>> GetSubjectTheoryParctical(CommonDDLSubjectCodeMasterModel request);
        Task<List<SubjectMaster>> GetBackSubjectList(CommonDDLSubjectCodeMasterModel request);
        Task<DataTable> GetPrintRollAdmitCardSetting(CollegeMasterSearchModel model);
        Task<DataTable> Get_ITIPrintRollAdmitCardSetting(CollegeMasterSearchModel model);

        Task<List<CommonDDLModel>> GetDteCategory_BranchWise(int ID);
        Task<List<CommonDDLModel>> GetDteEquipment_CategoryWise(int ID);
        Task<List<CommonDDLModel>> GetEquipment_CategoryWise(int ID);
        Task<List<CommonDDLModel>> GetCategory_TradeWise(int ID);

        Task<List<CommonDDLModel>> GetDDl_StatusForGrivience();
        Task<DataTable> TradeListTradeTypeWise(ItiTradeSearchModel request);
        Task<List<CommonDDLModel>> GetITICenterDDL(int ID,int CourseType);
        Task<int> Dummy_SaveAndMoveStudentImages(string json);
        Task<DataTable> GetCenter_DistrictWise(CenterMasterDDLDataModel body);
        Task<DataTable> GetExamDate(CenterMasterDDLDataModel body);
        Task<DataTable> GetStaff_InstituteWise(StaffMasterDDLDataModel body);
        Task<List<CommonDDLModel>> GetCategory_BranchWise(int ID);

        Task<List<CommonDDLModel>> GetDTEEquipment_CategoryWise(int ID);
        Task<DataTable> GetCenterCodeInstituteWise(int ID);

        Task<List<CommonDDLModel>> GetddlCenterDownloadOrReceived(string Type);

        Task<List<CommonDDLModel>> GetDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID);
        Task<List<CommonDDLModel>> GetRevalDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID);
        Task<DataTable> GetCurrentAdmissionSession(int DepartmentId);

        Task<List<CommonDDLModel>> GetDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID);
        Task<DataTable> DDL_GroupCode_ExaminerWise(DDL_GroupCode_ExaminerWiseModel model);

        Task<DataTable> GetITIOptionFormData(ItiTradeSearchModel request);

        Task<List<CommonDDLModel>> DDL_CampusPostTypeMaster(string type);

        Task<DataTable> PublicInfo(PublicInfoModel request);

        Task<DataTable> GetLateralQualificationBoard(string ExamType);

        Task<DataTable> GetApplicationSubmittedSteps(string AppplicationId);

        Task<List<CommonDDLModel>> DDL_OfficeMaster(int DepartmentID, int LevelID);
        Task<List<CommonDDLModel>> DDL_PostMaster();

        Task<List<CommonDDLModel>> AllDDlManageByTypeCommanMaster(string type);
        Task<List<CommonDDLModel>> AllDDlCenterMaster(string type);

        Task<List<CommonDDLModel>> GetDesignationAndPostMaster();
        Task<DataTable> CenterSuperitendentDDL(CenterSuperitendentDDL body);
        Task<bool> HasValidAge(string dateFrom);
        Task<string> CommonVerifierApiSSOIDGetSomeDetails(CommonVerifierApiDataModel request);

        Task<DataTable> GetDteEquipment_Branch_Wise_CategoryWise(int Category);

        Task<List<CommonDDLModel>> GetITIDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID);

        Task<List<CommonDDLModel>> GetITIddlCenterDownloadOrReceived(string Type);
        Task<DataTable> Dummy_GetChangeInvalidPathOfDocuments();
        Task<DataTable> Dummy_GetMoveFilesFromStudentFolderToNewFolderStructure(List<TestTwoPathNew> model);
        Task<EmitraCollegeTransactionsModel> SaveEmitraCollegeTransation(EmitraCollegeTransactionsModel Model);
        Task<DataTable> GetITIStaffInstituteWise(StaffMasterDDLDataModel body);
        Task<DataTable> GetTables();
        Task<DataTable> GetSPs();
        Task<DataTable> GetTableColumn(string Table);
        Task<DataTable> GetTableRecordCount(string Table);
        Task<string> TruncateTableRow(string table);
        Task<string> AddTableRows(DataTable Table);
        Task<DataTable> GetTableRows(string Table, string PageNumber, string PageSize);
        Task<EmitraServiceAndFeeModel> GetEmitraServiceAndFeeData(EmitraServiceAndFeeRequestModel model);
        Task<long> UpdateEmitraCollegePaymentStatus(EmitraCollegeTransactionsModel model);
        Task<DataTable> GetEmitraCollegeTransactionDetails(string PRN);
        Task<List<CommonDDLModel>> GovtITICollege_DistrictWise(int DistrictID, int EndTermId);
        Task<DataTable> ITIGetStaff_InstituteWise(StaffMasterDDLDataModel body);
        Task<DataTable> GetBTEROriginalDocument(GetBTEROriginalListModel body);
        Task<DataTable> UploadBTEROriginalDocument(UploadOriginalFileWithPathDataModel Model);
        Task<List<CommonDDLModel>> GetCommonMasterDDLStatusByType(string type);

        Task<int> NodalCenterCreate(NodalCenterModel model);
        Task<DataTable> NodalCenterList(NodalCenterModel model);
        Task<DataTable> DC2ndYear_BranchesDDL(int CourseType, int CoreBranch);

        Task<DataTable> ITI_SemesterMaster(int parameter1 = 0 , string parameter2 ="");

        Task<DataTable> ExamSessionConfiguration(SessionConfigModel request);

        Task<List<CommonDDLModel>> GetALLEquipmentCategory();

        Task<DataTable> UnPublishData(UnPublishDataModel model);

        Task<DataTable> GetCollegeDetails(int collegeID);
        Task<DataTable> BterGetBranchbyCollege(BterCollegesSearchModel request);

        Task<DataTable> GetAllotmentMaster(CommonDDLCommonSubjectModel request);
        Task<DataTable> GetExamResultType();
        Task<DataTable> DDL_GroupCode_ExaminerWise_Reval(DDL_GroupCode_ExaminerWiseModel model);
        Task<DataTable> StudentListForAdmitCard_CS(StudentAdmitCardDownloadModel model);
        Task<List<CommonDDLModel>> GetITICampusPostMasterDDL(int DepartmentID);

        Task<List<CommonDDLModel>> GetITICampusWiseHiringRoleDDL(int campusPostId, int DepartmentID);
        Task<DataTable> ITIStreamMasterByCampus(int CampusPostID, int DepartmentID, int EndTermId);

        Task<DataTable> GetPublicInfoStatus(int DepartmentId);
        Task<DataTable> DDL_RWHEffectedEndTerm(DDL_RWHEffectedEndTermModel model);
        Task<DataTable> GetMigrationType();
        Task<DataTable> ITI_DeirectAdmissionOptionFormData(ItiTradeSearchModel request);
    }
}
