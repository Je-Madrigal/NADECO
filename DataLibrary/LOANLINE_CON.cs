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
    public class LOANLINE_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();


        public List<Loans> FetchAll()
        {
            List<Loans> Loans = new List<Loans>();
            con = new SqlConnection(conString);
            string spName = "SP_TRN_LOANLINE";
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
                trc.TransactionDate = DateTime.Parse(rdr["TransactionDate"].ToString());
                trc.No_ = rdr["No_"].ToString();
                trc.MemberNo = rdr["MemberNo"].ToString();
                trc.Type = rdr["Type"].ToString();
                trc.RefNo = rdr["RefNo"].ToString();
                trc.Name = rdr["Name"].ToString();
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

        public Loans Filter(string No)
        {
            Loans trc = null;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_TRN_LOANLINE";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            trc = new Loans();
                            trc.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);

                            trc.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                            trc.TransactionDate = DateTime.Parse(rdr["TransactionDate"].ToString());
                            trc.No_ = rdr["No_"].ToString();
                            trc.MemberNo = rdr["MemberNo"].ToString();
                            trc.Type = rdr["Type"].ToString();
                            trc.RefNo = rdr["RefNo"].ToString();
                            trc.Name = rdr["Name"].ToString();
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

        public void InsertLoanLine(Loans trc)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_TRN_LOANLINE";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";
  
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = trc.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = trc.No_;
                    cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = trc.MemberNo;
                    cmd.Parameters.AddWithValue("@Name", SqlDbType.Int).Value = trc.Name;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.Int).Value = trc.Type;
                    cmd.Parameters.AddWithValue("@Amortization", SqlDbType.Int).Value = trc.Amortization;
                    cmd.Parameters.AddWithValue("@Balance ", SqlDbType.Int).Value = trc.Balance;
                    cmd.Parameters.AddWithValue("@Total", SqlDbType.Int).Value = trc.Total;
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
