using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Category
{
    public int CategoryId { get; private set; }
    public string? Name { get; private set; }


    public Category(string name)
    {
        ValidationDomain(name);
    }


    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value");
        CategoryId = id;
        ValidationDomain(name);
    }


    public void Update(string name)
    {
        ValidationDomain(name);
    }
    


    //uma categoria tem um ou muitos produtos
    public ICollection<Product>? Products { get; set; }

    private void ValidationDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
        "Invalid name. Name is required");

        DomainExceptionValidation.When(name.Length < 3,
       "Invalid name, too short, minimum 3 charecters");

        Name = name;
    }
}
