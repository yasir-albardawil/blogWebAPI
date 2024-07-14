﻿using BiteWebAPI.DTOs;
using BiteWebAPI.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BiteWebAPI.Models
{
    public class ItemDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        [Required]
        public string? LongDescription { get; set; }

        public string? AllergyInformation { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Required]
        public string? ImageThumbnailUrl { get; set; }

        public bool IsPieOfTheWeek { get; set; }

        public bool InStock { get; set; }
        public CategoryDTO Category { get; set; } = default!;
    }
}
