using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Abstract
{
    public interface ILoginRepository
    {
        IEnumerable<Role> Roles { get; }
        IEnumerable<UserInformation> UserInformations { get; }

        bool ValidateLogin(UserInformation user, string Password);
        UserInformation GetUserInformation(string Name);

        Role GetRoleById(int roleId);
    }
}
