using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Doc
    {
        public int ID { get; set; }

        public byte[] DocByte { get; set; }

        public string DocSourceFileName { get; set; }

        public string DocContentType { get; set; }

        [Display(Name = "Student Has Subject")]
        public int StudentHasSubjectId { get; set; }

        [ForeignKey("StudentHasSubjectId")]
        [Display(Name = "Student Has Subjects")]
        public StudentHasSubject StudentHasSubject { get; set; }
    }
}
