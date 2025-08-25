namespace Kaushal_Darpan.Models.MenuMaster
{
    public class MenuMasterModel
    {
            public int MenuId { get; set; }
            public int? ParentId { get; set; }
            public string MenuNameEn { get; set; }
            public string MenuNameHi { get; set; }
            public string Remark { get; set; }
            public string? MenuUrl { get; set; }
            public string? MenuActionId { get; set; }
            public bool ActiveStatus { get; set; }
            public bool DeleteStatus { get; set; }
            public int? Priority { get; set; }
            public int CreatedBy { get; set; }
            public DateTime ModifyDate { get; set; }
            public DateTime? RTS { get; set; }
            public int? ModifyBy { get; set; }
            public int userId { get; set; }

   

        public string? CreatedIP { get; set; }
        public string? UpdatedIP { get; set; }
        public string? MenuIcon { get; set; }
      
            public int? MenuLevel { get; set; }
        public int DepartmentID { get; set; }
    }
    public class MenuMasterSerchModel
    {
        public int MenuId { get; set; }
        public string? MenuNameEn { get; set; }

        public int DepartmentID { get; set; }
    }

}


    