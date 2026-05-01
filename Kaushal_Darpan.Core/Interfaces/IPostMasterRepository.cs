using Kaushal_Darpan.Models.PostMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public  interface IPostMasterRepository
    {
        Task<DataTable> GetAllData(PostMasterModel body);

        Task<PostMasterModel> GetById(int postID);

        Task<bool> SaveData(PostMasterModel request);

        Task<bool> DeleteDataById(PostMasterModel request);
    }
}
