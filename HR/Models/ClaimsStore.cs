using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HR.Models
{
    public class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Create Role permission", "Create Role permission"),
            new Claim("Edit Role permission", "Edit Role permission"),
            new Claim("Delete Role permission", "Delete Role permission"),
            new Claim("EditUsersInRole permission", "EditUsersInRole permission"),
            new Claim("ManageUserClaims permission", "ManageUserClaims permission"),
            new Claim("AddUser permission", "AddUser permission"),
            new Claim("DeleteUser permission", "DeleteUser permission"),
            new Claim("EditUser permission", "EditUser permission"),
            new Claim("ViewEditUser permission", "ViewEditUser permission"),
            new Claim("ManageUserRoles permission", "ManageUserRoles permission"),
            new Claim("DownloadAsExel permission", "DownloadAsExel permission"),
            new Claim("AddUserDetails permission", "AddUserDetails permission"),
            new Claim("EditUserDetails permission", "EditUserDetails permission"),
            new Claim("DownloadAllAsExel permission", "DownloadAllAsExel permission"),
            new Claim("Edit&DeleteLeave permission", "Edit&DeleteLeave permission"),
            new Claim("Edit&AndOfficialDetails permission", "Edit&AndOfficialDetails permission"),
        };
    }
}
