using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AcessoBancoDados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class PedidoItemNegocios
    {
        AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();

        public string Inserir(PedidoItem pedidoItem) 
        {
            try
            {
                acessoDados.LimparParametros();

                acessoDados.AdicionarParametros("@IDPedido",pedidoItem.Pedido.IDPedido);
                acessoDados.AdicionarParametros("@IDProduto",pedidoItem.Produto.IDProduto);
                acessoDados.AdicionarParametros("@Quantidade",pedidoItem.Quantidade);
                acessoDados.AdicionarParametros("@ValorUnitario", pedidoItem.ValorUnitario);
                acessoDados.AdicionarParametros("@PercentualDesconto", pedidoItem.PercentualDesconto);
                acessoDados.AdicionarParametros("@ValorDesconto", pedidoItem.ValorDesconto);
                acessoDados.AdicionarParametros("@ValorTotal", pedidoItem.ValorTotal);

                string idProduto = acessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "uspPedidoItemInserir").ToString();

                return idProduto;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
             
        }

        public PedidoItemColecao Consultar(int idPedido) 
        {
            try
            {
                PedidoItemColecao pedidoItemColecao = new PedidoItemColecao();
                
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@IDPedido",idPedido);

                DataTable dataTable = acessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspPedidoItemConsultar");

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    PedidoItem pedidoItem = new PedidoItem();

                    pedidoItem.Pedido = new Pedido();
                    pedidoItem.Pedido.IDPedido = Convert.ToInt32(dataRow["IDPedido"]);

                    pedidoItem.Produto = new Produto();
                    pedidoItem.Produto.IDProduto = Convert.ToInt32(dataRow["IDProduto"]);
                    pedidoItem.Produto.Descricao = Convert.ToString(dataRow["DescProduto"]);

                    pedidoItem.Quantidade = Convert.ToInt32(dataRow["Quantidade"]);
                    pedidoItem.ValorUnitario = Convert.ToDecimal(dataRow["ValorUnitario"]);
                    pedidoItem.PercentualDesconto = Convert.ToDecimal(dataRow["PercentualDesconto"]);
                    pedidoItem.ValorDesconto = Convert.ToDecimal(dataRow["ValorDesconto"]);
                    pedidoItem.ValorTotal = Convert.ToDecimal(dataRow["ValorTotal"]);
                   
                    pedidoItemColecao.Add(pedidoItem);
                }

                return pedidoItemColecao;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao consultar item do pedido. Detalhes : "+ ex.Message);
            }
        
        }






    }
}
