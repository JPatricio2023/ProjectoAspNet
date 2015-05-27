using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Clase modelo entity da tabela prevendaDetalhesComplemento
    ###*/
    public class prevendaDetalhesComplemento
    {
        public int prevendaLinhaId { get; set; }
        public string prevendaDetalheTipo { get; set; }
        public string prevendaDetalheDescricao { get; set; }
    }
}