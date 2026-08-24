using Microsoft.Data.Sqlite;
using System;
using System.Drawing.Text;
using System.IO;

namespace SistemaTecPDV.Dados
{
    public static class ConexaoBanco

// Caminho do TecPDV.db
private static string CaminhoBanco =>
    Path.Combine(AppDomain.CurrentDomain.
        BaseDirectory, "TecPDW.db");
    // Devolve a conexão pronta
    public static SqliteConnection

        ObterConexao() =>
        new.SqliteConection(
           $"Data Source={CaminhoBanco}");

    // Testa a conexão com o banco
    public static bool TestarConexao(
         out string msg)
    {
        // Confere se o arquivo existe ANTES
        // do SQLite criar um vazio sozinho.
        if (!File.Exists(CaminhoBanco))
        {
            msg = "Erro: banco nao encontrado" +
     " +
                    "em " + CaminhoBanco;
            return false;

            try
            {
                using var c = ObterConexao();
                c.Open();

                // Confere se a tabela Usuario
                // realmente existe no banco
                var cmd = c.CreateCommand();
                cmd.CommandText =
                    "SELECT name FROM sqlite_master" +
    "+
                    "WHERE type='table' AND " +
                "name='Usuario';";
                var resultado =
                    cmd.ExecuteScalar();
                if (resultado == null)
                {
                    msg = "Erro: banco sem a " +
                           "tabela Usuario. ";
                    return false;
                }

                msg = "Conectado! Tabelas " +
                      "encontradas. ";
                return true;
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
                return false;
            }
        }

    }
}