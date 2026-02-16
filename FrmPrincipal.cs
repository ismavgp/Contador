using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using WinContador.Entity;
using WinContador.Data;

namespace WinContador
{
    public partial class FrmPrincipal : Form
    {
        private FrmSecondary frmSecondary;
        // private DatabaseHelper dbHelper;
        private JuegoRepository juegoRepository;
        private bool _limpiarAlEscribir = false;
        private Timer _clearTimer;

        public FrmPrincipal()
        {
            InitializeComponent();
            SetupTextBoxValidation();

            // Allow the form to capture key presses before controls
            this.KeyPreview = true;
            this.KeyDown += FrmPrincipal_KeyDown;

            // Initialize the clear timer
            _clearTimer = new Timer();
            _clearTimer.Interval = 50; // 50ms delay
            _clearTimer.Tick += ClearTimer_Tick;

            LimpiarForm();
        }

        private void ClearTimer_Tick(object sender, EventArgs e)
        {
            _clearTimer.Stop();
            if (_limpiarAlEscribir)
            {
                txtNumero2.Clear();
                txtNumero2.Focus();
                _limpiarAlEscribir = false;
            }
        }

        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            // + => siempre suma
            if (e.KeyCode == Keys.Add)
            {
                try
                {
                    rbSuma.Focus();
                    rbSuma.Checked = true;
                    // Ejecutar la misma acción que el botón procesar
                    //btnProcesa.PerformClick();
                }
                catch
                {
                    // ignorar si no existen los controles en tiempo de diseño
                }
                e.Handled = true;
            }

            // - => siempre resta
            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                try
                {
                    rbResta.Focus();
                    rbResta.Checked = true;
                   // btnProcesa.PerformClick();
                }
                catch
                {
                    // ignorar si no existen los controles en tiempo de diseño
                }
                e.Handled = true;
            }

            // con enter procesar
            if (e.KeyCode == Keys.Enter)
            {

                btnProcesa.Focus();
                btnProcesa.PerformClick();
            }

            //Con i iniciar
            if (e.KeyCode == Keys.I)
            {
                btnIniciar.Focus();
                btnIniciar.PerformClick();
            }

            //Con r resetear
            if (e.KeyCode == Keys.R)
            {
                btnReset.Focus();
                btnReset.PerformClick();
            }

