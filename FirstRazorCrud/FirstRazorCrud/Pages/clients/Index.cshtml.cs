using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FirstRazorCrud.Pages.clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();        
        public void OnGet()
        {
            try
            {
                string connectionstring = @"Data Source=DESKTOP-19LCO0E;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection())
                {   
                    connection.ConnectionString = connectionstring;
                    connection.Open();
                    string sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" +  reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            
                            }
                        }
                    }
                }
            }
            catch (Exception ex )
            {

                Console.WriteLine("Exception: ",ex.Message);
            }
        }
    public static string GetConnectionString() => "Data Source=DESKTOP-19LCO0E;Initial Catalog=mystore;Integrated Security=True";
    
    }
    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;   
    }
}
