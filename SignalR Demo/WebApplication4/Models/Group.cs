using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;

namespace WebApplication4.Models
{
    public class Group
    {
        public int Id;

        [Required]
        public String Name { get; set; }

        [DisplayName("Upload File")]
        public String Image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}