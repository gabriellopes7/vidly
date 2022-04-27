using System;
using System.Collections.Generic;
using System.Data.Entity; //biblioteca para fazer o Include()
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        //Adicionando o DBContext para acessar o banco de d ados e fazer query no objeto
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //Pelo context ser descartável, devemos colocar o Dispose method chamando context.Dispose
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ViewResult Index()
        {

            //var customers = new List<Customer>
            //{   
            //    new Customer { Name = "Customer 1",Id=1},
            //    new Customer { Name = "Customer 2",Id = 1}
            //};

            //var customers = _context.Customers.Include(c=>c.MembershipType).ToList(); //Adicionar System.Data.Entity para funcionar



            //Para armazenar a lista de gêneros no cache
            //Data Caching
            //if (MemoryCache.Default["Genre"] == null)
            //{
            //    MemoryCache.Default["Genres"] = _context.Genres.ToList();
            //}

            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;



            return View();
        }
            
        [Route("Customers/Details/{id}")]
        public ActionResult Details(int Id)
        {
            
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == Id);
            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList(); //Nao é recomendado jogar essa variavel direto para a view, pois não ira funcionar depois para editar o Customer
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost] //Aplicar esse atributo a esta ação, para certificar que só pode ser chamado por Post e nunca como Get
        //Como boa prática, todas as ações de moficação de dados nunca devem poder ser acessadas por HTTPGet
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) //Model Binding, MVC framework vincula esse viewModel aos dados requisitados
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };


                return View("CustomerForm", viewModel);
            } //Validation data fica em Model State


            if(customer.Id == 0)
            {
                //Para adicionar ao banco de dados, primeiro precisamos de um DBContext
                _context.Customers.Add(customer); //Quando se escreve isso, não é escrito no banco de dados, somente na memória
                
            }
            else
            {
                var customerInDb = _context.Customers.Single(c=> c.Id == customer.Id);

                //Forma mais segura de salvar os dados na entidade editada
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;  
            }
            _context.SaveChanges(); //Para persistir as alterações no contexto, pega os objetos alterados, gera SQL statements e roda eles no banco de dados
            //Todos esses statements são amarradas na transação, ou tudo vai, ou nada vai
            return RedirectToAction("Index","Customers");
        }

        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer{Id=1,Name ="John Smith"},
        //        new Customer{Id=2,Name= "Mary Williams"}
        //    };
        //}
        

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Single(c=>c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        
    }
}