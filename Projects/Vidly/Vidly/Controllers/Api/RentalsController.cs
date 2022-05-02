using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {

        ApplicationDbContext _context;


        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Rentals
        //api/rentals

        public IEnumerable<RentalDto> GetRentals(string query = null, bool pending = false)
        {
            var rentalsList = new List<RentalDto>();

            var rentals = new List<Rental>();

            if (!string.IsNullOrWhiteSpace(query))
            {
                rentals = _context.Rentals
                .Include(c => c.Customer)
                .Where(r => r.DateReturned.HasValue == pending)
                .Where(c => c.Customer.Name.Contains(query))
                .Include(m => m.Movie).ToList();
            }
            else
            {
                rentals = _context.Rentals
                .Include(c => c.Customer)
                .Where(r => r.DateReturned.HasValue == pending)
                .Include(m => m.Movie).ToList();
            }


            foreach (var rental in rentals)
            {
                var rentalDto = new RentalDto
                {
                    CustomerId = rental.Customer.Id,
                    CustomerName = rental.Customer.Name,
                    RentalId = rental.Id,
                    MovieId = rental.Movie.Id,
                    MovieName = rental.Movie.Name,
                    RentedAt = rental.DateRented,
                    ReturnedAt = rental.DateReturned
                };

                rentalsList.Add(rentalDto);
            }



            //if (!String.IsNullOrWhiteSpace(query))
            //{
            //    rentals = _context.Rentals
            //        .Include(c => c.Customer)
            //        .Where(r => r.DateReturned.HasValue == pending)
            //        .Where(cn => cn.Customer.Name.Contains(query))
            //    .GroupBy(n => new { n.Customer.Name, n.Customer.Id })
            //    .Select(t => new RentalDto
            //    {
            //        CustomerId = t.Key.Id,
            //        CustomerName = t.Key.Name.ToString(),
            //        Rentals = t.Count()
            //    }).ToList();
            //}
            //else
            //{
            //    rentals = _context.Rentals
            //        .Include(c => c.Customer)
            //        .Where(r => r.DateReturned.HasValue == pending)
            //        .GroupBy(n => new { n.Customer.Name, n.Customer.Id })
            //    .Select(t => new RentalDto
            //    {
            //        CustomerId = t.Key.Id,
            //        CustomerName = t.Key.Name.ToString(),
            //        Rentals = t.Count()
            //    }).ToList();
            //}


            //foreach (var rental in rentals)
            //{
            //    rentalsList.Add(rental);
            //}

            //var rentals = _context.Rentals.Where(d => d.DateReturned == null).Include(c => c.Customer).GroupBy(c=>c.Customer.Name).Select(m=> new { Name = m.Key, total = m.Count()});

            //foreach (var rental in rentals)
            //{
            //    var rentalDto = new RentalDto
            //    {
            //        Id = rental.Id,
            //        CustomerId = rental.Customer.Id,
            //        CustomerName = rental.Customer.Name,
            //        MovieId = rental.Movie.Id,
            //        MovieName = rental.Movie.Name,
            //        DateRented = rental.DateRented,
            //        DateReturned = rental.DateReturned,
            //        NumberOfMovies = rental.Movie.Length

            //    };

            //    rentalsList.Add(rentalDto);

            //};


            return rentalsList;
        }

        public IHttpActionResult GetRentalsDetail(int Id, bool pending = false)
        {

            var rentalsList = new List<RentalDto>();

            var rentals = new List<Rental>();


            rentals = _context.Rentals
                .Where(c => c.Customer.Id == Id)
                .Include(c => c.Customer)
                .Include(m => m.Movie).ToList();


            foreach (var rental in rentals)
            {
                var rentalDto = new RentalDto
                {
                    CustomerId = rental.Customer.Id,
                    CustomerName = rental.Customer.Name,
                    RentalId = rental.Id,
                    MovieId = rental.Movie.Id,
                    MovieName = rental.Movie.Name,
                    RentedAt = rental.DateRented,
                    ReturnedAt = rental.DateReturned
                };

                rentalsList.Add(rentalDto);
            }

            return Ok(rentalsList);
        }


        //PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult DeleteMovie(int id)
        {

            var movieInDb = _context.Rentals.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();


            movieInDb.DateReturned = DateTime.Now;

            _context.SaveChanges();

            return Ok();
        }
    }
}
