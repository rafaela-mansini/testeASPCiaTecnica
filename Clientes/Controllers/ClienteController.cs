using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clientes.Models;

namespace Clientes.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {

            return View();
        }
        
        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult SalvarCadastro()
        {
            Funcoes f = new Funcoes();
            Cliente cliente = new Cliente();
            Endereco endereco = new Endereco();
            
            cliente.Nome = f.VerificarStringVazia(Request.Form["nome"]);
            cliente.Documento = f.VerificarStringVazia(Request.Form["documento"]);
            cliente.TipoCliente = f.VerificarStringVazia(Request.Form["tipo_cliente"]);
            cliente.Sobrenome = "";
            cliente.RazaoSocial = "";
            if (cliente.TipoCliente == "fisica")
            {
                cliente.Sobrenome = f.VerificarStringVazia(Request.Form["sobrenome"]);
                //DateTime nascimento = DateTime.Parse(Request.Form["data_nascimento"]);
            }
            else
            {
                cliente.RazaoSocial = f.VerificarStringVazia(Request.Form["razao_social"]);
            }
            
            endereco.Cep = f.VerificarStringVazia(Request.Form["cep"]);
            endereco.Logradouro = f.VerificarStringVazia(Request.Form["logradouro"]);
            endereco.Numero = f.VerificarStringVazia(Request.Form["numero"]);
            endereco.Bairro = f.VerificarStringVazia(Request.Form["bairro"]);
            endereco.Cidade = f.VerificarStringVazia(Request.Form["cidade"]);
            endereco.Uf = f.VerificarStringVazia(Request.Form["uf"]);
            endereco.Complemento = Request.Form["complemento"];
            cliente.Endereco = endereco;

            if (f.Erro > 0) {
                return Content("<script>alert('Atenção, existem campos em branco!');window.history.back(-1);</script>");
            }

            if (cliente.Cadastrar(cliente))
            {
                return RedirectToAction("","Home");
            }
            else
            {
                return Content("<script>alert('Atenção, um erro ocorreu. Tente novamente.');window.history.back(-1);</script>");
            }
      
        }

        public ActionResult Editar(int id)
        {
            Cliente cliente = new Cliente();
            cliente.Id_cliente = id;
            Endereco endereco = new Endereco();
            endereco = endereco.BuscarEnderecoCliente(cliente);
            
            cliente = cliente.SelecionarPorId(id);
            cliente.Endereco = endereco;
            return View(cliente);
        }

        public ActionResult SalvarEdicao()
        {
            Funcoes f = new Funcoes();
            Cliente cliente = new Cliente();
            Endereco endereco = new Endereco();

            cliente.Id_cliente = int.Parse(Request.Form["id_cliente"]);
            cliente.Nome = f.VerificarStringVazia(Request.Form["nome"]);
            cliente.Documento = f.VerificarStringVazia(Request.Form["documento"]);
            cliente.TipoCliente = f.VerificarStringVazia(Request.Form["tipo_cliente"]);
            cliente.Sobrenome = "";
            cliente.RazaoSocial = "";
            if (cliente.TipoCliente == "fisica")
            {
                cliente.Sobrenome = f.VerificarStringVazia(Request.Form["sobrenome"]);
                //DateTime nascimento = DateTime.Parse(Request.Form["data_nascimento"]);
            }
            else
            {
                cliente.RazaoSocial = f.VerificarStringVazia(Request.Form["razao_social"]);
            }
            endereco.IdEndereco = int.Parse(Request.Form["id_endereco"]);
            endereco.Cep = f.VerificarStringVazia(Request.Form["cep"]);
            endereco.Logradouro = f.VerificarStringVazia(Request.Form["logradouro"]);
            endereco.Numero = f.VerificarStringVazia(Request.Form["numero"]);
            endereco.Bairro = f.VerificarStringVazia(Request.Form["bairro"]);
            endereco.Cidade = f.VerificarStringVazia(Request.Form["cidade"]);
            endereco.Uf = f.VerificarStringVazia(Request.Form["uf"]);
            endereco.Complemento = Request.Form["complemento"];
            cliente.Endereco = endereco;

            if (f.Erro > 0)
            {
                return Content("<script>alert('Atenção, existem campos em branco!');window.history.back(-1);</script>");
            }

            if (cliente.Editar(cliente))
            {
                return RedirectToAction("", "Home");
            }
            else
            {
                return Content("<script>alert('Atenção, um erro ocorreu. Tente novamente.');window.history.back(-1);</script>");
            }
        }

        public ActionResult Excluir()
        {
            Cliente cliente = new Cliente();
            Endereco endereco = new Endereco();
            cliente.Id_cliente = int.Parse(Request.Form["idCliente"]);
            endereco = endereco.BuscarEnderecoCliente(cliente);
            cliente.Endereco = endereco;
            if (cliente.Excluir(cliente, endereco))
            {
                return Content("<script>alert(Alteração realizada com sucesso!Recarregue a página);location.reload();</script>");
            }
            else
            {
                return Content("<script>alert('Atenção, um erro ocorreu. Tente novamente.');window.history.back(-1);</script>");
            }
        }
    }
}