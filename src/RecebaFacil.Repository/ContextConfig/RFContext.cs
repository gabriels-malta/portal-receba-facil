using Microsoft.EntityFrameworkCore;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;

namespace RecebaFacil.Repository.ContextConfig
{
    public class RFContext : DbContext
    {
        public RFContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Encomenda> Encomenda { get; set; }
        public DbSet<EncomendaHistoria> EncomendaHistoria { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Expediente> Expediente { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<PreRegistro> PreRegistro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Empresa>()
                .HasDiscriminator<TipoEmpresa>("TipoEmpresa")
                .HasValue<Empresa>(TipoEmpresa.SemDados)
                .HasValue<PontoRetirada>(TipoEmpresa.PontoRetirada)
                .HasValue<PontoVenda>(TipoEmpresa.PontoVenda);

            modelBuilder
                .Entity<PontoRetirada>()
                .HasMany(x => x.Expediente)
                .WithOne(x => x.PontoRetirada);
        }
    }
}
