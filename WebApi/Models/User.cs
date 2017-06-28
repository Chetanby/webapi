using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {
        public Int64 ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
