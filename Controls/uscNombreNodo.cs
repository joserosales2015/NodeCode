using NodeCode.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NodeCode.Controls
{
    public partial class uscNombreNodo : UserControl
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
                lblTitulo.Text = text;
                txtTitulo.Text = text;
                RedimensionarAnchoPorTexto(lblTitulo, text);
            }
        }

        [Browsable(true)]
        [Category("Eventos Personalizad0s")]
        [Description("Tiene lugar cuando el texto ha cambiado.")]
        public event EventHandler<EventArgs> TextNodeChanged;
        #endregion

        public uscNombreNodo()
        {
            InitializeComponent();
            this.MouseLeave += txtTitulo_MouseLeave!;
            this.Leave += txtTitulo_Leave!;

        }

        #region Metodos
        private void RedimensionarAnchoPorTexto(object objeto, string texto)
        {
            if (objeto is Label)
            {
                ((Label)objeto).Width = TextRenderer.MeasureText(texto + " ", ((Label)objeto).Font).Width + 10;
            }
            else if (objeto is System.Windows.Forms.TextBox)
            {
                ((System.Windows.Forms.TextBox)objeto).Width = TextRenderer.MeasureText(texto + " ", ((System.Windows.Forms.TextBox)objeto).Font).Width + 10;
            }
        }

        private void DesenfocarNombreNodo()
        {
            this.BackColor = Color.FromArgb(82, 84, 86);
            txtTitulo.BackColor = this.BackColor;
            txtTitulo.SelectionStart = 0;
            txtTitulo.Visible = false;
            txtTitulo.Text = txtTitulo.Text.Trim();
            lblTitulo.Text = txtTitulo.Text;
            text = txtTitulo.Text;
            RedimensionarAnchoPorTexto(lblTitulo, lblTitulo.Text);
            lblTitulo.Visible = true;
            this.ActiveControl = null;
            TextNodeChanged?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Eventos
        private void lblTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            this.BackColor = Color.FromArgb(45, 46, 46);
            lblTitulo.Visible = false;
            txtTitulo.BackColor = this.BackColor;
            txtTitulo.Visible = true;
        }

        private void txtTitulo_Click(object sender, EventArgs e)
        {
            tituloClic = true;
        }

        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {
            RedimensionarAnchoPorTexto(txtTitulo, txtTitulo.Text);
        }

        private void txtTitulo_MouseLeave(object sender, EventArgs e)
        {
            if (!this.tituloClic)
            {
                DesenfocarNombreNodo();
            }
        }

        private void txtTitulo_Leave(object sender, EventArgs e)
        {
            DesenfocarNombreNodo();
            tituloClic = false;
        }        

        private void txtTitulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DesenfocarNombreNodo();
                e.SuppressKeyPress = true;
            }
        }
        #endregion
    }
}
