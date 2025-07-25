﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KentBlog.Entity.Concrete
{
    public class Counter
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public string? Symbol { get; set; }
        public string? Title { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }
    }
}
