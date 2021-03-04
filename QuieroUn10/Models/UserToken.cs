using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuieroUn10.Models
{
    public class UserToken
    {
        public int ID { get; set; }

        [Required]
        public String Token { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Generated Date")]
        public DateTime GeneratedDate { get; set; }

        public int Life { get; set; }

        [Display(Name = "User Account")]
        public int UserAccountId { get; set; }


        [ForeignKey("UserAccountId")]
        [Display(Name = "User Account")]
        public UserAccount UserAccount { get; set; }

    }
}
