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
    public class Seller
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Phone { get; set; }
        public DateTime? DeleteDt { get; set; }
        public Seller()
        {
            Id = Guid.NewGuid();
            Name = null!;
            Surname = null!;
            Phone = null!;
        }
        public Seller(SqlDataReader reader)
        {
            Id = reader.GetGuid("Id");
            Surname = reader.GetString("Surname");
            Name = reader.GetString("Name");
            Phone = reader.GetString("Phone");
            DeleteDt = reader.IsDBNull("DeleteDt") ? null : reader.GetDateTime("DeleteDt");
        }
        public override string ToString()
        {
            return $"ID: {Id}\nSurname: {Surname}\nName: {Name}\nPhone: {Phone}\nDeleted: {DeleteDt}";
        }
    }
}
