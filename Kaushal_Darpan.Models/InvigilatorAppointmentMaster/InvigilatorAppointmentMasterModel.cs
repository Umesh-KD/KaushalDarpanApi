using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.InvigilatorAppointmentMaster
{
    public class InvigilatorAppointmentMasterModel
    {
        public int InvigilatorAppointmentID { get; set; }
        public int CourseID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public string RollNumberFrom { get; set; }
        public string RollNumberTo { get; set; }
        public string RoomNumber { get; set; }
        public int InstituteID { get; set; }
        public string Date { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public List<InvigilatorSSOIDModel> InvigilatorSSOID{ get; set; }
    }

    public class InvigilatorSSOIDModel
    {
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public bool IsPrimary { get; set; }
        public string SSOID { get; set; }
        public string? Name { get; set; }
 
    }
}
