using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Clase modelo entity da tabela Prevenda
    ###*/
    public class Prevenda
    {
        public int prevendaId { get; set; }
        //public int prevendaCaixa { get; set; }
        public int cartao { get; set; }
        //public int prevendaCpFiscal { get; set; }
        public Int16 prevendaSituacao { get; set; }
        public Int16 prevendaFechada { get; set; }
        public DateTime data { get; set; }
    }
}