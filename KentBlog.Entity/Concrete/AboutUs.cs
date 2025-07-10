using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace KentBlog.Entity.Concrete
{
    public class AboutUs
    {
        public int Id { get; set; }
        public string? Icon { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }
        public bool Status {  get; set; }
        public int Order {  get; set; }

        [NotMapped] // EF Core bu alanı görmez
        public IFormFile? AboutFile { get; set; }
    }
}
