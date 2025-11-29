using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManagement.EF.Models
{
    public partial class UserLogin
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("Username")]
        [Required, MaxLength(255)]
        public string Username { get; set; } = default!;

        [Column("Email")]
        [Required, MaxLength(255)]
        public string Email { get; set; } = default!;

        [Column("PasswordHash")]
        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = default!;

        [Column("UserId")]
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }

        public User User { get; set; } = default!;
    }
}