﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TestingFebruary.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
    }
}