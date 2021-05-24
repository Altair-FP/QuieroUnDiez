using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Dtos
{
    public class FormStudenHasSubjectsDto
    {
        public int ID { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDateI { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDateF { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        public string Ordering { get; set; }

    }
}
