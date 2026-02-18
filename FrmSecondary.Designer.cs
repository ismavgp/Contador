namespace WinContador
{
    partial class FrmSecondary
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
            if (disposing)
            {
                
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSecondary));
            this.lblResultado = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnSuperior = new System.Windows.Forms.Panel();
            this.pnSuperiorDerecha = new System.Windows.Forms.Panel();
            this.pnSuperiorIzquierda = new System.Windows.Forms.Panel();
            this.pnInferior = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnInferiorArriba = new System.Windows.Forms.Panel();
            this.pnInferiorAbajo = new System.Windows.Forms.Panel();
            this.pnSuperior.SuspendLayout();
            this.pnSuperiorDerecha.SuspendLayout();
            this.pnSuperiorIzquierda.SuspendLayout();
            this.pnInferior.SuspendLayout();
            this.pnInferiorArriba.SuspendLayout();
            this.pnInferiorAbajo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = false;
            this.lblResultado.BackColor = System.Drawing.Color.Transparent;
            this.lblResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F);
            this.lblResultado.ForeColor = System.Drawing.Color.Lime;
            this.lblResultado.Location = new System.Drawing.Point(5, 5);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(774, 167);
            this.lblResultado.TabIndex = 5;
            this.lblResultado.Text = "9000000";
            this.lblResultado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = false;
            this.lblTimer.BackColor = System.Drawing.Color.Transparent;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F);
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(5, 5);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(539, 158);
            this.lblTimer.TabIndex = 8;
            this.lblTimer.Text = "000";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = false;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 158);
            this.label1.TabIndex = 10;
            this.label1.Text = "SEG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnSuperior
            // 
            this.pnSuperior.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnSuperior.Controls.Add(this.pnSuperiorDerecha);
            this.pnSuperior.Controls.Add(this.pnSuperiorIzquierda);
            this.pnSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnSuperior.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnSuperior.Name = "pnSuperior";
            this.pnSuperior.Size = new System.Drawing.Size(784, 168);
            this.pnSuperior.TabIndex = 11;
            // 
            // pnSuperiorDerecha
            // 
            this.pnSuperiorDerecha.Controls.Add(this.label1);
            this.pnSuperiorDerecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSuperiorDerecha.Location = new System.Drawing.Point(549, 0);
            this.pnSuperiorDerecha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnSuperiorDerecha.Name = "pnSuperiorDerecha";
            this.pnSuperiorDerecha.Size = new System.Drawing.Size(235, 168);
            this.pnSuperiorDerecha.TabIndex = 1;
            // 
            // pnSuperiorIzquierda
            // 
            this.pnSuperiorIzquierda.Controls.Add(this.lblTimer);
            this.pnSuperiorIzquierda.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSuperiorIzquierda.Location = new System.Drawing.Point(0, 0);
            this.pnSuperiorIzquierda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnSuperiorIzquierda.Name = "pnSuperiorIzquierda";
            this.pnSuperiorIzquierda.Size = new System.Drawing.Size(549, 168);
            this.pnSuperiorIzquierda.TabIndex = 0;
            // 
            // pnInferior
            // 
            this.pnInferior.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnInferior.Controls.Add(this.pnInferiorAbajo);
            this.pnInferior.Controls.Add(this.pnInferiorArriba);
            this.pnInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnInferior.Location = new System.Drawing.Point(0, 172);
            this.pnInferior.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnInferior.Name = "pnInferior";
            this.pnInferior.Size = new System.Drawing.Size(784, 231);
            this.pnInferior.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = false;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(774, 44);
            this.label3.TabIndex = 7;
            this.label3.Text = "APUESTA";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnInferiorArriba
            // 
            this.pnInferiorArriba.Controls.Add(this.label3);
            this.pnInferiorArriba.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnInferiorArriba.Location = new System.Drawing.Point(0, 0);
            this.pnInferiorArriba.Name = "pnInferiorArriba";
            this.pnInferiorArriba.Size = new System.Drawing.Size(784, 48);
            this.pnInferiorArriba.TabIndex = 8;
            // 
            // pnInferiorAbajo
            // 
            this.pnInferiorAbajo.Controls.Add(this.lblResultado);
            this.pnInferiorAbajo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnInferiorAbajo.Location = new System.Drawing.Point(0, 54);
            this.pnInferiorAbajo.Name = "pnInferiorAbajo";
            this.pnInferiorAbajo.Size = new System.Drawing.Size(784, 177);
            this.pnInferiorAbajo.TabIndex = 9;
            // 
            // FrmSecondary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 403);
            this.Controls.Add(this.pnInferior);
            this.Controls.Add(this.pnSuperior);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "FrmSecondary";
            this.Text = "..........:::::::::::::::Juego::::::::::::::::::..............";
            this.Load += new System.EventHandler(this.FrmSecondary_Load);
            this.pnSuperior.ResumeLayout(false);
            this.pnSuperiorDerecha.ResumeLayout(false);
            this.pnSuperiorIzquierda.ResumeLayout(false);
            this.pnInferior.ResumeLayout(false);
            this.pnInferiorArriba.ResumeLayout(false);
            this.pnInferiorAbajo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnSuperior;
        private System.Windows.Forms.Panel pnInferior;
        private System.Windows.Forms.Panel pnSuperiorIzquierda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnSuperiorDerecha;
        private System.Windows.Forms.Panel pnInferiorAbajo;
        private System.Windows.Forms.Panel pnInferiorArriba;
    }
}