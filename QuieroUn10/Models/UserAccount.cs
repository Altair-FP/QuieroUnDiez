using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class UserAccount
    {
        public int ID { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        public String Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [EmailAddress]
        [StringLength(100, ErrorMessage = "Must be between 1 and 100 characters", MinimumLength = 1)]
        public String Email { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }


        [ForeignKey("RoleId")]
        [Display(Name = "Role")]
        public Role Role { get; set; }

        [InverseProperty("UserAccount")]
        [Display(Name = "User Tokens")]
        public List<UserToken> UserTokens { get; set; }


    }
}
