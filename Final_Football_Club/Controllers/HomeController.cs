using Final_Football_Club.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Football_Club.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ClubEntities club = new ClubEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            SignUp[] signups = club.SignUps.ToArray();
            return View();

        }
        [HttpPost]
        public ActionResult Signup(SignUp sign)
        {

            if (ModelState.IsValid)
            {

                //Signup s = new Signup();
                club.SignUps.Add(sign);

                try
                {
                    club.SaveChanges();
                }

                catch(DbUpdateException e)
                {
                    ModelState.AddModelError("UserName", "Username allready existed");

                    return View(sign); 
                }



                var test = club.SignUps.Where(x => x.Type == sign.Type).FirstOrDefault();
                if (test.Type == "Player")
                {
                    Player p = new Player();
                    p.UserName = sign.UserName;
                    p.Password = sign.Password;
                    p.Salary = sign.Salary;
                    p.Email = sign.Email;
                    p.Name = sign.Name;

                    club.Players.Add(p);
                    club.SaveChanges();
                    return RedirectToAction("PlayerList");
                }
                else
                {
                    Coach c = new Coach();
                    c.UserName = sign.UserName;
                    c.Password = sign.Password;
                    c.Salary = sign.Salary;
                    c.Email = sign.Email;
                    c.Name = sign.Name;
                    
                    club.Coachs.Add(c);
                    club.SaveChanges();
                    return RedirectToAction("CoachList");



                }

            }

            return View(sign);


        }


        public ActionResult UserList()
        {
            return View(club.SignUps.ToList());

        }

        public ActionResult PlayerList()
        {
            return View(club.Players.ToList()); ;

        }

        public ActionResult CoachList()
        {
            return View(club.Coachs.ToList()); ;

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(SignUp log)
        {
            var logins = club.SignUps.Where(x => x.UserName == log.UserName && x.Password == log.Password).FirstOrDefault();
            if (logins != null)
            {
                if (logins.Type == "Player")
                {
                   
                    Session["UserName"] = log.UserName;
                    Session["PlayerUsername"] = log.UserName;
                    return RedirectToAction("PlayerProfile", "Player");
                }
                else if (logins.Type == "Coach")
                {
                    
                    Session["UserName"] = log.UserName;
                    Session["Name"] = log.Name;
                    return RedirectToAction("Details", "Coach");
                }
                else
                {
                    Session["UserName"] = log.UserName;
                    Session["AdminUserName"] = log.UserName;
                    return RedirectToAction("AdminDashboard" , "Admin");
                }


            }
            else
            {
                ViewBag.Errormessage = "Username or Password is incorrect";
                return View("Login", log);

            }

        }

        [HttpGet]
        public ActionResult Edit()
        {

            string id = Session["UserName"].ToString();
            var result = from item in club.SignUps
                         where item.UserName == id
                         select item;
            SignUp personToUpdate = result.FirstOrDefault();
            return View(personToUpdate);
        }

        [HttpPost]
        public ActionResult Edit(SignUp sign)
        {
            string id = Session["UserName"].ToString();
            var result = from item in club.SignUps
                         where item.UserName == id
                         select item;
            SignUp personToUpdate = result.FirstOrDefault();
            personToUpdate.Name = sign.Name;
            personToUpdate.Salary = sign.Salary;
            personToUpdate.Email = sign.Email;
            personToUpdate.Password = sign.Password;
            club.SaveChanges();
               
            if (personToUpdate.Type == "Player")
            {
                var p = from item in club.Players
                        where item.UserName == id
                        select item;
                Player player = new Player();
                player = p.FirstOrDefault();
                player.Name = sign.Name;
                player.Password = sign.Password;
                player.Email = sign.Email;
                club.SaveChanges();
            }
            else
            {
                var c = from item in club.Coachs
                        where item.UserName == id
                        select item;
                Coach coach = new Coach();
                coach = c.FirstOrDefault();
                coach.Name = sign.Name;
                coach.Password = sign.Password;
                coach.Email = sign.Email;
                club.SaveChanges();

            }


            return RedirectToAction("Details", "Coach");

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}