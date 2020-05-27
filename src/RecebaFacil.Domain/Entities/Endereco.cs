namespace RecebaFacil.Domain.Entities
{
    public class Endereco : EntityBase<int>
    {
        public Endereco(int enderecoId,
                        int empresaId,
                        string cep,
                        string logradouro,
                        string bairro,
                        string municipio,
                        string uf,
                        string observacao,
                        bool principal,
                        bool ativo)
        {
            Id = enderecoId;
            EmpresaId = empresaId;
            Cep = cep;
            Logradouro = logradouro;
            Bairro = bairro;
            Municipio = municipio;
            Uf = uf;
            Observacao = observacao;
            Principal = principal;
            Ativo = ativo;
        }

        public Endereco(int empresaId,
                       string cep,
                       string logradouro,
                       string bairro,
                       string municipio,
                       string uf,
                       string observacao,
                       bool principal,
                       bool ativo)
        {
            EmpresaId = empresaId;
            Cep = cep;
            Logradouro = logradouro;
            Bairro = bairro;
            Municipio = municipio;
            Uf = uf;
            Observacao = observacao;
            Principal = principal;
            Ativo = ativo;
        }

        public int EmpresaId { get; private set; }
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Municipio { get; private set; }
        public string Uf { get; private set; }
        public string Observacao { get; private set; }
        public bool Principal { get; private set; }
        public bool Ativo { get; private set; }
    }
}
