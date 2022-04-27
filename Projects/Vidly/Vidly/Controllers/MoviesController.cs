using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        //Trocar o result para View Result é uma boa pratica

        public ViewResult Index()
        {

            if (User.IsInRole("CanManageMovie")) //Para selecionar qual view retornar conforme o permissionamento do usuario
                return View("List");
            return View("ReadOnlyList");
        }
        public ActionResult Random()
        {




            //var movies = new List<Movie>
            //{
            //    new Movie { Name = "Shrek", Id= 1 } ,
            //    new Movie { Name = "Wall-E", Id=2}
            //};
            

            var viewModel = new RandomMovieViewModel
            {
                Movies = _context.Movies.ToList()
            };

            return View(viewModel);

            //Não usar esses dois modelos abaixo para passar dados, pois são de dificil manutenção
            //Devem ser ajustados no Controller e na View
            //ViewData["RandomMovie"] = movie;
            //ViewBag.Movie = movie;

            //var viewResult = new ViewResult();

            //ViewData = View Data dictionary, pode ser usado como dictionary ou como Model Property
            //viewResult.ViewData.Model = movie;

            //return View(movie); //Com ViewData a gente passa data para a View sem precisar de inserir no parenteses


            //return View(movie);
            //return Content("Hello World"); //Exemplo de retorno de conteudo
            //return HttpNotFound(); 

            //return new EmptyResult();   
            //return RedirectToAction("Index", "Home", new { page = 1,sortBy = "name"}); //Argumentos: Action, Controller, Argumentos para a ação que vão para a url
        }


        //dentro do regex eu especifico a quantidade de numero de digitos para ano e mes, assim como o range permitido de meses
        //Para aplicar restrições, google: ASP.NET MVC Attribute Route Constraints
        [Route ("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")] //Mais poderoso
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);
        //}

        //esse action será chamado quando navegarmos por movies
        //public  ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    //se pageIndex nao for especificado, mostramos a pagina 1
        //    //se sortBy nao for especificado, organizamos por nome do filme
        //    //Para ter um parâmetro opcional, devemos torna-lo nullable

        //    if (pageIndex == null)
        //        pageIndex = 1;

        //    if (sortBy == null)
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy={1}",pageIndex,sortBy));
        //}


        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m=>m.Genre).SingleOrDefault(m=> m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        //public IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie> 
        //    {
        //        new Movie { Name = "Shrek", Id= 1 } ,
        //        new Movie { Name = "Wall-E", Id=2}
        //    };
        //}

        [Authorize(Roles = RoleName.CanManageMovies)] //Indicar quais roles podem criar movies
        //Esse formato acima nao é recomendado pois se mudarmos o nome do role, a manutenção fica dificil
        public ActionResult New()
        {
            var movieGenres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                
                Genres = movieGenres
            };

            return View("MovieForm", viewModel);
        }


        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m=>m.Id == Id);

            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm",viewModel);
            }


            if(movie.Id == 0)
            {

                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
                
            }
            else
            {
                var movieInDb = _context.Movies.Single(m=>m.Id==movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        
    }
}