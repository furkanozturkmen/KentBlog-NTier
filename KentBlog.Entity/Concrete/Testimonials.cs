using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class Testimonials
    {
        public int Id { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }

        [NotMapped]
        public IFormFile? TestimonialFile { get; set; }
    }
}
