using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Clase modelo auxiliar para la obtencion de consumo por mesa
    ###*/
    public class Consumo
    {

        public string produtoCodigo { get; set; }
        public string produtoCodigobarra { get; set; }
        public string produtoOrdem { get; set; }
        public string descricao { get; set; }
        public string produtoQuantidade { get; set; }
        public string produtoUnitario { get; set; }
        public string produtoSituacao { get; set; }
        public string total { get; set; }
    }
}