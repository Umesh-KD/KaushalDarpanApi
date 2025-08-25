namespace Kaushal_Darpan.Models.ItiCompanyMaster
{
    public class ItiCompanyMasterModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int DepartmentID { get; set; }
        public string CompanyPhoto { get; set; }

        public string CompanyTypeId { get; set; }

        public string Dis_CompanyName { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }

        public int ModifyBy { get; set; }

        public string? IPAddress { get; set; }
    }


    public class ItiCompanyMaster_Action
    {
        public int ID { get; set; }
        public int ActionBy { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public string Action { get; set; }
        public string? ActionRemarks { get; set; }
    }



    public class ItiReportDataModel
    {
        public int CollegeID { get; set; }
        public string? CollegeName { get; set; }
        public string? Loksabha { get; set; }
        public string? Vidhansabha { get; set; }
        public string? LandAvailable { get; set; }
        public string? PanchayatDis { get; set; }
        public string? SanctionOrderNo { get; set; }
        public string? SanctionOrderDate { get; set; }
        public string? TradeOrderNo { get; set; }
        public string? AdministrativeOrderNo { get; set; }
        public string? TradeOrderDate { get; set; }
        public string? AdministrativeOrderDate { get; set; }
        public string? ApproachRoad { get; set; }
        public string? InternalRoad { get; set; }
        public string? WaterSupply { get; set; }
        public string? Harvesting { get; set; }
        public string? ElectPhase { get; set; }
        public string? ElectPhaserequired { get; set; }
        public string? ElectConnection { get; set; }
        public string? IsSolarPanel { get; set; }
        public string? PanelCapacity { get; set; }
        public string? IsBoundaryWall { get; set; }
        public string? BuildShortage { get; set; }
        public string? IsHostel { get; set; }
        public string? HostelUtilized { get; set; }
        public string? NoOfTree { get; set; }
        public string? Remarks { get; set; }
        public string? FrontPhoto { get; set; }
        public string? SidePhoto { get; set; }
        public string? InteriorPhoto { get; set; }
        public string? SanctionOrderCopy { get; set; }
        public string? TradeCopy { get; set; }
        public string? AdministrativeCopy { get; set; }
        public string? ConstructionAgency { get; set; }
        public string? PDName { get; set; }
        public string? ContractorName { get; set; }
        public string? PDMobile { get; set; }
        public string? ContractorMobile { get; set; }
        public string? IsDispute { get; set; }
        public string? FinancialSanction { get; set; }
        public string? FinancialCopy { get; set; }
        public string? PercentCivilWork { get; set; }
        public string? PercentCivilDate { get; set; }
        public string? ContractLoad { get; set; }
        public string? IsPurposeHall { get; set; }
        public string? IsMainITI { get; set; }
        public string? IsBuildingTaken { get; set; }
        public string? TakenOverDate { get; set; }
        public string? IsOperatingOwn { get; set; }
        public string? ShilanyasDate { get; set; }
        public string? LokarpanDate { get; set; }
        public string? LokarpanName { get; set; }
        public string? LokarpanPost { get; set; }
        public string? AllotmentLetter { get; set; }
        public string? BuildingPlanCopy { get; set; }
        public string? DomeViewCopy { get; set; }
        public string? ShilanyasPost { get; set; }
        public string? ShilanyasName { get; set; }
        public int IsNewCollege { get; set; }
        public int ModifyBy { get; set; }
        public int Esttablishment_Year { get; set; }

    }



}
