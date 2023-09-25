using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.CustomModelBinders;

public class PersonBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        return context.Metadata.ModelType == typeof(Person) ? new BinderTypeModelBinder(typeof(PersonModelBinder)) : null;
    }
}
