using System;
using System.Data.SQLite;
using System.IO;

namespace WinContador.Data
{
    public class ConfigRepository
    {
        private readonly string _dbPath;
        private readonly string _connectionString;
        private readonly string _logPath;

        public ConfigRepository()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WinContador");

            Directory.CreateDirectory(appDataPath);

            _dbPath = Path.Combine(appDataPath, "contador.db");
            _logPath = Path.Combine(basePath, "app.log");

            _connectionString = $"Data Source={_dbPath};Version=3;";
        }

        public void CrearBaseSiNoExiste()
        {
            try
            {
                Log("=== Inicio CrearBaseSiNoExiste ===");

                if (!File.Exists(_dbPath))
                {
                    Log("Base de datos no existe. Creando archivo...");
                    SQLiteConnection.CreateFile(_dbPath);
                    Log("Archivo contador.db creado correctamente.");
                }
                else
                {
                    Log("Base de datos ya existe.");
                }

                using (var conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    Log("Conexión abierta correctamente.");

                    string sql = @"
                    CREATE TABLE IF NOT EXISTS Configuracion (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Clave TEXT UNIQUE,
                        Valor TEXT
                    );";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Log("Tabla Configuracion verificada/creada.");
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

                    Log("Valores por defecto verificados.");
                }

                Log("=== Fin CrearBaseSiNoExiste ===");
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
                Log("STACKTRACE: " + ex.StackTrace);
                throw; // relanza para que puedas detectar el error si ocurre
            }
        }
        private void Log(string mensaje)
        {
            File.AppendAllText(_logPath,
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensaje}{Environment.NewLine}");
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
