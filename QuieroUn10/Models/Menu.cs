using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Menu
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Must be between 1 and 30 characters", MinimumLength = 1)]
        public String Controller { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Must be between 1 and 20 characters", MinimumLength = 1)]
        public String Action { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Must be between 1 and 20 characters", MinimumLength = 1)]
        public String Label { get; set; }

        [InverseProperty("Menu")]
        [Display(Name = "Role Has Menus")]
        public List<RoleHasMenu> RoleHasMenus { get; set; }

        [NotMapped]
        public string FullName { get => (Controller + " - " + Action); }

    }
}
