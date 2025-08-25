using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DocumentDetails
{
    public class DeleteDocumentDetailsModel
    {
        public int ApplicationID { get; set; }
        public int AcademicYear { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public string? FolderName { get; set; }
        public string? FileName { get; set; }
    }
}
