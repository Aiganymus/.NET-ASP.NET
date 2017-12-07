using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetDemoProject.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter name.")]
        [MinLength(2, ErrorMessage = "Minimum length is 2.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter surname.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please, enter birthdate.")]
        public DateTime BirthDate { get; set; }

        public string DisplayBirthDate { get; set; }

        [Required(ErrorMessage = "Please, enter email.")]
        [EmailAddress(ErrorMessage = "Wrong email. Please, try again.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter city.")]
        public string City { get; set; }
    }
    
}