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
            Empresa recebaFacil = CriarRecebaFacil();
            PontoRetirada pontoRetirada = CriarPontoRetirada();
            PontoVenda pontoVenda = CriarPontoVenda();

            yield return recebaFacil;
            yield return pontoRetirada;
            yield return pontoVenda;
        }

        private Empresa CriarRecebaFacil()
        {
            Empresa recebaFacil = new Empresa
            {
                Id = Guid.Parse("37479693-b577-499d-ac43-53e253946a80"),
                Cnpj = "74650274000162",
                DataCadastro = DateTime.UtcNow,
                DataUltimaModificacao = DateTime.UtcNow,
                RazaoSocial = "Receba Facil LTDA",
                NomeFantasia = "Receba Facil"
            };

            recebaFacil.Usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = Guid.Parse("5b6cfb25-7414-4842-997d-796394c57f35"),
                    EmpresaId = recebaFacil.Id,
                    GrupoId = Guid.Parse("8bfe7e07-52d4-420b-a349-0037d98a84ef"),
                    Login = "recebafacil",
                    Senha = _securityService.HashValue("recebafacil")
                }
            };

            return recebaFacil;
        }

        private PontoRetirada CriarPontoRetirada()
        {
            PontoRetirada pontoRetirada = new PontoRetirada
            {
                Id = Guid.Parse("38206f56-9480-4d2a-9745-187775fc05b8"),
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
                    EmpresaId = pontoRetirada.Id,
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
                    EmpresaId = pontoRetirada.Id
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Email,
                    Valor = "posvenda@josefaemarinacontabilme.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoRetirada.Id
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Site,
                    Valor = "www.josefaemarinacontabilme.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoRetirada.Id
                }
            };
            pontoRetirada.Enderecos = new List<Endereco>()
            {
                new Endereco
                {
                    Id = Guid.NewGuid(),
                    EmpresaId = pontoRetirada.Id,
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
            return pontoRetirada;
        }

        private PontoVenda CriarPontoVenda()
        {
            PontoVenda pontoVenda = new PontoVenda
            {
                Id = Guid.Parse("4a1cc27c-1011-4d19-9d9e-c0099266fab3"),
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
                    EmpresaId = pontoVenda.Id,
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
                    EmpresaId = pontoVenda.Id
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Email,
                    Valor = "atentimento@tenditudo.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoVenda.Id
                },
                new Contato
                {
                    Id = Guid.NewGuid(),
                    TipoContato = TipoContato.Site,
                    Valor = "www.tenditudo.com.br",
                    DataCadastro = DateTime.UtcNow,
                    DataUltimaModificacao = DateTime.UtcNow,
                    Ativo = true,
                    EmpresaId = pontoVenda.Id
                }
            };
            pontoVenda.Enderecos = new List<Endereco>()
            {
                new Endereco
                {
                    Id = Guid.NewGuid(),
                    EmpresaId = pontoVenda.Id,
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
            return pontoVenda;
        }
    }
}
