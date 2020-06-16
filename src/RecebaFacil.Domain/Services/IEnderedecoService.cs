
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IEnderedecoService
    {
        Endereco ObterPorId(int id);
        Endereco ObterPorEmpresa(int empresaId);
        int Salvar(Endereco endereco);
    }
}
