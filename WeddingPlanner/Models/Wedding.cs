using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models
{
    public class Wedding
    {
    // auto-implemented properties need to match the columns in your table
    // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int WeddingId { get; set; }
        
        [Required]
        [MinLength(5, ErrorMessage="Please provide a first name for this wedder that is at least 5 characters long.")]
        public string WedderOne { get; set; }

        [Required]
        [MinLength(5, ErrorMessage="Please provide a last name for this wedder that is at least 5 characters long.")]
        public string WedderTwo { get; set; }
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }
        public List<RSVP> RSVPs {get; set;}
    }
}