﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using AcessoBancoDados.Properties;

namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //Criar a conexão
        private SqlConnection CriarConexao() {

            //string strSql = "Data Source=BRUNO-PC\\MSSQLSERVER2014;Initial Catalog=SistemaEstoque;User ID=kuba;Password=sql123";

            return new SqlConnection(Settings.Default.stringConexao);
        }

        //Parâmetros que vão para o banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void LimparParametros() {
            sqlParameterCollection.Clear();
        }

        public void AdicionarParametros(string nomeParametro,object valorParametro){
            
            sqlParameterCollection.Add(new SqlParameter(nomeParametro,valorParametro));
        }

        //Persistência - Inserir,Alterar,Excluir
        public object ExecutarManipulacao(CommandType commandType, string nomeStoredProcedureOuTextoSql) 
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir Conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando(dentro da caixa que vai trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //em segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Executar o comando, ou seja , manda o comando ir até o banco de dados 
                return sqlCommand.ExecuteScalar();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        
        
        }

        //Consultar registros do banco de dados
        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql) {

            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = CriarConexao();
                
                //Abrir conexão
                sqlConnection.Open();
                
                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                
                //Colocando as coisas dentro do comando
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; 

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Criar um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //DataTable = tabela de dados vazia
                DataTable dataTable = new DataTable();

                //Mandar o comando ir até o banco buscar os dados e o adaptador preencher o datatable
                sqlDataAdapter.Fill(dataTable);


                return dataTable;


            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
                   
        }
    }
}
