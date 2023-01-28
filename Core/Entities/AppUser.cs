﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = null!;
    public bool IsActive { get; set; }
}