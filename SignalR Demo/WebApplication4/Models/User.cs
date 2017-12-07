using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class User
    {
        [Required]
        public String name { get; set; }

        public String Id { get; set; }

        public String email { get; set; }

        public String status { get; set; }

        [Required]
        public String password { get; set; }

        public String gender { get; set; }

        public DateTime birthday { get; set; }

        public String image { get; set; }
    }
}