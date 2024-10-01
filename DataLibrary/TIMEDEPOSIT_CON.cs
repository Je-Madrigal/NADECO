using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Models;
using DataLibrary;
using System.Xml.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Reflection;

namespace DataLibrary
{
    public class TIMEDEPOSIT_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();


        public List<TimeDeposits> FetchAll()
        {

            List<TimeDeposits> tdepo = new List<TimeDeposits>();

            con = new SqlConnection(conString);
 
            string spName = "SP_TRN_TIMEDEPOSIT";
            cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sqlDa = new SqlDataAdapter(constring.cmd);

            cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "FetchAll";
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                TimeDeposits tdh = new TimeDeposits();
                tdh.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                string x = rdr["TransactionDate"].ToString();
                if (x != "" && x != null)
                {
                    tdh.TransactionDate = DateTime.Parse(rdr["TransactionDate"].ToString());
                }
                tdh.No_ = rdr["No_"].ToString();
                tdh.MemberName = rdr["MemberName"].ToString();
                tdh.Deposit = Convert.ToDouble(rdr["Deposit"].ToString());
                tdh.Interest = Convert.ToDouble(rdr["Interest"].ToString());
                tdh.Total = Convert.ToDouble(rdr["Total"].ToString());
                tdh.Term = rdr["Term"].ToString();


                tdepo.Add(tdh);
            }
            //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            return tdepo;

        }
    }
}
