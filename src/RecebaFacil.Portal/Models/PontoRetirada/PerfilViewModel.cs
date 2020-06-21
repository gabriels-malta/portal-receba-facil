using System;
using System.Collections.Generic;

namespace RecebaFacil.Portal.Models.PontoRetirada
{
    public class PerfilViewModel
    {
        public string Usuario { get; internal set; }
        public DateTime DataCadastro { get; internal set; }
        public string Cnpj { get; internal set; }
        public string NomeFantasia { get; internal set; }
        public string RazaoSocial { get; internal set; }
        public IEnumerable<ContatoViewModel> Contatos { get; internal set; }
    }

    public class ContatoViewModel
    {
        public Guid Id { get; set; }
        public string Valor { get; set; }
    }
}
