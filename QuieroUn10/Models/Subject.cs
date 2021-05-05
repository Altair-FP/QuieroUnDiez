using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Subject
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} tiene una longitud máxima de 50 caracteres")]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Student_Create { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} tiene una longitud máxima de 50 caracteres")]
        public string Course { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} tiene una longitud máxima de 50 caracteres")]
        public string Acronym { get; set; }

        [InverseProperty("Subject")]
        [Display(Name = "Student Has Subject")]
        public List<StudentHasSubject> StudentHasSubjects { get; set; }

        [InverseProperty("Subject")]
        [Display(Name = "Study Has Subject")]
        public List<StudyHasSubject> StudyHasSubjects { get; set; }


    }
}
