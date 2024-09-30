using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Loans
    {
        public int Timestamp { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public string EntryBy { get; set; }
        public string MemberNo { get; set; }
        public string MemberName { get; set; }

        public string RefNo { get; set; }
        public string No_ { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Amortization { get; set; }
        public string AmortizationPeriod { get; set; }
        public double LoanGranted { get; set; }
        public DateTime DateGranted { get; set; }
        public double Balance { get; set; }
        public double Total { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Status { get; set; }
        public string Posted { get; set; }

    }
}
