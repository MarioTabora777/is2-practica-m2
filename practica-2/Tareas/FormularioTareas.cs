using System.ComponentModel;

namespace Tareas;

public class FormularioTareas : Form
{
    private readonly BindingList<Tarea> tareas = new();
    private readonly TextBox entradaDescripcion = new() { Width = 350, MaxLength = 80 };
    private readonly ListBox listaTareas = new() { Dock = DockStyle.Fill };
    private readonly Label resumen = new() { AutoSize = true };

    public FormularioTareas()
    {
        SuspendLayout();
        Text = "Mis tareas";
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10);
        ClientSize = new Size(560, 370);

        var botonAgregar = new Button { Text = "Agregar", AutoSize = true };
        var botonCompletar = new Button { Text = "Completar", AutoSize = true };
        var botonEliminar = new Button { Text = "Eliminar", AutoSize = true };
        botonAgregar.Click += (_, _) => AgregarTarea();
        botonCompletar.Click += (_, _) => CompletarTareaSeleccionada();
        botonEliminar.Click += (_, _) => EliminarTareaSeleccionada();

        var panelEntrada = new FlowLayoutPanel { Dock = DockStyle.Fill };
        panelEntrada.Controls.AddRange([entradaDescripcion, botonAgregar]);

        var panelAcciones = new FlowLayoutPanel { Dock = DockStyle.Fill };
        panelAcciones.Controls.AddRange([botonCompletar, botonEliminar]);

        var contenido = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(16),
            RowCount = 5,
            ColumnCount = 1
        };
        contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 24));
        contenido.Controls.Add(new Label { Text = "Descripción de la tarea", AutoSize = true }, 0, 0);
        contenido.Controls.Add(panelEntrada, 0, 1);
        contenido.Controls.Add(listaTareas, 0, 2);
        contenido.Controls.Add(panelAcciones, 0, 3);
        contenido.Controls.Add(resumen, 0, 4);
        Controls.Add(contenido);

        listaTareas.DataSource = tareas;
        AcceptButton = botonAgregar;
        ActualizarResumen();
        AutoScaleDimensions = new SizeF(96, 96);
        AutoScaleMode = AutoScaleMode.Dpi;
        ResumeLayout(true);
    }

    private void AgregarTarea()
    {
        if (string.IsNullOrWhiteSpace(entradaDescripcion.Text))
        {
            MessageBox.Show("Escribe una descripción.", "Mis tareas");
            entradaDescripcion.Focus();
            return;
        }

        tareas.Add(new Tarea(entradaDescripcion.Text));
        entradaDescripcion.Clear();
        entradaDescripcion.Focus();
        ActualizarResumen();
    }

    private void CompletarTareaSeleccionada()
    {
        if (listaTareas.SelectedItem is not Tarea tareaSeleccionada)
            return;

        tareaSeleccionada.Completar();
        tareas.ResetBindings();
        ActualizarResumen();
    }

    private void EliminarTareaSeleccionada()
    {
        if (listaTareas.SelectedItem is not Tarea tareaSeleccionada)
            return;

        tareas.Remove(tareaSeleccionada);
        ActualizarResumen();
    }

    private void InitializeComponent()
    {

    }

    private void ActualizarResumen()
    {
        int cantidadCompletadas = tareas.Count(tarea => tarea.EstaCompletada);
        int cantidadPendientes = tareas.Count - cantidadCompletadas;
        resumen.Text = $"Pendientes: {cantidadPendientes}  |  Completadas: {cantidadCompletadas}";
    }
}
