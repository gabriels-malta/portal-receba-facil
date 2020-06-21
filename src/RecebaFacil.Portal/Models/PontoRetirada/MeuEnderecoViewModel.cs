using System;

namespace RecebaFacil.Portal.Models.PontoRetirada
{
    public class MeuEnderecoViewModel
    {
        public Guid EnderecoId { get; set; }
        public int EmpresaId { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Observacao { get; set; }
    }
}
