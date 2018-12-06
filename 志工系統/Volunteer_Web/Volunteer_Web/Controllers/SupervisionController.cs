using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;

namespace Volunteer_Web.Controllers
{
    public class SupervisionController : Controller
    {
        VolunteerEntities dbContext = new VolunteerEntities();
        // GET: Supervision
        public ActionResult Index()
        {
            return View();
        }
        //
        public ActionResult Experience(int id=1)
        {
            Supervision_Experience_VM supervision_Experience_VM = new Supervision_Experience_VM();
            supervision_Experience_VM.Service_groups = dbContext.Service_group.ToList();
            supervision_Experience_VM.Experiences = supervision_Experience_VM.SelectExperience_byGroup(id);
            return View(supervision_Experience_VM);
        }
        public ActionResult Experience_Insert()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id= 0)
        {
            Experience_VM experience_VM = new Experience_VM();
            experience_VM.SelectExperiencebyExperience_no(id);
            return View(experience_VM);
        }
        [HttpPost]
        public ActionResult Edit(Experience_VM experience)
        {
            var test = Request.Form["Experience"];

            Experience_VM experience_VM = new Experience_VM();
            experience_VM.UpdateExperience(experience);
            return RedirectToAction("Index");
        }
        public ActionResult Issued(int id = 0)
        {
            return RedirectToAction("Index");
        }
    }
}