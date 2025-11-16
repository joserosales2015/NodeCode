using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NodeCode.Controls
{
    public partial class uscDescripcionNodo : UserControl
    {
        #region Variables y propiedades
        private bool tituloClic = false;
        private string text = "";

        [Browsable(true)]
        [Category("Propiedades Personalizadas")]
        [Description("Indica el texto que se mostrará en el control.")]
        public override string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value.Trim();
                txtDescripcion.Text = text;
            }
        }

        [Browsable(true)]
        [Category("Eventos Personalizados")]
        [Description("Tiene lugar cuando el texto ha cambiado.")]
        public event EventHandler<EventArgs> TextNodeChanged;
        #endregion

        public uscDescripcionNodo()
        {
            InitializeComponent();
            this.MouseLeave += txtDescripcion_MouseLeave!;
            this.Leave += txtDescripcion_Leave!;
        }

        #region Metodos
        private void DesenfocarDescripcionNodo()
        {
            this.BackColor = Color.FromArgb(82, 84, 86);
            txtDescripcion.BackColor = this.BackColor;
            txtDescripcion.SelectionStart = 0;
            txtDescripcion.Text = txtDescripcion.Text.Trim();
            text = txtDescripcion.Text;
            this.ActiveControl = null;
            TextNodeChanged?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Eventos
        private void txtDescripcion_MouseMove(object sender, MouseEventArgs e)
        {
            this.BackColor = Color.FromArgb(45, 46, 46);
            //lblTitulo.Visible = false;
            txtDescripcion.BackColor = this.BackColor;
            //txtTitulo.Visible = true;
        }

        private void txtDescripcion_Click(object sender, EventArgs e)
        {
            tituloClic = true;
        }
        #endregion

        private void txtDescripcion_MouseLeave(object sender, EventArgs e)
        {
            if (!this.tituloClic)
            {
                DesenfocarDescripcionNodo();
            }
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            DesenfocarDescripcionNodo();
            tituloClic = false;
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DesenfocarDescripcionNodo();
                e.SuppressKeyPress = true;
            }
        }
    }
}
