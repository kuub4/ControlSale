using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjetoTransferencia
{
    public class PessoaJuridica
    {
        public Pessoa Pessoa{ get; set; }
        public String NomeFantasia { get; set; }
        public String RazaoSocial{ get; set; }
        public String CNPJ{ get; set; }
        public String InscricaoSocial{ get; set; }
        public DateTime DataFundacao{ get; set; }
    }
}
