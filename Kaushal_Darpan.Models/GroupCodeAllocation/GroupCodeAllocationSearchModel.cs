namespace Kaushal_Darpan.Models.GroupCodeAllocation
{
    public class GroupCodeAllocationSearchModel : RequestBaseModel
    {
        public int SemesterId { get; set; }
        public int PartitionSize { get; set; }
        public int CommonSubjectYesNo { get; set; }
    }

}
