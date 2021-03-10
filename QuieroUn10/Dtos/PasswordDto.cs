using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Dtos
{
    public class PasswordDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(30, ErrorMessage = "El campo {0} tiene una longitud máxima de 30 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public String Token { get; set; }

    }
}
