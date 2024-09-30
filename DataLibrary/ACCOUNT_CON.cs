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
    public class ACCOUNT_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();

        public void UpdateAccountStatus(Account account)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_ACCOUNT";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "UpdateAccountStatus";

                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = account.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = account.No_;

                    //cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = account.MemberNo;
                    cmd.Parameters.AddWithValue("@AccountStatus", SqlDbType.Int).Value = account.AccountStatus;
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.Int).Value = account.Password;
                    cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = account.MemberId;
            

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<Account> Filter(string No_)
        {
            List<Account> acct = new List<Account>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_ACCOUNT";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "GetAccount");
                        cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No_;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Account account = new Account();
                            account.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                            account.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                            account.Name = rdr["Name"].ToString();
                            account.Password = rdr["Password"].ToString();
                            account.Username = rdr["Username"].ToString();
                            account.Status = rdr["Status"].ToString();
                            account.Usertype = rdr["UserType"].ToString();
                            account.Department = rdr["Department"].ToString();
                            account.AccountStatus = rdr["AccountStatus"].ToString();
                            account.MemberId = rdr["MemberId"].ToString();

 
                            // TCT DETAILS


                            acct.Add(account);
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

            return acct;
        }
        public void Delete(string No_)
        {
            con = new SqlConnection(conString);
            con.Open();
            string spName = "SP_ACCOUNT";
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


        public void Logout(string No_)
        {
            con = new SqlConnection(conString);
            con.Open();
            string spName = "SP_ACCOUNT";
            using (cmd = new SqlCommand(spName, con))
            {
                sqlDa = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "Logout";
                cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No_;
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void Update(Account acct)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {

                con.Open();
                string spName = "SP_ACCOUNT";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "Update";

                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = acct.No_;
                    cmd.Parameters.AddWithValue("@Name", SqlDbType.Int).Value = acct.Name;
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.Int).Value = acct.Username;
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.Int).Value = acct.Password;
                    cmd.Parameters.AddWithValue("@Department", SqlDbType.Int).Value = acct.Department;
                    cmd.Parameters.AddWithValue("@UserType", SqlDbType.Int).Value = acct.Usertype;
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = acct.Status;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        public void InsertAccount(Account acct)
        {
            string[] data = GetSeriesNo();

            acct.No_ = data[0].ToString(); //LC24-0001

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_ACCOUNT";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";

                    cmd.Parameters.AddWithValue("@MaxLen", SqlDbType.Int).Value = data[1].ToString();
                    cmd.Parameters.AddWithValue("@SeriesName", SqlDbType.Int).Value = "ACCNO";
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = acct.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = acct.No_;
                    cmd.Parameters.AddWithValue("@Name", SqlDbType.Int).Value = acct.Name;
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.Int).Value = acct.Username;
                    cmd.Parameters.AddWithValue("@Department", SqlDbType.Int).Value = acct.Department;
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.Int).Value = acct.Password;
                    cmd.Parameters.AddWithValue("@UserType", SqlDbType.Int).Value = acct.Usertype;
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Int).Value = acct.Status;

                    cmd.Parameters.AddWithValue("@MemberId", SqlDbType.Int).Value = acct.MemberId;
                    cmd.Parameters.AddWithValue("@AccountStatus", SqlDbType.Int).Value = acct.AccountStatus;
                    //cmd.Parameters.AddWithValue("@MemberNo", SqlDbType.Int).Value = acct.MemberNo;

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //IUnsert SPNAME
        }
        public string[] GetSeriesNo()
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
                cmd.Parameters.AddWithValue("@SeriesName", "ACCNO"); // Corrected parameter type

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
        public List<Account> FetchAll
        {
            get
            {
                List<Account> acct = new List<Account>();

                con = new SqlConnection(conString);


                string spName = "SP_ACCOUNT";
                cmd = new SqlCommand(spName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter sqlDa = new SqlDataAdapter(constring.cmd);

                cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "FetchAll";
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Account account = new Account();
                    account.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                    account.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                    account.No_ = rdr["No_"].ToString();
                    account.Name = rdr["Name"].ToString();
                    account.Password = rdr["Password"].ToString();

                    account.Status = rdr["Status"].ToString();
                    account.Username = rdr["Username"].ToString();

                    account.Usertype = rdr["UserType"].ToString();
                    account.Department = rdr["Department"].ToString();

                    account.AccountStatus = rdr["AccountStatus"].ToString();
                    account.MemberId = rdr["MemberId"].ToString();

                    // TCT DETAILS


                    acct.Add(account);
                }
                //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
                return acct;
            }
        }
        public Account ValidateUser(string username, string password, string IPAddress, string UserPCName, string ComName)
        {
            Account validAccount = null;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_ACCOUNT";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@IPaddress", IPAddress);
                        cmd.Parameters.AddWithValue("@UserPCName", UserPCName);
                        cmd.Parameters.AddWithValue("@ComName", ComName);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                validAccount = new Account
                                {
                                    Timestamp = rdr["TimeStamp"] != DBNull.Value ? Convert.ToInt32(rdr["TimeStamp"]) : 0,
                                    Username = rdr["Username"]?.ToString(),
                                    Name = rdr["Name"]?.ToString(),
                                    No_ = rdr["No_"]?.ToString(),
                                    Password = rdr["Password"]?.ToString()
                                    // Map other properties as needed
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return validAccount;
        }




    }
}
