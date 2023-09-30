using System.ComponentModel.DataAnnotations;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.CustomValidators;

public class ProductsListValidatorAttribute : ValidationAttribute
{
    private static string DefaultErrorMessage => "Order should have at least one product";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //check if the value of "Products" property is not null
        if (value == null) return null;
        var products = (List<Product>)value;

        //if no products
        return products.Count == 0
            ?
            //return validation error
            new ValidationResult(DefaultErrorMessage, new[] { nameof(validationContext.MemberName) })
            :
            //No validation error
            ValidationResult.Success;
    }
}