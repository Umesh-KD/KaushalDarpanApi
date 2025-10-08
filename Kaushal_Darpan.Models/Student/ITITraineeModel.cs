using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Student
{
    public class ITITraineeUploadModel
    {
        public string? StateRegNumber { get; set; }
        public string? TraineeName { get; set; }
        public string? UIDNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Category { get; set; }
        public string? FatherGuardianName { get; set; }
        public string? MotherName { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailID { get; set; }
        public string? Session { get; set; }
        public string? AdmissionDate { get; set; }
        public string? HighestQualification { get; set; }
        public string? Trade { get; set; }
        public string? Shift { get; set; }
        public string? Unit { get; set; }
        public string? IsTraineeDualMode { get; set; }
        public string? MISITICode { get; set; }
        public string? PersonwithDisability { get; set; }
        public string? PWDcategory { get; set; }
        public string? EconomicWeakerSection { get; set; }
        public string? TraineeType { get; set; }
    }
    public class TraineeUploadResponse
    {
        public int? SucessRec { get; set; }
        public int? ErrorRec { get; set; }
        public int? TotalRec { get; set; }
        public List<ResponseData>? ResponseData { get; set; }
    }

    public class ResponseData
    {
        public string? StateRegNumber { get; set; }
        public string? TraineeName { get; set; }
        public string? ErrorDescription { get; set; }
        public string? RecordStatus { get; set; }
    }
}
