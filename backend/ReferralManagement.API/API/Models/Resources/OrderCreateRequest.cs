using System;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.API.Models.Resources
{
    public sealed class OrderCreateRequest
    {
        public int Id { get; set; } = default!;

        [MaxLength(255)]
        public string InternalId { get; set; } = default!;

        [Required]
        public long OriginPlaceId { get; set; }

        [Required]
        public long DestinationPlaceId { get; set; }

        public long? ContactId { get; set; }

        [MaxLength(100)]
        public string? TrackingNumber { get; set; }

        [MaxLength(100)]
        public string? PublicId { get; set; }

        public long? VehicleId { get; set; }

        public long? DriverId { get; set; }

        [MaxLength(100)]
        public string? Type { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public DateTime? ScheduledAt { get; set; }
        public DateTime? DispatchedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        [MaxLength(10)]
        public string? Currency { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? BaseAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? TotalAmount { get; set; }

        [MaxLength(50)]
        public string? PodMethod { get; set; }

        public string? Payload { get; set; }

        [MaxLength(2000)]
        public string? Notes { get; set; }

        public string? Metadata { get; set; }

        [Required]
        public long VendorId { get; set; }
    }
}
