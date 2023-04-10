using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank_transaction_system.Models
{
    public class BankTransaction
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Please enter the amount.")]
        public long Amount { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "From Account ID")]
        public string FromAccountId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "To Account ID")]
        [Required(ErrorMessage = "please enter the account number to transact the money to")]
        public string ToAccountId { get; set; }
    }
}

