namespace Kaushal_Darpan.Models.UploadFileWithPathData
{
    public class UploadFileDataModel
    {
        public string? FileName { get; set; }
    }
    public class UploadFileWithPathDataModel
    {
        public int ApplicationID { get; set; }
        public int AcademicYear { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? Dis_FileName { get; set; }
        public string? FolderName { get; set; }
        public string? OldFileName { get; set; }
        public int ?FinYearID { get; set; }
        public int ?CreatedBy { get; set; }

    }

    public class UploadOriginalFileWithPathDataModel
    {
        public int ApplicationID { get; set; }
        public int DocumentMasterID { get; set; }
        public int AcademicYear { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? Dis_FileName { get; set; }
        public string? FolderName { get; set; }
        public string? OldFileName { get; set; }

    }
}
