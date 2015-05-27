using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Autor: Gustavo Ovelar
     Analista Programador
     Esta calse llamado manager haciendo honor a su nombre, gerencia y provee los datos referente a la prevenda atraves de los diferentes
      metodos desde la base de datos para ser enviado en la requisicion
     
    ###*/
    public class PrevendaManager
    {
        public static ConectionString stringconexion = new ConectionString();
        //registra a prevenda
        public bool InsertarPrevenda(Prevenda prevenda)
        {
            stringconexion.conexaoAbrir();
            string sql = "INSERT INTO prevenda(prevendaCartao, prevendaData) VALUES(@PREVENDACARTAO, @PREVENDADATA)";
            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);

            cmd.Parameters.AddWithValue("@ID", prevenda.prevendaId);
            cmd.Parameters.AddWithValue("@PREVENDACARTAO", prevenda.cartao);
            cmd.Parameters.AddWithValue("@PREVENDADATA", prevenda.data);

            int res = cmd.ExecuteNonQuery();

            stringconexion.conexaoFechar();

            return (res == 1);
        }
        //aqui me aseguro si el consumo de la mesa esta abierto
        public static bool ConsultarConsumoAberto(int cartao)
        {
            Boolean estado = true;
            stringconexion.conexaoAbrir();

            String sql = "SELECT prevenda.prevendaCartao, prevenda.prevendaFechada, prevenda.prevendaData FROM";
            sql+= " prevenda WHERE prevenda.prevendaCartao=@CARTAO AND prevenda.prevendaFechada=0";

            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);
            cmd.Parameters.AddWithValue("@CARTAO", cartao);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count == 0)
            {
                estado = false;
            }
            stringconexion.conexaoFechar();
            return estado;

        }
        //retorna el ultimo id del insert na tabela prevenda
        public static int ultimoRegPrevenda()
        {
            stringconexion.conexaoAbrir();

            String sql = "SELECT MAX(prevendaid) FROM prevenda";

            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);
            decimal totalConsumo = Convert.ToInt32(cmd.ExecuteScalar());
            int maxid = Convert.ToInt32(cmd.ExecuteScalar());
            stringconexion.conexaoFechar();
            return maxid;

        }
        //Retorna el consumo de la mesa abierta
        public List<Consumo> retornarConsumoAberto(int cartao, int id)
        {
            stringconexion.conexaoAbrir();
            List<Consumo> lista = new List<Consumo>();

            String query = "SELECT prevenda.prevendaCartao, prevendaProdutoCodigo AS Código,prevendaProdutoOrdem as Ordem, estoque.descricao AS Descrição,";
            query += " prevendaProdutoQuantidade AS Quant, prevendaProdutoUnitario AS Unitario,";
            query += " (prevendaProdutoQuantidade*prevendaProdutoUnitario) AS Total , prevendaProdutoSituacao AS Situação,prevendaProdutoCodigoGtim as Barra";
            query += " FROM prevenda";
            query += " INNER JOIN prevendaDetalhes ON prevenda.prevendaId = prevendaDetalhes.prevendaId";
            query += " INNER JOIN estoque ON prevendaDetalhes.prevendaProdutoCodigo = estoque.idProduto";
            query += " WHERE prevenda.prevendaCartao = @CARTAO AND prevenda.prevendaid=@ID ORDER BY prevendaDetalhes.prevendaProdutoOrdem";

            SqlCommand cmd = new SqlCommand(query, stringconexion.conexion);
            cmd.Parameters.AddWithValue("@CARTAO", cartao);
            cmd.Parameters.AddWithValue("@ID", id);

            SqlDataReader reader =
                  cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Consumo item; //= new PrevendaDetalhes();

                //item = new PrevendaDetalhes();
                item = new Consumo();
                string xdesc;//variable para tener temporalmente el valor de los campos
                int leng;//variable para tener temporalmente la longitud de los campos

                xdesc = reader.GetInt32(1).ToString();
                //leng = xdesc.Length;
                //if (leng < 10)
                //{
                //   leng = 10 - leng;
                // xdesc = xdesc.PadRight(leng);
                //}
                item.produtoCodigo = xdesc;
                xdesc = reader.GetString(8).ToString();
                //leng = xdesc.Length;
                //if (leng < 15)
                //{
                //   leng = 15 - leng;
                //  xdesc = xdesc.PadRight(leng);
                // }
                item.produtoCodigobarra = xdesc;

                xdesc = reader.GetInt16(2).ToString();
                leng = xdesc.Length;
                if (leng < 10)
                {
                    leng = 10 - leng;
                    xdesc = xdesc.PadRight(leng);
                }
                item.produtoOrdem = xdesc;

                xdesc = reader.GetString(3);
                leng = xdesc.Length;

                if (leng < 70)
                {

                    if (leng < 14)
                    {
                        leng = 70 - 10;
                    }
                    else
                        leng = 70 - leng;
                    xdesc = xdesc.PadRight(leng);
                }
                item.descricao = xdesc;

                xdesc = reader.GetDecimal(4).ToString("0.00");
                leng = xdesc.Length;
                if (leng < 10)
                {
                    leng = 10 - leng;
                    xdesc = xdesc.PadRight(leng);
                }
                item.produtoQuantidade = xdesc;

                xdesc = reader.GetDecimal(5).ToString("0.00");
                leng = xdesc.Length;
                if (leng < 10)
                {
                    leng = 10 - leng;
                    xdesc = xdesc.PadRight(leng);
                }
                item.produtoUnitario = xdesc;

                xdesc = reader.GetDecimal(6).ToString("0.00");
                leng = xdesc.Length;
                if (leng < 10)
                {
                    leng = 10 - leng;
                    xdesc = xdesc.PadRight(leng);
                }
                item.total = xdesc;

                if (reader.GetInt16(7) == 0)
                    item.produtoSituacao = "Normal";
                if (reader.GetInt16(7) == 1)
                    item.produtoSituacao = "Cancelado";

                lista.Add(item);
            }

            reader.Close();
            stringconexion.conexaoFechar();
            return lista;
        }

        //retorno o total de consumo
        public static Decimal sumaConsumoMesa(int id)
        {
            stringconexion.conexaoAbrir();
            decimal totalConsumo = 0;
            string query = "SELECT SUM (prevendaProdutoQuantidade*prevendaProdutoUnitario)";
            query += " FROM prevendaDetalhes WHERE prevendaid=@ID AND prevendaProdutoSituacao=0";
            SqlCommand cmd = new SqlCommand(query, stringconexion.conexion);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                totalConsumo = Convert.ToDecimal(cmd.ExecuteScalar());

            }
            catch { }
            Decimal total = Convert.ToDecimal((string.Format("{0:0.00}", totalConsumo)), CultureInfo.CreateSpecificCulture("pt-BR"));
            stringconexion.conexaoFechar();
            return (total);


        }
        //retorno o id da mesa aberta
        public static int retornaIdMesaAberto(int cartao)
        {
            stringconexion.conexaoAbrir();
            SqlCommand cmd = new SqlCommand("SELECT prevenda.prevendaid, prevenda.prevendaCartao, prevenda.prevendaFechada, prevenda.prevendaData FROM prevenda WHERE prevenda.prevendaCartao=@CARTAO AND prevenda.prevendaFechada=0", stringconexion.conexion);
            cmd.Parameters.AddWithValue("@CARTAO", cartao);

            //cria o sqlDataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            //cria o DataSet de retorno
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            int id = 0;
            if (dataTable.Rows.Count > 0)//Se Contem registro
            {
                //recupero o id desse registro(da mesa)
                id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            }
            stringconexion.conexaoFechar();
            return id;
        }

        //retorna el ultimo nro de oredem da prevendaDetalhes
        public static int ultimoNroOrdemItem(int id)
        {
            stringconexion.conexaoAbrir();
            Int16 maxordem = 0;
            SqlCommand cmd = new SqlCommand("SELECT MAX(prevendaProdutoOrdem) FROM prevendaDetalhes WHERE prevendaid=@ID ", stringconexion.conexion);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                maxordem = Convert.ToInt16(cmd.ExecuteScalar());

            }
            catch { }
            stringconexion.conexaoFechar();
            return maxordem;

        }

        //retorna el ultimo nro de linha da prevendaDetalhes
        public static int ultimoLinhaDetalhe()
        {
            stringconexion.conexaoAbrir();
            SqlCommand cmd = new SqlCommand("SELECT MAX(prevendaLinhaId) FROM prevendaDetalhes", stringconexion.conexion);
            decimal totalConsumo = Convert.ToInt32(cmd.ExecuteScalar());
            int maxidLinha = Convert.ToInt32(cmd.ExecuteScalar());
            stringconexion.conexaoFechar();
            return maxidLinha;
            
        }

        //registra a prevenda
        public bool inserirPrevenda(Prevenda prevenda)
        {
            stringconexion.conexaoAbrir();

            string sql = "INSERT INTO prevenda(prevendaCartao,prevendaSituacao,prevendaFechada,prevendaData) ";
                   sql += "VALUES(@PREVENDACARTAO,@SITUACAO,@FECHADA,@PREVENDADATA)";

            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);

            //cmd.Parameters.Add("@nombre", System.Data.SqlDbType.NVarChar).Value = cli.Nombre;
            //cmd.Parameters.Add("@telefono", System.Data.SqlDbType.Int).Value = cli.Telefono;
            cmd.Parameters.AddWithValue("@ID", prevenda.prevendaId);
            cmd.Parameters.AddWithValue("@PREVENDACARTAO", prevenda.cartao);
            cmd.Parameters.AddWithValue("@SITUACAO", prevenda.prevendaSituacao);
            cmd.Parameters.AddWithValue("@FECHADA", prevenda.prevendaFechada);
            cmd.Parameters.AddWithValue("@PREVENDADATA", prevenda.data);

            int res = cmd.ExecuteNonQuery();

            stringconexion.conexaoFechar();

            return (res == 1);
        }

        //registrar detalle de prevenda
        public bool inserirPrevendaDetalhe(PrevendaDetalhes prevendaItem)
        {
            stringconexion.conexaoAbrir();
            string sql = "INSERT INTO prevendaDetalhes(prevendaId, prevendaProdutoOrdem, prevendaProdutoCodigo, prevendaProdutoQuantidade, ";
            sql += "prevendaProdutoUnitario, prevendaProdutoSituacao, prevendaProdutoCodigoGtim, prevendaProdutoAtendente) ";
            sql += "VALUES (@PREVENDAID, @PRODUTOORDEM, @PRODUTOCODIGO, @PRODUTOQUANTIDADE, @PRODUTOUNITARIO, @PRODUTOSITUACAO, ";
            sql += "@PRODUTOCODIGOGTIM, @PRODUTOATENDENTE)";

            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);

            cmd.Parameters.AddWithValue("@PREVENDAID", prevendaItem.prevendaId);
            cmd.Parameters.AddWithValue("@PRODUTOORDEM", prevendaItem.produtoOrdem);
            cmd.Parameters.AddWithValue("@PRODUTOCODIGO", prevendaItem.produtoCodigo);
            cmd.Parameters.AddWithValue("@PRODUTOQUANTIDADE", Convert.ToDecimal(prevendaItem.produtoQuantidade));
            cmd.Parameters.AddWithValue("@PRODUTOUNITARIO", Convert.ToDecimal(prevendaItem.produtoUnitario));
            cmd.Parameters.AddWithValue("@PRODUTOSITUACAO", prevendaItem.produtoSituacao);
            cmd.Parameters.AddWithValue("@PRODUTOCODIGOGTIM", prevendaItem.produtoCodigobarra);
            cmd.Parameters.AddWithValue("@PRODUTOATENDENTE", prevendaItem.idatendente);

            int res = cmd.ExecuteNonQuery();

            stringconexion.conexaoFechar();

            return (res == 1);
        }

        //registra complemento
        public bool inserirComplemento(prevendaDetalhesComplemento complemento)
        {
            stringconexion.conexaoAbrir();

            string sql = "INSERT INTO prevendaDetalhesComplemento(prevendaLinhaId, prevendaDetalheTipo, prevendaDetalheDescricao) ";
            sql += "VALUES(@IDLINHA, @TIPO, @DESCRICAO)";

            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);
            cmd.Parameters.AddWithValue("@IDLINHA", complemento.prevendaLinhaId);
            cmd.Parameters.AddWithValue("@TIPO", complemento.prevendaDetalheTipo);
            cmd.Parameters.AddWithValue("@DESCRICAO", complemento.prevendaDetalheDescricao);

            int res = cmd.ExecuteNonQuery();

            stringconexion.conexaoFechar();

            return (res == 1);


        }

        //Atualizar prevenda detalhes (Cancelar)
        public bool cancelarItemPrevenda(PrevendaDetalhes prevendaItem)
        {
            stringconexion.conexaoAbrir();

            string sql = "UPDATE prevendaDetalhes SET prevendaProdutoOrdem=@PRODUTOORDEM, prevendaProdutoCodigo=@PRODUTOCODIGO, prevendaProdutoQuantidade=@PRODUTOQUANTIDADE, ";
            sql += "prevendaProdutoUnitario=@PRODUTOUNITARIO, prevendaProdutoSituacao=@PRODUTOSITUACAO, prevendaProdutoCodigoGtim=@PRODUTOCODIGOGTIM, prevendaProdutoAtendente=@PRODUTOATENDENTE ";
            sql += "WHERE prevendaid=@PREVENDAID AND prevendaProdutoordem=@PRODUTOORDEM";
            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);
            cmd.Parameters.AddWithValue("@PREVENDAID", prevendaItem.prevendaId);
            cmd.Parameters.AddWithValue("@PRODUTOORDEM", prevendaItem.produtoOrdem);
            cmd.Parameters.AddWithValue("@PRODUTOCODIGO", prevendaItem.produtoCodigo);
            cmd.Parameters.AddWithValue("@PRODUTOQUANTIDADE", Convert.ToDecimal(prevendaItem.produtoQuantidade));
            cmd.Parameters.AddWithValue("@PRODUTOUNITARIO", Convert.ToDecimal(prevendaItem.produtoUnitario));
            cmd.Parameters.AddWithValue("@PRODUTOSITUACAO", prevendaItem.produtoSituacao);
            cmd.Parameters.AddWithValue("@PRODUTOCODIGOGTIM", prevendaItem.produtoCodigobarra);
            cmd.Parameters.AddWithValue("@PRODUTOATENDENTE", prevendaItem.idatendente);
            int res = cmd.ExecuteNonQuery();

            stringconexion.conexaoFechar();

            return (res == 1);

        }

        //Atualiza prevenda (Cancela Consumo)
        public bool cancelarConsumo(Prevenda prevenda)
        {
            stringconexion.conexaoAbrir();

            string sql = "UPDATE prevenda SET prevendacartao=@CARTAO, ";
            sql += "prevendaSituacao=@SITUACAO, prevendafechada=@FECHADA, prevendadata=@DATA ";
            sql += "WHERE prevendaid=@ID AND prevendaCartao=@CARTAO";
            SqlCommand cmd = new SqlCommand(sql, stringconexion.conexion);
            cmd.Parameters.AddWithValue("@ID", prevenda.prevendaId);
            //cmd.Parameters.AddWithValue("@CAIXA", prevenda.prevendaCaixa);
            cmd.Parameters.AddWithValue("@CARTAO", prevenda.cartao);
            //cmd.Parameters.AddWithValue("@CPFISCAL", prevenda.prevendaCpFiscal);
            cmd.Parameters.AddWithValue("@SITUACAO", prevenda.prevendaSituacao);
            cmd.Parameters.AddWithValue("@FECHADA", prevenda.prevendaFechada);
            cmd.Parameters.AddWithValue("@DATA", prevenda.data);
            int res = cmd.ExecuteNonQuery();

            stringconexion.conexaoFechar();

            return (res == 1);
        }
    }
}