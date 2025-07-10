using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class ThemeSettings
    {
        [Key]
        public int ID { get; set; }

        public string? ThemeValue { get; set; }
        public string? SliderHeight { get; set; }
        public string? TopBarDesign { get; set; }
        public string? PrivateCSS { get; set; }
        public string? ThemeColor { get; set; }
        public string? TopImage { get; set; }
        public string? FooterImage { get; set; }
        public string? CategoryType { get; set; }
        public string? BlogType { get; set; }
        public string? BlogColumn { get; set; }

    }
}
