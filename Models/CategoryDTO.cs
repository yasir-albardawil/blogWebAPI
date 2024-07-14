using BiteWebAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiteWebAPI.DTOs
{
    public class CategoryDTO
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = String.Empty;
        public string? Description { get; set; }
    }
}
