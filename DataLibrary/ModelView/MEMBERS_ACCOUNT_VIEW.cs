using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.ModelView
{
    public class MEMBERS_ACCOUNT_VIEW
    {
        public Members members_ { get; set; }
        public List<Members> membersLists { get; set; }
        public Account acct { get; set; }
        public List<Account> acctLists { get; set; }
    }
}
