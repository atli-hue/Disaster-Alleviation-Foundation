using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Monetary
{
    public class EditModel : PageModel
    {
        public MonetaryInfo monetaryInfo = new MonetaryInfo();
        public String errorMessage = "";
        public String sucessMessage = "";
        public void OnGet()
        {
            String id = Request.Query["monetaryid"];

            try
            {
                String connectionString = "Data Source=desktop-m9ia4fv\\sqlexpress;Initial Catalog=DisasterAlleviationFoundation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM monetaryDonations WHERE id=@monetaryid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@monetaryid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                monetaryInfo.monetaryid = "" + reader.GetInt32(0);
                                monetaryInfo.date = reader.GetString(1);
                                monetaryInfo.amount = reader.GetString(2);
                                monetaryInfo.donar = reader.GetString(3);
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
            monetaryInfo.monetaryid = Request.Form["monetaryid"];
            monetaryInfo.date = Request.Form["date"];
            monetaryInfo.amount = Request.Form["amount"];
            monetaryInfo.donar = Request.Form["donar"];
           
            //Verify fields
            if (monetaryInfo.monetaryid.Length == 0 || monetaryInfo.date.Length == 0 ||
                monetaryInfo.amount.Length == 0 || monetaryInfo.donar.Length == 0)
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
                    String sql = "UPDATE monetaryDonations " +
                        "SET date=@date, amount=@amount , donar=@donar"
                + "WHERE monetaryid=@monetaryid";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@date", monetaryInfo.date);
                        command.Parameters.AddWithValue("@amount", monetaryInfo.amount);
                        command.Parameters.AddWithValue("@donar", monetaryInfo.donar);
                        command.Parameters.AddWithValue("@id", monetaryInfo.monetaryid);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Monetary/Index");
        }
    }
}
