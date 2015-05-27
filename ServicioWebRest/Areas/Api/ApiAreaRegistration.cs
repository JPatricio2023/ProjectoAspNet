using System.Web.Mvc;

namespace ServicioWebRest.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AccesoVendedor",
                "Api/Vendedores/Vendedor/{id}",
                 new
                 {
                     controller = "Vendedores",
                     action = "Vendedor",
                     id = UrlParameter.Optional
                 }
            );
        }
    }
}
