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

            context.MapRoute(
               "AccesoProduto",
               "Api/Produtos/Produto/{id}",
                new
                {
                    controller = "Produtos",
                    action = "Produto",
                    id = UrlParameter.Optional
                }
           );

            context.MapRoute(
                "ConsultaPrevendaId",
                "Api/Prevenda/UltimoId",
                 new
                 {
                     controller = "Prevenda",
                     action = "UltimoId",
                 }
            );

            context.MapRoute(
               "RetornaUltimaLinha",
               "Api/Prevenda/UltimoLinha",
                new
                {
                    controller = "Prevenda",
                    action = "UltimoLinha",
                }
           );

            context.MapRoute(
               "ConsultaPrevendaEstado",
               "Api/Prevenda/Estado/{cartao}",
                new
                {
                    controller = "Prevenda",
                    action = "EstadoConsumo",
                    cartao = UrlParameter.Optional
                }
           );

            context.MapRoute(
               "TotalConsumo",
               "Api/Prevenda/TotalConsumo/{id}",
                new
                {
                    controller = "Prevenda",
                    action = "TotalConsumo",
                    id = UrlParameter.Optional
                }
           );
            context.MapRoute(
               "IdConsumo",
               "Api/Prevenda/IdConsumo/{cartao}",
                new
                {
                    controller = "Prevenda",
                    action = "IdMesaAberto",
                    id = UrlParameter.Optional
                }
           );
            context.MapRoute(
               "ConsultaConsumo",
               "Api/Prevendas/Prevenda/{cartao}/{id}",
                new
                {
                    controller = "Prevenda",
                    action = "Consumo",
                    cartao = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
           );
            context.MapRoute(
               "UltimoNroOrdem",
               "Api/Prevenda/OrdemItem/{id}",
                new
                {
                    controller = "Prevenda",
                    action = "NroOrdemItem",
                    id = UrlParameter.Optional
                }
           );
            context.MapRoute(
            "InsertarPrevenda",
            "Api/Prevendas/Insertar",
            new
            {
                controller = "Prevenda",
                action = "Prevenda",
                id = UrlParameter.Optional
            }
         );
            context.MapRoute(
            "InsertarPrevendaDetalhes",
            "Api/Prevendas/InsertarPrevendaDetalhes",
            new
            {
                controller = "Prevenda",
                action = "PrevendaDetalhes",

            }
         );

            context.MapRoute(
            "InsertarComplemento",
            "Api/Prevendas/InsertarComplemento",
            new
            {
                controller = "Prevenda",
                action = "ComplementoDetalhes",
                id = UrlParameter.Optional
            }
         );
        }
    }
}
