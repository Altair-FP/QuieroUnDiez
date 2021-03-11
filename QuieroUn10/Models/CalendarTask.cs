using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class CalendarTask
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Task Date")]
        public DateTime Day { get; set; }

        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        [Display(Name = "Student")]
        public Student Student { get; set; }
    }
}
