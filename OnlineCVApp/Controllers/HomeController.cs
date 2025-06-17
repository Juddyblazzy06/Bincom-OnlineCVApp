using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineCVApp.Models;


namespace OnlineCvApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult Education() => View();
        public IActionResult Experience() => View();
        public IActionResult Skills() => View();
        [HttpGet]
        public IActionResult TaxCalculator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TaxCalculator(decimal income)
        {
            // Example Nigeria tax logic (very simplified)
            decimal tax = 0;

            if (income <= 300000)
            {
                tax = income * 0.07m; 
            }
            else if (income <= 600000)
            {
                tax = income * 0.11m; 
            }
            else if (income <= 1100000)
            {
                tax = income * 0.15m; 
            }
            else
            {
                tax = income * 0.20m; 
            }

            ViewBag.Income = income;
            ViewBag.Tax = tax;

            return View();
        }
    }
}
