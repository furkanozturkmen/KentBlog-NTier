using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class Slider
    {

        [Key]
        public int SliderID { get; set; }
        public string SliderImage { get; set; }
        public string SliderTitle { get; set; }
        public string SliderDescription { get; set; }
        public string SliderButtonText { get; set; }
        public string SliderButtonLink { get; set; }
        public bool SliderStatus { get; set; }


    }
}
