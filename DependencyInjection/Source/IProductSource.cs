using DependencyInjection.Model;

namespace DependencyInjection.Source;

public interface IProductSource
{
    void Open();
    bool hasMoreProducts();
    Product GetNextProduct();
    void Close();
}