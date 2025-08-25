using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Examiners
{
    public class RoomsMasterDataModel
    {
        public int? RoomMasterID { get; set; }   
        public int RoomNumber { get; set; }    
        public int TotalRows { get; set; }   
        public int TotalColumns { get; set; }  
        public int TotalSeats { get; set; }       
        public int InstituteID { get; set; } 
        public DateTime RTS { get; set; } 
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
    }

}
