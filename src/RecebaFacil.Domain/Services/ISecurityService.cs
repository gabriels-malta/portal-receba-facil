using System;

namespace RecebaFacil.Domain.Services
{
    public interface ISecurityService
    {
        string EncryptValue(Guid valor);
        string EncryptValue(int valor);
        string EncryptValue(string valor);
        string DecryptValue(string valor);

        string HashValue(string valor);
        bool Match(string hashText, string valor);
    }
}
