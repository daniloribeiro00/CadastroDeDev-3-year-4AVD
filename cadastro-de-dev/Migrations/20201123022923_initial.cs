using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace cadastro_de_dev.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    EmpresaID = table.Column<long>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    NomeEmpresa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.EmpresaID);
                });

            migrationBuilder.CreateTable(
                name: "Linguagens",
                columns: table => new
                {
                    LinguagemID = table.Column<long>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    NomeLinguagem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linguagens", x => x.LinguagemID);
                });

            migrationBuilder.CreateTable(
                name: "Desenvolvedores",
                columns: table => new
                {
                    DesenvolvedorID = table.Column<long>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    EmpresaID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desenvolvedores", x => x.DesenvolvedorID);
                    table.ForeignKey(
                        name: "FK_Desenvolvedores_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesenvolvedorLinguagens",
                columns: table => new
                {
                    DesenvolvedorLinguagemID = table.Column<long>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    LinguagemID = table.Column<long>(nullable: false),
                    DesenvolvedorID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesenvolvedorLinguagens", x => x.DesenvolvedorLinguagemID);
                    table.ForeignKey(
                        name: "FK_DesenvolvedorLinguagens_Desenvolvedores_DesenvolvedorID",
                        column: x => x.DesenvolvedorID,
                        principalTable: "Desenvolvedores",
                        principalColumn: "DesenvolvedorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesenvolvedorLinguagens_Linguagens_LinguagemID",
                        column: x => x.LinguagemID,
                        principalTable: "Linguagens",
                        principalColumn: "LinguagemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desenvolvedores_EmpresaID",
                table: "Desenvolvedores",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_DesenvolvedorLinguagens_DesenvolvedorID",
                table: "DesenvolvedorLinguagens",
                column: "DesenvolvedorID");

            migrationBuilder.CreateIndex(
                name: "IX_DesenvolvedorLinguagens_LinguagemID",
                table: "DesenvolvedorLinguagens",
                column: "LinguagemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesenvolvedorLinguagens");

            migrationBuilder.DropTable(
                name: "Desenvolvedores");

            migrationBuilder.DropTable(
                name: "Linguagens");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
