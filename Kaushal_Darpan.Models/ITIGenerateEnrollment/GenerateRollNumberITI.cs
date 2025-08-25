using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIGenerateEnrollment
{
    public class GenerateRollNumberITI
    {
        public int? InstituteID { get; set; }
        public int? StreamID { get; set; }
        public int? SemesterID { get; set; }
        public int? VerifierStatus { get; set; }
        public int? DistrictID { get; set; }
        public int? ShowAll {  get; set; }
        public int? Status { get; set; }
        public int? StudentTypeID { get; set; }
    }

    public class ITIGenerateRollSearchModel: RequestBaseModel
    {
        public int? InstituteID { get; set; }
        public int? StreamID { get; set; }
        public int? SemesterID { get; set; }
        public int? VerifierStatus { get; set; }
        public int? DistrictID { get; set; }
        public int? ShowAll { get; set; }
        public int? Status { get; set; }
        public int? StudentTypeID { get; set; }
    }
}
