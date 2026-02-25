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
        private bool _isInitializing = false;

        private int currentProportionIndex = 0;
        private readonly int[][] timerProportions = new int[][]
        {
            new int[] {1, 1}, // 50% : 50% (default)
            new int[] {3, 2}, // 60% : 40% 
            new int[] {2, 3}, // 40% : 60%
            new int[] {3, 1}   // 75% : 25%
        };

        public FrmSecondary()
        {
            _isInitializing = true;
            InitializeComponent();
            InitializeTimer();
            InitializeScaling();
            SetupKeyboardShortcuts();
            _isInitializing = false;

            // Aplicar layout inicial
            ApplyResponsiveLayout();
        }

        private void SetupKeyboardShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += FrmSecondary_KeyDown;
        }

        private void FrmSecondary_KeyDown(object sender, KeyEventArgs e)
        {
            // F5: Refrescar layout
            if (e.KeyCode == Keys.F5)
            {
                RefreshLayout();
                e.Handled = true;
            }
            // Ctrl + R: Reset to original size
            else if (e.Control && e.KeyCode == Keys.R)
            {
                ResetToOriginalSize();
                e.Handled = true;
            }
            // Ctrl + Plus: Increase size
            else if (e.Control && (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus))
            {
                ApplyScaleFactor(1.1f);
                e.Handled = true;
            }
            // Ctrl + Minus: Decrease size
            else if (e.Control && (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus))
            {
                ApplyScaleFactor(0.9f);
                e.Handled = true;
            }
            // Ctrl + F: Force text to fit
            else if (e.Control && e.KeyCode == Keys.F)
            {
                EnsureTextFits();
                e.Handled = true;
            }
            // Ctrl + M: Maximize font size (Make text as large as possible)
            else if (e.Control && e.KeyCode == Keys.M)
            {
                MaximizeFontSize();
                e.Handled = true;
            }
            // Ctrl + D: Show debug info
            else if (e.Control && e.KeyCode == Keys.D)
            {
                string debugInfo = GetTextFitDebugInfo();
                MessageBox.Show(debugInfo, "Debug: Layout 50:50", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
            // Ctrl + T: Cycle proportions
            else if (e.Control && e.KeyCode == Keys.T)
            {
                CycleTimerProportions();
                e.Handled = true;
            }
            // Ctrl + E: Aplicar layout 50:50 (Equal)
            else if (e.Control && e.KeyCode == Keys.E)
            {
                ForzarLayout5050();
                e.Handled = true;
            }
            // F11: Toggle fullscreen
            else if (e.KeyCode == Keys.F11)
            {
                ToggleFullscreen();
                e.Handled = true;
            }
        }

        private void CycleTimerProportions()
        {
            currentProportionIndex = (currentProportionIndex + 1) % timerProportions.Length;
            int[] proportion = timerProportions[currentProportionIndex];
            
            SetTimerProportion(proportion[0], proportion[1]);
            
            // Mostrar mensaje con la nueva proporción
            float timerPercent = (proportion[0] * 100.0f) / (proportion[0] + proportion[1]);
            float resultPercent = (proportion[1] * 100.0f) / (proportion[0] + proportion[1]);
            
            MessageBox.Show($"Nueva proporción aplicada:\nTimer: {timerPercent:F1}%\nResultado: {resultPercent:F1}%", 
                          "Proporción Cambiada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToggleFullscreen()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }

            ApplyResponsiveLayout();
        }

        #region Inicialización del Sistema de Escalado

        private void InitializeScaling()
        {
            // Guardar el tamaño original del formulario
            _originalFormSize = this.Size;

            // Inicializar diccionarios para guardar valores originales
            _originalFonts = new Dictionary<Control, Font>();
            _originalBounds = new Dictionary<Control, Rectangle>();

            // Guardar propiedades originales de todos los controles
            SaveOriginalProperties();
        }

        private void SaveOriginalProperties()
        {
            SaveControlProperties(this);
        }

        private void SaveControlProperties(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Guardar fuente original si existe
                if (control.Font != null)
                {
                    _originalFonts[control] = new Font(control.Font.FontFamily, control.Font.Size, control.Font.Style);
                }

                // Guardar bounds originales
                _originalBounds[control] = control.Bounds;

                // Procesar controles hijos recursivamente
                if (control.HasChildren)
                {
                    SaveControlProperties(control);
                }
            }
        }

        #endregion

        #region Layout Responsivo Mejorado

        /// <summary>
        /// Aplica el layout responsivo completo con proporciones:
        /// - Pantalla dividida 50:50 - Timer izquierda : Resultado derecha
        /// - Ambos con el mismo tamaño de fuente grande
        /// </summary>
        private void ApplyResponsiveLayout()
        {
            if (_isInitializing || this.Width <= 0 || this.Height <= 0)
                return;

            try
            {
                this.SuspendLayout();

                // Hacer que pnSuperior ocupe toda la pantalla
                pnSuperior.Dock = DockStyle.Fill;

                // Distribuir paneles horizontalmente 50:50 (mitad y mitad)
                DistribuirHorizontal(pnSuperior, pnSuperiorIzquierda, 1, pnSuperiorDerecha, 1);

                // Ajustar fuentes de forma adaptativa y mejorada
                AjustarFuentesInteligente();

                // Centrar y posicionar controles
                PosicionarControles();

            }
            finally
            {
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// Distribuye verticalmente dos controles según las proporciones especificadas
        /// </summary>
        /// <param name="contenedor">Control contenedor</param>
        /// <param name="superior">Control superior</param>
        /// <param name="partesSuperior">Partes proporcionales para el control superior</param>
        /// <param name="inferior">Control inferior</param>
        /// <param name="partesInferior">Partes proporcionales para el control inferior</param>
        private void DistribuirVertical(Control contenedor, Control superior, int partesSuperior, Control inferior, int partesInferior)
        {
            if (contenedor?.ClientSize == null) return;

            int totalPartes = partesSuperior + partesInferior;
            int alturaSuperior = (contenedor.ClientSize.Height * partesSuperior) / totalPartes;
            int alturaInferior = contenedor.ClientSize.Height - alturaSuperior;

            superior?.SetBounds(0, 0, contenedor.ClientSize.Width, alturaSuperior);
            inferior?.SetBounds(0, alturaSuperior, contenedor.ClientSize.Width, alturaInferior);
        }

        /// <summary>
        /// Distribuye horizontalmente dos controles según las proporciones especificadas
        /// </summary>
        /// <param name="contenedor">Control contenedor</param>
        /// <param name="izquierdo">Control izquierdo</param>
        /// <param name="partesIzquierdo">Partes proporcionales para el control izquierdo</param>
        /// <param name="derecho">Control derecho</param>
        /// <param name="partesDerecho">Partes proporcionales para el control derecho</param>
        private void DistribuirHorizontal(Control contenedor, Control izquierdo, int partesIzquierdo, Control derecho, int partesDerecho)
        {
            if (contenedor?.ClientSize == null) return;

            int totalPartes = partesIzquierdo + partesDerecho;
            int anchoIzquierdo = (contenedor.ClientSize.Width * partesIzquierdo) / totalPartes;
            int anchoDerecho = contenedor.ClientSize.Width - anchoIzquierdo;

            izquierdo?.SetBounds(0, 0, anchoIzquierdo, contenedor.ClientSize.Height);
            derecho?.SetBounds(anchoIzquierdo, 0, anchoDerecho, contenedor.ClientSize.Height);
        }

        #endregion

        #region Sistema de Fuentes Inteligente

        private void AjustarFuentesInteligente()
        {
            // Calcular el tamaño de fuente máximo que funciona para AMBOS controles
            float tamanoFuenteUnificado = CalcularTamanoFuenteUnificado();

            // Aplicar el MISMO tamaño de fuente a ambos controles
            AplicarFuenteUnificada(lblTimer, tamanoFuenteUnificado);
            AplicarFuenteUnificada(lblResultado, tamanoFuenteUnificado);
        }

        private float CalcularTamanoFuenteUnificado()
        {
            if (pnSuperiorIzquierda == null || pnSuperiorDerecha == null || 
                string.IsNullOrEmpty(lblTimer?.Text) || string.IsNullOrEmpty(lblResultado?.Text))
                return 50f; // Tamaño por defecto más grande

            // Calcular el área disponible para cada panel con márgenes mínimos
            Size areaTimer = new Size(
                Math.Max(50, pnSuperiorIzquierda.ClientSize.Width - 20), // Solo 20px de margen total
                Math.Max(50, pnSuperiorIzquierda.ClientSize.Height - 20)
            );

            Size areaResultado = new Size(
                Math.Max(50, pnSuperiorDerecha.ClientSize.Width - 20), // Solo 20px de margen total
                Math.Max(50, pnSuperiorDerecha.ClientSize.Height - 20)
            );

            // Calcular el tamaño máximo de fuente que funciona para cada texto en SU PROPIA área
            float tamanoMaxTimer = CalcularTamanoMaximoPara(lblTimer.Text, areaTimer);
            float tamanoMaxResultado = CalcularTamanoMaximoPara(lblResultado.Text, areaResultado);

            // Usar el menor de los dos para garantizar que ambos quepan
            float tamanoUnificado = Math.Min(tamanoMaxTimer, tamanoMaxResultado);

            // Aplicar límites mínimos y máximos más generosos
            return Math.Max(50f, Math.Min(800f, tamanoUnificado));
        }

        private float CalcularTamanoMaximoPara(string texto, Size areaDisponible)
        {
            if (string.IsNullOrEmpty(texto))
                return 50f;

            try
            {
                using (Graphics g = this.CreateGraphics())
                {
                    // Empezar con un tamaño mucho más grande y escalar según el área disponible
                    float tamanoInicial = Math.Min(areaDisponible.Width, areaDisponible.Height) * 0.8f; // 80% del área menor
                    float tamanoActual = Math.Max(50f, tamanoInicial);
                    Font fuentePrueba = null;

                    try
                    {
                        fuentePrueba = new Font("Digital-7", tamanoActual, FontStyle.Bold);
                    }
                    catch
                    {
                        // Si Digital-7 falla, usar fuente por defecto
                        fuentePrueba = new Font("Microsoft Sans Serif", tamanoActual, FontStyle.Bold);
                    }

                    while (tamanoActual > 30f)
                    {
                        try
                        {
                            // Medir el texto sin limitar el ancho para evitar saltos de línea
                            SizeF tamanoTexto = g.MeasureString(texto, fuentePrueba);

                            // Verificar si cabe completamente en el área disponible
                            if (tamanoTexto.Width <= areaDisponible.Width && 
                                tamanoTexto.Height <= areaDisponible.Height)
                            {
                                fuentePrueba?.Dispose();
                                return tamanoActual;
                            }

                            fuentePrueba?.Dispose();
                            tamanoActual *= 0.95f; // Reducir más gradualmente
                            
                            try
                            {
                                fuentePrueba = new Font("Digital-7", tamanoActual, FontStyle.Bold);
                            }
                            catch
                            {
                                fuentePrueba = new Font("Microsoft Sans Serif", tamanoActual, FontStyle.Bold);
                            }
                        }
                        catch
                        {
                            tamanoActual *= 0.9f;
                            try
                            {
                                fuentePrueba?.Dispose();
                                fuentePrueba = new Font("Digital-7", tamanoActual, FontStyle.Bold);
                            }
                            catch
                            {
                                try
                                {
                                    fuentePrueba = new Font("Microsoft Sans Serif", tamanoActual, FontStyle.Bold);
                                }
                                catch
                                {
                                    return 50f;
                                }
                            }
                        }
                    }

                    fuentePrueba?.Dispose();
                    return Math.Max(30f, tamanoActual); // Tamaño mínimo más grande
                }
            }
            catch
            {
                return 50f;
            }
        }

        private void AplicarFuenteUnificada(Label label, float tamano)
        {
            if (label == null) return;

            try
            {
                // Crear la nueva fuente con el tamaño unificado
                Font nuevaFuente = new Font("Digital-7", tamano, FontStyle.Bold);
                
                // Liberar la fuente anterior si existe
                label.Font?.Dispose();
                
                // Aplicar la nueva fuente
                label.Font = nuevaFuente;
                label.AutoSize = false;

                // Asegurar que el texto esté centrado y no tenga saltos de línea
                label.TextAlign = ContentAlignment.MiddleCenter;
                
            }
            catch (Exception ex)
            {
                // Fallback seguro
                try
                {
                    label.Font = new Font("Microsoft Sans Serif", Math.Max(24f, tamano), FontStyle.Bold);
                    label.AutoSize = false;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine($"Error aplicando fuente unificada: {ex.Message}");
                }
            }
        }

        #endregion

        #region Posicionamiento y Centrado Avanzado

        private void PosicionarControles()
        {
            // Asegurar que los labels ocupen todo el espacio disponible de sus contenedores
            ConfigurarTamanos();

            // Centrar ambos controles perfectamente en el centro de sus paneles
            CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
            CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);
        }

        private void ConfigurarTamanos()
        {
            // Configurar lblTimer para que ocupe casi todo el espacio de su contenedor
            if (lblTimer != null && pnSuperiorIzquierda != null)
            {
                lblTimer.Size = new Size(
                    pnSuperiorIzquierda.ClientSize.Width - 10, // Solo 5px margen a cada lado
                    pnSuperiorIzquierda.ClientSize.Height - 10
                );
            }

            // Configurar lblResultado para que ocupe casi todo el espacio de su contenedor
            if (lblResultado != null && pnSuperiorDerecha != null)
            {
                lblResultado.Size = new Size(
                    pnSuperiorDerecha.ClientSize.Width - 10, // Solo 5px margen a cada lado
                    pnSuperiorDerecha.ClientSize.Height - 10
                );
            }
        }

        private void CentrarEnContenedor(Control control, Control contenedor, ContentAlignment alignment = ContentAlignment.MiddleCenter)
        {
            if (control == null || contenedor == null) return;

            int centroX = Math.Max(0, (contenedor.ClientSize.Width - control.Width) / 2);
            int centroY = Math.Max(0, (contenedor.ClientSize.Height - control.Height) / 2);

            control.Location = new Point(centroX, centroY);
            
            // Asegurar que el texto esté centrado dentro del control
            if (control is Label label)
            {
                label.TextAlign = alignment;
            }
        }

        private void CentrarHorizontalmente(Control control, Control contenedor)
        {
            if (control == null || contenedor == null) return;

            int centroX = Math.Max(0, (contenedor.ClientSize.Width - control.Width) / 2);
            control.Left = centroX;
            
            // Asegurar que el texto esté centrado dentro del control
            if (control is Label label)
            {
                label.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        #endregion

        #region Eventos del Sistema

        private void FrmSecondary_Resize(object sender, EventArgs e)
        {
            if (!_isInitializing)
            {
                ApplyResponsiveLayout();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (!_isInitializing && this.WindowState != FormWindowState.Minimized)
            {
                ApplyResponsiveLayout();
            }
        }

        private void FrmSecondary_Load(object sender, EventArgs e)
        {
            // Forzar un recálculo completo del layout al cargar
            this.WindowState = FormWindowState.Normal; // Asegurar estado normal primero
            ApplyResponsiveLayout();
            
            // Dar un pequeño retraso para que el formulario se estabilice y recalcular
            Timer delayTimer = new Timer();
            delayTimer.Interval = 100;
            delayTimer.Tick += (s, ev) =>
            {
                delayTimer.Stop();
                delayTimer.Dispose();
                AjustarFuentesInteligente();
                CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
                CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);
            };
            delayTimer.Start();
        }

        #endregion

        #region Métodos Públicos para Control Externo

        /// <summary>
        /// Fuerza la aplicación del layout responsivo
        /// </summary>
        public void RefreshLayout()
        {
            ApplyResponsiveLayout();
        }

        /// <summary>
        /// Resetea el formulario a su tamaño original y aplica el layout
        /// </summary>
        public void ResetToOriginalSize()
        {
            if (_originalFormSize.Width > 0 && _originalFormSize.Height > 0)
            {
                this.Size = _originalFormSize;
                this.WindowState = FormWindowState.Normal;
                ApplyResponsiveLayout();
            }
        }

        /// <summary>
        /// Aplica un factor de escala específico al formulario
        /// </summary>
        public void ApplyScaleFactor(float factor)
        {
            Size currentSize = this.Size;
            Size newSize = new Size(
                (int)(currentSize.Width * factor),
                (int)(currentSize.Height * factor)
            );
            
            this.Size = newSize;
            ApplyResponsiveLayout();
        }

        /// <summary>
        /// Cambia la proporción del timer vs resultado en la pantalla
        /// </summary>
        /// <param name="timerParts">Partes para el timer (por defecto 1 = 50%)</param>
        /// <param name="resultParts">Partes para resultado (por defecto 1 = 50%)</param>
        public void SetTimerProportion(int timerParts = 1, int resultParts = 1)
        {
            if (_isInitializing || this.Width <= 0 || this.Height <= 0)
                return;

            try
            {
                this.SuspendLayout();

                // Redistribuir con las nuevas proporciones
                DistribuirHorizontal(pnSuperior, pnSuperiorIzquierda, timerParts, pnSuperiorDerecha, resultParts);

                // Aplicar fuentes unificadas
                AjustarFuentesInteligente();

                // Reposicionar controles con centrado perfecto
                ConfigurarTamanos();
                CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
                CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);

            }
            finally
            {
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// Fuerza el layout 50:50 con fuentes grandes iguales
        /// </summary>
        public void ForzarLayout5050()
        {
            SetTimerProportion(1, 1);
            MessageBox.Show("Layout 50:50 aplicado\nTimer (Izquierda) : Resultado (Derecha)", "Layout Aplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Fuerza el reajuste de todas las fuentes para evitar recortes
        /// </summary>
        public void EnsureTextFits()
        {
            // Usar el sistema unificado para garantizar el mismo tamaño
            AjustarFuentesInteligente();
            
            // Recentrar ambos controles
            CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
            CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);
        }

        /// <summary>
        /// Obtiene información de depuración sobre el ajuste de texto
        /// </summary>
        public string GetTextFitDebugInfo()
        {
            var info = new System.Text.StringBuilder();
            info.AppendLine("=== DEBUG: Layout 50:50 ===");
            
            // Información del sistema
            Screen currentScreen = Screen.FromControl(this);
            
            info.AppendLine($"Pantalla: {currentScreen.WorkingArea.Width}x{currentScreen.WorkingArea.Height}");
            info.AppendLine($"Formulario: {this.Width}x{this.Height}");
            info.AppendLine("Layout: Timer (Izquierda) : Resultado (Derecha) = 50% : 50%");
            info.AppendLine();
            
            info.AppendLine($"Distribución actual - Timer: {pnSuperiorIzquierda?.Width}px, Resultado: {pnSuperiorDerecha?.Width}px");
            if (pnSuperiorIzquierda?.Width > 0 && pnSuperiorDerecha?.Width > 0)
            {
                float totalWidth = pnSuperiorIzquierda.Width + pnSuperiorDerecha.Width;
                info.AppendLine($"Ratio real: {(pnSuperiorIzquierda.Width * 100.0 / totalWidth):F1}% : {(pnSuperiorDerecha.Width * 100.0 / totalWidth):F1}%");
                
                // Información de áreas disponibles
                Size areaTimer = new Size(
                    Math.Max(50, pnSuperiorIzquierda.ClientSize.Width - 20),
                    Math.Max(50, pnSuperiorIzquierda.ClientSize.Height - 20)
                );
                Size areaResultado = new Size(
                    Math.Max(50, pnSuperiorDerecha.ClientSize.Width - 20),
                    Math.Max(50, pnSuperiorDerecha.ClientSize.Height - 20)
                );
                
                info.AppendLine($"Área Timer: {areaTimer.Width}x{areaTimer.Height}");
                info.AppendLine($"Área Resultado: {areaResultado.Width}x{areaResultado.Height}");
                
                float tamanoCalculadoTimer = CalcularTamanoMaximoPara(lblTimer?.Text ?? "00", areaTimer);
                float tamanoCalculadoResultado = CalcularTamanoMaximoPara(lblResultado?.Text ?? "100", areaResultado);
                
                info.AppendLine($"Tamaño calculado Timer: {tamanoCalculadoTimer:F1}pt");
                info.AppendLine($"Tamaño calculado Resultado: {tamanoCalculadoResultado:F1}pt");
                info.AppendLine($"Tamaño final unificado: {Math.Min(tamanoCalculadoTimer, tamanoCalculadoResultado):F1}pt");
            }
            info.AppendLine();
            
            AddControlDebugInfo(info, "Timer", lblTimer, pnSuperiorIzquierda);
            AddControlDebugInfo(info, "Resultado", lblResultado, pnSuperiorDerecha);
            
            return info.ToString();
        }

        private void AddControlDebugInfo(System.Text.StringBuilder info, string nombre, Control control, Control contenedor)
        {
            if (control?.Font == null || contenedor == null) return;

            try
            {
                using (Graphics g = control.CreateGraphics())
                {
                    SizeF tamanoTexto = g.MeasureString(control.Text, control.Font);
                    
                    info.AppendLine($"{nombre}:");
                    info.AppendLine($"  Texto: '{control.Text}'");
                    info.AppendLine($"  Fuente: {control.Font.Size:F1}pt");
                    info.AppendLine($"  Tamaño Control: {control.Width}x{control.Height}");
                    info.AppendLine($"  Tamaño Contenedor: {contenedor.Width}x{contenedor.Height}");
                    info.AppendLine($"  Tamaño Texto: {tamanoTexto.Width:F1}x{tamanoTexto.Height:F1}");
                    info.AppendLine($"  Cabe: {(tamanoTexto.Width <= control.Width && tamanoTexto.Height <= control.Height ? "SÍ" : "NO")}");
                    info.AppendLine();
                }
            }
            catch (Exception ex)
            {
                info.AppendLine($"{nombre}: Error - {ex.Message}");
                info.AppendLine();
            }
        }

        /// <summary>
        /// Maximiza el tamaño de fuente para ocupar toda la pantalla disponible
        /// </summary>
        public void MaximizeFontSize()
        {
            // Forzar recalcular con configuraciones más agresivas
            ConfigurarTamanos();
            AjustarFuentesInteligente();
            CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
            CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);
            
            MessageBox.Show("Fuente maximizada para ocupar toda la pantalla disponible", "Fuente Maximizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Métodos Originales del Temporizador

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
            string newText = currentCount.ToString("00");
            
            // Solo recalcular fuentes si el texto cambió de longitud o formato significativamente
            bool shouldRecalculateFont = lblTimer.Text.Length != newText.Length ||
                                       (currentCount <= 9 && lblTimer.Text.Length >= 2) ||
                                       (currentCount >= 10 && lblTimer.Text.Length < 2);
            
            lblTimer.Text = newText;
            
            if (shouldRecalculateFont)
            {
                AjustarFuentesInteligente();
                CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
                CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);
            }

            if (currentCount <= 0)
            {
                countdownTimer.Stop();
                lblTimer.Text = "00";
                
                // Recalcular fuentes cuando llega a 00
                AjustarFuentesInteligente();
                CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
                CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);

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
            
            // Recalcular fuentes unificadas cuando el texto cambia
            AjustarFuentesInteligente();
            
            // Recentrar ambos controles
            CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
            CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);
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

            // Recalcular fuentes unificadas cuando el texto cambia
            AjustarFuentesInteligente();

            // Reposicionar ambos controles para mantener centrado perfecto
            CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleCenter);
            CentrarEnContenedor(lblResultado, pnSuperiorDerecha, ContentAlignment.MiddleCenter);
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

        #endregion
    }
}
