using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WeddingPlanner.Models
{
    public class RSVP
    {
    // auto-implemented properties need to match the columns in your table
    // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int RSVPId { get; set; }
        public int WeddingId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public User AttendingUser { get; set; }
        public Wedding AttendedWedding { get; set; }
    }
}