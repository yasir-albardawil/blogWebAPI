﻿namespace BiteWebAPI.Entities
{
    public class Customer
    {

        public int Id { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }
    }
}
