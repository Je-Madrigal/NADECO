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
using System.Security.Principal;

namespace DataLibrary
{
    public class MEMBERSHIPFEE_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();


        public List<MembershipFee> FetchAll()
        {

            List<MembershipFee> members_ = new List<MembershipFee>();

            con = new SqlConnection(conString);


            string spName = "SP_LKT_MEMBERSHIPFEE";
            cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sqlDa = new SqlDataAdapter(constring.cmd);

            cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "FetchAll";
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                MembershipFee members = new MembershipFee();
                members.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);

                members.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                members.No_ = rdr["No_"].ToString();
                members.Name = rdr["Name"].ToString();
                members.Fee = Convert.ToInt32(rdr["Fee"]);
                members.Year = rdr["Year"].ToString();
                members.Status = rdr["Status"].ToString();
              
                members_.Add(members);
            }
            //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            return members_;
        }


        public List<MembershipFee> Filter()
        {
            List<MembershipFee> members_ = new List<MembershipFee>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_LKT_MEMBERSHIPFEE";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        //cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No_;
                        cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = "ACTIVE";

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            MembershipFee members = new MembershipFee();

                            members.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                            members.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                            members.No_ = rdr["No_"].ToString();
                        
                            members.Name = rdr["Name"].ToString();
                            members.Fee = Convert.ToInt32(rdr["Fee"]);
                            members.Year = rdr["Year"].ToString();
                            members.Status = rdr["Status"].ToString();

                            members_.Add(members);
                        }
                        //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);

                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return members_;
        }
  
    }
}
