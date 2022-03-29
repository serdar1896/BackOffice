using Inveon.Admin.Models;
using Inveon.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Inveon.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IProductService productService;


        public HomeController(IAccountService accountService, IProductService productService)
        {
            this.accountService = accountService;
            this.productService = productService;
        }
        public IActionResult Index()
        {
            var accountCount =  accountService.GetAllAsync().Result.ToList().Count();
            var productCount = productService.GetAllAsync().Result.ToList().Count();
            ViewBag.TotalAccount = accountCount;
            ViewBag.TotalProduct = productCount;

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
