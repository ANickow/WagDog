using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WagDog.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
            ViewBag.RegisterDog = new RegisterViewModel();
            ViewBag.ProfileDog = new DogProfileViewModel();
            ViewBag.LoginDog = new LoginViewModel();
            return View();
        }

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

        [HttpGet]
        [Route("Search")]
        public IActionResult Search()
        {
            int? dogId = HttpContext.Session.GetInt32("CurrentDog");
            Dog CurrentDog = _context.Dogs.Include(d => d.Interests).ThenInclude(di => di.Interest).Include(d => d.Humans).ThenInclude(f => f.Human).Include(d => d.Animals).ThenInclude(c => c.Animal).SingleOrDefault(dog => dog.DogId == dogId);
            IEnumerable<Dog> Dogs = _context.Dogs.ToList();
            ViewBag.Dogs=_context.Dogs.ToList();
            return View();
        }

        [HttpGet]
        [Route("UserProfile")]
        public IActionResult Profile()
        {
            int? dogId = HttpContext.Session.GetInt32("CurrentDog");
            Dog CurrentDog = _context.Dogs.Include(d => d.Interests).ThenInclude(di => di.Interest).Include(d => d.Humans).ThenInclude(f => f.Human).Include(d => d.Animals).ThenInclude(c => c.Animal).SingleOrDefault(dog => dog.DogId == dogId);
            return View(CurrentDog);
        }

        [HttpPost]
        [Route("PreRegister")]
        public JsonResult PreRegister(RegisterViewModel RegAuth)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("errors", "True");
            response.Add("Name", "");
            response.Add("Email", "");
            response.Add("Password", "");
            response.Add("PassConf", "");
            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.Where(dogs => dogs.Email == RegAuth.Email).SingleOrDefault();
                if(CurrentDog != null) 
                {
                    response["Email"] = "That email already exists";
                } else {
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
                    ViewBag.ProfileDog = NewDog;
                    _context.Dogs.Add(NewDog);
                    _context.SaveChanges();
                    CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Name == NewDog.Name);
                    HttpContext.Session.SetInt32("CurrentDog", CurrentDog.DogId);
                    response["errors"] = "False";
                }
            } else {
                foreach (string key in ViewData.ModelState.Keys){
                    try {
                        string error = ViewData.ModelState.Keys.Where(k => k == key).Select(k => ModelState[k].Errors[0].ErrorMessage).First();
                        response[key] = error;
                    } catch {
                        continue;
                    }
                }
            } 
            return Json(response);
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public JsonResult UpdateProfile(DogProfileViewModel DogModel)
        {
            int? dogId = HttpContext.Session.GetInt32("CurrentDog");
            Dog CurrentDog = _context.Dogs.Include(d => d.Interests).ThenInclude(di => di.Interest).Include(d => d.Humans).ThenInclude(f => f.Human).Include(d => d.Animals).ThenInclude(c => c.Animal).SingleOrDefault(dog => dog.DogId == dogId);
            CurrentDog.Age = DogModel.Age;
            CurrentDog.BodyType = DogModel.BodyType;
            CurrentDog.HighestEducation = DogModel.HighestEducation;
            CurrentDog.Barking = DogModel.Barking; 
            CurrentDog.Accidents = DogModel.Accidents;
            CurrentDog.Breed = DogModel.Breed;
            CurrentDog.Description = DogModel.Description;
            foreach(var animal in DogModel.Animals){
                if(CurrentDog.Animals.Where(a => a.AnimalId == animal).ToList().Count == 0){
                    Cohab newCohab = new Cohab();
                    newCohab.DogId = CurrentDog.DogId;
                    newCohab.AnimalId = animal;
                    CurrentDog.Animals.Add(newCohab);
                    _context.Cohabs.Add(newCohab);
                }
            }   
            foreach(var human in DogModel.Humans){
                if(CurrentDog.Humans.Where(h => h.HumanId == human).ToList().Count == 0){
                    Family newFamily = new Family();
                    newFamily.DogId = CurrentDog.DogId;
                    newFamily.HumanId = human;
                    CurrentDog.Humans.Add(newFamily);
                    _context.Families.Add(newFamily);
                }
            }   
            foreach(var interest in DogModel.Interests){
                if(CurrentDog.Interests.Where(i => i.InterestId == interest).ToList().Count == 0){
                    DogInterest newInterest = new DogInterest();
                    newInterest.DogId = CurrentDog.DogId;
                    newInterest.InterestId = interest;
                    CurrentDog.Interests.Add(newInterest);
                    _context.DogInterests.Add(newInterest);
                }
            } 
            _context.SaveChanges();
            return Json(CurrentDog);
        }

        [HttpPost]
        [Route("ProcessLogin")]
        public JsonResult ProcessLogin(LoginViewModel LoginAuth)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("errors", "True");
            response.Add("Email", "");
            response.Add("Password", "");
            if (ModelState.IsValid)
            {
                Dog CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.Email == LoginAuth.Email);
                if(CurrentDog != null && LoginAuth.Password != null)
                {
                    var Hasher = new PasswordHasher<Dog>();
                    if(0 != Hasher.VerifyHashedPassword(CurrentDog, CurrentDog.Password, LoginAuth.Password)){
                        HttpContext.Session.SetInt32("CurrentDog", (int)CurrentDog.DogId);
                        response["errors"] = "False";
                    } else {
                        response["Password"] = "Incorrect Password";
                    }
                }
                else 
                {
                    response["Email"] = "Does not match our records";
                }
            }
            else
            {
                foreach (string key in ViewData.ModelState.Keys){
                    try {
                        string error = ViewData.ModelState.Keys.Where(k => k == key).Select(k => ModelState[k].Errors[0].ErrorMessage).First();
                        response[key] = error;
                    } catch {
                        continue;
                    }
                }
            } 
            return Json(response);
        }

        [HttpGet]
        [Route("Messages")]
        public IActionResult Messages(){
            int? dogId = HttpContext.Session.GetInt32("CurrentDog");
            // Dog CurrentDog = _context.Dogs.Include(d => d.Interests).ThenInclude(di => di.Interest).Include(d => d.Humans).ThenInclude(f => f.Human).Include(d => d.Animals).ThenInclude(c => c.Animal).SingleOrDefault(dog => dog.DogId == dogId);
            // return View(CurrentDog);
            return View("Messages");
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard(){
            int? dogId = HttpContext.Session.GetInt32("CurrentDog");
            // Dog CurrentDog = _context.Dogs.Include(d => d.Interests).ThenInclude(di => di.Interest).Include(d => d.Humans).ThenInclude(f => f.Human).Include(d => d.Animals).ThenInclude(c => c.Animal).SingleOrDefault(dog => dog.DogId == dogId);
            // return View(CurrentDog);
            return RedirectToAction("Profile", new { DogId = (int)dogId});
        }

        [HttpGet]
        [Route("Profile/{DogId}")]
        public IActionResult Profile(int DogId){
            int? currDogId = HttpContext.Session.GetInt32("CurrentDog");
            ViewBag.currDogId = (int)currDogId;
            Dog ProfileDog = _context.Dogs.Include(d => d.Interests).ThenInclude(di => di.Interest).Include(d => d.Humans).ThenInclude(f => f.Human).Include(d => d.Animals).ThenInclude(c => c.Animal).SingleOrDefault(dog => dog.DogId == DogId);
            return View(ProfileDog);
        }

        [HttpPost]
        [Route("PhotoUpload")]
        public async Task<IActionResult> PhotoUpload(IFormFile Photo)
        {
            int? dogId = HttpContext.Session.GetInt32("CurrentDog");
            Dog CurrentDog = _context.Dogs.SingleOrDefault(dog => dog.DogId == dogId);
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/profiles");
            var fileName = CurrentDog.Name + Path.GetExtension(Photo.FileName);
            CurrentDog.PhotoPath = "/profiles/" + fileName;
            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create)){
                Console.WriteLine("ready to save");
                await Photo.CopyToAsync(fileStream);
            }
            _context.SaveChanges();
            return RedirectToAction("Profile", new{ DogId = dogId});
        }
// MESSAGES ROUTE**********************************************************************
        [HttpPost]
        [Route("sendMessage")]
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