﻿using Microsoft.AspNetCore.Mvc;
using StudioWeb.Models;
using System.Diagnostics;

namespace StudioWeb.Controllers
{
    public class EquipoController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public EquipoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Equipo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}