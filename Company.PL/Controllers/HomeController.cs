using System.Diagnostics;
using System.Text;
using Company.PL.Dependency_Injection;
using Company.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScoped scoped1;
        //private readonly IScoped scoped2;
        private readonly ITransient transient1;
        private readonly ITransient transient2;
        private readonly ISingleton singleton1;
        private readonly ISingleton singleton2;

        public HomeController(ILogger<HomeController> logger
                             ,IScoped scoped1
                             //,IScoped scoped2
                             ,ITransient transient1
                             ,ITransient transient2
                             ,ISingleton singleton1
                             ,ISingleton singleton2
                             )
        {
            _logger = logger;
            this.scoped1 = scoped1;
            //this.scoped2 = scoped2;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.singleton1 = singleton1;
            this.singleton2 = singleton2;
        }

        public string Test1()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Scoped1 : {scoped1.GetGuid()}");
            stringBuilder.AppendLine($"Scoped1 : {scoped1.GetGuid()}");
            stringBuilder.AppendLine($"Transient1 : {transient1.GetGuid()}");
            stringBuilder.AppendLine($"Transient2 : {transient2.GetGuid()}");
            stringBuilder.AppendLine($"Singleton1 : {singleton1.GetGuid()}");
            //stringBuilder.AppendLine($"Singleton2 : {singleton2.GetGuid()}");
            return stringBuilder.ToString();
        }

        public string Test2()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Scoped1 : {scoped1.GetGuid()}");
            //stringBuilder.AppendLine($"Scoped2 : {scoped2.GetGuid()}");
            stringBuilder.AppendLine($"Transient1 : {transient1.GetGuid()}");
            //stringBuilder.AppendLine($"Transient2 : {transient2.GetGuid()}");
            stringBuilder.AppendLine($"Singleton1 : {singleton2.GetGuid()}");
            //stringBuilder.AppendLine($"Singleton2 : {singleton2.GetGuid()}");
            return stringBuilder.ToString();
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
