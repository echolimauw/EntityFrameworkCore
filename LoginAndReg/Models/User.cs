    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    namespace LoginAndReg.Models
    {
        public class User
        {
            // auto-implemented properties need to match the columns in your table
            // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
            [Key]
            public int UserId { get; set; }
            
            // MySQL VARCHAR and TEXT types can be represeted by a string
            [Required]
            [MinLength(2, ErrorMessage="First names must be at least 2 characters!")]
            public string FirstName { get; set; }
            [Required]
            [MinLength(2, ErrorMessage="Last names must be at least 2 characters!")]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [MinLength(8, ErrorMessage="Passwords must be at least 8 characters!")]
            public string Password { get; set; }
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            [NotMapped]
            [Compare("Password")]
            [DataType(DataType.Password)]
            public string Confirm {get; set;}
        }
    }
    