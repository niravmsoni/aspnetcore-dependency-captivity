using DependencyInjection.Model;
using DependencyInjection.Shared;
using DependencyInjection.Transformation.Transformations;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Transformation
{
    /// <summary>
    /// Core service that is responsible for applying transformations
    /// </summary>
    public class ProductTransformer : IProductTransformer
    {
        // This class has dependency on 3 services - IProductTransformationContext, INameDecapitalizer and ICurrencyNormalizer
        // If we inject them directly to ctor for ProductTransformer, they will be reused throughout lifetime of ProductTransformer i.e. instantiated once
        // Instead, we should tie them to lifetime of product we are transforming. This is where we could use Scoped lifetime
        // Creating scope here with IServiceScopeFactory
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IImportStatistics _importStatistics;

        public ProductTransformer(IServiceScopeFactory serviceScopeFactory,
            IImportStatistics importStatistics)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _importStatistics = importStatistics;
        }
        public Product ApplyTransformations(Product product)
        {
            // Need to wrap this with using since IServiceScope implements IDisposable
            using var scope = _serviceScopeFactory.CreateScope();
            var transformationContext = scope.ServiceProvider.GetRequiredService<IProductTransformationContext>();
            
            // Setting product in context
            transformationContext.SetProduct(product);

            var nameCapitalizer = scope.ServiceProvider.GetRequiredService<INameDecapitaliser>();
            var currencyNormalizer = scope.ServiceProvider.GetRequiredService<ICurrencyNormalizer>();
            var referenceAdder = scope.ServiceProvider.GetRequiredService<IReferenceAdder>();

            nameCapitalizer.Execute();
            currencyNormalizer.Execute();
            referenceAdder.Execute();

            //For slowing things down and so that we are able to generate new time from IDateTimeProvider
            Thread.Sleep(2000);

            // Check if prodyct has changed, if so increment count
            if (transformationContext.IsProductChanged())
            {
                _importStatistics.IncrementTransformationCount();
            }

            // Return product from context
            return transformationContext.GetProduct();
        }
    }
}
