using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_de_dev.Models
{
    public class Linguagem
    {
        public long? LinguagemID { get; set; }
        public string NomeLinguagem { get; set; }
        public virtual ICollection<DesenvolvedorLinguagem> DesenvolvedorLinguagem { get; set; }
    }
}
