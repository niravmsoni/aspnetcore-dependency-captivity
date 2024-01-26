using DependencyInjection.Model;

namespace DependencyInjection.Target;

public interface IProductTarget
{
    void Open();
    void AddProduct(Product product);
    void Close();
}
