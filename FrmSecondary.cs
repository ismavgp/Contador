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
            new int[] {8, 2}, // 80% : 20% (default)
            new int[] {9, 1}, // 90% : 10%
            new int[] {17, 3}, // 85% : 15%
            new int[] {7, 3}   // 70% : 30%
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
            // Ctrl + D: Show debug info
            else if (e.Control && e.KeyCode == Keys.D)
            {
                string debugInfo = GetTextFitDebugInfo();
                MessageBox.Show(debugInfo, "Debug: Ajuste de Texto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
            // Ctrl + T: Cycle timer proportions (80%, 85%, 90%, 70%, back to 80%)
            else if (e.Control && e.KeyCode == Keys.T)
            {
                CycleTimerProportions();
                e.Handled = true;
            }
            // Ctrl + S: Forzar layout pantalla pequeña (Small)
            else if (e.Control && e.KeyCode == Keys.S)
            {
                ForzarLayoutPantallaPequena();
                MessageBox.Show("Layout forzado para pantalla pequeña (70%:30%)", "Layout Cambiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
            // Ctrl + L: Forzar layout pantalla grande (Large)
            else if (e.Control && e.KeyCode == Keys.L)
            {
                ForzarLayoutPantallaGrande();
                MessageBox.Show("Layout forzado para pantalla grande (80%:20%)", "Layout Cambiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
            // Ctrl + A: Aplicar layout automático (Auto)
            else if (e.Control && e.KeyCode == Keys.A)
            {
                RefreshLayout(); // Esto aplicará la detección automática
                bool isPantallaPequena = DetectarPantallaPequena();
                string mensaje = isPantallaPequena ? 
                    "Layout automático aplicado:\nPantalla pequeña detectada (70%:30%)" :
                    "Layout automático aplicado:\nPantalla grande detectada (80%:20%)";
                MessageBox.Show(mensaje, "Layout Automático", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            float segPercent = (proportion[1] * 100.0f) / (proportion[0] + proportion[1]);
            
            string layoutType = DetectarPantallaPequena() ? "Pantalla Pequeña (Auto)" : "Pantalla Grande (Auto)";
            
            MessageBox.Show($"Proporción manual aplicada:\nTimer: {timerPercent:F1}%\nSEG: {segPercent:F1}%\n\nDetección automática: {layoutType}", 
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
        /// - Formulario: Superior 66% : Inferior 33%
        /// - Superior: Automático según pantalla (70%:30% en pequeñas, 80%:20% en grandes)
        /// - Inferior: Arriba 20% : Abajo 80%
        /// </summary>
        private void ApplyResponsiveLayout()
        {
            if (_isInitializing || this.Width <= 0 || this.Height <= 0)
                return;

            try
            {
                this.SuspendLayout();

                // 1. Distribuir paneles principales (4:2 vertical = 66.6% : 33.3%)
                DistribuirVertical(this, pnSuperior, 4, pnInferior, 2);

                //// 2. Distribuir paneles superiores con proporción inteligente según tamaño de pantalla
                //bool isPantallaPequena = DetectarPantallaPequena();
                //if (isPantallaPequena)
                //{
                //    // Pantallas pequeñas: 70% : 30% (más espacio para SEG)
                //    DistribuirHorizontal(pnSuperior, pnSuperiorIzquierda, 7, pnSuperiorDerecha, 3);
                //}
                //else
                //{
                //    // Pantallas grandes: 80% : 20% (timer más prominente)
                //    DistribuirHorizontal(pnSuperior, pnSuperiorIzquierda, 8, pnSuperiorDerecha, 2);
                //}

                DistribuirHorizontal(pnSuperior, pnSuperiorIzquierda, 5, pnSuperiorDerecha, 4);

                // 3. Distribuir paneles inferiores (1:4 vertical = 20% : 80%)
                DistribuirVertical(pnInferior, pnInferiorArriba, 1, pnInferiorAbajo, 4);

                // 4. Ajustar fuentes de forma adaptativa y mejorada
                AjustarFuentesInteligente();

                // 5. Centrar y posicionar controles
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
            // Calcular factor base para el escalado
            float factorEscala = CalcularFactorEscala();

            // Detectar pantallas pequeñas para ajustar factores de fuente
            bool isPantallaPequena = DetectarPantallaPequena();

            // Ajustar cada control con factores mejorados para pantallas pequeñas
            float timerFactor = isPantallaPequena ? 0.20f : 0.22f; // Reducir timer en pantallas pequeñas
            float segFactor = isPantallaPequena ? 0.08f : 0.06f;   // Aumentar SEG en pantallas pequeñas

            AjustarFuenteConAjusteAutomatico(lblTimer, pnSuperiorIzquierda, factorEscala * timerFactor, FontStyle.Bold, 20f, 400f);
            AjustarFuenteConAjusteAutomatico(label1, pnSuperiorDerecha, factorEscala * segFactor, FontStyle.Bold, 10f, 120f);
            AjustarFuenteConAjusteAutomatico(label3, pnInferiorArriba, factorEscala * 0.06f, FontStyle.Bold, 10f, 80f);
            AjustarFuenteConAjusteAutomatico(lblResultado, pnInferiorAbajo, factorEscala * 0.15f, FontStyle.Bold, 16f, 300f);
        }

        private bool DetectarPantallaPequena()
        {
            // Detectar si estamos en una pantalla pequeña (laptop 14" típicamente tiene ~1366x768 o 1920x1080)
            Screen currentScreen = Screen.FromControl(this);
            int screenWidth = currentScreen.WorkingArea.Width;
            int screenHeight = currentScreen.WorkingArea.Height;
            
            // Considerar pantalla pequeña si:
            // - Ancho menor a 1400px, o
            // - Formulario actual menor a 900px de ancho, o  
            // - Área de SEG menor a 120px
            bool isScreenSmall = screenWidth < 1400;
            bool isFormSmall = this.Width < 900;
            bool isSegAreaTooSmall = pnSuperiorDerecha != null && pnSuperiorDerecha.Width < 120;
            
            return isScreenSmall || isFormSmall || isSegAreaTooSmall;
        }

        private float CalcularFactorEscala()
        {
            if (_originalFormSize.Width <= 0 || _originalFormSize.Height <= 0)
                return 1.0f;

            // Usar el menor factor entre ancho y alto para mantener proporciones
            float scaleX = (float)this.ClientSize.Width / _originalFormSize.Width;
            float scaleY = (float)this.ClientSize.Height / _originalFormSize.Height;

            return Math.Min(scaleX, scaleY);
        }

        private void AjustarFuenteConAjusteAutomatico(Control control, Control contenedor, float factor, FontStyle estilo = FontStyle.Regular, float minSize = 8f, float maxSize = 200f)
        {
            if (control?.Font == null || contenedor == null) return;

            try
            {
                // Calcular nuevo tamaño basado en el tamaño del contenedor
                float baseSize = Math.Min(this.ClientSize.Width, this.ClientSize.Height);
                float nuevoTamano = baseSize * factor;

                // Aplicar límites iniciales
                nuevoTamano = Math.Max(minSize, Math.Min(maxSize, nuevoTamano));

                // Intentar usar la fuente original si es posible
                string fontFamily = "Microsoft Sans Serif";
                if (_originalFonts.ContainsKey(control))
                {
                    fontFamily = _originalFonts[control].FontFamily.Name;
                }

                // Crear fuente inicial
                Font nuevaFuente = new Font(fontFamily, nuevoTamano, estilo);
                
                // Ajustar el tamaño de la fuente para que el texto quepa en el contenedor
                nuevaFuente = AjustarTamanoParaQueQuepa(control, contenedor, nuevaFuente, minSize);
                
                control.Font = nuevaFuente;
                
                // Desactivar AutoSize para evitar problemas de recorte
                if (control is Label label)
                {
                    label.AutoSize = false;
                }

            }
            catch (Exception ex)
            {
                // Fallback a fuente segura
                try
                {
                    control.Font = new Font("Microsoft Sans Serif", Math.Max(minSize, 12f), estilo);
                    if (control is Label label)
                    {
                        label.AutoSize = false;
                    }
                }
                catch
                {
                    // Último recurso
                    System.Diagnostics.Debug.WriteLine($"Error ajustando fuente: {ex.Message}");
                }
            }
        }

        private Font AjustarTamanoParaQueQuepa(Control control, Control contenedor, Font fuenteInicial, float minSize)
        {
            if (string.IsNullOrEmpty(control.Text) || contenedor.Width <= 0 || contenedor.Height <= 0)
                return fuenteInicial;

            Font fuenteActual = fuenteInicial;
            
            using (Graphics g = control.CreateGraphics())
            {
                // Calcular el área disponible (con márgenes más agresivos para textos cortos)
                int margen = control.Text.Length <= 3 ? 2 : 5; // Margen menor para textos cortos como "SEG"
                Size areaDisponible = new Size(
                    Math.Max(1, contenedor.Width - (margen * 2)),
                    Math.Max(1, contenedor.Height - (margen * 2))
                );

                // Medir el tamaño del texto con la fuente actual
                SizeF tamanoTexto = g.MeasureString(control.Text, fuenteActual);

                // Para el control SEG, ser más agresivo en el ajuste
                bool isSegControl = control.Text == "SEG";
                float factorReduccion = isSegControl ? 0.85f : 0.9f; // Reducir más rápido para SEG

                // Reducir la fuente hasta que quepa
                int intentos = 0;
                int maxIntentos = 50; // Evitar bucles infinitos
                
                while ((tamanoTexto.Width > areaDisponible.Width || tamanoTexto.Height > areaDisponible.Height) && 
                       fuenteActual.Size > minSize && intentos < maxIntentos)
                {
                    float nuevoTamano = Math.Max(minSize, fuenteActual.Size * factorReduccion);
                    
                    Font nuevaFuente = new Font(fuenteActual.FontFamily, nuevoTamano, fuenteActual.Style);
                    fuenteActual.Dispose();
                    fuenteActual = nuevaFuente;
                    
                    tamanoTexto = g.MeasureString(control.Text, fuenteActual);
                    intentos++;
                }

                // Si aún no cabe, intentar estrategias adicionales
                if (tamanoTexto.Width > areaDisponible.Width && fuenteActual.Size <= minSize)
                {
                    // Estrategia 1: Intentar con Arial Narrow
                    fuenteActual = TentarFuenteCondensada(control, areaDisponible, fuenteActual, g);
                    
                    // Estrategia 2: Si sigue sin caber, intentar con Arial Black (más compacta para números)
                    if (control.Text.All(c => char.IsDigit(c) || char.IsWhiteSpace(c)))
                    {
                        tamanoTexto = g.MeasureString(control.Text, fuenteActual);
                        if (tamanoTexto.Width > areaDisponible.Width)
                        {
                            fuenteActual = TentarFuenteCompacta(control, areaDisponible, fuenteActual, g);
                        }
                    }
                }

                // Última validación: asegurar que al menos sea legible
                if (fuenteActual.Size < 8f && isSegControl)
                {
                    // Para SEG, prefiere usar tamaño mínimo legible aunque se salga un poco
                    Font nuevaFuente = new Font(fuenteActual.FontFamily, 8f, fuenteActual.Style);
                    fuenteActual.Dispose();
                    fuenteActual = nuevaFuente;
                }
            }

            return fuenteActual;
        }

        private Font TentarFuenteCondensada(Control control, Size areaDisponible, Font fuenteActual, Graphics g)
        {
            try
            {
                Font fuenteCondensada = new Font("Arial Narrow", fuenteActual.Size, fuenteActual.Style);
                SizeF tamanoCondensado = g.MeasureString(control.Text, fuenteCondensada);
                
                if (tamanoCondensado.Width <= areaDisponible.Width)
                {
                    fuenteActual.Dispose();
                    return fuenteCondensada;
                }
                else
                {
                    fuenteCondensada.Dispose();
                }
            }
            catch
            {
                // Si falla Arial Narrow, continuar con la fuente actual
            }
            
            return fuenteActual;
        }

        private Font TentarFuenteCompacta(Control control, Size areaDisponible, Font fuenteActual, Graphics g)
        {
            string[] fuentesCompactas = { "Tahoma", "Segoe UI", "Calibri" };
            
            foreach (string nombreFuente in fuentesCompactas)
            {
                try
                {
                    Font fuenteCompacta = new Font(nombreFuente, fuenteActual.Size, fuenteActual.Style);
                    SizeF tamanoCompacto = g.MeasureString(control.Text, fuenteCompacta);
                    
                    if (tamanoCompacto.Width <= areaDisponible.Width)
                    {
                        fuenteActual.Dispose();
                        return fuenteCompacta;
                    }
                    else
                    {
                        fuenteCompacta.Dispose();
                    }
                }
                catch
                {
                    continue;
                }
            }
            
            return fuenteActual;
        }

        #endregion

        #region Posicionamiento y Centrado Avanzado

        private void PosicionarControles()
        {
            // Asegurar que los labels ocupen todo el espacio disponible de sus contenedores
            ConfigurarTamanos();

            // Centrar timer en panel izquierdo superior
            CentrarEnContenedor(lblTimer, pnSuperiorIzquierda, ContentAlignment.MiddleRight);

            // Centrar "SEG" en panel derecho superior
            CentrarEnContenedor(label1, pnSuperiorDerecha, ContentAlignment.MiddleLeft);

            // Centrar "APUESTA" en panel inferior arriba
            CentrarEnContenedor(label3, pnInferiorArriba);

            // Centrar resultado en panel inferior abajo
            CentrarEnContenedor(lblResultado, pnInferiorAbajo);
        }

        private void ConfigurarTamanos()
        {
            // Configurar lblTimer para que ocupe la mayor parte de su contenedor
            if (lblTimer != null && pnSuperiorIzquierda != null)
            {
                lblTimer.Size = new Size(
                    pnSuperiorIzquierda.ClientSize.Width - 10, // 5px margen a cada lado
                    pnSuperiorIzquierda.ClientSize.Height - 10
                );
            }

            // Configurar label1 (SEG) para que ocupe la mayor parte de su contenedor
            if (label1 != null && pnSuperiorDerecha != null)
            {
                label1.Size = new Size(
                    pnSuperiorDerecha.ClientSize.Width - 10,
                    pnSuperiorDerecha.ClientSize.Height - 10
                );
            }

            // Configurar label3 (APUESTA) para que ocupe todo el panel superior del área inferior
            if (label3 != null && pnInferiorArriba != null)
            {
                label3.Size = new Size(
                    pnInferiorArriba.ClientSize.Width - 10,
                    pnInferiorArriba.ClientSize.Height - 10
                );
            }

            // Configurar lblResultado para que ocupe todo el panel inferior del área inferior
            if (lblResultado != null && pnInferiorAbajo != null)
            {
                lblResultado.Size = new Size(
                    pnInferiorAbajo.ClientSize.Width - 10,
                    pnInferiorAbajo.ClientSize.Height - 10
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

        private void CentrarYPosicionarResultado()
        {
            // Este método ya no es necesario ya que el resultado tiene su propio panel
            // Mantener por compatibilidad, pero usar CentrarEnContenedor directamente
            CentrarEnContenedor(lblResultado, pnInferiorAbajo);
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
            ApplyResponsiveLayout();
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
        /// Cambia la proporción del timer vs SEG en el área superior
        /// </summary>
        /// <param name="timerParts">Partes para el timer (por defecto 8 = 80%)</param>
        /// <param name="segParts">Partes para SEG (por defecto 2 = 20%)</param>
        public void SetTimerProportion(int timerParts = 8, int segParts = 2)
        {
            if (_isInitializing || this.Width <= 0 || this.Height <= 0)
                return;

            try
            {
                this.SuspendLayout();

                // Solo redistribuir la sección superior con las nuevas proporciones
                DistribuirHorizontal(pnSuperior, pnSuperiorIzquierda, timerParts, pnSuperiorDerecha, segParts);

                // Reajustar fuentes según las nuevas proporciones
                float factorEscala = CalcularFactorEscala();
                float timerFactor = 0.18f + (timerParts - 4) * 0.01f; // Ajustar según proporción
                float segFactor = Math.Max(0.04f, 0.08f - (timerParts - 4) * 0.005f); // Reducir si timer es más grande

                AjustarFuenteConAjusteAutomatico(lblTimer, pnSuperiorIzquierda, factorEscala * timerFactor, FontStyle.Bold, 20f, 400f);
                AjustarFuenteConAjusteAutomatico(label1, pnSuperiorDerecha, factorEscala * segFactor, FontStyle.Bold, 10f, 120f);

                // Reposicionar controles
                ConfigurarTamanos();
                CentrarEnContenedor(lblTimer, pnSuperiorIzquierda,ContentAlignment.TopRight);
                CentrarEnContenedor(label1, pnSuperiorDerecha, ContentAlignment.MiddleLeft);

            }
            finally
            {
                this.ResumeLayout();
            }
        }

        /// <summary>
        /// Fuerza el uso de proporciones para pantalla pequeña (70%:30%)
        /// </summary>
        public void ForzarLayoutPantallaPequena()
        {
            SetTimerProportion(7, 3);
            
            // También ajustar factores de fuente para pantalla pequeña
            float factorEscala = CalcularFactorEscala();
            AjustarFuenteConAjusteAutomatico(lblTimer, pnSuperiorIzquierda, factorEscala * 0.20f, FontStyle.Bold, 20f, 400f);
            AjustarFuenteConAjusteAutomatico(label1, pnSuperiorDerecha, factorEscala * 0.08f, FontStyle.Bold, 10f, 120f);
        }

        /// <summary>
        /// Fuerza el uso de proporciones para pantalla grande (80%:20%)
        /// </summary>
        public void ForzarLayoutPantallaGrande()
        {
            SetTimerProportion(8, 2);
            
            // También ajustar factores de fuente para pantalla grande
            float factorEscala = CalcularFactorEscala();
            AjustarFuenteConAjusteAutomatico(lblTimer, pnSuperiorIzquierda, factorEscala * 0.22f, FontStyle.Bold, 20f, 400f);
            AjustarFuenteConAjusteAutomatico(label1, pnSuperiorDerecha, factorEscala * 0.06f, FontStyle.Bold, 10f, 120f);
        }

        /// <summary>
        /// Fuerza el reajuste de todas las fuentes para evitar recortes
        /// </summary>
        public void EnsureTextFits()
        {
            ReajustarFuenteSiEsNecesario(lblTimer, pnSuperiorIzquierda);
            ReajustarFuenteSiEsNecesario(label1, pnSuperiorDerecha);
            ReajustarFuenteSiEsNecesario(label3, pnInferiorArriba);
            ReajustarFuenteSiEsNecesario(lblResultado, pnInferiorAbajo);
        }

        /// <summary>
        /// Obtiene información de depuración sobre el ajuste de texto
        /// </summary>
        public string GetTextFitDebugInfo()
        {
            var info = new System.Text.StringBuilder();
            info.AppendLine("=== DEBUG: Ajuste de Texto ===");
            
            // Información del sistema
            Screen currentScreen = Screen.FromControl(this);
            bool isPantallaPequena = DetectarPantallaPequena();
            
            info.AppendLine($"Pantalla: {currentScreen.WorkingArea.Width}x{currentScreen.WorkingArea.Height}");
            info.AppendLine($"Formulario: {this.Width}x{this.Height}");
            info.AppendLine($"Pantalla pequeña detectada: {(isPantallaPequena ? "SÍ" : "NO")}");
            info.AppendLine($"Proporción automática: {(isPantallaPequena ? "70%:30%" : "80%:20%")}");
            info.AppendLine();
            
            info.AppendLine($"Proporción actual - Timer: {pnSuperiorIzquierda?.Width}px, SEG: {pnSuperiorDerecha?.Width}px");
            if (pnSuperiorIzquierda?.Width > 0 && pnSuperiorDerecha?.Width > 0)
            {
                float totalWidth = pnSuperiorIzquierda.Width + pnSuperiorDerecha.Width;
                info.AppendLine($"Ratio real: {(pnSuperiorIzquierda.Width * 100.0 / totalWidth):F1}% : {(pnSuperiorDerecha.Width * 100.0 / totalWidth):F1}%");
            }
            info.AppendLine();
            
            AddControlDebugInfo(info, "Timer", lblTimer, pnSuperiorIzquierda);
            AddControlDebugInfo(info, "SEG", label1, pnSuperiorDerecha);
            AddControlDebugInfo(info, "APUESTA", label3, pnInferiorArriba);
            AddControlDebugInfo(info, "Resultado", lblResultado, pnInferiorAbajo);
            
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
            lblTimer.Text = currentCount.ToString("00");
            
            // Reajustar fuente solo si el formato cambió (ej: de "99" a "9" o a "00")
            string previousFormat = (currentCount + 1).ToString("00");
            if (previousFormat.Length != lblTimer.Text.Length)
            {
                ReajustarFuenteSiEsNecesario(lblTimer, pnSuperiorIzquierda);
            }

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
            
            // Reajustar fuente si es necesario
            ReajustarFuenteSiEsNecesario(lblTimer, pnSuperiorIzquierda);
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

            // Reajustar la fuente si el texto cambió significativamente
            ReajustarFuenteSiEsNecesario(lblResultado, pnInferiorAbajo);

            // Reposicionar el resultado después de actualizar el texto
            CentrarYPosicionarResultado();
        }

        private void ReajustarFuenteSiEsNecesario(Control control, Control contenedor)
        {
            if (control?.Font == null || contenedor == null || string.IsNullOrEmpty(control.Text))
                return;

            try
            {
                using (Graphics g = control.CreateGraphics())
                {
                    // Medir el tamaño actual del texto
                    SizeF tamanoTexto = g.MeasureString(control.Text, control.Font);
                    
                    // Verificar si el texto se sale del control
                    if (tamanoTexto.Width > control.Width || tamanoTexto.Height > control.Height)
                    {
                        // Recalcular la fuente para que quepa
                        Font nuevaFuente = AjustarTamanoParaQueQuepa(control, contenedor, control.Font, 8f);
                        if (nuevaFuente != control.Font)
                        {
                            control.Font = nuevaFuente;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reajustando fuente: {ex.Message}");
            }
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
