using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Orders.Frontend.Repositories;

public class HttpResponseWrapper<T>
{
    public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
    {
        Response = response;
        Error = error;
        HttpResponseMessage = httpResponseMessage;
    }

    public T? Response { get; }
    public bool Error { get; }
    public HttpResponseMessage HttpResponseMessage { get; }

    public async Task<string?> GetErrorMessageAsync()
    {
        if (!Error)
        {
            return null;
        }

        var statusCode = HttpResponseMessage.StatusCode;
        if (statusCode == HttpStatusCode.NotFound)
        {
            return "Recurso no encontrado.";
        }
        if (statusCode == HttpStatusCode.BadRequest)
        {
            var content = await HttpResponseMessage.Content.ReadAsStringAsync();
            return ParseBadRequestContent(content);
        }
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            return "Tienes que estar logueado para ejecutar esta operación.";
        }
        if (statusCode == HttpStatusCode.Forbidden)
        {
            return "No tienes permisos para hacer esta operación.";
        }

        return "Ha ocurrido un error inesperado.";
    }

    private static string ParseBadRequestContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return "Solicitud incorrecta.";
        }

        try
        {
            // Intentar deserializar como ValidationProblemDetails (errores de DataAnnotations / FluentValidation)
            var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (problemDetails?.Errors != null && problemDetails.Errors.Count > 0)
            {
                // Extrae el primer mensaje de error encontrado en la lista
                return problemDetails.Errors.FirstOrDefault().Value?.FirstOrDefault() ?? "Error de validación.";
            }

            if (!string.IsNullOrEmpty(problemDetails?.Detail))
            {
                return problemDetails.Detail;
            }
        }
        catch (JsonException)
        {
            // Si no es un JSON estructurado, devuelve el string directo
            return content;
        }

        return content;
    }
}