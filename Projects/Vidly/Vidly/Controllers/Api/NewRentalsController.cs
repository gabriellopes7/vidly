using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        public ApplicationDbContext _context;


        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {



            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);  //Usamos single pois é uma API interna e não aberta publicamente
                                                                                          //Se for aberta publicamente devemos usar SingleOrDefault e checar se o estado do customer é valid





            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();


            //if (movies.Count != newRental.MovieIds.Count)
            //    return BadRequest("One or more MovieIds are invalid.");



            //if (customer == null)
            //    return BadRequest("Invalid customer ID.");//Para caso de APIs abertas

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");


                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }

        public IEnumerable<NewRentalDto> GetRentals(string query = null)
        {

        }

    }
}