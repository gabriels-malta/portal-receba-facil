using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceEndereco
    {
        Endereco ObterPorId(int id);
        Endereco ObterEnderecoPorEmpresa(int empresaId, bool somentePrincipal); 
        int Salvar(Endereco endereco);
    }
}
