using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CollegeMaster
{

    public class ITICollegeProfileDataModel
    {
        public int InstituteID { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? website { get; set; }
        public string SecretaryName { get; set; }
        public string SecretaryMobile { get; set; }
        public string? ItiEmail { get; set; }
        public string? Email { get; set; }
        public string? Ssoid { get; set; }
        public string? PrincipalName { get; set; }
        public string? PrincipalMobile { get; set; }
        public string? Dis_Name { get; set; }
        public string? Logo { get; set; }
        public int InstitutionCategoryId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int SubDivisionId { get; set; }
        public int TehsilId { get; set; }
        public int UrbanRural { get; set; }
        public int ParliamentId { get; set; }
        public int AssemblyId { get; set; }
        public string Pincode { get; set; }
        public string? PlotNo { get; set; }
        public string? Address { get; set; }
        public string? Street { get; set; }
        public string? Area { get; set; }
        public string? Landmark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int FinancialYearId { get; set; }
        public int EndTermID { get; set; }
        public int AdministrativeId { get; set; }
        public string? NagarNigamName { get; set; }
        public string? NagarPalikaName { get; set; }
        public string? NagarParishadName { get; set; }
        public string? CantonmentBoardName { get; set; }
        public string? Remark { get; set; }
        public string? StatusName { get; set; }
        public int CityID { get; set; }
        public int Ward { get; set; }
        public int VillageId { get; set; }
        public int PanchayatsamityId { get; set; }
        public int GrampanchayatId { get; set; }
        public string UpdatedBy { get; set; }

    }
}
