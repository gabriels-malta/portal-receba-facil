using RecebaFacil.Domain.Entities;
using System.Data;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceEndereco : IDataServiceBase<int>
    {
        DataSet ObterEnderecoPorEmpresa(int empresaId, bool somentePrincipal); 
        int Salvar(Endereco endereco);
    }
}
