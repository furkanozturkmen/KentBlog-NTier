using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }
        public string? PageType { get; set; }
        public string? SubMenuSelect { get; set; }
        public string? MenuAdres { get; set; }
        public string? Name { get; set; }
        public string? MenuInfo { get; set; }
        public bool MenuStatus { get; set; }
        public bool MenuHide { get; set; }
        public bool MenuChecked { get; set; }
        public bool MenuTarget { get; set; }
        public string? SeoTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MenuKey { get; set; }

        public int Order { get; set; }
    }
}
