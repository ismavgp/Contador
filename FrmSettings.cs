using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinContador.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinContador
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            CargarSonidos();
        }




        private void CargarSonidos()
        {

            cboSonido.Items.AddRange(new string[]
            {
        nameof(Properties.Resources.alerta),
        nameof(Properties.Resources.alerta2)    
            });

            cboSonido.SelectedIndex = 0;

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ConfigRepository repo = new ConfigRepository();
            repo.CrearBaseSiNoExiste();

            repo.Guardar("SonidoAlerta", cboSonido.SelectedItem.ToString());
            this.Close();

        }

        private async Task ReproducirMp3(string nombreRecurso)
        {
            var bytes = (byte[])Properties.Resources
                .ResourceManager.GetObject(nombreRecurso);

            using (var ms = new MemoryStream(bytes))
            using (var reader = new Mp3FileReader(ms))
            using (var output = new WaveOutEvent())
            {
                output.Init(reader);
                output.Play();

                await Task.Delay(3000); // o esperar PlaybackStopped
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _ = ReproducirMp3(cboSonido.SelectedItem.ToString());
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            ConfigRepository repo = new ConfigRepository();
            repo.CrearBaseSiNoExiste();
            

            string valor = repo.Obtener("SonidoAlerta");
            if (valor != null)
            {
                cboSonido.SelectedItem = valor;
            }
        }

        private void cboSonido_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Si el ítem está seleccionado, usamos verde
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Green, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds);
            }

            // Dibujar el texto
            e.Graphics.DrawString(cboSonido.Items[e.Index].ToString(),
                e.Font, Brushes.White, e.Bounds);
        }
    }
}
