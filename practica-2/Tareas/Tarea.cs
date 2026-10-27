namespace Tareas;

public class Tarea
{
    public string Descripcion { get; }
    public bool EstaCompletada { get; private set; }

    public Tarea(string descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("Escribe una descripción.", nameof(descripcion));

        Descripcion = descripcion.Trim();
    }

    public void Completar()
    {
        EstaCompletada = true;
    }

    public override string ToString()
    {
        string estado = EstaCompletada ? "Completada" : "Pendiente";
        return $"[{estado}] {Descripcion}";
    }
}
