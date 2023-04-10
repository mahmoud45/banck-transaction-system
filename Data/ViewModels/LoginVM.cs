using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace bank_transaction_system.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

