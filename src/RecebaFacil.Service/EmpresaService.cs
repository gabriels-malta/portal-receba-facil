using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Domain.Services;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IDataServiceEmpresa _DataServiceEmpresa;
        private readonly IEmpresaMapper _EmpresaMapper;
        private readonly IEnderedecoService _EnderecoService;

        public EmpresaService(IDataServiceEmpresa dataServiceEmpresa,
                              IEmpresaMapper empresaMapper, 
                              IEnderedecoService enderecoService)
        {
            _DataServiceEmpresa = dataServiceEmpresa;
            _EmpresaMapper = empresaMapper;
            _EnderecoService = enderecoService;
        }

        public Empresa BuscarPorId(int id)
        {
            using DataSet ds = _DataServiceEmpresa.ObterPorId(id);
            return _EmpresaMapper.Map(ds.Tables[0].AsEnumerable()).FirstOrDefault();
        }

        public Empresa BuscarPorId(int id, bool carregarEndereco)
        {
            Empresa empresa = BuscarPorId(id);

            if(carregarEndereco)
                empresa.AdicionarEndereco(_EnderecoService.ObterPorEmpresa(empresaId: id));

            return empresa;
        }
    }
}
