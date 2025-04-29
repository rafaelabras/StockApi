﻿using System.ComponentModel.DataAnnotations.Schema;

namespace aprendizahem.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        // Navigation 
        public Stock? Stock { get; set; }

        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }



    }
}
