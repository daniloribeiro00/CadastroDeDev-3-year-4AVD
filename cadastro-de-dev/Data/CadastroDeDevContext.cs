using cadastro_de_dev.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_de_dev.Data
{
    public class CadastroDeDevContext : DbContext
    {
        public CadastroDeDevContext(DbContextOptions<CadastroDeDevContext> options): base(options)
        {

        }

        public DbSet<Desenvolvedor> Desenvolvedores { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Linguagem> Linguagens { get; set; }
        public DbSet<DesenvolvedorLinguagem> DesenvolvedorLinguagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DesenvolvedorLinguagem>()
                .HasKey(cd => new { cd.DesenvolvedorLinguagemID });
            modelBuilder.Entity<DesenvolvedorLinguagem>()
                .HasOne(c => c.Linguagem)
                .WithMany(cd => cd.DesenvolvedorLinguagem)
                .HasForeignKey(c => c.LinguagemID);
            modelBuilder.Entity<DesenvolvedorLinguagem>()
                .HasOne(d => d.Desenvolvedor)
                .WithMany(cd => cd.DesenvolvedorLinguagem)
                .HasForeignKey(d => d.DesenvolvedorID);
        }
    }
}
