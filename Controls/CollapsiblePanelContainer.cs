using System;
using System.Drawing;
using System.Windows.Forms;

namespace NodeCode.Controls
{
    // Control principal que contiene el panel colapsable
    public class CollapsiblePanelContainer : UserControl
    {
        private Panel mainContentPanel;
        private Panel rightPanel;
        private Panel tabsPanel;
        private Splitter splitter;
        private Panel contentPanel;
        private Button collapseButton;

        private bool isCollapsed = false;
        private int expandedWidth = 250;

        public Panel ContentPanel => contentPanel;
        public string PanelTitle { get; set; } = "Panel";

        public CollapsiblePanelContainer()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(37, 37, 38);

            // Panel principal de contenido (izquierda)
            mainContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            // Panel derecho que contiene tabs y contenido
            rightPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = expandedWidth,
                BackColor = Color.FromArgb(37, 37, 38)
            };

            // Panel de tabs (vertical, a la derecha del todo)
            tabsPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 30,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            // Panel de contenido colapsable
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(37, 37, 38),
                Padding = new Padding(5)
            };

            // Botón de colapsar/expandir
            collapseButton = new Button
            {
                Text = "<<",
                Dock = DockStyle.Top,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(51, 51, 55),
                ForeColor = Color.White
            };
            collapseButton.FlatAppearance.BorderSize = 0;
            collapseButton.Click += CollapseButton_Click;

            // Splitter para redimensionar
            splitter = new Splitter
            {
                Dock = DockStyle.Right,
                Width = 3,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            // Agregar controles al rightPanel
            rightPanel.Controls.Add(contentPanel);
            rightPanel.Controls.Add(collapseButton);
            rightPanel.Controls.Add(tabsPanel);

            // Agregar al contenedor principal
            this.Controls.Add(mainContentPanel);
            this.Controls.Add(splitter);
            this.Controls.Add(rightPanel);
        }

        public void AddTab(string tabName, Action onTabClick)
        {
            var tabButton = new Button
            {
                Text = tabName,
                Height = 80,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(51, 51, 55),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F)
            };
            tabButton.FlatAppearance.BorderSize = 0;
            tabButton.Click += (s, e) =>
            {
                if (isCollapsed)
                {
                    TogglePanel();
                }
                onTabClick?.Invoke();
            };

            tabsPanel.Controls.Add(tabButton);
            tabButton.BringToFront();
        }

        private void CollapseButton_Click(object sender, EventArgs e)
        {
            TogglePanel();
        }

        private void TogglePanel()
        {
            isCollapsed = !isCollapsed;

            if (isCollapsed)
            {
                expandedWidth = rightPanel.Width;
                rightPanel.Width = 30;
                contentPanel.Visible = false;
                collapseButton.Visible = false;
                splitter.Visible = false;
            }
            else
            {
                rightPanel.Width = expandedWidth;
                contentPanel.Visible = true;
                collapseButton.Visible = true;
                splitter.Visible = true;
            }
        }

        public void SetMainContent(Control control)
        {
            mainContentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainContentPanel.Controls.Add(control);
        }

        public void AddContentControl(Control control)
        {
            control.Dock = DockStyle.Top;
            contentPanel.Controls.Add(control);
            control.BringToFront();
        }
    }
}