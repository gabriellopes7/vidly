using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;


        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Rentals
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Index()
        {
            var rentals = _context.Rentals
                .Include(c => c.Customer)
                .GroupBy(n => new { n.Customer.Name, n.Customer.Id })
                .Select(t => new IndexRentalsViewModel
                {
                    CustomerId = t.Key.Id,
                    CustomerName = t.Key.Name.ToString(),
                    RentalsCount = t.Count()
                }).ToList();

            var rentalsCount = new List<IndexRentalsViewModel>();

            foreach (var rental in rentals)
            {
                rentalsCount.Add(rental);
            }



            return View(rentalsCount);
        }


        [Route("rentals/{id}")]
        public ActionResult Details(int id)
        {

            return View(id);
        }
    }
}