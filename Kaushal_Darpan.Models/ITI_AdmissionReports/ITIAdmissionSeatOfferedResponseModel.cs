using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_AdmissionReports
{
    public class ITIAdmissionSeatOfferedResponseModel
    {
        public int Id { get; set; }
        public int FinancialYearId { get; set; }
        public string Session { get; set; }

        public int Govt_ITI_Count { get; set; }
        public int Govt_Seat_Offered { get; set; }
        public int Govt_Admission { get; set; }
        public decimal Govt_Percentage { get; set; }

        public int Pvt_ITI_Count { get; set; }
        public int Pvt_Seat_Offered { get; set; }
        public int Pvt_Admission { get; set; }
        public decimal Pvt_Percentage { get; set; }

        public int Total_ITI_Count { get; set; }
        public int Total_Seat_Offered { get; set; }
        public int Total_Admission { get; set; }
        public decimal Total_Percentage { get; set; }
        public string? TableHeading { get; set; }
    }
}
