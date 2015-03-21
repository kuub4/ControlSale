using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjetoTransferencia
{
    public class EstoqueReservado
    {
        public Filial Filial { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