            //Con p pausar/reanudar
            if (e.KeyCode == Keys.P)
            {
                btnPausa.Focus();
                btnPausa.PerformClick();
            }
        }

        private void SetupTextBoxValidation()
        {
            // Configurar eventos de validación para los TextBoxes
            txtResultado.KeyPress += TextBox_KeyPress;
            txtNumero2.KeyPress += TextBox_KeyPress;
            txtResultado.Leave += TextBox_Leave;
            txtNumero2.Leave += TextBox_Leave;
            txtResultado.Enter += TextBox_Enter;
            txtNumero2.Enter += TextBox_Enter;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            // Remover formato cuando el usuario entra al campo para facilitar edición
            string cleanText = textBox.Text.Replace(",", "");
            if (decimal.TryParse(cleanText, out decimal number))
            {
                if (number == 0)
                {
                    textBox.Text = "";
                }
                else
                {
                    textBox.Text = cleanText;
                }
            }
            textBox.SelectAll();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, backspace, y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            TextBox textBox = sender as TextBox;

            // Solo permitir un punto decimal
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }

            // Manejar Enter para formato inmediato
            if (e.KeyChar == (char)Keys.Enter)
            {
                FormatNumberInTextBox(textBox);
                e.Handled = true;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            FormatNumberInTextBox(textBox);
        }

        private void FormatNumberInTextBox(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                return;
            }

            // Remover formato anterior para obtener el número puro
            string cleanText = textBox.Text.Replace(",", "");

            if (decimal.TryParse(cleanText, out decimal number))
            {
                // Formatear con separadores de miles y mantener o quitar decimales según corresponda
                textBox.Text = FormatDecimal(number);
            }
            else
            {
                textBox.Text = "0";
            }
        }

        private string FormatDecimal(decimal value)
        {
            // Redondear siempre a 2 decimales
            decimal rounded = Math.Round(value, 2);

            // Si es entero después del redondeo, mostrar sin decimales
            if (rounded % 1 == 0)
            {
                return rounded.ToString("N0", CultureInfo.CurrentCulture);
            }

            // Si tiene decimales distintos de .00, mostrar exactamente 2 decimales
            return rounded.ToString("N2", CultureInfo.CurrentCulture);
        }

        private decimal ParseFormattedNumber(string formattedText)
        {
            if (string.IsNullOrWhiteSpace(formattedText))
                return 0;

            // Remover separadores de miles para parsing
            string cleanText = formattedText.Replace(",", "");

            if (decimal.TryParse(cleanText, out decimal result))
                return result;

            return 0;
        }

        private void btnProcesa_Click(object sender, EventArgs e)
        {
            
            decimal numero1 = ParseFormattedNumber(txtResultado.Text);
            decimal numero2 = ParseFormattedNumber(txtNumero2.Text);
            decimal resultado;

            if (rbSuma.Checked)
            {
                resultado = numero1 + numero2;
            }
            else
            {
                resultado = numero1 - numero2;
            }

            // Actualizar el resultado en el formulario principal
            txtResultado.Text = FormatDecimal(resultado);

            // Actualizar el resultado en el formulario secundario
            if (frmSecondary != null)
            {
                frmSecondary.UpdateResult(FormatDecimal(resultado));
            }

            calcularUtilidad();

            _limpiarAlEscribir = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Verificar que frmSecondary no sea null
            if (frmSecondary != null)
            {
                // Asegurarse de que el formulario esté visible
                if (!frmSecondary.Visible)
                {
                    frmSecondary = new FrmSecondary();
                    frmSecondary.Show();
                }

                // Iniciar el countdown
                frmSecondary.SetCountdown((int)numericUpDown1.Value);
                btnPausa.Text = "Pausar";
                btnPausa.Enabled = false;
            }
            else
            {
                MessageBox.Show("FrmSecondary es null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            frmSecondary = new FrmSecondary();
            frmSecondary.Show();

            // Establecer valores iniciales formateados
            FormatNumberInTextBox(txtResultado);
            FormatNumberInTextBox(txtNumero2);

           // txtNumero2.Value = 00;


            //dar formato al menu desplegable

            // Aplicamos el renderizador personalizado
            menuStrip1.Renderer = new MyMenuRenderer();

            // Cambiamos el color de la barra principal y el texto
            menuStrip1.BackColor = Color.FromArgb(30, 30, 30);
            menuStrip1.ForeColor = Color.White;

            // Cambiar recursivamente el color de texto de todos los sub-ítems
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                SetBlackThemeToMenu(item);
            }


            btnPausa.Enabled = false;

        }

        private void SetBlackThemeToMenu(ToolStripMenuItem item)
        {
            item.ForeColor = Color.White;
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem)
                {
                    subItem.ForeColor = Color.White;
                    SetBlackThemeToMenu((ToolStripMenuItem)subItem);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            //verificar que el resultado no sea 0 antes de iniciar el countdown
            //var resultado = ParseFormattedNumber(txtResultado.Text);
            //if (resultado <= 0)
            //{
            //    MessageBox.Show("El Monto debe ser mayor a 0", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}


            // Verificar que frmSecondary no sea null
            if (frmSecondary != null)
            {
                // Asegurarse de que el formulario esté visible
                if (!frmSecondary.Visible)
                {
                    frmSecondary = new FrmSecondary();
                    frmSecondary.Show();
                }

                // Iniciar el countdown
                frmSecondary.SetCountdown((int)numericUpDown1.Value);
                frmSecondary.StartCountdown();
                btnPausa.Enabled = true;
            }
            else
            {
                MessageBox.Show("FrmSecondary es null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            FrmHistorial frmHistorial = new FrmHistorial();
            frmHistorial.ShowDialog();
        }

        private void txtNumero2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtResultado_TextChanged(object sender, EventArgs e)
        {
            // Actualizar el resultado en el formulario secundario
            if (frmSecondary != null && !string.IsNullOrWhiteSpace(txtResultado.Text))
            {
                decimal resultado = ParseFormattedNumber(txtResultado.Text);
                frmSecondary.UpdateResult(FormatDecimal(resultado));
            }
            calcularUtilidad();
        }

        private void brnGuardarJuego_Click(object sender, EventArgs e)
        {


            //validar que no sea 0

            decimal res = decimal.Parse(txtResultado.Text);
            if (res < 0)
            {
                MessageBox.Show("No se puede guardar un juego");
                return;
            }

            //validar porcentaje utilidad
            int porcent = Convert.ToInt32(txtPorcentaje.Text);
            if (porcent < 0)
            {
                MessageBox.Show("No se puede guardar un juego");
                return;
            }


            try
            {
                // Obtener los valores actuales
                JuegoEntity entity = new JuegoEntity()
                {
                    Id = 0,
                    Fecha = DateTime.Now,
                    Hora = DateTime.Now.ToString("HH:mm:ss"),
                    Monto = ParseFormattedNumber(txtResultado.Text),
                    PorcentajeUtilidad = txtPorcentaje.Value.ToString(),
                    Utilidad = ParseFormattedNumber(txtResultado.Text) * (txtPorcentaje.Value / 100)


                };

                var repo = new JuegoRepository();
                repo.CrearBaseSiNoExiste();


                //validar si existe

               if(!repo.ExisteId(int.Parse(txtNroJuego.Text)))
                {
                    if (!repo.Insertar(entity))
                    {
                        MessageBox.Show("Error al guardar el juego.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    entity.Id = int.Parse(txtNroJuego.Text);
                    if (!repo.Actualizar(entity))
                    {
                        MessageBox.Show("Error al guardar el juego.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }




            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarForm();
        }


        private void LimpiarForm()
        {
            juegoRepository = new JuegoRepository();
            juegoRepository.CrearBaseSiNoExiste();
            txtNumero2.Text = "0";
            txtResultado.Text = "0";
            rbSuma.Checked = true;
            txtNroJuego.Text = juegoRepository.ObtenerSiguienteId();
            txtUtilidad.Text = "0";
            txtPorcentaje.Value = 0;

        }

        private void txtPorcentaje_ValueChanged(object sender, EventArgs e)
        {
            calcularUtilidad();
        }

        private void calcularUtilidad()
        {
            //calcular la utilidad cada vez que cambie el porcentaje
            decimal monto = ParseFormattedNumber(txtResultado.Text);
            if (monto > 0)
            {
                decimal utilidad = monto * (txtPorcentaje.Value / 100);
                txtUtilidad.Text = FormatDecimal(utilidad);
            }
            else
            {
                txtUtilidad.Text = "0";
            }
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHelp frm = new FrmHelp();
            frm.ShowDialog();
        }

        private void ajustesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frmSecondary = Application.OpenForms["FrmSecondary"];

            if (frmSecondary != null)
            {
                frmSecondary.Close();
            }

            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        private void alertaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChangePassword frmChangePassword = new FrmChangePassword();
            frmChangePassword.ShowDialog();
        }


        private void txtNumero2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Si está marcado para limpiar al escribir y se está escribiendo un número
            if (_limpiarAlEscribir && char.IsDigit(e.KeyChar))
            {
                txtNumero2.Clear();
                _limpiarAlEscribir = false;
            }

            // Validar solo números
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnPausa_Click(object sender, EventArgs e)
        {
            // Verificar que frmSecondary no sea null
            if (frmSecondary != null)
            {
                // Si está pausado, reanudar; si no está pausado, pausar
                if (frmSecondary.IsPaused)
                {
                    frmSecondary.ResumeCountdown();
                    btnPausa.Text = "Pausar";
                }
                else
                {
                    frmSecondary.PauseCountdown();
                    btnPausa.Text = "Reanudar";
                }
            }
            else
            {
                MessageBox.Show("FrmSecondary es null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumero2_Enter(object sender, EventArgs e)
        {
            btnProcesa.PerformClick();
            
            // Start timer to clear the field after processing
            if (_limpiarAlEscribir)
            {
                _clearTimer.Start();
            }
        }
    }

    public class MyMenuRenderer : ToolStripProfessionalRenderer
    {
        public MyMenuRenderer() : base(new MyColorTable()) { }
    }

    public class MyColorTable : ProfessionalColorTable
    {
        // Color de fondo del menú desplegable
        public override Color ToolStripDropDownBackground => Color.FromArgb(20, 20, 20);

        // Color del borde del menú
        public override Color MenuBorder => Color.FromArgb(50, 50, 50);

        // Color del ítem cuando pasas el mouse (Hover)
        public override Color MenuItemSelected => Color.FromArgb(0, 120, 0); // Verde Fiera

        // Color del borde del ítem seleccionado
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(0, 120, 0);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(0, 120, 0);
    }
}
