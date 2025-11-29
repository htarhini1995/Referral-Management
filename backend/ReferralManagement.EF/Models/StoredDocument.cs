using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.EF.Models
{
    public partial class StoredDocument
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("OriginalFileName")]
        [Required, MaxLength(255)]
        public string OriginalFileName { get; set; } = default!;

        [Column("StoragePath")]
        [Required, MaxLength(1024)]
        public string StoragePath { get; set; } = default!;

        [Column("ContentType")]
        [Required, MaxLength(255)]
        public string ContentType { get; set; } = default!;

        [Column("SizeBytes")]
        [Required]
        public long SizeBytes { get; set; }

        [Column("UploadedAtUtc")]
        [Required]
        public DateTime UploadedAtUtc { get; set; }

        [Column("UploadedBy")]
        [Required, MaxLength(255)]
        public string UploadedBy { get; set; } = default!;
    }
}
