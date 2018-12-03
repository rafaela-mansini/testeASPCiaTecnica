using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clientes.Models
{
    public class Funcoes
    {
        public int Erro { get; set; }
        public string Mensagem { get; set; }

        public string VerificarStringVazia(String valor)
        {
            if (!string.IsNullOrEmpty(valor)) {
                return valor;
            }
            else
            {
                Funcoes f = new Funcoes();
                f.Erro = 1;
                return f.Mensagem = "Campo vazio. Por favor verifique.";
            }
        }
    }
}