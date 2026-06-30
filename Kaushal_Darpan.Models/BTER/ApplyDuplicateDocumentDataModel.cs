using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BTER
{
    public class ApplyDuplicateDocumentDataModel : RequestBaseModel
    { 
        public int ID { get; set; }
        public int DocumentID { get; set; }
        public int? SemesterID { get; set; }
        public int DepartmentTypeID { get; set; } 
        public int FeeAmount { get; set; }
        public int ServiceID { get; set; }
        public int StudentID { get; set; }
        public int CourseTypeID { get; set; }
        public int ApplicationID { get; set; }
        public int UniqueServiceID { get; set; }
        public string MobileNo { get; set; }
        public string StudentName { get; set; }
        public string ApplicationNo { get; set; }
        public int? InstituteID { get; set; }
        public int createdBy { get; set; }
        public int modifyBy { get; set; }
        public bool IsPayment { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; } 

        public int? ConfigurationTypeID { get; set; }

        public int RequestEndTerm { get; set; }

        public int FianancialYearID { get; set; }

        public int? FeesTypeID { get; set; }
        public int? FeeID { get; set; }



    }


    public class DuplicateDocumentSearchModel
    {
        public int ID { get; set; }
        public int DocumentID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int FeeAmount { get; set; }
        public int ServiceID { get; set; }
        public int StudentID { get; set; }
        public int CourseTypeID { get; set; }
        public int ApplicationID { get; set; }
        public int UniqueServiceID { get; set; }
        public string MobileNo { get; set; }
        public string StudentName { get; set; }
        public string ApplicationNo { get; set; }
        public int? InstituteID { get; set; }
        public int createdBy { get; set; }
        public int modifyBy { get; set; }
        public bool IsPayment { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public int? ConfigurationTypeID { get; set; }

        public string Name { get; set; }
        public string action { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string SortOrder { get; set; }
        public string SortColumn { get; set; }
        public int Eng_NonEng { get; set; }
        public int? EndTermID { get; set; }
    }



    public class DuplicateDoc_Action
    {
        public int ID { get; set; }
        public int DocumentID { get; set; }

        public int StudentID { get; set; }
        public string Action { get; set; }

        public int ActionBy { get; set; }
        public string ActionRemarks { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }

        public int SemesterId { get; set; }

        public int EndTermID { get; set; }
        public int RequestEndTerm { get; set; }

        public int FianancialYearID { get; set; }
        public int CourseTypeID { get; set; }


    }



}
