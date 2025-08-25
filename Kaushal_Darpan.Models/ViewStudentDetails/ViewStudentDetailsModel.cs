using Kaushal_Darpan.Models.DocumentDetails;
using System.Data;

namespace Kaushal_Darpan.Models.ViewStudentDetailsModel
{
    public class ViewStudentDetailsModel
    {
        public DataTable ViewStudentDetails { get; set; }
        public DataTable Student_QualificationDetails { get; set; }
        public List<DocumentDetailsModel> documentDetails { get; set; }

    }
}
