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
    public class MEMBER_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();


        public List<Members> FetchAll()
        {

            List<Members> members_ = new List<Members>();

            con = new SqlConnection(conString);


            string spName = "SP_MEMBERS";
            cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sqlDa = new SqlDataAdapter(constring.cmd);

            cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "FetchAll";
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Members members = new Members();
                members.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);

                members.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                members.No_ = rdr["No_"].ToString();
                members.FullName = rdr["FullName"].ToString();
                members.EmployeeStatus = rdr["EmployeeStatus"].ToString();
                members.TINNo = rdr["TINNo"].ToString();
                members.Barangay = rdr["Barangay"].ToString();
                members.City = rdr["City"].ToString();
                members.Province = rdr["Province"].ToString();
                members.FullAddress = rdr["FullAddress"].ToString();
                members.RegistrationStatus = rdr["RegistrationStatus"].ToString();
                members.AccountStatus = rdr["AccountStatus"].ToString();
                members_.Add(members);
            }
            //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            return members_;

        }

        public List<Members> Filter(string No_)
        {
            List<Members> members_ = new List<Members>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "SP_MEMBERS";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No_;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Members members = new Members();
                            members.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                            members.EntryDate = DateTime.Parse(rdr["EntryDate"].ToString());
                            members.No_ = rdr["No_"].ToString();
                            members.UserName = rdr["UserName"].ToString();
                            members.MemberId = rdr["MemberId"].ToString();
                            members.FirstName = rdr["FirstName"].ToString();
                            members.MiddleName = rdr["MiddleName"].ToString();
                            members.LastName = rdr["LastName"].ToString();
                            members.UserEmail = rdr["EmailAddress"].ToString();
                            members.PhoneNumber = rdr["PhoneNo"].ToString();
                            members.BirthDate = DateTime.Parse(rdr["BirthDate"].ToString());
                            members.Nationality = rdr["Nationality"].ToString();
                            members.Gender = rdr["Gender"].ToString();
                            members.CivilStatus = rdr["CivilStatus"].ToString();
                            members.Province = rdr["Province"].ToString();
                            members.City = rdr["City"].ToString();
                            members.CityNo = rdr["CityNo"].ToString();
                            members.Barangay = rdr["Barangay"].ToString();
                            members.BarangayNo = rdr["BarangayNo"].ToString();
                            members.ZipCode = rdr["ZipCode"].ToString();
                            members.EmployeeStatus = rdr["EmployeeStatus"].ToString();
                            members.MembershipDate = DateTime.Parse(rdr["MembershipDate"].ToString());
                            members.EmployeeNo = rdr["EmployeeNo"].ToString();
                            members.Employer = rdr["Employer"].ToString();
                            members.TINNo = rdr["TINNo"].ToString();
                            members.Occupation = rdr["Occupation"].ToString();
                            members.Education = rdr["Education"].ToString();
                            members.Salary = Convert.ToInt32(rdr["Salary"]);
                            members.OtherSource = rdr["OtherSource"].ToString();
                            members.Dependents = Convert.ToInt32(rdr["Dependents"]);
                            members.DependentName = rdr["DependentName"].ToString();
                            members.DependentAge = Convert.ToInt32(rdr["DependentAge"]);
                            members.Relationship = rdr["Relationship"].ToString();
                            members.AccountStatus = rdr["AccountStatus"].ToString();
                            // TCT DETAILS


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

     

        public void InsertMember(Members members)
        {
            string[] data = GetSeriesNo();

            members.No_ = data[0].ToString(); //LC24-0001

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_MEMBERS";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "New";

                    cmd.Parameters.AddWithValue("@MaxLen", SqlDbType.Int).Value = data[1].ToString();
                    cmd.Parameters.AddWithValue("@SeriesName", SqlDbType.Int).Value = "MEMBER";
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = members.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = members.No_;
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.Int).Value = members.UserName;
                    cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = members.MemberId;
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.Int).Value = members.FirstName;
                    cmd.Parameters.AddWithValue("@MiddleName", SqlDbType.Int).Value = members.MiddleName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.Int).Value = members.LastName;
                    cmd.Parameters.AddWithValue("@EmailAddress", SqlDbType.Int).Value = members.UserEmail;
                    cmd.Parameters.AddWithValue("@PhoneNo", SqlDbType.Int).Value = members.PhoneNumber;
                    cmd.Parameters.AddWithValue("@BirthDate", SqlDbType.Int).Value = members.BirthDate;
                    cmd.Parameters.AddWithValue("@Nationality", SqlDbType.Int).Value = members.Nationality;
                    cmd.Parameters.AddWithValue("@Gender", SqlDbType.Int).Value = members.Gender;
                    cmd.Parameters.AddWithValue("@CivilStatus", SqlDbType.Int).Value = members.CivilStatus;
                    cmd.Parameters.AddWithValue("@Province", SqlDbType.Int).Value = members.Province;
                    cmd.Parameters.AddWithValue("@City", SqlDbType.Int).Value = members.City;
                    cmd.Parameters.AddWithValue("@CityNo", SqlDbType.Int).Value = members.CityNo;
                    cmd.Parameters.AddWithValue("@Barangay", SqlDbType.Int).Value = members.Barangay;
                    cmd.Parameters.AddWithValue("@BarangayNo", SqlDbType.Int).Value = members.BarangayNo; 
                    cmd.Parameters.AddWithValue("@ZipCode", SqlDbType.Int).Value = members.ZipCode;
                    cmd.Parameters.AddWithValue("@EmployeeStatus", SqlDbType.Int).Value = members.EmployeeStatus;
                    cmd.Parameters.AddWithValue("@MembershipDate", SqlDbType.Int).Value = members.MembershipDate;
                    cmd.Parameters.AddWithValue("@EmployeeNo", SqlDbType.Int).Value = members.EmployeeNo;
                    cmd.Parameters.AddWithValue("@Employer", SqlDbType.Int).Value = members.Employer;
                    cmd.Parameters.AddWithValue("@TINNo", SqlDbType.Int).Value = members.TINNo;
                    cmd.Parameters.AddWithValue("@Occupation", SqlDbType.Int).Value = members.Occupation;
                    cmd.Parameters.AddWithValue("@Education", SqlDbType.Int).Value = members.Education;
                    cmd.Parameters.AddWithValue("@Salary", SqlDbType.Int).Value = members.Salary;
                    cmd.Parameters.AddWithValue("@OtherSource", SqlDbType.Int).Value = members.OtherSource;
                    cmd.Parameters.AddWithValue("@Dependents", SqlDbType.Int).Value = members.Dependents;
                    cmd.Parameters.AddWithValue("@DependentName", SqlDbType.Int).Value = members.DependentName;
                    cmd.Parameters.AddWithValue("@DependentAge", SqlDbType.Int).Value = members.DependentAge;
                    cmd.Parameters.AddWithValue("@Relationship", SqlDbType.Int).Value = members.Relationship;
 
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            //IUnsert SPNAME
        }

        public void UpdateMember(Members members)
        {
 
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                string spName = "SP_MEMBERS";
                using (cmd = new SqlCommand(spName, con))
                {
                    sqlDa = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "Update";
 
                    cmd.Parameters.AddWithValue("@EntryBy", SqlDbType.Int).Value = members.EntryBy;
                    cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = members.No_;
                    cmd.Parameters.AddWithValue("@UserName", SqlDbType.Int).Value = members.UserName;
                    cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = members.MemberId;
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.Int).Value = members.FirstName;
                    cmd.Parameters.AddWithValue("@MiddleName", SqlDbType.Int).Value = members.MiddleName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.Int).Value = members.LastName;
                    cmd.Parameters.AddWithValue("@EmailAddress", SqlDbType.Int).Value = members.UserEmail;
                    cmd.Parameters.AddWithValue("@PhoneNo", SqlDbType.Int).Value = members.PhoneNumber;
                    cmd.Parameters.AddWithValue("@BirthDate", SqlDbType.Int).Value = members.BirthDate;
                    cmd.Parameters.AddWithValue("@Nationality", SqlDbType.Int).Value = members.Nationality;
                    cmd.Parameters.AddWithValue("@Gender", SqlDbType.Int).Value = members.Gender;
                    cmd.Parameters.AddWithValue("@CivilStatus", SqlDbType.Int).Value = members.CivilStatus;
                    cmd.Parameters.AddWithValue("@Province", SqlDbType.Int).Value = members.Province;
                    cmd.Parameters.AddWithValue("@City", SqlDbType.Int).Value = members.City;
                    cmd.Parameters.AddWithValue("@CityNo", SqlDbType.Int).Value = members.CityNo;
                    cmd.Parameters.AddWithValue("@Barangay", SqlDbType.Int).Value = members.Barangay;
                    cmd.Parameters.AddWithValue("@BarangayNo", SqlDbType.Int).Value = members.BarangayNo;
                    cmd.Parameters.AddWithValue("@ZipCode", SqlDbType.Int).Value = members.ZipCode;
                    cmd.Parameters.AddWithValue("@EmployeeStatus", SqlDbType.Int).Value = members.EmployeeStatus;
                    cmd.Parameters.AddWithValue("@MembershipDate", SqlDbType.Int).Value = members.MembershipDate;
                    cmd.Parameters.AddWithValue("@EmployeeNo", SqlDbType.Int).Value = members.EmployeeNo;
                    cmd.Parameters.AddWithValue("@Employer", SqlDbType.Int).Value = members.Employer;
                    cmd.Parameters.AddWithValue("@TINNo", SqlDbType.Int).Value = members.TINNo;
                    cmd.Parameters.AddWithValue("@Occupation", SqlDbType.Int).Value = members.Occupation;
                    cmd.Parameters.AddWithValue("@Education", SqlDbType.Int).Value = members.Education;
                    cmd.Parameters.AddWithValue("@Salary", SqlDbType.Int).Value = members.Salary;
                    cmd.Parameters.AddWithValue("@OtherSource", SqlDbType.Int).Value = members.OtherSource;
                    cmd.Parameters.AddWithValue("@Dependents", SqlDbType.Int).Value = members.Dependents;
                    cmd.Parameters.AddWithValue("@DependentName", SqlDbType.Int).Value = members.DependentName;
                    cmd.Parameters.AddWithValue("@DependentAge", SqlDbType.Int).Value = members.DependentAge;
                    cmd.Parameters.AddWithValue("@Relationship", SqlDbType.Int).Value = members.Relationship;

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
                cmd.Parameters.AddWithValue("@SeriesName", "MEMBER"); // Corrected parameter type

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


        
    }
}
