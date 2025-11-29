using System;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.API.Models.Resources
{
    public sealed class TransactionCreateRequest
    {
        public int Id { get; set; } = default!;

        [MaxLength(255)]
        public string InternalId { get; set; } = default!;

        [Required]
        public long OrderId { get; set; }

        [MaxLength(100)]
        public string? Reference { get; set; }

        [MaxLength(100)]
        public string? Type { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Amount { get; set; }

        [MaxLength(10)]
        public string? Currency { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        [MaxLength(50)]
        public string? PaymentMethod { get; set; }

        public DateTime? ProcessedAt { get; set; }

        public string? LineItems { get; set; }

        public string? Metadata { get; set; }
    }
}
