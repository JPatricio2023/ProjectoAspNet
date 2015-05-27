using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Autor: Gustavo Ovelar
     Analista Programador
     Esta calse llamado manager haciendo honor a su nombre gerencia y provee los datos de productos que se encuentra 
      en la base de datos para ser enviado en la requisicion
    ###*/

    public class ProdutoManager
    {
        ConectionString stringconexion = new ConectionString();
        //Cria uma consulta de produto  por codigo interno
        public Produto ObtenerProdutoId(int id)
        {

            Produto prod = null;

            //SqlConnection con = new SqlConnection();

            stringconexion.conexaoAbrir();

            string sql = "SELECT Estoque.descricao, Estoque.preco_venda, Estoque.unidade, Estoque.produtoRequerProducao,";
            sql += " Estoque.produtoRequerProducaoPermiteAlterar, EstoqueBarras.codigobarras FROM Estoque";
            sql += " LEFT JOIN EstoqueBarras ON Estoque.idproduto=EstoqueBarras.idproduto WHERE Estoque.idproduto=@idproduto";

            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);

            cmd.Parameters.Add("@idproduto", System.Data.SqlDbType.NVarChar).Value = id;

            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                prod = new Produto();
                prod.idproduto = id;
                prod.descricao = reader.GetString(0);

                prod.preco_venda = reader.GetDecimal(1).ToString();
                if (!reader.IsDBNull(2)) { prod.unidade = reader.GetString(2); }
                prod.prodReqProducao = reader.GetBoolean(3);
                prod.prodReqProdPermAlterar = reader.GetBoolean(4);
                if (!reader.IsDBNull(5)) { prod.codigobarras = reader.GetString(5); }
            }

            reader.Close();
            stringconexion.conexaoFechar();
            return prod;
        }
    }
}