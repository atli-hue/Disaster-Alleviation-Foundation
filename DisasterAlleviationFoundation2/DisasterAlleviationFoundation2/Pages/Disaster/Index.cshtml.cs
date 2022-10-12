using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Disaster
{
    public class IndexModel : PageModel
    {
        public List<DisasterInfo> ListdisasterInfo = new List<DisasterInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //read data frm tble
                    String sql = "SELECT * FROM Disaster";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisasterInfo disasterInfo = new DisasterInfo();
                                disasterInfo.disasterid = "" + reader.GetInt32(0);
                                disasterInfo.startdate = reader.GetDateTime(5).ToString();
                                disasterInfo.enddate = reader.GetString(2);
                                disasterInfo.location = reader.GetString(3);
                                disasterInfo.description = reader.GetString(4);
                                disasterInfo.aidtype = reader.GetString(4);

                                ListdisasterInfo.Add(disasterInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class DisasterInfo
    {//Allows us to store clients data
        public String disasterid;
        public String startdate;
        public String enddate;
        public String location;
        public String description;
        public String aidtype;
    }
}
