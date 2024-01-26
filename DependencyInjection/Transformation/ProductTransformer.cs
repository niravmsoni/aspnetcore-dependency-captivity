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

            // Ideally, not right way to resolve classes from container. This is called Service Locator pattern. Not recommended

            // Since they are used within SCOPE - IT IS IMPORTANT TO INSTANTIATE THEM(All 3 - Context and transformations) IN SUCH A WAY THAT THEY ARE REUSED WITHIN SCOPE
            // It can't be singleton since we don't need to share the same context for different products
            // Hence scoped
            var transformationContext = scope.ServiceProvider.GetRequiredService<IProductTransformationContext>();
            
            // Setting product in context
            transformationContext.SetProduct(product);

            var nameCapitalizer = scope.ServiceProvider.GetRequiredService<INameDecapitaliser>();
            var currencyNormalizer = scope.ServiceProvider.GetRequiredService<ICurrencyNormalizer>();

            nameCapitalizer.Execute();
            currencyNormalizer.Execute();

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
