using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.ModelView
{
    public class GENERALSETTINGSVIEW
    {
        public MembershipFee membership { get; set; }
        public List<MembershipFee> membershipLists { get; set; }
    }
}
