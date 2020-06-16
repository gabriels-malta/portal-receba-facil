using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System;
using System.Collections.Generic;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceExpediente : RepositoryBase<Expediente>, IDataServiceExpediente
    {
        public DataServiceExpediente(ISqlAccess databaseHandler)
            : base(databaseHandler)
        {
        }

        public int Alterar(Expediente expediente)
        {
            return ExecuteNonQuery("sproc_Expediente_Alterar", new
            {
                @indIdEmpresa = expediente.PontoRetiradaID,
                @tinDiaSemana = expediente.DiaSemana,
                @tmHoraAbertura = expediente.HoraAbertura,
                @tmHoraEncerramento = expediente.HoraEncerramento
            });
        }

        public IEnumerable<Expediente> ObterPorEmpresa(int empresaId)
        {
            return ExecuteToEnumerable("sproc_Expediente_ObterPorEmpresa", new { intIdEmpresa = empresaId });
        }

        public Expediente ObterPorId(Guid id)
        {
            return ExecuteToFirstOrDefault("sproc_Expediente_ObterPorId", new { id });
        }

        public int Salvar(Expediente expediente)
        {
            return ExecuteNonQuery("sproc_Expediente_Inserir", new
            {
                id = Guid.NewGuid(),
                indIdEmpresa = expediente.PontoRetiradaID,
                tinDiaSemana = expediente.DiaSemana,
                tmHoraAbertura = expediente.HoraAbertura,
                tmHoraEncerramento = expediente.HoraEncerramento
            });
        }
    }
}
