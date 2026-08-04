using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Orders.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Orders.Shared.Entities;

public class City : IEntityWithName
{
    public int Id { get; set; }

    [Display(Name = "Ciudad")]
    [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Name { get; set; } = null!;

    public int StateId { get; set; }

    [JsonIgnore]
    [ValidateNever]
    public State? State { get; set; } = null!;

    public ICollection<User>? Users { get; set; }
}