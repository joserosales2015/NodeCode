using NodeCode.Controls;
using NodeCode.Models;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.Host.Mef;
using System.IO;
using System.Reflection;
using System.Composition;
using System.Composition.Hosting;


namespace NodeCode.Forms
{
    public partial class MainForm : Form
    {
        #region Variables y Propiedades
        private FlowNode? nodoSeleccionado = null;
        private System.Windows.Forms.Timer analysisTimer;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            InitializeComponents();
            ConfigurarTemaDark();
            ConfigurarAnalisisEnTiempoReal();
        }

        #region Metodos
        private void InitializeComponents()
        {
            var toolStrip = new ToolStrip { Dock = DockStyle.Top };

            var addNodeButton = new ToolStripButton("Agregar Nodo");
            //addNodeButton.Click += AddNodeButton_Click;

            var clearButton = new ToolStripButton("Limpiar Canvas");
            clearButton.Click += ClearButton_Click!;

            var helpLabel = new ToolStripLabel("   |   Clic izq: mover | Clic der: conectar | Doble clic: propiedades");

            toolStrip.Items.AddRange(new ToolStripItem[]
            {
                addNodeButton,
                clearButton,
                new ToolStripSeparator(),
                helpLabel
            });

            Controls.Add(toolStrip);

            // Agregar algunos nodos de ejemplo
            uscCanvas.AddNode("Inicio", "Inicia el flujo", new Point(100, 100), 0);
            uscCanvas.AddNode("Entrada", "Ingreso de datos", new Point(100, 200), 2);
            uscCanvas.AddNode("Proceso", "Ejecución de algoritmo", new Point(100, 300), 4);
            uscCanvas.AddNode("Salida", "Muestra resultados", new Point(100, 400), 5);
            uscCanvas.AddNode("Final", "Fin del flujo", new Point(100, 500), 1);
        }
        #endregion

