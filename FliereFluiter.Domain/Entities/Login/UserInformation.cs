using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("UserInformation")]
    public class UserInformation
    {

        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        //foreign key
        public int RoleId { get; set; }
        //navigation property
        public virtual Role Role { get; set; }
    }
}
