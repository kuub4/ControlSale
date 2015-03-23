using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AcessoBancoDados;
using ObjetoTransferencia;
using System.Data;

namespace Negocios
{
    public class PedidoNegocios
    {
        AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
    
        //Inserir
        public string Inserir(Pedido pedido) 
        {
           try
            {
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@IDOperacao", pedido.Operacao.IDOperacao);
                acessoDados.AdicionarParametros("@IDSituacao", pedido.Situacao.IDSituacao);
                acessoDados.AdicionarParametros("@IDPessoaEmitente", pedido.Emitente.IDPessoa);
                acessoDados.AdicionarParametros("@IDPessoaDestinario", pedido.Destinatario.IDPessoa);

                string idPedido = acessoDados.ExecutarManipulacao(CommandType.StoredProcedure, "uspPedidoInserir").ToString();

                return idPedido;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        
        }

        public PedidoColecao ConsultarPorData(DateTime dataInicial,DateTime dataFinal) 
        {
            try
            {
                PedidoColecao pedidoColecao = new PedidoColecao();
                
                acessoDados.LimparParametros();
                acessoDados.AdicionarParametros("@DataInicial",dataInicial);
                acessoDados.AdicionarParametros("@DataFinal", dataFinal);

                DataTable dataTable = acessoDados.ExecutarConsulta(CommandType.StoredProcedure, "uspPedidoConsultarPorData");

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Pedido pedido = new Pedido();
                    pedido.IDPedido = Convert.ToInt32(dataRow["IDPedido"]);
                    pedido.DataHora = Convert.ToDateTime(dataRow["DataHora"]);

                    pedido.Operacao = new Operacao();
                    pedido.Operacao.IDOperacao = Convert.ToInt32(dataRow["IDOperacao"]);
                    pedido.Operacao.Descricao = dataRow["DescOperacao"].ToString();

                    pedido.Situacao = new Situacao();
                    pedido.Situacao.IDSituacao = Convert.ToInt32(dataRow["IDSituacao"]);
                    pedido.Situacao.Descricao = dataRow["DescSituacao"].ToString();

                    pedido.Emitente = new Pessoa();
                    pedido.Emitente.IDPessoa = Convert.ToInt32(dataRow["IDPessoaEmitente"]);
                    pedido.Emitente.Nome = dataRow["NomeEmitente"].ToString();

                    pedido.Destinatario = new Pessoa();
                    pedido.Destinatario.IDPessoa = Convert.ToInt32(dataRow["IDPessoaDestinatario"]);
                    pedido.Destinatario.Nome = dataRow["NomeDestinatario"].ToString();


                    pedidoColecao.Add(pedido);
                }

                return pedidoColecao;
            }
            catch (Exception erroOcorrido)
            {
                
                throw new Exception("Erro ao consultar pedido por data. Detalhes: "+erroOcorrido.Message);
            }
        
        }
      
    }
}
