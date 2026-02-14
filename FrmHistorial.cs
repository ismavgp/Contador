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
using WinContador.Utils;

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
            dgHistorico.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgHistorico.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgHistorico.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgHistorico.Columns[3].DefaultCellStyle.Alignment =    DataGridViewContentAlignment.MiddleRight;
            dgHistorico.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgHistorico.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            // 1. IMPORTANTE: Permitir que los colores personalizados se apliquen
            dgHistorico.EnableHeadersVisualStyles = false;

            // 2. Estilo de las Cabeceras (Negro con texto blanco)
            dgHistorico.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgHistorico.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgHistorico.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgHistorico.ColumnHeadersHeight = 35; // Un poco más alto para que luzca mejor

            // 3. Estilo de las Celdas (Fondo oscuro, texto verde neón como tus números)
            dgHistorico.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20);
            dgHistorico.DefaultCellStyle.ForeColor = Color.FromArgb(0, 192, 0); // Verde Fiera PKR
            dgHistorico.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 40, 40); // Fondo al seleccionar
            dgHistorico.DefaultCellStyle.SelectionForeColor = Color.White;
            dgHistorico.GridColor = Color.FromArgb(50, 50, 50); // Color de las líneas de la cuadrícula

            // 4. Fondo general del control (la parte donde no hay filas)
            dgHistorico.BackgroundColor = Color.FromArgb(15, 15, 15);
            dgHistorico.BorderStyle = BorderStyle.None;



            dtpFiltro.CalendarMonthBackground = Color.FromArgb(30, 30, 30); // Fondo oscuro
            dtpFiltro.CalendarForeColor = Color.White;                      // Texto blanco
            dtpFiltro.CalendarTitleBackColor = Color.FromArgb(0, 100, 0);   // Título verde oscuro
            dtpFiltro.CalendarTitleForeColor = Color.White;                 // Texto del título
            dtpFiltro.CalendarTrailingForeColor = Color.Gray;

            LoadGameHistory();

            //cambiar el color de fondo de las cabeceras del datagrid


        }

        private void LoadGameHistory()
        {
            try
            {


                var repo = new JuegoRepository();
                repo.CrearBaseSiNoExiste();
                List<JuegoResultEntity> games = repo.ObtenerTodos(dtpFiltro.Value.ToString("dd/MM/yyyy"));
                dgHistorico.DataSource = games;

                decimal totalMonto = games.Sum(x =>    decimal.TryParse(x.Monto, out var monto) ? monto : 0);

                decimal porcentaje = games.Sum(x => decimal.TryParse(x.PorcentajeUtilidad, out var porce) ? porce : 0);
                decimal utilidad = games.Sum(x => decimal.TryParse(x.Utilidad, out var util) ? util : 0);

                txtMontoAcumulado.Text = FormatoNumerico.FormatDecimal(totalMonto);
                txtPorcentajeAcumulado.Text = FormatoNumerico.FormatDecimal(porcentaje);
                txtUtilidadAcumulada.Text = FormatoNumerico.FormatDecimal(utilidad);


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            LoadGameHistory();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgHistorico.RowCount <1)
            {
                MessageBox.Show("No se puede exportar sin cargar la información", "Validación Exportar Excel");
                return;

            }

            var repo = new JuegoRepository();

            List<JuegoResultEntity> games = repo.ObtenerTodos(dtpFiltro.Value.ToString("dd/MM/yyyy"));


            Exportar.Excel(games, dtpFiltro.Value.ToString("dd/MM/yyyy"));

        }
    }
}
