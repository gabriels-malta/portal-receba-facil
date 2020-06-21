using RecebaFacil.Domain.Core.BaseEntities;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class PreRegistro : IEntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string NomeEmpresa { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string Cnpj { get; set; }
        public string Objetivo { get; set; }
        public Guid Id { get; set; }
    }
}
