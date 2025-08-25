using Kaushal_Darpan.Models.StudentMeritIInfoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;

namespace Kaushal_Darpan.Models.ITIStudentMeritInfo
{
    public  class ITIStudentMeritInfoModel
    {
        public int ApplicationID { get; set; }
        public int MeritId { get; set; }
        public int AllotmentMasterId { get; set; }
        public string ApplicationNo { get; set; }
        public string? StudentName { get; set; }
        public string? Gender { get; set; }
        public string? categoryName { get; set; }
        public string? DOB { get; set; }
        public int Class { get; set; }
        public decimal TenthPer { get; set; }
        public decimal MaxMarks { get; set; }
        public decimal MarksObt { get; set; }
        public int CategoryId { get; set; }

        public int CourseTypeID { get; set; }
        public int DepartmentID { get; set; }
        public string? PrefentialCategory { get; set; }
        public int MeritNo { get; set; }
        public int CategoryMeritNo { get; set; }
        public bool IsPH { get; set; }
        public bool IsKM { get; set; }
        public bool IsWID { get; set; }
        public bool IsRajDOMICILE { get; set; }
        public bool IsSingleMotherDependent { get; set; }
        public bool IsTSP { get; set; }
        public bool IsExServicemen { get; set; }
        public bool ExServicemenId { get; set; }
        public Boolean IsEdit { get; set; }
        public bool IsApply { get; set; }
        public int Action { get; set; }
        public List<ItiQualificationViewDetails> QualificationViewDetails { get; set; }
        //public List<RecheckDocumentModel> RecheckDocumentModel { get; set; }
    }
}
