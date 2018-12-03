using Clientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clientes.ViewModels
{
    public class ClientesViewModel
    {
        public List<Cliente> ClientesPJ{ get; set; }
        public List<Cliente> ClientesPF { get; set; }
    }
}