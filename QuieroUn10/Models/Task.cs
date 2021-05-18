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
        public String Title { get; set; }

        public String Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }


        [DataType(DataType.Date)]
        public DateTime? End { get; set; }

        public bool AllDay { get; set; }

        public String ClassName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }


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
