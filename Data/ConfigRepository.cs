using System;
using System.Data.SQLite;

namespace WinContador.Data
{
    public class ConfigRepository
    {
        private readonly string _connectionString =
            "Data Source=contador.db;Version=3;";

        public void CrearBaseSiNoExiste()
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
                    CREATE TABLE IF NOT EXISTS Configuracion (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Clave TEXT UNIQUE,
                        Valor TEXT
                    );";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string insertSql = @"
                    INSERT INTO Configuracion (Clave, Valor)
                    VALUES (@Clave, @Valor)
                    ON CONFLICT(Clave) DO NOTHING;";

                using (var cmd = new SQLiteCommand(insertSql, conn))
                {
                    InsertarDefault(cmd, "SonidoAlerta", "alerta");
                    InsertarDefault(cmd, "USER", "sadmin");
                    InsertarDefault(cmd, "PASSWORD", "12345678");
                }
            }
        }

        private void InsertarDefault(SQLiteCommand cmd, string clave, string valor)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Clave", clave);
            cmd.Parameters.AddWithValue("@Valor", valor);
            cmd.ExecuteNonQuery();
        }

        public bool Guardar(string clave, string valor)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
                    INSERT INTO Configuracion (Clave, Valor)
                    VALUES (@Clave, @Valor)
                    ON CONFLICT(Clave) DO UPDATE SET Valor=excluded.Valor;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Clave", clave);
                    cmd.Parameters.AddWithValue("@Valor", valor);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public string Obtener(string clave)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT Valor FROM Configuracion WHERE Clave=@Clave LIMIT 1;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Clave", clave);

                    var result = cmd.ExecuteScalar();
                    return result == null ? null : result.ToString();
                }
            }
        }

        public bool Actualizar(string clave, string valor)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
                    UPDATE Configuracion 
                    SET Valor = @Valor
                    WHERE Clave = @Clave;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Clave", clave);
                    cmd.Parameters.AddWithValue("@Valor", valor);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
