using System.ComponentModel.DataAnnotations;

namespace FleetManagement.API.Models.Resources
{
    public sealed class VendorCreateRequest
    {
        public int Id { get; set; } = default!;

        [MaxLength(255)]
        public string InternalId { get; set; } = default!;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [MaxLength(100)]
        public string? Type { get; set; }

        [EmailAddress]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? Website { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        [MaxLength(1000)]
        public string? BillingAddress { get; set; }

        public string? Metadata { get; set; }
    }
}
