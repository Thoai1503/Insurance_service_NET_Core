﻿using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class InsuranceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
