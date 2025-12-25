using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact(DisplayName = "Create Product With Valid Parameters")]
    public void CreateProduct_WithValidParamenters_ResultOkObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 10.0m, 10, "image.jpg");
        action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }



    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "Product Description", 10.0m, 10, "image.jpg");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
        .WithMessage("Invalid Id value");
    }


    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Description", 10.0m, 10, "image.jpg");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
        .WithMessage("Invalid name, too short, minimum 3 charecters");
    }



    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () => new Product(1, "Prduct name", "Product Description", 10.0m, 10, "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...image.jpg");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
        .WithMessage("Invalid image name, too long, maximum 250 charecters");
    }


    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Product name", "Product Description", 10.0m, 10, null);
        action.Should().NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }


    [Fact]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () => new Product(1, "Product name", "Product Description", 10.0m, 10, null);
        action.Should().NotThrow<NullReferenceException>();
    }


    [Fact]
    public void CreateProduct_InvalidPriceValue_DomainException()
    {
        Action action = () => new Product(1, "Product name", "Product Description", -10.0m, 10, "imagem test");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid price value");
    }




    [Theory]
    [InlineData(-5)]
    public void CreateProduct_WithIdStockValue_DomainExceptionNegativeValue(int value)
    {
        Action action = () => new Product(1, "Product name", "Product Description", 10.0m, value, "product imagem");
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid stock value");
    }

}
