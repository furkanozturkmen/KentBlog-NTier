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
    public class GeneralSettings
    {
        [Key]
        public int ID { get; set; }

        public string? BusinessName { get; set; }
        public string? Mail { get; set; }
        public string? Mail2 { get; set; }
        public string? Logo { get; set; }
        public string? Phone { get; set; }
        public string? Phone2 { get; set; }
        public string? Fax { get; set; }
        public string? Fax2 { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string? GoogleMaps { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }
        public string? WhatsApp { get; set; }
        public string? YouTube { get; set; }
        public string? LinkedIn { get; set; }
        public string? Pinterest { get; set; }
        public string? AboutUs { get; set; }

        [NotMapped]
        public IFormFile? LogoFile { get; set; }
    }
}
