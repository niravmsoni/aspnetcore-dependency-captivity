using DependencyInjection;
using DependencyInjection.Shared;
using DependencyInjection.Source;
using DependencyInjection.Target;
using DependencyInjection.Transformation.Transformations;
using DependencyInjection.Transformation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DependencyInjection.Util;

// Creating default builder for application host
using var host = Host.CreateDefaultBuilder(args)
    .UseDefaultServiceProvider((context, options) =>
    {
        //Validating if there are any straight forward captive dependencies present in code?
        options.ValidateScopes = true;
    })
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<Configuration>();
        services.AddTransient<IPriceParser, PriceParser>();
        services.AddTransient<IProductSource, ProductSource>();

        services.AddTransient<IProductFormatter, ProductFormatter>();
        services.AddTransient<IProductTarget, ProductTarget>();

        services.AddTransient<ProductImporter>();

        services.AddSingleton<IImportStatistics, ImportStatistics>();

        services.AddTransient<IProductTransformer, ProductTransformer>();

        services.AddScoped<IProductTransformationContext, ProductTransformationContext>();
        services.AddScoped<INameDecapitaliser, NameDecapitaliser>();
        services.AddScoped<ICurrencyNormalizer, CurrencyNormalizer>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IReferenceAdder, ReferenceAdder>();

        //Changing this from Singleton to Scoped since we have moved responsibility to generate counter in separate svc
        //services.AddSingleton<IReferenceGenerator, ReferenceGenerator>();
        services.AddScoped<IReferenceGenerator, ReferenceGenerator>();
        services.AddSingleton<IIncrementingCounter, IncrementingCounter>();

    })
    .Build();

//Here it's in resolving phase and code deals with class IServiceProvider
var productImporter = host.Services.GetRequiredService<ProductImporter>();

#region Testing instance creation using DI
//using var firstScope = host.Services.CreateScope();
//var resolvedOnce = firstScope.ServiceProvider.GetRequiredService<ProductImporter>();
//var resolvedTwice = firstScope.ServiceProvider.GetRequiredService<ProductImporter>();

////True
//var isSameInFirstScope = Object.ReferenceEquals(resolvedTwice, resolvedOnce);

//using var secondScope = host.Services.CreateScope();
//var resolvedThrice = secondScope.ServiceProvider.GetRequiredService<ProductImporter>();
//var resolvedFourth = secondScope.ServiceProvider.GetRequiredService<ProductImporter>();

////True
//var isSameInSecondScope = Object.ReferenceEquals(resolvedThrice, resolvedFourth);

////False - Since we're comparing cross scope
//var IsSameInCrossScope = Object.ReferenceEquals(resolvedOnce, resolvedFourth);

//Console.WriteLine(isSameInFirstScope);
//Console.WriteLine(isSameInSecondScope);
//Console.WriteLine(IsSameInCrossScope);
#endregion

productImporter.Run();