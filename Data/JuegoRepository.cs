using System;
using System.Collections.Generic;
using System.Data.SQLite;
using WinContador.Entity;

namespace WinContador.Data
{
    public class JuegoRepository
    {
        private readonly string _connectionString =
            "Data Source=contador.db;Version=3;";

        public void CrearBaseSiNoExiste()
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

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
                }
            }
        }

        public bool Insertar(JuegoEntity juego)
        {
            bool res = false;
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
                    cmd.Parameters.AddWithValue("@Fecha", juego.Fecha.ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("@Hora", juego.Hora);
                    cmd.Parameters.AddWithValue("@Monto", juego.Monto);
                    cmd.Parameters.AddWithValue("@PorcentajeUtilidad", juego.PorcentajeUtilidad);
                    cmd.Parameters.AddWithValue("@Utilidad", juego.Utilidad);

                    cmd.ExecuteNonQuery();
                    res = true;
                }
            }

            return res;
        }
        public List<JuegoEntity> ObtenerTodos(string dtFiltro)
        {
            var lista = new List<JuegoEntity>();

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
                            lista.Add(new JuegoEntity
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Hora = reader["Hora"].ToString(),
                                Monto = Convert.ToDecimal(reader["Monto"]),
                                PorcentajeUtilidad = reader["PorcentajeUtilidad"].ToString(),
                                Utilidad = Convert.ToDecimal(reader["Utilidad"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public string ObtenerSiguienteId()
        {
            string nextId = "1"; 
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT MAX(Id) FROM Juegos";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        int maxId = Convert.ToInt32(result);
                        nextId = (maxId + 1).ToString();
                    }
                }
            }
            return nextId;
        }

    }
}
