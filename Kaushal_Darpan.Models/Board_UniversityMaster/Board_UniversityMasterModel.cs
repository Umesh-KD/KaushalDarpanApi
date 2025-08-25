namespace Kaushal_Darpan.Models.BTERIMCAllocationModel
{
    public class Board_UniversityMasterModel
    {
        public int? ID { get; set; } 
        public string? Name { get; set; } 
        public string? Code { get; set; } 
        public string? Remark { get; set; } 
        public bool? ActiveStatus { get; set; } 
        public bool? DeleteStatus { get; set; } 
        public string? RTS { get; set; } 
        public int? CreatedBy { get; set; } 
        public int? ModifyBy { get; set; } 
        public string? ModifyDate { get; set; } 
        public string? IPAddress { get; set; } 
    }
    public class Board_UniversityMasterSearchModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int ModifyBy { get; set; }
    }
}
