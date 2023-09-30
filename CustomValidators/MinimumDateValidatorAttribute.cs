using System.ComponentModel.DataAnnotations;

namespace MyFirstDotNetCoreApp.CustomValidators;

public class MinimumDateValidatorAttribute : ValidationAttribute
{
    private static string DefaultErrorMessage => "Order date should be greater than or equal to {0}";

    private DateTime MinimumDate { get; }

    public MinimumDateValidatorAttribute(string minimumDateString)
    {
        //According to CS0181 rule, we can't use DateTime data type as one of the parameter types in an attribute class
        MinimumDate = Convert.ToDateTime(minimumDateString);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //check if the value of "OrderDate" property is not null
        if (value == null) return null;
        //get the value of "OrderDate" property
        var orderDate = (DateTime)value;

        //if the value of "OrderDate" property is greater than minimumDate
        return orderDate >= MinimumDate
            ?
            //return validation error
            ValidationResult.Success
            :
            //No validation error
            new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumDate.ToString("yyyy-MM-dd")),
                new[] { nameof(validationContext.MemberName) });
    }
}