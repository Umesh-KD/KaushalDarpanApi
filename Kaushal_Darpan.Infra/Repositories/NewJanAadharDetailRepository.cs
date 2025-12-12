using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.SSOUserDetails;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentDetailUpdate;
using Kaushal_Darpan.Models.StudentMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class NewJanAadharDetailRepository : INewJanAadharDetailRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;

        public NewJanAadharDetailRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "NewJanAadharDetailRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


    
    }

}
