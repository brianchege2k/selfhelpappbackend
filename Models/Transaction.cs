using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfHelpGroupAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member? Member { get; set; } // Made optional by removing 'required' and adding '?'

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; } = "Deposit"; // Simplified to non-nullable with default value

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}