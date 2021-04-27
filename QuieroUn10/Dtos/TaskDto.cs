using QuieroUn10.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Dtos
{
    public class TaskDto
    {

        [Required]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Type")]
        public TaskType Type { get; set; }
    }
}
