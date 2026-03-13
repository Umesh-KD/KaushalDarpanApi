using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class FileUploadMasterRepository : IFileUploadMasterRepository

    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public FileUploadMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "FileUploadMasterRepository";
        }
        
        public async Task<string> GetKeyOfOpenPage(string key)
        {
            _actionName = "GetKeyOfOpenPage(string key)";
            string keyVal = "";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"select PageKey from M_PageManagement where PageName='{key}'";

                    _sqlQuery = command.GetSqlExecutableQuery();
                    dataTable = await command.FillAsync_DataTable();
                }
                // class
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    keyVal = Convert.ToString(dataTable.Rows[0][0] ?? "");
                }
            }
            catch (Exception ex)
            {
            }
            return keyVal;
        }
    }
}

