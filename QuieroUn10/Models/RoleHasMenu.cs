using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class RoleHasMenu
    {
        public int ID { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "Menu")]
        public int MenuId { get; set; }

        [ForeignKey("RoleId")]
        [Display(Name = "Role")]
        public Role Role { get; set; }

        [ForeignKey("MenuId")]
        [Display(Name = "Menu")]
        public Menu Menu { get; set; }


    }
}
