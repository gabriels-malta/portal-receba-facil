using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceUsuario : RepositoryBase<Usuario>, IDataServiceUsuario
    {
        public DataServiceUsuario(ISqlAccess sqlAccess)
            : base(sqlAccess)
        { }

        public long BuscarPorAutenticacao(string email, string senha)
        {
            return Convert.ToInt64(ExecuteScalar("sproc_Usuario_ObterPorAutenticacao", new
            {
                login = email,
                senha
            }));
        }

        public Usuario ObterPorId(long id)
        {
            return ExecuteToFirstOrDefault("sproc_Usuario_ObterPorId", new { id });
        }
    }
}
