using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Domain.Services;
using System;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service
{
    public class GrupoService : IGrupoService
    {
        private readonly IDataServiceGrupo _DataServiceGrupo;
        private readonly IGrupoMapper _GrupoMapper;

        public GrupoService(IDataServiceGrupo dataServiceGrupo, IGrupoMapper grupoMapper)
        {
            _DataServiceGrupo = dataServiceGrupo;
            _GrupoMapper = grupoMapper;
        }

        public Grupo BuscarPorId(short id)
        {
            using DataSet ds = _DataServiceGrupo.ObterPorId(id);
            var grupo = _GrupoMapper.Map(ds.Tables[0].AsEnumerable()).FirstOrDefault();
            return grupo;
        }

        public int Excluir(short id)
        {
            throw new NotImplementedException();
        }

        public int Salvar(Grupo entity)
        {
            throw new NotImplementedException();
        }
    }
}
