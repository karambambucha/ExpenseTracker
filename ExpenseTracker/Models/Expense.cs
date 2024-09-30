using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public string Description { set; get; }
        [Required]
        public decimal Amount { set; get; }
        [Required]
        public DateTime Date { set; get; }
        [Required]
        public int UserId { set; get; }
    }
}
