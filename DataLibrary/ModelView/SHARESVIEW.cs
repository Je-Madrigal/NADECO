using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.ModelView
{
    public class SHARESVIEW
    {
        public Shares shareHeader { get; set; }
        public List<Shares> shareLine { get; set; }
 
    }
}
