using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace newProject.Controllers
{
    public class EventController : Controller
    {
         private ActivityContext _context;
 
        public EventController(ActivityContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard")]
        //display the dashboard showing all activities and the option to join, delete, leave 
        public IActionResult Dashboard()
        {
            //check whether user is logged in, and if so get session variables (user id and name)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Index", "User");
            }
            ViewBag.userid = userId;
            ViewBag.username = HttpContext.Session.GetString("Username");
            
            //get all activities to show 
            ViewBag.activities = _context.Activities.Include(w => w.Participants).ToList();

            //collection of activities
            List<Participant> participants = _context.Participants.Where(p => p.UserId == userId).ToList();
            List<int> activitiesAttending = new List<int>();
            foreach (Participant p in participants) 
            {
                activitiesAttending.Add(p.ActivityId);
            }
            ViewBag.attending = activitiesAttending;
            return View();
        }
      

        [HttpGet]
        [Route("add")]
        //show the page to add a trip
        public IActionResult ShowAdd() {
            //check whether user is logged in == get session variables (user id and name)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Index", "User");
            }
            ViewBag.userid = userId;
            ViewBag.username = HttpContext.Session.GetString("Username");
            return View("Add");
        }

        [HttpPost]
        [Route("create")]
        //validate the submission and add the activity to the db
        public IActionResult CreateActivity(Activity act) {
            //check whether user is logged in, and if so get session variables (user id and name)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Index", "User");
            }
            if (ModelState.IsValid) 
            {
                //set current user as activity creator and add to db
                act.UserId = (int)userId;
                _context.Add(act);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            //show add page with errors
            return View("Add");
        }

          [HttpGet]
        [Route("show/{id}")]
        //show a specific activity 
        public IActionResult Show(int id) {
            //check whether user is logged in, and if so get session variables (user id and name)
            
           
            ViewBag.activity = _context.Activities.Where(a => a.ActivityId == id).Include(a => a.Participants).ThenInclude(p => p.User).SingleOrDefault();
       
            return View();
        }

         [HttpGet]
        [Route("join/{Aid}")]
        //add user to guest list for the given wedding -- USE POST
        public IActionResult JOIN(int Aid) {
            //check whether user is logged in, and if so get session variables (user id and name)
            int? uId = HttpContext.Session.GetInt32("UserId");
            if (uId == null) {
                return RedirectToAction("Index", "User");
            }
            //create participant and add to db
            Participant newPart = new Participant {
                ActivityId = Aid,
                UserId = (int)uId
            };
            _context.Participants.Add(newPart);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

         [HttpGet]
        [Route("leave/{Anid}")]
        //remove user from partipants' list for the given wedding -- USE POST
        public IActionResult LEAVE(int Anid) {
            //check whether user is logged in, and if so get session variables (user id and name)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Index", "User");
            }
            //get participant to then delete from db
            Participant toDelete = _context.Participants.Where(g => g.ActivityId == Anid && g.UserId == userId).SingleOrDefault();
            _context.Participants.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        
         [HttpGet]
        [Route("delete/{id}")]
    
        public IActionResult DeleteEvent(int id) {
            Activity act = _context.Activities.Where(w => w.ActivityId == id).SingleOrDefault();
            int? userId = HttpContext.Session.GetInt32("UserId");
            //validate the user deleting is the user who created the activity
            if (userId == act.UserId) {
                _context.Activities.Remove(act);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
   }
}
    