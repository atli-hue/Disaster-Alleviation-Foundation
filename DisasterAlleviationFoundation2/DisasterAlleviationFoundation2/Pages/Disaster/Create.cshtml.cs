using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Disaster
{
    public class CreateModel : PageModel
    {
        public DisasterInfo disasterInfo = new DisasterInfo();
        public String errorMessage = "";
        public String successMesage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            disasterInfo.disasterid = Request.Form["disasterid "];
            disasterInfo.startdate = Request.Form["startdate "];
            disasterInfo.enddate = Request.Form["enddate"];
            disasterInfo.location = Request.Form["location "];
            disasterInfo.description = Request.Form["description"];
            disasterInfo.description = Request.Form["aidtype"];

            //Verify fields
            if (disasterInfo.disasterid.Length == 0 || disasterInfo.startdate.Length == 0 ||
                disasterInfo.enddate.Length == 0 || disasterInfo.location.Length == 0 || disasterInfo.description.Length == 0 || disasterInfo.aidtype.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            //Save data to db
            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO goodsDonations" +
                        "(disasterid , startdate  , enddate  , location , description, aidtype) VALUES " +
                        "(@disasterid, @startdate, @enddate  , @location, @description, @aidtype);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@goodsid ", disasterInfo.disasterid);
                        command.Parameters.AddWithValue("@date ", disasterInfo.startdate);
                        command.Parameters.AddWithValue("@category ", disasterInfo.enddate);
                        command.Parameters.AddWithValue("@location  ", disasterInfo.location);
                        command.Parameters.AddWithValue("@donar ", disasterInfo.description);
                        command.Parameters.AddWithValue("@aidtype ", disasterInfo.aidtype);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            disasterInfo.disasterid = ""; disasterInfo.startdate = ""; disasterInfo.enddate = ""; disasterInfo.location = ""; disasterInfo.description = ""; disasterInfo.aidtype = "";
            successMesage = "New Good Donation added Correctly";

            Response.Redirect("/Disaster/Index");
        }
    }
}
