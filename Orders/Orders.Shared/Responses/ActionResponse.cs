namespace Orders.Shared.Responses;

public class ActionResponse<T>
{
    //Si dio ok devolvemos true si fallo false
    public bool WasSuccess { get; set; }

    //se utiliza cuando falla y se devuelve el msj
    public string? Message { get; set; }

    //si no falla devuelve con las mismas propiedades de lo q mandamos
    public T? Result { get; set; }
}