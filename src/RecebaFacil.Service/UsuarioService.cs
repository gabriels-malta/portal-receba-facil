using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using System;

namespace RecebaFacil.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDataServiceUsuario _DataServiceUsuario;
        private readonly IGrupoService _GrupoService;
        private readonly IContatoService _ContatoService;

        public UsuarioService(IDataServiceUsuario dataServiceUsuario,
                              IGrupoService grupoService,
                              IContatoService contatoService)
        {
            _DataServiceUsuario = dataServiceUsuario;
            _GrupoService = grupoService;
            _ContatoService = contatoService;
        }

        public Usuario BuscarPorAutenticacao(string login, string senha)
        {
            long usuarioId = _DataServiceUsuario.BuscarPorAutenticacao(login, senha);

            if (usuarioId == 0)
                throw new Exception("Usuário ou senha inválido");

            return ObterPorId(usuarioId);
        }

        public Usuario ObterPorId(long id)
        {
            Usuario usuario =  _DataServiceUsuario.ObterPorId(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            usuario
                .AdicionarGrupo(_GrupoService.ObterPorId(usuario.GrupoId))
                .AdicionarContato(_ContatoService.ObterPorId(usuario.ContatoId.GetValueOrDefault(-1), true));

            return usuario;
        }
    }
}
