using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Orders.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Orders.Shared.Entities;

public class State : IEntityWithName
{
    public int Id { get; set; }

    [Display(Name = "Provincia")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    [JsonIgnore]
    [ValidateNever]
    public Country Country { get; set; } = null!;

    public ICollection<City>? Cities { get; set; }

    [Display(Name = "Ciudades")]
    public int CitiesNumber => Cities == null || Cities.Count == 0 ? 0 : Cities.Count;
}