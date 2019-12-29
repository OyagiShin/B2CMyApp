using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers {

    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller {
        public IActionResult Index( int? id) {

            // test 123344
            return View();
        }
    }
}