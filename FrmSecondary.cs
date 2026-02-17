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
        private bool isPaused;

        // Variables para el escalado de fuentes y controles
        private Size _originalFormSize;
        private Dictionary<Control, Font> _originalFonts;
        private Dictionary<Control, Rectangle> _originalBounds;

        public FrmSecondary()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeScaling();
            SetupKeyboardShortcuts();
            
            // Aplicar tamaños de fuente duplicados
            SetSpecificFontSizes();
        }

        private void InitializeScaling()
        {
            // Guardar el tamaño original del formulario
            _originalFormSize = this.Size;

            // Inicializar diccionarios para guardar valores originales
            _originalFonts = new Dictionary<Control, Font>();
            _originalBounds = new Dictionary<Control, Rectangle>();

            // Guardar fuentes y posiciones originales de todos los controles
            SaveOriginalControlProperties(this);

            // Suscribirse al evento de redimensionamiento
            this.Resize += FrmSecondary_ResizeHandler;
        }

        private void SaveOriginalControlProperties(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Guardar fuente original
                if (control.Font != null)
                {
                    _originalFonts[control] = new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style);
                }

                // Guardar bounds originales
                _originalBounds[control] = control.Bounds;

                // Procesar controles hijos recursivamente
                if (control.HasChildren)
                {
                    SaveOriginalControlProperties(control);
                }
            }
        }

        private void FrmSecondary_ResizeHandler(object sender, EventArgs e)
        {
            ScaleControlsAndFonts();
        }

        private void ScaleControlsAndFonts()
        {
            if (_originalFormSize.Width == 0 || _originalFormSize.Height == 0)
                return;

            // Calcular factores de escala
            float scaleFactorX = (float)this.Width / _originalFormSize.Width;
            float scaleFactorY = (float)this.Height / _originalFormSize.Height;

            // Usar el menor factor para mantener proporciones uniformes
            float uniformScaleFactor = Math.Min(scaleFactorX, scaleFactorY);

            // Aplicar escalado a todos los controles
            ScaleControls(this, uniformScaleFactor, scaleFactorX, scaleFactorY);
        }

        private void ScaleControls(Control parent, float fontScaleFactor, float scaleFactorX, float scaleFactorY)
        {
            foreach (Control control in parent.Controls)
            {
                // Escalar fuente manteniendo proporciones
                if (_originalFonts.ContainsKey(control))
                {
                    Font originalFont = _originalFonts[control];
                    float newSize = Math.Max(8f, originalFont.Size * fontScaleFactor); // Tamaño mínimo de 8pt
                    
                    try
                    {
                        Font newFont = new Font(originalFont.FontFamily, newSize, originalFont.Style);
                        control.Font = newFont;
                    }
                    catch
                    {
                        // En caso de error con la fuente, usar fuente por defecto
                        try
                        {
                            control.Font = new Font("Microsoft Sans Serif", newSize, originalFont.Style);
                        }
                        catch
                        {
                            // Si también falla, usar Arial como respaldo final
                            control.Font = new Font("Arial", newSize, FontStyle.Regular);
                        }
                    }
                }

                // Escalar posición y tamaño del control
                if (_originalBounds.ContainsKey(control))
                {
                    Rectangle originalBounds = _originalBounds[control];
                    
                    int newX = (int)(originalBounds.X * scaleFactorX);
                    int newY = (int)(originalBounds.Y * scaleFactorY);
                    int newWidth = (int)(originalBounds.Width * scaleFactorX);
                    int newHeight = (int)(originalBounds.Height * scaleFactorY);

                    control.SetBounds(newX, newY, newWidth, newHeight);
                }

                // Procesar controles hijos recursivamente
                if (control.HasChildren)
                {
                    ScaleControls(control, fontScaleFactor, scaleFactorX, scaleFactorY);
                }
            }
        }

        private void SetupKeyboardShortcuts()
        {
            // Habilitar preview de teclas para atajos de teclado
            this.KeyPreview = true;
            this.KeyDown += FrmSecondary_KeyDown;
        }

        private void FrmSecondary_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl + Plus o Ctrl + Add: Aumentar tamaño
            if (e.Control && (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus))
            {
                ApplyCustomScale(1.1f);
                e.Handled = true;
            }
            // Ctrl + Minus o Ctrl + Subtract: Disminuir tamaño
            else if (e.Control && (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus))
            {
                ApplyCustomScale(0.9f);
                e.Handled = true;
            }
            // Ctrl + 0: Resetear a tamaño original
            else if (e.Control && e.KeyCode == Keys.D0)
            {
                ResetToOriginalSize();
                e.Handled = true;
            }
            // Ctrl + D: Duplicar tamaño de fuente
            else if (e.Control && e.KeyCode == Keys.D)
            {
                DoubleFontSize();
                e.Handled = true;
            }
            // Ctrl + R: Restaurar fuentes duplicadas específicas
            else if (e.Control && e.KeyCode == Keys.R)
            {
                SetSpecificFontSizes();
                e.Handled = true;
            }
            // F11: Alternar pantalla completa
            else if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
                e.Handled = true;
            }
            // Escape: Salir de pantalla completa
            else if (e.KeyCode == Keys.Escape && this.FormBorderStyle == FormBorderStyle.None)
            {
                ExitFullscreen();
                e.Handled = true;
            }
        }

        private void ToggleFullscreen()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                EnterFullscreen();
            }
            else
            {
                ExitFullscreen();
            }
        }

        private void EnterFullscreen()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
        }

        private void ExitFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.TopMost = false;
        }

        // Método para resetear a tamaño original
        public void ResetToOriginalSize()
        {
            this.Size = _originalFormSize;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.TopMost = false;
        }

        // Método mejorado para aplicar escala personalizada
        public void ApplyCustomScale(float scaleFactor)
        {
            Size currentSize = this.Size;
            Size newSize = new Size(
                Math.Max(this.MinimumSize.Width, (int)(currentSize.Width * scaleFactor)),
                Math.Max(this.MinimumSize.Height, (int)(currentSize.Height * scaleFactor))
            );
            
            // Verificar que no exceda el tamaño de la pantalla
            Screen currentScreen = Screen.FromControl(this);
            Rectangle workingArea = currentScreen.WorkingArea;
            
            if (newSize.Width > workingArea.Width * 0.95)
                newSize.Width = (int)(workingArea.Width * 0.95);
            if (newSize.Height > workingArea.Height * 0.95)
                newSize.Height = (int)(workingArea.Height * 0.95);
                
            this.Size = newSize;
            
            // Centrar si es necesario
            if (this.Location.X + this.Width > workingArea.Width || 
                this.Location.Y + this.Height > workingArea.Height)
            {
                this.CenterToScreen();
            }
        }

        // Método para ajustar el formulario a un porcentaje del tamaño de la pantalla
        public void FitToScreenPercentage(float percentage)
        {
            Screen currentScreen = Screen.FromControl(this);
            Rectangle workingArea = currentScreen.WorkingArea;
            
            int newWidth = (int)(workingArea.Width * percentage);
            int newHeight = (int)(workingArea.Height * percentage);
            
            // Mantener proporciones del formulario original
            float aspectRatio = (float)_originalFormSize.Width / _originalFormSize.Height;
            if (newWidth / aspectRatio > newHeight)
            {
                newWidth = (int)(newHeight * aspectRatio);
            }
            else
            {
                newHeight = (int)(newWidth / aspectRatio);
            }
            
            this.Size = new Size(
                Math.Max(this.MinimumSize.Width, newWidth),
                Math.Max(this.MinimumSize.Height, newHeight)
            );
            
            this.CenterToScreen();
        }

        // Método para duplicar el tamaño del texto
        public void DoubleFontSize()
        {
            ApplyFontScaleToAllControls(2.0f);
        }

        // Método para aplicar un factor de escala específico a todas las fuentes
        public void ApplyFontScaleToAllControls(float scaleFactor)
        {
            foreach (Control control in this.Controls)
            {
                if (control.Font != null)
                {
                    try
                    {
                        float newSize = Math.Max(8f, control.Font.Size * scaleFactor);
                        Font newFont = new Font(control.Font.FontFamily, newSize, control.Font.Style);
                        control.Font = newFont;
                    }
                    catch
                    {
                        // En caso de error, usar fuente por defecto
                        try
                        {
                            float newSize = Math.Max(8f, control.Font.Size * scaleFactor);
                            control.Font = new Font("Microsoft Sans Serif", newSize, control.Font.Style);
                        }
                        catch
                        {
                            // Si también falla, usar Arial como respaldo final
                            control.Font = new Font("Arial", 24f, FontStyle.Regular);
                        }
                    }
                }
            }
        }

        // Método para resetear fuentes a tamaños específicos
        public void SetSpecificFontSizes()
        {
            try
            {
                // Aplicar tamaños específicos duplicados
                lblTimer.Font = new Font("DS-Digital", 144F, FontStyle.Bold | FontStyle.Italic);
                lblResultado.Font = new Font("Digital-7", 144.62608F, FontStyle.Bold);
                label1.Font = new Font("Digital-7 Italic", 72.62608F, FontStyle.Bold | FontStyle.Italic);
                label2.Font = new Font("Digital-7 Italic", 32.55652F, FontStyle.Bold | FontStyle.Italic);
            }
            catch
            {
                // Fallback con fuentes estándar
                lblTimer.Font = new Font("Arial", 144F, FontStyle.Bold);
                lblResultado.Font = new Font("Arial", 144F, FontStyle.Bold);
                label1.Font = new Font("Arial", 72F, FontStyle.Bold);
                label2.Font = new Font("Arial", 32F, FontStyle.Bold);
            }
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
                string rutaArchivo = Path.Combine(rutaBase, alerta + ".mp3");

                // Mostrar desde dónde se ejecuta y qué archivo intenta abrir
                //MessageBox.Show($"Ruta de ejecución:\n{rutaBase}\n\nArchivo buscado:\n{rutaArchivo}");

                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("No se encontró el archivo MP3.");
                    return;
                }

                _ = ReproducirMp3(alerta);
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
            isPaused = false;
            countdownTimer.Start();
        }

        public void StopCountdown()
        {
            countdownTimer.Stop();
            isPaused = false;
            currentCount = 0;
            lblTimer.Text = "00";
        }

        public void PauseCountdown()
        {
            if (countdownTimer.Enabled)
            {
                countdownTimer.Stop();
                isPaused = true;
            }
        }

        public void ResumeCountdown()
        {
            if (isPaused && currentCount > 0)
            {
                countdownTimer.Start();
                isPaused = false;
            }
        }

        public bool IsPaused
        {
            get { return isPaused; }
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
                    lblResultado.Text = "S/ " + rounded.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
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
