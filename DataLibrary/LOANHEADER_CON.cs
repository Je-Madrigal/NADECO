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
    public class LOANHEADER_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();

        public List<Loans> FetchAll()
        {
            List<Loans> Loans = new List<Loans>();
            con = new SqlConnection(conString);
            string spName = "SP_TRN_LOANHEADER";
            cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sqlDa = new SqlDataAdapter(constring.cmd);

            cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "FetchAll";
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Loans trc = new Loans();
                trc.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);

                trc.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                trc.No_ = rdr["No_"].ToString();
                trc.MemberName = rdr["MemberName"].ToString();
                trc.Type = rdr["Type"].ToString();
                trc.RefNo = rdr["RefNo"].ToString();
                trc.No_ = rdr["No_"].ToString();
                trc.Name = rdr["Name"].ToString();
               
                trc.LoanGranted = Convert.ToDouble(rdr["LoanGranted"].ToString());
                trc.DateGranted = DateTime.Parse(rdr["DateGranted"].ToString());
                trc.AmortizationPeriod = rdr["AmortizationPeriod"].ToString();
                trc.Amortization = Convert.ToDouble(rdr["Amortization"].ToString());
                trc.Balance = Convert.ToDouble(rdr["Balance"].ToString());
                trc.Total = Convert.ToDouble(rdr["Total"].ToString());
                trc.Month = rdr["Month"].ToString();
                trc.Year = rdr["Year"].ToString();
                trc.Status = rdr["Status"].ToString();
                trc.Posted = rdr["Posted"].ToString();

                Loans.Add(trc);
            }
            //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            return Loans;

        }


        public Loans Filter(string MemberNo)
        {
            Loans trc = null;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_TRN_LOANHEADER";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = MemberNo;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            trc = new Loans();
                            trc.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);

                            trc.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                            trc.No_ = rdr["No_"].ToString();
                            trc.MemberName = rdr["MemberName"].ToString();
                            trc.Type = rdr["Type"].ToString();
                            trc.RefNo = rdr["RefNo"].ToString();
                            trc.No_ = rdr["No_"].ToString();
                            trc.Name = rdr["Name"].ToString();

                            trc.LoanGranted = Convert.ToDouble(rdr["LoanGranted"].ToString());
                            trc.DateGranted = DateTime.Parse(rdr["DateGranted"].ToString());
                            trc.AmortizationPeriod = rdr["AmortizationPeriod"].ToString();
                            trc.Amortization = Convert.ToDouble(rdr["Amortization"].ToString());
                            trc.Balance = Convert.ToDouble(rdr["Balance"].ToString());
                            trc.Total = Convert.ToDouble(rdr["Total"].ToString());
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


        public void Delete(string No_)
        {
            con = new SqlConnection(conString);
            con.Open();
            string spName = "SP_TRN_LOANHEADER";
            using (cmd = new SqlCommand(spName, con))
            {
                sqlDa = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "Delete";
                cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No_;
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}
