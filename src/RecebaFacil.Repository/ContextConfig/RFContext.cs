using Microsoft.EntityFrameworkCore;
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Repository.ContextConfig
{
    public class RFContext : DbContext
    {
        public RFContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Encomenda> Encomenda { get; set; }
        public DbSet<EncomendaEmpresa> EncomendaEmpresa { get; set; }
        public DbSet<EncomendaHistoria> EncomendaHistoria { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Expediente> Expediente { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<PreRegistro> PreRegistro { get; set; }
    }
}
