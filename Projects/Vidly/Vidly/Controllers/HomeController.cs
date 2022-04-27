using System;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    [AllowAnonymous] //Atributo para permitir acesso às ações da HomePage por qualquer usuário, mesmo os não logados
    public class HomeController : Controller
    {
        //[OutputCache(Duration = 50, Location = OutputCacheLocation.Server)] //Armazena a homepage no cache, sem atualização de dados durante 50 segundos
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            throw new Exception();


            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}