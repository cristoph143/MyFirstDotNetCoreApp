using System.ComponentModel.DataAnnotations;

namespace MyFirstDotNetCoreApp.CustomValidators;

public class MinimumYearValidatorAttribute: ValidationAttribute
{
    private int MinimumYear { get; set; } = 2000;
    private string DefaultErrorMessage { get; set; } = "Year should not be less than {0}";
    
    //parameterized constructor
    public MinimumYearValidatorAttribute(){}

    //parameterized constructor
    public MinimumYearValidatorAttribute(int minimumYear) => MinimumYear = minimumYear;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return null;
        DateTime date = (DateTime)value;
        return date.Year <= MinimumYear
            ? new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear))
            : ValidationResult.Success;
    }
}