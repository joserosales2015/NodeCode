namespace NodeCode.Forms
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            imageList1 = new ImageList(components);
            pnlDetalleNodo = new Panel();
            btnOcultarPanelPropiedades = new Button();
            imageList2 = new ImageList(components);
            scintilla = new ScintillaNET.Scintilla();
            pnlCabeceraNodo = new Panel();
            uscDescripcionNodo = new NodeCode.Controls.uscDescripcionNodo();
            uscNombreNodo = new NodeCode.Controls.uscNombreNodo();
            lblTituloPanelPropiedades = new Label();
            label4 = new Label();
            uscCanvas = new NodeCode.Controls.NodeCanvas();
            button1 = new Button();
            pnlTabsDerecha = new FlowLayoutPanel();
            pnlTabPropiedadesNodo = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            label2 = new Label();
            splitter1 = new Splitter();
            pnlDetalleNodo.SuspendLayout();
            pnlCabeceraNodo.SuspendLayout();
            uscCanvas.SuspendLayout();
            pnlTabsDerecha.SuspendLayout();
            pnlTabPropiedadesNodo.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "start.png");
            imageList1.Images.SetKeyName(1, "end.png");
            imageList1.Images.SetKeyName(2, "input.png");
            imageList1.Images.SetKeyName(3, "input2.png");
            imageList1.Images.SetKeyName(4, "process.png");
            imageList1.Images.SetKeyName(5, "output.png");
            imageList1.Images.SetKeyName(6, "decision.png");
            imageList1.Images.SetKeyName(7, "decision2.png");
            imageList1.Images.SetKeyName(8, "loop.png");
            imageList1.Images.SetKeyName(9, "cycle.png");
            // 
            // pnlDetalleNodo
            // 
            pnlDetalleNodo.BackColor = Color.FromArgb(65, 66, 68);
            pnlDetalleNodo.Controls.Add(btnOcultarPanelPropiedades);
            pnlDetalleNodo.Controls.Add(scintilla);
            pnlDetalleNodo.Controls.Add(pnlCabeceraNodo);
            pnlDetalleNodo.Controls.Add(lblTituloPanelPropiedades);
            pnlDetalleNodo.Controls.Add(label4);
            pnlDetalleNodo.Dock = DockStyle.Right;
            pnlDetalleNodo.Location = new Point(582, 0);
            pnlDetalleNodo.MaximumSize = new Size(600, 0);
            pnlDetalleNodo.Name = "pnlDetalleNodo";
            pnlDetalleNodo.Size = new Size(600, 534);
            pnlDetalleNodo.TabIndex = 0;
            pnlDetalleNodo.Visible = false;
            // 
            // btnOcultarPanelPropiedades
            // 
            btnOcultarPanelPropiedades.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOcultarPanelPropiedades.BackColor = Color.FromArgb(31, 31, 31);
            btnOcultarPanelPropiedades.FlatAppearance.BorderColor = Color.Silver;
            btnOcultarPanelPropiedades.FlatAppearance.BorderSize = 0;
            btnOcultarPanelPropiedades.FlatAppearance.MouseDownBackColor = Color.FromArgb(82, 84, 86);
            btnOcultarPanelPropiedades.FlatAppearance.MouseOverBackColor = Color.FromArgb(65, 66, 68);
            btnOcultarPanelPropiedades.FlatStyle = FlatStyle.Flat;
            btnOcultarPanelPropiedades.ImageIndex = 0;
            btnOcultarPanelPropiedades.ImageList = imageList2;
            btnOcultarPanelPropiedades.Location = new Point(581, 5);
            btnOcultarPanelPropiedades.Name = "btnOcultarPanelPropiedades";
            btnOcultarPanelPropiedades.Size = new Size(19, 18);
            btnOcultarPanelPropiedades.TabIndex = 7;
            btnOcultarPanelPropiedades.UseVisualStyleBackColor = false;
            btnOcultarPanelPropiedades.Click += btnOcultarPanelPropiedades_Click;
            // 
            // imageList2
            // 
            imageList2.ColorDepth = ColorDepth.Depth32Bit;
            imageList2.ImageStream = (ImageListStreamer)resources.GetObject("imageList2.ImageStream");
            imageList2.TransparentColor = Color.Transparent;
            imageList2.Images.SetKeyName(0, "right_arrow.png");
            // 
            // scintilla
            // 
            scintilla.AutocompleteListSelectedBackColor = Color.FromArgb(0, 120, 215);
            scintilla.AutomaticFold = ScintillaNET.AutomaticFold.Show;
            scintilla.BufferedDraw = false;
            scintilla.Dock = DockStyle.Fill;
            scintilla.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            scintilla.LexerName = "cpp";
            scintilla.Location = new Point(0, 100);
            scintilla.Name = "scintilla";
            scintilla.ScrollWidth = 49;
            scintilla.Size = new Size(600, 434);
            scintilla.TabIndex = 0;
            scintilla.Technology = ScintillaNET.Technology.DirectWrite;
            scintilla.Text = "\r\n";
            scintilla.UseTabs = true;
            scintilla.CharAdded += scintilla_CharAdded;
            scintilla.InsertCheck += scintilla_InsertCheck;
            scintilla.MarginClick += scintilla_MarginClick;
            // 
            // pnlCabeceraNodo
            // 
            pnlCabeceraNodo.BackColor = Color.FromArgb(82, 84, 86);
            pnlCabeceraNodo.Controls.Add(uscDescripcionNodo);
            pnlCabeceraNodo.Controls.Add(uscNombreNodo);
            pnlCabeceraNodo.Dock = DockStyle.Top;
            pnlCabeceraNodo.Location = new Point(0, 24);
            pnlCabeceraNodo.Name = "pnlCabeceraNodo";
            pnlCabeceraNodo.Size = new Size(600, 76);
            pnlCabeceraNodo.TabIndex = 6;
            // 
            // uscDescripcionNodo
            // 
            uscDescripcionNodo.AutoSize = true;
            uscDescripcionNodo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            uscDescripcionNodo.BackColor = Color.FromArgb(82, 84, 86);
            uscDescripcionNodo.Location = new Point(13, 39);
            uscDescripcionNodo.Margin = new Padding(5, 2, 5, 5);
            uscDescripcionNodo.Name = "uscDescripcionNodo";
            uscDescripcionNodo.Padding = new Padding(4, 0, 4, 0);
            uscDescripcionNodo.Size = new Size(261, 22);
            uscDescripcionNodo.TabIndex = 5;
            uscDescripcionNodo.TextNodeChanged += uscDescripcionNodo_TextNodeChanged;
            // 
            // uscNombreNodo
            // 
            uscNombreNodo.AutoSize = true;
            uscNombreNodo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            uscNombreNodo.BackColor = Color.FromArgb(82, 84, 86);
            uscNombreNodo.Location = new Point(13, 11);
            uscNombreNodo.Margin = new Padding(10, 2, 5, 5);
            uscNombreNodo.Name = "uscNombreNodo";
            uscNombreNodo.Padding = new Padding(4, 0, 4, 0);
            uscNombreNodo.Size = new Size(137, 21);
            uscNombreNodo.TabIndex = 4;
            uscNombreNodo.TextNodeChanged += uscNombreNodo_TextNodeChanged;
            // 
            // lblTituloPanelPropiedades
            // 
            lblTituloPanelPropiedades.BackColor = Color.FromArgb(31, 31, 31);
            lblTituloPanelPropiedades.Dock = DockStyle.Top;
            lblTituloPanelPropiedades.ForeColor = Color.White;
            lblTituloPanelPropiedades.Location = new Point(0, 4);
            lblTituloPanelPropiedades.Name = "lblTituloPanelPropiedades";
            lblTituloPanelPropiedades.Padding = new Padding(3, 0, 0, 2);
            lblTituloPanelPropiedades.Size = new Size(600, 20);
            lblTituloPanelPropiedades.TabIndex = 5;
            lblTituloPanelPropiedades.Text = "Propiedades del Nodo";
            lblTituloPanelPropiedades.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.BackColor = Color.FromArgb(255, 111, 92);
            label4.Dock = DockStyle.Top;
            label4.Location = new Point(0, 0);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(600, 4);
            label4.TabIndex = 3;
            // 
            // uscCanvas
            // 
            uscCanvas.BackColor = Color.FromArgb(45, 46, 46);
            uscCanvas.Controls.Add(button1);
            uscCanvas.Dock = DockStyle.Fill;
            uscCanvas.Location = new Point(0, 0);
            uscCanvas.Name = "uscCanvas";
            uscCanvas.NodeIcons = imageList1;
            uscCanvas.Size = new Size(1212, 534);
            uscCanvas.TabIndex = 2;
            uscCanvas.TabStop = true;
            uscCanvas.ClickNode += uscCanvas_ClickNode;
            uscCanvas.ClickCanvas += uscCanvas_ClickCanvas;
            // 
            // button1
            // 
            button1.Location = new Point(310, 28);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pnlTabsDerecha
            // 
            pnlTabsDerecha.AutoSize = true;
            pnlTabsDerecha.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlTabsDerecha.BackColor = Color.FromArgb(31, 31, 31);
            pnlTabsDerecha.Controls.Add(pnlTabPropiedadesNodo);
            pnlTabsDerecha.Controls.Add(panel2);
            pnlTabsDerecha.Dock = DockStyle.Right;
            pnlTabsDerecha.Location = new Point(1182, 0);
            pnlTabsDerecha.Margin = new Padding(3, 3, 0, 3);
            pnlTabsDerecha.Name = "pnlTabsDerecha";
            pnlTabsDerecha.Size = new Size(30, 534);
            pnlTabsDerecha.TabIndex = 0;
            // 
            // pnlTabPropiedadesNodo
            // 
            pnlTabPropiedadesNodo.Controls.Add(label1);
            pnlTabPropiedadesNodo.Location = new Point(0, 0);
            pnlTabPropiedadesNodo.Margin = new Padding(0, 0, 0, 5);
            pnlTabPropiedadesNodo.Name = "pnlTabPropiedadesNodo";
            pnlTabPropiedadesNodo.Size = new Size(30, 145);
            pnlTabPropiedadesNodo.TabIndex = 0;
            pnlTabPropiedadesNodo.Click += pnlTabPropiedadesNodo_Click;
            pnlTabPropiedadesNodo.Paint += pnlTabPropiedadesNodo_Paint;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(255, 111, 92);
            label1.Dock = DockStyle.Right;
            label1.Location = new Point(26, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(4, 145);
            label1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Location = new Point(0, 150);
            panel2.Margin = new Padding(0, 0, 0, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(30, 100);
            panel2.TabIndex = 1;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(56, 56, 56);
            label2.Dock = DockStyle.Right;
            label2.Location = new Point(26, 0);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(4, 100);
            label2.TabIndex = 1;
            // 
            // splitter1
            // 
            splitter1.BackColor = Color.FromArgb(46, 46, 46);
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(578, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(4, 534);
            splitter1.TabIndex = 3;
            splitter1.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1212, 534);
            Controls.Add(splitter1);
            Controls.Add(pnlDetalleNodo);
            Controls.Add(pnlTabsDerecha);
            Controls.Add(uscCanvas);
            KeyPreview = true;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NodeCode v1.0";
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            pnlDetalleNodo.ResumeLayout(false);
            pnlCabeceraNodo.ResumeLayout(false);
            pnlCabeceraNodo.PerformLayout();
            uscCanvas.ResumeLayout(false);
            pnlTabsDerecha.ResumeLayout(false);
            pnlTabPropiedadesNodo.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ImageList imageList1;
        private Panel pnlDetalleNodo;
        private Controls.uscNombreNodo userControl11;
        private Controls.NodeCanvas uscCanvas;
        private FlowLayoutPanel pnlTabsDerecha;
        private Panel pnlTabPropiedadesNodo;
        private Panel panel2;
        private Splitter splitter1;
        private Label label1;
        private Label label2;
        private Panel pnlCabeceraNodo;
        private Controls.uscDescripcionNodo uscDescripcionNodo;
        private Controls.uscNombreNodo uscNombreNodo;
        private Label lblTituloPanelPropiedades;
        private Label label4;
        private ImageList imageList2;
        private Button btnOcultarPanelPropiedades;
        private ScintillaNET.Scintilla scintilla;
        private Button button1;
    }
}