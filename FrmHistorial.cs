using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinContador.Data;
using WinContador.Entity;
using System.Globalization;

namespace WinContador
{
    public partial class FrmHistorial : Form
    {
        //private DatabaseHelper dbHelper;

        public FrmHistorial()
        {
            InitializeComponent();
            // dbHelper = new DatabaseHelper();
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

                var repo = new JuegoRepository();
                repo.CrearBaseSiNoExiste();
                List<JuegoEntity> games = repo.ObtenerTodos(dtpFiltro.Value.ToString("dd/MM/yyyy"));
                dgHistorico.DataSource = games;



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Botón Buscar - Filtrar por fecha
            try
            {


                var repo = new JuegoRepository();
                repo.CrearBaseSiNoExiste();

                var resultados = repo.ObtenerTodos(dtpFiltro.Value.ToString("dd/MM/yyyy"));

                dgHistorico.DataSource = resultados;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }
    }
}
