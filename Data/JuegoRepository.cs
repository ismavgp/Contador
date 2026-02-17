using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using WinContador.Entity;
using WinContador.Utils;

namespace WinContador.Data
{
    public class JuegoRepository
    {
        private readonly string _dbPath;
        private readonly string _connectionString;
        private readonly string _logPath;

        public JuegoRepository()
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
                Log("=== Inicio CrearBaseSiNoExiste (Juegos) ===");

                if (!File.Exists(_dbPath))
                {
                    Log("Base de datos no encontrada. Creando archivo...");
                    SQLiteConnection.CreateFile(_dbPath);
                    Log("Archivo contador.db creado correctamente.");
                }

                using (var conn = new SQLiteConnection(_connectionString))
                {
                    conn.Open();
                    Log("Conexión SQLite abierta correctamente.");

                    string sql = @"
                CREATE TABLE IF NOT EXISTS Juegos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Fecha TEXT,
                    Hora TEXT,
                    Monto REAL,
                    PorcentajeUtilidad TEXT,
                    Utilidad REAL
                );";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Log("Tabla Juegos verificada/creada correctamente.");
                    }
                }

                Log("=== Fin CrearBaseSiNoExiste (Juegos) ===");
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
                Log("STACKTRACE: " + ex.StackTrace);
                throw;
            }
        }

        private void Log(string mensaje)
        {
            File.AppendAllText(_logPath,
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensaje}{Environment.NewLine}");
        }

        public bool Insertar(JuegoEntity juego)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
                INSERT INTO Juegos
                (Fecha, Hora, Monto, PorcentajeUtilidad, Utilidad)
                VALUES
                (@Fecha, @Hora, @Monto, @PorcentajeUtilidad, @Utilidad);";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Fecha", juego.Fecha.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Hora", juego.Hora);
                    cmd.Parameters.AddWithValue("@Monto", juego.Monto);
                    cmd.Parameters.AddWithValue("@PorcentajeUtilidad", juego.PorcentajeUtilidad);
                    cmd.Parameters.AddWithValue("@Utilidad", juego.Utilidad);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<JuegoResultEntity> ObtenerTodos(string dtFiltro)
        {
            var lista = new List<JuegoResultEntity>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Juegos WHERE Fecha = @Fecha";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Fecha", dtFiltro);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal monto = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                            decimal utilidad = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);

                            lista.Add(new JuegoResultEntity
                            {
                                Id = reader["Id"].ToString(),
                                Fecha = reader["Fecha"] != DBNull.Value
    ? DateTime.Parse(reader["Fecha"].ToString()).ToString("dd/MM/yyyy")
    : "",
                                Hora = reader["Hora"]?.ToString(),
                                Monto = FormatoNumerico.FormatDecimal(monto),
                                PorcentajeUtilidad = reader["PorcentajeUtilidad"]?.ToString(),
                                Utilidad = FormatoNumerico.FormatDecimal(utilidad)
                            });
                        }
                    }
                }
            }

            return lista;
        }


        public List<JuegoResultEntity> ObtenerPorRango(string fechaInicio, string fechaFin)
        {
            var lista = new List<JuegoResultEntity>();

            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT * FROM Juegos WHERE Fecha >= @FechaInicio AND Fecha <= @FechaFin";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal monto = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                            decimal utilidad = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);

                            lista.Add(new JuegoResultEntity
                            {
                                Id = reader["Id"].ToString(),
                                Fecha = reader["Fecha"] != DBNull.Value
    ? DateTime.Parse(reader["Fecha"].ToString()).ToString("dd/MM/yyyy")
    : "",
                                Hora = reader["Hora"]?.ToString(),
                                Monto = FormatoNumerico.FormatDecimal(monto),
                                PorcentajeUtilidad = reader["PorcentajeUtilidad"]?.ToString(),
                                Utilidad = FormatoNumerico.FormatDecimal(utilidad)
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public string ObtenerSiguienteId()
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT IFNULL(MAX(Id),0) FROM Juegos";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    int maxId = Convert.ToInt32(cmd.ExecuteScalar());
                    return (maxId + 1).ToString();
                }
            }
        }

        public bool Actualizar(JuegoEntity juego)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
                UPDATE Juegos SET
                    Fecha = @Fecha,
                    Hora = @Hora,
                    Monto = @Monto,
                    PorcentajeUtilidad = @PorcentajeUtilidad,
                    Utilidad = @Utilidad
                WHERE Id = @Id;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Fecha", juego.Fecha.ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("@Hora", juego.Hora);
                    cmd.Parameters.AddWithValue("@Monto", juego.Monto);
                    cmd.Parameters.AddWithValue("@PorcentajeUtilidad", juego.PorcentajeUtilidad);
                    cmd.Parameters.AddWithValue("@Utilidad", juego.Utilidad);
                    cmd.Parameters.AddWithValue("@Id", juego.Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ExisteId(int id)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = "SELECT COUNT(1) FROM Juegos WHERE Id = @Id;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
