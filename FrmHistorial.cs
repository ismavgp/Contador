using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinContador
{
    public partial class FrmHistorial : Form
    {
        private DatabaseHelper dbHelper;

        public FrmHistorial()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void FrmHistorial_Load(object sender, EventArgs e)
        {
            LoadGameHistory();
        }

        private void LoadGameHistory()
        {
            try
            {
                // Limpiar el DataGridView
                dgHistorico.Rows.Clear();

                // Obtener los datos de los juegos
                DataTable gamesData = dbHelper.GetGames();

                // Llenar el DataGridView con los datos
                foreach (DataRow row in gamesData.Rows)
                {
                    string gameInfo = $"N1:{row["Numero1"]} {row["Operacion"]} N2:{row["Numero2"]} = {row["Resultado"]}";
                    DateTime fechaHora = DateTime.Parse(row["FechaHora"].ToString());
                    
                    dgHistorico.Rows.Add(
                        row["Id"].ToString(),
                        fechaHora.ToString("dd/MM/yyyy"),
                        fechaHora.ToString("HH:mm:ss"),
                        gameInfo, // Usar el campo "monto" para mostrar la información del juego
                        $"{row["TiempoContador"]} seg", // Usar el campo "porcentaje" para mostrar el tiempo del contador
                        row["Resultado"].ToString() // Usar el campo "utilidad" para mostrar el resultado
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Botón Buscar - Filtrar por fecha
            try
            {
                DateTime selectedDate = dateTimePicker1.Value.Date;
                
                // Limpiar el DataGridView
                dgHistorico.Rows.Clear();

                // Obtener todos los datos
                DataTable gamesData = dbHelper.GetGames();

                // Filtrar por fecha seleccionada
                foreach (DataRow row in gamesData.Rows)
                {
                    DateTime gameDate = DateTime.Parse(row["FechaHora"].ToString()).Date;
                    
                    if (gameDate == selectedDate)
                    {
                        string gameInfo = $"N1:{row["Numero1"]} {row["Operacion"]} N2:{row["Numero2"]} = {row["Resultado"]}";
                        DateTime fechaHora = DateTime.Parse(row["FechaHora"].ToString());
                        
                        dgHistorico.Rows.Add(
                            row["Id"].ToString(),
                            fechaHora.ToString("dd/MM/yyyy"),
                            fechaHora.ToString("HH:mm:ss"),
                            gameInfo,
                            $"{row["TiempoContador"]} seg",
                            row["Resultado"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
