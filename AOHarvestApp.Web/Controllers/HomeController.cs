using System.Linq;
using AOHarvestApp.Web.ViewModels.Home;
using System.Web.Mvc;
using AOHarvestApp.Manager.Interfaces;
using AOHarvestApp.Models;
using AOHarvestApp.Models.Requests;

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
        public ActionResult GetDailyEntries(GetDayEntriesViewModel model)
        {
            var vm = new GetDayEntriesViewModel();

            _harvestManager.SubDomain = model.SubDomain;
            _harvestManager.Email = model.Email;
            _harvestManager.Password = model.Password;

            var request = new GetDailyEntriesRequest();
            request.DayOfTheYear = model.DayOfTheYear;
            request.Year = model.Year;
            request.UserId = model.UserId;

            var response = _harvestManager.GetDailyEntries(request);

            vm.DailyEntries = response != null ? response.Entries : Enumerable.Empty<DayEntry>();

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