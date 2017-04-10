using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AccountingSite.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string Login)
        {
            string[] role = new string[] { };
            using (ManageContext db = new ManageContext())
            {
                // Получаем пользователя
                Employee Employee = db.Employees.FirstOrDefault(u => u.Login == Login);
                if (Employee != null)
                {
                    // Получаем роль
                    Role userRole = db.Roles.Find(Employee.RoleId);
                    if (userRole != null)
                    {
                        role = new string[] { userRole.Name };
                    }
                }
            }
            return role;
        }
        public override void CreateRole(string roleName)
        {
            Role newRole = new Role() { Name = roleName };
            ManageContext db = new ManageContext();
            db.Roles.Add(newRole);
            db.SaveChanges();
        }
        public override bool IsUserInRole(string Login, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (ManageContext db = new ManageContext())
            {
                // Получаем пользователя
                Employee Employee = db.Employees.FirstOrDefault(u => u.Login == Login);
                if (Employee != null)
                {
                    // получаем роль
                    Role userRole = db.Roles.Find(Employee.RoleId);
                    //сравниваем
                    if (userRole != null && userRole.Name == roleName)
                    {
                        outputResult = true;
                    }
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}