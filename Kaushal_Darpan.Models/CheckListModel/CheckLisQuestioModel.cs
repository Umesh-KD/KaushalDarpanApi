using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CheckListModel
{
    public class CheckListQuestionModel
    {
        public int QuestionID { get; set; }
        public int TypeID { get; set; }
        public int QuestionType { get; set; }
        public string Remarks { get; set; } = "";
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string Questions { get; set; } = "";

    }


    public class CheckListAnswerModel
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }

        public string AnswerText { get; set; } = "";

        public string Remarks { get; set; } = "";

        public bool ActiveStatus { get; set; }

        public bool DeleteStatus { get; set; }

        public DateTime RTS { get; set; }

        public int CreatedBy { get; set; }

        public int ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }
    }

    public class CheckListTypeModel
    {
       

        public int TypeId { get; set; }
        public string Remarks { get; set; } = "";
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int UserID { get; set; }

    }

    public class CheckListSearchModel
    {
        public int TypeID { get; set; }
        public int DepartmentID { get; set; }
        public int ID { get; set; }
        public string Remarks { get; set; }
        public int UserID { get; set; }
    }

    public class ChecklistAnswerRequest
    {
        public List<CheckQuestion_WithAnswer> Data { get; set; }
    }
    public class CheckQuestion_WithAnswer
    {
        public int QuestionID { get; set; }
        public int TypeID { get; set; }
        public string Questions { get; set; } = string.Empty;
        public string AnswerText { get; set; } = string.Empty;
        public int QuestionType { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public int TeamID { get; set; }
       
    }
}
