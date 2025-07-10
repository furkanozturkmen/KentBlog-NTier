using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace KentBlog.UI.ViewModels
{
    public class CreateUserViewModel
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SelectedRole { get; set; }

        public List<SelectListItem>? Roles { get; set; }
    }
}
