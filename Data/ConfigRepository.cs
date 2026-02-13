using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinContador.Data
{
    public class ConfigRepository
    {
            private readonly string _connectionString =
                "Data Source=contador.db;Version=3;";
    
            public void CrearBaseSiNoExiste()
            {
                using (var conn = new System.Data.SQLite.SQLiteConnection(_connectionString))
                {
                    conn.Open();
    
                    string sql = @"
                    CREATE TABLE IF NOT EXISTS Configuracion (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Clave TEXT UNIQUE,
                        Valor TEXT
                    );";
    
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                //Insertar valores por defecto si no existen
                string insertSql = @"
                    INSERT INTO Configuracion (Clave, Valor)
                    VALUES (@Clave, @Valor)
                    ON CONFLICT(Clave) DO NOTHING;";
    
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Clave", "SonidoAlerta");
                        cmd.Parameters.AddWithValue("@Valor", "alerta");
                        cmd.ExecuteNonQuery();
    

                        //cmd.Parameters.Clear();
                        //cmd.Parameters.AddWithValue("@Clave", "PorcentajeUtilidad");
                        //cmd.Parameters.AddWithValue("@Valor", "20");
                        //cmd.ExecuteNonQuery();
                }

            }
        }
    
            public bool Guardar(string clave, string valor)
            {
                bool res = false;
                using (var conn = new System.Data.SQLite.SQLiteConnection(_connectionString))
                {
                    conn.Open();
    
                    string sql = @"
                    INSERT INTO Configuracion (Clave, Valor)
                    VALUES (@Clave, @Valor)
                    ON CONFLICT(Clave) DO UPDATE SET Valor=excluded.Valor;";
    
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        cmd.Parameters.AddWithValue("@Valor", valor);
    
                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
                return res;
            }
    
            public string Obtener(string clave)
            {
                string valor = null;
                using (var conn = new System.Data.SQLite.SQLiteConnection(_connectionString))
                {
                    conn.Open();
    
                    string sql = "SELECT Valor FROM Configuracion WHERE Clave=@Clave LIMIT 1;";
    
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                            valor = result.ToString();
                    }
                }
                return valor;
        }

    }
}
