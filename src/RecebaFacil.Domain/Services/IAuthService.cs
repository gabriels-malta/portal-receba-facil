using RecebaFacil.Domain.Entities;
using System;

namespace RecebaFacil.Domain.Services
{
    public interface IAuthService
    {
        Guid Autenticar(string email, string senha);
    }
}
