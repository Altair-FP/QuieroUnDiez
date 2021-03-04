using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class Role
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Must be between 1 and 30 characters", MinimumLength = 1)]
        public String Name { get; set; }

        [StringLength(100, ErrorMessage = "Must be inferior than 100 characters")]
        public String Description { get; set; }

        public Boolean Enabled { get; set; }

        [InverseProperty("Role")]
        [Display(Name = "User Accounts")]
        public List<UserAccount> UserAccounts { get; set; }

        [InverseProperty("Role")]
        [Display(Name = "Role Has Menus")]
        public List<RoleHasMenu> RoleHasMenus { get; set; }




    }
}
