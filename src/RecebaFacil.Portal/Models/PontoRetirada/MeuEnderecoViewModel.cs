using System;
using System.Collections.Generic;
using System.Linq;

namespace RecebaFacil.Portal.Models.PontoRetirada
{
    public class MeuEnderecoViewModel
    {
        public EnderecoViewModel EnderecoAtivo { get; internal set; }
        public IEnumerable<EnderecoViewModel> OutrosEnderecos { get; internal set; }

        public MeuEnderecoViewModel()
        {
            EnderecoAtivo = new EnderecoViewModel();
            OutrosEnderecos = Enumerable.Empty<EnderecoViewModel>();
        }

        public void Configure(IEnumerable<EnderecoViewModel> enderecos)
        {
            EnderecoAtivo = enderecos.FirstOrDefault(x => x.Ativo);
            OutrosEnderecos = enderecos.Where(x => !x.Ativo);
        }

        public class EnderecoViewModel
        {
            public Guid EnderecoId { get; set; }
            public int EmpresaId { get; set; }
            public string Cep { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public string Municipio { get; set; }
            public string Uf { get; set; }
            public string Observacao { get; set; }
            public bool Ativo { get; internal set; }
        }
    }
}
