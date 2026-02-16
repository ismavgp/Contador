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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.btnReset = new System.Windows.Forms.Button();
            this.btnProcesa = new System.Windows.Forms.Button();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.rbSuma = new System.Windows.Forms.RadioButton();
            this.rbResta = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.txtNroJuego = new System.Windows.Forms.TextBox();
            this.brnGuardarJuego = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUtilidad = new System.Windows.Forms.TextBox();
            this.txtPorcentaje = new System.Windows.Forms.NumericUpDown();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPausa = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ajustesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alertaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarContraseñaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNumero2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.BackgroundImage = global::WinContador.Properties.Resources.btnReset;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReset.Location = new System.Drawing.Point(251, 41);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(76, 39);
            this.btnReset.TabIndex = 4;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnProcesa
            // 
            this.btnProcesa.BackColor = System.Drawing.Color.Transparent;
            this.btnProcesa.BackgroundImage = global::WinContador.Properties.Resources.btnProcesar;
            this.btnProcesa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProcesa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProcesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnProcesa.Location = new System.Drawing.Point(38, 215);
            this.btnProcesa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProcesa.Name = "btnProcesa";
            this.btnProcesa.Size = new System.Drawing.Size(227, 44);
            this.btnProcesa.TabIndex = 1;
            this.btnProcesa.UseVisualStyleBackColor = false;
            this.btnProcesa.Click += new System.EventHandler(this.btnProcesa_Click);
            // 
            // txtResultado
            // 
            this.txtResultado.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtResultado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResultado.Font = new System.Drawing.Font("Segoe UI", 16.27826F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultado.ForeColor = System.Drawing.Color.Lime;
            this.txtResultado.Location = new System.Drawing.Point(43, 74);
            this.txtResultado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.Size = new System.Drawing.Size(224, 37);
            this.txtResultado.TabIndex = 0;
            this.txtResultado.Text = "00";
            this.txtResultado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtResultado.TextChanged += new System.EventHandler(this.txtResultado_TextChanged);
            // 
            // rbSuma
            // 
            this.rbSuma.AutoSize = true;
            this.rbSuma.BackColor = System.Drawing.Color.Transparent;
            this.rbSuma.Checked = true;
            this.rbSuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbSuma.ForeColor = System.Drawing.Color.White;
            this.rbSuma.Location = new System.Drawing.Point(38, 122);
            this.rbSuma.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbSuma.Name = "rbSuma";
            this.rbSuma.Size = new System.Drawing.Size(79, 24);
            this.rbSuma.TabIndex = 8;
            this.rbSuma.TabStop = true;
            this.rbSuma.Text = "Sumar";
            this.rbSuma.UseVisualStyleBackColor = false;
            // 
            // rbResta
            // 
            this.rbResta.AutoSize = true;
            this.rbResta.BackColor = System.Drawing.Color.Transparent;
            this.rbResta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbResta.ForeColor = System.Drawing.Color.White;
            this.rbResta.Location = new System.Drawing.Point(138, 122);
            this.rbResta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbResta.Name = "rbResta";
            this.rbResta.Size = new System.Drawing.Size(80, 24);
            this.rbResta.TabIndex = 9;
            this.rbResta.Text = "Restar";
            this.rbResta.UseVisualStyleBackColor = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.numericUpDown1.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.ForeColor = System.Drawing.Color.White;
            this.numericUpDown1.Location = new System.Drawing.Point(50, 59);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 30);
            this.numericUpDown1.TabIndex = 3;
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
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(134, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "SEG.";
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.Transparent;
            this.btnIniciar.BackgroundImage = global::WinContador.Properties.Resources.fondoBTN;
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIniciar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIniciar.Location = new System.Drawing.Point(28, 118);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(84, 39);
            this.btnIniciar.TabIndex = 14;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // txtNroJuego
            // 
            this.txtNroJuego.BackColor = System.Drawing.Color.Black;
            this.txtNroJuego.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNroJuego.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNroJuego.ForeColor = System.Drawing.Color.White;
            this.txtNroJuego.Location = new System.Drawing.Point(130, 120);
            this.txtNroJuego.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNroJuego.Name = "txtNroJuego";
            this.txtNroJuego.ReadOnly = true;
            this.txtNroJuego.Size = new System.Drawing.Size(161, 23);
            this.txtNroJuego.TabIndex = 15;
            this.txtNroJuego.Text = "0";
            this.txtNroJuego.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // brnGuardarJuego
            // 
            this.brnGuardarJuego.BackColor = System.Drawing.Color.Transparent;
            this.brnGuardarJuego.BackgroundImage = global::WinContador.Properties.Resources.btnGuardar;
            this.brnGuardarJuego.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.brnGuardarJuego.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.brnGuardarJuego.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.brnGuardarJuego.Location = new System.Drawing.Point(73, 278);
            this.brnGuardarJuego.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.brnGuardarJuego.Name = "brnGuardarJuego";
            this.brnGuardarJuego.Size = new System.Drawing.Size(180, 44);
            this.brnGuardarJuego.TabIndex = 2;
            this.brnGuardarJuego.UseVisualStyleBackColor = false;
            this.brnGuardarJuego.Click += new System.EventHandler(this.brnGuardarJuego_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(40, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 23);
            this.label4.TabIndex = 19;
            this.label4.Text = "N° Juego";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(109, 419);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Ver Historial";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(32, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 23);
            this.label6.TabIndex = 22;
            this.label6.Text = "% Utilidad";
            // 
            // txtUtilidad
            // 
            this.txtUtilidad.BackColor = System.Drawing.Color.Black;
            this.txtUtilidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUtilidad.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilidad.ForeColor = System.Drawing.Color.White;
            this.txtUtilidad.Location = new System.Drawing.Point(130, 214);
            this.txtUtilidad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUtilidad.Name = "txtUtilidad";
            this.txtUtilidad.ReadOnly = true;
            this.txtUtilidad.Size = new System.Drawing.Size(161, 23);
            this.txtUtilidad.TabIndex = 23;
            this.txtUtilidad.Text = "00";
            this.txtUtilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.BackColor = System.Drawing.Color.Black;
            this.txtPorcentaje.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPorcentaje.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentaje.ForeColor = System.Drawing.Color.White;
            this.txtPorcentaje.Location = new System.Drawing.Point(130, 164);
            this.txtPorcentaje.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(161, 26);
            this.txtPorcentaje.TabIndex = 24;
            this.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPorcentaje.ValueChanged += new System.EventHandler(this.txtPorcentaje_ValueChanged);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Transparent;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnNuevo.Location = new System.Drawing.Point(14, 54);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(112, 40);
            this.btnNuevo.TabIndex = 4;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(51, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 23);
            this.label3.TabIndex = 27;
            this.label3.Text = "Utilidad";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.BackgroundImage = global::WinContador.Properties.Resources.fondoDet;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnPausa);
            this.panel1.Controls.Add(this.btnIniciar);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Location = new System.Drawing.Point(14, 125);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 179);
            this.panel1.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::WinContador.Properties.Resources.fondoCabecera;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 35);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "TIEMPO";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnPausa
            // 
            this.btnPausa.BackColor = System.Drawing.Color.Transparent;
            this.btnPausa.BackgroundImage = global::WinContador.Properties.Resources.fondoBTN;
            this.btnPausa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPausa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPausa.Font = new System.Drawing.Font("Segoe UI Semibold", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPausa.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPausa.Location = new System.Drawing.Point(119, 118);
            this.btnPausa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPausa.Name = "btnPausa";
            this.btnPausa.Size = new System.Drawing.Size(146, 39);
            this.btnPausa.TabIndex = 14;
            this.btnPausa.Text = "Pausar";
            this.btnPausa.UseVisualStyleBackColor = false;
            this.btnPausa.Click += new System.EventHandler(this.btnPausa_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.BackgroundImage = global::WinContador.Properties.Resources.fondoMenu;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajustesToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(710, 31);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ajustesToolStripMenuItem
            // 
            this.ajustesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alertaToolStripMenuItem,
            this.cambiarContraseñaToolStripMenuItem});
            this.ajustesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.ajustesToolStripMenuItem.Image = global::WinContador.Properties.Resources.IcoSetting;
            this.ajustesToolStripMenuItem.Name = "ajustesToolStripMenuItem";
            this.ajustesToolStripMenuItem.Size = new System.Drawing.Size(97, 27);
            this.ajustesToolStripMenuItem.Text = "Ajustes";
            this.ajustesToolStripMenuItem.Click += new System.EventHandler(this.ajustesToolStripMenuItem_Click);
            // 
            // alertaToolStripMenuItem
            // 
            this.alertaToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.alertaToolStripMenuItem.Name = "alertaToolStripMenuItem";
            this.alertaToolStripMenuItem.Size = new System.Drawing.Size(250, 28);
            this.alertaToolStripMenuItem.Text = "&Alerta";
            this.alertaToolStripMenuItem.Click += new System.EventHandler(this.alertaToolStripMenuItem_Click);
            // 
            // cambiarContraseñaToolStripMenuItem
            // 
            this.cambiarContraseñaToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cambiarContraseñaToolStripMenuItem.Name = "cambiarContraseñaToolStripMenuItem";
            this.cambiarContraseñaToolStripMenuItem.Size = new System.Drawing.Size(250, 28);
            this.cambiarContraseñaToolStripMenuItem.Text = "&Cambiar Contraseña";
            this.cambiarContraseñaToolStripMenuItem.Click += new System.EventHandler(this.cambiarContraseñaToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.ayudaToolStripMenuItem.Image = global::WinContador.Properties.Resources.IcoHelp;
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(91, 27);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            this.ayudaToolStripMenuItem.Click += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.BackgroundImage = global::WinContador.Properties.Resources.fondoDet;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.txtResultado);
            this.panel3.Controls.Add(this.btnProcesa);
            this.panel3.Controls.Add(this.rbResta);
            this.panel3.Controls.Add(this.txtNumero2);
            this.panel3.Controls.Add(this.rbSuma);
            this.panel3.Location = new System.Drawing.Point(14, 331);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(331, 324);
            this.panel3.TabIndex = 29;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = global::WinContador.Properties.Resources.fondoCabecera;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label7);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(331, 35);
            this.panel4.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 23);
            this.label7.TabIndex = 13;
            this.label7.Text = "APUESTA";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DimGray;
            this.panel5.BackgroundImage = global::WinContador.Properties.Resources.fondoDet;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.txtUtilidad);
            this.panel5.Controls.Add(this.txtPorcentaje);
            this.panel5.Controls.Add(this.brnGuardarJuego);
            this.panel5.Controls.Add(this.txtNroJuego);
            this.panel5.Location = new System.Drawing.Point(364, 128);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(331, 528);
            this.panel5.TabIndex = 30;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImage = global::WinContador.Properties.Resources.fondoCabecera;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label8);
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(331, 35);
            this.panel6.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(3, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 23);
            this.label8.TabIndex = 13;
            this.label8.Text = "RESULTADO";
            // 
            // txtNumero2
            // 
            this.txtNumero2.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtNumero2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumero2.Font = new System.Drawing.Font("Segoe UI", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero2.ForeColor = System.Drawing.Color.White;
            this.txtNumero2.Location = new System.Drawing.Point(42, 158);
            this.txtNumero2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNumero2.Name = "txtNumero2";
            this.txtNumero2.Size = new System.Drawing.Size(225, 27);
            this.txtNumero2.TabIndex = 7;
            this.txtNumero2.Text = "00";
            this.txtNumero2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumero2.Enter += new System.EventHandler(this.txtNumero2_Enter);
            this.txtNumero2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumero2_KeyPress_1);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = global::WinContador.Properties.Resources.FondoForm;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(710, 684);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...:::Contador:::...";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPrincipal_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnProcesa;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.RadioButton rbSuma;
        private System.Windows.Forms.RadioButton rbResta;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.TextBox txtNroJuego;
        private System.Windows.Forms.Button brnGuardarJuego;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUtilidad;
        private System.Windows.Forms.NumericUpDown txtPorcentaje;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ajustesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem alertaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarContraseñaToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPausa;
        private System.Windows.Forms.TextBox txtNumero2;
    }
}

