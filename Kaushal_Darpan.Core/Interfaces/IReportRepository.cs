using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.BterApplication;
using Kaushal_Darpan.Models.BterCertificateReport;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.FlyingSquad;
using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.OptionalFormatReport;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IReportRepository
    {
        Task<DataTable> GetAllDataRpt(TheorySearchModel filterModel);

        Task<DataSet> GetStudentAdmitCard(GenerateAdmitCardSearchModel model);
        Task<DataSet> GetTestRDLC(GenerateAdmitCardSearchModel model);
        Task<DataSet> GetApplicationFormPreview(BterSearchModel model);
        Task<DataSet> GetITIApplicationFormPreview(ItiApplicationSearchModel model);
        Task<DataSet> GetStudentAdmitCardBulk(int StudentExamID, int DepartmentID);
        Task<DataSet> GetStudentMarksheet(MarksheetDownloadSearchModel model);
        Task<DataSet> GetStudentHostelallotment(MarksheetDownloadSearchModel model);
        Task<DataSet> GetStudentEnrolledForm(ReportBaseModel model);
        Task<DataSet> GetStudentFeeReceipt(string EnrollmentNo);
        Task<DataSet> GetStudentApplicationFeeReceipt(string EnrollmentNo);

        Task<DataSet> GetStudentAllotmentFeeReceipt(string EnrollmentNo);
        Task<DataSet> GetPassoutStudentReport(PassoutStudentReport model);
        Task<DataSet> GetStudentApplicationChallanReceipt(int ApplicationID);
        Task<DataSet> GetITIStudentApplicationChallanReceipt(int ApplicationID);
        Task<DataSet> GetStudentAllotmentReceipt(int ApplicationID);
        Task<DataSet> GetStudentReportingCertificate(int ApplicationID);
        Task<DataSet> GetExaminerDetails(int StaffID, int DepartmentID);
        Task<DataSet> GetExaminerAppointLetter(int ExaminerID, int DepartmentID, int InstituteID, int EndTermID);
        Task<DataSet> GetAbsentReport();
        Task<DataSet> GetDispatchGroupDetails(int ID, int EndTermID, int CourseTypeID);
        Task<DataSet> DownloadDispatchGroupCertificate(int ID, int StaffID, int DepartmentID);
        Task<DataTable> GetStudentEnrollmentReports(DTEApplicationDashboardModel filterModel);
        Task<DataTable> GetGroupCenterMappingReports(GroupCenterMappingModel filterModel);
        Task<DataTable> GetCenterDailyReports(GroupCenterMappingModel filterModel);
        Task<DataTable> GetCenterDailyReport(GroupCenterMappingModel filterModel);
        Task<DataTable> ExaminationsReportsMenuWise(ExaminationsReportsMenuWiseModel filterModel);
        Task<DataTable> DownloadStudentEnrollmentDetails(DownloadStudentEnrollmentDetailsModel filterModel);
        Task<DataTable> DownloadStudentChangeEnrollmentDetails(DownloadStudentChangeEnrollmentDetailsModel filterModel);
        Task<DataTable> DownloadOptionalFormatReport(OptionalFormatReportModel filterModel);
        Task<DataTable> DateWiseAttendanceReport(DateWiseAttendanceReport filterModel);
        Task<DataTable> GetDownloadCenterDailyReports(GroupCenterMappingModel filterModel);
        Task<DataTable> GetStaticsReportProvideByExaminer(GroupCenterMappingModel filterModel);
        Task<DataTable> GetOnlineReportProvideByExaminer(OnlineMarkingSearchModel filterModel);
        Task<DataTable> GetExaminerReportAndMarksTracking(GroupCenterMappingModel filterModel);
        Task<DataTable> GetExaminerReportAndMarksTrackingStudent(GroupCenterMappingModel filterModel);
        Task<DataTable> GetExaminerReportAndPresentTrackingStudent(GroupCenterMappingModel filterModel);
        Task<DataSet> GetExaminerReportAndMarksDownload(GroupCenterMappingModel filterModel);
        Task<DataSet> GetExaminerReportAndPresentDownload(GroupCenterMappingModel filterModel);
        Task<DataTable> GetPrincipleDashboardReport(DTEApplicationDashboardModel filterModel);
        Task<DataTable> GetCollegeNodalReportsData(DTEApplicationDashboardModel filterModel);
        Task<DataTable> GetItiStudentEnrollmentReports(DTEApplicationDashboardModel filterModel);
        Task<DataTable> GetIitStudentExamReports(DTEApplicationDashboardModel filterModel);
        Task<DataTable> GetCollegesWiseReports();
        Task<DataTable> GetCollegesWiseExaminationReports();
        Task<DataSet> GetExaminationForm(ReportBaseModel model);
        Task<DataSet> GetITIExaminationForm(ReportBaseModel model);
        Task<DataSet> GetFlyingSquadDutyOrder(GetFlyingSquadDutyOrder model);
        Task<DataTable> AttendanceReport13B(AttendanceReport13BDataModel model);
        Task<DataSet> Report33(AttendanceReport13BDataModel model);
        Task<DataSet> DailyReport_BhandarForm1(AttendanceReport13BDataModel model);
        Task<DataSet> GetFlyingSquadReports(GetFlyingSquadDutyOrder model);
        Task<DataTable> GetFlyingSquad(GetFlyingSquadModal model);
        Task<DataTable> GetFlyingSquadTeamReports(GetFlyingSquadModal model);
        Task<DataTable> GetITIFlyingSquadTeamReports(GetFlyingSquadModal model);
        Task<DataTable> GetFlyingSquadReport(GetFlyingSquadModal model);
        Task<DataSet> GetITIFlyingSquadDutyOrder(GetFlyingSquadDutyOrder model);
        Task<DataSet> GetITIFlyingSquadReports(GetFlyingSquadDutyOrder model);
        Task<DataTable> GetITIFlyingSquad(GetFlyingSquadModal model);
        Task<DataTable> GetITIFlyingSquadReport(GetFlyingSquadModal model);
        Task<DataTable> GetStudentRollNoList(DownloadnRollNoModel model);
        Task<int> SaveRollNumbePDFData(DownloadnRollNoModel request);
        Task<int> ITISaveRollNumbePDFData(DownloadnRollNoModel request);
        Task<DataTable> DownloadAppearedPassed(DownloadAppearedPassed model);
        Task<DataTable> DownloadAppearedPassedInstitutewise(DownloadAppearedPassed model);
        Task<DataTable> DownloadBranchWiseStatistics(DownloadAppearedPassed model);
        Task<DataTable> DownloadInstituteBranchWiseStatisticsReport(DownloadAppearedPassed model);
        Task<DataTable> GetITIStudentRollNoList(DownloadnRollNoModel model);
        Task<DataTable> GetITIStudentRollNoList_collegewise(DownloadnRollNoModel model);
        Task<DataSet> DownloadTimeTable(ReportBaseModel model);
        Task<DataSet> ItiDownloadTimeTable(ReportBaseModel model);
        Task<DataTable> DownloadTimeTable_Header(ReportBaseModel model);
        Task<DataTable> DownloadStudentProfileDetails(ReportBaseModel model);
        Task<DataSet> GetITIStudentFeeReceipt(string EnrollmentNo);
        Task<DataSet> GetITIStudentApplicationFeeReceipt(string EnrollmentNo);
        Task<DataSet> GetITICollegeProfile(int CollegeId);
        Task<DataSet> GetITIApplicationForm(ItiApplicationSearchModel model);
        Task<DataSet> GetITIStudentAdmitCardBulk(int StudentExamID, int DepartmentID);
        Task<DataSet> GetITIStudentAdmitCard(GenerateAdmitCardModel model);
        Task<DataTable> GetEnrollmentList(ReportBaseModel model);
        Task<DataTable> GetBlankReport(BlankReportModel model);
        Task<DataTable> GetStudentCustomizetReportsColumns();
        Task<DataTable> GetStudentCustomizetReports(ReportCustomizeBaseModel model);
        Task<List<CommonDDLModel>> GetGender();
        Task<List<CommonDDLModel>> GetBlock();
        Task<List<CommonDDLModel>> GetCourseType(int DepartmentID = 0);
        Task<List<CommonDDLModel>> GetInstitute();
        Task<List<CommonDDLModel>> GetEndTerm();
        Task<DataSet> GetAllotmentReceipt(string AllotmentId);
        Task<DataTable> PaperCountCustomizeReportColumnsAndList(ReportCustomizeBaseModel model);

        Task<DataTable> PaperCountCustomizeReportList(ReportCustomizeBaseModel model);

        Task<DataTable> GetCenterWiseSubjectCountReportColumnsAndList(ReportCustomizeBaseModel model);
        Task<DataTable> GetOptionalFormatReportData(OptionalFromatReportSearchModel model);
        Task<DataTable> GetNonElectiveFormFillingReportData(NonElectiveFormFillingReportSearchModel model);
        Task<DataTable> GetRport33Data(Report33DataModel model);
        Task<DataTable> DailyReportBhandarForm(Report33DataModel model);


        Task<DataSet> GetDispatchPrincipalGroupCodeDetails(int ID, int DepartmentID);

        Task<DataSet> GetDispatchSuperintendentRptReport(int ID, int DepartmentID);
        Task<DataSet> GetDispatchSuperintendentRptReport1(int ID, int DepartmentID);


        Task<DataTable> GetCenterWiseSubjectCountReportNew(ReportCustomizeBaseModel model);
        Task<DataSet> TheoryMarkListReport(ReportCustomizeBaseModel ID);

        Task<DataTable> ScaReportAdmin(StudentCenteredActivitesMasterSearchModel filterModel);

        Task<DataSet> InstituteMasterReport();
        Task<DataSet> TeacherWiseReportPdf();

        Task<DataSet> SubjectWiseReportPdf();

        Task<DataTable> GetCenterSuperintendentStudentReport(DTEApplicationDashboardModel filterModel);

        Task<DataSet> StatisticsInformationReportPdf(GroupCenterMappingModel model);

        Task<DataSet> TheorymarksReportPdf(TheorySearchModel filterModel);


        Task<DataSet> TheoryMarkListPDFReport(ReportCustomizeBaseModel ID);
        Task<DataTable> GetITISearchRepot(ITISearchDataModel filterModel);
        Task<DataSet> Report23(AttendanceReport23DataModel model);
        Task<DataSet> GetExaminerReportOfPresentAndAbsentDownload(GroupCenterMappingModel filterModel);
        Task<DataSet> GetCollegePaymentFeeReceipt(string TransactionId);

        Task<DataSet> GetITIDispatchSuperintendentRptReport1(int ID, int DepartmentID);


        Task<DataSet> GetITI_Dispatch_ShowbundleByExaminerToAdminData(ITI_DispatchAdmin_ByExaminer_RptSearchModel model);
        Task<DataSet> ITIStateTradeCertificateReport(ITIStateTradeCertificateModel model);
        Task<DataSet> GetITIStudent_Marksheet(StudentMarksheetSearchModel model);
        Task<DataSet> GetITIStudent_MarksheetList(StudentMarksheetSearchModel model);

        Task<DataSet> GetPracticalExaminerMark(BlankReportModel model);
        Task<DataSet> GetPracticalExaminerAttendence(BlankReportModel model);
        Task<DataSet> DownloadTheoryStudentITI(ItiTheoryStudentMaster model);
        Task<DataSet> GetPracticalExaminerMarksReport(BlankReportModel model);
        Task<DataTable> StateTradeCertificateDetails(ITIStateTradeCertificateModel body);

        Task<DataSet> ITIMarksheetConsolidated(ITIStateTradeCertificateModel model);
        Task<DataSet> GetITIStudent_PassList(StudentMarksheetSearchModel model);
        Task<DataSet> GetITIStudent_PassDataList(StudentMarksheetSearchModel model);

        Task<DataSet> ITITradeWiseResult(ITIStateTradeCertificateModel model);

        Task<DataSet> GetITITradeWiseResultDataList(ITIStateTradeCertificateModel model);
        Task<DataSet> GetITIAddmissionStatisticsDataList(ITIAddmissionReportSearchModel model);
        Task<DataSet> GetITISeatUtilizationStatusDataList(ITIAddmissionReportSearchModel model);
        Task<DataSet> GetFinalAdmissionGenderWise(FinalAdmissionGenderWiseRequestModel model);

        Task<DataSet> GetZoneDistrictSeatUtilization(ZoneDistrictSeatUtilizationRequestModel model);
        Task<DataSet> GetZoneDistrictSeatUtilization_ByGender(ZoneDistrictSeatUtilizationByGenderRequestModel model);


        Task<DataSet> GetVacantSeatReport(VacantSeatReportRequestModel model);
        Task<DataSet> GetAllottedAndReportedCountByITI(AllottedReportedRequestModel model);

        Task<DataSet> GetStudentDataAgeBetween15And29(StudentDataAgeBetween15And29RequestModel model);
        Task<DataSet> GetITIAdmissionsInWomenWingDataList(ITIAddmissionWomenReportSearchModel model);
        Task<DataSet> GetITIStudentSeatAllotmentDataList(ITIAddmissionWomenReportSearchModel model);
        Task<DataSet> GetITIStudentSeatWithdrawDataList(ITIAddmissionWomenReportSearchModel model);
        Task<DataTable> CenterWiseTradeStudentCount(CenterStudentSearchModel filterModel);
        Task<DataSet> GetITITradeWiseAdmissionStatusDataList(ITIAddmissionWomenReportSearchModel model);
        Task<DataSet> GetITIPlaningDetailsDataList(ITIAddmissionWomenReportSearchModel model);
        Task<DataSet> GetITICategoryWiseSeatUtilizationDataList(ITIAddmissionReportSearchModel model);

        Task<DataSet> GetAllotmentReportCollege(AllotmentReportCollegeRequestModel model);

        Task<DataSet> GetAllotmentReportCollegeForAdmin(AllotmentReportCollegeForAdminRequestModel model);
        Task<DataTable> GetBterCertificateReport(BterCertificateReportDataModel filterModel);
        Task<DataSet> BterCertificateReportDownload(BterCertificateReportDataModel filterModel);
        Task<DataSet> BterDiplomaReportDownload(BterCertificateReportDataModel filterModel);
        Task<DataSet> AppearedPassedStatisticsReportDownload(BterCertificateReportDataModel filterModel);
        Task<DataSet> AppearedPassedInstituteWiseDownload(BterCertificateReportDataModel filterModel);


        Task<DataTable> GetStudentjanaadharDetailReport(StudentItiSearchModel model);

        Task<DataSet> GetDirectAdmissionReport(DirectAdmissionReportRequestModel model);
        Task<DataSet> GetIMCAllotmentReport(IMCAllotmnentReportRequestModel model);
        Task<DataTable> GetInstitutejanaadharDetailReport();

        Task<DataTable> GetDropOutStudentListbyinstituteID(int InstituteID);

        Task<DataSet> GetInternalSlidingForAdminReport(InternalSlidingForAdminReport model);

        Task<DataSet> GetSwappingForAdminReport(SwappingForAdminReport model);

        Task<DataTable> GetEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model);
        Task<DataTable> GetBterStatisticsReport(BterStatisticsReportDataModel model);
        Task<DataTable> GetMassCoppingReport(BterStatisticsReportDataModel model);
        Task<DataSet> GetBterBridgeCourseReport(BterStatisticsReportDataModel filterModel);

        Task<DataTable> ResultStatisticsBridgeCourseReport(StatisticsBridgeCourseModel model);
        Task<DataTable> DownloadResultStatisticsReport(StatisticsBridgeCourseModel model);
        Task<DataTable> DownloadResultStatisticsBridgeCourseStreamWiseReport(StatisticsBridgeCourseModel model);
        Task<DataSet> GetBterBranchWiseStatisticalReport(BterStatisticsReportDataModel filterModel);

        Task<DataTable> GetCollegeInformationReport(CollegeInformationReportSearchModel model);

        Task<DataTable> GetEwsReport(EWSReportSearchModel model);

        Task<DataTable> GetUFMStudentReport(UFMStudentReportSearchModel model);

        Task<DataTable> GetSessionalFailStudentReport(GetSessionalFailStudentReport model);
        Task<DataTable> GetInstituteStudentReport(InstituteStudentReport model);
     
      
        Task<DataTable> GetRMIFailStudentReport(RMIFailStudentReport model);

        Task<DataSet> RelievingLetterReport(RelievingLetterSearchModel model);
        Task<DataSet> TheorymarksReportPdf_BTER(TheorySearchModel body);
        Task<DataSet> ApprenticeshipFresherReport(ApprenticeshipRegistrationSearchModal model);
        Task<DataSet> ApprenticeshipPassoutReport(ApprenticeshipRegistrationSearchModal model);
        Task<DataSet> ApprenticeshipReport(ApprenticeshipRegistrationSearchModal model);
        Task<DataSet> WorkshopProgressReport(WorkshopProgressRPTSearchModal model);
        Task<DataSet> PmnamMelaReport(ITIPMNAM_Report_SearchModal body);
        Task<DataSet> MelaReport(ITIPMNAM_Report_SearchModal model);
        Task<DataSet> GetRevalDispatchGroupDetails(int ID, int EndTermID, int CourseTypeID);
        Task<DataSet> DownloadRevalDispatchGroupCertificate(int ID, int StaffID, int DepartmentID);
        Task<DataTable> GetTheoryFailStudentReport(TheoryFailStudentReport model);
        Task<DataSet> GetITIAllotmentReport(IMCAllotmnentReportRequestModel model);

        Task<DataTable> GetRevaluationStudentDetailsReport(RevaluationStudentDetailReport model);

        Task<DataTable> GetCenterSuperinstendentAttendanceReport(searchCenterSuperintendentAttendance model);

        Task<DataTable> GetStudentExaminerDetailsReport(StudentExaminerDetailReport model);
       // Task<DataTable> GetStudentExaminerDetailsReport(StudentExaminerDetailReport model);  
        //Task<DataTable> GetCentarlSupridententDistrictReportDataListReport(CentarlSupridententDistrictRequestModel model);
        Task<DataSet>? GetCentarlSupridententDistrictReportDataListReport(CentarlSupridententDistrictRequestModel model);

        //Task<DataSet> GetStudentFailTheoryReport(StudentFailTheoryReportModel model);
        //Task<DataSet> GetStudentFailTheoryReport(StudentFailTheoryReportModel model);
        Task<DataTable> GetITIEstablishManagementStaffReport(BTER_EstablishManagementReportSearchModel model);
        Task<DataTable> GetBterDuplicateCertificateReport(BterCertificateReportDataModel filterModel);
        Task<DataSet> GetStudentDuplicateMarksheet(MarksheetDownloadSearchModel model);
        Task<DataSet> BterDuplicateProvisionalCertificateDownload(BterCertificateReportDataModel filterModel);
        Task<DataSet> PmnamMelaReportnodelOfficer(ITIPMNAM_Report_SearchModal body);
        Task<DataSet> GetStudentWithdranSeat(AllotmentReportCollegeRequestModel model);
        Task<DataSet> GetstudentWithdrawnList(AllotmentReportCollegeRequestModel model);
    }
}