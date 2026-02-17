namespace WinContador
{
    partial class FrmHistorial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHistorial));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgHistorico = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.utilidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMensual = new System.Windows.Forms.RadioButton();
            this.rbDiario = new System.Windows.Forms.RadioButton();
            this.cboMeses = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpFiltro = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.txtPorcentajeAcumulado = new System.Windows.Forms.TextBox();
            this.txtUtilidadAcumulada = new System.Windows.Forms.TextBox();
            this.txtMontoAcumulado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistorico)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinContador.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(274, 189);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // dgHistorico
            // 
            this.dgHistorico.AllowUserToAddRows = false;
            this.dgHistorico.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.dgHistorico.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgHistorico.BackgroundColor = System.Drawing.Color.Black;
            this.dgHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistorico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.fecha,
            this.hora,
            this.monto,
            this.porcentaje,
            this.utilidad});
            this.dgHistorico.Location = new System.Drawing.Point(12, 204);
            this.dgHistorico.MultiSelect = false;
            this.dgHistorico.Name = "dgHistorico";
            this.dgHistorico.ReadOnly = true;
            this.dgHistorico.RowHeadersVisible = false;
            this.dgHistorico.RowHeadersWidth = 62;
            this.dgHistorico.RowTemplate.Height = 28;
            this.dgHistorico.Size = new System.Drawing.Size(788, 244);
            this.dgHistorico.TabIndex = 1;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.Frozen = true;
            this.Id.HeaderText = "N° DE JUEGO";
            this.Id.MinimumWidth = 8;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 70;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "Fecha";
            this.fecha.HeaderText = "FECHA";
            this.fecha.MinimumWidth = 8;
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Width = 120;
            // 
            // hora
            // 
            this.hora.DataPropertyName = "Hora";
            this.hora.HeaderText = "HORA";
            this.hora.MinimumWidth = 8;
            this.hora.Name = "hora";
            this.hora.ReadOnly = true;
            this.hora.Width = 120;
            // 
            // monto
            // 
            this.monto.DataPropertyName = "Monto";
            this.monto.HeaderText = "MONTO APOSTADO ACUMULADO";
            this.monto.MinimumWidth = 8;
            this.monto.Name = "monto";
            this.monto.ReadOnly = true;
            this.monto.Width = 200;
            // 
            // porcentaje
            // 
            this.porcentaje.DataPropertyName = "PorcentajeUtilidad";
            this.porcentaje.HeaderText = "% UTILIDAD";
            this.porcentaje.MinimumWidth = 8;
            this.porcentaje.Name = "porcentaje";
            this.porcentaje.ReadOnly = true;
            this.porcentaje.Width = 130;
            // 
            // utilidad
            // 
            this.utilidad.DataPropertyName = "Utilidad";
            this.utilidad.HeaderText = "UTILIDAD";
            this.utilidad.MinimumWidth = 8;
            this.utilidad.Name = "utilidad";
            this.utilidad.ReadOnly = true;
            this.utilidad.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.BackgroundImage = global::WinContador.Properties.Resources.fondoCabecera;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.rbMensual);
            this.groupBox1.Controls.Add(this.rbDiario);
            this.groupBox1.Controls.Add(this.cboMeses);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dtpFiltro);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(294, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // rbMensual
            // 
            this.rbMensual.AutoSize = true;
            this.rbMensual.BackColor = System.Drawing.Color.Transparent;
            this.rbMensual.Location = new System.Drawing.Point(201, 23);
            this.rbMensual.Name = "rbMensual";
            this.rbMensual.Size = new System.Drawing.Size(89, 25);
            this.rbMensual.TabIndex = 3;
            this.rbMensual.TabStop = true;
            this.rbMensual.Text = "Mensual";
            this.rbMensual.UseVisualStyleBackColor = false;
            // 
            // rbDiario
            // 
            this.rbDiario.AutoSize = true;
            this.rbDiario.BackColor = System.Drawing.Color.Transparent;
            this.rbDiario.Location = new System.Drawing.Point(6, 28);
            this.rbDiario.Name = "rbDiario";
            this.rbDiario.Size = new System.Drawing.Size(71, 25);
            this.rbDiario.TabIndex = 3;
            this.rbDiario.TabStop = true;
            this.rbDiario.Text = "Diario";
            this.rbDiario.UseVisualStyleBackColor = false;
            // 
            // cboMeses
            // 
            this.cboMeses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMeses.FormattingEnabled = true;
            this.cboMeses.Items.AddRange(new object[] {
            "ENERO",
            "FEBRERO",
            "MARZO",
            "ABRIL",
            "MAYO",
            "JUNIO",
            "JULIO",
            "AGOSTO",
            "SETIEMBRE",
            "OCTUBRE",
            "NOVIEMBRE",
            "DICIEMBRE"});
            this.cboMeses.Location = new System.Drawing.Point(201, 56);
            this.cboMeses.Name = "cboMeses";
            this.cboMeses.Size = new System.Drawing.Size(201, 29);
            this.cboMeses.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Location = new System.Drawing.Point(426, 50);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(71, 35);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtpFiltro
            // 
            this.dtpFiltro.CalendarForeColor = System.Drawing.Color.White;
            this.dtpFiltro.CalendarMonthBackground = System.Drawing.Color.Black;
            this.dtpFiltro.CalendarTitleBackColor = System.Drawing.Color.DarkGreen;
            this.dtpFiltro.CalendarTitleForeColor = System.Drawing.SystemColors.ButtonFace;
            this.dtpFiltro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltro.Location = new System.Drawing.Point(6, 56);
            this.dtpFiltro.Name = "dtpFiltro";
            this.dtpFiltro.Size = new System.Drawing.Size(158, 29);
            this.dtpFiltro.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BackgroundImage = global::WinContador.Properties.Resources.fondoCabecera;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Location = new System.Drawing.Point(294, 149);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 51);
            this.panel1.TabIndex = 3;
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.Transparent;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Location = new System.Drawing.Point(396, 3);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(107, 45);
            this.btnExportar.TabIndex = 0;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // txtPorcentajeAcumulado
            // 
            this.txtPorcentajeAcumulado.BackColor = System.Drawing.Color.Black;
            this.txtPorcentajeAcumulado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPorcentajeAcumulado.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeAcumulado.ForeColor = System.Drawing.Color.White;
            this.txtPorcentajeAcumulado.Location = new System.Drawing.Point(581, 455);
            this.txtPorcentajeAcumulado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPorcentajeAcumulado.Name = "txtPorcentajeAcumulado";
            this.txtPorcentajeAcumulado.ReadOnly = true;
            this.txtPorcentajeAcumulado.Size = new System.Drawing.Size(102, 29);
            this.txtPorcentajeAcumulado.TabIndex = 4;
            this.txtPorcentajeAcumulado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUtilidadAcumulada
            // 
            this.txtUtilidadAcumulada.BackColor = System.Drawing.Color.Black;
            this.txtUtilidadAcumulada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUtilidadAcumulada.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilidadAcumulada.ForeColor = System.Drawing.Color.White;
            this.txtUtilidadAcumulada.Location = new System.Drawing.Point(695, 455);
            this.txtUtilidadAcumulada.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUtilidadAcumulada.Name = "txtUtilidadAcumulada";
            this.txtUtilidadAcumulada.ReadOnly = true;
            this.txtUtilidadAcumulada.Size = new System.Drawing.Size(102, 29);
            this.txtUtilidadAcumulada.TabIndex = 4;
            this.txtUtilidadAcumulada.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMontoAcumulado
            // 
            this.txtMontoAcumulado.BackColor = System.Drawing.Color.Black;
            this.txtMontoAcumulado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMontoAcumulado.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoAcumulado.ForeColor = System.Drawing.Color.White;
            this.txtMontoAcumulado.Location = new System.Drawing.Point(402, 455);
            this.txtMontoAcumulado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMontoAcumulado.Name = "txtMontoAcumulado";
            this.txtMontoAcumulado.ReadOnly = true;
            this.txtMontoAcumulado.Size = new System.Drawing.Size(172, 29);
            this.txtMontoAcumulado.TabIndex = 4;
            this.txtMontoAcumulado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(290, 455);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "SUMA TOTAL";
            // 
            // FrmHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WinContador.Properties.Resources.FondoForm;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(837, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUtilidadAcumulada);
            this.Controls.Add(this.txtMontoAcumulado);
            this.Controls.Add(this.txtPorcentajeAcumulado);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgHistorico);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHistorial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "                     ";
            this.Load += new System.EventHandler(this.FrmHistorial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistorico)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgHistorico;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpFiltro;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.TextBox txtPorcentajeAcumulado;
        private System.Windows.Forms.TextBox txtUtilidadAcumulada;
        private System.Windows.Forms.TextBox txtMontoAcumulado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn hora;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn porcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn utilidad;
        private System.Windows.Forms.RadioButton rbMensual;
        private System.Windows.Forms.RadioButton rbDiario;
        private System.Windows.Forms.ComboBox cboMeses;
    }
}