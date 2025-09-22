using System;
using System.ComponentModel.DataAnnotations;

namespace DocManagementSystem.Common.Models.Entities
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public required string ItemName { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? Supplier { get; set; }

        // Optional: Price, Category, etc.
        public decimal? Price { get; set; }

        public string? Category { get; set; }
    }
}