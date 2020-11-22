using cadastro_de_dev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_de_dev.Data
{
    public class CadastroDeDevDbInitializer
    {
        public static void Initialize(CadastroDeDevContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Desenvolvedores.Any())
            {
                return;
            }

            var empresas = new Empresa[]
            {
                new Empresa{NomeEmpresa = "Google"},
                new Empresa{NomeEmpresa = "Amazon"},
                new Empresa{NomeEmpresa = "Microsoft"}
            };

            foreach (Empresa e in empresas)
            {
                context.Empresas.Add(e);
            }
            context.SaveChanges();

            var linguagens = new Linguagem[]
            {
                new Linguagem{NomeLinguagem = "Java"},
                new Linguagem{NomeLinguagem = "Python"},
                new Linguagem{NomeLinguagem = "C"}
            };

            foreach (Linguagem l in linguagens)
            {
                context.Linguagens.Add(l);
            }
            context.SaveChanges();


            var desenvolvedores = new Desenvolvedor[]
            {
                new Desenvolvedor{Nome = "Rafael Couto", Email = "rafacouto@gmail.com", Telefone = "24999644346", EmpresaID = 1},
                new Desenvolvedor{Nome = "Danilo Ribeiro", Email = "danribeiro@gmail.com", Telefone = "24988173881", EmpresaID = 3}
            };

            foreach (Desenvolvedor d in desenvolvedores)
            {
                context.Desenvolvedores.Add(d);
            }
            context.SaveChanges();

        }
    }
}