        #region Eventos
        private void MainForm_Load(object sender, EventArgs e)
        {
            var tooltip = new ToolTip();
            Point lastMousePos = Point.Empty;

            scintilla.MouseMove += async (s, ev) =>
            {
                if (lastMousePos == ev.Location)
                    return;

                lastMousePos = ev.Location;

                int pos = scintilla.CharPositionFromPoint(ev.X, ev.Y);
                if (pos < 0 || pos >= scintilla.TextLength)
                    return;

                // Verificar si hay un indicador en esta posición
                var mask = scintilla.IndicatorAllOnFor(pos);
                if ((mask & (1 << 0)) != 0 || (mask & (1 << 1)) != 0)
                {
                    // Hay un error o advertencia, buscar el diagnóstico
                    string codigo = scintilla.Text;
                    var syntaxTree = CSharpSyntaxTree.ParseText(codigo);
                    var compilation = CSharpCompilation.Create("MiAnalisis")
                        .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                        .AddReferences(MetadataReference.CreateFromFile(typeof(Console).Assembly.Location))
                        .AddSyntaxTrees(syntaxTree);

                    var diagnostics = compilation.GetDiagnostics();

                    foreach (var diagnostic in diagnostics.Where(d => d.Location.IsInSource))
                    {
                        var span = diagnostic.Location.SourceSpan;
                        if (pos >= span.Start && pos <= span.Start + span.Length)
                        {
                            tooltip.Show(diagnostic.GetMessage(), scintilla, ev.Location.X, ev.Location.Y + 20, 3000);
                            break;
                        }
                    }
                }
                else
                {
                    tooltip.Hide(scintilla);
                }
            };
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro de que desea limpiar todo el canvas?", "Confirmar limpieza", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                uscCanvas.Nodes.Clear();
                pnlDetalleNodo.Visible = false;
                uscCanvas.Invalidate();
            }
        }

        private void uscCanvas_ClickNode(object sender, NodeCode.Classes.ClickNodeEventArgs e)
        {
            pnlDetalleNodo.Visible = true;
            pnlTabsDerecha.Visible = false;
            uscNombreNodo.Text = e.Node.Name;
            uscDescripcionNodo.Text = e.Node.Summary;
            nodoSeleccionado = e.Node;
        }

        private void uscCanvas_ClickCanvas(object sender, MouseEventArgs e)
        {
            pnlDetalleNodo.Visible = false;
            pnlTabsDerecha.Visible = true;
            nodoSeleccionado = null;
        }

        private void btnOcultarPanelPropiedades_Click(object sender, EventArgs e)
        {
            pnlDetalleNodo.Visible = false;
            pnlTabsDerecha.Visible = true;
        }

        private void uscNombreNodo_TextNodeChanged(object sender, EventArgs e)
        {
            if (nodoSeleccionado != null)
            {
                nodoSeleccionado.Name = uscNombreNodo.Text;
                uscCanvas.Invalidate();
            }
        }

        private void uscDescripcionNodo_TextNodeChanged(object sender, EventArgs e)
        {
            if (nodoSeleccionado != null)
            {
                nodoSeleccionado.Summary = uscDescripcionNodo.Text;
                uscCanvas.Invalidate();
            }
        }

        private void pnlTabPropiedadesNodo_Click(object sender, EventArgs e)
        {
            if (nodoSeleccionado != null)
            {
                pnlDetalleNodo.Visible = true;
                pnlTabsDerecha.Visible = false;
            }
        }

        private void pnlTabPropiedadesNodo_Paint(object sender, PaintEventArgs e)
        {
            string texto = "Propiedades del Nodo";
            Font fuente = new Font("Segoe UI", 9, FontStyle.Regular);
            Brush brush = Brushes.WhiteSmoke;
            Graphics g = e.Graphics;
            var estado = g.Save();

            g.TranslateTransform(pnlTabPropiedadesNodo.Width - 8, 13);
            g.RotateTransform(90);
            g.DrawString(texto, fuente, brush, 0, 0);
            g.Restore(estado);
        }

        private void scintilla_InsertCheck(object sender, InsertCheckEventArgs e)
        {
            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n") || e.Text.EndsWith("\r\n"))
            {
                int currentLine = scintilla.LineFromPosition(scintilla.CurrentPosition);
                string currentLineText = scintilla.Lines[currentLine].Text;

                // Calcular espacios de indentación de la línea actual
                int indent = 0;
                foreach (char c in currentLineText)
                {
                    if (c == ' ')
                        indent++;
                    else if (c == '\t')
                        indent += scintilla.TabWidth;
                    else
                        break;
                }

                // Si la línea termina con { agregar indentación extra
                string trimmedLine = currentLineText.Trim();
                if (trimmedLine.EndsWith("{"))
                {
                    indent += scintilla.TabWidth;
                }

                // Agregar la indentación al texto insertado
                e.Text += new string(' ', indent);
            }
        }

        private void scintilla_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == 2) // Margen de folding
            {
                int line = scintilla.LineFromPosition(e.Position);

                // Toggle fold
                if (scintilla.Lines[line].FoldLevelFlags.HasFlag(FoldLevelFlags.Header))
                {
                    scintilla.Lines[line].ToggleFold();
                }
            }
            MessageBox.Show("");
        }

        private void scintilla_CharAdded(object sender, CharAddedEventArgs e)
        {
            int currentPos = scintilla.CurrentPosition;

            // Mostrar miembros después de un punto
            if (e.Char == '.')
            {
                MostrarMiembrosDeObjeto(currentPos);
                return;
            }

            // Obtener palabra actual
            int wordStartPos = scintilla.WordStartPosition(currentPos, true);
            string currentWord = scintilla.GetTextRange(wordStartPos, currentPos - wordStartPos);

            // Mostrar autocompletado después de 2 caracteres
            if (currentWord.Length >= 2)
            {
                // Usar versión simple o con Roslyn
                //MostrarAutocompletado(currentWord, currentPos); // Simple
                MostrarAutocompletadoConRoslyn(currentWord, currentPos); // Avanzado
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Texto de ejemplo
            scintilla.Text = @"using System;

namespace MiApp
{
    class Program {
        
        static void Main(string[] args) {
            Console.WriteLine(""Hola Mundo"");
            
            // Error intencional: variable sin declarar
            int x = 10;
            
            // Advertencia: variable declarada pero no usada
            int y = 5;
            
            Sumar(x, y);
        }
        
        #region Metodos
        private static string Sumar (int a, int b) {
            
            return $""Resultado {a + b}"";
        }
        #endregion
    }
}";

        }
        #endregion

        private async Task AnalizarCodigo()
        {
            string codigo = scintilla.Text;

            // Limpiar indicadores anteriores
            scintilla.IndicatorCurrent = 0;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);
            scintilla.IndicatorCurrent = 1;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            await Task.Run(() =>
            {
                // Crear árbol sintáctico con Roslyn
                var syntaxTree = CSharpSyntaxTree.ParseText(codigo);

                // Obtener referencias de ensamblados del dominio actual
                var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

                var references = new List<MetadataReference>
                {
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Core.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Console.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Collections.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "netstandard.dll")),
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Console).Assembly.Location)
        };

                // Crear compilación con todas las referencias
                var compilation = CSharpCompilation.Create(
                    "MiAnalisis",
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.ConsoleApplication));

                // Obtener diagnósticos (errores y advertencias)
                var diagnostics = compilation.GetDiagnostics();

                // Procesar cada diagnóstico
                foreach (var diagnostic in diagnostics)
                {
                    if (diagnostic.Location.IsInSource)
                    {
                        var span = diagnostic.Location.SourceSpan;
                        int startPos = span.Start;
                        int length = span.Length;

                        // Invocar en el hilo de UI
                        scintilla.Invoke(new Action(() =>
                        {
                            if (diagnostic.Severity == DiagnosticSeverity.Error)
                            {
                                // Error: subrayado rojo
                                scintilla.IndicatorCurrent = 0;
                                scintilla.IndicatorFillRange(startPos, length);
                            }
                            else if (diagnostic.Severity == DiagnosticSeverity.Warning)
                            {
                                // Advertencia: subrayado verde
                                scintilla.IndicatorCurrent = 1;
                                scintilla.IndicatorFillRange(startPos, length);
                            }
                        }));
                    }
                }
            });
        }

        private static void CargarCodigoNodo(ref Scintilla editor, CodeNode nodo)
        {
            editor.Text = nodo.CodigoMetodo ?? "// Escribe tu código aquí\n";
        }

        private void ConfigurarAnalisisEnTiempoReal()
        {
            // Timer para analizar después de que el usuario deje de escribir
            analysisTimer = new System.Windows.Forms.Timer();
            analysisTimer.Interval = 500; // 500ms después de la última tecla
            analysisTimer.Tick += async (s, e) =>
            {
                analysisTimer.Stop();
                await AnalizarCodigo();
            };

            // Evento cuando cambia el texto
            scintilla.TextChanged += (s, e) =>
            {
                analysisTimer.Stop();
                analysisTimer.Start();
            };
        }

        private void ConfigurarTemaDark()
        {
            // Colores del tema oscuro
            Color fondoOscuro = Color.FromArgb(30, 30, 30);
            Color textoClaro = Color.FromArgb(220, 220, 220);
            Color seleccion = Color.FromArgb(51, 153, 255);
            Color margenOscuro = Color.FromArgb(45, 45, 48);
            Color numeroLinea = Color.FromArgb(43, 145, 175);

            // Configuración básica con tema oscuro
            scintilla.LexerName = "cpp";
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 12;
            scintilla.Styles[Style.Default].BackColor = fondoOscuro;
            scintilla.Styles[Style.Default].ForeColor = textoClaro;
            scintilla.StyleClearAll();

            // Colores de sintaxis para C# - Tema Oscuro (estilo VS Dark)
            scintilla.Styles[Style.Cpp.Default].ForeColor = textoClaro;
            scintilla.Styles[Style.Cpp.Default].BackColor = fondoOscuro;

            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(87, 166, 74); // Verde
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(87, 166, 74);
            scintilla.Styles[Style.Cpp.CommentDoc].ForeColor = Color.FromArgb(87, 166, 74);
            scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(87, 166, 74);

            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.FromArgb(181, 206, 168); // Verde claro

            scintilla.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(214, 157, 133); // Naranja claro
            scintilla.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(214, 157, 133);
            scintilla.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(214, 157, 133);

            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.FromArgb(86, 156, 214); // Azul (palabras clave)
            scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.FromArgb(78, 201, 176); // Cyan (tipos)

            scintilla.Styles[Style.Cpp.Operator].ForeColor = textoClaro;
            scintilla.Styles[Style.Cpp.Preprocessor].ForeColor = Color.FromArgb(155, 155, 155); // Gris

            scintilla.Styles[Style.Cpp.Identifier].ForeColor = textoClaro;

            // Palabras clave de C#
            scintilla.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while async await yield");
            scintilla.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void var dynamic");

            // Colores del editor
            scintilla.CaretForeColor = Color.White;
            scintilla.CaretLineBackColor = Color.FromArgb(100, 40, 40, 40);

            // Selección
            scintilla.SelectionBackColor = Color.FromArgb(38, 79, 120);
            scintilla.SelectionTextColor = Color.White;

            // Números de línea - Tema oscuro
            scintilla.Margins[0].Width = 40;
            scintilla.Margins[0].Type = MarginType.Number;
            scintilla.Margins[0].BackColor = margenOscuro;
            scintilla.Styles[Style.LineNumber].ForeColor = numeroLinea;
            scintilla.Styles[Style.LineNumber].BackColor = margenOscuro;

            // Code folding - Tema oscuro
            scintilla.Margins[2].Width = 20;
            scintilla.Margins[2].Type = MarginType.Symbol;
            scintilla.Margins[2].Mask = Marker.MaskFolders;
            scintilla.Margins[2].Sensitive = true;
            scintilla.Margins[2].BackColor = margenOscuro;

            scintilla.SetFoldMarginColor(true, margenOscuro);
            scintilla.SetFoldMarginHighlightColor(true, margenOscuro);

            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "1");

            // Configurar marcadores de folding - Tema oscuro
            for (int i = 25; i <= 31; i++)
            {
                scintilla.Markers[i].SetForeColor(Color.FromArgb(200, 200, 200));
                scintilla.Markers[i].SetBackColor(Color.FromArgb(80, 80, 80));
            }

            scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Habilitar eventos de margen para code folding
            scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;

            // Configuración de indentación
            scintilla.TabWidth = 4;
            scintilla.UseTabs = false;
            scintilla.IndentationGuides = IndentView.LookBoth;

            // Configurar indicadores para errores y advertencias
            scintilla.Indicators[0].Style = IndicatorStyle.Squiggle;
            scintilla.Indicators[0].ForeColor = Color.FromArgb(255, 100, 100); // Rojo más brillante

            scintilla.Indicators[1].Style = IndicatorStyle.Squiggle;
            scintilla.Indicators[1].ForeColor = Color.FromArgb(100, 200, 100); // Verde más brillante

            // Guías de indentación
            scintilla.Styles[Style.IndentGuide].ForeColor = Color.FromArgb(80, 80, 80);
            scintilla.Styles[Style.IndentGuide].BackColor = fondoOscuro;

            // Paréntesis y llaves coincidentes
            scintilla.Styles[Style.BraceLight].BackColor = Color.FromArgb(80, 80, 80);
            scintilla.Styles[Style.BraceLight].ForeColor = Color.White;
            scintilla.Styles[Style.BraceBad].ForeColor = Color.Red;

            // Configuración de autocompletado
            scintilla.AutoCMaxHeight = 10; // Altura máxima de la lista
            scintilla.AutoCAutoHide = true;
            scintilla.AutoCIgnoreCase = false;
            scintilla.AutoCOrder = Order.PerformSort;

            // Estilo del autocompletado para tema oscuro
            scintilla.SelectionBackColor = Color.FromArgb(51, 153, 255);
        }

        private void MostrarAutocompletado(string currentWord, int position)
        {
            // Lista de palabras clave y tipos comunes de C#
            var palabrasClave = new List<string>
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch",
        "char", "checked", "class", "const", "continue", "decimal", "default",
        "delegate", "do", "double", "else", "enum", "event", "explicit",
        "extern", "false", "finally", "fixed", "float", "for", "foreach",
        "goto", "if", "implicit", "in", "int", "interface", "internal",
        "is", "lock", "long", "namespace", "new", "null", "object",
        "operator", "out", "override", "params", "private", "protected",
        "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
        "sizeof", "stackalloc", "static", "string", "struct", "switch",
        "this", "throw", "true", "try", "typeof", "uint", "ulong",
        "unchecked", "unsafe", "ushort", "using", "virtual", "void",
        "volatile", "while", "async", "await", "var", "dynamic"
    };

            // Filtrar palabras que coincidan
            var sugerencias = palabrasClave
                .Where(p => p.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p)
                .ToList();

            if (sugerencias.Any())
            {
                // Mostrar lista de autocompletado
                scintilla.AutoCShow(currentWord.Length, string.Join(" ", sugerencias));
            }
        }
        
        private async void MostrarAutocompletadoConRoslyn(string currentWord, int position)
        {
            if (string.IsNullOrWhiteSpace(currentWord) || currentWord.Length < 2)
                return;

            try
            {
                string codigo = scintilla.Text;
                await Task.Run(async () =>
                {
                    // Crear árbol sintáctico
                    var syntaxTree = CSharpSyntaxTree.ParseText(codigo);

                    // Crear compilación
                    var compilation = CSharpCompilation.Create("AutocompletadoTemp")
                        .AddReferences(ObtenerReferencias())
                        .AddSyntaxTrees(syntaxTree);

                    // Obtener modelo semántico
                    var semanticModel = compilation.GetSemanticModel(syntaxTree);

                    // Obtener todas las palabras clave de C#
                    var palabrasClave = new HashSet<string>
                    {
                        "abstract", "as", "base", "bool", "break", "byte", "case", "catch",
                        "char", "checked", "class", "const", "continue", "decimal", "default",
                        "delegate", "do", "double", "else", "enum", "event", "explicit",
                        "extern", "false", "finally", "fixed", "float", "for", "foreach",
                        "goto", "if", "implicit", "in", "int", "interface", "internal",
                        "is", "lock", "long", "namespace", "new", "null", "object",
                        "operator", "out", "override", "params", "private", "protected",
                        "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
                        "sizeof", "stackalloc", "static", "string", "struct", "switch",
                        "this", "throw", "true", "try", "typeof", "uint", "ulong",
                        "unchecked", "unsafe", "ushort", "using", "virtual", "void",
                        "volatile", "while", "async", "await", "var", "dynamic", "yield"
                    };

                    // Obtener símbolos disponibles en el contexto actual
                    var root = await syntaxTree.GetRootAsync();
                    var token = root.FindToken(position);
                    var node = token.Parent;

                    // Obtener símbolos en el alcance
                    var symbols = semanticModel.LookupSymbols(position);

                    // Agregar nombres de símbolos
                    var nombresSimbolos = symbols
                        .Select(s => s.Name)
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .ToHashSet();

                    // Combinar palabras clave y símbolos
                    var todasLasSugerencias = palabrasClave
                        .Union(nombresSimbolos)
                        .Where(s => s.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(s => s)
                        .Take(50)
                        .ToList();

                    if (todasLasSugerencias.Any())
                    {
                        scintilla.Invoke(new Action(() =>
                        {
                            scintilla.AutoCShow(currentWord.Length, string.Join(" ", todasLasSugerencias));
                        }));
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en autocompletado: {ex.Message}");
            }
        }

        private async void MostrarMiembrosDeObjeto(int position)
        {
            try
            {
                string codigo = scintilla.Text;

                await Task.Run(async () =>
                {
                    // Crear árbol sintáctico
                    var syntaxTree = CSharpSyntaxTree.ParseText(codigo);

                    // Crear compilación
                    var compilation = CSharpCompilation.Create("MiembrosTemp")
                        .AddReferences(ObtenerReferencias())
                        .AddSyntaxTrees(syntaxTree);

                    // Obtener modelo semántico
                    var semanticModel = compilation.GetSemanticModel(syntaxTree);

                    var root = await syntaxTree.GetRootAsync();

                    // Buscar el nodo antes del punto
                    var token = root.FindToken(position - 1);
                    if (token.Parent == null)
                        return;

                    // Buscar el nodo de acceso a miembro
                    var memberAccessNode = token.Parent.AncestorsAndSelf()
                        .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax>()
                        .FirstOrDefault();

                    if (memberAccessNode == null)
                    {
                        // Intentar con identificador simple
                        var identifierNode = token.Parent.AncestorsAndSelf()
                            .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax>()
                            .FirstOrDefault();

                        if (identifierNode != null)
                        {
                            var symbolInfo = semanticModel.GetSymbolInfo(identifierNode);
                            if (symbolInfo.Symbol != null)
                            {
                                MostrarMiembrosDelTipo(symbolInfo.Symbol.GetType().Name);
                                return;
                            }
                        }

                        return;
                    }

                    // Obtener el símbolo del objeto antes del punto
                    var expressionBeforeDot = memberAccessNode.Expression;
                    var typeInfo = semanticModel.GetTypeInfo(expressionBeforeDot);

                    if (typeInfo.Type != null)
                    {
                        // Obtener todos los miembros del tipo
                        var miembros = typeInfo.Type.GetMembers()
                            .Where(m => m.DeclaredAccessibility == Microsoft.CodeAnalysis.Accessibility.Public)
                            .Where(m => !m.IsImplicitlyDeclared)
                            .Select(m => m.Name)
                            .Distinct()
                            .OrderBy(n => n)
                            .ToList();

                        if (miembros.Any())
                        {
                            scintilla.Invoke(new Action(() =>
                            {
                                scintilla.AutoCShow(0, string.Join(" ", miembros));
                            }));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error mostrando miembros: {ex.Message}");
            }
        }

        private void MostrarMiembrosDelTipo(string tipoNombre)
        {
            // Para tipos comunes conocidos
            var miembrosComunes = new Dictionary<string, List<string>>
            {
                ["String"] = new List<string>
                {
                    "Length", "ToUpper", "ToLower", "Substring", "Contains",
                    "Replace", "Split", "Trim", "StartsWith", "EndsWith", "IndexOf"
                },
                ["Console"] = new List<string>
                {
                    "WriteLine", "Write", "ReadLine", "ReadKey", "Clear",
                    "ForegroundColor", "BackgroundColor", "Title"
                },
                ["Int32"] = new List<string>
                {
                    "ToString", "Parse", "TryParse", "MaxValue", "MinValue"
                }
            };

            if (miembrosComunes.ContainsKey(tipoNombre))
            {
                scintilla.Invoke(new Action(() =>
                {
                    scintilla.AutoCShow(0, string.Join(" ", miembrosComunes[tipoNombre]));
                }));
            }
        }

        private List<MetadataReference> ObtenerReferencias()
        {
            // Esta es la forma más simple y funciona en .NET Framework, .NET Core, .NET 5-8
            var referencias = new List<MetadataReference>
            {
                // Referencia básica (el más importante)
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            };

            // Agregar todas las referencias del dominio actual
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location))
                {
                    try
                    {
                        referencias.Add(MetadataReference.CreateFromFile(assembly.Location));
                    }
                    catch
                    {
                        // Ignorar ensamblados que no se puedan cargar
                    }
                }
            }

            return referencias;
        }

        private static void ParsearParametros(ref CodeNode nodo, string texto)
        {
            nodo.Parametros.Clear();

            if (string.IsNullOrWhiteSpace(texto))
                return;

            var partes = texto.Split(',');
            foreach (var parte in partes)
            {
                var limpio = parte.Trim();
                var tokens = limpio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length >= 2)
                {
                    nodo.Parametros.Add(new ParametroMetodo
                    {
                        Tipo = tokens[0],
                        Nombre = tokens[1]
                    });
                }
            }
        }

        

        
    }


}
