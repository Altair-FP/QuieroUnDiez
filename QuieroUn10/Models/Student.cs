using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Student : Person
    {

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required]
        public Boolean Activate { get; set; }

        [InverseProperty("Student")]
        [Display(Name = "Student Has Subject")]
        public List<StudentHasSubject> StudentHasSubjects { get; set; }

        [InverseProperty("Student")]
        [Display(Name = "Calendar Task")]
        public List<CalendarTask> CalendarTasks { get; set; }

    }
}
