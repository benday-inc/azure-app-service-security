using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Benday.EasyAuthDemo.WebUi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Benday.EasyAuthDemo.WebUi.Controllers
{
    public class HomeController : Controller
    {
        [SuppressMessage("csharp", "IDE0052")]
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
