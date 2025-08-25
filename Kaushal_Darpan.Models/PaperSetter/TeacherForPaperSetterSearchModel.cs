using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PaperSetter
{
    public class TeacherForPaperSetterSearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int GroupCodeID { get; set; }
        public int CommonSubjectYesNo { get; set; } = 1;
        public int CommonSubjectID { get; set; }
        public int? CommonSubjectDetailID { get; set; }
        public int PaperSetterStatus { get; set; }
        public int? StaffID { get; set; }
    }
}
