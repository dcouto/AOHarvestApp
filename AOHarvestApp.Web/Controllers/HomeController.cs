﻿using AOHarvestApp.Web.ViewModels.Home;
using System.Linq;
using System.Web.Mvc;
using AOHarvestApp.Manager.Interfaces;

namespace AOHarvestApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHarvestManager _harvestManager;
        private readonly IEmailManager _emailManager;

        public HomeController() { }

        public HomeController(IHarvestManager harvestManager, IEmailManager emailManager)
        {
            _harvestManager = harvestManager;
            _emailManager = emailManager;
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDayEntries(GetDayEntriesViewModel model)
        {
            var vm = new GetDayEntriesViewModel();

            _harvestManager.SubDomain = model.SubDomain;
            _harvestManager.Email = model.Email;
            _harvestManager.Password = model.Password;

            vm.DayEntries = _harvestManager.GetDayEntries();

            return View(vm);
        }

        [HttpPost]
        public ActionResult SendIncompleteDailyEntriesEmail(SendIncompleteDailyEntriesEmailViewModel model)
        {
            try
            {
                _emailManager.SendIncompleteDailyEntriesEmail(model.Email);
                model.EmailSuccessfullySent = true;
            }
            catch
            {
                model.EmailSuccessfullySent = false;
            }

            return View(model);
        }
    }
}