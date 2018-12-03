using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Clientes.Models
{
    public class Endereco
    {
        public int IdEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
        public int IdCliente { get; set; }


        public Boolean Cadastrar(Endereco endereco)
        {
            SqlConnection con = null;
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();

                string query = "INSERT INTO endereco (cep,logradouro,numero,bairro,cidade,uf,complemento,id_cliente) output INSERTED.id_endereco VALUES ('"+endereco.Cep+"','"+endereco.Logradouro+"','"+endereco.Numero+"','"+endereco.Bairro+"','"+endereco.Cidade+"','"+endereco.Uf+"','"+endereco.Complemento+"',"+endereco.IdCliente+") ";

                SqlCommand comando = new SqlCommand(query, con);
                int result = (int)comando.ExecuteScalar();
                if (result > 0)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return false;
            }
        }
        public Endereco BuscarEnderecoCliente(Cliente cliente)
        {
            SqlConnection con = null;
            Endereco endereco = new Endereco();
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();
                string query = "SELECT * FROM endereco WHERE id_cliente = '" + cliente.Id_cliente + "'";

                SqlCommand comando = new SqlCommand(query, con);
                SqlDataReader result = comando.ExecuteReader();
                result.Read();
                endereco.IdEndereco = result.GetInt32(0);
                endereco.Cep = result.GetString(1);
                endereco.Logradouro = result.GetString(2);
                endereco.Numero = result.GetString(3);
                endereco.Bairro= result.GetString(4);
                endereco.Cidade= result.GetString(5);
                endereco.Uf= result.GetString(6);
                endereco.Complemento = result.IsDBNull(7) ? "" : result.GetString(7);
                endereco.IdCliente = cliente.Id_cliente;
                return endereco;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return endereco;
            }
        }
        
        public Boolean Editar(Endereco endereco)
        {
            SqlConnection con = null;
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();

                string query = "UPDATE endereco SET ";
                query += "cep = '" + endereco.Cep + "', ";
                query += "logradouro = '" + endereco.Logradouro + "', ";
                query += "numero = '" + endereco.Numero + "', ";
                query += "bairro = '" + endereco.Bairro + "', ";
                query += "cidade = '" + endereco.Cidade + "', ";
                query += "uf = '" + endereco.Uf + "', ";
                query += "complemento = '" + endereco.Complemento + "' ";
                query += "WHERE id_endereco = '" + endereco.IdEndereco + "' ";

                SqlCommand comando = new SqlCommand(query, con);
                int result = comando.ExecuteNonQuery();
                return result > 0 ? true : false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return false;
            }
        }
        public Boolean Excluir(int id)
        {
            SqlConnection con = null;
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();

                string query = "DELETE FROM endereco WHERE id_endereco = " + id;

                SqlCommand comando = new SqlCommand(query, con);
                int result = comando.ExecuteNonQuery();

                return result > 0 ? true : false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return false;
            }
        }
    }
}