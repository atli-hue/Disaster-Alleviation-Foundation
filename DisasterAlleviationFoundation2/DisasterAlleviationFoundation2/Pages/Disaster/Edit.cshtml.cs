using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Disaster
{
    public class EditModel : PageModel
    {
        public DisasterInfo disasterInfo = new DisasterInfo();
        public String errorMessage = "";
        public String sucessMessage = "";
        public void OnGet()
        {
            String id = Request.Query["disasterid "];

            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Disaster WHERE disasterid=@disasterid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@disasterid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                disasterInfo.disasterid = "" + reader.GetInt32(0);
                                disasterInfo.startdate = reader.GetString(1); 
                                disasterInfo.enddate = reader.GetString(2);
                                disasterInfo.location = reader.GetString(3);
                                disasterInfo.description = reader.GetString(4);
                                disasterInfo.aidtype = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public void OnPost()
        {
            disasterInfo.disasterid = Request.Form["disasterid"];
            disasterInfo.startdate = Request.Form["startdate"];
            disasterInfo.enddate = Request.Form["enddate"];
            disasterInfo.location = Request.Form["location"];
            disasterInfo.description = Request.Form["description"];
            disasterInfo.aidtype = Request.Form["aidtype"];

            //Verify fields
            if (disasterInfo.disasterid.Length == 0 || disasterInfo.startdate.Length == 0 ||
                disasterInfo.enddate.Length == 0 || disasterInfo.location.Length == 0 || disasterInfo.description.Length == 0 || disasterInfo.aidtype.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Disaster " +
                        "SET disasterid=@disasterid, startdate=@startdate, enddate=@enddate, location=@location, description=@description, aidtype=@aidtype"
                + "WHERE disasterid=@disasterid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@disasterid", disasterInfo.disasterid);
                        command.Parameters.AddWithValue("@startdate", disasterInfo.startdate);
                        command.Parameters.AddWithValue("@category", disasterInfo.enddate);
                        command.Parameters.AddWithValue("@description", disasterInfo.location);
                        command.Parameters.AddWithValue("@donar", disasterInfo.description);
                        command.Parameters.AddWithValue("@donar", disasterInfo.aidtype);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Disaster/Index");
        }
    }
}
