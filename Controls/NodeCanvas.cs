using NodeCode.Classes;
using NodeCode.Models;
using NodeCode.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NodeCode.Controls
{
    public class NodeCanvas : Panel
    {
        private List<FlowNode> nodes = new List<FlowNode>();
        private FlowNode selectedNode = null;
        private FlowNode draggedNode = null;
        private Point dragOffset;
        private FlowNode connectionSourceNode = null;
        private Point currentMousePos;
        private bool isConnecting = false;
        private NodeRenderer renderer;
        private NodeConnection selectedConnection = null;
        private const int GridSize = 16; // Tamaño del grid en píxeles

        public List<FlowNode> Nodes 
        { 
            get { return nodes; }
        }
        
        [Browsable(true)]
        [Category("Propiedades Personalizadas")]
        [Description("Lista de imágenes para mostrar en los nodos.")]
        public ImageList NodeIcons { get; set; }



        public event EventHandler<ClickNodeEventArgs> ClickNode;
        public event EventHandler<MouseEventArgs> ClickCanvas;

        public NodeCanvas()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(45, 46, 46);
            renderer = new NodeRenderer();

            // Habilitar el canvas para recibir eventos de teclado
            this.TabStop = true;
            this.Focus();

            MouseDown += NodeCanvas_MouseDown;
            MouseMove += NodeCanvas_MouseMove;
            MouseUp += NodeCanvas_MouseUp;
            Paint += NodeCanvas_Paint;
            KeyDown += NodeCanvas_KeyDown;
        }

        private Point SnapToGrid(Point point)
        {
            int x = (int)Math.Round(point.X / (double)GridSize) * GridSize;
            int y = (int)Math.Round(point.Y / (double)GridSize) * GridSize;
            return new Point(x, y);
        }

        public void AddNode(string name, string description, Point position, int iconIndex = -1)
        {
            var node = new FlowNode(name, description, SnapToGrid(position), iconIndex);
            nodes.Add(node);
            Invalidate();
        }

        private void NodeCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // Asegurar que el canvas tenga el foco para capturar eventos de teclado
            this.Focus();

            // Primero verificar si se hizo clic en un nodo
            bool nodeClicked = false;

            foreach (var node in nodes.AsEnumerable().Reverse())
            {
                if (node.GetBounds().Contains(e.Location))
                {
                    selectedNode = node;
                    selectedConnection = null; // Deseleccionar conexión
                    nodeClicked = true;

                    if (e.Clicks == 2)
                    {
                        ClickNode?.Invoke(this, new ClickNodeEventArgs(node));
                        return;
                    }

                    if (e.Button == MouseButtons.Right)
                    {
                        connectionSourceNode = node;
                        isConnecting = true;
                        currentMousePos = e.Location;
                        return;
                    }

                    if (e.Button == MouseButtons.Left)
                    {
                        draggedNode = node;
                        dragOffset = new Point(e.X - node.Position.X, e.Y - node.Position.Y);
                    }

                    Invalidate();
                    return;
                }
            }

            // Si no se hizo clic en un nodo, verificar si se hizo clic en una conexión
            if (!nodeClicked && e.Button == MouseButtons.Left)
            {
                foreach (var node in nodes)
                {
                    foreach (var connectedNode in node.Connections)
                    {
                        var connection = new NodeConnection(node, connectedNode);
                        if (connection.IsNearConnection(e.Location))
                        {
                            selectedConnection = connection;
                            selectedNode = null; // Deseleccionar nodo
                            Invalidate();
                            return;
                        }
                    }
                }
            }

            // Si no se hizo clic en nada, deseleccionar todo
            selectedNode = null;
            selectedConnection = null;
            Invalidate();
            ClickCanvas?.Invoke(this, e);
        }

        private void NodeCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            // Eliminar conexión seleccionada con la tecla Supr
            if (e.KeyCode == Keys.Delete && selectedConnection != null)
            {
                // Buscar y eliminar la conexión
                if (selectedConnection.SourceNode.Connections.Contains(selectedConnection.TargetNode))
                {
                    selectedConnection.SourceNode.Connections.Remove(selectedConnection.TargetNode);
                    selectedConnection = null;
                    Invalidate();
                }
            }
        }

        private void NodeCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedNode != null && e.Button == MouseButtons.Left)
            {
                Point newPosition = new Point(e.X - dragOffset.X, e.Y - dragOffset.Y);
                draggedNode.Position = SnapToGrid(newPosition);
                Invalidate();
            }

            if (isConnecting)
            {
                currentMousePos = e.Location;
                Invalidate();
            }
        }

        private void NodeCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isConnecting && connectionSourceNode != null)
            {
                foreach (var node in nodes)
                {
                    if (node != connectionSourceNode && node.GetBounds().Contains(e.Location))
                    {
                        if (!connectionSourceNode.Connections.Contains(node))
                        {
                            connectionSourceNode.Connections.Add(node);
                        }
                        break;
                    }
                }

                isConnecting = false;
                connectionSourceNode = null;
                Invalidate();
            }

            draggedNode = null;
        }

        private void NodeCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Dibujar el grid de puntos
            DrawGrid(e.Graphics);

            foreach (var node in nodes)
            {
                foreach (var connectedNode in node.Connections)
                {
                    // Verificar si esta conexión está seleccionada
                    bool isSelected = selectedConnection != null &&
                                     selectedConnection.SourceNode == node &&
                                     selectedConnection.TargetNode == connectedNode;

                    renderer.DrawConnection(e.Graphics, node.GetOutputPoint(), connectedNode.GetInputPoint(), false, isSelected);
                }
            }

            if (isConnecting && connectionSourceNode != null)
            {
                renderer.DrawConnection(e.Graphics, connectionSourceNode.GetOutputPoint(), currentMousePos, true);
            }

            foreach (var node in nodes)
            {
                renderer.DrawNode(e.Graphics, node, node == selectedNode, NodeIcons);
            }
        }

        private void DrawGrid(Graphics g)
        {
            using (var brush = new SolidBrush(Color.FromArgb(150, 150, 150)))
            {
                for (int x = 0; x < this.Width; x += GridSize)
                {
                    for (int y = 0; y < this.Height; y += GridSize)
                    {
                        g.FillRectangle(brush, x, y, 1, 1);
                    }
                }
            }
        }

        private void ShowNodeProperties(FlowNode node)
        {
            var dialog = new NodePropertiesDialog(node);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Invalidate();
            }

            if (dialog.NodeDeleted)
            {
                nodes.Remove(node);
                foreach (var n in nodes)
                {
                    n.Connections.Remove(node);
                }
                Invalidate();
            }
        }

        public void DeleteSelectedConnection()
        {
            if (selectedNode != null && selectedNode.Connections.Count > 0)
            {
                selectedNode.Connections.RemoveAt(selectedNode.Connections.Count - 1);
                Invalidate();
            }
        }
    }
}