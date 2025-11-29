using System.ComponentModel.DataAnnotations;

namespace FleetManagement.API.Models.Resources
{
    public sealed class VehicleCreateRequest
    {
        public int Id { get; set; } = default!;

        [MaxLength(255)]
        public string InternalId { get; set; } = default!;

        [MaxLength(255)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? PlateNumber { get; set; }

        [MaxLength(100)]
        public string? Vin { get; set; }

        [MaxLength(100)]
        public string? Type { get; set; }

        [MaxLength(100)]
        public string? Make { get; set; }

        [MaxLength(100)]
        public string? Model { get; set; }

        [Range(1900, 2100)]
        public int? Year { get; set; }

        [MaxLength(50)]
        public string? Color { get; set; }

        [Range(0, int.MaxValue)]
        public int? Capacity { get; set; }

        [MaxLength(20)]
        public string? CapacityUnit { get; set; }

        public long? DriverId { get; set; }

        [MaxLength(50)]
        public string? Status { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? CurrentOdometer { get; set; }

        public long? FleetId { get; set; }

        [MaxLength(50)]
        public string? FuelType { get; set; }

        public string? Metadata { get; set; }

        [Required]
        public long VendorId { get; set; }
    }
}
