using Final_Football_Club.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Football_Club.Controllers
{
    public class TeamController : Controller
    {
        ClubEntities team = new ClubEntities();
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }
        public List<SelectListItem> GetPlayer()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = team.Players;
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.UserName, Text = item.UserName });
            }
            return list;
        }
        public List<SelectListItem> GetMatch()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = team.Matches;
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Date+"" });
            }
            return list;
        }

        public List<Player> getAllPlayers() {
            return team.Players.OrderBy(x=>x.Position).ToList();
        } 
        public ActionResult SelectPlayerForTeam()
        {

            ViewBag.Allplayer = getAllPlayers();
           /* ViewBag.GetPlayer = GetPlayer();*/
            ViewBag.GetMatch = GetMatch();
            return View();
        }
        [HttpPost]
        public ActionResult SelectPlayerForTeam(TeamsPlayer item)
        {
            ViewBag.Allplayer = getAllPlayers();
            /*ViewBag.GetPlayer = GetPlayer();*/
            ViewBag.GetMatch = GetMatch();

            if (team.TeamForMatches.Where(x=> x.MatchID == item.MatchID).Count()>= 14)
            {
                ViewBag.Error = "Already 14 players existed!!";
                return View();
            }

            DateTime date =  team.Matches.Find(item.MatchID).Date ?? DateTime.Now;
/*            if (item.Match != null)
            {
                date = item.Match.Date ;
            }*/

            foreach(var data in item.PlayerUserName)
            {
                TeamForMatch tdm = new TeamForMatch();
                tdm.MatchID = item.MatchID;
                tdm.Note = item.Note;
                tdm.status = item.status;
                tdm.Team = item.Team;
                tdm.Username = data;
                team.TeamForMatches.Add(tdm);

                team.Notifications.Add(new Notification { UserName = data, Title = "You are selected.", Description = "You are selected for " + date.ToString("dd/MM/yy") + " .Please check Proposed_Selectted_Player to Details.", Date = DateTime.Now });
                team.SaveChanges();
            }

            
           
            
            
            
           
            return RedirectToAction("Coach" , "Coach");
        }

        public ActionResult ProposedTeam()
        {

            return View(team.TeamForMatches.OrderByDescending(x=> x.Match.Date).ToList());
        }

    }
}