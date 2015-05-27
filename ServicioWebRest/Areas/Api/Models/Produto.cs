using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Clase modelo entity da tabela Produto
    ###*/
    public class Produto
    {
        public int idproduto { get; set; }
        public string descricao { get; set; }
        public string preco_venda { get; set; }
        public string unidade { get; set; }
        public Boolean prodReqProducao { get; set; }
        public Boolean prodReqProdPermAlterar { get; set; }
        public string codigobarras { get; set; }
    }
}