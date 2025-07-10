using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class Visitor
    {
        [Key]
        public int VisitorIP { get; set; }
        public string VisitorDescription { get; set; }
        public DateTime VisitorDateTime { get; set; }
    }
}
