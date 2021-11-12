using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThreePoint.E_commerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return Content("index");
        }

        public IActionResult ProductCalculation()
        {
            ViewData["title"] = "ProductCalculation";
            ViewBag.Currency = new List<SelectListItem>() {
            new SelectListItem() { Value = "1", Text = "美元" },
            new SelectListItem() { Value = "2", Text = "欧元" }
            };
            ViewBag.SalesRegions = new List<SelectListItem>() {
            new SelectListItem() { Value = "1", Text = "美国" },
            new SelectListItem() { Value = "2", Text = "英国" }
            };
            return View();
        }
    }
}