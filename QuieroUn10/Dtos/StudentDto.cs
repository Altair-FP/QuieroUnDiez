using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Dtos
{
    public class StudentDto
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "User name is required")]
        [Index(IsUnique = true)]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        public String Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 1)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public String ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(70, ErrorMessage = "El campo {0} tiene una longitud máxima de 30 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "El campo {0} tiene una longitud máxima de 20 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(40, ErrorMessage = "El campo {0} tiene una longitud máxima de 40 caracteres")]
        public string Surname { get; set; }

        [StringLength(15, ErrorMessage = "El campo {0} tiene una longitud máxima de 15 caracteres")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

    }
}
