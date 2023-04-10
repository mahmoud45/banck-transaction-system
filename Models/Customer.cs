using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace bank_transaction_system.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Name is required.")]
        
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Column(TypeName = "varchar(350)")]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required.")]
        [MinLength(10)]
        public string Address { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Mobile")]
        [Required(AllowEmptyStrings = true)]
        public string Mobile { get; set; } = "";

        public List<BankAccount> bankAccounts { get; set; }
    }
}

