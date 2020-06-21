using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecebaFacil.Repository.Migrations
{
    public partial class CreateContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataUltimaModificacao = table.Column<DateTime>(nullable: false),
                    RazaoSocial = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encomenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PontoVendaId = table.Column<int>(nullable: false),
                    PontoRetiradaId = table.Column<int>(nullable: false),
                    NotaFiscal = table.Column<string>(nullable: true),
                    NumeroPedido = table.Column<string>(nullable: true),
                    DataPedido = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EncomendaEmpresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EncomendaId = table.Column<Guid>(nullable: false),
                    PontoVendaId = table.Column<int>(nullable: false),
                    PontoRetiradaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncomendaEmpresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expediente",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PontoRetiradaId = table.Column<Guid>(nullable: false),
                    DiaSemana = table.Column<int>(nullable: false),
                    HoraAbertura = table.Column<TimeSpan>(nullable: false),
                    HoraEncerramento = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expediente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreRegistro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    NomeEmpresa = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Objetivo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreRegistro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataUltimaModificacao = table.Column<DateTime>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true),
                    TipoContato = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataUltimaModificacao = table.Column<DateTime>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    Cep = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Uf = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncomendaHistoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EncomendaId = table.Column<Guid>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    TipoMovimento = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncomendaHistoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncomendaHistoria_Encomenda_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    GrupoId = table.Column<Guid>(nullable: false),
                    Bloqueado = table.Column<bool>(nullable: false),
                    TrocarSenha = table.Column<bool>(nullable: false),
                    EmpresaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_EmpresaId",
                table: "Contato",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_EncomendaHistoria_EncomendaId",
                table: "EncomendaHistoria",
                column: "EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EmpresaId",
                table: "Endereco",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmpresaId",
                table: "Usuario",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_GrupoId",
                table: "Usuario",
                column: "GrupoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "EncomendaEmpresa");

            migrationBuilder.DropTable(
                name: "EncomendaHistoria");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Expediente");

            migrationBuilder.DropTable(
                name: "PreRegistro");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Encomenda");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Grupo");
        }
    }
}
