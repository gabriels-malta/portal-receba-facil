using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Domain.Services;
using System;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDataServiceUsuario _DataServiceUsuario;
        private readonly IUsuarioMapper _UsuarioMapper;
        private readonly IGrupoService _GrupoService;
        private readonly IContatoService _ContatoService;

        public UsuarioService(IDataServiceUsuario dataServiceUsuario,
                              IGrupoService grupoService,
                              IUsuarioMapper usuarioMapper,
                              IContatoService contatoService)
        {
            _DataServiceUsuario = dataServiceUsuario;
            _GrupoService = grupoService;
            _UsuarioMapper = usuarioMapper;
            _ContatoService = contatoService;
        }

        public Usuario BuscarPorAutenticacao(string login, string senha)
        {
            long usuarioId = _DataServiceUsuario.BuscarPorAutenticacao(login, senha);

            if (usuarioId == 0)
                throw new Exception("Usuário ou senha inválido");

            return BuscarPorId(usuarioId);
        }

        public Usuario BuscarPorId(long id)
        {
            using DataSet ds = _DataServiceUsuario.ObterPorId(id);
            Usuario usuario = _UsuarioMapper.Map(ds.Tables[0].AsEnumerable()).FirstOrDefault();

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            usuario
                .AdicionarGrupo(_GrupoService.BuscarPorId(usuario.GrupoId))
                .AdicionarContato(_ContatoService.BuscarPorId(usuario.ContatoId.GetValueOrDefault(-1), true));

            return usuario;
        }

        public int Excluir(long id)
        {
            throw new NotImplementedException();
        }

        public int Salvar(Usuario entity)
        {
            throw new NotImplementedException();
        }
    }
}
