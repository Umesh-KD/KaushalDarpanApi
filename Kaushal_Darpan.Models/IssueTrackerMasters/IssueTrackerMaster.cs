using Kaushal_Darpan.Models.ITIPlanning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.IssueTrackerMasters
{
    public class IssueTrackerMaster
    {
        public string ActionType { get; set; }
        public int IssueID { get; set; }
        public string ProjectName { get; set; }
        public string Issue { get; set; }
        public string Description { get; set; }
        public int PriorityID { get; set; }
        public int StatusID { get; set; }
        public string IssueFileName { get; set; }
       public string IssueDisFileName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string SolutionComment { get; set; }
        public string Comment { get; set; }
        public DateTime IssueDate { get; set; }
        public string SolutionBy { get; set; }
        public string SolvedFile { get; set; }
        public string IssueName { get; set; }
        public string SolvedDisFile { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public string IssueTitle { get; set; }
        public int DeleteStatus { get; set; }
        public int Return { get; set; }
        public int RoleID { get; set; }
        public int IssueTypeID { get; set; }
        public int IssueTypeDescriptionID { get; set; }
    }

    public class IssueTrackerSearchModel : RequestBaseModel
    {
        public int IssueID { get; set; }
        public string? IssueName { get; set; }
        public string? IssueDate { get; set; }
        public string? IssuePriorityDate { get; set; }
        public string? IssueresolvedDate { get; set; }
        public int Priority { get; set; }
    }


    public class IssueTrackerListSearchModel : RequestBaseModel
    {
        public int IssueID { get; set; }

        public string? Issue { get; set; }

        public string? IssueDate { get; set; }

        public string? IssuePriorityDate { get; set; }

        public string? IssueresolvedDate { get; set; }

        public int PriorityID { get; set; }

        public int Status { get; set; }
        public int RoleID { get; set; }
        public int IssueTypeDescriptionID { get; set; }
        public int IssueTypeID { get; set; }

        public int UserName { get; set; }
        public int IssueRoleID { get; set; }
        public int IssueRoleName { get; set; }
        public int StatusID { get; set; }

        public List<IssueFile>? IssueFile { get; set; }

      

    }


    public class IssueSaveData
    {
        public string? ActionType { get; set; }
        public int IssueID { get; set; }
        public string? ProjectName { get; set; }

        public string? Discription { get; set; }
        public int PriorityID { get; set; }
        public int StatusTypeID { get; set; }
        public string? Document { get; set; }
        public string? Dis_DocName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }

        public string? SolutionComment { get; set; }
        public string? Comment { get; set; }

        public int SolutionBy { get; set; }
        public string? SolvedFile { get; set; }
        public string? IssueName { get; set; }
        public string? SolvedDisFile { get; set; }
        public string? RegFileName { get; set; }
        public string? RegDisFileName { get; set; }

        public int RoleID { get; set; }
        public int IssueTypeID { get; set; }
        public int IssueTypeDescriptionID { get; set; }

        public List<IssueFile>? IssueFile { get; set; }

        //public List<ItiMembersModel>? ItiMembersModel { get; set; }

      

    }

    public class RoleRequest
    {
        public int RoleID { get; set; }
    }


    public class IssueFile
    {

        public string? RegFileName { get; set; }
        public string? RegDisFileName { get; set; }
    }
}



