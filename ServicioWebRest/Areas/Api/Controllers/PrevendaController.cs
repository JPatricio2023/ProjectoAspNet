using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicioWebRest.Areas.Api.Models;

namespace ServicioWebRest.Areas.Api.Controllers
{
    /*###
     Autor: Gustavo Ovelar
     Analista Programador
     Esta clase que extiende del Controler 
     será el encargado de contener las diferentes acciones que se podrán llamar según la URL y datos HTTP 
     que recibamos como petición de entrada al servicio.
     se encargara de llamar a los diferentes métodos  y formatear los resultados como JSON de acuerdo al tipo de
     requisicion (GET, POST, PUT E DELETE). Para hacer esto 
     nos valemos del  objetoJsonResult llamado al método Json() pasándole como parámetro 
     de entrada el objeto a formatear
     POST    /Api/Vendedores/Vendedor    { Nombre:"nombre", Telefono:123456789 }
     PUT     /Api/Vendedores/Vendedor/1  { Id:3, Nombre:"nombre", Telefono:123456789 }
     GET     /Api/Vendedores/Vendedor/1  {"idvendedor":1,"nome":"AKISSANIER MENESES KIOKI"}
     DELETE  /Api/Vendedores/Vendedor/1
    ###*/
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