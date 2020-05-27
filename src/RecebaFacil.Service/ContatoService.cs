using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Domain.Services;
using System;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service
{
    public class ContatoService : IContatoService
    {
        private readonly IDataServiceContato _DataServiceContato;
        private readonly IContatoMapper _Mapper;
        private readonly IEmpresaService _EmpresaService;

        public ContatoService(IDataServiceContato dataServiceContato,
                              IContatoMapper mapper,
                              IEmpresaService empresaService)
        {
            _DataServiceContato = dataServiceContato;
            _Mapper = mapper;
            _EmpresaService = empresaService;
        }

        public Contato BuscarPorId(int id)
        {
            using DataSet ds = _DataServiceContato.ObterPorId(id);
            Contato contato = _Mapper.Map(ds.Tables[0].AsEnumerable()).FirstOrDefault();

            if (contato == null)
                throw new Exception("Contato não encontrado");

            return contato;
        }

        public Contato BuscarPorId(int id, bool carregarEmpresa)
        {
            Contato contato = BuscarPorId(id);

            if (carregarEmpresa)
                contato.AdicionarEmpresa(_EmpresaService.BuscarPorId(contato.EmpresaID));

            return contato;
        }

        public int Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public int Salvar(Contato entity)
        {
            throw new NotImplementedException();
        }
    }
}
