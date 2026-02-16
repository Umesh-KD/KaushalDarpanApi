using System;
using System.Collections.Generic;
using System.Globalization;
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
        public decimal TotalDays { get; set; }
        public decimal RemainingLeave { get; set; }
        public bool IsHeadQuarter { get; set; }
        public int LeaveTypeID { get; set; }

        public string? txtIsHeadQuarterAddress { get; set; }
        public string? txtIsHeadQuarterMobileNo { get; set; }
        public string? DisUploadDoc { get; set; }
        public string? UploadDoc { get; set; }
        public int? StaffTypeID { get; set; }

    }
    public class LeaveMasterSearchModel:RequestBaseModel
    {
        public string Name { get; set; }
        public int InstituteID {  set; get; }
        public string? Status { get; set; }
        public string? SSOID { get; set; }

        public int? StaffID { get; set; }
        public string? From_Date { get; set; }
        public string? To_Date { get; set; }
        public int? StaffTypeID { get; set; }
        public string? Action { get; set; }

        public int? LeaveID { get; set; }
        public int? RoleID { get; set; }


    }

    public class CreditLeaveModel : RequestBaseModel
    {
        public int StaffID { get; set; }
        public int InstituteID { set; get; }
        public int ModifyBy { get; set; }
        public int StaffTypeID { get; set; }


    }

}
