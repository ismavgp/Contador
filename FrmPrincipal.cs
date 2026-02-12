using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void btnProcesa_Click(object sender, EventArgs e)
        {
            int numero1 = int.Parse(txtResultado.Text); 
            int numero2 = int.Parse(txtNumero2.Text);   
            int resultado;

            if (rbSuma.Checked)
            {
                resultado = numero1 + numero2;
            }
            else
            {
                resultado = numero1 - numero2;
            }

            // Actualizar el resultado en el formulario principal
            txtResultado.Text = resultado.ToString();

            // Actualizar el resultado en el formulario secundario
            if (frmSecondary != null)
            {
                frmSecondary.UpdateResult(resultado);
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
            if (frmSecondary != null)
            {
                frmSecondary.UpdateResult(int.Parse(txtResultado.Text));
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

                int numero1 = int.Parse(txtResultado.Text);
                int numero2 = int.Parse(txtNumero2.Text);
                int resultado;

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

                // Guardar en la base de datos
                bool guardadoExitoso = dbHelper.SaveGame(numero1, numero2, resultado, operacion, tiempoContador);

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
