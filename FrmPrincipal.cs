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

namespace WinContador
{
    public partial class FrmPrincipal : Form
    {
        private FrmSecondary frmSecondary;
        private DatabaseHelper dbHelper;

        public FrmPrincipal()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            SetupTextBoxValidation();
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
                // Formatear con separadores de miles y mantener decimales
                textBox.Text = number.ToString("N", CultureInfo.CurrentCulture);
            }
            else
            {
                textBox.Text = "0";
            }
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
            txtResultado.Text = resultado.ToString("N", CultureInfo.CurrentCulture);

            // Actualizar el resultado en el formulario secundario
            if (frmSecondary != null)
            {
                frmSecondary.UpdateResult((int)resultado);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Verificar que frmSecondary no sea null
            if (frmSecondary != null)
            {
                // Asegurarse de que el formulario esté visible
                if (!frmSecondary.Visible)
                {
                    frmSecondary.Show();
                }
                
                // Iniciar el countdown
                frmSecondary.SetCountdown((int)numericUpDown1.Value);
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
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // Verificar que frmSecondary no sea null
            if (frmSecondary != null)
            {
                // Asegurarse de que el formulario esté visible
                if (!frmSecondary.Visible)
                {
                    frmSecondary.Show();
                }

                // Iniciar el countdown
                frmSecondary.SetCountdown((int)numericUpDown1.Value);
                frmSecondary.StartCountdown();
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
                frmSecondary.UpdateResult((int)resultado);
            }
        }

        private void brnGuardarJuego_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores actuales
                if (string.IsNullOrWhiteSpace(txtResultado.Text) || string.IsNullOrWhiteSpace(txtNumero2.Text))
                {
                    MessageBox.Show("Por favor, complete los campos antes de guardar.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal numero1 = ParseFormattedNumber(txtResultado.Text);
                decimal numero2 = ParseFormattedNumber(txtNumero2.Text);
                decimal resultado;

                // Determinar la operación y calcular el resultado
                string operacion;
                if (rbSuma.Checked)
                {
                    operacion = "Suma";
                    resultado = numero1 + numero2;
                }
                else
                {
                    operacion = "Resta";
                    resultado = numero1 - numero2;
                }

                // Obtener el tiempo del contador desde el NumericUpDown
                int tiempoContador = (int)numericUpDown1.Value;

                // Guardar en la base de datos (convertir a int para compatibilidad)
                bool guardadoExitoso = dbHelper.SaveGame((int)numero1, (int)numero2, (int)resultado, operacion, tiempoContador);

                if (guardadoExitoso)
                {
                    MessageBox.Show("Juego guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar el juego.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
