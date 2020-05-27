using System;

namespace RecebaFacil.Portal.Models.PontoRetirada
{
    public class PerfilViewModel
    {
        public string Usuario { get; internal set; }
        public string Contato { get; internal set; }
        public DateTime DataCadastro { get; internal set; }
        public string Cnpj { get; internal set; }
        public string NomeFantasia { get; internal set; }
        public string RazaoSocial { get; internal set; }
    }
}
