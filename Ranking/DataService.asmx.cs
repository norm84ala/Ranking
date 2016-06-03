using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Globalization;

namespace Ranking
{
    /// <summary>
    /// Summary description for DataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DataService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetSitesLog(string selectedSite)
        {

            List<WebLog> logs = new List<WebLog>();

            string connstring = ConfigurationManager.ConnectionStrings["websDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@site";
                param.DbType = System.Data.DbType.String;
                param.Value = selectedSite;

                SqlCommand cmd = new SqlCommand("SELECT [dateReported] ,[visits] FROM [dbo].[WebSiteHits] where [website] = @site order by dateReported desc", con);
                cmd.Parameters.Add(param);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WebLog web = new WebLog();
                   web.ReportedDate =Convert.ToDateTime( reader["dateReported"]).ToString("MMMM dd,yyyy");
                    web.TotalVisit = Convert.ToInt32(reader["visits"]);
                    logs.Add(web);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(logs);
        }

        [WebMethod]
        public void GetAvailableSites()
        {
            List<string> websites = new List<string>();

            string connstring = ConfigurationManager.ConnectionStrings["websDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand("SELECT distinct [website] FROM [dbo].[WebSiteHits] order by website", con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    websites.Add(reader["website"].ToString());
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Write(js.Serialize(websites));
        }


        [WebMethod]
        public void GetAvailableReportDate()
        {
            List<string> availabledate = new List<string>();

            string connstring = ConfigurationManager.ConnectionStrings["websDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand("SELECT distinct [dateReported] FROM [dbo].[WebSiteHits] order by dateReported desc", con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                     string dDate =Convert.ToDateTime(reader["dateReported"]).ToString("dd-MM-yyyy");
                    availabledate.Add(dDate);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();

            Context.Response.Write(js.Serialize(availabledate));
        }

        [WebMethod]
        public string GetReport(string selectedReport)
        {

            List<WebHits> topFive = new List<WebHits>();

            string connstring = ConfigurationManager.ConnectionStrings["websDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connstring))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@ReportDate";
                param.DbType = System.Data.DbType.DateTime;
                param.Value = DateTime.ParseExact(selectedReport, "dd-MM-yyyy", CultureInfo.InvariantCulture);  

                SqlCommand cmd = new SqlCommand("SELECT top 5 [website] ,[visits] FROM [dbo].[WebSiteHits] where [dateReported] = @ReportDate order by visits desc", con);
                cmd.Parameters.Add(param);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WebHits web = new WebHits();
                    web.website = reader["website"].ToString();
                    web.TotalVisit = Convert.ToInt32(reader["visits"]);
                    topFive.Add(web);

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
           return js.Serialize(topFive); 
        }
    }
}
