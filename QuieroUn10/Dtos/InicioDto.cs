using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Dtos
{
    public class InicioDto
    {
        public int ID { get; set; }


        [Display(Name = "Studies")]
        public int StudiesId { get; set; }

        [Display(Name = "Subjects")]
        public List<int> SubjectId { get; set; }

    }
}
