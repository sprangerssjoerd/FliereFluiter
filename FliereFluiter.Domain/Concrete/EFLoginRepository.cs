using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;


using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FliereFluiter.Domain.Concrete;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace FliereFluiter.Domain.Concrete
{
    public class EFLoginRepository : ILoginRepository
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

        public bool ValidateLogin(UserInformation user, string Password)
        {
                if (user.Password == Password)
                {
                    return true;
                }
                else
                {
                // throw new Exception("password doesn't match");
                    
                    return false;
                }
        }

        public UserInformation GetUserInformation(string Name)
        {
            UserInformation user = null;
            try
            {
                user = context.UserInformations.Single(u => u.UserName == Name);
                user.Role = GetRoleById(user.RoleId);
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception("UserName not found", ex);
            }
        }

        public Role GetRoleById(int roleId)
        {
            Role role = null;
            try
            {
                role = context.Roles.Single(r => r.RoleId == roleId);
                return role;
            }
            catch(Exception ex)
            {
                throw new Exception("UserName not found", ex);
            }
        }
    }
}
