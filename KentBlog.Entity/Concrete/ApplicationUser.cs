﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KentBlog.Entity.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
