using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StudentRequestsModel
{
    public class RoomAllotmentModel
    {
        public int AllotSeatId { get; set; }
        public int HostelID { get; set; }
        public int ReqId { get; set; } 
        public int RoomTypeId { get; set; } 
        public int RoomNoId { get; set; } 
        public string HostelFeesReciept { get; set; } 
        public string Dis_HostelFeesReciept { get; set; } 
        public int Relation { get; set; } 
        public string ContactPersonName { get; set; } 
        public string MobileNo { get; set; } 
        public int EndTermId { get; set; } 
        public string? Remark { get; set; } 
        public bool ActiveStatus { get; set; } 
        public bool DeleteStatus { get; set; } 
        public DateTime? RTS { get; set; } 
        public int CreatedBy { get; set; } 
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; } 
        public string? IPAddress { get; set; } 
        public int? CourseTypeID { get; set; } 
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int FessAmount { get; set; }
        public int? AllotmentStatus { get; set; }
    }

    
}
