using Final_Football_Club.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Football_Club.Controllers
{
    public class NotificationController : Controller
    {
        ClubEntities db = new ClubEntities();
        // GET: Notification
        public ActionResult Index()
        {
            var data = Session["UserName"];
            return View(db.Notifications.Where(x=> x.UserName == data).OrderByDescending(x=> x.Date).ToList());
        }

        public ActionResult Create()
        {
            

            ViewBag.UserName = new SelectList(db.SignUps, "UserName", "Name");
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Notification noti)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(noti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            ViewBag.UserName = new SelectList(db.SignUps, "UserName", "Name", noti.UserName);
            return View(noti);
        }

        // GET: Notification/Details/5

    }
}
