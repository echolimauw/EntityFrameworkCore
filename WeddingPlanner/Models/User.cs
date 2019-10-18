using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class User
    {
    // auto-implemented properties need to match the columns in your table
    // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int UserId { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z ]+)$", ErrorMessage = "Please only use letters and spaces for names.")]
        [MinLength(5, ErrorMessage="Names must be at least 5 characters.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z ]+)$", ErrorMessage = "Please only use letters and spaces for names.")]
        [MinLength(5, ErrorMessage="Names must be at least 5 characters.")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Passwords must be at least 8 characters.")]
        public string Password { get; set; }
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get; set;}
        public List<Wedding> CreatedWeddings {get; set;}
        public List<RSVP> RSVPs {get; set;}
    }
}