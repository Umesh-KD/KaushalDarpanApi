using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.SurveyPerformModel
{
   
    public class SurveyPerformModel
    {
        public int SurveyPerformID { get; set; }
        public string? NameofEstablishment { get; set; }
        public string? NameofDesignation { get; set; }
        public string? HeadofEstablishmentAddress { get; set; }
        public string? NatureOfBusiness { get; set; }
        public int TotalNoPersonEmployeed { get; set; }
        public string? BasicTraningFacility { get; set; }
        public string? DistributionofWorker { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

        public List<WorkerDesignationTradeModel>? OtherITIWorkerDesignationTrade { get; set; }
        public List<WorkerDetailsOfExistingApprenticeshipModel>? OtherITIApprWorkerDetailsOfExistingApprenticeship { get; set; }
        public List<WorkerDetalisOffacilitiesModel>? OtherITIApprWorkerDetalisOffacilities { get; set; }

    }

    public class WorkerDesignationTradeModel
    {
        public int DesignationTradeID { get; set; }
        public int SurveyPerformID { get; set; }
        public int NCONumberWorkers { get; set; }
        public int LessSkilledWorker { get; set; }
        public int FullySkilledWorker { get; set; }
        public int TotalWorker { get; set; }
        public string? Remark { get; set; }

    }

    public class WorkerDetailsOfExistingApprenticeshipModel
    {
        public int DetailsOfExistingApprenticeshipID { get; set; }
        public int SurveyPerformID { get; set; }
        public string? TradeTraning { get; set; }
        public string? DurationofLastSurvey { get; set; }
        public int NumberOfSeatsLocated { get; set; }
        public int NumberActuallyUndergoingtraning { get; set; }

    }

    
    public class WorkerDetalisOffacilitiesModel
    {
        public int DetalisOffacilitiesTradeID { get; set; }
        public string? TradeName { get; set; }
        public int SurveyPerformID { get; set; }
        public int DurationOfTraning { get; set; }
        public int NumberOfSeatsSanctioned { get; set; }
        public string? NAUT_Deginate { get; set; }
        public string? NAUT_Optional { get; set; }
        public string? NAUT_NATS { get; set; }
        public string? NAUT_Fresher { get; set; }
        
    }



    public class GetSurveyPerformModel
    {
        public int SurveyPerformID { get; set; }
        public string? NameofEstablishment { get; set; }
        public string? NameofDesignation { get; set; }
        public string? HeadofEstablishmentAddress { get; set; }
        public string? NatureOfBusiness { get; set; }
        public int TotalNoPersonEmployeed { get; set; }
        public string? BasicTraningFacility { get; set; }
        public string? DistributionofWorker { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }

    }




}
