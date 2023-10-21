using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobOffer.Infrastructure.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nivels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nivels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recrutadores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recrutadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    Password = table.Column<string>(maxLength: 10, nullable: false),
                    Experiencia = table.Column<string>(maxLength: 255, nullable: false),
                    NivelEscolaridade = table.Column<int>(maxLength: 40, nullable: false),
                    NivelCompeteciaDigitais = table.Column<string>(maxLength: 60, nullable: false),
                    NivelId = table.Column<int>(nullable: false),
                    Idiomas = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatos_Nivels_NivelId",
                        column: x => x.NivelId,
                        principalTable: "Nivels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipo = table.Column<string>(maxLength: 255, nullable: false),
                    Regime = table.Column<string>(maxLength: 100, nullable: false),
                    Horario = table.Column<string>(maxLength: 100, nullable: false),
                    Tipo_contrato = table.Column<string>(maxLength: 100, nullable: false),
                    N_vaga = table.Column<int>(maxLength: 100, nullable: false),
                    RecrutadorId = table.Column<int>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    CargoId = table.Column<int>(nullable: false),
                    EmpresaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vagas_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vagas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vagas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vagas_Recrutadores_RecrutadorId",
                        column: x => x.RecrutadorId,
                        principalTable: "Recrutadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidato_has_vagas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CandidatoId = table.Column<int>(nullable: false),
                    VagaId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(maxLength: 45, nullable: false),
                    Estado_da_Candidatura = table.Column<string>(maxLength: 45, nullable: false),
                    VagaId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato_has_vagas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidato_has_vagas_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidato_has_vagas_Vagas_VagaId",
                        column: x => x.VagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidato_has_vagas_Vagas_VagaId1",
                        column: x => x.VagaId1,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "Disponivel" });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 2, "Analise" });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 3, "Ocupado" });

            migrationBuilder.InsertData(
                table: "Nivels",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "Iniciante" });

            migrationBuilder.InsertData(
                table: "Nivels",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 2, "Medio" });

            migrationBuilder.InsertData(
                table: "Nivels",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 3, "Master" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_has_vagas_CandidatoId",
                table: "Candidato_has_vagas",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_has_vagas_Id",
                table: "Candidato_has_vagas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_has_vagas_VagaId",
                table: "Candidato_has_vagas",
                column: "VagaId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_has_vagas_VagaId1",
                table: "Candidato_has_vagas",
                column: "VagaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_Id",
                table: "Candidatos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_NivelId",
                table: "Candidatos",
                column: "NivelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_Id",
                table: "Cargos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Id",
                table: "Empresas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_Id",
                table: "Estados",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nivels_Id",
                table: "Nivels",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recrutadores_Id",
                table: "Recrutadores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_CargoId",
                table: "Vagas",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_EmpresaId",
                table: "Vagas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_EstadoId",
                table: "Vagas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_Id",
                table: "Vagas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vagas_RecrutadorId",
                table: "Vagas",
                column: "RecrutadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidato_has_vagas");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Vagas");

            migrationBuilder.DropTable(
                name: "Nivels");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Recrutadores");
        }
    }
}
