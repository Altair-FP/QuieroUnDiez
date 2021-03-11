using QuieroUn10.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Task
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Task Date")]
        public DateTime TaskDate { get; set; }

        [Required]
        [Display(Name = "Type")]
        public TaskType Type { get; set; }

        [Display(Name = "Student Has Subject")]
        public int StudentHasSubjectId { get; set; }


        [ForeignKey("StudentHasSubjectId")]
        [Display(Name = "Student Has Subjects")]
        public StudentHasSubject StudentHasSubject { get; set; }
    }
}
