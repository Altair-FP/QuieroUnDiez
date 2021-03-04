using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Person
    {
        public int ID { get; set; }


        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} tiene una longitud máxima de 20 caracteres")]
        public string Name { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "El campo {0} tiene una longitud máxima de 40 caracteres")]
        public string Surname { get; set; }


        [StringLength(15, ErrorMessage = "El campo {0} tiene una longitud máxima de 15 caracteres")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "User Account")]
        public int UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public UserAccount UserAccount { get; set; }

        public String FullName { get => (Name + " " + Surname); }

    }
}
