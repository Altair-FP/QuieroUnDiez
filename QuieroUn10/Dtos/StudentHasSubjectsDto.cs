using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Dtos
{
    public class StudentHasSubjectsDto
    {
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
    }
}
