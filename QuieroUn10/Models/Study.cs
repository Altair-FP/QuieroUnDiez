using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Study
    {
        public int ID { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} tiene una longitud máxima de 50 caracteres")]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} tiene una longitud máxima de 50 caracteres")]
        public string Acronym { get; set; }

        [InverseProperty("Study")]
        [Display(Name = "Study Has Subject")]
        public List<StudyHasSubject> StudyHasSubjects { get; set; }


    }
}
