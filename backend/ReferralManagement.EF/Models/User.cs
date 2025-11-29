using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace FleetManagement.EF.Models
{
    public partial class User
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("FirstName")]
        [Required, MaxLength(255)]
        public string FirstName { get; set; } = default!;


        [Column("LastName")]
        [Required, MaxLength(255)]
        public string LastName { get; set; } = default!;

        [Column("CompanyName")]
        [MaxLength(255)]
        public string CompanyName { get; set; } = default!;


        [Column("PhoneNumber")]
        [MaxLength(255)]
        public string PhoneNumber { get; set; } = default!;


        [Column("MobilePhoneNumber")]
        [MaxLength(255)]
        public string MobilePhoneNumber { get; set; } = default!;

        public ICollection<UserLogin> Logins { get; set; } = new List<UserLogin>();
    }
}