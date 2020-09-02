using Final_Football_Club.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Final_Football_Club.Controllers
{
    public class PlayerController : Controller
    {
        ClubEntities club = new ClubEntities();
        public ActionResult PlayerList()
        {
            return View(club.Players.ToList());

        }

        [HttpGet]
        public ActionResult PlayerListEdit(string id)
        {

            var result = from item in club.Players
                         where item.UserName == id
                         select item;
            Player personToUpdate = result.FirstOrDefault();

           
            return View(personToUpdate);
        }

        [HttpPost]
        public ActionResult PlayerListEdit(Player player, string id)
        {
            var result = from item in club.Players
                         where item.UserName == id
                         select item;
            Player personToUpdate = result.FirstOrDefault();
            personToUpdate.Name = player.Name;
            personToUpdate.Phone = player.Phone;
            personToUpdate.Address = player.Address;
            personToUpdate.Age = player.Age;
            personToUpdate.Position = player.Position;
            personToUpdate.Email = player.Email;
            personToUpdate.Password = player.Password;

            if (Request.Files.Count > 0)
            {
                var postedFile = Request.Files[0];
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    string imagesPath = HttpContext.Server.MapPath("~/Content/Images");
                    string extension = Path.GetExtension(postedFile.FileName);
                    string newFileName = Session["UserName"] + extension;
                    string saveToPath = Path.Combine(imagesPath, newFileName);
                    personToUpdate.Image = newFileName;
                    postedFile.SaveAs(saveToPath);
                }
            }



            club.SaveChanges();
            SignUp sign = new SignUp();
            var s = from item in club.SignUps
                         where item.UserName == id
                         select item;
            sign = s.FirstOrDefault();
            sign.Name = player.Name;
            sign.Email = player.Email;
            sign.Password = player.Password;






            club.SaveChanges();
            return RedirectToAction("PlayerList", "Player");
        }
        [HttpGet]
        public ActionResult PlayerProfile()
        {
            var username = Session["PlayerUsername"];
            
            Session["Notifications"] = club.Notifications.Where(x => x.UserName == username).Count();
            var player = club.Players.Where(x => x.UserName == username).FirstOrDefault();

            return View(player);
        }


        public ActionResult ClubHistory()
        {
            return View(club.Histories.ToList());
        }
    }

}