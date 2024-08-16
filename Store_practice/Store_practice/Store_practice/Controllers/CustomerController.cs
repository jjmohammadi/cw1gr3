using Microsoft.AspNetCore.Mvc;
using Store_practice.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace Store_practice.Controllers
{
    public class CustomerController : Controller
    {
        public string connectionString = "Data Source=.;Initial Catalog=store;TrustServerCertificate=True;Integrated Security=SSPI";
       
        public IActionResult GetAll()
        {
            var customerList = new List<Customer>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerId = Convert.ToInt32(rdr["Id"]);
                    customer.FirstName = rdr["FirstName"].ToString();
                    customer.LastName = rdr["LastName"].ToString();
                    customer.City= rdr["City"].ToString();
                    customer.Country = rdr["Country"].ToString();
                    customer.Phone = rdr["Phone"].ToString();
                    customerList.Add(customer);
                }
                con.Close();
            }
            return Json(customerList);
        }
    }
}
