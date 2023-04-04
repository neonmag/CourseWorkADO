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
    public class Users
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime? DeleteDt { get; set; }
        public Users()
        {
            Id = Guid.NewGuid();
            Name = null!;
            Surname = null!;
        }
        public Users(SqlDataReader reader)
        {
            Id = reader.GetGuid("Id");
            Surname = reader.GetString("Surname");
            Name = reader.GetString("Name");
            DeleteDt = reader.IsDBNull("DeleteDt") ? null : reader.GetDateTime("DeleteDt");
        }
    }
}
