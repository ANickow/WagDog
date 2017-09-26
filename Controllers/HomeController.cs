using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WagDog.Models;
using System.Linq;

namespace WagDog.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }
       
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }

}