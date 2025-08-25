using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIBUDGET
{
    public class CollegeBudgetAllotedModel
    {
        public long DistributedID { get; set; }
        public int CollegeID { get; set; }
        public int FinYearID { get; set; }
        public int? DistributedType { get; set; }
        public decimal? DistributedAmount { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string? ActionType { get; set; }
        public string? Remarks { get; set; }
    }
}
