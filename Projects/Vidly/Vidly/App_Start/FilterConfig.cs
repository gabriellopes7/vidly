using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); //Redireciona o usuário para uma página de erro quando uma ação retorna uma exceção
            filters.Add(new AuthorizeAttribute()); //Filtro de autorização da aplicação, somente permitindo a navegação para usuários logados
            filters.Add(new RequireHttpsAttribute()); //Permitir somente acesso pela URL com HTTPS, navegação segura
        }
    }
}
