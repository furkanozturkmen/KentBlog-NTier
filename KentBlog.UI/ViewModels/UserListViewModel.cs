﻿namespace KentBlog.UI.ViewModels
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // sadece ilk rolü gösteriyoruz
    }
}
