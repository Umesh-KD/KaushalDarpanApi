namespace Kaushal_Darpan.Models.ViewPlacedStudents
{
    public class ViewPlacedStudents
    {
        public int key { get; set; }
        public int Pk_Id { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
    }

    public class PlacementReportSearchData
    {
        public int EndTermID { get; set; }
        public int CourseType { get; set; }
        public int DepartmentID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CollegeName { get; set; }
        public string BranchName { get; set; }
    }
}
