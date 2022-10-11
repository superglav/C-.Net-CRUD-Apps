using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace FirstRazorCrud.Pages.clients
{
    public class createModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public Regex regexLs = new Regex("^[A-Za-z ]+$");
        public void OnGet()
        {
        }
        
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0|| clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return; 
            }
            if (!regexLs.IsMatch(clientInfo.name))
            {
                errorMessage = "Your name must contain only Letters";
                return;
            }
            if (regex.IsMatch(clientInfo.email) == false){
                errorMessage = "Your email has not the right format";
                return;
            }
            if (!clientInfo.phone.All(Char.IsNumber))
            {
                errorMessage = "Your phone must contain only Numbers";
                return;
            }
            if (regexLs.IsMatch(clientInfo.address))
            {
                errorMessage = "Your address must contain only Letters and Digits";
                return;
            }


            //saving data to the db
            try
            {
                String connectionString = @"Data Source=DESKTOP-19LCO0E;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "INSERT INTO clients " + "(name,email,phone,address) VALUES " + "(@name,@email,@phone,@address)";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");
        }

    }
}
