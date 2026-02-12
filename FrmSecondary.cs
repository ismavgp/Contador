using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

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

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            currentCount--;
            lblTimer.Text = currentCount.ToString("00");

            if (currentCount <= 0)
            {
                countdownTimer.Stop();
                lblTimer.Text = "00";
                
                // Emitir sonido de alerta cuando el contador llegue a 00
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
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
