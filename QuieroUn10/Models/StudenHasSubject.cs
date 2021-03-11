using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class StudentHasSubject
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Inscription Date")]
        public DateTime InscriptionDate { get; set; }

        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [ForeignKey("StudentId")]
        [Display(Name = "Student")]
        public Student Student { get; set; }

        [ForeignKey("SubjectId")]
        [Display(Name = "Subject")]
        public Subject Subject { get; set; }

        [InverseProperty("StudentHasSubject")]
        [Display(Name = "Task")]
        public List<Task> Tasks { get; set; }

        [InverseProperty("StudentHasSubject")]
        [Display(Name = "Doc")]
        public List<Doc> Docs { get; set; }

    }
}
