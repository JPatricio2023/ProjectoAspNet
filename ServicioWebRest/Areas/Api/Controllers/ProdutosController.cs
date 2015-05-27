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
     se limitará a llamar al método ObtenerProdutoId() y formatear los resultados como JSON. Para hacer esto 
     creamos directamente un objetoJsonResult llamando al método Json() pasándole como parámetro 
     de entrada el objeto a formatear. Todo esto se reduce a una sola linea de código:
    ###*/
    public class ProdutosController : Controller
    {
        private ProdutoManager produtosManager;

        public ProdutosController()
        {
            produtosManager = new ProdutoManager();
        }

        // GET     /Api/Produtos/Produto/id
        [HttpGet]
        public JsonResult Produto(int? id, Produto item)
        {
            if (Request.HttpMethod == "GET")
                return Json(produtosManager.ObtenerProdutoId(id.GetValueOrDefault()),
                JsonRequestBehavior.AllowGet);
            else
                return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }

    }
}