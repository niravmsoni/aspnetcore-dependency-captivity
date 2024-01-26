using DependencyInjection.Model;

namespace DependencyInjection.Transformation.Transformations
{
    /// <summary>
    /// Responsible for applying case related transformation on Prodyct
    /// </summary>
    public class NameDecapitaliser : INameDecapitaliser
    {
        private readonly IProductTransformationContext _productTransformationContext;

        public NameDecapitaliser(IProductTransformationContext productTransformationContext)
        {
            _productTransformationContext = productTransformationContext;
        }

        public void Execute()
        {
            //Gets product from context
            var product = _productTransformationContext.GetProduct();

            if (product.Name.Any(x => char.IsUpper(x)))
            {
                var newProduct = new Product(product.Id, product.Name.ToLowerInvariant(), product.Price, product.Stock);

                //Setting the product back to the context
                _productTransformationContext.SetProduct(newProduct);
            }
        }
    }
}
