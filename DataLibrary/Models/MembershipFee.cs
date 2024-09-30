using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class MembershipFee
    {
        public int Timestamp { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryBy { get; set; }
        public string No_ { get; set; }
        public string Name { get; set; }
        public int Fee { get; set; }
        public string Year { get; set; }
        public string Status { get; set; }
    }
}
