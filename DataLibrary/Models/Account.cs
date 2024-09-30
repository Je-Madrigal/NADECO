using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace DataLibrary.Models
{
    public class Account
    {
        public int Timestamp { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryBy { get; set; }
        public string No_ { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Usertype { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string MemberId { get; set; }
        ////public string MemberNo { get; set; }
        public string AccountStatus { get; set; }
    }
}
