namespace RecebaFacil.Domain.Entities
{
    public class Usuario : EntityBase<int>
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public byte GrupoId { get; set; }
        public bool Bloqueado { get; set; }
        public bool TrocarSenha { get; set; }
        public int? ContatoId { get; set; }

        public virtual Grupo Grupo { get; private set; }
        public virtual Contato Contato { get; private set; }

        public Usuario AdicionarGrupo(Grupo grupo)
        {
            Grupo = grupo;
            return this;
        }

        public Usuario AdicionarContato(Contato contato)
        {
            Contato = contato;
            return this;
        }
    }
}
