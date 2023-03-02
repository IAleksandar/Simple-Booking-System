using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;

namespace AvailabilityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private SQLiteConnection Sqliteconnection;
        private IRepository _repo;

        public AvailabilityController()
        {
            Sqliteconnection = Data.ConnectionClass.CreateConnection();
            _repo = new Repository.Repository();
        }

        [HttpGet(Name = "availability")]
        public bool CheckIfResourceAvailable(Booking booking)
        {
            int totalQuantity = _repo.ReadDataResources(Sqliteconnection).Where(x => x.Id == booking.ResourceId).FirstOrDefault().Quantity;

            if (booking.BookedQuantity > totalQuantity)
            {
                return false;
            }

            List<Domain.Booking> bookings = _repo.ReadDataBookings(Sqliteconnection, booking.ResourceId);

            if (bookings != null && bookings.Count > 0)
            {
                bookings = bookings.Where(x => x.DateTo > booking.DateFrom && x.DateFrom < booking.DateTo).ToList();

                if (bookings.Sum(x => x.BookedQuantity) + booking.BookedQuantity > totalQuantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}