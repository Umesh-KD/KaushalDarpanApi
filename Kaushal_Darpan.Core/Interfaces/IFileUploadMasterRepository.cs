

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IFileUploadMasterRepository
    {
        Task<string> GetKeyOfOpenPage(string key);
    }
}
