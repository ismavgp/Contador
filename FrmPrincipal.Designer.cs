namespace WinContador
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReset = new System.Windows.Forms.Button();
            this.btnProcesa = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.txtNumero2 = new System.Windows.Forms.TextBox();
            this.rbSuma = new System.Windows.Forms.RadioButton();
            this.rbResta = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.brnGuardarJuego = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUtilidad = new System.Windows.Forms.TextBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnReset.Location = new System.Drawing.Point(115, 118);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(130, 68);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnProcesa
            // 
            this.btnProcesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnProcesa.Location = new System.Drawing.Point(163, 470);
            this.btnProcesa.Name = "btnProcesa";
            this.btnProcesa.Size = new System.Drawing.Size(228, 78);
            this.btnProcesa.TabIndex = 5;
            this.btnProcesa.Text = "Procesar";
            this.btnProcesa.UseVisualStyleBackColor = true;
            this.btnProcesa.Click += new System.EventHandler(this.btnProcesa_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F);
            this.txtResultado.Location = new System.Drawing.Point(66, 262);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.Size = new System.Drawing.Size(469, 71);
            this.txtResultado.TabIndex = 6;
            this.txtResultado.Text = "00";
            this.txtResultado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtResultado.TextChanged += new System.EventHandler(this.txtResultado_TextChanged);
            // 
            // txtNumero2
            // 
            this.txtNumero2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.txtNumero2.Location = new System.Drawing.Point(66, 386);
            this.txtNumero2.Name = "txtNumero2";
            this.txtNumero2.Size = new System.Drawing.Size(467, 62);
            this.txtNumero2.TabIndex = 7;
            this.txtNumero2.Text = "00";
            this.txtNumero2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumero2.TextChanged += new System.EventHandler(this.txtNumero2_TextChanged);
            // 
            // rbSuma
            // 
            this.rbSuma.AutoSize = true;
            this.rbSuma.Checked = true;
            this.rbSuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbSuma.Location = new System.Drawing.Point(68, 339);
            this.rbSuma.Name = "rbSuma";
            this.rbSuma.Size = new System.Drawing.Size(108, 33);
            this.rbSuma.TabIndex = 8;
            this.rbSuma.TabStop = true;
            this.rbSuma.Text = "Sumar";
            this.rbSuma.UseVisualStyleBackColor = true;
            // 
            // rbResta
            // 
            this.rbResta.AutoSize = true;
            this.rbResta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbResta.Location = new System.Drawing.Point(210, 339);
            this.rbResta.Name = "rbResta";
            this.rbResta.Size = new System.Drawing.Size(108, 33);
            this.rbResta.TabIndex = 9;
            this.rbResta.Text = "Restar";
            this.rbResta.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.numericUpDown1.Location = new System.Drawing.Point(229, 35);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(121, 53);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(356, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 29);
            this.label1.TabIndex = 12;
            this.label1.Text = "SEG.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(59, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 35);
            this.label2.TabIndex = 13;
            this.label2.Text = "Temporizador";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnIniciar.Location = new System.Drawing.Point(261, 118);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(130, 68);
            this.btnIniciar.TabIndex = 14;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.textBox1.Location = new System.Drawing.Point(133, 652);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(71, 44);
            this.textBox1.TabIndex = 15;
            // 
            // brnGuardarJuego
            // 
            this.brnGuardarJuego.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.brnGuardarJuego.Location = new System.Drawing.Point(787, 643);
            this.brnGuardarJuego.Name = "brnGuardarJuego";
            this.brnGuardarJuego.Size = new System.Drawing.Size(165, 53);
            this.brnGuardarJuego.TabIndex = 16;
            this.brnGuardarJuego.Text = "Guardar";
            this.brnGuardarJuego.UseVisualStyleBackColor = true;
            this.brnGuardarJuego.Click += new System.EventHandler(this.brnGuardarJuego_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.textBox2.Location = new System.Drawing.Point(513, 652);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(253, 44);
            this.textBox2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(508, 616);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 29);
            this.label3.TabIndex = 18;
            this.label3.Text = "Ganador";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(128, 616);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 29);
            this.label4.TabIndex = 19;
            this.label4.Text = "N°";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(128, 714);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 25);
            this.label5.TabIndex = 20;
            this.label5.Text = "Ver Historial";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(224, 616);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 35);
            this.label6.TabIndex = 22;
            this.label6.Text = "% Utilidad";
            // 
            // txtUtilidad
            // 
            this.txtUtilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.txtUtilidad.Location = new System.Drawing.Point(329, 654);
            this.txtUtilidad.Name = "txtUtilidad";
            this.txtUtilidad.ReadOnly = true;
            this.txtUtilidad.Size = new System.Drawing.Size(163, 44);
            this.txtUtilidad.TabIndex = 23;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.numericUpDown2.Location = new System.Drawing.Point(229, 654);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(94, 44);
            this.numericUpDown2.TabIndex = 24;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 764);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.txtUtilidad);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.brnGuardarJuego);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.rbResta);
            this.Controls.Add(this.rbSuma);
            this.Controls.Add(this.txtNumero2);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnProcesa);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label2);
            this.Name = "FrmPrincipal";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...:::Contador:::...";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnProcesa;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.TextBox txtNumero2;
        private System.Windows.Forms.RadioButton rbSuma;
        private System.Windows.Forms.RadioButton rbResta;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button brnGuardarJuego;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUtilidad;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
    }
}

