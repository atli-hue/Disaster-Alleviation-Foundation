using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Monetary
{
    public class IndexModel : PageModel
    {
        public List<MonetaryInfo> listMonetary = new List<MonetaryInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //read data frm tble
                    String sql = "SELECT * FROM monetaryDonations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MonetaryInfo monetaryInfo = new MonetaryInfo();
                                MonetaryInfo.monetaryid = "" + reader.GetInt32(0);
                                MonetaryInfo.date = reader.GetDateTime(5).ToString();
                                MonetaryInfo.amount = reader.GetString(2);
                                MonetaryInfo.donar = reader.GetString(3);

                                MonetaryInfo.Add(monetaryInfo);

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

    public class MonetaryInfo
    {//Allows us to store clients data
        public String monetaryid;
        public String date;
        public String amount;
        public String donar;
    }
}
