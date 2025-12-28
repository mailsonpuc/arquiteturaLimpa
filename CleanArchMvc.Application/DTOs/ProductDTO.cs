

using System.ComponentModel.DataAnnotations;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "The Name is required")]
    [MinLength(3, ErrorMessage = "The Name must have at least 3 characters")]
    [MaxLength(100, ErrorMessage = "The Name must have a maximum of 100 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Description is required")]
    [MinLength(5, ErrorMessage = "The Description must have at least 5 characters")]
    [MaxLength(200, ErrorMessage = "The Description must have a maximum of 200 characters")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "The Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be a positive value")]
    // [DataFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The Stock is required")]
    [Range(0, int.MaxValue, ErrorMessage = "The Stock must be a positive value")]
    public int Stock { get; set; }

    [MaxLength(250, ErrorMessage = "The Image must have a maximum of 250 characters")]
    public string? Image { get; set; }


    //produto esta relacionado com uma categoria
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
