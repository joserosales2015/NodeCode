namespace NodeCode.Controls
{
    partial class uscNombreNodo
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            txtTitulo = new TextBox();
            lblTitulo = new Label();
            SuspendLayout();
            // 
            // txtTitulo
            // 
            txtTitulo.BackColor = Color.FromArgb(82, 84, 86);
            txtTitulo.BorderStyle = BorderStyle.None;
            txtTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTitulo.ForeColor = Color.WhiteSmoke;
            txtTitulo.Location = new Point(4, 0);
            txtTitulo.MaximumSize = new Size(220, 0);
            txtTitulo.MinimumSize = new Size(120, 0);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(160, 22);
            txtTitulo.TabIndex = 0;
            txtTitulo.TabStop = false;
            txtTitulo.Visible = false;
            txtTitulo.Click += txtTitulo_Click;
            txtTitulo.TextChanged += txtTitulo_TextChanged;
            txtTitulo.KeyDown += txtTitulo_KeyDown;
            txtTitulo.Leave += txtTitulo_Leave;
            txtTitulo.MouseLeave += txtTitulo_MouseLeave;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoEllipsis = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.WhiteSmoke;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.MaximumSize = new Size(220, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(136, 21);
            lblTitulo.TabIndex = 1;
            lblTitulo.MouseMove += lblTitulo_MouseMove;
            // 
            // uscNombreNodo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(82, 84, 86);
            Controls.Add(txtTitulo);
            Controls.Add(lblTitulo);
            Name = "uscNombreNodo";
            Padding = new Padding(4, 0, 4, 0);
            Size = new Size(171, 25);
            Text = "NombreNodo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTitulo;
        private Label lblTitulo;
    }
}
