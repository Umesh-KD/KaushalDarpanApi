using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ITIPapperSetter;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIPapperSetterRepository 
    {
        Task<DataTable> SaveData(ITIPapperSetterModel PapperSetterModal);
        Task<DataTable> GetSubjectList(int TradeId , int ExamType);

        Task<DataTable> GetProfessorList(int SubjectId);

        //Task<PaperSetterAssginListModel> GetAllPaperSeterAssignList(ITIPapperSetterModel Modal);

        Task<List<PaperSetterAssginListModel>> GetAllPaperSeterAssignList(ITIPapperSetterModel Modal);

        Task<List<ITIPapperSetterModel>> PaperSetterAssignListByID(int ID );

        Task<DataTable> PaperSetterAssignListRemoveByID(int ID , int Deletedby , int Roleid);

        Task<DataTable> GetTradeListByYearTradeID(int YearTradeId , int CourseTypeID);

        Task<DataTable> GetListForPaperUploadByProfessorID(int ProfessorID, string SSOID , int Roleid , int TypeID);

        Task<DataTable> UpdateUploadedPaperData(string UploadedPaperDocument, string Remark , int userid , int PKID , int Roleid);

        Task<DataTable> AutoSelectPaperDetailsUpdate(int SelectedProfessorID , int PKID, int userid , int roleid , string ssoid);

        Task<DataTable> PaperSetterProfessorDashboardCount(int userid, int EndTermID, int RoleID,   string ssoid , string para1);

        Task<DataTable> PaperRevertByExaminer(int ProfessorID, int PKID, int userid, int roleid, string ssoid , string RevertReason);
    }


    
}
