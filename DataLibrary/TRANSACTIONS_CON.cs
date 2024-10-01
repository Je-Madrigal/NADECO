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
    public class TRANSACTIONS_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();


        public List<Transactions> FetchAll()
        {

            List<Transactions> trans = new List<Transactions>();

            con = new SqlConnection(conString);


            string spName = "SP_TRANSACTIONS";
            cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sqlDa = new SqlDataAdapter(constring.cmd);

            cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "FetchAll";
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Transactions trc = new Transactions();
                trc.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);

                trc.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                trc.No_ = rdr["No_"].ToString();
                trc.MemberName = rdr["MemberName"].ToString();
                trc.Type = rdr["Type"].ToString();
                trc.MembershipFee = Convert.ToInt32(rdr["MembershipFee"].ToString());
                trc.Shares = Convert.ToInt32(rdr["ShareCapital"].ToString());
                trc.TimeDeposits = Convert.ToInt32(rdr["TimeDeposits"].ToString());
                trc.Loan = Convert.ToInt32(rdr["Loan"].ToString());
                trc.others = rdr["others"].ToString();
                trc.othersFee = Convert.ToInt32(rdr["Loan"].ToString());
                trc.Total = Convert.ToInt32(rdr["Total"].ToString());
                trc.Month = rdr["Month"].ToString();
                trc.Year = rdr["Year"].ToString();
                trc.Status = rdr["Status"].ToString();
                trc.Posted = rdr["Posted"].ToString();

                trans.Add(trc);
            }
            //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            return trans;

        }

        public Transactions Filter(string MemberNo)
        {
            Transactions trc = null;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_TRANSACTIONS";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = MemberNo;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            trc = new Transactions();
                            trc.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);

                            trc.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                            trc.No_ = rdr["No_"].ToString();
                            trc.MemberName = rdr["MemberName"].ToString();
                            trc.Type = rdr["Type"].ToString();
                            trc.MembershipFee = Convert.ToInt32(rdr["MembershipFee"].ToString());
                            trc.Shares = Convert.ToInt32(rdr["ShareCapital"].ToString());
                            trc.TimeDeposits = Convert.ToInt32(rdr["TimeDeposits"].ToString());
                            trc.Loan = Convert.ToInt32(rdr["Loan"].ToString());
                            trc.others = rdr["others"].ToString();
                            trc.othersFee = Convert.ToInt32(rdr["Loan"].ToString());
                            trc.Total = Convert.ToInt32(rdr["Total"].ToString());
                            trc.Month = rdr["Month"].ToString();
                            trc.Year = rdr["Year"].ToString();
                            trc.Status = rdr["Status"].ToString();
                            trc.Posted = rdr["Posted"].ToString();

                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return trc; // Return the single MembershipFee object
        }
        public string[] GetSeriesNo(string SeriesName)
        {
            List<string> setSeries = new List<string>(); // Using List<string> instead of string[] for dynamic size
            con = new SqlConnection(conString);
            con.Open();
            string spName = "SP_SERIESNOS";
            using (cmd = new SqlCommand(spName, con))
            {
                sqlDa = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SQLExec", "FndAllprCompBU"); // Corrected parameter type
                cmd.Parameters.AddWithValue("@SeriesName", SeriesName); // Corrected parameter type

                // Removed extra con.Open() here

                SqlDataReader DR1 = cmd.ExecuteReader();
                if (DR1 != null)
                {
                    if (DR1.Read())
                    {
                        setSeries.Add(DR1["TransactionCode"].ToString() + DateTime.Now.Year + '-' + DR1["SeriesNo"].ToString());
                        setSeries.Add(DR1["MaxLen"].ToString());
                    }
                }
            }

            return setSeries.ToArray(); // Convert List<string> to string[]
        }
        public void InsertRegistration(Transactions trc)
        {
            string[] data = GetSeriesNo("REGISTRATION");
            trc.No_ = data[0].ToString(); //LC24-0001

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_TRANSACTIONS";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";

                    cmd.Parameters.AddWithValue("@MaxLen", SqlDbType.Int).Value = data[1].ToString();
                    cmd.Parameters.AddWithValue("@SeriesName", SqlDbType.Int).Value = "REGISTRATION";
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = trc.EntryBy;
                    cmd.Parameters.AddWithValue("@TransactionDate", SqlDbType.Int).Value = trc.TransactionDate;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = trc.No_;
                    cmd.Parameters.AddWithValue("@Total", SqlDbType.Int).Value = trc.Total;
                    cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = trc.MemberNo;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = "REGISTRATION";
                    cmd.Parameters.AddWithValue("@MembershipFee", SqlDbType.Int).Value = trc.MembershipFee;
                    cmd.Parameters.AddWithValue("@Subscription", SqlDbType.Int).Value = trc.Subscription;
                    cmd.Parameters.AddWithValue("@ShareCapital", SqlDbType.Int).Value = trc.Shares;
                    cmd.Parameters.AddWithValue("@TimeDeposits", SqlDbType.Int).Value = trc.TimeDeposits;
                    cmd.Parameters.AddWithValue("@Loan", SqlDbType.Int).Value = trc.Loan;
                    cmd.Parameters.AddWithValue("@Others", SqlDbType.Int).Value = trc.others;
                    cmd.Parameters.AddWithValue("@OthersFee", SqlDbType.Int).Value = trc.othersFee;
            
                    cmd.Parameters.AddWithValue("@Month", SqlDbType.Int).Value = trc.Month;
                    cmd.Parameters.AddWithValue("@Year", SqlDbType.Int).Value = trc.Year;
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = "POSTED";
                    cmd.Parameters.AddWithValue("@Posted", SqlDbType.Int).Value = "Y";
 
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //IUnsert SPNAME
        }

        public void InsertSharesAndContri(Transactions trc)
        {
            string[] data = GetSeriesNo("SHARES");

            trc.No_ = data[0].ToString(); //LC24-0001

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_TRN_SHAREHEADER";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";

                    cmd.Parameters.AddWithValue("@MaxLen", SqlDbType.Int).Value = data[1].ToString();
                    cmd.Parameters.AddWithValue("@SeriesName", SqlDbType.Int).Value = "SHARES";
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = trc.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = trc.No_;
                    cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = trc.MemberNo;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = "SHARES";

                    cmd.Parameters.AddWithValue("@Subscription", SqlDbType.Int).Value = trc.Subscription;
                    cmd.Parameters.AddWithValue("@ShareCapital", SqlDbType.Int).Value = trc.Shares;
           
                    cmd.Parameters.AddWithValue("@Month", SqlDbType.Int).Value = trc.Month;
                    cmd.Parameters.AddWithValue("@Year", SqlDbType.Int).Value = trc.Year;
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = "POSTED";
                    cmd.Parameters.AddWithValue("@Posted", SqlDbType.Int).Value = "Y";

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //IUnsert SPNAME
        }

        public void InsertLoans(Transactions trc)
        {
            string[] data = GetSeriesNo("LOANS");

            trc.No_ = data[0].ToString(); //LC24-0001

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_TRN_LOANHEADER";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";

                    cmd.Parameters.AddWithValue("@MaxLen", SqlDbType.Int).Value = data[1].ToString();
                    cmd.Parameters.AddWithValue("@SeriesName", SqlDbType.Int).Value = "LOANS";
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = trc.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = trc.No_;
                    cmd.Parameters.AddWithValue("@Total", SqlDbType.Int).Value = trc.Total;
                    cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = trc.MemberNo;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = "LOANS";
                    cmd.Parameters.AddWithValue("@Loan", SqlDbType.Int).Value = trc.Loan;
 
 
                    cmd.Parameters.AddWithValue("@Month", SqlDbType.Int).Value = trc.Month;
                    cmd.Parameters.AddWithValue("@Year", SqlDbType.Int).Value = trc.Year;
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = "POSTED";
                    cmd.Parameters.AddWithValue("@Posted", SqlDbType.Int).Value = "Y";

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //IUnsert SPNAME
        }

        public void InsertTimeDeposits(Transactions trc)
        {
            string[] data = GetSeriesNo("DEPOSITS");

            trc.No_ = data[0].ToString(); //LC24-0001

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_TRN_TIMEDEPOSIT";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";

                    cmd.Parameters.AddWithValue("@MaxLen", SqlDbType.Int).Value = data[1].ToString();
                    cmd.Parameters.AddWithValue("@SeriesName", SqlDbType.Int).Value = "DEPOSITS";
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = trc.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = trc.No_;
                    cmd.Parameters.AddWithValue("@Total", SqlDbType.Int).Value = trc.Total;

                    cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = trc.MemberNo;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = "DEPOSITS";
 
                    cmd.Parameters.AddWithValue("@TimeDeposits", SqlDbType.Int).Value = trc.TimeDeposits;
 
                    cmd.Parameters.AddWithValue("@Month", SqlDbType.Int).Value = trc.Month;
                    cmd.Parameters.AddWithValue("@Year", SqlDbType.Int).Value = trc.Year;
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = "POSTED";
                    cmd.Parameters.AddWithValue("@Posted", SqlDbType.Int).Value = "Y";

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //IUnsert SPNAME
        }

        public void InsertOtherTransactions(Transactions trc)
        {
            string[] data = GetSeriesNo("OTHERS");

            trc.No_ = data[0].ToString(); //LC24-0001

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_TRANSACTIONS";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";

                    cmd.Parameters.AddWithValue("@MaxLen", SqlDbType.Int).Value = data[1].ToString();
                    cmd.Parameters.AddWithValue("@SeriesName", SqlDbType.Int).Value = "OTHERS";
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = trc.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = trc.No_;
                    cmd.Parameters.AddWithValue("@Total", SqlDbType.Int).Value = trc.Total;
                    cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = trc.MemberNo;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = "OTHERS";
 
 
                    cmd.Parameters.AddWithValue("@Others", SqlDbType.Int).Value = trc.others;
                    cmd.Parameters.AddWithValue("@OthersFee", SqlDbType.Int).Value = trc.othersFee;

                    cmd.Parameters.AddWithValue("@Month", SqlDbType.Int).Value = trc.Month;
                    cmd.Parameters.AddWithValue("@Year", SqlDbType.Int).Value = trc.Year;
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = "POSTED";
                    cmd.Parameters.AddWithValue("@Posted", SqlDbType.Int).Value = "Y";

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //IUnsert SPNAME
        }

      



    }
}
