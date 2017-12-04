using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("Role")]
    public class Role
    {

        [Key, Column(Order = 0)]
        public int RoleId { get; set; }

        public string Name { get; set; }
        public int roleLvl { get; set; }

        [ForeignKey("RoleId")]
        public virtual IEnumerable<UserInformation> UserInformations { get; set; }
    }
}
