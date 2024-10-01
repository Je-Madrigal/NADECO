﻿using System;
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
    public class SHAREHEADER_CON
    {
        string conString = ConfigurationManager.ConnectionStrings["NADECO"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sqlDa = new SqlDataAdapter();


        public List<Shares> FetchAll()
        {

            List<Shares> shares_ = new List<Shares>();

            con = new SqlConnection(conString);


            string spName = "SP_TRN_SHAREHEADER";
            cmd = new SqlCommand(spName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sqlDa = new SqlDataAdapter(constring.cmd);

            cmd.Parameters.AddWithValue("@SQLExec", SqlDbType.Int).Value = "FetchAll";
            con.Open();

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Shares shr = new Shares();
                shr.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                string x = rdr["TransactionDate"].ToString();
                if(x != "" && x != null)
                 {
                    shr.TransactionDate = DateTime.Parse(rdr["TransactionDate"].ToString());
                }
                shr.No_ = rdr["No_"].ToString();
                shr.MemberName = rdr["MemberName"].ToString();
                shr.Subscription = Convert.ToDouble(rdr["Subscription"].ToString());
                shr.ShareCapital = Convert.ToDouble(rdr["ShareCapital"].ToString());
                shr.Balance = Convert.ToDouble(rdr["Balance"].ToString());
                shr.Status = rdr["Status"].ToString();

                shares_.Add(shr);
            }
            //SqlDataAdapter sqlDa = new SqlDataAdapter(cmd);
            return shares_;

        }

        public Shares Filter(string No_)
        {
            Shares shr = null;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    string spName = "TRN_SHARESHEADER";

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SQLExec", "Filter");
                        cmd.Parameters.AddWithValue("@No_", SqlDbType.Int).Value = No_;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            shr = new Shares();
                            shr.Timestamp = Convert.ToInt32(rdr["TimeStamp"]);
                            string x = rdr["TransactionDate"].ToString();
                            if (x != "" && x != null)
                            {
                                shr.TransactionDate = DateTime.Parse(rdr["TransactionDate"].ToString());
                            }
                            shr.No_ = rdr["No_"].ToString();
                            shr.MemberName = rdr["MemberName"].ToString();
                            shr.Subscription = Convert.ToDouble(rdr["Subscription"].ToString());
                            shr.ShareCapital = Convert.ToDouble(rdr["ShareCapital"].ToString());
                            shr.Balance = Convert.ToDouble(rdr["Balance"].ToString());
                            shr.Status = rdr["Status"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return shr; // Return the single MembershipFee object
        }


    }
}
