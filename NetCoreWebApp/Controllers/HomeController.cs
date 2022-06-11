using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreWebApp.Models;
using System.Threading.Tasks;
using System.Xml;

namespace NetCoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {           
            return View();
        }
        
        public void DoSomething(HttpContext ctx, string employeeName)
        {
            //XML Injection vulnerability
             using (XmlWriter writer = XmlWriter.Create("employees.xml"))
            {
                writer.WriteStartDocument();

                // BAD: Insert user input directly into XML
                writer.WriteRaw("<employee><name>" + employeeName + "</name></employee>");

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            //Hardcoded pwd
            string password = ctx.Request.Query["password"];
 
            if (password == "AAABBB")
            {
                ctx.Response.Redirect("login");
            }
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
