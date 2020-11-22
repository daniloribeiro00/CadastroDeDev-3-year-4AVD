using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_de_dev.Models
{
    public class Desenvolvedor
    {
        public long? DesenvolvedorID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public virtual ICollection<DesenvolvedorLinguagem> DesenvolvedorLinguagem { get; set; }

        public long? EmpresaID { get; set; }
        public Empresa Empresa { get; set; }

        public string NomeEmpresa { get { return Empresa?.NomeEmpresa; } }
    }
}
