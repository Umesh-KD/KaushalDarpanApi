using System.Data;

namespace Kaushal_Darpan.Models.ViewStudentDetailsModel
{
    public class ViewStudentDetailsRequestModel : RequestBaseModel
    {
        public int StudentID { get; set; }
        public int StudentFilterStatusId { get; set; }
        public int ApplicationID { get; set; }
        public int StudentExamID { get; set; } = 0;

    }
}
