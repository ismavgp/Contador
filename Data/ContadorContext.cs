using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using WinContador.Entity;

namespace WinContador.Data
{
    internal class ContadorContext : DbContext
    {
        // Constructor que usa la cadena de conexión desde App.config o genera una dinámica
        public ContadorContext() : base(GetConnectionString())
        {
            // Configurar la inicialización de la base de datos
            ConfigureDatabase();
        }

        // Constructor con cadena de conexión específica
        public ContadorContext(string connectionString) : base(connectionString)
        {
            ConfigureDatabase();
        }

        // DbSet para la entidad Juego
        public virtual DbSet<JuegoEntity> Juegos { get; set; }

        /// <summary>
        /// Configuración inicial de la base de datos
        /// </summary>
        private void ConfigureDatabase()
        {
            try
            {
                // Configurar inicializador si está habilitado en configuración
                bool createIfNotExists = bool.Parse(ConfigurationManager.AppSettings["CreateDatabaseIfNotExists"] ?? "true");
                
                if (createIfNotExists)
                {
                    Database.SetInitializer(new ContadorDatabaseInitializer());
                }

                // Configurar timeout
                Database.CommandTimeout = 30;

                // Habilitar logging si está configurado
                bool enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableDatabaseLogging"] ?? "false");
                if (enableLogging)
                {
                    Database.Log = sql => System.Diagnostics.Debug.WriteLine(sql);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configuring database: {ex.Message}");
            }
        }

        /// <summary>
        /// Configuración del modelo de Entity Framework
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remover convenciones de pluralización para SQLite
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            // Remover convención de eliminación en cascada por defecto
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Configuración específica para JuegoEntity
            modelBuilder.Entity<JuegoEntity>(entity =>
            {
                // Configurar tabla
                entity.ToTable("Juegos");

                // Configurar clave primaria
                entity.HasKey(j => j.Id);

                // Configurar propiedades con validaciones específicas para SQLite
                entity.Property(j => j.Id)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnType("TEXT");

                entity.Property(j => j.Fecha)
                      .HasMaxLength(20)
                      .HasColumnType("TEXT");

                entity.Property(j => j.Hora)
                      .HasMaxLength(20)
                      .HasColumnType("TEXT");

                entity.Property(j => j.Monto)
                      .HasPrecision(18, 2)
                      .HasColumnType("DECIMAL"); // SQLite lo tratará como REAL

                entity.Property(j => j.PorcentajeUtilidad)
                      .HasMaxLength(20)
                      .HasColumnType("TEXT");

                entity.Property(j => j.Utilidad)
                      .HasMaxLength(50)
                      .HasColumnType("TEXT")
                      .IsOptional(); // Puede ser null

                // Índices para optimizar consultas
                entity.HasIndex(j => j.Fecha)
                      .HasName("IX_Juegos_Fecha");
            });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Obtiene la cadena de conexión desde configuración o genera una por defecto
        /// </summary>
        private static string GetConnectionString()
        {
            try
            {
                // Intentar obtener de App.config
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
                if (connectionString != null && !string.IsNullOrEmpty(connectionString.ConnectionString))
                {
                    return connectionString.ConnectionString;
                }

                // Generar cadena de conexión dinámica
                return GenerateDynamicConnectionString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting connection string: {ex.Message}");
                return GenerateDynamicConnectionString();
            }
        }

        /// <summary>
        /// Genera una cadena de conexión dinámica para SQLite
        /// </summary>
        private static string GenerateDynamicConnectionString()
        {
            try
            {
                // Obtener ruta de la base de datos desde configuración o usar por defecto
                string dbFileName = ConfigurationManager.AppSettings["DatabasePath"] ?? "ContadorDB.sqlite";
                string appPath = Application.StartupPath ?? Environment.CurrentDirectory;
                string dbPath = Path.Combine(appPath, dbFileName);
                
                // Asegurar que el directorio existe
                Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? appPath);

                // Construir cadena de conexión optimizada para SQLite
                var builder = new SQLiteConnectionStringBuilder
                {
                    DataSource = dbPath,
                    Version = 3,
                    ForeignKeys = true,
                    JournalMode = SQLiteJournalModeEnum.Wal,
                    DefaultTimeout = 30,
                    SyncMode = SynchronizationModes.Full,
                    PageSize = 4096,
                    CacheSize = 10000,
                    ReadOnly = false,
                    FailIfMissing = false,
                    BinaryGUID = false,
                    DateTimeFormat = SQLiteDateFormats.ISO8601,
                    DateTimeKind = DateTimeKind.Local
                };

                return builder.ConnectionString;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating connection string: {ex.Message}");
                
                // Cadena de conexión de emergencia
                return "Data Source=ContadorDB.sqlite;Version=3;Foreign Keys=True;";
            }
        }

        /// <summary>
        /// Método para probar la conexión a la base de datos
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (var connection = Database.Connection)
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Connection test failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Asegura que la base de datos esté creada y sea accesible
        /// </summary>
        public void EnsureDatabaseReady()
        {
            try
            {
                if (!Database.Exists())
                {
                    Database.Create();
                }

                // Verificar que las tablas existen
                var tableCount = Database.SqlQuery<int>("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Juegos'").First();
                
                if (tableCount == 0)
                {
                    throw new InvalidOperationException("La tabla Juegos no existe en la base de datos");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error ensuring database is ready: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Limpieza específica para SQLite
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    // Limpiar pool de conexiones de SQLite
                    SQLiteConnection.ClearAllPools();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error during disposal: {ex.Message}");
                }
            }
            base.Dispose(disposing);
        }
    }
}