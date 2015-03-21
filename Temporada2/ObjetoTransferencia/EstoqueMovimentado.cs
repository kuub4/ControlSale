using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjetoTransferencia
{
    public class EstoqueMovimentado
    {
        //public int IDEstoqueMovimentado{ get; set; }
        public Filial Filial { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataHora { get; set; }
        public string EntrouSaiu { get; set; }
    }
}
