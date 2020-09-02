using Final_Football_Club.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Final_Football_Club.Controllers
{
    public class AdminController : Controller
    {
        ClubEntities club = new ClubEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult AdminDashboard()
        {

            var username = Session["AdminUserName"];

            var sign = club.SignUps.Where(x => x.UserName == username).FirstOrDefault();

            return View(sign);
        }

        public ActionResult UserList()
        {
            return View(club.SignUps.ToList());

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            var result = from item in club.SignUps
                         where item.UserName == id
                         select item;
            SignUp personToUpdate = result.FirstOrDefault();
            return View(personToUpdate);
        }

        [HttpPost]
        public ActionResult Edit(SignUp sign, string id)
        {
            var result = from item in club.SignUps
                         where item.UserName == id
                         select item;
            SignUp personToUpdate = result.FirstOrDefault();
            personToUpdate.Name = sign.Name;
            personToUpdate.Salary = sign.Salary;
            personToUpdate.Email = sign.Email;
            personToUpdate.Password = sign.Password;
            personToUpdate.Type = sign.Type;
            
            club.SaveChanges();
            var test = club.SignUps.Where(x => x.Type == sign.Type).FirstOrDefault();
            if (test.Type == "Player")
            {
                var p = from item in club.Players
                        where item.UserName == id
                        select item;
                Player player = new Player();
                player = p.FirstOrDefault();
                player.Name = sign.Name;
                player.Password = sign.Password;
                player.Email = sign.Email;
                player.Salary = sign.Salary;
               

                club.SaveChanges();
            }
            else if (test.Type == "Coach")
            {
                var c = from item in club.Coachs
                        where item.UserName == id
                        select item;
                Coach coach = new Coach();
                coach = c.FirstOrDefault();
                coach.Name = sign.Name;
                coach.Password = sign.Password;
                coach.Email = sign.Email;
                coach.Salary = sign.Salary;
                club.SaveChanges();

            }


            return RedirectToAction("UserList", "Admin");

        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            SignUp sign = club.SignUps.Where(x => x.UserName == id).FirstOrDefault();
            return View(sign);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfrimDelete(string id)
        {
            SignUp sign = club.SignUps.Where(x => x.UserName == id).FirstOrDefault();
            Player player = club.Players.Where(x => x.UserName == id).FirstOrDefault();
            Coach coach = club.Coachs.Where(x => x.UserName == id).FirstOrDefault();

            club.SignUps.Remove(sign);
            club.SaveChanges();
           /* var test = club.SignUps.Where(x => x.Type == sign.Type).FirstOrDefault();
            if(test.Type == "Player")
            {
                club.Players.Remove(player);
                club.SaveChanges();
            }
            else
            {
                club.Coachs.Remove(coach);
                club.SaveChanges();
            }*/
           
          
       
            return RedirectToAction("UserList", "Admin");
        }


        [HttpGet]
        public ActionResult SendEmail()
        {

            return View();
        }



        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("aatp4367@gmail.com", "Admin");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Test3383@";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Valid Email Needed";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ClubHistory()
        {
            return View(club.Histories.ToList());
        }

        [HttpGet]
        public ActionResult HistoryEdit(int id)
        {

            var result = from item in club.Histories
                         where item.Id == id
                         select item;
            History his = result.FirstOrDefault();
            return View(his);
        }
        [HttpPost]
        public ActionResult HistoryEdit(History h, int id)
        {
            var result = from item in club.Histories
                         where item.Id == id
                         select item;
            History his = result.FirstOrDefault();
            his.Seasons = h.Seasons;
            his.Tournament = h.Tournament;
            his.Coach = h.Coach;
            his.Achivements = h.Achivements;
            club.SaveChanges();
            return RedirectToAction("ClubHistory", "Home");
        }
        [HttpGet]
        public ActionResult HistoryDelete(int id)
        {
            History his = club.Histories.Where(x => x.Id == id).FirstOrDefault();
            return View(his);
        }

        [HttpPost, ActionName("HistoryDelete")]
        public ActionResult ConfrimDelete(int id)
        {
            History his = club.Histories.Where(x => x.Id == id).FirstOrDefault();
            club.Histories.Remove(his);
            club.SaveChanges();

            return RedirectToAction("ClubHistory", "Home");
        }

    }
}