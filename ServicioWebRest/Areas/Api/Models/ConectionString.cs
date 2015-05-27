using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace ServicioWebRest.Areas.Api.Models
{
    /*###
     Autor: Gustavo Ovelar
     Analista Programador
     Esta clase se encarga de la conexion al banco de datos, para ellos y para que sea dinamico practico, utiliza un achivo
     txt llamado conexao.txt que devera ser creado en la carpeta raiz de servidor con la conection strin al
     banco("Server=GOLIAS;Database=Akira;user Id=sa;password=********"), en este caso como es un IIS  la raiz es o sera 
     *"C:\\inetpub\\wwwroot\\RestService\\Conexao.txt"... sera pos si todavia no esta confirado el ISS deberemos configurarlo despues
    ###*/
    public class ConectionString
    {
        //Declarando um objeto do tipo connection
        public SqlConnection conexion = new SqlConnection();
        private const string FILE_NAME = "C:\\inetpub\\wwwroot\\RestService\\Conexao.txt";

        public int conexaoAbrir()//(string stringConnection)
        {
            try
            {
                String lineatexto = "";
                if (File.Exists(FILE_NAME) != false)
                {
                    lineatexto = File.ReadAllText(FILE_NAME);
                }
                //SqlConnection conexao = new SqlConnection("Server=GOLIAS;Database=Akira;user Id=sa;password=********");
                //Configura a string de conexão
                //Cnn.ConnectionString = stringConnection;
                conexion.ConnectionString = @lineatexto;
                //Abre a conexão
                conexion.Open();
                //Dá retorno a função
                //Dá retorno a função
                return 1;
            }
            catch (SqlException se)
            {
                throw se;

            }
            catch (FileNotFoundException fe)
            {
                throw fe;
            }

        }
        public int conexaoFechar()
        {
            try
            {
                //Se a conexão estiver aberto
                if (conexion.State == ConnectionState.Open)
                {
                    //Tenta fechar a conexão
                    conexion.Close();
                    //Dá retorno a função
                    return 1;
                }
                //Dá retorno a função
                return 1;
            }
            catch
            {
                //Dá retorno a função
                return 0;
            }
        }
    }
}