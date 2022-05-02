using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Rota com multiplos parâmetros
            //Devemos organizar as rotas aqui dentro, da mais específica, para a mais genérica
            //Senao a rota mais genérica será sempre a aplicada na url
            //Custom Route

            //Defining custom route using attribute
            //Attribute Routing
            routes.MapMvcAttributeRoutes();


            //routes.MapRoute(
            //    "MoviesByReleaseDate",
            //    "movies/released/{year}/{month}", //padrão da url
            //    new { controller = "Movies", action = "ByReleaseDate" }, //default
            //    //new {year = @"2015|2016"} //Para limitar os anos da url em 2016 ou 2015
            //    new {year = @"\d{4}", month =@"\d{2}"} //Definindo a quantidade de digitos permitidos para os parametros year e month
            //    );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    //Route com multiplos parâmetros
}
