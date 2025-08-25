using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DateConfiguration
{
    public class DateConfigurationIdModel:RequestBaseModel
    {
        public int DateConfigID { get; set; }

    }
    public class DateConfigurationModel: DateConfigurationIdModel
    {
        public int? TypeID { get; set; } = 0;
        public int? ConfigurationID { get; set; } = 0;
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public int? SemesterID { get; set; } = 0;
        public int? StreamID { get; set; } = 0;
        public int? CourseTypeID { get; set; } = 0;
        public int? CourseSubTypeID { get; set; } = 0;
        public int ModifyBy { get; set; }
        public string SSOID { get; set; } = string.Empty;
       
    }

    public class listDateConfigurationModel 
    {
        public int? TypeID { get; set; } = 0;
        public int? ConfigurationID { get; set; } = 0;
        public DateTime? From_Date { get; set; }
        public DateTime? To_Date { get; set; }
        public int? SemesterID { get; set; } = 0;
        public int? StreamID { get; set; } = 0;
        public int? CourseTypeID { get; set; } = 0;
        public int? CourseSubTypeID { get; set; } = 0;
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DateConfigID { get; set; }
        public int? DepartmentID { get; set; } = 0;
        public int? Eng_NonEng { get; set; } = 0;// course type
        public int? EndTermID { get; set; } = 0;
        public int? TermPart { get; set; } = 0;
        public int? FinancialYearID { get; set; } = 0;

    }
}
