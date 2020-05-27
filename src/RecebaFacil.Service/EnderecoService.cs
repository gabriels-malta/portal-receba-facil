using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Domain.Services;
using System;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service
{
    public class EnderecoService : IEnderedecoService
    {
        private readonly IDataServiceEndereco _DataServiceEndereco;
        private readonly IEnderecoMapper _EnderecoMapper;

        public EnderecoService(IDataServiceEndereco dataServiceEndereco,
                               IEnderecoMapper enderecoMapper)
        {
            _DataServiceEndereco = dataServiceEndereco;
            _EnderecoMapper = enderecoMapper;
        }

        public Endereco BuscarPorId(int id)
        {
            using DataSet ds = _DataServiceEndereco.ObterPorId(id);
            return _EnderecoMapper.Map(ds.Tables[0].AsEnumerable()).FirstOrDefault();
        }

        public Endereco ObterPorEmpresa(int empresaId)
        {
            using DataSet ds = _DataServiceEndereco.ObterEnderecoPorEmpresa(empresaId, somentePrincipal: true);
            return _EnderecoMapper.Map(ds.Tables[0].AsEnumerable()).FirstOrDefault();
        }

        public int Salvar(Endereco endereco)
        {
            if (endereco.EmpresaId < 0)
                throw new RecebaFacilException("Empresa inválida");

            if (string.IsNullOrWhiteSpace(endereco.Cep) || endereco.Cep.Length > 8)
                throw new RecebaFacilException("CEP inválida");

            if (string.IsNullOrWhiteSpace(endereco.Uf) || endereco.Uf.Length != 2)
                throw new RecebaFacilException("UF inválida");

            try
            {
                return _DataServiceEndereco.Salvar(endereco);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new RecebaFacilException("Erro ao salvar o endereço");
            }
        }
    }
}
