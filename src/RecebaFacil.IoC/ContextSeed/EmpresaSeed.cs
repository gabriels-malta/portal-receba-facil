using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.IoC.ContextSeed
{
    public class EmpresaSeed : ISeedService
    {
        private readonly IRepositoryBase<Empresa> _repository;
        private readonly ISecurityService _securityService;

        public EmpresaSeed(IRepositoryBase<Empresa> repository,
                           ISecurityService securityService)
        {
            _repository = repository;
            _securityService = securityService;
        }

        public async Task Feed()
        {
            foreach (Empresa empresa in CriarEmpresas())
            {
                await _repository.Salvar(empresa);
            }
        }

        private IEnumerable<Empresa> CriarEmpresas()
        {
            Guid pontoRetiradaId = Guid.Parse("abe3dbb7-baaf-4699-a568-6c6bb21f7d0b");
            Guid pontoVendaId = Guid.Parse("4a1cc27c-1011-4d19-9d9e-c0099266fab3");

            PontoRetirada pontoRetirada = new PontoRetirada
            {
                Id = pontoRetiradaId,
                RazaoSocial = "Josefa e Marina Contábil ME",
                NomeFantasia = "Contabilidade Central",
                Cnpj = "96764621000110",
                DataCadastro = DateTime.UtcNow,
                DataUltimaModificacao = DateTime.UtcNow
            };
            pontoRetirada.Usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id =  Guid.NewGuid(),
                    EmpresaId = pontoRetiradaId,
                    GrupoId = Guid.Parse("44edaf28-785d-49a7-a798-8b1893be19d0"),
                    Login = "josefaemarina",
                    Senha = _securityService.HashValue("123qwe")
                }
            };
            pontoRetirada.Contatos = new List<Contato>()
            {
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.TelefoneFixo,
                    Valor = "1136375784",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoRetiradaId
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Email,
                    Valor = "posvenda@josefaemarinacontabilme.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoRetiradaId
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Site,
                    Valor = "www.josefaemarinacontabilme.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoRetiradaId
                }
            };
            pontoRetirada.Enderecos = new List<Endereco>()
            {
                new Endereco
                {
                    Id = Guid.NewGuid(),
                    EmpresaId = pontoRetiradaId,
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Logradouro = "Estrada da Embratel, 205",
                    Bairro = "Orquidiama Parque Ribeirão",
                    Cep = "07158600",
                    Municipio = "Guarulhos",
                    Uf = "SP"
                }
            };            

            PontoVenda pontoVenda = new PontoVenda
            {
                Id = pontoVendaId,
                RazaoSocial = "Comercio Eletrônico TendiTudo LTDA-ME",
                NomeFantasia = "TendiTudo!",
                Cnpj = "15536219000187",
                DataCadastro = DateTime.UtcNow,
                DataUltimaModificacao = DateTime.UtcNow
            };
            pontoVenda.Usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = Guid.NewGuid(),
                    EmpresaId = pontoVendaId,
                    GrupoId = Guid.Parse("d010bd9d-b922-4cc5-a3fe-cb37bd418802"),
                    Login = "tenditudo",
                    Senha = _securityService.HashValue("123qwe")
                }
            };
            pontoVenda.Contatos = new List<Contato>()
            {
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.TelefoneFixo,
                    Valor = "1438402629",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoVendaId
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Email,
                    Valor = "atentimento@tenditudo.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoVendaId
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Site,
                    Valor = "www.tenditudo.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoVendaId
                }
            };
            pontoVenda.Enderecos = new List<Endereco>()
            {
                new Endereco
                {
                    Id = Guid.NewGuid(),
                    EmpresaId = pontoVendaId,
                    Ativo = true,
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Logradouro = "Praça Rosa Rodrigues Devide, 233",
                    Bairro = "Jardim Paraíso II",
                    Cep = "Botucatu",
                    Municipio = "Guarulhos",
                    Uf = "SP"
                }
            };

            yield return pontoRetirada;
            yield return pontoVenda;
        }
    }
}
