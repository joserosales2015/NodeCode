using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NodeCode.Models;

namespace NodeCode.Controls
{
    public class NodePropertiesDialog : Form
    {
        private FlowNode node;
        public bool NodeDeleted { get; private set; }

        public NodePropertiesDialog(FlowNode node)
        {
            this.node = node;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = $"Propiedades - {node.Name}";
            Size = new Size(400, 300);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var nameLabel = new Label { Text = "Nombre:", Location = new Point(20, 20), AutoSize = true };
            var nameTextBox = new TextBox { Location = new Point(20, 45), Size = new Size(340, 25), Text = node.Name };

            var propsLabel = new Label { Text = "Propiedades:", Location = new Point(20, 80), AutoSize = true };
            var propsTextBox = new TextBox
            {
                Location = new Point(20, 105),
                Size = new Size(340, 100),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Text = string.Join(Environment.NewLine, node.Properties.Select(kvp => $"{kvp.Key}: {kvp.Value}"))
            };

            var saveButton = new Button
            {
                Text = "Guardar",
                Location = new Point(200, 220),
                Size = new Size(80, 30)
            };

            saveButton.Click += (s, e) =>
            {
                node.Name = nameTextBox.Text;
                node.Summary = propsTextBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            };

            var deleteButton = new Button
            {
                Text = "Eliminar Nodo",
                Location = new Point(290, 220),
                Size = new Size(100, 30),
                BackColor = Color.IndianRed,
                ForeColor = Color.White
            };

            deleteButton.Click += (s, e) =>
            {
                NodeDeleted = true;
                DialogResult = DialogResult.OK;
                Close();
            };

            Controls.AddRange(new Control[] { nameLabel, nameTextBox, propsLabel, propsTextBox, saveButton, deleteButton });
        }
    }
}