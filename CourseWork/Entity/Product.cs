using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid Seller { get; set; }
        public int Amount { get; set; }
        public DateTime? DeleteDt { get; set; }
        public int Price { get; set; }
        public Product()
        {
            Id = Guid.NewGuid();
            Name = null!;
            Seller = Guid.NewGuid();
            Amount = 1;
            Price = 1;
        }
        public Product(SqlDataReader reader)
        {
            Id = reader.GetGuid("Id");
            Name = reader.GetString("Name");
            Seller = reader.GetGuid("Seller");
            Amount = reader.GetInt32("Amount");
            DeleteDt = reader.IsDBNull("DeleteDt") ? null : reader.GetDateTime("DeleteDt");
            Price = reader.GetInt32("Price");
        }
    }
}
