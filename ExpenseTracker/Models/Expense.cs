using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Expense
    {
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
