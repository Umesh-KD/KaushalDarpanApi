using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.GenerateAdmitCard
{
    public class DownloadDataPagingListModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPages { get; set; }
        public int PageFrom { get; set; }
        public int PageTo { get; set; }
        public string? StudentIds { get; set; }
        public string StudentExamIDs { get; set; } = "";
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public string EndTermName { get; set; } = "";
        public int Eng_NonEng { get; set; }
        public int UserID { get; set; }


    }
}
