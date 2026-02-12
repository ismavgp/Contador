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
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpFiltro = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistorico)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinContador.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(11, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(244, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // dgHistorico
            // 
            this.dgHistorico.AllowUserToAddRows = false;
            this.dgHistorico.AllowUserToDeleteRows = false;
            this.dgHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHistorico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.fecha,
            this.hora,
            this.monto,
            this.porcentaje,
            this.utilidad});
            this.dgHistorico.Location = new System.Drawing.Point(11, 176);
            this.dgHistorico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgHistorico.MultiSelect = false;
            this.dgHistorico.Name = "dgHistorico";
            this.dgHistorico.ReadOnly = true;
            this.dgHistorico.RowHeadersVisible = false;
            this.dgHistorico.RowHeadersWidth = 62;
            this.dgHistorico.RowTemplate.Height = 28;
            this.dgHistorico.Size = new System.Drawing.Size(1141, 332);
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
            this.Id.Width = 150;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "Fecha";
            this.fecha.HeaderText = "FECHA";
            this.fecha.MinimumWidth = 8;
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Width = 150;
            // 
            // hora
            // 
            this.hora.DataPropertyName = "Hora";
            this.hora.HeaderText = "Hora";
            this.hora.MinimumWidth = 8;
            this.hora.Name = "hora";
            this.hora.ReadOnly = true;
            this.hora.Width = 150;
            // 
            // monto
            // 
            this.monto.DataPropertyName = "Monto";
            this.monto.HeaderText = "MONTO APOSTADO ACUMULADO";
            this.monto.MinimumWidth = 8;
            this.monto.Name = "monto";
            this.monto.ReadOnly = true;
            this.monto.Width = 300;
            // 
            // porcentaje
            // 
            this.porcentaje.DataPropertyName = "PorcentajeUtilidad";
            this.porcentaje.HeaderText = "% UTILIDAD";
            this.porcentaje.MinimumWidth = 8;
            this.porcentaje.Name = "porcentaje";
            this.porcentaje.ReadOnly = true;
            this.porcentaje.Width = 150;
            // 
            // utilidad
            // 
            this.utilidad.DataPropertyName = "Utilidad";
            this.utilidad.HeaderText = "UTILIDAD";
            this.utilidad.MinimumWidth = 8;
            this.utilidad.Name = "utilidad";
            this.utilidad.ReadOnly = true;
            this.utilidad.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dtpFiltro);
            this.groupBox1.Location = new System.Drawing.Point(261, 25);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(572, 59);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(267, 18);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(72, 27);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtpFiltro
            // 
            this.dtpFiltro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltro.Location = new System.Drawing.Point(93, 20);
            this.dtpFiltro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpFiltro.Name = "dtpFiltro";
            this.dtpFiltro.Size = new System.Drawing.Size(141, 22);
            this.dtpFiltro.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.btnExportar);
            this.panel1.Location = new System.Drawing.Point(261, 107);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(891, 48);
            this.panel1.TabIndex = 3;
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(773, 6);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(95, 34);
            this.btnExportar.TabIndex = 0;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // FrmHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 604);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgHistorico);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmHistorial";
            this.Text = "                     ";
            this.Load += new System.EventHandler(this.FrmHistorial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgHistorico)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgHistorico;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpFiltro;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn hora;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn porcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn utilidad;
    }
}