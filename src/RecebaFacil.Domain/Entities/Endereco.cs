using RecebaFacil.Domain.Core.BaseEntities;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class Endereco : LoggableEntity
    {
        public Guid EmpresaId { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Uf { get; set; }
        public string Observacao { get; set; }
        public bool Ativo { get; set; }
    }
}
