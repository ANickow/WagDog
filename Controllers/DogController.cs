using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        // [HttpGet]
        // [Route("")]
        // public IActionResult Index()
        // {
        //     return View();
        // }

        // TEST ROUTE LANDING_PAGE
        [HttpGet]
        [Route("LANDING_PAGE")]
        
        
        public IActionResult LANDING_PAGE()
        {
            int? loggedInId = HttpContext.Session.GetInt32("CurrentDog");
            if (loggedInId == null){
                return RedirectToAction("index");;
            }
            return View();
        }

        [HttpPost]
        [Route("PreRegister")]
        public IActionResult PreRegister(RegisterViewModel RegAuth)
        {
            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.Where(dogs => dogs.Email == RegAuth.Email).SingleOrDefault();
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
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now

                };
                PasswordHasher<Dog> Hasher = new PasswordHasher<Dog>();
                NewDog.Password = Hasher.HashPassword(NewDog, NewDog.Password);
                _context.Dogs.Add(NewDog);
                _context.SaveChanges();
                CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Name == NewDog.Name);
                HttpContext.Session.SetInt32("CurrentDog", CurrentDog.DogId);
                ViewBag.CurrentDog = CurrentDog;
                return View("LANDING_PAGE");
            }
            else
            {
                return View("index", RegAuth);
            }
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile(RegisterViewModel RegAuth)
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
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now

                };
                _context.Add(NewDog);
                _context.SaveChanges();
                CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Email == NewDog.Email);
                HttpContext.Session.SetInt32("CurrentDog", (int)CurrentDog.DogId);
                return Redirect("LANDING_PAGE");
            }
            else
            {
                return View("index", RegAuth);
            }
        }

        [HttpPost]
        [Route("ProcessLogin")]
        public IActionResult ProcessLogin(LoginViewModel LoginAuth)
        {
            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Email == LoginAuth.Email);
                if(CurrentDog != null)
                {
                    HttpContext.Session.SetInt32("CurrentDog", (int)CurrentDog.DogId);
                    return Redirect("LANDING_PAGE");
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
// MESSAGES ROUTE**********************************************************************
        [HttpPost]
        [Route("PostMessage")]
        public IActionResult PostMessage(int Id, MessageViewModel Message)
        {

            int? loggedInId = HttpContext.Session.GetInt32("CurrentDog");
            if (loggedInId == null){
                // WHERE DO YOU WANT TO GO IF NOT LOGGED IN????**********************
                return RedirectToAction("index");;
            }

            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.Where(dogs => dogs.DogId == loggedInId).SingleOrDefault();
                
                Message NewMessage = new Message
                {
                    SenderId = CurrentDog.DogId,
                    MessageContent = Message.MessageContent,
                    ReceiverId = Id,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now

                };
                _context.Messages.Add(NewMessage);
                _context.SaveChanges();



                // REDIRECT REDIRECT REDIRECT 
                HttpContext.Session.SetInt32("CurrentDog", CurrentDog.DogId);
                ViewBag.CurrentDog = CurrentDog;
                return View("LANDING_PAGE");
            }
            else
            {
                return View("index", Message);
            }
        }
    }
}