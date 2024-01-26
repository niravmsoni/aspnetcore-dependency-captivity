using DependencyInjection.Model;

namespace DependencyInjection.Transformation
{
    public interface IProductTransformationContext
    {
        void SetProduct(Product product);
        public Product GetProduct();
        bool IsProductChanged();
    }
}
