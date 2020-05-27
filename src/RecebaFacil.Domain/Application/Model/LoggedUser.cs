namespace RecebaFacil.Domain.Application.Model
{
    public class LoggedUser
    {
        public LoggedUser(
            string login,
            string role,
            string empresa,
            string contato,
            int usuarioId,
            int empresaId,
            byte grupoId,
            int contatoId)
        {
            Login = login;
            Role = role;
            Empresa = empresa;
            Contato = contato;
            UsuarioId = usuarioId;
            EmpresaId = empresaId;
            GrupoId = grupoId;
            ContatoId = contatoId;
        }

        public string Login { get; private set;}
        public string Role { get; private set;}
        public string Empresa { get; private set;}
        public string Contato { get; private set;}

        public int UsuarioId { get; private set;}
        public int EmpresaId { get; private set; }
        public byte GrupoId { get; private set;}
        public int ContatoId { get; private set;}
    }
}
