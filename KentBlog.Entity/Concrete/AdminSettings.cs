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
    public class AdminSettings
    {
        [Key]
        public int ID { get; set; }

        public bool SiteStatus { get; set; }
        public bool PhoneTop { get; set; }
        public bool MailTop { get; set; }
        public bool CookieButton { get; set; }
        public bool WhatsAppEffect { get; set; }
        public bool PhoneEffect { get; set; }
        public bool SpotText { get; set; }
        public bool CategoryFooter { get; set; }
        public bool BlogFooter { get; set; }
        public bool OtherCategory { get; set; }
        public string? Title { get; set; }
        public string? MetaKeyword { get; set; }
        public string? MetaDescription { get; set; }
        public string? Favicon { get; set; }
        public string? FooterInfo { get; set; }
        public string? Cookie { get; set; }


        [NotMapped]
        public IFormFile? FaviconFile { get; set; }
    }
}
