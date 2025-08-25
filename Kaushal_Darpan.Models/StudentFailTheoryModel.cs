using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StudentFailTheoryModel
{
    public class StudentFailTheoryModel
    {
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string SPNNO { get; set; }
        public string StudentName { get; set; }
        public decimal ObtainedTheory { get; set; }
        public bool IsPresentTheory { get; set; }
        public bool IsTheory { get; set; }
        public bool ActiveStatus { get; set; }
        public string Grade { get; set; }
        public decimal MaxTheory { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string RollNo { get; set; }
        public string IPAddress { get; set; }
        public DateTime RTS { get; set; }
    }
}
