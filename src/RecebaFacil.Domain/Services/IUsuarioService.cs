
using RecebaFacil.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> ObterPorId(Guid id);
        Task NovoUsuario(Usuario usuario);
    }
}
