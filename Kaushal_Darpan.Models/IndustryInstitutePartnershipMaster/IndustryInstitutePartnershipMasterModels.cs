namespace Kaushal_Darpan.Models.CompanyMaster
{
    public class IndustryInstitutePartnershipMasterModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int DepartmentID { get; set; }
        public string CompanyPhoto { get; set; }
        public string Dis_CompanyName { get; set; }
        public string CompanyDocument { get; set; }
        public string Dis_DocName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }

        public int ModifyBy { get; set; }

        public string? IPAddress { get; set; }

        public int EventTypeID { get; set; }
    }


    public class IndustryInstitutePartnershipMaster_Action
    {
        public int ID { get; set; }
        public int ActionBy { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public string Action { get; set; }
        public string? ActionRemarks { get; set; }

        public int EventTypeID { get; set; }
    }

    public class IndustryTrainingMaster
    {
        public int IndustryTRID { get; set; }
        public int IndustryID { get; set; }
        public int EventTypeID { get; set; }
        public DateTime EventDate { get; set; }
        public int SemesterID { get; set; }
        public string Purpose { get; set; }   
        public int TradeID { get; set; }   
        public int DepartmentID { get; set; }   
        public bool ActiveStatus { get; set; }   
        public bool DeleteStatus { get; set; }   
        public int CreatedBy { get; set; }   
        public int ModifyBy { get; set; }   
        public string IPAddress { get; set; }   


    }
    public class IndustryTrainingSearch
    {
        public int IndustryTRID { get; set; }
        public int IndustryID { get; set; }
        public int EventTypeID { get; set; }
        public String EventDate { get; set; }
        public int SemesterID { get; set; }
        public int TradeID { get; set; }
        public int DepartmentID { get; set; }
       
    }




}
