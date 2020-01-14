using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace B2CMyApp.Controllers {
    public class AboutController : Controller {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index( ) {
            return View();
        }
    }
}