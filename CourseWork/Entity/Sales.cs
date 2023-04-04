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
    public class Sales
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid Seller { get; set; }
        public Guid Buyer { get; set; }
        public Guid Product { get; set; }
        public DateTime? DeleteDt { get; set; }
        public Sales()
        {
            Id = Guid.NewGuid();
            Name = null!;
        }
        public Sales(SqlDataReader reader)
        {
            Id = reader.GetGuid("Id");
            Seller = reader.GetGuid("Id_Seller");
            Name = reader.GetString("Name");
            Buyer = reader.GetGuid("Id_Buyer");
            Product = reader.GetGuid("Product");
            DeleteDt = reader.IsDBNull("DeleteDt") ? null : reader.GetDateTime("DeleteDt");
        }
    }
}
