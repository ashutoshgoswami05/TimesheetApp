using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using TimesheetApp.Context;

namespace TimesheetApp.Models
{
    public class UserRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
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

        public override string[] GetRolesForUser(string username)
        {
            using (Timesheet_projectEntities context = new Timesheet_projectEntities())
            {
                int count = context.Users.Where(x => x.Employee_Id == username).Count();
                var userRoles = context.Users.Where(x => x.Employee_Id == username).Join(context.Roles, u => u.Role_Id, r => r.Role_Id, (u, r) => r.Role_Name).ToArray();
               

                
                return userRoles;
            
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
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