using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank_transaction_system.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        public long CurrentBalance { get; set; }

        public int CustomerId { get; set; }
    }
}

