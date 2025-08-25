namespace Kaushal_Darpan.Models.ReservationRosterModel
{
    public class ReservationRosterMasterModel
    {
        public int Reservation_Id { get; set; }
        public int CategoryId { get; set; }
        public string ReservationPr { get; set; }
        public bool IsHorizontalCateogory { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearID { get; set; }
        public string Remarks { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string IPAddress { get; set; }


    }
    public class ReservationRosterSearchModel
    {
        public int Reservation_Id { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public int AcademicYearID { get; set; }

    }
}
