using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaushal_Darpan.Models.RevaluationDataModel
{
    
    public class ITIStudentRevaluationDataModel
    {

        public string DOB { get; set; }
        public Int64? RollNo { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeIDs { get; set; }

    }


     //iti student reval request details

    public class ITIRevalRequestStudentDetailsModel
    {

        public string DOB { get; set; }
        public string? RollNo { get; set; }
        public string? Name { get; set; }

        public int? ActionBy { get; set; }

        public int? RevalReqID { get; set; }

        public int DepartmentID { get; set; }
        public int CourseTypeIDs { get; set; }

        public List<StudentOptionItem>? StudentOptionList { get; set; } = new List<StudentOptionItem>();
        public string action { get; set; }
    }




    public class StudentOptionItem
    {
        public int RequestSubjectID { get; set; }
        public int StudentExamPaperMarksID { get; set; }
        public string UploadedCopy { get; set; } 

        public string Remarks { get; set; }

        //public int? RevalRequestID { get; set; }

    }


    //model to update enroll response in bulk 
    public class UpdateEnrollResponseBulkExcelModel
    {
        //[Column("State Reg Number")]
        [DisplayName("State Reg Number")]
        public string? StateRegNumber { get; set; }
    }

}
