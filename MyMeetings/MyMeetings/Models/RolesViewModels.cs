using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}