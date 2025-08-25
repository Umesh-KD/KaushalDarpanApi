using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Kaushal_Darpan.Models.Test;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITestRepository
    {
        Task<DataTable> Dummy_SendMessage(string Type);
        Task<int> Test_SaveHindiData(List<Test_SaveHindiData> model);
    }
}
