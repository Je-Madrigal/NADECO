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
    public class SHARELINE_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();


        public List<Shares> Filter(string No_)
        {
            List<Shares> shrs = new List<Shares>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_TRN_SHARELINE";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No_;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Shares shr = new Shares();
                            shr.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                            string x = rdr["TransactionDate"].ToString();
                            if (x != "" && x != null)
                            {
                                shr.TransactionDate = DateTime.Parse(rdr["TransactionDate"].ToString());
                            }
                            shr.No_ = rdr["No_"].ToString();
                            shr.MemberName = rdr["MemberName"].ToString();
                            shr.RefNo = rdr["RefNo"].ToString();
                            shr.Name = rdr["Name"].ToString();
                            shr.Type = rdr["Type"].ToString();
                            shr.ShareCapital = Convert.ToDouble(rdr["ShareCapital"].ToString());
                            shr.Status = rdr["Status"].ToString();

                            shrs.Add(shr);
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

            return shrs;
        }



    }
}
