using Microsoft.EntityFrameworkCore;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecebaFacil.Repository
{
    public class RepositoryUsuario : RepositoryBase<Usuario>, IRepositoryUsuario
    {
        public RepositoryUsuario(RFContext context)
            : base(context)
        { }

        public async override ValueTask<Usuario> ObterPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Usuario
                .Include(x => x.Grupo)
                .Include(x => x.Empresa)
                .FirstAsync(x => x.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
