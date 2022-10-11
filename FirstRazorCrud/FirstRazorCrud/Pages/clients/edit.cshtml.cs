using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace FirstRazorCrud.Pages.clients
{
    public class Index1Model : PageModel
    {
        public Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public Regex regexLs = new Regex("^[A-Za-z ]+$");
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String ConString = "Data Source=DESKTOP-19LCO0E;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    con.Open();

                    String sql = "SELECT * FROM clients WHERE id = @id" ;
                    using (SqlCommand command = new SqlCommand(sql,con)) {

                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
            
        }

        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            if (!regexLs.IsMatch(clientInfo.name))
            {
                errorMessage = "Your name must contain only Letters";
                return;
            }
            if (regex.IsMatch(clientInfo.email) == false)
            {
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

            try
            {
                String connectionString = @"Data Source=DESKTOP-19LCO0E;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sql = "UPDATE clients SET name=@name, email=@email, phone = @phone, address=@address WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
