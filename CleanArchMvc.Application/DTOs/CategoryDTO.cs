

using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "The Name is required")]
    [MinLength(3, ErrorMessage = "The Name must have at least 3 characters")]
    [MaxLength(100, ErrorMessage = "The Name must have a maximum of 100 characters")]
    public string? Name { get; set; }
}
