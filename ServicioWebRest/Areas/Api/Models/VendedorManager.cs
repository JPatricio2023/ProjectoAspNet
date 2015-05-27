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
     Esta calse llamado manager haciendo honor a su nombre gerencia y provee los datos de vendedores que se encuentra 
      en la base de datos para ser enviado en la requisicion
    ###*/
    public class VendedorManager
    {


        ConectionString stringconexion = new ConectionString();
       
        //Cria uma consulta de produto  por codigo interno
        public Vendedor ObtenerVendedor(int id)
        {
            Vendedor vend = null;

            //SqlConnection con = new SqlConnection();

            stringconexion.conexaoAbrir();

            string sql = "SELECT nome FROM Vendedores WHERE IdVendedor = @idvendedor";

            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);

            cmd.Parameters.Add("@idvendedor", System.Data.SqlDbType.Int).Value = id;

            SqlDataReader reader =
                cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                vend = new Vendedor();
                vend.idvendedor = id;
                vend.nome = reader.GetString(0);
            }

            reader.Close();

            return vend;
        }
    }
}