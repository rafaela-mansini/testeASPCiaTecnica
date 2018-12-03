using Clientes.Models;
using Clientes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clientes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Cliente clientes = new Cliente();
            List<Cliente> clientesPF = clientes.SelecionarPorTipo("fisica");
            List<Cliente> clientesPJ = clientes.SelecionarPorTipo("juridica");

            var viewModel = new ClientesViewModel
            {
                ClientesPF = clientesPF,
                ClientesPJ = clientesPJ
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}