namespace Kaushal_Darpan.Models.ITIPapperSetter
{
    
    public class AddprofessorList
    {   
        public string DistrictName { get; set; }
        public int DistrictId { get; set; }
        public string ProfessorName { get; set; }
        public int ProfessorId { get; set; }
    }

    public class ITIPapperSetterModel
    {
        public int PKID { get; set; }
        public int yearTrade { get; set; }
        public int TwoYearTradeID { get; set; }
        public int TradeSchemeId { get; set; }
        public int ExamType { get; set; }
        public int TradeID { get; set; }
        public int SubjectId { get; set; }
        public string PapperSubmitionLastDate { get; set; }
        public string PaperCodeName { get; set; }
        public string NumberofQuestion { get; set; }
        public string Remark { get; set; }
        public string GuideLinesDocumentFile { get; set; }
        public int DistrictID { get; set; }
        public int ProfessorId { get; set; }
        public int Createdby { get; set; }
        public int Roleid { get; set; }
        public int Endtermid { get; set; }
        public int FYID { get; set; }
        public List<AddprofessorList> PapperSetterListModel { get; set; }
        public string ActionName { get; set; }
    }


    public class PaperSetterAssginListModel
    {
        public int id { get; set; }
        public string yearTrade { get; set; }
        public string Tradename { get; set; }
        public string SubjectName { get; set; }
        public string papperSubmitionLastDate { get; set; }
        public string PapperCode_Name { get; set; } 
        public string Remark { get; set; } 
        public string UploadGuidelinePath { get; set; } 
        public int IsAutoSelectComplete { get; set; }
        public string FinalAutoSelectedPaperFile { get; set; } = "";
        public List<AssignprofessorList> AssignprofessorListModel { get; set; }  
    }
public class AssignprofessorList
{
    public string DistrictName { get; set; }
    public string ProfessorName { get; set; }
    public string RemarkByProfessor { get; set; } 
    public string UploadedPaperDocument { get; set; } 
    public int ProfessorId { get; set; }
    public int DistrictId { get; set; }
    public bool ishighlight { get; set; } = false;
    public int IsAutoSelect { get; set; }
    public string Status { get; set; }
    public int StatusID { get; set; }


    }
}
