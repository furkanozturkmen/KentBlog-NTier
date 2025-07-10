using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class Keywords
    {
        [Key]
        public int KeywordID { get; set; }
        public string? KeywordName { get; set; }
        public string? KeywordLink { get; set; }

        public bool Status { get; set; }

        public int Order { get; set; }
    }
}
