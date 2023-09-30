using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.CustomValidators;

public class InvoicePriceValidatorAttribute : ValidationAttribute
{
    private const double Tolerance = 0.0001;

    private static string DefaultErrorMessage =>
        "Invoice Price should be equal to the total cost of all products (i.e. {0}) in the order.";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //check if the value of "InvoicePrice" property is not null
        if (value == null) return null;
        //get "Products" property reference, using reflection
        var otherProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
        if (otherProperty == null) return null;
        //get value of "Products" property of the current object of "Order" class
        var
            products = (List<Product>)otherProperty.GetValue(validationContext
                .ObjectInstance)!; //"!" operator specifies that the value returned by GetValue() will not be null

        //calculate total price
        var totalPrice = products.Sum(product => product.Price * product.Quantity);

        //value of "InvoicePrice" property
        var actualPrice = (double)value;

        return totalPrice > 0
            ?
            //return model error is no products found
            !(Math.Abs(totalPrice - actualPrice) > Tolerance)
                ?
                //return model error
                ValidationResult.Success
                :
                //No validation error
                new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, totalPrice),
                    new[] { nameof(validationContext.MemberName) })
            :
            //if the value of "InvoicePrice" property is not equal to the total cost of all products in the order
            new ValidationResult("No products found to validate invoice price",
                new[] { nameof(validationContext.MemberName) });
    }
}