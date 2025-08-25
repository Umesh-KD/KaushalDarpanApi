using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.LeaveMaster
{
    public class LeaveMaster:RequestBaseModel
    {
        public int StaffLeaveID {  get; set; }
        public int LeaveID {  get; set; }
        public int StaffID {  get; set; }
        public string? From_Date {  get; set; }
        public string? To_Date {  get; set; }
        public string? Remark {  get; set; }
        public string? Action {  get; set; }
        public int ActionBy {  get; set; }
        public string? ActionDate {  get; set; }
        public string? ActionRemark {  get; set; }
        public string? SSOID {  get; set; }
        public int ModifyBy {  get; set; }
        public int InstituteID {  get; set; }
        public int TotalDays { get; set; }

    }
    public class LeaveMasterSearchModel:RequestBaseModel
    {
        public string Name { get; set; }
        public int RoleID { get; set; }
        public int InstituteID {  set; get; }
        public string? Status { get; set; }
        public string? SSOID { get; set; }
    }
}
