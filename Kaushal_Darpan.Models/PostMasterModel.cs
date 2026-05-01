using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PostMaster
{
    public class PostMasterModel
    {
        public int PostID { get; set; }

        public string? PostName { get; set; }

        public int ServiceID { get; set; }   

        public bool ActiveStatus { get; set; } = true;

        public int? CreatedBy { get; set; }

        public int? ModifyBy { get; set; }

        public string? IPAddress { get; set; }

        public int UserID { get; set; } 
        public string? PostTypeName { get; set; }

        public string? Status { get; set; }
    }
}