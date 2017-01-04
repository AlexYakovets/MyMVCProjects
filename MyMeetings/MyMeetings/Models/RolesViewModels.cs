using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyMeetings.Models
{
    public class RolesViewModels
    {
        public class EditRoleModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class CreateRoleModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class AddToUser
        {
            public string RoleName { get; set; }
            public string Username { get; set; }

        }
        public class UserRoles
        {
            public IdentityRole Role { get; set; }
            public bool IsAvaible  { get; set; }
        }
    }
}