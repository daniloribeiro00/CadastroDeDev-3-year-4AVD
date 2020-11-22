using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_de_dev.Models
{
    public class Empresa
    {
        public long? EmpresaID { get; set; }
        public string NomeEmpresa { get; set; }

        public virtual ICollection<Desenvolvedor> Desenvolvedor { get; set; }
    }
}
