using System;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.API.Models.Resources
{
    public sealed class DriverCreateRequest
    {
        public int Id { get; set; } = default!;

        [MaxLength(255)]
        public string InternalId { get; set; } = default!;

        public long? VehicleId { get; set; } = null;

        public long? FleetId { get; set; } = null;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [Phone]
        [MaxLength(50)]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Url]
        [MaxLength(500)]
        public string? AvatarUrl { get; set; }

        [MaxLength(100)]
        public string? DriverLicenseNumber { get; set; }

        public DateTime? DriverLicenseExpiry { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        public string? Metadata { get; set; } = default!;

        public long? UserId { get; set; } = null;
    }
}
