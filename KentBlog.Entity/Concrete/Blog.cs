using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? Date { get; set; }
        public string? SeoTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeyword { get; set; }
        public string? Gallery { get; set; }
        public string? Tags { get; set; }
        public bool BlogStatus { get; set; }
        public bool CategoryStatus { get; set; }
        public bool BlogHide { get; set; }
        public bool MainPageStatus { get; set; }
        public bool SliderStatus { get; set; }
        public int Order { get; set; }

        [NotMapped]
        public IFormFile? BlogFile { get; set; }

        // Category ilişkisi
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }



    }
}
