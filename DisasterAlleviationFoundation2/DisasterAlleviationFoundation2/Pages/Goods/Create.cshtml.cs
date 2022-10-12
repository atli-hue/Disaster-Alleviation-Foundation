using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Goods
{
    public class CreateModel : PageModel
    {
        public GoodsDonationsInfo goodsDonationsInfo = new GoodsDonationsInfo();
        public String errorMessage = "";
        public String successMesage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            goodsDonationsInfo.goodsid = Request.Form["goodsid"];
            goodsDonationsInfo.date = Request.Form["date"];
            goodsDonationsInfo.category = Request.Form["category"]; 
            goodsDonationsInfo.description = Request.Form["description"];
            goodsDonationsInfo.donar = Request.Form["donar"];

            //Verify fields
            if (goodsDonationsInfo.goodsid.Length == 0 || goodsDonationsInfo.date.Length == 0 || 
                goodsDonationsInfo.category.Length == 0 || goodsDonationsInfo.description.Length == 0 || goodsDonationsInfo.donar.Length == 0)
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
                        "(goodsid , date , category , description, donar) VALUES " +
                        "(@goodsid, @date, @category , @donar );";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@goodsid ", goodsDonationsInfo.goodsid);
                        command.Parameters.AddWithValue("@date ", goodsDonationsInfo.date);
                        command.Parameters.AddWithValue("@category ", goodsDonationsInfo.category);
                        command.Parameters.AddWithValue("@description ", goodsDonationsInfo.description);
                        command.Parameters.AddWithValue("@donar ", goodsDonationsInfo.donar);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            goodsDonationsInfo.goodsid = ""; goodsDonationsInfo.date = ""; goodsDonationsInfo.category = ""; goodsDonationsInfo.description = "";goodsDonationsInfo.donar = "";
            successMesage = "New Good Donation added Correctly";

            Response.Redirect("/Goods/Index");
        }
    }
}
