using System.ComponentModel.DataAnnotations;

namespace SelfHelpGroupAPI.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string PhoneNumber { get; set; }

        [Required]
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public decimal Balance { get; set; } = 0.0m;
    }
}
