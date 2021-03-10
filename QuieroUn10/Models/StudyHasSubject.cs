using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class StudyHasSubject
    {
        public int ID { get; set; }

        [Display(Name = "Study")]
        public int StudyId { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [ForeignKey("StudyId")]
        [Display(Name = "Study")]
        public Study Study { get; set; }

        [ForeignKey("SubjectId")]
        [Display(Name = "Subject")]
        public Subject Subject { get; set; }


    }
}
