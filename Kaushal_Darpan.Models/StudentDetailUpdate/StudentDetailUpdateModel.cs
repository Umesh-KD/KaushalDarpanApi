using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.StudentMaster;

namespace Kaushal_Darpan.Models.StudentDetailUpdate
{
    public class StudentDetailUpdateModel
    {
        public string EnrollmentNo { get; set; }
        public string ApplicationNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string Document { get; set; }
        public string Dis_Document { get; set; }
        public int CreatedBy { get; set; }
        public int StudentID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng {  get; set; }
    }

}
