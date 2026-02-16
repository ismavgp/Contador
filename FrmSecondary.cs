using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WinContador.Data;


namespace WinContador
{
    public partial class FrmSecondary : Form
    {
        private Timer countdownTimer;
        private int currentCount;

        public FrmSecondary()
        {
            InitializeComponent();
            InitializeTimer();
        }




        private void FrmSecondary_Load(object sender, EventArgs e)
        {

        }

        private void FrmSecondary_Resize(object sender, EventArgs e)
        {

        }


        private void InitializeTimer()
        {
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1 segundo
            countdownTimer.Tick += CountdownTimer_Tick;
            currentCount = 0;
            lblTimer.Text = "00";
        }

        private async void CountdownTimer_Tick(object sender, EventArgs e)
        {
            currentCount--;
            lblTimer.Text = currentCount.ToString("00");

            if (currentCount <= 0)
            {
                countdownTimer.Stop();
                lblTimer.Text = "00";

                ConfigRepository configRepository = new ConfigRepository();
                configRepository.CrearBaseSiNoExiste();
               string alerta = configRepository.Obtener("SonidoAlerta");

                //mostrar la ruta donde se ejecuta y donde busca el mp3
                string rutaBase = Application.StartupPath;
                string rutaArchivo = Path.Combine(rutaBase, alerta+".mp3");

                // Mostrar desde dónde se ejecuta y qué archivo intenta abrir
                //MessageBox.Show($"Ruta de ejecución:\n{rutaBase}\n\nArchivo buscado:\n{rutaArchivo}");

                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("No se encontró el archivo MP3.");
                    return;
                }

                _= ReproducirMp3(alerta);
            }
        }

        public void SetCountdown(int contador)
        {
            // Detener el timer si ya está corriendo
            countdownTimer.Stop();
            
            // Configurar el contador inicial
            currentCount = contador;
            lblTimer.Text = currentCount.ToString("00");
        }

        public void StartCountdown()
        {
            countdownTimer.Start();
        }

        public void StopCountdown()
        {
            countdownTimer.Stop();
            currentCount = 0;
            lblTimer.Text = "00";
        }

        public void UpdateResult(string resultado)
        {
            // Intent: always show the amount formatted for the current culture
            if (decimal.TryParse(resultado,
                                  System.Globalization.NumberStyles.Number,
                                  System.Globalization.CultureInfo.CurrentCulture,
                                  out decimal value))
            {
                // Round to 2 decimal places
                decimal rounded = Math.Round(value, 2);

                // If after rounding the value is an integer, show without decimals
                if (rounded % 1 == 0)
                {
                    lblResultado.Text = "S/ "+ rounded.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
                }
                else
                {
                    // Show exactly two decimals
                    lblResultado.Text = "S/ " + rounded.ToString("N2", System.Globalization.CultureInfo.CurrentCulture);
                }
            }
            else
            {
                // Fallback: show the original text if parsing fails
                lblResultado.Text = "S/ " + resultado;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
