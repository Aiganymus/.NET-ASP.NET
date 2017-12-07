using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Message
    {
        [Required]
        public String from { get; set; }

        public String to { get; set; }

        [Required]
        public String text { get; set; }

        [Required]
        public DateTime date { get; set; }

        public int groupID { get; set; }

        public String FromName { get; set; }
    }
}