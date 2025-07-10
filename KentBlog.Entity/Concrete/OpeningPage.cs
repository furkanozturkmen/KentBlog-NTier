using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class OpeningPage
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? SectionTitle { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
        public string? SectionKey { get; set; }
        public string? SectionColor { get; set; }


    }
}
