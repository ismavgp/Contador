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
            //cargar lista de sonidos desde los resources
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
    }
}
