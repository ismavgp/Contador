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
            this.pnSuperior = new System.Windows.Forms.Panel();
            this.pnSuperiorDerecha = new System.Windows.Forms.Panel();
            this.pnSuperiorIzquierda = new System.Windows.Forms.Panel();
            this.pnSuperior.SuspendLayout();
            this.pnSuperiorDerecha.SuspendLayout();
            this.pnSuperiorIzquierda.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblResultado
            // 
            this.lblResultado.BackColor = System.Drawing.Color.Transparent;
            this.lblResultado.Font = new System.Drawing.Font("Digital-7", 50F);
            this.lblResultado.ForeColor = System.Drawing.Color.Lime;
            this.lblResultado.Location = new System.Drawing.Point(46, 32);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(242, 103);
            this.lblResultado.TabIndex = 5;
            this.lblResultado.Text = "9000000";
            this.lblResultado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTimer
            // 
            this.lblTimer.BackColor = System.Drawing.Color.Transparent;
            this.lblTimer.Font = new System.Drawing.Font("Digital-7", 80F);
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(5, 5);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(400, 158);
            this.lblTimer.TabIndex = 8;
            this.lblTimer.Text = "000";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnSuperior
            // 
            this.pnSuperior.BackColor = System.Drawing.Color.Black;
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
            this.pnSuperiorDerecha.Controls.Add(this.lblResultado);
            this.pnSuperiorDerecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSuperiorDerecha.Location = new System.Drawing.Point(411, 0);
            this.pnSuperiorDerecha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnSuperiorDerecha.Name = "pnSuperiorDerecha";
            this.pnSuperiorDerecha.Size = new System.Drawing.Size(373, 168);
            this.pnSuperiorDerecha.TabIndex = 1;
            // 
            // pnSuperiorIzquierda
            // 
            this.pnSuperiorIzquierda.BackColor = System.Drawing.Color.Black;
            this.pnSuperiorIzquierda.Controls.Add(this.lblTimer);
            this.pnSuperiorIzquierda.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSuperiorIzquierda.Location = new System.Drawing.Point(0, 0);
            this.pnSuperiorIzquierda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnSuperiorIzquierda.Name = "pnSuperiorIzquierda";
            this.pnSuperiorIzquierda.Size = new System.Drawing.Size(411, 168);
            this.pnSuperiorIzquierda.TabIndex = 0;
            // 
            // FrmSecondary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 405);
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
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Panel pnSuperior;
        private System.Windows.Forms.Panel pnSuperiorIzquierda;
        private System.Windows.Forms.Panel pnSuperiorDerecha;
    }
}