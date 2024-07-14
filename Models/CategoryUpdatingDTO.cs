using BiteWebAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiteWebAPI.DTOs
{
    public class CategoryUpdatingDTO
    {
        [Required]
        public string CategoryName { get; set; } = String.Empty;
        public string? Description { get; set; }
    }
}
