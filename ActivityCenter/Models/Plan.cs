using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class Plan
    {
    // auto-implemented properties need to match the columns in your table
    // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage="Names must be at least 2 characters.")]
        public string Name { get; set; }

        public DateTime ScheduledFor { get; set; }

        public int DurationInt { get; set; }

        public string DurationType { get; set; }
        public string CreatorID { get; set; }

        [Required]
        [MinLength(8, ErrorMessage="Descriptions must be at least 8 characters.")]
        public string Description { get; set; }

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        
    }
}