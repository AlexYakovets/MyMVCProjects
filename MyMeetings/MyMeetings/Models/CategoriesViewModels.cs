using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMeetings.Models
{
    public class CategoriesViewModels
    {
        public class EditCategoryModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class CreateCategoryModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class AddToPublication
        {
            public string RoleName { get; set; }
            public string Username { get; set; }

        }
}
}