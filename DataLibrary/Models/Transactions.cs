using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Transactions
    {
        public int Timestamp { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryBy { get; set; }

        public string MemberNo { get; set; }

        public string MemberName { get; set; }
        public string No_ { get; set; }
        public string Type { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string others { get; set; }

        public double othersFee { get; set; }
        public double Loan { get; set; }
        public double MembershipFee { get; set; }
        public double Subscription { get; set; }
        public double Shares { get; set; }
        public double TimeDeposits { get; set; }

        public double Total { get; set; }
        public string Status { get; set; }

        public string Posted { get; set; }
    }
}
