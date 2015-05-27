using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Clase modelo entity da tabelar vendedores
    ###*/
    public class Vendedor
    {
        public int idvendedor { get; set; }
        public string nome { get; set; }
    }
}