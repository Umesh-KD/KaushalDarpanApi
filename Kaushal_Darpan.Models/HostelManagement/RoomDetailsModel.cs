using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.HostelManagement
{
    public class RoomDetailsModel
    {
        public int HSRoomID { get; set; }
        public int HostelID { get; set; }
        public int RoomTypeID { get; set; }
        public int RoomNo { get; set; }
        public int StudyTableFacilities { get; set; }
        public int AttachedBathFacilities { get; set; }
        public int FanFacilities { get; set; }
        public int CoolingFacilities { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }
    
    public class RoomExcelDetailsModel
    {
        public int SrNo { get; set; }
        public int HSRoomID { get; set; }
        public int HostelID { get; set; }
        public string RoomType { get; set; }
        public int RoomNo { get; set; }
        public string StudyTableFacilities { get; set; }
        public string FanFacilities { get; set; }
        public string CoolingFacilities { get; set; }
        public string AttachedBathFacilities { get; set; }
        public string IPAddress { get; set; }
    }


    public class StatusChangeModelNew
    {
        public int PK_ID { get; set; }
        public int ModifyBy { get; set; }
    }



}
