﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestingFebruary.Models
{
    public class Person
    {
     
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
