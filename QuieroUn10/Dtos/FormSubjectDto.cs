using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Dtos
{
    public class FormSubjectDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Formal Subject")]
        public bool Formal_SubjectT { get; set; }
        public bool Formal_SubjectF { get; set; }

        [Display(Name = "Student Create")]
        public bool Student_CreateT { get; set; }
        public bool Student_CreateF { get; set; }

        public int Course { get; set; }
    }
}
