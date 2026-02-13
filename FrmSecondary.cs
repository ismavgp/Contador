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
using NAudio.Wave;
using System.IO;


namespace WinContador
{
    public partial class FrmSecondary : Form
    {
        private Timer countdownTimer;
        private int currentCount;
        private Font originalTimerFont;
        private Font originalResultFont;
        private Font originalLabelFont;

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

                //mostrar la ruta donde se ejecuta y donde busca el mp3
                string rutaBase = Application.StartupPath;
                string rutaArchivo = Path.Combine(rutaBase, "alerta2.mp3");

                // Mostrar desde dónde se ejecuta y qué archivo intenta abrir
                MessageBox.Show($"Ruta de ejecución:\n{rutaBase}\n\nArchivo buscado:\n{rutaArchivo}");

                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("No se encontró el archivo MP3.");
                    return;
                }

                using (var audioFile = new AudioFileReader("alerta2.mp3"))
                {
                    var outputDevice = new WaveOutEvent();

                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    await Task.Delay(3000);

                    outputDevice.Stop();
                }
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
                    lblResultado.Text = rounded.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
                }
                else
                {
                    // Show exactly two decimals
                    lblResultado.Text = rounded.ToString("N2", System.Globalization.CultureInfo.CurrentCulture);
                }
            }
            else
            {
                // Fallback: show the original text if parsing fails
                lblResultado.Text = resultado;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
