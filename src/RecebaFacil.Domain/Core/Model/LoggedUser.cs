using System;

namespace RecebaFacil.Domain.Core.Model
{
    public class LoggedUser
    {
        public LoggedUser(
            string login,
            string role,
            string empresa,
            Guid usuarioId,
            Guid empresaId,
            Guid grupoId)
        {
            Login = login;
            Role = role;
            Empresa = empresa;
            UsuarioId = usuarioId;
            EmpresaId = empresaId;
            GrupoId = grupoId;
        }

        public string Login { get; private set;}
        public string Role { get; private set;}
        public string Empresa { get; private set;}

        public Guid UsuarioId { get; private set;}
        public Guid EmpresaId { get; private set; }
        public Guid GrupoId { get; private set;}
    }
}
