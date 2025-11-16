namespace NodeCode.Controls
{
    partial class uscDescripcionNodo
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
            txtDescripcion = new TextBox();
            SuspendLayout();
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(82, 84, 86);
            txtDescripcion.BorderStyle = BorderStyle.None;
            txtDescripcion.ForeColor = Color.LightGray;
            txtDescripcion.Location = new Point(4, 3);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(250, 16);
            txtDescripcion.TabIndex = 0;
            txtDescripcion.Text = "Descripción de pruebas para el nodo";
            txtDescripcion.Click += txtDescripcion_Click;
            txtDescripcion.KeyDown += txtDescripcion_KeyDown;
            txtDescripcion.Leave += txtDescripcion_Leave;
            txtDescripcion.MouseLeave += txtDescripcion_MouseLeave;
            txtDescripcion.MouseMove += txtDescripcion_MouseMove;
            // 
            // uscDescripcionNodo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(82, 84, 86);
            Controls.Add(txtDescripcion);
            Name = "uscDescripcionNodo";
            Padding = new Padding(4, 0, 4, 0);
            Size = new Size(261, 22);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtDescripcion;
    }
}
