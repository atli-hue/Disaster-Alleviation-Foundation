using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DisasterAlleviationFoundation2.Pages.Monetary
{
    public class CreateModel : PageModel
    {
        public MonetaryInfo monetaryInfo = new MonetaryInfo();
        public String errorMessage = "";
        public String successMesage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            monetaryInfo.monetaryid = Request.Form["ID"];
            monetaryInfo.date = Request.Form["Date"];
            monetaryInfo.amount = Request.Form["Amount"];
            monetaryInfo.donar = Request.Form["Donar"];

            //Verify fields
            if (monetaryInfo.monetaryid.Length == 0 || monetaryInfo.date.Length == 0 ||
                monetaryInfo.amount.Length == 0 || monetaryInfo.donar.Length == 0)
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
                    String sql = "INSERT INTO monetaryDonations " +
                        "(monetaryid, date, amount, donar) VALUES " +
                        "(@monetaryid, @date, @amount, @donar);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@monetaryid", monetaryInfo.monetaryid);
                        command.Parameters.AddWithValue("@date", monetaryInfo.date);
                        command.Parameters.AddWithValue("@amount", monetaryInfo.amount);
                        command.Parameters.AddWithValue("@donar", monetaryInfo.donar);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            monetaryInfo.monetaryid = ""; monetaryInfo.date = ""; monetaryInfo.amount = ""; monetaryInfo.donar = "";
            successMesage = "New Monetary Donation Added Correctly";

            Response.Redirect("/Monetary/Index");
        }
    }
}
