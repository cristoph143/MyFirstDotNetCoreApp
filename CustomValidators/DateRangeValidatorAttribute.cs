using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MyFirstDotNetCoreApp.CustomValidators;

public class DateRangeValidatorAttribute : ValidationAttribute
{
    private string OtherPropertyName { get; set; }

    public DateRangeValidatorAttribute(string otherPropertyName)
    {
        OtherPropertyName = otherPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return null;
        //get to_date
        var toDate = Convert.ToDateTime(value);

        //get from_date
        var otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

        if (otherProperty == null) return null;
        var fromDate = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

        return fromDate > toDate
            ? new ValidationResult(ErrorMessage, new[] { OtherPropertyName, validationContext.MemberName }!)
            : ValidationResult.Success;
    }
}