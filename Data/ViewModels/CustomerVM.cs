using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace bank_transaction_system.Data.ViewModels
{
    public class CustomerVM
    {
        [Display(Name = "Username")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "Username length must be at least 3 and maximum 45")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(45, MinimumLength = 8, ErrorMessage = "Password length must be at least 8 and maximum 45")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%&*-+./?]).*$", ErrorMessage = 
        "Password must contain at least 1 upper case, 1 lower case, 1 number and 1 special character.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required.")]
        [MinLength(10)]
        public string Address { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Mobile")]
        [Required(AllowEmptyStrings = true)]
        public string Mobile { get; set; } = "";

    }
}

