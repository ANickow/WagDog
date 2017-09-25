using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WagDog.Models;
using System.Linq;

namespace WagDog.Controllers
{
    public class DogController : Controller
    {
        private Context _context;

        public DogController(Context context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("preregister")]
        public IActionResult preregister(RegisterViewModel RegAuth)
        {
            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.SingleOrDefault(dogs => dogs.Email == RegAuth.Email);
                if(CurrentDog != null) 
                {
                    ModelState.AddModelError("Email", "That email already exists");
                    return View("index", RegAuth);
                }
                 Dog NewDog = new Dog
                {
                    Name = RegAuth.Name,
                    Email = RegAuth.Email,
                    Password = RegAuth.Password,
                    

                };
                _context.Add(NewDog);
                _context.SaveChanges();
                CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Email == NewDog.Email);
                HttpContext.Session.SetInt32("CurrentDog", (int)CurrentDog.DogId);
                return Redirect("<LANDING_PAGE>");
            }
            else
            {
                return View("index", RegAuth);
            }
        }

        [HttpPost]
        [Route("updateprofile")]
        public IActionResult updateprofile(RegisterViewModel RegAuth)
        {
            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.SingleOrDefault(dogs => dogs.Email == RegAuth.Email);
                if(CurrentDog != null) 
                {
                    ModelState.AddModelError("Email", "That email already exists");
                    return View("index", RegAuth);
                }
                 Dog NewDog = new Dog
                {
                    Name = RegAuth.Name,
                    Email = RegAuth.Email,
                    Password = RegAuth.Password,
                    Breed = RegAuth.Breed,
                    Age = RegAuth.Age,
                    BodyType = RegAuth.BodyType,
                    HighestEducation = RegAuth.HighestEducation,
                    Barking = RegAuth.Barking,
                    Accidents = RegAuth.Accidents

                };
                _context.Add(NewDog);
                _context.SaveChanges();
                CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Email == NewDog.Email);
                HttpContext.Session.SetInt32("CurrentDog", (int)CurrentDog.DogId);
                return Redirect("<LANDING_PAGE>");
            }
            else
            {
                return View("index", RegAuth);
            }
        }

        [HttpPost]
        [Route("processLogin")]
        public IActionResult processLogin(LoginViewModel LoginAuth)
        {
            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Email == LoginAuth.Email);
                if(CurrentDog != null)
                {
                    HttpContext.Session.SetInt32("CurrentDog", (int)CurrentDog.DogId);
                    return Redirect("<LANDING_PAGE");
                }
                else 
                {
                    ModelState.AddModelError("Email", "Does not match our records");
                    return View("login", LoginAuth);
                }
            }
            else
            {
                return View ("login", LoginAuth);
            }
        }
    }
}