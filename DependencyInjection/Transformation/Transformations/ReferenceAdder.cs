using DependencyInjection.Model;
using DependencyInjection.Util;

namespace DependencyInjection.Transformation.Transformations
{
    public class ReferenceAdder : IReferenceAdder
    {
        private readonly IProductTransformationContext _productTransformationContext;
        private readonly IReferenceGenerator _refenceGenerator;

        public ReferenceAdder(IProductTransformationContext productTransformationContext,
            IReferenceGenerator refenceGenerator)
        {
            _productTransformationContext = productTransformationContext;
            _refenceGenerator = refenceGenerator;
        }

        public void Execute()
        {
            var product = _productTransformationContext.GetProduct();

            var reference = _refenceGenerator.GetReference();

            var newProduct = new Product(product.Id, product.Name.ToLowerInvariant(), product.Price, product.Stock, reference);

            _productTransformationContext.SetProduct(newProduct);
        }
    }
}
