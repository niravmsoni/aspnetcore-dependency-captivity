using DependencyInjection.Model;

namespace DependencyInjection.Transformation.Transformations
{
    /// <summary>
    /// Responsible for applying currency related transformation on Product
    /// </summary>
    public class CurrencyNormalizer : ICurrencyNormalizer
    {
        private readonly IProductTransformationContext _productTransformationContext;

        public CurrencyNormalizer(IProductTransformationContext productTransformationContext)
        {
            _productTransformationContext = productTransformationContext;
        }

        public void Execute()
        {
            //Gets product from context
            var product = _productTransformationContext.GetProduct();

            if (product.Price.IsoCurrency == Money.USD)
            {
                var newPrice = new Money(Money.EUR, product.Price.Amount * Money.USDToEURRate);
                var newProduct = new Product(product.Id, product.Name, newPrice, product.Stock);

                //Writing product to the context
                _productTransformationContext.SetProduct(newProduct);
            }
        }
    }
}
