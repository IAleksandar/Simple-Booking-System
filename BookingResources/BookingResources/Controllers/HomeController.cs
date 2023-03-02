using AvailabilityService.Controllers;
using BookingResources.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Data.SQLite;
using System.Diagnostics;

namespace BookingResources.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SQLiteConnection Sqliteconnection;
        private IRepository _repo;
        private AvailabilityController service;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Sqliteconnection = Data.ConnectionClass.CreateConnection();
            _repo = new Repository.Repository();
            service = new AvailabilityController();
        }

        public IActionResult Index(string message)
        {
            ResourceViewModel model = new ResourceViewModel(message, _repo.ReadDataResources(Sqliteconnection));
            if (model.Resources == null || model.Resources.Count == 0)
            {
                _repo.InsertData(Sqliteconnection);
                model.Resources = _repo.ReadDataResources(Sqliteconnection);
            }
            //_repo.DeleteData(Sqliteconnection);

            return View(model);
        }
        [HttpGet]
        public IActionResult BookResource(int Id)
        {
            ViewBag.Resource = Id;
            return PartialView("_Booking");
        }

        [HttpPost]
        public IActionResult BookResource(Domain.Booking booking)
        {
            if(booking.DateFrom > booking.DateTo)
            {
                return RedirectToAction("Index", new { message = "Please select a valid period, date from must be before date to." });
            }
            if(booking.DateFrom < DateTime.Now)
            {
                return RedirectToAction("Index", new { message = "The selected period is in the past." });
            }
            if (!service.CheckIfResourceAvailable(booking))
            {
                return RedirectToAction("Index", new {message = "The quantity is unavailable for selected period." });
            }
            ViewBag.Error = "";
            _repo.InsertDataBooking(Sqliteconnection, booking.DateFrom, booking.DateTo, booking.BookedQuantity, booking.ResourceId);
            return RedirectToAction("Index", new { message = "The resource with Id:" + booking.ResourceId + " was successfully booked for period " + booking.DateFrom.ToString("dd/MM/yyyy") + " - " + booking.DateTo.ToString("dd/MM/yyyy") + "." });
        }    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}