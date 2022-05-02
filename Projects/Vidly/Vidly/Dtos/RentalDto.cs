using System;

namespace Vidly.Dtos
{
    public class RentalDto
    {
        //public int Id { get; set; }
        //public int CustomerId { get; set; }

        //public string CustomerName { get; set; }

        //public int MovieId { get; set; }

        //public string MovieName { get; set; }

        //public DateTime DateRented { get; set; }

        //public DateTime? DateReturned { get; set; }

        //public int NumberOfMovies { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int RentalId { get; set; }

        public string MovieName { get; set; }

        public int MovieId { get; set; }

        public DateTime RentedAt { get; set; }

        public DateTime? ReturnedAt { get; set; }
    }
}