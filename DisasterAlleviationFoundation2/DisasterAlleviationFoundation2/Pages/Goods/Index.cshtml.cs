using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Goods
{
    public class IndexModel : PageModel
    {
        public List<GoodsDonationsInfo> ListGoodsDonationsInfo = new List<GoodsDonationsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //read data frm tble
                    String sql = "SELECT * FROM goodsDonations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoodsDonationsInfo goodsDonationsInfo = new GoodsDonationsInfo();
                                goodsDonationsInfo.goodsid = "" + reader.GetInt32(0);
                                goodsDonationsInfo.date = reader.GetDateTime(5).ToString();
                                goodsDonationsInfo.category = reader.GetString(2);
                                goodsDonationsInfo.description = reader.GetString(3);
                                goodsDonationsInfo.donar = reader.GetString(4);
                                

                                ListGoodsDonationsInfo.Add(goodsDonationsInfo);

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

    public class GoodsDonationsInfo
    {//Allows us to store clients data
        public String goodsid;
        public String date;
        public String category;
        public String description;
        public String donar;
    }
}
