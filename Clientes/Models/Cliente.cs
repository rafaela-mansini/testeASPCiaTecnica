using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Clientes.Models
{
    public class Cliente
    {
        public int Id_cliente { get; set; }
        public string Documento { get; set; }        
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataNascimento { get; set; }
        public string TipoCliente { get; set; }
        public Endereco Endereco { get; set; }

        public List<Cliente> SelecionarPorTipo(string tipo)
        {
            SqlConnection con = null;
            List<Cliente> clientes = new List<Cliente>();
            
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();
                string query = "SELECT * FROM cliente WHERE tipo_cliente = '" + tipo + "'";
                
                SqlCommand comando = new SqlCommand(query, con);
                SqlDataReader result = comando.ExecuteReader();

                while (result.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Id_cliente = result.GetInt32(0);
                    cliente.Documento = result.GetString(1);
                    cliente.Nome = result.GetString(2);
                    cliente.Sobrenome = result.IsDBNull(3) ? "" : result.GetString(3);
                    cliente.RazaoSocial = result.IsDBNull(4) ? "" : result.GetString(4);
                    if (!result.IsDBNull(5))
                    {
                        cliente.DataNascimento = result.GetDateTime(5);
                    }
                    cliente.TipoCliente = result.GetString(6);

                    Endereco end = new Endereco();
                    Endereco = end.BuscarEnderecoCliente(cliente);

                    clientes.Add(cliente);
                }
                con.Close();
                return clientes;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return null;
            }
            
        }

        public Cliente SelecionarPorId(int id)
        {
            SqlConnection con = null;

            try
            {
                con = Conexao.BuscarConexao();
                con.Open();
                string query = "SELECT * FROM cliente WHERE id_cliente = " + id;

                SqlCommand comando = new SqlCommand(query, con);
                SqlDataReader result = comando.ExecuteReader();
                result.Read();
                Cliente cliente = new Cliente();
                cliente.Id_cliente = id;
                cliente.Documento = result.GetString(1);
                cliente.Nome = result.GetString(2);
                cliente.Sobrenome = result.IsDBNull(3) ? "-" : result.GetString(3);
                cliente.RazaoSocial = result.IsDBNull(4) ? "-" : result.GetString(4);
                cliente.TipoCliente = result.GetString(6);

                con.Close();
                return cliente;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return null;
            }

        }

        public Boolean Cadastrar(Cliente cliente)
        {
            SqlConnection con = null;
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();

                string query = "INSERT INTO cliente (documento, nome, sobrenome, razao_social, tipo_cliente) output INSERTED.id_cliente VALUES ('"+cliente.Documento+"','" + cliente.Nome+ "','"+cliente.Sobrenome+ "','"+cliente.RazaoSocial+ "','"+cliente.TipoCliente+"') ";
                
                SqlCommand comando = new SqlCommand(query, con);
                int result = (int)comando.ExecuteScalar();

                if (result > 0)
                {
                    cliente.Id_cliente = result;
                    Endereco endereco = new Endereco();
                    endereco.Cep = cliente.Endereco.Cep;
                    endereco.Logradouro = cliente.Endereco.Logradouro;
                    endereco.Numero = cliente.Endereco.Numero;
                    endereco.Bairro = cliente.Endereco.Bairro;
                    endereco.Cidade = cliente.Endereco.Cidade;
                    endereco.Uf = cliente.Endereco.Uf;
                    endereco.Complemento = cliente.Endereco.Complemento;
                    endereco.IdCliente = cliente.Id_cliente;
                    if(!endereco.Cadastrar(endereco))
                    {
                        con.Close();
                        return false;
                    }
                    
                }
                con.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return false;
            }
        }

        public Boolean Editar(Cliente cliente)
        {
            SqlConnection con = null;
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();

                string query = "UPDATE cliente SET ";
                query += "documento = '" + cliente.Documento + "', ";
                query += "nome = '" + cliente.Nome + "', ";
                

                if (cliente.TipoCliente == "fisica")
                {
                    query += "sobrenome = '" + cliente.Sobrenome + "' ";
                }
                else
                {
                    query += "razao_social = '" + cliente.RazaoSocial + "' ";
                }
                query += "WHERE id_cliente = '" + cliente.Id_cliente + "' ";

                SqlCommand comando = new SqlCommand(query, con);
                int result = comando.ExecuteNonQuery();

                if (result > 0)
                {
                    Endereco endereco = new Endereco();
                    endereco.Cep = cliente.Endereco.Cep;
                    endereco.Logradouro = cliente.Endereco.Logradouro;
                    endereco.Numero = cliente.Endereco.Numero;
                    endereco.Bairro = cliente.Endereco.Bairro;
                    endereco.Cidade = cliente.Endereco.Cidade;
                    endereco.Uf = cliente.Endereco.Uf;
                    endereco.Complemento = cliente.Endereco.Complemento;
                    endereco.IdEndereco = cliente.Endereco.IdEndereco;
                    if (!endereco.Editar(endereco))
                    {
                        con.Close();
                        return false;
                    }
                }
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                con.Close();
                return false;
            }
        }

        public Boolean Excluir(Cliente cliente, Endereco endereco)
        {
            SqlConnection con = null;
            try
            {
                con = Conexao.BuscarConexao();
                con.Open();

                string query = "DELETE FROM cliente WHERE id_cliente = "+cliente.Id_cliente;

                SqlCommand comando = new SqlCommand(query, con);
                int result = comando.ExecuteNonQuery();
                
                if (result > 0)
                {

                    if (endereco.Excluir(cliente.Endereco.IdEndereco))
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


    }
}