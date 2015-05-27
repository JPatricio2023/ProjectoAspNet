using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicioWebRest.Areas.Api.Models;

namespace ServicioWebRest.Areas.Api.Controllers
{
    public class PrevendaController : Controller
    {
        private PrevendaManager PrevendaManager;

        public PrevendaController()
        {
            PrevendaManager = new PrevendaManager();
        }

        // GET /Api/Clientes
        [HttpGet]
        public JsonResult Consumo(int? cartao, int? id)
        {
            return Json(PrevendaManager.retornarConsumoAberto(cartao.GetValueOrDefault(), id.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UltimoId()
        {
            return Json(PrevendaManager.ultimoRegPrevenda(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UltimoLinha()
        {
            return Json(PrevendaManager.ultimoLinhaDetalhe(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult EstadoConsumo(int? cartao)
        {
            return Json(PrevendaManager.ConsultarConsumoAberto(cartao.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IdMesaAberto(int? cartao)
        {
            return Json(PrevendaManager.retornaIdMesaAberto(cartao.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult NroOrdemItem(int? id)
        {
            return Json(PrevendaManager.ultimoNroOrdemItem(id.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalConsumo(int? id)
        {
            return Json(PrevendaManager.sumaConsumoMesa(id.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
        }

        // POST    /Api/Clientes/Cliente    { Nombre:"nombre", Telefono:123456789 }
        // PUT     /Api/Clientes/Cliente/3  { Id:3, Nombre:"nombre", Telefono:123456789 }
        // GET     /Api/Clientes/Cliente/3
        // DELETE  /Api/Clientes/Cliente/3
        //insertar prevenda detalhes
        /*public JsonResult PrevendaDetalhesUpdate(int? id, short? ordem)
        {
            //if (Request.HttpMethod == "PUT")
            //{
            return Json(PrevendaManager.cancelarItemPrevenda(id.GetValueOrDefault(), ordem.GetValueOrDefault()), JsonRequestBehavior.AllowGet);
            //}
            //return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }*/

        //insertar complemento detalhes
        public JsonResult ComplementoDetalhes(prevendaDetalhesComplemento item)
        {
            return Json(PrevendaManager.inserirComplemento(item));
        }

        // insertar prevenda
        public JsonResult Prevenda(Prevenda item)
        {

            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(PrevendaManager.InsertarPrevenda(item));
                case "PUT":
                    return Json(PrevendaManager.cancelarConsumo(item));
                /*case "GET":
                    return Json(clientesManager.ObtenerCliente(id.GetValueOrDefault()),
                                JsonRequestBehavior.AllowGet);
                case "DELETE":
                    return Json(clientesManager.EliminarCliente(id.GetValueOrDefault()));*/
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }

        public JsonResult PrevendaDetalhes(PrevendaDetalhes item)
        {

            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(PrevendaManager.inserirPrevendaDetalhe(item));
                case "PUT":
                    return Json(PrevendaManager.cancelarItemPrevenda(item));
                /*case "GET":
                    return Json(clientesManager.ObtenerCliente(id.GetValueOrDefault()),
                                JsonRequestBehavior.AllowGet);
                case "DELETE":
                    return Json(clientesManager.EliminarCliente(id.GetValueOrDefault()));*/
            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }
}