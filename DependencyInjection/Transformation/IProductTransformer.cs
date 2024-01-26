using DependencyInjection.Model;

namespace DependencyInjection.Transformation
{
    public interface IProductTransformer
    {
        Product ApplyTransformations(Product product);
    }

}
