using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using DataLibrary;
using DataLibrary.Models;
using System.Configuration;
using System.Collections;


namespace NADECO.Features
{
    /// <summary>
    /// Summary description for LookUpWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LookUpWebService : System.Web.Services.WebService
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void FetchBarangay(string Param, string SPName,string CityNo)
        {
            List<Barangay> brgy = new List<Barangay>();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();


                    using (SqlCommand cmd = new SqlCommand(SPName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@No_", Param);
                        cmd.Parameters.AddWithValue("@CityNo", CityNo);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Barangay bgy = new Barangay();
                                bgy.No_ = rdr["No_"]?.ToString();
                                bgy.Name = rdr["Name"]?.ToString();
                                bgy.Municipality = rdr["CITY"]?.ToString();
                                bgy.Province = rdr["Province"]?.ToString();
                                bgy.ZipCode = rdr["ZipCode"]?.ToString();
                                brgy.Add(bgy);
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

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(brgy));
        }


        [WebMethod]
        public void FetchCity(string Param, string SPName)
        {
            List<Municipality> mci = new List<Municipality>();
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();


                    using (SqlCommand cmd = new SqlCommand(SPName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@No_", Param);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Municipality nc = new Municipality();
                                nc.No_ = rdr["No_"]?.ToString();
                                nc.Name = rdr["Name"]?.ToString();

                                nc.Province = rdr["Province"]?.ToString();
                                nc.ZipCode = rdr["ZipCode"]?.ToString();
                                mci.Add(nc);
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

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(mci));
        }
    }
}
