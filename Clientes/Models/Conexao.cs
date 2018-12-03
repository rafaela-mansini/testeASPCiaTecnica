using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Clientes.Models
{
    public class Conexao
    {

        public static SqlConnection BuscarConexao()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=RAFAELA-PC\SQLEXPRESS;Initial Catalog=cia_tecnica;User ID=sa; Password=qaz123";
            return con;

        }
    }
}