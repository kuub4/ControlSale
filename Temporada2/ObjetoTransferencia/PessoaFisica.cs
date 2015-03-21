using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjetoTransferencia
{
    public class PessoaFisica
    {
        public Pessoa Pessoa { get; set; }
        public String Nome{ get; set; }
        public String CPF { get; set; }
        public String RG{ get; set; }
        public DateTime DataNascimento{ get; set; }
    }
}
