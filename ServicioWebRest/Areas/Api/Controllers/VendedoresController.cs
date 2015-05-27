using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServicioWebRest.Areas.Api.Models;
using System.Web.Mvc;

namespace ServicioWebRest.Areas.Api.Controllers
{
    /*###
     Autor: Gustavo Ovelar
     Analista Programador
     Esta clase que extiende del Controler 
     será el encargado de contener las diferentes acciones que se podrán llamar según la URL y datos HTTP 
     que recibamos como petición de entrada al servicio.
     se limitará a llamar al método ObtenerVendedor() y formatear los resultados como JSON. Para hacer esto 
     creamos directamente un objetoJsonResult llamado al método Json() pasándole como parámetro 
     de entrada el objeto a formatear. Todo esto se reduce a una sola linea de código:
    ###*/
    public class VendedoresController:Controller
    {
        private VendedorManager vendedoresManager;
        public VendedoresController()
        {
            vendedoresManager = new VendedorManager();
        }

        // GET     /Api/Produtos/Produto/id
        [HttpGet]
        public JsonResult Vendedor(int? id, Vendedor item)
        {
            if (Request.HttpMethod == "GET")
                return Json(vendedoresManager.ObtenerVendedor(id.GetValueOrDefault()),
                 JsonRequestBehavior.AllowGet);
            else
                return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }
    }
}