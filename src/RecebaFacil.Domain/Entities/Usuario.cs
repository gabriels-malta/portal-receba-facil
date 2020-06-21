using RecebaFacil.Domain.Core.BaseEntities;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class Usuario : IEntityBase
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Guid GrupoId { get; set; }
        public bool Bloqueado { get; set; }
        public bool TrocarSenha { get; set; }
        public Guid EmpresaId { get; set; }

        public virtual Grupo Grupo { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
