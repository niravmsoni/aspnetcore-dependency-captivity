using DependencyInjection.Model;

namespace DependencyInjection.Target;

public interface IProductFormatter
{
    string Format(Product product);
    string GetHeaderLine();
}
