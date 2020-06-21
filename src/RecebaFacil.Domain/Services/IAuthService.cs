using System;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IAuthService
    {
        Task<Guid> Autenticar(string email, string senha);
    }
}
