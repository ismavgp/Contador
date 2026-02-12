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
        private Size originalFormSize;

        public FrmSecondary()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeResponsiveDesign();
        }

        private void InitializeResponsiveDesign()
        {
            // Guardar fuentes y tamaño originales
            originalTimerFont = new Font(lblTimer.Font.FontFamily, lblTimer.Font.Size, lblTimer.Font.Style);
            originalResultFont = new Font(lblResultado.Font.FontFamily, lblResultado.Font.Size, lblResultado.Font.Style);
            originalLabelFont = new Font(label2.Font.FontFamily, label2.Font.Size, label2.Font.Style);
            originalFormSize = this.ClientSize;

            // Configurar el form para ser redimensionable
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new Size(400, 300);

            // Suscribirse al evento Resize
            this.Resize += FrmSecondary_Resize;
            this.Load += FrmSecondary_Load;

            // Configurar anclaje inicial
            SetupAnchoring();
        }

        private void SetupAnchoring()
        {
            // Configurar anclaje para mantener centrado
            lblTimer.Anchor = AnchorStyles.Top;
            label2.Anchor = AnchorStyles.None;
            label3.Anchor = AnchorStyles.None;
            lblResultado.Anchor = AnchorStyles.None;
        }

        private void FrmSecondary_Load(object sender, EventArgs e)
        {
            CenterControls();
            AdjustFontSizes();
        }

        private void FrmSecondary_Resize(object sender, EventArgs e)
        {
            CenterControls();
            AdjustFontSizes();
        }

        private void CenterControls()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Asegurar que las dimensiones del control estén actualizadas
            Application.DoEvents();

            // Centrar lblTimer horizontalmente en la parte superior
            int timerY = Math.Max(20, formHeight / 10); // Mínimo 20px, máximo 10% de la altura
            lblTimer.Location = new Point((formWidth - lblTimer.Width) / 2, timerY);

            // Calcular posiciones para el grupo de controles del resultado
            int spacing = Math.Max(10, formHeight / 40); // Espaciado dinámico
            int resultGroupHeight = label2.Height + lblResultado.Height + spacing;
            int resultGroupY = Math.Max(lblTimer.Bottom + 30, (formHeight - resultGroupHeight) / 2);

            // Centrar label2 (APUESTA)
            label2.Location = new Point((formWidth - label2.Width) / 2, resultGroupY);

            // Posicionar label3 (S/.) y lblResultado en la misma línea, centrados
            int resultY = resultGroupY + label2.Height + spacing;
            int totalResultWidth = label3.Width + lblResultado.Width + 10; // 10px de espacio entre S/. y el número
            int resultStartX = Math.Max(10, (formWidth - totalResultWidth) / 2);
            
            label3.Location = new Point(resultStartX, resultY);
            lblResultado.Location = new Point(resultStartX + label3.Width + 10, resultY);
        }

        private void AdjustFontSizes()
        {
            if (originalFormSize.Width == 0 || originalFormSize.Height == 0) return;

            // Calcular factor de escala basado en el tamaño del form
            float scaleFactorWidth = (float)this.ClientSize.Width / originalFormSize.Width;
            float scaleFactorHeight = (float)this.ClientSize.Height / originalFormSize.Height;
            float scaleFactor = Math.Min(scaleFactorWidth, scaleFactorHeight);

            // Limitar el factor de escala para evitar fuentes demasiado pequeñas o grandes
            scaleFactor = Math.Max(0.3f, Math.Min(scaleFactor, 5.0f));

            try
            {
                // Ajustar fuente del timer con un mínimo y máximo
                float newTimerSize = Math.Max(20f, Math.Min(originalTimerFont.Size * scaleFactor, 150f));
                if (lblTimer.Font.Size != newTimerSize)
                {
                    lblTimer.Font = new Font(originalTimerFont.FontFamily, newTimerSize, originalTimerFont.Style);
                }

                // Ajustar fuente del resultado con un mínimo y máximo
                float newResultSize = Math.Max(12f, Math.Min(originalResultFont.Size * scaleFactor, 80f));
                if (lblResultado.Font.Size != newResultSize)
                {
                    lblResultado.Font = new Font(originalResultFont.FontFamily, newResultSize, originalResultFont.Style);
                    label3.Font = new Font(originalResultFont.FontFamily, newResultSize, originalResultFont.Style);
                }

                // Ajustar fuente de la etiqueta con un mínimo y máximo
                float newLabelSize = Math.Max(8f, Math.Min(originalLabelFont.Size * scaleFactor, 32f));
                if (label2.Font.Size != newLabelSize)
                {
                    label2.Font = new Font(originalLabelFont.FontFamily, newLabelSize, originalLabelFont.Style);
                }
            }
            catch (ArgumentException)
            {
                // En caso de error al crear la fuente, mantener la fuente actual
            }
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

        public void UpdateResult(int resultado)
        {
            lblResultado.Text = resultado.ToString();
            // Recentrar después de actualizar el texto
            this.BeginInvoke(new MethodInvoker(CenterControls));
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
