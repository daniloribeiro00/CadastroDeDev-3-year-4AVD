using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_de_dev.Models
{
    public class DesenvolvedorLinguagem
    {
        public long? DesenvolvedorLinguagemID { get; set; }
        public long LinguagemID { get; set; }
        public Linguagem Linguagem { get; set; }
        public long DesenvolvedorID { get; set; }
        public Desenvolvedor Desenvolvedor { get; set; }
    }
}
