using System.ComponentModel.DataAnnotations;

namespace FleetManagement.API.Models.Resources
{
    public sealed class PlaceCreateRequest
    {
        public int Id { get; set; } = default!;

        [MaxLength(255)]
        public string InternalId { get; set; } = default!;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [MaxLength(100)]
        public string? Type { get; set; }

        public long? ContactId { get; set; }

        [MaxLength(255)]
        public string? AddressLine1 { get; set; }

        [MaxLength(255)]
        public string? AddressLine2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Range(-180, 180)]
        public decimal? Longitude { get; set; }

        [MaxLength(100)]
        public string? Timezone { get; set; }

        [MaxLength(255)]
        public string? ContactName { get; set; }

        [Phone]
        [MaxLength(50)]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [MaxLength(2000)]
        public string? Notes { get; set; }

        public string? Metadata { get; set; }

        [Required]
        public long VendorId { get; set; }
    }
}
