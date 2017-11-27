using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Concrete
{
    class EFLoginRepository : ILoginRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Role> Roles
        {
            get { return context.Roles; }
        }

        public IEnumerable<UserInformation> UserInformations
        {
            get { return context.UserInformations; }
        }
    }
}
