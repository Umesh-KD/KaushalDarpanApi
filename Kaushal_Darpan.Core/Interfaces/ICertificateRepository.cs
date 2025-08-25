using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.CertificateDownload;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.Student;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICertificateRepository
    {
        Task<DataTable> GetAllMigrationCertificateData(CertificateSearchModel filterModel);
        Task<DataTable> GetAllProvisionalCertificateData(CertificateSearchModel filterModel);
    }
}
