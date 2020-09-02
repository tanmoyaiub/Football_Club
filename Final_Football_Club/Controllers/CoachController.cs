using Final_Football_Club.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Final_Football_Club.Controllers
{
    public class CoachController : Controller
    {
        // GET: Coach
 /*       public ActionResult Index()
        {
            return View();
        }*/

        // GET: Coach/Details/5
         
        public ActionResult Coach()
        {
            using (ClubEntities dbObj = new ClubEntities())
            {   
                var uname = Session["UserName"];
                Session["Notifications"] = dbObj.Notifications.Where(x=> x.UserName == uname).Count();
                var coach = dbObj.Coachs.Where(x => x.UserName == uname).FirstOrDefault();
                return View(coach);
            }
            
        }

        public ActionResult Details()
        {
            using (ClubEntities dbObj = new ClubEntities())
            {

                var uname = Session["UserName"];
                var coach = dbObj.Coachs.Where(x => x.UserName == uname).FirstOrDefault();
                return View(coach);
            }

        }



        public ActionResult HistoryCreate()
        {
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HistoryCreate(History history)
        {
            if (ModelState.IsValid)
            {
                ClubEntities dbObj = new ClubEntities();
                history.Coach = Session["UserName"].ToString();
                dbObj.Histories.Add(history);
                dbObj.SaveChanges();
                return RedirectToAction("HistoryList");
            }

            return View(history);
        }

        public ActionResult HistoryList()
        {
            ClubEntities dbObj = new ClubEntities();

            return View(dbObj.Histories);
        }

        public ActionResult CoachList()
        {
            ClubEntities dbObj = new ClubEntities();
                return View(dbObj.Coachs.ToList());
        }

        ClubEntities club = new ClubEntities();

        [HttpGet]
        public ActionResult CoachListEdit(string id)
        {

            var result = from item in club.Coachs
                         where item.UserName == id
                         select item;
            Coach personToUpdate = result.FirstOrDefault();

            return View(personToUpdate);
        }

        [HttpPost]
        public ActionResult CoachListEdit(Coach coach, string id)
        {
            var result = from item in club.Coachs
                         where item.UserName == id
                         select item;
            Coach personToUpdate = result.FirstOrDefault();
            personToUpdate.Name = coach.Name;
            personToUpdate.Phone = coach.Phone;
            personToUpdate.Address = coach.Address;
            personToUpdate.Age = coach.Age;
            personToUpdate.Designation = coach.Designation;
            personToUpdate.Email = coach.Email;
            personToUpdate.Password = coach.Password;
            club.SaveChanges();
            SignUp sign = new SignUp();
            var s = from item in club.SignUps
                    where item.UserName == id
                    select item;
            sign = s.FirstOrDefault();
            sign.Name = coach.Name;

            sign.Email = coach.Email;
            sign.Password = coach.Password;
            club.SaveChanges();
            return RedirectToAction("CoachList", "Coach");
        }





       



        /*        // GET: Coach/Create
                public ActionResult Create()
                {
                    return View();
                }

                // POST: Coach/Create
                [HttpPost]
                public ActionResult Create(FormCollection collection)
                {
                    try
                    {
                        // TODO: Add insert logic here

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }

                // GET: Coach/Edit/5
                public ActionResult Edit(int id)
                {
                    return View();
                }

                // POST: Coach/Edit/5
                [HttpPost]
                public ActionResult Edit(int id, FormCollection collection)
                {
                    try
                    {
                        // TODO: Add update logic here

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }

                // GET: Coach/Delete/5
                public ActionResult Delete(int id)
                {
                    return View();
                }

                // POST: Coach/Delete/5
                [HttpPost]
                public ActionResult Delete(int id, FormCollection collection)
                {
                    try
                    {
                        // TODO: Add delete logic here

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }*/
    }
}
