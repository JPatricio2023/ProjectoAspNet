using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Clase modelo entity da tabela prevendaDetalhes
    ###*/
    public class PrevendaDetalhes
    {
        public int prevendaId { get; set; }
        public short produtoOrdem { get; set; }
        public int produtoCodigo { get; set; }
        public string produtoQuantidade { get; set; }
        public string descricao { get; set; }
        public string produtoUnitario { get; set; }
        public short produtoSituacao { get; set; }
        public string produtoCodigobarra { get; set; }
        public int idatendente { get; set; }
        public decimal total { get; set; }
    }
}