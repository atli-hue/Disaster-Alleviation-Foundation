using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Goods
{
    public class EditModel : PageModel
    {
        public GoodsDonationsInfo goodsDonationsInfo = new GoodsDonationsInfo();
        public String errorMessage = "";
        public String sucessMessage = "";
        public void OnGet()
        {
            String id = Request.Query["goodsid"];

            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM goodsDonations WHERE goodsid=@goodsid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@goodsid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                goodsDonationsInfo.goodsid = "" + reader.GetInt32(0);
                                goodsDonationsInfo.date = reader.GetString(1);
                                goodsDonationsInfo.category = reader.GetString(2);
                                goodsDonationsInfo.description = reader.GetString(3);
                                goodsDonationsInfo.donar = reader.GetString(4);
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

            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE goodsDonations " +
                        "SET goodsid=@goodsid, date=@date, category=@category, description=@description, donar=@donar"
                + "WHERE goodsid=@goodsid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@goodsid", goodsDonationsInfo.goodsid);
                        command.Parameters.AddWithValue("@date", goodsDonationsInfo.date);
                        command.Parameters.AddWithValue("@category", goodsDonationsInfo.category);
                        command.Parameters.AddWithValue("@description", goodsDonationsInfo.description);
                        command.Parameters.AddWithValue("@donar", goodsDonationsInfo.donar);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Goods/Index");
        }
    }
}
